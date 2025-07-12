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

namespace GadrocsWorkshop.Helios.Gauges.F5E
{
    using GadrocsWorkshop.Helios.ComponentModel;
    using GadrocsWorkshop.Helios.Controls;
    using GadrocsWorkshop.Helios.Interfaces.DCS.F5E.Functions;
    using System;
    using System.Windows;

    [HeliosControl("Helios.F5E.GuardCloseOnToggleState", "Guard", "F-5E", typeof(GuardCloseOnToggleStateRenderer))]
    public class GuardCloseOnToggleState : HeliosVisual
    {
        private static readonly Rect GuardUpRegionDefault = new Rect(0, 0, 50, 60);
        private static readonly Rect SwitchRegionDefault = new Rect(0, 30, 50, 50);
        private static readonly Rect GuardDownRegionDefault = new Rect(0, 75, 50, 60);

        private bool noUseClosable = false;

        private LinearClickType _clickType = LinearClickType.Swipe;
        private ToggleSwitchOrientation _orientation;

        private ToggleSwitchType _switchType = ToggleSwitchType.OnOn;
        private GuardPosition _guardPosition = GuardPosition.Down;

        private string _positionOneGuardUpImage;
        private string _positionTwoGuardDownImage;

        private ToggleSwitchPosition _defaultPosition = ToggleSwitchPosition.Two;
        private GuardPosition _defaultGuardPosition = GuardPosition.Down;

        private HeliosValue _guardPositionValue;
        private HeliosTrigger _releaseTrigger;

        private HeliosTrigger _guardUpAction;
        private HeliosTrigger _guardDownAction;

        private HeliosValue _guardReferLeverValue; // closable lever reference

        public Rect _guardUpRegion;
        public Rect _switchRegion;
        public Rect _guardDownRegion;
        
        private DateTime _lastChanged;
        private Point? _draggingLast;

        private static int WaitMillisecond = 300;   // wait milsecond
        private int _referLeverValue = 0;
        private int _closableValue = 1;
        private int _openDirection = 0; // 0: drag upward to open cover
                                        // 1: drag downward to open cover


        private Point _mouseDownLocation;
        private bool _mouseAction;

        public GuardCloseOnToggleState()
            : base("GuardCloseOnToggleState", new System.Windows.Size(46, 100))
        {
            _positionOneGuardUpImage = "{F-5E}/Images/MetalLevers/MasterArmCover/MasterArmCover_03.png";
            _positionTwoGuardDownImage = "{F-5E}/Images/MetalLevers/MasterArmCover/MasterArmCover_01.png";

            _guardUpRegion = GuardCloseOnToggleState.GuardUpRegionDefault;
            _switchRegion = GuardCloseOnToggleState.SwitchRegionDefault;
            _guardDownRegion = GuardCloseOnToggleState.GuardDownRegionDefault;

            _guardUpAction = new HeliosTrigger(this, "", "guard", "up", "Triggered when guard is moved up.");
            Triggers.Add(_guardUpAction);
            _guardDownAction = new HeliosTrigger(this, "", "guard", "down", "Triggered when guard is moved down.");
            Triggers.Add(_guardDownAction);
            _releaseTrigger = new HeliosTrigger(this, "", "", "released", "This trigger is fired when the user releases pressure on the switch (lifts finger or mouse button.).");
            Triggers.Add(_releaseTrigger);

            _guardPositionValue = new HeliosValue(this, new BindingValue((double)GuardPosition), "", "guard position", "Current position of the switch guard.", "1 = Up, 2 = Down.", BindingValueUnits.Numeric);
            _guardPositionValue.Execute += new HeliosActionHandler(SetGuardPositionAction_Execute);

            // _guardReferLeverValue = new HeliosValue(this, new BindingValue(0), "", "guard lever value",
            //     "Current Lever posiotion of Inner Guard", "1:DN, 2:MD(UP),3:UP.", BindingValueUnits.Numeric);
            // _guardReferLeverValue.Execute += new HeliosActionHandler(SetGuardLeverPosition_Execute);


            Values.Add(_guardPositionValue);
            Actions.Add(_guardPositionValue);
            Triggers.Add(_guardPositionValue);
        }

