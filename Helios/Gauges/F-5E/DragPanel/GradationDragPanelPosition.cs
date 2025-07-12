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

    public class GradationDragPanelPosition : NotificationObject
    {
        private int _index;
        private int _x;
        private int _y;

        public GradationDragPanelPosition(HeliosObject _, int index,int x, int y)
        {
            _index = index;
            _x = x;
            _y = y;
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

        #endregion
    }
}
