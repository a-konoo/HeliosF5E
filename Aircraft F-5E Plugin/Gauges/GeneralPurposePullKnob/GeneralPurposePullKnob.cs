//  Copyright 2014 Craig Courtney
//  Copyright 2020 Helios Contributors
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

namespace GadrocsWorkshop.Helios.Controls.F5E
{
    using ComponentModel;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Windows;
    using System.Xml;

    [HeliosControl("Helios.F5E.GeneralPurposePullKnob", "GeneralPurposePullKnob", "F-5E", typeof(GeneralPurposeKnobBaseRenderer))]
    public class GeneralPurposePullKnob : GeneralPurposeKnobBase
    {
        private static readonly NLog.Logger Logger = NLog.LogManager.GetCurrentClassLogger();

        public GeneralPurposePullKnob(string name, Point posn, Size size,
            string knobImagePath, string knobImagePulledPath,
            string frontImagePath, string pullReadyImage, string basePath,
            List<Tuple<int, bool, double, double, string, string>> knobPositions,
            Point frontAdjust, Point frontPos,
            double pullJudgeAngle = 270, double pullJudgeDistance = 30,
            double frontRatio = 1.0d, double stepValue = 0.25d,
            bool _prohibitOperate = false, bool pullable = false)
            : base(name, posn, new Size(size.Width, size.Height))
        {

            IsRenderReady = false;
            KnobImage = knobImagePath ?? "{F-5E}/Images/GeneralPurposePullKnob/knobImage.xaml";
            Pullable = pullable;
            ProhibitOperate = _prohibitOperate;
            KnobImagePulled = knobImagePulledPath ?? "{F-5E}/Images/GeneralPurposePullKnob/knobPulledImage.xaml";
            FrontImage = frontImagePath ?? "{F-5E}/Images/GeneralPurposePullKnob/KnobFront.xaml";
            PullReadyImage = pullReadyImage ?? "{F-5E}/Images/GeneralPurposePullKnob/SAIPullReady.xaml";
            var positionPath = basePath ?? "{F-5E}/Images/GeneralPurposePullKnob/";

            knobPositions.ForEach(x =>
            {
                Positions.Add(new GeneralPurposeKnobBasePosition(this,
                    x.Item1, x.Item2, x.Item3, x.Item4,
                    positionPath + x.Item5, positionPath + x.Item6));
            });
            FrontLeft = frontPos.X;
            FrontTop = frontPos.Y;
            InitTriggersAndActions();

            AdjustCenterX = frontAdjust.X;
            AdjustCenterY = frontAdjust.Y;
            PullJudgeAngle = pullJudgeAngle;
            PullJudgeDistance = pullJudgeDistance;
            FrontRatio = frontRatio;
            InclDeclStep = stepValue;
            _currentPosition = 1;
            _defaultPosition = 1;
            _draggingLast = null;
            _currentPullReleaseKnob = false;
            IsRenderReady = true;
        }
        protected sealed override void InitTriggersAndActions()
        {
            _positionValue = new HeliosValue(this, new BindingValue(1), "",
                "position", "Current position of the switch.", "", BindingValueUnits.Numeric);
            _positionValue.Execute += SetPositionAction_Execute;

            Values.Add(_positionValue);
            Actions.Add(_positionValue);
            Triggers.Add(_positionValue);

            _pullableTrigger = new HeliosTrigger(this, "", "", "can pull/release knob value",
                "Current this Knob can be pull/release.",
                "0:not pull position,1:pull position.", BindingValueUnits.Boolean);

            Triggers.Add(_pullableTrigger);

            _sendValueTrigger = new HeliosTrigger(this, "", "", "sendValue",
                "trigger will fire, when position changed",
                "0 - 1", BindingValueUnits.Numeric);

            Triggers.Add(_sendValueTrigger);

            _pullReleaseKnob = new HeliosValue(this, new BindingValue(1), "",
                "PullReleaseKnob", "Pulling State of Knob", "", BindingValueUnits.Boolean);
            _pullReleaseKnob.Execute += SetPullReleaseKnob_Execute;

            Values.Add(_pullReleaseKnob);
            Actions.Add(_pullReleaseKnob);
            Triggers.Add(_pullReleaseKnob);

            Positions.CollectionChanged += Positions_CollectionChanged;
            Positions.PositionChanged += PositionsChanged;

            _pullReleaseTrigger = new HeliosTrigger(this, "", "", "pull/release knob",
                "trigger will fire, when knob is pulled/released.",
                "false:release,true:pull.", BindingValueUnits.Boolean);

            Triggers.Add(_pullReleaseTrigger);

            _pushedTrigger = new HeliosTrigger(this, "", "", "pushed", "Fired when OffFlag is pushed.",
                "Always returns true.", BindingValueUnits.Boolean);
            _releasedTrigger = new HeliosTrigger(this, "", "", "released", "Fired when OffFlag is released.",
                "Always returns false.", BindingValueUnits.Boolean);
            Triggers.Add(_pushedTrigger);
            Triggers.Add(_releasedTrigger);


            _increseTrigger = new HeliosTrigger(this, "", "", "incleased", "Fired when index inclease.",
                "return inclease/declease step", BindingValueUnits.Numeric);
            _decreaseTrigger = new HeliosTrigger(this, "", "", "declease", "Fired when index declease.",
                "return inclease/declease step", BindingValueUnits.Numeric);
            Triggers.Add(_increseTrigger);
            Triggers.Add(_decreaseTrigger);

            _secondaryInput = new HeliosValue(this, new BindingValue(0), "",
                "secondaryInput", "position change input of the switch(-1:down/0:normal/1:up).", "", BindingValueUnits.Numeric);
            _secondaryInput.Execute += SecondaryInputAction_Execute;

            Values.Add(_secondaryInput);
            Actions.Add(_secondaryInput);

        }