        public GuardCloseOnToggleState(string name, Point posn, Size size,
            string guardUpImagePath, string guardDownImagePath, bool noUseClosable, int openDirection,
            Rect guardOpenRegion, Rect switchRegion, Rect guardCloseRegion)
            : base(name, new System.Windows.Size(size.Width, size.Height))
        {
            Left = posn.X;
            Top = posn.Y;

            _positionOneGuardUpImage = guardUpImagePath;
            _positionTwoGuardDownImage = guardDownImagePath;

            _guardUpAction = new HeliosTrigger(this, "", "guard", "up", "Triggered when guard is moved up.");
            Triggers.Add(_guardUpAction);
            _guardDownAction = new HeliosTrigger(this, "", "guard", "down", "Triggered when guard is moved down.");
            Triggers.Add(_guardDownAction);
            _releaseTrigger = new HeliosTrigger(this, "", "", "released", "This trigger is fired when the user releases pressure on the switch (lifts finger or mouse button.).");
            Triggers.Add(_releaseTrigger);

            _guardPositionValue = new HeliosValue(this, new BindingValue((double)GuardPosition), "", "guard position", "Current position of the switch guard.", "1 = Up, 2 = Down.", BindingValueUnits.Numeric);
            _guardPositionValue.Execute += new HeliosActionHandler(SetGuardPositionAction_Execute);

            _guardReferLeverValue = new HeliosValue(this, new BindingValue(0), "", "guard lever value",
                "Current Lever posiotion of Inner Guard", "1:DN, 2:MD(UP),3:UP.", BindingValueUnits.Numeric);
            _guardReferLeverValue.Execute += new HeliosActionHandler(SetGuardLeverPosition_Execute);

            Values.Add(_guardPositionValue);
            Actions.Add(_guardPositionValue);
            Triggers.Add(_guardPositionValue);
            // set touch region
            this._guardDownRegion = guardCloseRegion;
            this._switchRegion = switchRegion;
            this._guardUpRegion = guardOpenRegion;

            this.noUseClosable = noUseClosable;
        }

        #region Properties

        public LinearClickType ClickType
        {
            get
            {
                return _clickType;
            }
            set
            {
                if (!_clickType.Equals(value))
                {
                    LinearClickType oldValue = _clickType;
                    _clickType = value;
                    OnPropertyChanged("ClickType", oldValue, value, true);
                }
            }
        }

        public ToggleSwitchOrientation Orientation
        {
            get
            {
                return _orientation;
            }
            set
            {
                if (!_orientation.Equals(value))
                {
                    ToggleSwitchOrientation oldValue = _orientation;
                    _orientation = value;
                    OnPropertyChanged("Orientation", oldValue, value, true);
                }
            }
        }

        public GuardPosition GuardPosition
        {
            get
            {
                return _guardPosition;
            }
            set
            {
                if (!_guardPosition.Equals(value))
                {
                    GuardPosition oldValue = _guardPosition;
                    _guardPosition = value;

                    _guardPositionValue.SetValue(new BindingValue((double)_guardPosition), BypassTriggers);
                    if (!BypassTriggers)
                    {
                        switch (value)
                        {
                            case GuardPosition.Up:
                                _guardUpAction.FireTrigger(BindingValue.Empty);
                                break;
                            case GuardPosition.Down:
                                _guardDownAction.FireTrigger(BindingValue.Empty);
                                break;
                        }

                    }

                    OnPropertyChanged("GuardPosition", oldValue, value, false);
                    OnDisplayUpdate();
                }
            }
        }

        public int GuardLeverPosition
        {
            get
            {
                return _referLeverValue;
            }
            set
            {
                if (!_guardPosition.Equals(value))
                {
                    GuardPosition oldValue = _guardPosition;
                    _referLeverValue = value;
                    OnPropertyChanged("GuardLeverPosition", oldValue, value, false);
                    OnDisplayUpdate();
                }
            }
        }

        public int ClosableValue
        {
            get
            {
                return _closableValue;
            }
            set
            {
                if (!_closableValue.Equals(value))
                {
                    int oldValue = _closableValue;
                    _closableValue = value;
                    OnPropertyChanged("ClosableValue", oldValue, value, false);
                    OnDisplayUpdate();
                }
            }
        }

        public int OpenDirection
        {
            get
            {
                return _openDirection;
            }
            set
            {
                if (!_openDirection.Equals(value))
                {
                    int oldValue = _openDirection;
                    _closableValue = value;
                    OnPropertyChanged("OpenDirection", oldValue, value, false);
                    OnDisplayUpdate();
                }
            }
        }

        public Rect OpenRegion
        {
            get
            {
                return _guardUpRegion;
            }
            set
            {
                if (!_guardUpRegion.Equals(value))
                {
                    Rect oldValue = _guardUpRegion;
                    _guardUpRegion = value;
                    OnPropertyChanged("OpenRegion", oldValue, value, false);
                    OnDisplayUpdate();
                }
            }
        }

