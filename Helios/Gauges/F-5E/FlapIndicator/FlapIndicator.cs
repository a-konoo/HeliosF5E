//  Copyright 2014 Craig Courtney
//    
//  Helios is free software: you can redistribute it and/or modify
//  it under the terms of the GNU General Public License as published by
//  the Free Software Foundation, either version 3 of the License, or
//  (at your option) any later version.
//
//  Helios is distributed in the hope that it will be useful,
//  but WITHOUT ANY WARRANTY; without even the implied warranty of
//  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//  GNU General Public License for more details.
//
//  You should have received a copy of the GNU General Public License
//  along with this program.  If not, see <http://www.gnu.org/licenses/>.
using NLog;

namespace GadrocsWorkshop.Helios.Gauges.F5E
{
    using GadrocsWorkshop.Helios.ComponentModel;
    using System;
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.Linq;
    using System.Windows;
    using System.Windows.Media;
    using System.Windows.Threading;


    public enum F5E_FLAP_CARD_POSITION
    {
        UP = -42,
        DOWN = 0,
        MID
    }

    public enum F5E_FLAP_DISPLAY_SEQUENCE
    {
        START = 1,
        RESTART,
        HIDE,
        CHECK,
        SHOW,
        PREEND,
        END
    }

    public enum F5E_FLAP_DISPLAY_RUN_STATE
    {
        STOP = 0,
        RUN
    }

    // Annotation Indicating the unique name for this control, display, toolbox group, and control renderer.
    [HeliosControl("Helios.F5E.FlapIndicator", "FlapIndicator", "F-5E Gauges", typeof(GaugeRenderer))]
    public class FlapIndicator : BaseGauge
    {

        private static readonly NLog.Logger Logger = NLog.LogManager.GetCurrentClassLogger();

        private HeliosValue _flapIndex;
        private HeliosValue _flapsLeverValue;
        private HeliosValue _autoFlapSwitch;

        private GaugeNeedle _fullcardNeedle;
        private GaugeNeedle _upcardNeedle;
        private GaugeNeedle _autocardNeedle;
        private GaugeNeedle _fixcardNeedle;
        private static int _prevValue;

        private static ConcurrentQueue<Tuple<DateTimeOffset, int>> queue
            = new ConcurrentQueue<Tuple<DateTimeOffset, int>>();

        private FlapDisplayControl _flapDisplayControl;
        private int _flapsLeverPos = 0;
        private int _autoSwitchPos = 0;
        private const int FLAP_UP = 3;
        private const int FLAP_AUTO = 6;
        private const int FLAP_FIXED = 9;
        private const int FLAP_FULL = 12;
        private const int THUMB_AUTO = 30;
        private const int LEVER_UP = 20;
        private readonly TimeSpan LIMITTIME = TimeSpan.FromMilliseconds(5000);
        private static F5E_FLAP_DISPLAY_RUN_STATE running;
        private static Dictionary<int, GaugeNeedle> _gaugeHash
            = new Dictionary<int, GaugeNeedle>();

        private static readonly Dictionary<Guid, DispatcherTimer> _observeTimers
            = new Dictionary<Guid, DispatcherTimer>();


