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

namespace GadrocsWorkshop.Helios.Controls
{
    using ComponentModel;
    using System;
    using System.Collections.Generic;
    using System.Collections.Specialized;
    using System.ComponentModel;
    using System.Globalization;
    using System.Linq;
    using System.Text;
    using System.Windows;
    using System.Windows.Media;
    using System.Xml;

    [HeliosControl("Helios.Base.RoundSolidPullKnob", "RoundSolidPullKnob", "F-5E", typeof(RoundSolidPullKnobRenderer))]
    public class RoundSolidPullKnob : Rotary
    {
        private static readonly NLog.Logger Logger = NLog.LogManager.GetCurrentClassLogger();

        private HeliosTrigger _pullReleaseTrigger;
        private HeliosTrigger _pullableTrigger;
        private HeliosTrigger _pushedTrigger;
        private HeliosTrigger _releasedTrigger;
        private HeliosTrigger _sendValueTrigger;
        private HeliosTrigger _increseTrigger;
        private HeliosTrigger _decreaseTrigger;

        private int _currentPosition;
        private int _defaultPosition;
        private bool _currentPullReleaseKnob;
        private int _pulledPosition = -1;
        private bool _smootheKnob = true;
        private DateTime _lastChanged = DateTime.Now;
        private int _waitMilisecond = 300;

        private string _errorMessage;

        private string _knobImage;
        private string _frontImage;
        private string _knobImagePulled;
        private string _backRoundKnobImage;

        // config
        private bool _isContinuous = false;
        private bool _isBasementFlag = false;         // if true, use knob basement
        private double _basementLeft = 0;
        private double _basementTop = 0;
        private double _frontLeft = 13;
        private double _frontTop = 33;
        private double _pullDistanceX = 10;
        private double _pullDistanceY = 20;
        private double _adjustCenterX = 1.0d;
        private double _adjustCenterY = 0.5d;
        private double _incldeclStep = 0.1d;
        private bool _pullable;         // allow rotary pull action 
        private bool _isReleaseManual;    // if true, dragging up knob will release 
        // variables
        private int PULL_DIST = 100;

        // HeliosValue (input parameter)
        private readonly HeliosValue _positionValue;
        private readonly HeliosValue _pullReleaseKnob;

        private bool _releaseLock = false;
        private Point? _draggingLast;
        // if true, block render method run.(for change property init/move on profiler)
        
        private bool _isRenderReady = false;

        // array ordered by position angles in degrees, so we can dereference to
        // their position index
        private PositionIndexEntry[] _positionIndex;

        // comparison function to binary search in sorted array
        private static readonly PositionSortComparer PositionIndexComparer
            = new PositionSortComparer();