        public Rect SwitchRegion
        {
            get
            {
                return _switchRegion;
            }
            set
            {
                if (!_switchRegion.Equals(value))
                {
                    Rect oldValue = _switchRegion;
                    _switchRegion = value;
                    OnPropertyChanged("SwitchRegion", oldValue, value, false);
                    OnDisplayUpdate();
                }
            }
        }

        public Rect CloseRegion
        {
            get
            {
                return _guardDownRegion;
            }
            set
            {
                if (!_guardDownRegion.Equals(value))
                {
                    Rect oldValue = _guardDownRegion;
                    _guardDownRegion = value;
                    OnPropertyChanged("CloseRegion", oldValue, value, false);
                    OnDisplayUpdate();
                }
            }
        }

        public ToggleSwitchType SwitchType
        {
            get
            {
                return _switchType;
            }
            set
            {
                if (!_switchType.Equals(value))
                {
                    ToggleSwitchType oldValue = _switchType;
                    _switchType = value;
                    OnPropertyChanged("SwitchType", oldValue, value, false);
                    OnDisplayUpdate();
                }
            }
        }

        public ToggleSwitchPosition DefaultPosition
        {
            get
            {
                return _defaultPosition;
            }
            set
            {
                if (!_defaultPosition.Equals(value))
                {
                    ToggleSwitchPosition oldValue = _defaultPosition;
                    _defaultPosition = value;
                    OnPropertyChanged("DefaultPosition", oldValue, value, true);
                }
            }
        }

        public GuardPosition DefaultGuardPosition
        {
            get
            {
                return _defaultGuardPosition;
            }
            set
            {
                if (!_defaultGuardPosition.Equals(value))
                {
                    GuardPosition oldValue = _defaultGuardPosition;
                    _defaultGuardPosition = value;
                    OnPropertyChanged("DefaultGuardPosition", oldValue, value, true);
                }
            }
        }


        public string PositionOneGuardUpImage
        {
            get
            {
                return _positionOneGuardUpImage;
            }
            set
            {
                if ((_positionOneGuardUpImage == null && value != null)
                    || (_positionOneGuardUpImage != null && !_positionOneGuardUpImage.Equals(value)))
                {
                    string oldValue = _positionOneGuardUpImage;
                    _positionOneGuardUpImage = value;
                    OnPropertyChanged("PositionOneGuardUpImage", oldValue, value, true);
                    Refresh();
                }
            }
        }

        public string PositionTwoGuardDownImage
        {
            get
            {
                return _positionTwoGuardDownImage;
            }
            set
            {
                if ((_positionTwoGuardDownImage == null && value != null)
                    || (_positionTwoGuardDownImage != null && !_positionTwoGuardDownImage.Equals(value)))
                {
                    string oldValue = _positionTwoGuardDownImage;
                    _positionTwoGuardDownImage = value;
                    OnPropertyChanged("PositionTwoGuardDownImage", oldValue, value, true);
                    Refresh();
                }
            }
        }

        #endregion

        protected override void OnPropertyChanged(PropertyNotificationEventArgs args)
        {
            if (args.PropertyName.Equals("Width") || args.PropertyName.Equals("Height"))
            {
                double scaleX = Width / NativeSize.Width;
                double scaleY = Height / NativeSize.Height;

                // _guardUpRegion.Scale(scaleX, scaleY);
                // _switchRegion.Scale(scaleX, scaleY);
                // _guardDownRegion.Scale(scaleX, scaleY);
            }
            base.OnPropertyChanged(args);
        }

        #region HeliosControl Implementation

        public override void Reset()
        {
            base.Reset();

            BeginTriggerBypass(true);
            GuardPosition = DefaultGuardPosition;
            EndTriggerBypass(true);
        }

        public override bool HitTest(Point location)
        {
            switch (GuardPosition)
            {
                case GuardPosition.Up:
                    return _guardUpRegion.Contains(location);
                case GuardPosition.Down:
                    return _guardDownRegion.Contains(location);
            }
            return false;
        }

        public override void MouseDown(Point location)
        {
            if (_referLeverValue != 0 && ClosableValue != _referLeverValue && noUseClosable)
            {
                return;
            }
        }

