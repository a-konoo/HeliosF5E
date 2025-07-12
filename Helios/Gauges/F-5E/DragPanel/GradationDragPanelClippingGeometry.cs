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

using System.Windows.Media;

namespace GadrocsWorkshop.Helios.Controls
{

    public class GradationDragPanelClippingGeometry : NotificationObject
    {
        private int _index;
        private int _x;
        private int _y;
        private GeometryPatternType _geometryType;
        private int _radius;
        private int _rectHeight;
        private Geometry _geometry;
        private string _geomText;
        private HeliosVisual _panel;

        public GradationDragPanelClippingGeometry(HeliosVisual panel, int index,
            int x, int y, int geometryType, int radius, int rectHeight, Geometry geometry, string geomText)
        {
            _panel = panel;
            _index = index;
            _x = x;
            _y = y;
            _geometry = geometry;
            _geometryType = (GeometryPatternType)geometryType;
            _radius = radius;
            _rectHeight = rectHeight;
            _geomText = geomText;
        }

        #region Properties

        public int Index
        {
            get
            {
                return _index;
            }
            set
            {
                if (!_index.Equals(value))
                {
                    int oldValue = _index;
                    _index = value;
                    OnPropertyChanged("Index", oldValue, value, false);
                }
            }
        }

        public int X
        {
            get
            {
                return _x;
            }
            set
            {
                if (!_x.Equals(value))
                {
                    int oldValue = _x;
                    _x = value;
                    OnPropertyChanged("X", oldValue, value, false);
                }
            }
        }

        public int Y
        {
            get
            {
                return _y;
            }
            set
            {
                if (!_y.Equals(value))
                {
                    int oldValue = _y;
                    _y = value;
                    OnPropertyChanged("Y", oldValue, value, false);
                }
            }
        }

        public GeometryPatternType GeometryType
        {
            get
            {
                return (GeometryPatternType)_geometryType;
            }
            set
            {
                if (!_geometryType.Equals(value))
                {
                    GeometryPatternType oldValue = (GeometryPatternType)_geometryType;
                    _geometryType = value;
                    OnPropertyChanged("GeometryType", oldValue, value, false);
                }
            }
        }

        public int Radius
        {
            get
            {
                return _radius;
            }
            set
            {
                if (!_radius.Equals(value) && _radius < _panel.Width && _radius > 0)
                {
                    int oldValue = _radius;
                    _radius = value;
                    OnPropertyChanged("Radius", oldValue, value, false);
                }
            }
        }

        public int RectHeight
        {
            get
            {
                return _rectHeight;
            }
            set
            {
                if (!_rectHeight.Equals(value) && value < _panel.Height && value > 0)
                {
                    int oldValue = _rectHeight;
                    _rectHeight = value;
                    OnPropertyChanged("RectHeight", oldValue, value, false);
                }
            }
        }
        
        public Geometry Geometry
        {
            get
            {
                return _geometry;
            }
            set
            {

                Geometry oldValue = _geometry;
                _geometry = value;
                OnPropertyChanged("Geometry", oldValue, value, false);
            }
        }

        public string GeomText
        {
            get
            {
                return _geomText;
            }
            set
            {
                if (!_geomText.Equals(value))
                {
                    string oldValue = _geomText;
                    _geomText = value;
                    OnPropertyChanged("GeomText", oldValue, value, false);
                }
            }
        }

        #endregion
    }
}