        public RoundSolidPullKnob()
            : base("Round Solid PullKnob", new Size(118, 140))
        {
            IsRenderReady = false;
            KnobImage = "{Helios}/Images/F-5E/RoundSolidPullKnob/knobImage.xaml";
            Pullable = false;
            KnobImagePulled = "{Helios}/Images/F-5E/RoundSolidPullKnob/knobPulledImage.xaml";
            BackRoundKnobImage = "{Helios}/Images/F-5E/RoundSolidPullKnob/KnobBackRing.png";
            FrontImage = "{Helios}/Images/F-5E/RoundSolidPullKnob/KnobFront.xaml";

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

            

            var basePath = "{Helios}/Images/F-5E/RoundSolidPullKnob/";

            Positions.Add(new RoundSolidPullKnobPosition(this, 1,  false, 
                1, 0d, basePath + "KnobLines01.xaml", basePath + "KnobPulledLines01.xaml"));
            Positions.Add(new RoundSolidPullKnobPosition(this, 2,  false,
                2, 30d, basePath + "KnobLines02.xaml", basePath + "KnobPulledLines04.xaml"));
            Positions.Add(new RoundSolidPullKnobPosition(this, 3,  false,
                3, 60d, basePath + "KnobLines01.xaml", basePath + "KnobPulledLines01.xaml"));
            Positions.Add(new RoundSolidPullKnobPosition(this, 4,  false,
                4, 90d, basePath + "KnobLines02.xaml", basePath + "KnobPulledLines04.xaml"));
            Positions.Add(new RoundSolidPullKnobPosition(this, 5,  false,
                5, 120d, basePath + "KnobLines01.xaml", basePath + "KnobPulledLines01.xaml"));
            Positions.Add(new RoundSolidPullKnobPosition(this, 6,  false,
                6, 150d, basePath + "KnobLines02.xaml", basePath + "KnobPulledLines04.xaml"));
            Positions.Add(new RoundSolidPullKnobPosition(this, 7,  true,
                7, 160d, basePath + "KnobLines01.xaml", basePath + "KnobPulledLines01.xaml"));
            Positions.Add(new RoundSolidPullKnobPosition(this, 8,  true,
                8, 190d, basePath + "KnobLines02.xaml", basePath + "KnobPulledLines04.xaml"));
            Positions.Add(new RoundSolidPullKnobPosition(this, 9,  false,
                9, 220d, basePath + "KnobLines01.xaml", basePath + "KnobPulledLines01.xaml"));
            Positions.Add(new RoundSolidPullKnobPosition(this, 10, false,
                10,250d, basePath + "KnobLines02.xaml", basePath + "KnobPulledLines04.xaml"));
            Positions.Add(new RoundSolidPullKnobPosition(this, 11, false,
                11,280d, basePath + "KnobLines01.xaml", basePath + "KnobPulledLines01.xaml"));
            Positions.Add(new RoundSolidPullKnobPosition(this, 12, false,
                12,310d, basePath + "KnobLines02.xaml", basePath + "KnobPulledLines04.xaml"));
            Positions.Add(new RoundSolidPullKnobPosition(this, 13, false,
                13,340d, basePath + "KnobLines01.xaml", basePath + "KnobPulledLines01.xaml"));
            Positions.Add(new RoundSolidPullKnobPosition(this, 14, false,
                14,360d, basePath + "KnobLines02.xaml", basePath + "KnobPulledLines04.xaml"));

            _currentPosition = 1;
            _defaultPosition = 1;
            _draggingLast = null;
            _currentPullReleaseKnob = false;
            IsRenderReady = true;
        }

        #region Properties

        public RoundSolidPullKnobPositionCollection Positions { get; }
            = new RoundSolidPullKnobPositionCollection();

        public Double InclDeclStep
        {
            get => _incldeclStep;
            set
            {
                if (_incldeclStep.Equals(value))
                {
                    return;
                }

                double oldValue = _incldeclStep;
                _incldeclStep = value;
                OnPropertyChanged("InclDeclStep", oldValue, value, true);
                Refresh();
            }
        }

        public bool IsSmoothKnob
        {
            get => _smootheKnob;
            set
            {
                if (_smootheKnob.Equals(value))
                {
                    return;
                }

                _smootheKnob = value;
                OnPropertyChanged("IsSmoothKnob", !value, value, true);
                Refresh();
            }
        }

        public int WaitMillisecond
        {
            get => _waitMilisecond;
            set
            {
                if (_waitMilisecond.Equals(value))
                {
                    return;
                }
                var oldValue = _waitMilisecond;
                _waitMilisecond = value;
                OnPropertyChanged("WaitMillisecond", oldValue, value, true);
                Refresh();
            }
        }
        public bool IsContinuous
        {
            get => _isContinuous;
            set
            {
                if (_isContinuous.Equals(value))
                {
                    return;
                }

                bool oldValue = _isContinuous;
                _isContinuous = value;
                OnPropertyChanged("IsContinuous", oldValue, value, true);
                Refresh();
            }
        }

        public bool IsBasementFlag
        {
            get => _isBasementFlag;
            set
            {
                if (_isBasementFlag.Equals(value))
                {
                    return;
                }

                bool oldValue = _isBasementFlag;
                _isBasementFlag = value;
                OnPropertyChanged("IsBasementFlag", oldValue, value, true);
            }
        }

        public bool IsReleaseManual
        {
            get => _isReleaseManual;
            set
            {
                if (_isReleaseManual.Equals(value))
                {
                    return;
                }

                bool oldValue = _isReleaseManual;
                _isReleaseManual = value;
                OnPropertyChanged("IsReleaseManual", oldValue, value, true);
            }
        }


        public bool IsRenderReady
        {
            get => _isRenderReady;
            set
            {
                if (_isRenderReady.Equals(value))
                {
                    return;
                }

                bool oldValue = _isRenderReady;
                _isRenderReady = value;
                OnPropertyChanged("IsRenderReady", oldValue, value, true);
                Refresh();
            }
        }


