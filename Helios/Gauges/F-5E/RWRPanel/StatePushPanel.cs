//  Copyright 2014 Craig Courtney
//  Copyright 2022 Helios Contributors
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
    using System.IO;
    using System.Text.RegularExpressions;
    using System.Windows.Media;
    using System.Xml;
    using Point = System.Windows.Point;
    using Size = System.Windows.Size;

    [HeliosControl("Helios.Base.StatePushPanel", "StatePushPanel", "F-5E", typeof(StatePushPanelRenderer))]
    public class StatePushPanel : HeliosVisual
    {
        private static readonly NLog.Logger Logger = NLog.LogManager.GetCurrentClassLogger();

        private static Regex regex = new Regex(Regex.Escape("_"));

        private bool _pushed;
        private bool _closed;

        private HeliosAction _pushAction;
        private HeliosAction _releaseAction;
        private HeliosValue _statusValue;
        private HeliosValue _powerState;

        private HeliosTrigger _openTrigger;
        private HeliosTrigger _closedTrigger;
        private HeliosTrigger _pushedTrigger;
        private HeliosTrigger _releasedTrigger;

        private DateTimeOffset touchedPassedTime;
        private PushButtonType _buttonType;
        private string _errorMessage;
        private int _currentState;
        private bool _isPowerOn;
        private bool _isRenderReady = false;
        private ImageSource[] _images = new ImageSource[] { };
        private String _animationFrameImageNamePattern;
        public StatePushPanelStepCollection Positions { get; }

        public StatePushPanel() : base("StatePushPanel", new Size(102.5, 101))
        {

            AnimationFrameImageNamePattern =
                "{Helios}/Images/F-5E/RWRPanel/HANDOFF_01.png";

            var positionFileName = Path.GetFileName(AnimationFrameImageNamePattern);
            Positions = new StatePushPanelStepCollection();
            Positions.Add(new StatePushPanelPosition(this, 0, "0"));
            Positions.Add(new StatePushPanelPosition(this, 1, "1"));
            Positions.Add(new StatePushPanelPosition(this, 2, "2"));

            _pushedTrigger = new HeliosTrigger(this, "", "", "pushed",
                "Fired when this button is pushed.", "Always returns true.", BindingValueUnits.Boolean);
            _releasedTrigger = new HeliosTrigger(this, "", "", "released",
                "Fired when this button is released.", "Always returns false.", BindingValueUnits.Boolean);
            _closedTrigger = new HeliosTrigger(this, "", "", "closed",
                "Fired when this button is in the closed state.", "Always returns true.", BindingValueUnits.Boolean);
            _openTrigger = new HeliosTrigger(this, "", "", "open",
                "Fired when this button is in the open state.", "Always returns false.", BindingValueUnits.Boolean);

            _statusValue = new HeliosValue(this, new BindingValue(0), "", "position",
                "Status Value(1:power-off/2:power-on/3:pushed).", "", BindingValueUnits.Numeric);

            _statusValue.Execute += SetStateAction_Execute;

            Values.Add(_statusValue);
            Actions.Add(_statusValue);
            Triggers.Add(_statusValue);

            _powerState = new HeliosValue(this, new BindingValue(0), "", "power",
                "Power Value(true:power-on,false:power-off).", "", BindingValueUnits.Boolean);

            _powerState.Execute += SetPower_Execute;

            Values.Add(_powerState);
            Actions.Add(_powerState);
            Triggers.Add(_powerState);

            Triggers.Add(_pushedTrigger);
            Triggers.Add(_releasedTrigger);
            Triggers.Add(_closedTrigger);
            Triggers.Add(_openTrigger);

            _pushAction = new HeliosAction(this, "", "", "push", "Simulate physically pushing this button.");
            _pushAction.Execute += new HeliosActionHandler(Push_ExecuteAction);
            _releaseAction = new HeliosAction(this, "", "", "release", "Simulate physically removing pressure from this button.");
            _releaseAction.Execute += new HeliosActionHandler(Release_ExecuteAction);
            Actions.Add(_pushAction);
            Actions.Add(_releaseAction);
            IsRenderReady = true;
        }

        #region Properties

        public String AnimationFrameImageNamePattern
        {
            get => _animationFrameImageNamePattern;
            set
            {
                if ((_animationFrameImageNamePattern == null && value != null)
                    || (_animationFrameImageNamePattern != null && !_animationFrameImageNamePattern.Equals(value)))
                {
                    String oldValue = _animationFrameImageNamePattern;
                    _animationFrameImageNamePattern = value;
                    OnPropertyChanged("AnimationFrameImageNamePattern", oldValue, value, true);
                    OnDisplayUpdate();
                }
            }
        }

        public int MaxPositionCount => Positions.Count;

        public StatePushPanelPosition LastPositionElement =>
            Positions.Count > 0 ? Positions[MaxPositionCount - 1] : null;


        /// <summary>
        /// State change
        /// </summary>
        /// <param name="action"></param>
        /// <param name="e"></param>
        public void SetStateAction_Execute(object action, HeliosActionEventArgs e)
        {
            BeginTriggerBypass(e.BypassCascadingTriggers);
            if (!int.TryParse(e.Value.StringValue, out int _state))
            {
                return;
            }
            if (_state < Positions.Count  && 0 <= _state)
            {
                var oldValue = _currentState;
                _currentState = _state;
                OnPropertyChanged("CurrentState", oldValue, _currentState, false);
            }
            EndTriggerBypass(e.BypassCascadingTriggers);
        }

        /// <summary>
        /// State change
        /// </summary>
        /// <param name="action"></param>
        /// <param name="e"></param>
        public void SetPower_Execute(object action, HeliosActionEventArgs e)
        {
            BeginTriggerBypass(e.BypassCascadingTriggers);
            if (e.Value.BoolValue && IsPowerOn)
            {
                IsPowerOn = e.Value.BoolValue;
            }
            EndTriggerBypass(e.BypassCascadingTriggers);
        }

        public bool Pushed
        {
            get
            {
                return _pushed;
            }
            set
            {
                if (!_pushed.Equals(value))
                {
                    _pushed = value;
                    OnPropertyChanged("Pushed", !value, value, false);
                    OnDisplayUpdate();
                }
            }
        }

        public bool IsPowerOn 
        {
            get
            {
                return _isPowerOn;
            }
            set
            {
                if (!_isPowerOn.Equals(value))
                {
                    _isPowerOn = value;
                    CurrentState = _isPowerOn ? 1 : 0;
                    OnPropertyChanged("Pushed", !value, value, false);
                    OnDisplayUpdate();
                }
            }
        }

        public int CurrentState
        {
            get { return _currentState; }
            set
            {
                if (!_currentState.Equals(value))
                {
                    int oldValue = _currentState;

                    _currentState = value;
                    OnPropertyChanged("CurrentState", oldValue, value, false);
                }
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


        public bool IsClosed
        {
            get
            {
                return _closed;
            }
            set
            {
                if (!_closed.Equals(value))
                {
                    bool oldValue = _closed;

                    _closed = value;

                    if (!BypassTriggers)
                    {
                        if (_closed)
                        {
                            _closedTrigger.FireTrigger(new BindingValue(true));
                        }
                        else
                        {
                            _openTrigger.FireTrigger(new BindingValue(true));
                        }
                    }
                    OnPropertyChanged("IsClosed", oldValue, value, false);
                }
            }
        }

        /// <summary>
        /// Images on state
        /// </summary>
        public ImageSource[] Images
        {
            get => _images;
            set => _images = value;
        }
        
        public PushButtonType ButtonType
        {
            get
            {
                return _buttonType;
            }
            set
            {
                if (!_buttonType.Equals(value))
                {
                    PushButtonType oldValue = _buttonType;
                    _buttonType = value;
                    OnPropertyChanged("ButtonType", oldValue, value, true);
                }
            }
        }

        /// <summary>
        /// message of image load error when initialize control
        /// </summary>
        public String ErrorMessage
        {
            get => regex.Replace(_errorMessage ?? "", "__", 1);
            set
            {
                String oldValue = _errorMessage;
                _errorMessage = value;
                OnPropertyChanged("ErrorMessage", oldValue, value, true);
            }
        }
        #endregion

        #region Actions

        void Push_ExecuteAction(object action, HeliosActionEventArgs e)
        {
            BeginTriggerBypass(e.BypassCascadingTriggers);

            if (!BypassTriggers)
            {
                _pushedTrigger.FireTrigger(new BindingValue(true));
            }

            if (ButtonType == PushButtonType.Momentary)
            {
                Pushed = true;
                IsClosed = true;
            }
            else
            {
                Pushed = !Pushed;
                IsClosed = Pushed;
            }

            EndTriggerBypass(e.BypassCascadingTriggers);
        }

        void Release_ExecuteAction(object action, HeliosActionEventArgs e)
        {
            BeginTriggerBypass(e.BypassCascadingTriggers);

            if (!BypassTriggers)
            {
                _releasedTrigger.FireTrigger(new BindingValue(false));
            }

            if (ButtonType == PushButtonType.Momentary)
            {
                Pushed = false;
                IsClosed = false;
            }

            EndTriggerBypass(e.BypassCascadingTriggers);
        }

        protected override void OnPropertyChanged(PropertyNotificationEventArgs args)
        {
            OnDisplayUpdate();
            base.OnPropertyChanged(args);
        }

        public override void Reset()
        {
            base.Reset();

            BeginTriggerBypass(true);
            Pushed = false;
            IsClosed = false;
            EndTriggerBypass(true);
        }

        public override void MouseDown(Point location)
        {
            if (DateTimeOffset.Now - touchedPassedTime < TimeSpan.FromMilliseconds(500)) { return; }
            touchedPassedTime = DateTimeOffset.Now;
            if (!BypassTriggers)
            {
                _pushedTrigger.FireTrigger(new BindingValue(true));
            }
            switch (ButtonType)
            {
                case PushButtonType.Momentary:
                    Pushed = true;
                    IsClosed = true;
                    break;

                case PushButtonType.Toggle:
                    Pushed = !Pushed;
                    IsClosed = Pushed;
                    break;
            }

            return;
        }

        public override void MouseUp(Point location)
        {
            if (Pushed == true &&  _buttonType == PushButtonType.Momentary)
            {
                _releasedTrigger.FireTrigger(new BindingValue(true));
            }
        }

        public override void MouseDrag(Point location)
        {
            // down
        }

        #endregion



        public override void WriteXml(XmlWriter writer)
        {
            base.WriteXml(writer);
            writer.WriteElementString("AnimationFrameImageNamePattern", AnimationFrameImageNamePattern);
            writer.WriteElementString("Type", ButtonType.ToString());
            writer.WriteStartElement("Positions");
            foreach (StatePushPanelPosition position in Positions)
            {
                writer.WriteStartElement("Position");
                writer.WriteAttributeString("Name", position.Index.ToString());
                writer.WriteEndElement();
            }
            writer.WriteEndElement();
            WriteOptionalXml(writer);
        }

        public override void ReadXml(XmlReader reader)
        {
            base.ReadXml(reader);
            AnimationFrameImageNamePattern = reader.ReadElementString("AnimationFrameImageNamePattern");
            ButtonType = (PushButtonType)Enum.Parse(typeof(PushButtonType), reader.ReadElementString("Type"));

            if (!reader.IsEmptyElement)
            {
                Positions.Clear();
                reader.ReadStartElement("Positions");
                int i = 0;
                while (reader.NodeType != XmlNodeType.EndElement)
                {
                    Int32.TryParse(reader.GetAttribute("Name"), out int name);
                    Positions.Add(new StatePushPanelPosition(
                        this,
                        i++,
                        name.ToString()));

                    reader.Read();
                }
                reader.ReadEndElement();
            }
            else
            {
                reader.Read();
                reader.ReadEndElement();
            }
            ReadOptionalXml(reader);
            IsRenderReady = true;
        }


        public void ReadOptionalXml(XmlReader reader)
        {
            // NOTHING
        }

        public void WriteOptionalXml(XmlWriter writer)
        {
            // NOTHING
        }

        #region Overrides of HeliosVisual

        public override void ScaleChildren(double scaleX, double scaleY)
        {
            // NOTHING
        }

        #endregion
    }
}