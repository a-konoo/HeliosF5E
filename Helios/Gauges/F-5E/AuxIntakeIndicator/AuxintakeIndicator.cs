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
    using GadrocsWorkshop.Helios.Interfaces.DCS.Common;
    using System;
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.Linq;
    using System.Windows;
    using System.Windows.Media;
    using System.Windows.Threading;


    public enum F5E_AUXINTAKE_CARD_POSITION
    {
        UP = -76,
        DOWN = 0,
        MID
    }

    public enum F5E_AUXINTAKE_DISPLAY_SEQUENCE
    {
        START = 1,
        HIDE,
        CHECK,
        SHOW,
        END
    }

    public enum F5E_AUXINTAKE_DISPLAY_RUN_STATE
    {
        STOP = 0,
        RUN
    }

    // Annotation Indicating the unique name for this control, display, toolbox group, and control renderer.
    [HeliosControl("Helios.F5E.AuxIntakeIndicator", "AuxIntakeIndicator", "F-5E Gauges", typeof(GaugeRenderer))]
    public class AuxintakeIndicator : BaseGauge
    {

        private static readonly NLog.Logger Logger = NLog.LogManager.GetCurrentClassLogger();

        private HeliosValue _airIntakeIndex;

        private GaugeNeedle _closedNeedle;
        private GaugeNeedle _openNeedle;
        private static ConcurrentQueue<Tuple<DateTimeOffset, int>> queue
            = new ConcurrentQueue<Tuple<DateTimeOffset, int>>();

        private AuxIntakeDisplayControl _airIntakeControl;
        private const int INTAKE_OPEN = 1;
        private const int INTAKE_CLOSE = 2;
        private static F5E_AUXINTAKE_DISPLAY_RUN_STATE running;
        private static Dictionary<int, GaugeNeedle> gaugeHash
            = new Dictionary<int, GaugeNeedle>();
        private readonly TimeSpan LIMITTIME = TimeSpan.FromMilliseconds(2500);

        private static readonly Dictionary<Guid, DispatcherTimer> observeTimers
            = new Dictionary<Guid, DispatcherTimer>();


        public AuxintakeIndicator()
            : base("Flight Instuments", new Size(271d, 380d))
        {

            _closedNeedle = new GaugeNeedle("{Helios}/Gauges/F-5E/AuxIntakeIndicator/closed_card.xaml", new Point(36d, 190d), new Size(128d, 160d), new Point(0, 0));
            _closedNeedle.Clip = new RectangleGeometry(new Rect(36d, 190d, 128d, 86d));
            Components.Add(_closedNeedle);
            _closedNeedle.IsHidden = true;

            _openNeedle = new GaugeNeedle("{Helios}/Gauges/F-5E/AuxIntakeIndicator/oepn_card.xaml", new Point(36d, 190d), new Size(128d, 160d), new Point(0, 0));
            _openNeedle.Clip = new RectangleGeometry(new Rect(36d, 190d, 128d, 86d));
            Components.Add(_openNeedle);
            Components.Add(new GaugeImage("{Helios}/Gauges/F-5E/AuxIntakeIndicator/RightIntakeGauge.png", new Rect(0d, 0d, 271d, 380d)));
            _openNeedle.IsHidden = false;

            _airIntakeIndex = new HeliosValue(this, new BindingValue(0d), "", "airIntake index", "airIntake index", "", BindingValueUnits.Numeric);
            _airIntakeIndex.Execute += new HeliosActionHandler(AuxIntakeStatusChange_Execute);
            Actions.Add(_airIntakeIndex);
            Values.Add(_airIntakeIndex);

            gaugeHash = new Dictionary<int, GaugeNeedle>();
            gaugeHash.Add(INTAKE_OPEN, _openNeedle);
            gaugeHash.Add(INTAKE_CLOSE, _closedNeedle);
        }

        public override void Reset()
        {
            base.Reset();
            var keys = observeTimers?.Keys;
            foreach (var key in keys)
            {
                observeTimers.Remove(key);
            }
        }

        private void AuxIntakeStatusChange_Execute(object action, HeliosActionEventArgs e)
        {

            _airIntakeIndex.SetValue(e.Value, e.BypassCascadingTriggers);
            double intakeRawValue = Convert.ToDouble(_airIntakeIndex.Value.DoubleValue);

            if (running == F5E_AUXINTAKE_DISPLAY_RUN_STATE.STOP)
            {
                int intakeValue = 0;
                if (intakeRawValue == 0.2)
                {
                    intakeValue = INTAKE_CLOSE;
                }
                else if (intakeRawValue >= 0.05 && intakeRawValue <= 0.1)
                {
                    intakeValue = INTAKE_OPEN;
                }
                else
                {
                    return;
                }

                if (running == F5E_AUXINTAKE_DISPLAY_RUN_STATE.STOP)
                {
                    _airIntakeControl = new AuxIntakeDisplayControl(gaugeHash, intakeValue);
                    lock (_airIntakeControl)
                    {
                        running = F5E_AUXINTAKE_DISPLAY_RUN_STATE.RUN;
                        Logger.Warn($"STATUS:RUN");
                        _airIntakeControl.StartTime = DateTimeOffset.Now;
                        _airIntakeControl.FinishAuxIntakeDisplayEvent += CleanUpInput;
                        _airIntakeControl.DequeueFunc = DequeueMax;
                        _airIntakeControl.TaskRun(Guid.NewGuid(), F5E_AUXINTAKE_DISPLAY_SEQUENCE.START, intakeValue);
                    }
                    return;
                }
                TryEnqueue(Guid.NewGuid(), new Tuple<DateTimeOffset, int>(DateTimeOffset.Now, intakeValue));
            }
        }

        public void TryEnqueue(Guid id, Tuple<DateTimeOffset, int> flapValue)
        {
            if (observeTimers != null && observeTimers.ContainsKey(id))
            {
                observeTimers[id].Stop();
                observeTimers.Remove(id);
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
            if (observeTimers.ContainsKey(id))
            {
                observeTimers[id] = observeTimer;
            }
            else
            {
                observeTimers.Add(id, observeTimer);
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
            running = F5E_AUXINTAKE_DISPLAY_RUN_STATE.STOP;

        }

        private (int, int) DequeueMax(int currentValue, DateTimeOffset observeTime)
        {
            TimeSpan diff = DateTimeOffset.Now - observeTime;

            if (queue.Count() == 0 && diff > LIMITTIME)
            {
                return new ValueTuple<int, int>(2, currentValue);
            }
            DateTimeOffset workDate = observeTime;
            bool isLoopEnd = false;
            int workValue = currentValue;
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

                if (diff2 > LIMITTIME)
                {
                    return new ValueTuple<int, int>(1, workValue);
                }

                loopCount++;
            }
            return new ValueTuple<int, int>(0, workValue);
        }
    }
}