        public int CurrentIndex
        {
            get => _currentPosition - 1 >= 0 ? _currentPosition - 1 : 0;
        }

        public int CurrentPosition
        {
            get => _currentPosition;
            set
            {
                if (_currentPosition.Equals(value) || value <= 0 || value > Positions.Count)
                {
                    return;
                }

                int oldValue = _currentPosition;

                _currentPosition = value;

                if (!BypassTriggers)
                {
                    if (oldValue > 0 && oldValue < Positions.Count)
                    {
                        Positions[oldValue - 1].ExitTrigger.FireTrigger(BindingValue.Empty);

                    }
                    Positions[CurrentIndex].EnterTriggger.FireTrigger(BindingValue.Empty);
                    if (oldValue < value)
                    {
                        _increseTrigger.FireTrigger(new BindingValue(InclDeclStep));
                    } else
                    {
                        _decreaseTrigger.FireTrigger(new BindingValue((-1 * InclDeclStep)));
                    }
                }
                var sendValue = Positions[CurrentIndex].SendValue;
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

                if (canPull == true && !_currentPullReleaseKnob)
                {
                    _pullableTrigger.FireTrigger(new BindingValue(1));
                }
                else
                {
                    _pullableTrigger.FireTrigger(new BindingValue(0));
                }
                if (!_smootheKnob)
                {
                    _lastChanged = DateTime.Now;
                }
                OnPropertyChanged("CurrentPosition", oldValue, value, false);
                OnDisplayUpdate();
            }
        }

        /// <summary>
        /// message of image load error when initialize control
        /// </summary>
        public String ErrorMessage
        {
            get => _errorMessage;
            set
            {
                String oldValue = _errorMessage;
                _errorMessage = value;
                OnPropertyChanged("ErrorMessage", oldValue, value, true);
            }
        }

        #region Actions

        void SetPositionAction_Execute(object action, HeliosActionEventArgs e)
        {
            BeginTriggerBypass(e.BypassCascadingTriggers);
            if (int.TryParse(e.Value.StringValue, out int index))
            {
                if (index > 0 && index <= Positions.Count)
                {
                    CurrentPosition = index;
                }

            }
            EndTriggerBypass(e.BypassCascadingTriggers);
        }

        void SetPullReleaseKnob_Execute(object action, HeliosActionEventArgs e)
        {
            BeginTriggerBypass(e.BypassCascadingTriggers);
            if (Boolean.TryParse(e.Value.StringValue, out bool flag))
            {
                PullReleaseKnob  = flag;
            }
        }


        #endregion

        public int DefaultPosition
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


        public override void MouseDrag(Point location)
        {
            var canPull = Positions[CurrentIndex].CanPull;
            if (!canPull && PullReleaseKnob)
            {
                base.MouseDrag(location);
                return;
            }
            if (!_smootheKnob && (DateTime.Now - _lastChanged) < TimeSpan.FromMilliseconds(300))
            {
                _draggingLast = location;
                return;
            }
            if (_draggingLast == null)
            {
                _draggingLast = location;
                return;
            }
            Vector diff = (_draggingLast - location) ?? new Vector(0,0);

            if (canPull == true && Math.Sqrt(diff.X * diff.X + diff.Y * diff.Y) > PULL_DIST)
            {
                PullReleaseKnob = true;
            }

            base.MouseDrag(location);
        }


        public bool PullReleaseKnob
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

        public string KnobImage
        {
            get { return _knobImage; }
            set
            {
                if ((_knobImage == null && value != null)
                    || (_knobImage != null && !_knobImage.Equals(value)))
                {
                    string oldValue = _knobImage;
                    _knobImage = value;
                    OnPropertyChanged("KnobImage", oldValue, value, true);
                    Refresh();
                }
            }
        }

        public string FrontImage
        {
            get { return _frontImage; }
            set
            {
                if ((_frontImage == null && value != null)
                    || _frontImage != null && !_frontImage.Equals(value))
                {
                    string oldValue = _frontImage;
                    _frontImage = value;
                    OnPropertyChanged("FrontImage", oldValue, value, true);
                    Refresh();
                }
            }
        }


        public bool Pullable
        {
            get { return _pullable; }
            set
            {
                if (!_pullable.Equals(value))
                {
                    bool oldValue = _pullable;
                    _pullable = value;
                    OnPropertyChanged("Pullable", oldValue, value, true);
                    Refresh();
                }
            }
        }
        

