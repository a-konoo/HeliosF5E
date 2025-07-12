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

    public class FloatPopLinePosition : NotificationObject
    {
        private int _index;
        private int _x;
        private int _y;
        private int _motionVertexX;
        private int _motionVertexY;
        private int _connectAngle;
        private bool _useMotionVertex;

        public FloatPopLinePosition(HeliosObject _, int index,
            int x, int y, int motionVertexX, int motionVertexY, bool useMotionVertex, int connectAngle)
        {
            _index = index;
            _x = x;
            _y = y;
            _motionVertexX = motionVertexX;
            _motionVertexY = motionVertexY;
            _useMotionVertex = useMotionVertex;
            _connectAngle = connectAngle;
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

        public int MotionVertexX
        {
            get
            {
                return _motionVertexX;
            }
            set
            {
                if (!_motionVertexX.Equals(value))
                {
                    int oldValue = _motionVertexX;
                    _motionVertexX = value;
                    OnPropertyChanged("MotionVertexX", oldValue, value, false);
                }
            }
        }

        public int MotionVertexY
        {
            get
            {
                return _motionVertexY;
            }
            set
            {
                if (!_motionVertexY.Equals(value))
                {
                    int oldValue = _motionVertexY;
                    _motionVertexY = value;
                    OnPropertyChanged("MotionVertexY", oldValue, value, false);
                }
            }
        }

        public bool UseMotionVertex
        {
            get
            {
                return _useMotionVertex;
            }
            set
            {
                if (!_useMotionVertex.Equals(value))
                {
                    _useMotionVertex = value;
                    OnPropertyChanged("UseMotionVertex", !value, value, false);
                }
            }
        }

        public int ConnectAngle
        {
            get
            {
                return _connectAngle;
            }
            set
            {
                if (!_connectAngle.Equals(value))
                {
                    int oldValue = _connectAngle;
                    _connectAngle = value;
                    OnPropertyChanged("ConnectAngle", oldValue, value, false);
                }
            }
        }


        #endregion
    }
}