        public GeneralPurposePullKnob() : base("GeneralPurposePullKnob", new Size(94, 91))
        {
            IsRenderReady = false;
            KnobImage = "{F-5E}/Images/GeneralPurposePullKnob/knobImage.xaml";
            Pullable = true;
            KnobImagePulled = "{F-5E}/Images/GeneralPurposePullKnob/knobPulledImage.xaml";
            FrontImage = "{F-5E}/Images/GeneralPurposePullKnob/KnobFront.xaml";
            PullReadyImage = "{F-5E}/Images/GeneralPurposePullKnob/PullReady.xaml";
            ProhibitOperate = false;

            var basePath = "{F-5E}/Images/GeneralPurposePullKnob/";

            Positions.Add(new GeneralPurposeKnobBasePosition(this, 1,  false, 
                0, 0d, basePath + "KnobLines01.xaml", basePath + "KnobPulledLines01.xaml"));
            Positions.Add(new GeneralPurposeKnobBasePosition(this, 2,  false,
                -0.85d, 25d, basePath + "KnobLines02.xaml", basePath + "KnobPulledLines02.xaml"));
            Positions.Add(new GeneralPurposeKnobBasePosition(this, 3,  false,
                -0.7d, 50d, basePath + "KnobLines03.xaml", basePath + "KnobPulledLines03.xaml"));
            Positions.Add(new GeneralPurposeKnobBasePosition(this, 4,  false,
                -0.55d, 75d, basePath + "KnobLines04.xaml", basePath + "KnobPulledLines04.xaml"));
            Positions.Add(new GeneralPurposeKnobBasePosition(this, 5,  false,
                -0.4d, 100d, basePath + "KnobLines01.xaml", basePath + "KnobPulledLines01.xaml"));
            Positions.Add(new GeneralPurposeKnobBasePosition(this, 6,  false,
                -0.25d, 125d, basePath + "KnobLines02.xaml", basePath + "KnobPulledLines02.xaml"));
            Positions.Add(new GeneralPurposeKnobBasePosition(this, 7, false,
                -0.1d, 150d, basePath + "KnobLines03.xaml", basePath + "KnobPulledLines03.xaml"));
            Positions.Add(new GeneralPurposeKnobBasePosition(this, 8, true,
                0.05d, 175d, basePath + "KnobLines04.xaml", basePath + "KnobPulledLines04.xaml"));
            Positions.Add(new GeneralPurposeKnobBasePosition(this, 9, true,
                0.2d, 200d, basePath + "KnobLines01.xaml", basePath + "KnobPulledLines01.xaml"));
            Positions.Add(new GeneralPurposeKnobBasePosition(this, 10, false,
                0.35d,225d, basePath + "KnobLines02.xaml", basePath + "KnobPulledLines02.xaml"));
            Positions.Add(new GeneralPurposeKnobBasePosition(this, 11, false,
                0.5d, 250d, basePath + "KnobLines03.xaml", basePath + "KnobPulledLines03.xaml"));
            Positions.Add(new GeneralPurposeKnobBasePosition(this, 12, false,
                0.65d,275d, basePath + "KnobLines04.xaml", basePath + "KnobPulledLines04.xaml"));
            Positions.Add(new GeneralPurposeKnobBasePosition(this, 13, false,
                0.8d, 300d, basePath + "KnobLines01.xaml", basePath + "KnobPulledLines01.xaml"));
            Positions.Add(new GeneralPurposeKnobBasePosition(this, 14, false,
                0.95d,325d, basePath + "KnobLines02.xaml", basePath + "KnobPulledLines02.xaml"));
            Positions.Add(new GeneralPurposeKnobBasePosition(this, 13, false,
                1d, 360d, basePath + "KnobLines03.xaml", basePath + "KnobPulledLines03.xaml"));
            InitTriggersAndActions();

            _currentPosition = 1;
            _defaultPosition = 1;
            _draggingLast = null;
            _currentPullReleaseKnob = false;
            IsRenderReady = true;
        }

