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

namespace GadrocsWorkshop.Helios.Controls
{
    using GadrocsWorkshop.Helios.ComponentModel;
    using System;
    using System.Windows;
    using System.Xml;

    [HeliosControl("Helios.Base.ThreeWayDispToggle", "Three Way DisplayToggle", "F-5E", typeof(ThreeWayDispToggleRenderer))]
    public class ThreeWayDispToggle : ToggleSwitchBase
    {
        private ThreeWayDispToggleType _switchType = ThreeWayDispToggleType.OnOnOn;
        private ThreeWayDispTogglePosition _position = ThreeWayDispTogglePosition.Two;
        private string _positionOneImage;
        private string _positionOneImageIndicatorOn;
        private string _positionTwoImage;
        private string _positionTwoImageIndicatorOn;
        private string _positionThreeImage;
        private string _positionThreeImageIndicatorOn;

        private ThreeWayDispTogglePosition _defaultPosition = ThreeWayDispTogglePosition.Two;

        private HeliosValue _positionValue;

        public ThreeWayDispToggle() : this("Three Way Disp Toggle", new System.Windows.Size(50, 100)) {}
        public ThreeWayDispToggle(string name, System.Windows.Size size)
            : base(name, size)
        {
            _positionOneImage = "{Helios}/Images/Toggles/toggle-up.png";
            _positionTwoImage = "{Helios}/Images/Toggles/toggle-norm.png";
            _positionThreeImage = "{Helios}/Images/Toggles/toggle-down.png";

            _positionValue = new HeliosValue(this, new BindingValue((double)SwitchPosition), "", "position", "Current position of the switch.", "Position number 1,2 or 3.  Positions are numbered from top to bottom.", BindingValueUnits.Numeric);
            _positionValue.Execute += new HeliosActionHandler(SetPositionAction_Execute);
            Values.Add(_positionValue);
            Actions.Add(_positionValue);
            Triggers.Add(_positionValue);

        }
 
        #region Properties

        public ThreeWayDispTogglePosition DefaultPosition
        {
            get
            {
                return _defaultPosition;
            }
            set
            {
                if (!_defaultPosition.Equals(value))
                {
                    ThreeWayDispTogglePosition oldValue = _defaultPosition;
                    _defaultPosition = value;
                    OnPropertyChanged("DefaultPosition", oldValue, value, true);
                }
            }
        }

        public ThreeWayDispToggleType SwitchType
        {
            get
            {
                return _switchType;
            }
            set
            {
                if (!_switchType.Equals(value))
                {
                    ThreeWayDispToggleType oldValue = _switchType;
                    _switchType = value;
                    OnPropertyChanged("SwitchType", oldValue, value, true);
                }
            }
        }

        public ThreeWayDispTogglePosition SwitchPosition
        {
            get
            {
                return _position;
            }
            set
            {
                if (!_position.Equals(value))
                {
                    ThreeWayDispTogglePosition oldValue = _position;

                    if (!BypassTriggers)
                    {
                        switch (oldValue)
                        {
                            case ThreeWayDispTogglePosition.One:
                                break;
                            case ThreeWayDispTogglePosition.Two:
                                break;
                            case ThreeWayDispTogglePosition.Three:
                                break;
                        }
                    }

                    _position = value;
                    _positionValue.SetValue(new BindingValue((double)_position), BypassTriggers);

                    if (!BypassTriggers)
                    {
                        switch (value)
                        {
                            case ThreeWayDispTogglePosition.One:
                                break;
                            case ThreeWayDispTogglePosition.Two:
                                break;
                            case ThreeWayDispTogglePosition.Three:
                                break;
                        }
                    }

                    OnPropertyChanged("SwitchPosition", oldValue, value, false);
                    OnDisplayUpdate();
                }
            }
        }

        public string PositionOneImage
        {
            get
            {
                return _positionOneImage;
            }
            set
            {
                if ((_positionOneImage == null && value != null)
                    || (_positionOneImage != null && !_positionOneImage.Equals(value)))
                {
                    string oldValue = _positionOneImage;
                    _positionOneImage = value;
                    OnPropertyChanged("PositionOneImage", oldValue, value, true);
                    Refresh();
                }
            }
        }

        public string PositionOneIndicatorOnImage
        {
            get
            {
                return _positionOneImageIndicatorOn;
            }
            set
            {
                if ((_positionOneImageIndicatorOn == null && value != null)
                    || (_positionOneImageIndicatorOn != null &&
                    !_positionOneImageIndicatorOn.Equals(value)))
                {
                    string oldValue = _positionOneImageIndicatorOn;
                    _positionOneImageIndicatorOn = value;
                    OnPropertyChanged("PositionOneIndicatorOnImage", oldValue, value, true);
                    Refresh();
                }
            }
        }

        public string PositionThreeImage
        {
            get
            {
                return _positionThreeImage;
            }
            set
            {
                if ((_positionThreeImage == null && value != null)
                    || (_positionThreeImage != null &&
                    !_positionThreeImage.Equals(value)))
                {
                    string oldValue = _positionThreeImage;
                    _positionThreeImage = value;
                    OnPropertyChanged("PositionThreeImage", oldValue, value, true);
                    Refresh();
                }
            }
        }

        public string PositionThreeIndicatorOnImage
        {
            get
            {
                return _positionThreeImageIndicatorOn;
            }
            set
            {
                if ((_positionThreeImageIndicatorOn == null && value != null)
                    || (_positionThreeImageIndicatorOn != null && !_positionThreeImageIndicatorOn.Equals(value)))
                {
                    string oldValue = _positionThreeImageIndicatorOn;
                    _positionThreeImageIndicatorOn = value;
                    OnPropertyChanged("PositionThreeIndicatorOnImage", oldValue, value, true);
                    Refresh();
                }
            }
        }