        public string KnobImagePulled
        {
            get { return _knobImagePulled; }
            set
            {
                if ((_knobImagePulled == null && value != null)
                    || (_knobImagePulled != null && !_knobImagePulled.Equals(value)))
                {
                    string oldValue = _knobImagePulled;
                    _knobImagePulled = value;
                    OnPropertyChanged("KnobImagePulled", oldValue, value, true);
                    Refresh();
                }
            }
        }

        public string BackRoundKnobImage
        {
            get { return _backRoundKnobImage; }
            set
            {
                if ((_backRoundKnobImage == null && value != null)
                    || (_backRoundKnobImage != null && !_backRoundKnobImage.Equals(value)))
                {
                    string oldValue = _backRoundKnobImage;
                    _backRoundKnobImage = value;
                    OnPropertyChanged("BackRoundKnobImage", oldValue, value, true);
                    Refresh();
                }
            }
        }

        public Double BasementLeft
        {
            get { return _basementLeft; }
            set
            {
                if (_basementLeft != value)
                {
                    Double oldValue = _basementLeft;
                    _basementLeft = value;
                    OnPropertyChanged("BasementLeft", oldValue, value, true);
                    Refresh();
                }
            }
        }

        public Double BasementTop
        {
            get { return _basementTop; }
            set
            {
                if (_basementTop != value)
                {
                    Double oldValue = _basementTop;
                    _basementTop = value;
                    OnPropertyChanged("BasementTop", oldValue, value, true);
                    Refresh();
                }
            }
        }

        public Double FrontLeft
        {
            get { return _frontLeft; }
            set
            {
                if (_frontLeft != value)
                {
                    Double oldValue = _frontLeft;
                    _frontLeft = value;
                    OnPropertyChanged("FrontLeft", oldValue, value, true);
                    Refresh();
                }
            }
        }

        public Double FrontTop
        {
            get { return _frontTop; }
            set
            {
                if (_frontTop != value)
                {
                    Double oldValue = _frontTop;
                    _frontTop = value;
                    OnPropertyChanged("FrontTop", oldValue, value, true);
                    Refresh();
                }
            }
        }


        public Double PullDistanceX
        {
            get { return _pullDistanceX; }
            set
            {
                if (_pullDistanceX != value)
                {
                    Double oldValue = _pullDistanceX;
                    _pullDistanceX = value;
                    OnPropertyChanged("PullDistanceX", oldValue, value, true);
                    Refresh();
                }
            }
        }

        public Double PullDistanceY
        {
            get { return _pullDistanceY; }
            set
            {
                if (_pullDistanceY != value)
                {
                    Double oldValue = _pullDistanceY;
                    _pullDistanceY = value;
                    OnPropertyChanged("PullDistanceY", oldValue, value, true);
                    Refresh();
                }
            }
        }

        public Double AdjustCenterX
        {
            get { return _adjustCenterX; }
            set
            {
                if (_adjustCenterX != value)
                {
                    Double oldValue = _adjustCenterX;
                    _adjustCenterX = value;
                    OnPropertyChanged("AdjustCenterX", oldValue, value, true);
                    Refresh();
                }
            }
        }

        public Double AdjustCenterY
        {
            get { return _adjustCenterY; }
            set
            {
                if (_adjustCenterY != value)
                {
                    Double oldValue = _adjustCenterY;
                    _adjustCenterY = value;
                    OnPropertyChanged("AdjustCenterY", oldValue, value, true);
                    Refresh();
                }
            }
        }

        #endregion

        void Positions_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.OldItems != null)
            {
                foreach (RoundSolidPullKnobPosition position in e.OldItems)
                {
                    Triggers.Remove(position.EnterTriggger);
                    Triggers.Remove(position.ExitTrigger);
                }

                if (Positions.Count == 0)
                {
                    _currentPosition = 0;
                }
                else if (_currentPosition > Positions.Count)
                {
                    _currentPosition = Positions.Count;
                }
            }

            if (e.NewItems != null)
            {
                foreach (RoundSolidPullKnobPosition position in e.NewItems)
                {
                    Triggers.Add(position.EnterTriggger);
                    Triggers.Add(position.ExitTrigger);
                }
            }