        #region Properties



        public override int CurrentPosition
        {
            get => _currentPosition;
            set
            {
                if (value < 0 || value >= Positions.Count)
                {
                    return;
                }
                int diffValue = _currentPosition - value;
                int oldValue = _currentPosition;
                
                _currentPosition = value;
   
                Positions[_currentPosition].EnterTriggger.FireTrigger(BindingValue.Empty);
                if (oldValue < value)
                {
                    _increseTrigger.FireTrigger(new BindingValue(InclDeclStep));
                } else
                {
                    _decreaseTrigger.FireTrigger(new BindingValue((-1 * InclDeclStep)));
                }

                var sendValue = Positions[_currentPosition].SendValue;
                _sendValueTrigger.FireTrigger(new BindingValue(sendValue));

                var canPull = Positions[CurrentIndex].CanPull;

                if (canPull && _currentPullReleaseKnob && _releaseLock)
                {
                    _releaseLock = false;
                    PullReleaseKnob = false;
                }

                if (_pulledPosition != -1 &&
                    _currentPullReleaseKnob && Math.Abs(_pulledPosition - CurrentPosition) > 2)
                {
                    _releaseLock = true;
                }
                // knob has pullable flag:true, knob position is pullable position,
                // and knob state is ready pull, then can pull
                if (_pullable)
                {
                    if (canPull == true && !_currentPullReleaseKnob)
                    {
                        _pullableTrigger.FireTrigger(new BindingValue(1));
                    }
                    else
                    {
                        _pullableTrigger.FireTrigger(new BindingValue(0));
                    }
                }
                if (!_smootheKnob)
                {
                    _lastChanged = DateTime.Now;
                }
                OnPropertyChanged("CurrentPosition", oldValue, value, false);
                OnDisplayUpdate();
            }
        }


        #region Actions

        protected override void SetPositionAction_Execute(object action, HeliosActionEventArgs e)
        {
            BeginTriggerBypass(e.BypassCascadingTriggers);
            if (int.TryParse(e.Value.StringValue, out int index))
            {
                if (index >= 0 && index < Positions.Count)
                {
                    CurrentPosition = index;
                }

            }
            EndTriggerBypass(e.BypassCascadingTriggers);
        }

        protected override void SecondaryInputAction_Execute(object action, HeliosActionEventArgs e)
        {

            BeginTriggerBypass(e.BypassCascadingTriggers);
            if (int.TryParse(e.Value.StringValue, out int buttonInput))
            {
                _fromButtonInput = 0;
                if (buttonInput == 1 && CurrentPosition < Positions.Count)
                {
                    _fromButtonInput = 1;
                    CurrentPosition += 1;
                }
                if (buttonInput == -1 && CurrentPosition > 0)
                {
                    _fromButtonInput = -1;
                    CurrentPosition -= 1;
                }
            }
            EndTriggerBypass(e.BypassCascadingTriggers);
        }


        protected override void SetPullReleaseKnob_Execute(object action, HeliosActionEventArgs e)
        {
            BeginTriggerBypass(e.BypassCascadingTriggers);
            if (e.Value.StringValue == "1" || e.Value.BoolValue == true)
            {
                PullReleaseKnob = true;
            }
            if (e.Value.StringValue == "0" || e.Value.BoolValue == false)
            {
                PullReleaseKnob = false;
            }
        }