        public FlapIndicator()
            : base("Flight Instuments", new Size(113d, 133d))
        {
            _fullcardNeedle = new GaugeNeedle("{Helios}/Gauges/F-5E/FlapIndicator/full_card.xaml", new Point(13, 27), new Size(86, 100), new Point(0, 0));
            _fullcardNeedle.Clip = new RectangleGeometry(new Rect(13d, 27d, 86d, 54d));
            Components.Add(_fullcardNeedle);

            _upcardNeedle = new GaugeNeedle("{Helios}/Gauges/F-5E/FlapIndicator/up_card.xaml", new Point(13, 27), new Size(86, 100), new Point(0, 0));
            _upcardNeedle.Clip = new RectangleGeometry(new Rect(13d, 27d, 86d, 54d));
            Components.Add(_upcardNeedle);

            _autocardNeedle = new GaugeNeedle("{Helios}/Gauges/F-5E/FlapIndicator/auto_card.xaml", new Point(13, 27), new Size(86, 100), new Point(0, 0));
            _autocardNeedle.Clip = new RectangleGeometry(new Rect(13d, 27d, 86d, 54d));
            Components.Add(_autocardNeedle);

            _fixcardNeedle = new GaugeNeedle("{Helios}/Gauges/F-5E/FlapIndicator/fxd_card.xaml", new Point(13, 27), new Size(86, 100), new Point(0, 0));
            _fixcardNeedle.Clip = new RectangleGeometry(new Rect(13d, 27d, 86d, 54d));
            Components.Add(_fixcardNeedle);

            _autocardNeedle.IsHidden = false;
            _fullcardNeedle.IsHidden = true;
            _upcardNeedle.IsHidden = true;
            _fixcardNeedle.IsHidden = true;

            Components.Add(new GaugeImage("{Helios}/Gauges/F-5E/FlapIndicator/LeftFlapGauge.png", new Rect(0d, 0d, 113d, 113d)));

            _flapIndex = new HeliosValue(this, new BindingValue(0d), "", "flap index", "flap index", "", BindingValueUnits.Degrees);
            _flapIndex.Execute += new HeliosActionHandler(FlapStatusChange_Execute);
            Actions.Add(_flapIndex);
            Values.Add(_flapIndex);
            _flapsLeverValue = new HeliosValue(this, new BindingValue(0d), "", "Flaps Lever Value",
                "Flaps Lever Value(1=EmerUp 2=Thumb 3=full)", "", BindingValueUnits.Numeric);

            _flapsLeverValue.Execute += new HeliosActionHandler(FlapsLeverChange_Execute);
            Actions.Add(_flapsLeverValue);
            Values.Add(_flapsLeverValue);

            _autoFlapSwitch = new HeliosValue(this, new BindingValue(0d), "", "Auto Flap Switch",
                "Auto Flap Switch(1=up 2=fixed 3=auto)", "", BindingValueUnits.Numeric);

            _autoFlapSwitch.Execute += new HeliosActionHandler(AutoFlapSwitchChange_Execute);
            Actions.Add(_autoFlapSwitch);
            Values.Add(_autoFlapSwitch);

            _gaugeHash = new Dictionary<int, GaugeNeedle>();
            _gaugeHash.Add(FLAP_UP, _upcardNeedle);
            _gaugeHash.Add(FLAP_AUTO, _autocardNeedle);
            _gaugeHash.Add(FLAP_FIXED, _fixcardNeedle);
            _gaugeHash.Add(FLAP_FULL, _fullcardNeedle);
            _flapDisplayControl = null;
        }

        public override void Reset()
        {
            base.Reset();
            var keys = _observeTimers?.Keys;
            foreach (var key in keys)
            {
                _observeTimers.Remove(key);
            }
        }

        private void FlapsLeverChange_Execute(object action, HeliosActionEventArgs e)
        {
            _flapsLeverValue.SetValue(e.Value, e.BypassCascadingTriggers);
            _flapsLeverPos = Convert.ToInt32(_flapsLeverValue.Value.DoubleValue);
        }

        private void AutoFlapSwitchChange_Execute(object action, HeliosActionEventArgs e)
        {
            _autoFlapSwitch.SetValue(e.Value, e.BypassCascadingTriggers);
            _autoSwitchPos = Convert.ToInt32(_autoFlapSwitch.Value.DoubleValue);

        }