            // Need to do it twice to prevent collisions.  This is
            // just an easy way to do it instead of reordering everything
            // in the loops above.
            int i = 1000000;
            foreach (RoundSolidPullKnobPosition position in Positions)
            {
                position.Index = i++;
            }

            i = 1;
            foreach (RoundSolidPullKnobPosition position in Positions)
            {
                position.Index = i++;
            }
            UpdateValueHelp();
            UpdatePositionIndex();
        }

        private void PositionsChanged(object sender, RoundSolidPullKnobPositionChangeArgs e)
        {
            PropertyNotificationEventArgs args = new PropertyNotificationEventArgs(
                e.Position, e.PropertyName, e.OldValue, e.NewValue, true);
            OnPropertyChanged("Positions", args);
            UpdateValueHelp();
            if (IsRenderReady) {
                Refresh();
            }
            
        }

        private void UpdateValueHelp()
        {
            StringBuilder sb = new StringBuilder("");
            sb.Append(" (");
            for (int i = 0; i < Positions.Count; i++)
            {
                if (i > 0)
                {
                    sb.Append(",");
                }
                RoundSolidPullKnobPosition position = Positions[i];
                sb.Append(i);
            }
            sb.Append(")");
            _positionValue.ValueDescription = sb.ToString();
        }

        private class PositionIndexEntry
        {
            public double Rotation { get; set; }
            public int Index { get; set; }
        }

        private class PositionSortComparer : IComparer<PositionIndexEntry>
        {
            public int Compare(PositionIndexEntry left, PositionIndexEntry right) => (int)(left.Index - right.Index);
        }

        private void UpdatePositionIndex()
        {
            _positionIndex = Positions.OrderBy(p => p.Index)
                .Select(p => new PositionIndexEntry { Index = p.Index }).ToArray();
        }

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
            base.MouseDown(location);
        }

        public override void MouseUp(Point location)
        {
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
            else if (newPosition < 1)
            {
                CurrentPosition = 1;
            }
            else
            {
                CurrentPosition = newPosition;
            }
        }

        #endregion

        #region IRotarySwitch

        public int MinPosition => 1;

        public int MaxPosition => Positions.Count;

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
            TypeConverter colorConverter = TypeDescriptor.GetConverter(typeof(Color));

            base.WriteXml(writer);

            writer.WriteElementString("KnobImage", KnobImage);
            writer.WriteElementString("Pullable", Pullable.ToString());
            writer.WriteElementString("KnobImagePulled", KnobImagePulled);
            writer.WriteElementString("BackRoundKnobImage", BackRoundKnobImage);
            writer.WriteElementString("FrontImage", FrontImage);

            writer.WriteElementString("BasementLeft", Convert.ToString(_basementLeft));
            writer.WriteElementString("BasementTop", Convert.ToString(_basementTop));

            writer.WriteElementString("FrontLeft", Convert.ToString(_frontLeft));
            writer.WriteElementString("FrontTop", Convert.ToString(_frontTop));

            writer.WriteElementString("PullDistanceX", Convert.ToString(_pullDistanceX));
            writer.WriteElementString("PullDistanceY", Convert.ToString(_pullDistanceY));

            writer.WriteElementString("AdjustCenterX", Convert.ToString(_adjustCenterX));
            writer.WriteElementString("AdjustCenterY", Convert.ToString(_adjustCenterY));

            writer.WriteStartElement("Positions");
            foreach (RoundSolidPullKnobPosition position in Positions)
            {
                writer.WriteStartElement("Position");
                writer.WriteAttributeString("CanPull", Convert.ToString(position.CanPull));
                writer.WriteAttributeString("SendValue", Convert.ToString(position.SendValue));
                writer.WriteAttributeString("KnobRotateImage", Convert.ToString(position.KnobRotateImage));
                writer.WriteAttributeString("KnobPulledbRotateImage", Convert.ToString(position.KnobPulledbRotateImage));
                writer.WriteAttributeString("Rotation", Convert.ToString(position.Rotation));

                writer.WriteEndElement();
            }
            writer.WriteEndElement();
            writer.WriteElementString("DefaultPosition", DefaultPosition.ToString(CultureInfo.InvariantCulture));

            if (IsContinuous)
            {
                writer.WriteElementString("Continuous", IsContinuous.ToString(CultureInfo.InvariantCulture));
            }
            if (IsReleaseManual)
            {
                writer.WriteElementString("IsReleaseManual", IsReleaseManual.ToString(CultureInfo.InvariantCulture));
            }
            if (IsBasementFlag)
            {
                writer.WriteElementString("IsBasementFlag", IsBasementFlag.ToString(CultureInfo.InvariantCulture));
            }
            writer.WriteElementString("IsSmoothKnob", IsSmoothKnob.ToString(CultureInfo.InvariantCulture));
            writer.WriteElementString("WaitMillisecond", WaitMillisecond.ToString(CultureInfo.InvariantCulture));
            
            writer.WriteElementString("InclDeclStep", InclDeclStep.ToString(CultureInfo.InvariantCulture));
            
            WriteOptionalXml(writer);
        }

        public override void ReadXml(XmlReader reader)
        {
            IsRenderReady = false;
            TypeConverter bc = TypeDescriptor.GetConverter(typeof(bool));

            base.ReadXml(reader);
            KnobImage = reader.ReadElementString("KnobImage");
            
            if (reader.Name.Equals("Pullable"))
            {
                Pullable = (bool)bc.ConvertFromInvariantString(reader.ReadElementString());
            }
            KnobImagePulled = reader.ReadElementString("KnobImagePulled");
            BackRoundKnobImage = reader.ReadElementString("BackRoundKnobImage");

            FrontImage = reader.ReadElementString("FrontImage");

            BasementLeft = Convert.ToDouble(reader.ReadElementString("BasementLeft"));
            BasementTop = Convert.ToDouble(reader.ReadElementString("BasementTop"));

            FrontLeft = Convert.ToDouble(reader.ReadElementString("FrontLeft"));
            FrontTop = Convert.ToDouble(reader.ReadElementString("FrontTop"));

            PullDistanceX = Convert.ToDouble(reader.ReadElementString("PullDistanceX"));
            PullDistanceY = Convert.ToDouble(reader.ReadElementString("PullDistanceY"));

            AdjustCenterX = Convert.ToDouble(reader.ReadElementString("AdjustCenterX"));
            AdjustCenterY = Convert.ToDouble(reader.ReadElementString("AdjustCenterY"));

            if (!reader.IsEmptyElement)
            {
                Positions.Clear();
                reader.ReadStartElement("Positions");
                int i = 1;
                while (reader.NodeType != XmlNodeType.EndElement)
                {
                    var canpull = Convert.ToBoolean(reader.GetAttribute("CanPull"));
                    var sendValue = Convert.ToDouble(reader.GetAttribute("SendValue"));
                    var knobRotateImage = reader.GetAttribute("KnobRotateImage");
                    var knobPulledbRotateImage = reader.GetAttribute("KnobPulledbRotateImage");

                    double rotation = Convert.ToDouble(reader.GetAttribute("Rotation"));

                    Positions.Add(new RoundSolidPullKnobPosition(this, i++,
                        canpull, sendValue, rotation, knobRotateImage, knobPulledbRotateImage));
                    reader.Read();
                }
                reader.ReadEndElement();
            }
            else
            {
                reader.Read();
            }
            DefaultPosition = int.Parse(reader.ReadElementString("DefaultPosition"), CultureInfo.InvariantCulture);

            
            if (reader.Name.Equals("Continuous"))
            {
                IsContinuous = (bool)bc.ConvertFromInvariantString(reader.ReadElementString());
            }
            if (reader.Name.Equals("IsReleaseManual"))
            {
                IsReleaseManual = (bool)bc.ConvertFromInvariantString(reader.ReadElementString());
            }
            if (reader.Name.Equals("IsBasementFlag"))
            {
                IsBasementFlag = (bool)bc.ConvertFromInvariantString(reader.ReadElementString());
            }
            if (reader.Name.Equals("IsSmoothKnob"))
            {
                IsSmoothKnob = (bool)bc.ConvertFromInvariantString(reader.ReadElementString());
            }
            if (reader.Name.Equals("WaitMillisecond"))
            {
                int.TryParse(reader.ReadElementString(), out int waitTime);
                WaitMillisecond = waitTime;
            }

            if (reader.Name.Equals("InclDeclStep"))
            {
                Double.TryParse(reader.ReadElementString(), out double incldeclStep);
                InclDeclStep = incldeclStep;
            }

            ReadOptionalXml(reader);
            _currentPosition = DefaultPosition;
            IsRenderReady = true;
        }
    }
}