        public override void MouseDrag(Point location)
        {
            if (_referLeverValue != 0 && ClosableValue != _referLeverValue && noUseClosable)
            {
                return;
            }
            if (!_mouseAction)
            {

                if (_lastChanged == null)
                {
                    _lastChanged = DateTime.Now;
                }
                if ((DateTime.Now - _lastChanged) < TimeSpan.FromMilliseconds(WaitMillisecond))
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
                
                var dragDistace = Math.Sqrt(diff.X * diff.X + diff.Y * diff.Y);
                if (dragDistace < 20)
                {
                    return;
                }
                // var dragAngle = Math.Atan2(diff.Y, diff.X); // rad
                // var dragJudgeToward = dragDistace * Math.Sin(dragAngle);
                if (GuardPosition == Controls.GuardPosition.Down)
                {
                        GuardPosition = GuardPosition.Up;
                        _mouseAction = true;
                }
                else if (GuardPosition == Controls.GuardPosition.Up)
                {
                        GuardPosition = GuardPosition.Down;
                        _mouseAction = true;
                }

            }
        }

        public override void MouseUp(Point location)
        {
            if (_mouseAction)
            {
                _releaseTrigger.FireTrigger(BindingValue.Empty);
                _mouseAction = false;
            }

        }

        public override void WriteXml(System.Xml.XmlWriter writer)
        {
            base.WriteXml(writer);
            writer.WriteElementString("SwitchType", SwitchType.ToString());
            writer.WriteElementString("Orientation", Orientation.ToString());
            writer.WriteElementString("ClickType", ClickType.ToString());
            writer.WriteStartElement("GuardUp");
            writer.WriteElementString("PositionOneImage", PositionOneGuardUpImage);
            writer.WriteElementString("PositionTwoImage", PositionTwoGuardDownImage);// writing this value just for compatibility with old profiles
            writer.WriteEndElement();
            writer.WriteStartElement("GuardDown");
            writer.WriteElementString("PositionOneImage", PositionOneGuardUpImage);// writing this value just for compatibility with old profiles
            writer.WriteElementString("PositionTwoImage", PositionTwoGuardDownImage);
            writer.WriteEndElement();
            writer.WriteElementString("DefaultPosition", "Two"); // writing this value just for compatibility with old profiles
            writer.WriteElementString("DefaultGuardPosition", DefaultGuardPosition.ToString());
            writer.WriteElementString("ClosablePosition", this._closableValue.ToString());
            writer.WriteElementString("OpenDirection", this._openDirection.ToString());

            /*
            writer.WriteStartElement("GuardCloseRegion");
            writer.WriteElementString("GuardCloseRegionX", this._guardDownRegion.X.ToString());
            writer.WriteElementString("GuardCloseRegionY", this._guardDownRegion.Y.ToString());
            writer.WriteElementString("GuardCloseWidth", this._guardDownRegion.Width.ToString());
            writer.WriteElementString("GuardCloseHeight", this._guardDownRegion.Height.ToString());
            writer.WriteEndElement();

            writer.WriteStartElement("GuardSwitchRegion");
            writer.WriteElementString("GuardSwtichRegionX", this._switchRegion.X.ToString());
            writer.WriteElementString("GuardSwtichRegionY", this._switchRegion.Y.ToString());
            writer.WriteElementString("GuardSwtichWidth", this._switchRegion.Width.ToString());
            writer.WriteElementString("GuardSwtichHeight", this._switchRegion.Height.ToString());
            writer.WriteEndElement();

            writer.WriteStartElement("GuardOpenRegion");
            writer.WriteElementString("GuardOpenRegionX", this._guardUpRegion.X.ToString());
            writer.WriteElementString("GuardOpenRegionY", this._guardUpRegion.Y.ToString());
            writer.WriteElementString("GuardOpenWidth", this._guardUpRegion.Width.ToString());
            writer.WriteElementString("GuardOpenHeight", this._guardUpRegion.Height.ToString());
            */
            writer.WriteEndElement();

        }