        public string PositionTwoImage
        {
            get
            {
                return _positionTwoImage;
            }
            set
            {
                if ((_positionTwoImage == null && value != null)
                    || (_positionTwoImage != null && !_positionTwoImage.Equals(value)))
                {
                    string oldValue = _positionTwoImage;
                    _positionTwoImage = value;
                    OnPropertyChanged("PositionTwoImage", oldValue, value, true);
                    Refresh();
                }
            }
        }

        public string PositionTwoIndicatorOnImage
        {
            get
            {
                return _positionTwoImageIndicatorOn;
            }
            set
            {
                if ((_positionTwoImageIndicatorOn == null && value != null)
                    || (_positionTwoImageIndicatorOn != null && !_positionTwoImageIndicatorOn.Equals(value)))
                {
                    string oldValue = _positionTwoImageIndicatorOn;
                    _positionTwoImageIndicatorOn = value;
                    OnPropertyChanged("PositionTwoIndicatorOnImage", oldValue, value, true);
                    Refresh();
                }
            }
        }

        #endregion

        #region HeliosControl Implementation

        public override void Reset()
        {
            base.Reset();

            BeginTriggerBypass(true);
            SwitchPosition = DefaultPosition;
            EndTriggerBypass(true);
        }

        protected override void ThrowSwitch(ToggleSwitchBase.SwitchAction action)
        {
            if (action == SwitchAction.Increment)
            {
                switch (SwitchPosition)
                {
                    case ThreeWayDispTogglePosition.One:
                        SwitchPosition = ThreeWayDispTogglePosition.Two;
                        break;
                    case ThreeWayDispTogglePosition.Two:
                        SwitchPosition = ThreeWayDispTogglePosition.Three;
                        break;
                }                
            }
            else if (action == SwitchAction.Decrement)
            {
                switch (SwitchPosition)
                {
                    case ThreeWayDispTogglePosition.Two:
                        SwitchPosition = ThreeWayDispTogglePosition.One;
                        break;
                    case ThreeWayDispTogglePosition.Three:
                        SwitchPosition = ThreeWayDispTogglePosition.Two;
                        break;
                }
            }
        }

        public override void WriteXml(XmlWriter writer)
        {
            base.WriteXml(writer);
            writer.WriteElementString("SwitchType", SwitchType.ToString());
            writer.WriteElementString("Orientation", Orientation.ToString());
            writer.WriteElementString("ClickType", ClickType.ToString());
            writer.WriteElementString("PositionOneImage", PositionOneImage);
            writer.WriteElementString("PositionTwoImage", PositionTwoImage);
            writer.WriteElementString("PositionThreeImage", PositionThreeImage);
            if (HasIndicator)
            {
                writer.WriteStartElement("Indicator");
                writer.WriteElementString("PositionOneImage", PositionOneIndicatorOnImage);
                writer.WriteElementString("PositionTwoImage", PositionTwoIndicatorOnImage);
                writer.WriteElementString("PositionThreeImage", PositionThreeIndicatorOnImage);
                writer.WriteEndElement();
            }
            writer.WriteElementString("DefaultPosition", DefaultPosition.ToString());
        }

        public override void ReadXml(XmlReader reader)
        {
            base.ReadXml(reader);
            SwitchType = (ThreeWayDispToggleType)Enum.Parse(typeof(ThreeWayDispToggleType), reader.ReadElementString("SwitchType"));
            Orientation = (ToggleSwitchOrientation)Enum.Parse(typeof(ToggleSwitchOrientation), reader.ReadElementString("Orientation"));
            if (reader.Name.Equals("ClickType"))
            {
                ClickType = (LinearClickType)Enum.Parse(typeof(LinearClickType), reader.ReadElementString("ClickType"));
            }
            else
            {
                ClickType = LinearClickType.Swipe;
            }
            PositionOneImage = reader.ReadElementString("PositionOneImage");
            PositionTwoImage = reader.ReadElementString("PositionTwoImage");
            PositionThreeImage = reader.ReadElementString("PositionThreeImage");
            if (reader.Name == "Indicator")
            {
                HasIndicator = true;
                reader.ReadStartElement("Indicator");
                PositionOneIndicatorOnImage = reader.ReadElementString("PositionOneImage");
                PositionTwoIndicatorOnImage = reader.ReadElementString("PositionTwoImage");
                PositionThreeIndicatorOnImage = reader.ReadElementString("PositionThreeImage");
                reader.ReadEndElement();
            }
            else
            {
                HasIndicator = false;
            }
            if (reader.Name == "DefaultPosition")
            {
                DefaultPosition = (ThreeWayDispTogglePosition)Enum.Parse(typeof(ThreeWayDispTogglePosition),
                    reader.ReadElementString("DefaultPosition"));
                BeginTriggerBypass(true);
                SwitchPosition = DefaultPosition;
                EndTriggerBypass(true);
            }
        }

        #endregion

        #region Actions

        public override void MouseDown(Point location)
        {
            // No Click Action(Display only)
        }

        public override void MouseDrag(Point location)
        {
            // No Drag Action(Display only) 
        }

        public override void MouseUp(Point location)
        {
            // No MouseUp Action(Display only) 
        }
        void SetPositionAction_Execute(object action, HeliosActionEventArgs e)
        {
            try
            {
                BeginTriggerBypass(e.BypassCascadingTriggers);
                int newPosition = 0;
                if (int.TryParse(e.Value.StringValue, out newPosition))
                {
                    if (newPosition > 0 && newPosition <= 3)
                    {
                        SwitchPosition = (ThreeWayDispTogglePosition)newPosition;
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
