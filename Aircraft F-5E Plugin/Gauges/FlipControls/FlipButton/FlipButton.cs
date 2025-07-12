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

namespace GadrocsWorkshop.Helios.Controls.F5E
{
    using ComponentModel;
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using System.Windows.Media;
    using System.Xml;
    using Point = System.Windows.Point;
    using Size = System.Windows.Size;

    [HeliosControl("Helios.F5E.FlipButton", "FlipButton", "F-5E", typeof(FlipButtonRenderer))]
    public class FlipButton : FlipAnimationBase
    {
        private static readonly NLog.Logger Logger = NLog.LogManager.GetCurrentClassLogger();


        private bool _pushed;
        private bool _closed;

        private HeliosAction _pushAction;
        private HeliosAction _releaseAction;
        
        private HeliosTrigger _openTrigger;
        private HeliosTrigger _closedTrigger;
        private HeliosTrigger _pushedTrigger;
        private HeliosTrigger _releasedTrigger;
        private DateTimeOffset touchedPassedTime;

        private PushButtonType _buttonType;
        private bool _isPositionLock;   // for move position on profile
        private Point _originPoint;

        public FlipButton(string name, Point posn, Size size,
            string animationPattern,
            int flipDisplayPatternNumber, List<Tuple<int, string, int, double , double, double>> flipButtonPostions)
            : base(name, new Size(size.Width, size.Height), new FlipAnimationStepCollection())
        {
            Left = posn.X;
            Top = posn.Y;
            AnimationFrameImageNamePattern = animationPattern ?? "{F-5E}/Images/LeftSidePanel/Throttle/CMDSButton/CMDSButton_01_01.png";
            var positionFileName = Path.GetFileName(AnimationFrameImageNamePattern);
            PatternNumber = (FlipDisplayPatternType)flipDisplayPatternNumber;
            flipButtonPostions.ForEach(x =>
            {
                Positions.Add(new FlipButtonPosition(this, x.Item1, x.Item2, x.Item3, x.Item4, x.Item5, x.Item6));
            });

            // init Triggers and Actions
            InitTriggersAndActions();

            CurrentPatternNumber = 1;   // released
            IsRenderReady = true;
        }


        public FlipButton()
            : base("FlipButton", new Size(47, 64), new FlipAnimationStepCollection())
        {

            AnimationFrameImageNamePattern =
                "{F-5E}/Images/LeftSidePanel/Throttle/MicButton/MicButton_01_01.png";

            var positionFileName = Path.GetFileName(AnimationFrameImageNamePattern);
            PatternNumber = (FlipDisplayPatternType)2;
            Positions.Add(new FlipButtonPosition(this, 0, "0", 1,  0, 0,  190));
            Positions.Add(new FlipButtonPosition(this, 1, "1", 2, 10, 10, 190));
            Positions.Add(new FlipButtonPosition(this, 2, "2", 3, 10, 10, 190));
            Positions.Add(new FlipButtonPosition(this, 3, "3", 4, 10, 10, 190));
            Positions.Add(new FlipButtonPosition(this, 4, "4", 5, 10, 10, 190));

            InitTriggersAndActions();

            CurrentPatternNumber = 1;   // released
            IsRenderReady = true;
        }

        private void InitTriggersAndActions()
        {
            _pushedTrigger = new HeliosTrigger(this, "", "", "pushed", "Fired when this button is pushed.", "Always returns true.", BindingValueUnits.Boolean);
            _releasedTrigger = new HeliosTrigger(this, "", "", "released", "Fired when this button is released.", "Always returns false.", BindingValueUnits.Boolean);
            _closedTrigger = new HeliosTrigger(this, "", "", "closed", "Fired when this button is in the closed state.", "Always returns true.", BindingValueUnits.Boolean);
            _openTrigger = new HeliosTrigger(this, "", "", "open", "Fired when this button is in the open state.", "Always returns false.", BindingValueUnits.Boolean);
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
        }

        #region Properties

        public override int CurrentPatternNumber
        {
            get => _currentPatternNumber;
            set
            {
                if (value >= 0 && value <= _patternNumber)
                {
                    var oldValue = _currentPatternNumber;
                    _currentPatternNumber = value;
                    OnPropertyChanged("CurrentPatternNumber", oldValue, value, true);
                    OnDisplayUpdate();
                }
            }
        }