        public override void ReadXml(System.Xml.XmlReader reader)
        {
            base.ReadXml(reader);
            SwitchType = (ToggleSwitchType)Enum.Parse(typeof(ToggleSwitchType), reader.ReadElementString("SwitchType"));
            Orientation = (ToggleSwitchOrientation)Enum.Parse(typeof(ToggleSwitchOrientation), reader.ReadElementString("Orientation"));
            if (reader.Name.Equals("ClickType"))
            {
                ClickType = (LinearClickType)Enum.Parse(typeof(LinearClickType), reader.ReadElementString("ClickType"));
            }
            else
            {
                ClickType = LinearClickType.Swipe;
            }

            reader.ReadStartElement("GuardUp");
            PositionOneGuardUpImage = reader.ReadElementString("PositionOneImage");
            reader.ReadElementString("PositionTwoImage"); // reading this value just for compatibility with old profiles
            reader.ReadEndElement();

            reader.ReadStartElement("GuardDown");
            reader.ReadElementString("PositionOneImage"); // reading this value just for compatibility with old profiles
            PositionTwoGuardDownImage = reader.ReadElementString("PositionTwoImage");
            reader.ReadEndElement();

            reader.ReadElementString("DefaultPosition"); // reading this value just for compatibility with old profiles
            DefaultGuardPosition = (GuardPosition)Enum.Parse(typeof(GuardPosition), reader.ReadElementString("DefaultGuardPosition"));
            
            ClosableValue = Convert.ToInt32(reader.ReadElementString("ClosablePosition"));
            OpenDirection = Convert.ToInt32(reader.ReadElementString("OpenDirection"));

            
            if (reader.Name.Equals("GuardCloseRegion"))
            {
                reader.ReadStartElement("GuardCloseRegion");
               /*
                Double.TryParse(reader.ReadElementString("GuardCloseRegionX"), out Double closeRegionX);
                Double.TryParse(reader.ReadElementString("GuardCloseRegionY"), out Double closeRegionY);
                Double.TryParse(reader.ReadElementString("GuardCloseWidth"), out Double closeRegionWidth);
                Double.TryParse(reader.ReadElementString("GuardCloseHeight"), out Double closeRegionHeight);
                this._guardDownRegion = new Rect(closeRegionX, closeRegionY, closeRegionWidth, closeRegionHeight);
                */
                reader.ReadEndElement();
            }

            if (reader.Name.Equals("GuardSwitchRegion"))
            {
                reader.ReadStartElement("GuardSwitchRegion");
                /*
                Double.TryParse(reader.ReadElementString("GuardSwtichRegionX"), out Double switchRegionX);
                Double.TryParse(reader.ReadElementString("GuardSwtichRegionY"), out Double switchRegionY);
                Double.TryParse(reader.ReadElementString("GuardSwtichWidth"), out Double switchRegionWidth);
                Double.TryParse(reader.ReadElementString("GuardSwtichHeight"), out Double switchRegionHeight);
                this._switchRegion = new Rect(switchRegionX, switchRegionY, switchRegionWidth, switchRegionHeight);
                */
                reader.ReadEndElement();
            }

            if (reader.Name.Equals("GuardOpenRegion"))
            {
                reader.ReadStartElement("GuardOpenRegion");
                /*
                Double.TryParse(reader.ReadElementString("GuardOpenRegionX"), out Double openRegionX);
                Double.TryParse(reader.ReadElementString("GuardOpenRegionY"), out Double openRegionY);
                Double.TryParse(reader.ReadElementString("GuardOpenWidth"), out Double openRegionWidth);
                Double.TryParse(reader.ReadElementString("GuardOpenHeight"), out Double openRegionHeight);
                this._guardUpRegion = new Rect(openRegionX, openRegionY, openRegionWidth, openRegionHeight);
                */
                reader.ReadEndElement();
            }


            BeginTriggerBypass(true);

            GuardPosition = DefaultGuardPosition;
            EndTriggerBypass(true);
        }

        #endregion

        #region Actions


        void SetGuardPositionAction_Execute(object action, HeliosActionEventArgs e)
        {
            try
            {
                BeginTriggerBypass(e.BypassCascadingTriggers);
                int newPosition = 0;
                if (int.TryParse(e.Value.StringValue, out newPosition))
                {
                    if (newPosition > 0 && newPosition < 3)
                    {
                        GuardPosition = (GuardPosition)newPosition;
                    }
                }

                EndTriggerBypass(e.BypassCascadingTriggers);
            }
            catch
            {
                // No-op if the parse fails we won't set the position.
            }
        }

        void SetGuardLeverPosition_Execute(object action, HeliosActionEventArgs e)
        {
            try
            {
                BeginTriggerBypass(e.BypassCascadingTriggers);
                int newPosition = 0;
                if (int.TryParse(e.Value.StringValue, out newPosition))
                {
                    if (newPosition > 0 && newPosition < 3)
                    {
                        GuardLeverPosition = newPosition;
                    }
                }

                EndTriggerBypass(e.BypassCascadingTriggers);
            }
            catch
            {
                // No-op if the parse fails we won't set the position.
            }
        }
        

        #endregion
    }
}