        #endregion

        public override int DefaultPosition
        {
            get => _defaultPosition;
            set
            {
                if (_defaultPosition.Equals(value) || value <= 0 || value > Positions.Count)
                {
                    return;
                }

                int oldValue = _defaultPosition;
                _defaultPosition = value;
                OnPropertyChanged("DefaultPosition", oldValue, value, true);
            }
        }

        public override bool PullReleaseKnob
        {
            get => _currentPullReleaseKnob;
            set
            {
                if (value == _currentPullReleaseKnob) { return; }
                bool oldValue = _currentPullReleaseKnob;
                _currentPullReleaseKnob = value;

                _pullReleaseKnob.SetValue(new BindingValue((bool)_currentPullReleaseKnob), BypassTriggers);

                if (_currentPullReleaseKnob)
                {
                    _pushedTrigger.FireTrigger(BindingValue.Empty);
                    _pulledPosition = CurrentIndex;
                    _pullReleaseTrigger.FireTrigger(new BindingValue((bool)_currentPullReleaseKnob));
                }
                else
                {
                    _releasedTrigger.FireTrigger(BindingValue.Empty);
                    _releaseLock = false;
                    _pulledPosition = -1;
                }


                OnPropertyChanged("PullReleaseKnob", oldValue, value, false);
                OnDisplayUpdate();
            }
        }

        #endregion


        public override void MouseDown(Point location)
        {
            if (NonClickableZones != null)
            {
                foreach (NonClickableZone zone in NonClickableZones)
                {
                    if (zone.AllPositions && zone.isClickInZone(location))
                    {
                        zone.ChildVisual.MouseDown(new System.Windows.Point(
                            location.X - (zone.ChildVisual.Left - this.Left),
                            location.Y - (zone.ChildVisual.Top - this.Top)));
                        return; //we get out to let the ChildVisual using the click
                    }
                }
            }

            if (_lastPushed == null)
            {
                _lastPushed = DateTime.Now;
            }


            if (ProhibitOperate)
            {
                return;
            }

            base.MouseDown(location);
        }

        public override void MouseDrag(Point location)
        {
            var canPull = Positions[CurrentIndex].CanPull;
            if (!canPull && PullReleaseKnob)
            {
                base.MouseDrag(location);
                return;
            }
            if (_lastChanged == null)
            {
                _lastChanged = DateTime.Now;
            }
            if (!_smootheKnob && (DateTime.Now - _lastChanged) < TimeSpan.FromMilliseconds(WaitMillisecond))
            {
                _draggingLast = location;
                return;
            }
            if (_draggingLast == null)
            {
                _draggingLast = location;
                return;
            }
            Vector diff = (_draggingLast - location) ?? new Vector(0, 0);
            var dragAngle = Math.Atan2(diff.Y, diff.Y);
            var dragDistace = Math.Sqrt(diff.X * diff.X + diff.Y * diff.Y);
            var dragJudgeAngle = Math.PI * PullJudgeAngle / 180;
            var dragJudgeToward = dragDistace * Math.Cos(dragJudgeAngle - dragAngle);

            if (canPull == true && Pullable && PullReady && dragJudgeToward > PullJudgeDistance)
            {
                PullReleaseKnob = true;
                PullReady = false;
                return;
            }
            if (PullReady)
            {
                return;
            }
            if (ProhibitOperate)
            {
                return;
            }
            base.MouseDrag(location);
        }

        public override void MouseUp(Point location)
        {
            if (ProhibitOperate)
            {
                return;
            }
            if (NonClickableZones != null)
            {
                foreach (NonClickableZone zone in NonClickableZones)
                {
                    if (zone.AllPositions && zone.isClickInZone(location))
                    {
                        zone.ChildVisual.MouseUp(new System.Windows.
                            Point(location.X - (zone.ChildVisual.Left - this.Left),
                            location.Y - (zone.ChildVisual.Top - this.Top)));
                        return; //we get out to let the ChildVisual using the click
                    }
                }
            }

            var canPull = Positions[CurrentIndex].CanPull;

            if (canPull == true && Pullable && !PullReady && !PullReleaseKnob && 
                (DateTime.Now - _lastPushed) > TimeSpan.FromMilliseconds(200))
            {
                PullReady = true;
            }

            if (PullReady && (DateTime.Now - _lastPushed) < TimeSpan.FromMilliseconds(200))
            {
                PullReady = false;
            }
            _lastChanged = null;
            _lastPushed = null;
            _draggingLast = null;

            base.MouseUp(location);
        }