        public Point OriginPoint
        {
            get => _originPoint;
            set
            {
                Point oldValue = _originPoint;
                _originPoint = value;
                OnPropertyChanged("OriginPoint", oldValue, value, true);
            }
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

                    CurrentPatternNumber = _pushed ? 2 : 1;
                    OnDisplayUpdate();
                }
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
                            CurrentPatternNumber = 2;
                        }
                        else
                        {
                            _openTrigger.FireTrigger(new BindingValue(true));
                            CurrentPatternNumber = 1;
                        }
                    }
                    OnPropertyChanged("IsClosed", oldValue, value, false);
                }
            }
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

        public bool IsPositionLock
        {
            get => _isPositionLock;
            set
            {
                if (_isPositionLock == value)
                {
                    return;
                }
                bool oldValue = _isPositionLock;
                _isPositionLock = value;
                OnPropertyChanged("IsPositionLock", oldValue, value, true);
            }
        }

        #endregion

        #region Actions



        void ChangePatternValue_Execute(object action, HeliosActionEventArgs e)
        {
            BeginTriggerBypass(e.BypassCascadingTriggers);

            if (Int32.TryParse(e.Value.StringValue, out int pattern))
            {
                CurrentPatternNumber = pattern;
            }
            EndTriggerBypass(e.BypassCascadingTriggers);
        }

        void PushedValue_Execute(object action, HeliosActionEventArgs e)
        {
            BeginTriggerBypass(e.BypassCascadingTriggers);

            Pushed = e.Value.BoolValue;
            IsClosed = Pushed;
            
            EndTriggerBypass(e.BypassCascadingTriggers);
        }

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


        public override void MouseUp(Point location)
        {
            if (Pushed == true &&  _buttonType == PushButtonType.Momentary)
            {
                _releasedTrigger.FireTrigger(new BindingValue(true));
                CurrentPatternNumber = 1;
            }
            if (ButtonType == PushButtonType.Toggle && IsClosed)
            {
                CurrentPatternNumber = 1;
            }
        }

        public override void MouseDrag(Point location)
        {
            //
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
        }

        #endregion



        public override void WriteXml(XmlWriter writer)
        {
            base.WriteXml(writer);
            writer.WriteElementString("AnimationFrameImageNamePattern", AnimationFrameImageNamePattern);
            writer.WriteElementString("Type", ButtonType.ToString());
            writer.WriteStartElement("Positions");
            foreach (FlipButtonPosition position in Positions.Cast<FlipButtonPosition>())
            {
                writer.WriteStartElement("Position");
                writer.WriteAttributeString("Name", position.Index.ToString());
                writer.WriteAttributeString("Frame", position.Frame.ToString(CultureInfo.InvariantCulture));
                writer.WriteAttributeString("Dx", position.Dx.ToString(CultureInfo.InvariantCulture));
                writer.WriteAttributeString("Dy", position.Dy.ToString(CultureInfo.InvariantCulture));
                writer.WriteAttributeString("DragAngle", position.DragAngle.ToString(CultureInfo.InvariantCulture));
                writer.WriteEndElement();
            }
            writer.WriteEndElement();
            writer.WriteElementString("PatternNumber", (Convert.ToInt32(PatternNumber)).ToString(CultureInfo.InvariantCulture));

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
                    Int32.TryParse(reader.GetAttribute("Frame"), out int frame);
                    Double.TryParse(reader.GetAttribute("Dx"), out double dx);
                    Double.TryParse(reader.GetAttribute("Dy"), out double dy);
                    Double.TryParse(reader.GetAttribute("DragAngle"), out double dragAngle);
                    Positions.Add(new FlipButtonPosition(
                        this,
                        i++,
                        name.ToString(),
                        frame,
                        dx, dy,
                        dragAngle));

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
            if (reader.Name.Equals("PatternNumber"))
            {
                var readPtnNum = int.Parse(reader.ReadElementString("PatternNumber"), CultureInfo.InvariantCulture);
                PatternNumber = (FlipDisplayPatternType)readPtnNum;
            }
            CurrentPosition = DefaultPosition;
            _originPoint = new Point(this.Left, this.Top);
            IsRenderReady = true;
        }


        public override void ReadOptionalXml(XmlReader reader)
        {
            // NOTHING
        }

        public override void WriteOptionalXml(XmlWriter writer)
        {
            // NOTHING
        }

        public override bool HitTest(Point location)
        {
            return true;
        }

        #region Overrides of HeliosVisual

        public override void ScaleChildren(double scaleX, double scaleY)
        {
            // NOTHING
        }

        #endregion
    }
}