        private void FlapStatusChange_Execute(object action, HeliosActionEventArgs e)
        {

            _flapIndex.SetValue(e.Value, e.BypassCascadingTriggers);
            int nextValue = Convert.ToInt32(_flapIndex.Value.DoubleValue);
            int flapValue = 0;
            var trend = (nextValue - _prevValue) > 0 ? 1 : -1;
            if (nextValue == 0)
            {
                _prevValue = 0;
                trend = 0;
            }

            var leverSwitchValue = _flapsLeverPos * 10 + _autoSwitchPos;
            if (leverSwitchValue > THUMB_AUTO)
            {
                flapValue = Convert.ToInt32(Math.Ceiling(nextValue * 0.33) * 3);
            }
            else
            {
                flapValue = leverSwitchValue < LEVER_UP ? FLAP_UP : FLAP_FULL;
            }

            if (running == F5E_FLAP_DISPLAY_RUN_STATE.STOP)
            {
                _flapDisplayControl = new FlapDisplayControl(_gaugeHash, flapValue);
                lock (_flapDisplayControl)
                {
                    running = F5E_FLAP_DISPLAY_RUN_STATE.RUN;
                    _flapDisplayControl.StartTime = DateTimeOffset.Now;
                    _flapDisplayControl.FinishFlapDisplayEvent += CleanUpInput;
                    _flapDisplayControl.DequeueFunc = DequeueMax;
                    _flapDisplayControl.TaskRun(Guid.NewGuid(), F5E_FLAP_DISPLAY_SEQUENCE.START, flapValue);
                }
                return;
            }
            if (trend < 1) { return; }
            _prevValue = flapValue;
            TryEnqueue(Guid.NewGuid(), new Tuple<DateTimeOffset, int>(DateTimeOffset.Now, flapValue));
        }


        public void TryEnqueue(Guid id, Tuple<DateTimeOffset, int> flapValue)
        {
            if (_observeTimers != null && _observeTimers.ContainsKey(id))
            {
                _observeTimers[id].Stop();
                _observeTimers.Remove(id);
            }

            queue.Enqueue(flapValue);
        }

        public void Retry(Guid id, Tuple<DateTimeOffset, int> flapValue)
        {
            var observeTimer = new DispatcherTimer();

            observeTimer.Tick += (sender, e2) =>
            {
                TryEnqueue(id, flapValue);
            };
            observeTimer.Interval = TimeSpan.FromMilliseconds(100L);
            observeTimer.Tag = flapValue.Item2;
            if (_observeTimers.ContainsKey(id))
            {
                _observeTimers[id] = observeTimer;
            }
            else
            {
                _observeTimers.Add(id, observeTimer);
            }
            observeTimer.Start();
        }

        public void CleanUpInput(int flapValue, DateTimeOffset dateTime)
        {
            DateTimeOffset cleanStart = DateTimeOffset.Now;
            bool isElasped = false;

            while (queue.Count() > 0 && !isElasped)
            {
                Tuple<DateTimeOffset, int> result;
                if (queue.TryPeek(out result))
                {
                    if (result?.Item1 < dateTime)
                    {
                        queue.TryDequeue(out _);
                    }
                }
                var diff = (cleanStart - DateTimeOffset.Now);
                isElasped = diff > TimeSpan.FromMilliseconds(500);
            }
            running = F5E_FLAP_DISPLAY_RUN_STATE.STOP;

        }

        private (int, int) DequeueMax(int currentValue, DateTimeOffset observeTime)
        {
            TimeSpan diff = DateTimeOffset.Now - observeTime;

            if (queue.Count() == 0 && diff > LIMITTIME)
            {
                return new ValueTuple<int, int>(2, currentValue);
            }
            if (queue.Count() == 0) { return new ValueTuple<int, int>(1, currentValue); }
            DateTimeOffset workDate = observeTime;
            bool isLoopEnd = false;
            int workValue = 0;
            int loopCount = 0;


            while (!isLoopEnd && loopCount < 8)
            {
                Tuple<DateTimeOffset, int> workTuple = null;

                if (!queue.TryDequeue(out workTuple))
                {
                    return new ValueTuple<int, int>(1, workValue);
                }
                TimeSpan diff2 = workTuple.Item1 - observeTime;
                if (workValue <= workTuple.Item2)
                {
                    workValue = workTuple.Item2;
                }

                if (diff2 > LIMITTIME || (diff2 > TimeSpan.FromMilliseconds(1500)))
                {
                    return new ValueTuple<int, int>(1, workValue);
                }

                loopCount++;
            }
            return new ValueTuple<int, int>(0, workValue);
        }
    }
}