        #region IPulsedControl

        public override void Pulse(int pulses)
        {
            if (Positions.Count == 0)
            {
                // there are no positions so we cannot move
                return;
            }

            // WARNING: Positions is a zero-based array, but _currentPosition is 1-based
            int newPosition = _currentPosition + pulses;

            if (IsContinuous)
            {
                // wrap around if we have to
                newPosition = 1 + ((newPosition - 1) % Positions.Count);
                if (newPosition < 1)
                {
                    // don't use negative remainder
                    newPosition += Positions.Count;
                }
                CurrentPosition = newPosition;
                return;
            }

            // explicitly check boundaries
            if (newPosition > Positions.Count)
            {
                CurrentPosition = Positions.Count;
            }
            else if (newPosition < 0)
            {
                CurrentPosition = 0;
            }
            else
            {
                CurrentPosition = newPosition;
            }
        }

        public override void MouseWheel(int delta)
        {
            base.MouseWheel(delta);
        }

        #endregion

        #region IRotarySwitch

        public new int MinPosition => 1;

        public new int MaxPosition => Positions.Count;

        #endregion

        #region IRotaryControl

        // the angle that our control would be at if we were allowed to stop everywhere,
        // required so that IRotaryControl will operate correctly
        // with incremental changes
        private double _controlAngle;

        public override double ControlAngle
        {
            get => _controlAngle;
            set
            {
                if (_positionIndex.Length < 1)
                {
                    // no positions, nothing will work
                    return;
                }

                if (IsContinuous)
                {
                    _controlAngle = value % 360d;
                    if (_controlAngle < 0d)
                    {
                        // don't use negative remainder
                        _controlAngle += 360d;
                    }
                }
                else
                {
                    // clamp
                    _controlAngle = Math.Min(Math.Max(value, _positionIndex[0].Rotation),
                        _positionIndex[_positionIndex.Length - 1].Rotation);
                }

                // see where requested position falls in the sorted sequence of knob positions
                Logger.Debug("setting rotary switch based on input angle {Angle}", _controlAngle);
                int searchResult = Array.BinarySearch(_positionIndex,
                    new PositionIndexEntry { Rotation = _controlAngle }, PositionIndexComparer);
                if (searchResult >= 0)
                {
                    // direct hit
                    CurrentPosition = _positionIndex[searchResult].Index;
                    return;
                }

                // find closest two positons
                int nextLarger = ~searchResult;
                int lowIndex;
                int highIndex;
                double lowOffset;
                double highOffset;
                if (nextLarger == _positionIndex.Length)
                {
                    // larger than all values
                    lowIndex = _positionIndex.Length - 1;
                    highIndex = 0;
                    lowOffset = 0d;
                    highOffset = 360d;
                }
                else if (nextLarger == 0)
                {
                    // smaller than all values
                    lowIndex = _positionIndex.Length - 1;
                    highIndex = 0;
                    lowOffset = -360d;
                    highOffset = 0d;
                }
                else
                {
                    // somewhere in middle
                    lowIndex = nextLarger - 1;
                    highIndex = nextLarger;
                    lowOffset = 0d;
                    highOffset = 0d;
                }

                // snap to closest position
                double lowValue = _positionIndex[lowIndex].Rotation + lowOffset;
                double highValue = _positionIndex[highIndex].Rotation + highOffset;
                int closestIndex = Math.Abs(_controlAngle - lowValue)
                    < Math.Abs(_controlAngle - highValue) ? lowIndex : highIndex;
                CurrentPosition = _positionIndex[closestIndex].Index;
            }
        }

        #endregion

        public override void Reset()
        {
            base.Reset();

            BeginTriggerBypass(true);
            CurrentPosition = DefaultPosition;
            EndTriggerBypass(true);
        }

        public override void WriteXml(XmlWriter writer)
        {
            base.WriteXml(writer);
        }

        public override void ReadXml(XmlReader reader)
        {
            base.ReadXml(reader);
        }
    }
}