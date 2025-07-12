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
    using System.Windows;
    using System.Windows.Media;
    using System.Xml;

    [HeliosControl("Helios.Base.FloatPopLine", "FloatPopLine", "F-5E", typeof(FloatPopLineRenderer))]
    public class FloatPopLine : HeliosVisual
    {
        private static readonly NLog.Logger Logger = NLog.LogManager.GetCurrentClassLogger();

        private readonly HeliosValue _positionValue;


        private int _currentPosition;
        private int _defaultPosition;

        private Color _lineColor = Colors.OrangeRed;
        private double _lineThickness = 2d;

        private double _firstX = 50;
        private double _firstY = 5;

        private double _secondX = 15;
        private double _secondY = 5;

        private double _thirdX = 10;
        private double _thirdY = 10;

        private double _circleRadius = 15;

        // array ordered by position angles in degrees, so we can dereference to their position index
        private PositionIndexEntry[] _positionIndex;

        public FloatPopLine() : base("FloatPopLine", new Size(100, 300))
        {
            _positionValue = new HeliosValue(this, new BindingValue(1), "",
                "position", "Current position of the PopLine.", "", BindingValueUnits.Numeric);
            _positionValue.Execute += SetPositionAction_Execute;
            Values.Add(_positionValue);
            Actions.Add(_positionValue);
            Triggers.Add(_positionValue);

            Positions.Add(new FloatPopLinePosition(this, 1, 10, 50, 10, 10, false, 90));
            Positions.Add(new FloatPopLinePosition(this, 2, 10, 50, 10, 10, true, 90));
            Positions.Add(new FloatPopLinePosition(this, 3, 10, 50, 10, 10, true, 90));
            Positions.Add(new FloatPopLinePosition(this, 4, 10, 50, 10, 10, true, 90));
            Positions.Add(new FloatPopLinePosition(this, 5, 10, 50, 10, 10, true, 90));
            Positions.Add(new FloatPopLinePosition(this, 6, 10, 50, 10, 10, true, 90));
            Positions.Add(new FloatPopLinePosition(this, 7, 10, 50, 10, 10, true, 90));
            _currentPosition = 1;
            _defaultPosition = 1;
        }



        #region Properties

        public FloatPopLinePositionCollection Positions { get; }
            = new FloatPopLinePositionCollection();


        public int CurrentPosition
        {
            get => _currentPosition;
            set
            {
                if (_currentPosition.Equals(value) || value < 0 || value > Positions.Count)
                {
                    return;
                }

                int oldValue = _currentPosition;

                _currentPosition = value;

                OnPropertyChanged("CurrentPosition", oldValue, value, false);
                OnDisplayUpdate();
            }
        }

        public double FirstX
        {
            get => _firstX;
            set
            {
                if (_firstX.Equals(value))
                {
                    return;
                }

                double oldValue = _firstX;
                _firstX = value;
                OnPropertyChanged("FirstX", oldValue, value, true);
                Refresh();
            }
        }

        public double FirstY
        {
            get => _firstY;
            set
            {
                if (_firstY.Equals(value))
                {
                    return;
                }

                double oldValue = _firstY;
                _firstY = value;
                OnPropertyChanged("FirstY", oldValue, value, true);
                Refresh();
            }
        }


        public double SecondX
        {
            get => _secondX;
            set
            {
                if (_secondX.Equals(value))
                {
                    return;
                }

                double oldValue = _secondX;
                _secondX = value;
                OnPropertyChanged("SecondX", oldValue, value, true);
                Refresh();
            }
        }

        public double SecondY
        {
            get => _secondY;
            set
            {
                if (_secondY.Equals(value))
                {
                    return;
                }

                double oldValue = _secondY;
                _secondY = value;
                OnPropertyChanged("SecondY", oldValue, value, true);
                Refresh();
            }
        }

        public double ThirdX
        {
            get => _thirdX;
            set
            {
                if (_secondX.Equals(value))
                {
                    return;
                }

                double oldValue = _thirdX;
                _thirdX = value;
                OnPropertyChanged("ThirdX", oldValue, value, true);
                Refresh();
            }
        }

        public double ThirdY
        {
            get => _thirdY;
            set
            {
                if (_thirdY.Equals(value))
                {
                    return;
                }

                double oldValue = _thirdY;
                _thirdY = value;
                OnPropertyChanged("ThirdY", oldValue, value, true);
                Refresh();
            }
        }

        public double Radius
        {
            get => _circleRadius;
            set
            {
                if (_circleRadius.Equals(value))
                {
                    return;
                }

                double oldValue = _circleRadius;
                _circleRadius = value;
                OnPropertyChanged("Radius", oldValue, value, true);
                Refresh();
            }
        }

        public double LineThickness
        {
            get => _lineThickness;
            set
            {
                if (_lineThickness.Equals(value))
                {
                    return;
                }

                double oldValue = _lineThickness;
                _lineThickness = value;
                OnPropertyChanged("LineThickness", oldValue, value, true);
                Refresh();
            }
        }

        public Color LineColor
        {
            get => _lineColor;
            set
            {
                if (_lineColor.Equals(value))
                {
                    return;
                }

                Color oldValue = _lineColor;
                _lineColor = value;
                OnPropertyChanged("LineColor", oldValue, value, true);
                Refresh();
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

        #endregion





        public override void MouseDrag(Point location)
        {
            //
        }

        #endregion

        void Positions_CollectionChanged(object sender,
            NotifyCollectionChangedEventArgs e)
        {
            if (e.OldItems != null)
            {

                if (Positions.Count == 0)
                {
                    _currentPosition = 0;
                }
                else if (_currentPosition > Positions.Count)
                {
                    _currentPosition = Positions.Count;
                }
            }
        }

        private class PositionIndexEntry
        {
            public double Rotation { get; set; }
            public int Index { get; set; }
        }

        private class PositionSortComparer : IComparer<PositionIndexEntry>
        {
            public int Compare(PositionIndexEntry left,
                PositionIndexEntry right) => (int)(left.Rotation - right.Rotation);
        }

        private void UpdatePositionIndex()
        {
            _positionIndex = Positions.OrderBy(p => p.Index).Select(
                p => new PositionIndexEntry { Index = p.Index }).ToArray();
        }

        public override void MouseDown(Point location)
        {
            //
        }

        public override void MouseUp(Point location)
        {
            //
        }

        public override bool HitTest(Point location)
        {
            return false;
        }

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

            writer.WriteElementString("FirstX", FirstX.ToString(CultureInfo.InvariantCulture));
            writer.WriteElementString("FirstY", FirstY.ToString(CultureInfo.InvariantCulture));
            writer.WriteElementString("SecondX", SecondX.ToString(CultureInfo.InvariantCulture));
            writer.WriteElementString("SecondY", SecondY.ToString(CultureInfo.InvariantCulture));
            writer.WriteElementString("ThirdX", ThirdX.ToString(CultureInfo.InvariantCulture));
            writer.WriteElementString("ThirdY", ThirdY.ToString(CultureInfo.InvariantCulture));
            writer.WriteElementString("Radius", Radius.ToString(CultureInfo.InvariantCulture));

            writer.WriteStartElement("Positions");
            foreach (FloatPopLinePosition position in Positions)
            {
                writer.WriteStartElement("Position");
                writer.WriteAttributeString("X", Convert.ToString(position.X));
                writer.WriteAttributeString("Y", Convert.ToString(position.Y));
                writer.WriteAttributeString("MotionVertexX",
                    Convert.ToString(position.MotionVertexX));
                writer.WriteAttributeString("MotionVertexY",
                    Convert.ToString(position.MotionVertexY));
                writer.WriteAttributeString("UseMotionVertex",
                    Convert.ToString(position.UseMotionVertex));
                writer.WriteAttributeString("ConnectAngle", Convert.ToString(position.ConnectAngle));
                writer.WriteEndElement();
            }
            writer.WriteEndElement();
            writer.WriteElementString("DefaultPosition",
                DefaultPosition.ToString(CultureInfo.InvariantCulture));
            writer.WriteElementString("LineThickness",
                LineThickness.ToString(CultureInfo.InvariantCulture));
            writer.WriteElementString("LineColor", colorConverter.ConvertToInvariantString(LineColor));

        }

        public override void ReadXml(XmlReader reader)
        {
            TypeConverter colorConverter = TypeDescriptor.GetConverter(typeof(Color));

            base.ReadXml(reader);

            FirstX = int.Parse(reader.ReadElementString("FirstX"), CultureInfo.InvariantCulture);
            FirstY = int.Parse(reader.ReadElementString("FirstY"), CultureInfo.InvariantCulture);

            SecondX = int.Parse(reader.ReadElementString("SecondX"),
                CultureInfo.InvariantCulture);
            SecondY = int.Parse(reader.ReadElementString("SecondY"),
                CultureInfo.InvariantCulture);

            ThirdX = int.Parse(reader.ReadElementString("ThirdX"), CultureInfo.InvariantCulture);
            ThirdY = int.Parse(reader.ReadElementString("ThirdY"), CultureInfo.InvariantCulture);

            Radius = int.Parse(reader.ReadElementString("Radius"), CultureInfo.InvariantCulture);
            
            if (!reader.IsEmptyElement)
            {
                Positions.Clear();
                reader.ReadStartElement("Positions");
                int i = 0;
                while (reader.NodeType != XmlNodeType.EndElement)
                {
                    var x = Convert.ToInt32(reader.GetAttribute("X"));
                    var y = Convert.ToInt32(reader.GetAttribute("Y"));
                    var motionVertexX = Convert.ToInt32(reader.GetAttribute("MotionVertexX"));
                    var motionVertexY = Convert.ToInt32(reader.GetAttribute("MotionVertexX"));
                    var useMotionVertex = Convert.ToBoolean(reader.GetAttribute("UseMotionVertex"));
                    var connectAngle = Convert.ToInt32(reader.GetAttribute("ConnectAngle"));

                    Positions.Add(new FloatPopLinePosition(this, i++, x, y,
                        motionVertexX, motionVertexY, useMotionVertex, connectAngle));
                    reader.Read();
                }
                reader.ReadEndElement();
            }
            else
            {
                reader.Read();
            }
            DefaultPosition = int.Parse(reader.ReadElementString("DefaultPosition"), CultureInfo.InvariantCulture);
            LineThickness = int.Parse(reader.ReadElementString("LineThickness"), CultureInfo.InvariantCulture);
            LineColor = (Color)colorConverter.ConvertFromInvariantString(reader.ReadElementString("LineColor"));

            BeginTriggerBypass(true);
            CurrentPosition = DefaultPosition;
            EndTriggerBypass(true);
        }
    }
}