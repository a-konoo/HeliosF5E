//  Copyright 2014 Craig Courtney
//  Copyright 2021 Helios Contributors
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
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Globalization;
    using System.IO;
    using System.Text;
    using System.Windows;
    using System.Windows.Markup;
    using System.Windows.Media;
    using System.Xml;

    [HeliosControl("Drag.Panel", "Drag Panel", "F-5E", typeof(GradationDragPanelRenderer))]
    public class GradationDragPanel : HeliosVisualContainer
    {

        private HeliosTrigger _dragFwdTrigger;
        private HeliosTrigger _dragBackTrigger;
        private HeliosValue _positionValue;

        private Point _draggingLast;
        private bool isDragging = false;

        private bool _fillBackground = true;
        private string _backgroundImageFile = "";
        private ImageAlignment _backgroundAlignment = ImageAlignment.Stretched;
        private Color _backgroundColor = Color.FromArgb(255, 30, 30, 30);
        private int _dragAngle = 0;
        private int _dragDistance = 20;
        private int _currentPosition = 0;
        private double _opacity = 1d;
        private int _backImageSizeX = 800;
        private int _backImageSizeY = 300;
        private List<Geometry> _clips;

        public GradationDragPanel()
            : base("Panel", new Size(300,300))
        {
            _dragFwdTrigger = new HeliosTrigger(this, "", "", "dragging Fwd", "it fires ,when this panel dragged forward",
                "return current status value.", BindingValueUnits.Boolean);
            Triggers.Add(_dragFwdTrigger);

            _dragBackTrigger = new HeliosTrigger(this, "", "", "dragging Back", "it fires ,when this panel dragged backward",
                "return current status value.", BindingValueUnits.Boolean);
            Triggers.Add(_dragBackTrigger);

            _positionValue = new HeliosValue(this, new BindingValue(0), "", "position",
                "Current Position .", "", BindingValueUnits.Numeric);

            _positionValue.Execute += SetPositionAction_Execute;

            Values.Add(_positionValue);
            Actions.Add(_positionValue);
            Triggers.Add(_positionValue);

            GeometryCollections.Add(new GradationDragPanelClippingGeometry(this, 0, 150, 222, 1, 7, 0,
                new EllipseGeometry(new Point(150,222),7 ,7), ""));
            GeometryCollections.Add(new GradationDragPanelClippingGeometry(this, 1, 155, 207, 1, 8, 0,
                new EllipseGeometry(new Point(155, 207), 8, 8),""));
            GeometryCollections.Add(new GradationDragPanelClippingGeometry(this, 2, 165, 190, 1, 10, 0,
                new EllipseGeometry(new Point(165, 190), 10, 10), ""));
            GeometryCollections.Add(new GradationDragPanelClippingGeometry(this, 3, 180, 170, 1, 12, 0,
                new EllipseGeometry(new Point(180, 170), 12, 12), ""));
            GeometryCollections.Add(new GradationDragPanelClippingGeometry(this, 3, 205, 150, 1, 15, 0,
                new EllipseGeometry(new Point(205, 150), 15, 15), ""));
            GeometryCollections.Add(new GradationDragPanelClippingGeometry(this, 3, 240, 130, 1, 17, 0,
                new EllipseGeometry(new Point(240, 130), 17, 17), ""));
            GeometryCollections.Add(new GradationDragPanelClippingGeometry(this, 3, 280, 115, 1, 15, 0,
                new EllipseGeometry(new Point(280, 115), 20, 20), ""));

            PositionCollections.Add(new GradationDragPanelPosition(this, 0, -374, 0));
            PositionCollections.Add(new GradationDragPanelPosition(this, 1, -388, 0));
            PositionCollections.Add(new GradationDragPanelPosition(this, 2, -392, 0));
            PositionCollections.Add(new GradationDragPanelPosition(this, 3, -396, 0));
            PositionCollections.Add(new GradationDragPanelPosition(this, 4, -400, 0));
            PositionCollections.Add(new GradationDragPanelPosition(this, 5, -404, 0));
            PositionCollections.Add(new GradationDragPanelPosition(this, 6, -408, 0));
            PositionCollections.Add(new GradationDragPanelPosition(this, 7, -412, 0));
            PositionCollections.Add(new GradationDragPanelPosition(this, 8, -416, 0));
            PositionCollections.Add(new GradationDragPanelPosition(this, 9, -420, 0));
            PositionCollections.Add(new GradationDragPanelPosition(this, 10, -424, 0));
            PositionCollections.Add(new GradationDragPanelPosition(this, 11, -428, 0));
            PositionCollections.Add(new GradationDragPanelPosition(this, 12, -432, 0));
            PositionCollections.Add(new GradationDragPanelPosition(this, 13, -436, 0));
            PositionCollections.Add(new GradationDragPanelPosition(this, 14, -440, 0));
            PositionCollections.Add(new GradationDragPanelPosition(this, 15, -444, 0));
            PositionCollections.Add(new GradationDragPanelPosition(this, 16, -448, 0));
            PositionCollections.Add(new GradationDragPanelPosition(this, 17, -452, 0));
            PositionCollections.Add(new GradationDragPanelPosition(this, 18, -456, 0));
            PositionCollections.Add(new GradationDragPanelPosition(this, 19, -460, 0));
            PositionCollections.Add(new GradationDragPanelPosition(this, 20, -464, 0));
            PositionCollections.Add(new GradationDragPanelPosition(this, 21, -468, 0));
            PositionCollections.Add(new GradationDragPanelPosition(this, 22, -472, 0));
            PositionCollections.Add(new GradationDragPanelPosition(this, 23, -476, 0));
            PositionCollections.Add(new GradationDragPanelPosition(this, 24, -480, 0));
            PositionCollections.Add(new GradationDragPanelPosition(this, 25, -484, 0));
            PositionCollections.Add(new GradationDragPanelPosition(this, 26, -424, 0));
            PositionCollections.Add(new GradationDragPanelPosition(this, 27, -428, 0));
            PositionCollections.Add(new GradationDragPanelPosition(this, 28, -432, 0));
            PositionCollections.Add(new GradationDragPanelPosition(this, 29, -436, 0));
            PositionCollections.Add(new GradationDragPanelPosition(this, 30, -440, 0));
            PositionCollections.Add(new GradationDragPanelPosition(this, 31, -444, 0));
            PositionCollections.Add(new GradationDragPanelPosition(this, 32, -448, 0));
            PositionCollections.Add(new GradationDragPanelPosition(this, 33, -452, 0));

            BackgroundImage = "{Helios}/Images/F-5E/ThrottoleUI/UI_DRAG/UI_DRAG.png";
        }

        #region Properties

        public GradationDragPanelClippingGeometryCollection GeometryCollections { get; }
            = new GradationDragPanelClippingGeometryCollection();

        public GradationDragPanelPositionCollection PositionCollections { get; }
            = new GradationDragPanelPositionCollection();

        
        public int CurrentPosition
        {
            get => _currentPosition;
            set
            {
                if (_currentPosition.Equals(value) || value < 0 || value > PositionCollections.Count)
                {
                    return;
                }

                int oldValue = _currentPosition;

                _currentPosition = value;

                OnPropertyChanged("CurrentPosition", oldValue, value, false);
                OnDisplayUpdate();
            }
        }

        public int BackImageSizeX
        {
            get => _backImageSizeX;
            set
            {
                if (_backImageSizeX.Equals(value))
                {
                    return;
                }

                int oldValue = _backImageSizeX;
                _backImageSizeX = value;

                OnPropertyChanged("BackImageSizeX", oldValue, value, false);
                OnDisplayUpdate();
            }
        }

        public int BackImageSizeY
        {
            get => _backImageSizeY;
            set
            {
                if (_backImageSizeY.Equals(value))
                {
                    return;
                }

                int oldValue = _backImageSizeY;
                _backImageSizeY = value;

                OnPropertyChanged("BackImageSizeY", oldValue, value, false);
                OnDisplayUpdate();
            }
        }

        void SetPositionAction_Execute(object action, HeliosActionEventArgs e)
        {
            BeginTriggerBypass(e.BypassCascadingTriggers);
            if (int.TryParse(e.Value.StringValue, out int index))
            {

                if (index > 0 && index <= PositionCollections.Count)
                {
                    CurrentPosition = index;
                }

            }
            EndTriggerBypass(e.BypassCascadingTriggers);
        }

        public bool AllowInteractionFull { get; set; } = false;
        public bool AllowInteractionNone { get; set; } = true;
        public bool AllowInteractionLegacy { get; set; } = false;

        public string BackgroundImage
        {
            get
            {
                return _backgroundImageFile;
            }
            set
            {
                if ((_backgroundImageFile == null && value != null)
                    || (_backgroundImageFile != null && !_backgroundImageFile.Equals(value)))
                {
                    string oldValue = _backgroundImageFile;
                    _backgroundImageFile = value;
                    OnPropertyChanged("BackgroundImage", oldValue, value, true);
                    Refresh();
                }
            }
        }

        public bool FillBackground
        {
            get
            {
                return _fillBackground;
            }
            set
            {
                if (!_fillBackground.Equals(value))
                {
                    _fillBackground = value;
                    OnPropertyChanged("FillBackground", value, !value, true);
                    OnDisplayUpdate();
                }
            }
        }

        public int DragAngle
        {
            get
            {
                return _dragAngle;
            }
            set
            {
                if (!_dragAngle.Equals(value))
                {
                    int oldValue = _dragAngle;
                    _dragAngle = value;
                    OnPropertyChanged("DragAngle", _dragAngle, oldValue, true);
                    OnDisplayUpdate();
                }
            }
        }

        public int DragDistance
        {
            get
            {
                return _dragDistance;
            }
            set
            {
                if (!_dragDistance.Equals(value))
                {
                    int oldValue = _dragDistance;
                    _dragDistance = value;
                    OnPropertyChanged("DragDistance", _dragDistance, oldValue, true);
                    OnDisplayUpdate();
                }
            }
        }

        public List<Geometry> Clips
        {
            get
            {
                return _clips;
            }
            set
            {
                _clips = value;
                _clips?.ForEach(_clip =>
                {
                    _clip.Freeze();
                });                
            }
        }

        public ImageAlignment BackgroundAlignment
        {
            get
            {
                return _backgroundAlignment;
            }
            set
            {
                if (!_backgroundAlignment.Equals(value))
                {
                    ImageAlignment oldValue = _backgroundAlignment;
                    _backgroundAlignment = value;
                    OnPropertyChanged("BackgroundAlignment", oldValue, value, true);
                    Refresh();
                }
            }
        }

        public Color BackgroundColor
        {
            get
            {
                return _backgroundColor;
            }
            set
            {
                if (!_backgroundColor.Equals(value))
                {
                    Color oldValue = _backgroundColor;
                    _backgroundColor = value;
                    OnPropertyChanged("BackgroundColor", oldValue, value, true);
                    Refresh();
                }
            }
        }

        public double Opacity
        {
            get
            {
                return _opacity;
            }
            set
            {
                double oldValue = _opacity;
                _opacity = value;
                if (value != oldValue)
                {
                    OnPropertyChanged("Opacity", oldValue, value, true);
                    OnDisplayUpdate();
                }
            }
        }

        #endregion

        public override void WriteXml(XmlWriter writer)
        {
            base.WriteXml(writer);

            TypeConverter cc = TypeDescriptor.GetConverter(typeof(Color));

            writer.WriteStartElement("Background");
            if (FillBackground)
            {
                writer.WriteElementString("Color", cc.ConvertToInvariantString(BackgroundColor));
            }
            if (!string.IsNullOrWhiteSpace(BackgroundImage))
            {
                writer.WriteElementString("Image", BackgroundImage);
                writer.WriteElementString("ImageAlignment", BackgroundAlignment.ToString());
            }
            writer.WriteEndElement();

            writer.WriteElementString("BackImageSizeX", BackImageSizeX.ToString());
            writer.WriteElementString("BackImageSizeY", BackImageSizeY.ToString());

            writer.WriteStartElement("Interaction");
            writer.WriteElementString("AllowInteractionFull", AllowInteractionFull.ToString(CultureInfo.InvariantCulture));
            writer.WriteElementString("AllowInteractionNone", AllowInteractionNone.ToString(CultureInfo.InvariantCulture));
            writer.WriteElementString("AllowInteractionLegacy", AllowInteractionLegacy.ToString(CultureInfo.InvariantCulture));
            writer.WriteEndElement();

            writer.WriteStartElement("Dragging");
            writer.WriteElementString("DragAngle", Convert.ToString(DragAngle));
            writer.WriteElementString("DragDistance", Convert.ToString(DragDistance));
            writer.WriteEndElement();

            writer.WriteStartElement("Positions");
            foreach (GradationDragPanelPosition position in PositionCollections)
            {
                writer.WriteStartElement("Position");
                writer.WriteAttributeString("X", position.X.ToString(CultureInfo.InvariantCulture));
                writer.WriteAttributeString("Y", position.Y.ToString(CultureInfo.InvariantCulture));

                writer.WriteEndElement();
            }
            writer.WriteEndElement();


            writer.WriteStartElement("Geometries");
            foreach (GradationDragPanelClippingGeometry geometry in GeometryCollections)
            {
                writer.WriteStartElement("Geometry");
                writer.WriteAttributeString("X", geometry.X.ToString(CultureInfo.InvariantCulture));
                writer.WriteAttributeString("Y", geometry.Y.ToString(CultureInfo.InvariantCulture));
                writer.WriteAttributeString("Radius", geometry.Radius.ToString(CultureInfo.InvariantCulture));
                writer.WriteAttributeString("GeometryType", ((int)geometry.GeometryType).ToString(CultureInfo.InvariantCulture));
                Geometry created = CreateGeometry(((int)geometry.GeometryType), 
                    geometry.X, geometry.Y, geometry.Radius, geometry.RectHeight);
                geometry.GeomText = ExportB64Geometry(created);
                writer.WriteAttributeString("GeomText", geometry.GeomText.ToString(CultureInfo.InvariantCulture));
                writer.WriteEndElement();
            }
            writer.WriteEndElement();
        }

        public override void ReadXml(XmlReader reader)
        {
            base.ReadXml(reader);

            TypeConverter cc = TypeDescriptor.GetConverter(typeof(Color));

            reader.ReadStartElement("Background");
            if (reader.Name.Equals("Color"))
            {
                BackgroundColor = (Color)cc.ConvertFromInvariantString(reader.ReadElementString("Color"));
                FillBackground = BackgroundColor.A > 0;
            }
            else
            {
                FillBackground = false;
            }
            if (reader.Name.Equals("Image"))
            {
                BackgroundImage = reader.ReadElementString("Image");
                if (reader.Name.Equals("ImageAlignment"))
                {
                    BackgroundAlignment = (ImageAlignment)Enum.Parse(typeof(ImageAlignment), reader.ReadElementString("ImageAlignment"));
                }
            }
            else
            {
                BackgroundImage = "";
            }

            if (reader.Name.Equals("Background"))
            {
                reader.ReadEndElement();
            }

            if (reader.Name.Equals("BackImageSizeX"))
            {
                Int32.TryParse(reader.ReadElementString("BackImageSizeX"), out int backImageSizeX);
                BackImageSizeX = backImageSizeX;
            }
            else
            {
                BackImageSizeX = 800;
            }

            if (reader.Name.Equals("BackImageSizeY"))
            {
                Int32.TryParse(reader.ReadElementString("BackImageSizeY"), out int backImageSizeY);
                BackImageSizeY = backImageSizeY;
            }
            else
            {
                BackImageSizeY = 300;
            }

            //
            if (reader.Name.Equals("Interaction"))
            {
                reader.ReadStartElement();
                AllowInteractionFull = bool.Parse(reader.ReadElementString("AllowInteractionFull"));
                AllowInteractionNone = bool.Parse(reader.ReadElementString("AllowInteractionNone"));
                AllowInteractionLegacy = bool.Parse(reader.ReadElementString("AllowInteractionLegacy"));
                reader.ReadEndElement();
            }
            else
            {
                AllowInteractionFull = false;
                AllowInteractionNone = false;
                AllowInteractionLegacy = true;
            }

            if (reader.Name.Equals("Dragging"))
            {
                reader.ReadStartElement();
                Int32.TryParse(reader.ReadElementString("DragAngle"), out int dragAngle);
                DragAngle = dragAngle;
                Int32.TryParse(reader.ReadElementString("DragDistance"), out int dragDistance);
                DragDistance = dragDistance;
                reader.ReadEndElement();
            }
            else
            {
                DragAngle = 0;
                DragDistance = 20;
            }

            //
            if (!reader.IsEmptyElement)
            {
                PositionCollections.Clear();
                reader.ReadStartElement("Positions");
                int i = 0;
                while (reader.NodeType != XmlNodeType.EndElement)
                {
                    Int32.TryParse(reader.GetAttribute("X"), out int x);
                    Int32.TryParse(reader.GetAttribute("Y"), out int y);

                    PositionCollections.Add(new GradationDragPanelPosition(
                        this, i++, x, y));
                    reader.Read();
                }
                reader.ReadEndElement();
            }
            else
            {
                reader.Read();
                reader.ReadEndElement();
            }


            //
            if (!reader.IsEmptyElement)
            {
                GeometryCollections.Clear();
                reader.ReadStartElement("Geometries");
                int i = 0;
                //reader.Read();
                while (reader.NodeType != XmlNodeType.EndElement)
                {
                    Int32.TryParse(reader.GetAttribute("X"), out int x);
                    Int32.TryParse(reader.GetAttribute("Y"), out int y);
                    Int32.TryParse(reader.GetAttribute("Radius"), out int radius);
                    Int32.TryParse(reader.GetAttribute("RectHeight"), out int height);
                    Int32.TryParse(reader.GetAttribute("GeometryType"), out int gemetryType);
                    var geomText = reader.GetAttribute("GeomText");
                    var geometry = ImportB64Geometry(geomText);
                    GeometryCollections.Add(new GradationDragPanelClippingGeometry(
                        this, i++, x, y, gemetryType,  radius, height, geometry, geomText));
                    reader.Read();
                }
                reader.ReadEndElement();
            }
            else
            {
                reader.ReadEndElement();
            }

        }

        public override bool HitTest(Point location)
        {
            bool retVal = FillBackground;

            if (AllowInteractionFull)
            {
                retVal = false;
            }
            else if (AllowInteractionNone)
            {
                retVal = true;
            }

            return retVal;
        }

        public override void MouseDown(Point location)
        {
            // No-Op
            if (isDragging)
            {
                isDragging = false;
            }
        }

        // dragging acttion for firing trigger -> slightly throttle up key(not step) / slightly down key
        // I tested step or smooth throttle key bind.
        // Don't use pushkey action:{LSHIFT}+key! please use PAGEUP or PAGEDOWN etc
        // {LSHIFT}+key bind cause continuously interfaring my key(in debugging VS2022)
        // after dragging with this control 
        public override void MouseDrag(Point location)
        {
            if (!isDragging)
            {
                _draggingLast = location;
                isDragging = true;
            }
            var diff = _draggingLast - location;
;
            if (Math.Sqrt(diff.X * diff.X + diff.Y * diff.Y) > DragDistance)
            {
                isDragging = true;
                // i can't see why -1 multyplying.
                // but i dragging right toward,it converts fwd toward input.
                var product = (-1) * diff.X * Math.Cos(DragAngle / 180 * Math.PI) + diff.Y * Math.Sin(DragAngle / 180 * Math.PI);
                if (product > DragDistance * 0.5)
                {
                    _dragFwdTrigger.FireTrigger(new BindingValue(true));
                    _draggingLast = location;
                    isDragging = false;
                    return;
                }

                if (product < -1 * DragDistance * 0.5)
                {
                    _dragBackTrigger.FireTrigger(new BindingValue(true));
                    return;
                }
            }
        }

        public Geometry CreateGeometry(int geomType, int x, int y, int radius, int height)
        {
            PathGeometry resultGeometry = null;
            if ((int)GeometryPatternType.Circle == geomType)
            {
                resultGeometry =
                    new EllipseGeometry(new Point(x, y), radius, radius).GetFlattenedPathGeometry();
            }

            if ((int)GeometryPatternType.Rectangle == geomType)
            {
                height = height <= 0 ? 10 : height;
                resultGeometry = 
                    new RectangleGeometry(new Rect(new Point(x,y), new Point(x+ radius, y+ height)))
                        .GetFlattenedPathGeometry();
            }

            if ((int)GeometryPatternType.Diamond == geomType ||
                (int)GeometryPatternType.Pentagon == geomType ||
                (int)GeometryPatternType.Hexagon == geomType)
            {
                PathFigureCollection pfc = new PathFigureCollection();
                PathSegmentCollection psc = new PathSegmentCollection();
                var f = geomType + 1;
                var points = new List<Point>();
                var angle = 360d / f;

                for (var i = 0; i < f; i++)
                {
                    points.Add(new Point(x + (radius * Math.Cos((i * angle / 180d) * Math.PI)),
                        y + (radius * Math.Sin((i * angle / 180d) * Math.PI))));
                }

                for (var i = (f-1); i >= 0; i--)
                {
                    if (i == 0)
                    {
                        pfc.Add(new PathFigure(points[0], psc, true));
                    }
                    psc.Add(new LineSegment(points[i], false));

                }
                resultGeometry = new PathGeometry() { Figures = pfc }.GetFlattenedPathGeometry();
            }

            return resultGeometry;
        }

        public string ExportB64Geometry(Geometry geometry)
        {
            var s = XamlWriter.Save(geometry);

            byte[] byteArray = Encoding.ASCII.GetBytes(s);
            return Convert.ToBase64String(byteArray);
        }

        public PathGeometry ImportB64Geometry(String text)
        {
            if (String.IsNullOrEmpty(text)) { return new PathGeometry(); }

            var stream = new MemoryStream(Convert.FromBase64String(text));
            
            // Load
            var ob = XamlReader.Load(stream);

            return ob as PathGeometry;
        }

        public override void MouseUp(Point location)
        {
            // No-Op
        }
    }
}
