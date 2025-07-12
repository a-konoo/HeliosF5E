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
    using System.Globalization;
    using System.IO;
    using System.Xml;
    using Point = System.Windows.Point;
    using Size = System.Windows.Size;

    [HeliosControl("Helios.Base.Flip3WayToggle", "Flip3WayToggle", "F-5E", typeof(Flip3WayToggleRenderer))]
    public class Flip3WayToggle : FlipAnimationBase
    {
        private static readonly NLog.Logger Logger = NLog.LogManager.GetCurrentClassLogger();

        private HeliosTrigger _statusTrigger;
        private HeliosValue _patternValue;
        private HeliosTrigger _toggledTrigger;

        private Point _draggingLast;
        private readonly int PULL_DIST = 30;
        private Point _originPoint;
        private int _dragDistance;
        private bool _isPositionLock;
        public Flip3WayToggle()
            : base("Flip3WayToggle", new Size(77, 64), new FlipAnimationStepCollection())
        {

            AnimationFrameImageNamePattern = "{Helios}/Images/F-5E/ThrottleParts/AirBrake/Brakes_01_01.png";
            var positionFileName = Path.GetFileName(AnimationFrameImageNamePattern);
            PatternNumber = (FlipDisplayPatternType)3;
            Positions.Add(new Flip3WayTogglePosition(this, 0, "0", 1,  0, 0,  75.0d));
            Positions.Add(new Flip3WayTogglePosition(this, 1, "1", 2, 10, 10, 74.0d));
            Positions.Add(new Flip3WayTogglePosition(this, 2, "2", 3, 10, 10, 73.0d));
            Positions.Add(new Flip3WayTogglePosition(this, 3, "3", 4, 10, 10, 72.0d));
            Positions.Add(new Flip3WayTogglePosition(this, 4, "4", 5, 10, 10, 72.0d));


            _statusTrigger = new HeliosTrigger(this, "", "", "states changed", "Fired when this toggle status changed",
                "return current status value.", BindingValueUnits.Numeric);


            Triggers.Add(_statusTrigger);

            _patternValue = new HeliosValue(this, new BindingValue(0), "", "toggle pattern number",
                "3way toggle value(1,2,3) and pattern number(toggle display pattern)",
                "(1,2,3)", BindingValueUnits.Numeric);
            _patternValue.Execute += new HeliosActionHandler(ChangePatternValue_Execute);

            Values.Add(_patternValue);
            Actions.Add(_patternValue);

            _toggledTrigger = new HeliosTrigger(this, "", "", "toggled value",
                "trigger will fire, when toglle changed.",
                "(1,2,3).", BindingValueUnits.Numeric);
            Triggers.Add(_toggledTrigger);

            _dragDistance = PULL_DIST;
            CurrentPatternNumber = 1;
            IsRenderReady = true;
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

        public int DragDistance
        {
            get => _dragDistance;
            set
            {
                if (value > 0 && value < 400)
                {
                    int oldValue = _dragDistance;
                    _dragDistance = value;
                    OnPropertyChanged("DragDistance", oldValue, value, true);
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
                        return;
                    }
                }
            }
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
        }

        public override void MouseDrag(Point location)
        {
            if (_draggingLast == null)
            {
                _draggingLast = location;
            }
            var diff = _draggingLast - location;


            if (Math.Sqrt(diff.X * diff.X + diff.Y * diff.Y) > PULL_DIST)
            {
                var position = Positions[CurrentPosition] as Flip3WayTogglePosition;
                var dragAngle = position.DragAngle;
                var product = diff.X * Math.Cos(dragAngle) + diff.Y * Math.Sin(dragAngle);
                if (product > 0 && CurrentPatternNumber < 3)
                {
                    CurrentPatternNumber += 1;
                    _toggledTrigger.FireTrigger(new BindingValue(CurrentPatternNumber));
                }
                else if (product < 0 && CurrentPatternNumber > 1)
                {
                    CurrentPatternNumber -= 1;
                    _toggledTrigger.FireTrigger(new BindingValue(CurrentPatternNumber));
                }
                // This is an implementation to prevent the value from
                // changing continuously with a single drag.
                _draggingLast = location;
                return;
            }

            base.MouseDrag(location);
        }



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


        #endregion



        public override void WriteXml(XmlWriter writer)
        {
            base.WriteXml(writer);
            writer.WriteElementString("AnimationFrameImageNamePattern", AnimationFrameImageNamePattern);
            
            writer.WriteElementString("DragDistance", Convert.ToString(DragDistance));
            writer.WriteStartElement("Positions");
            foreach (Flip3WayTogglePosition position in Positions)
            {
                writer.WriteStartElement("Position");
                writer.WriteAttributeString("Name", position.Index.ToString());
                writer.WriteAttributeString("Frame", position.Frame.ToString(CultureInfo.InvariantCulture));
                writer.WriteAttributeString("SendValue", position.SendValue.ToString(CultureInfo.InvariantCulture));

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

            Int32.TryParse(reader.ReadElementString("DragDistance"), out int dd);
            DragDistance = dd;

            if (!reader.IsEmptyElement)
            {
                Positions.Clear();
                reader.ReadStartElement("Positions");
                int i = 0;
                while (reader.NodeType != XmlNodeType.EndElement)
                {
                    Int32.TryParse(reader.GetAttribute("Name"), out int name);
                    Int32.TryParse(reader.GetAttribute("Frame"), out int frame);
                    Int32.TryParse(reader.GetAttribute("SendValue"), out int sendValue);
                    Double.TryParse(reader.GetAttribute("Dx"), out double dx);
                    Double.TryParse(reader.GetAttribute("Dy"), out double dy);
                    Double.TryParse(reader.GetAttribute("DragAngle"), out double dragAngle);
                    Positions.Add(new Flip3WayTogglePosition(
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
            if (reader.Name.Equals("PatternNumber"))
            {
                var readPtnNum = int.Parse(reader.ReadElementString("PatternNumber"), CultureInfo.InvariantCulture);
                PatternNumber = (FlipDisplayPatternType)readPtnNum;
            }
            ReadOptionalXml(reader);
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

        #region Overrides of HeliosVisual

        public override void ScaleChildren(double scaleX, double scaleY)
        {
            // NOTHING
        }

        #endregion
    }
}