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
    using System;

    public class GradationDragPanelPositionChangeArgs : EventArgs
    {
        private GradationDragPanelPosition _position;
        private string _property;
        private object _oldValue;
        private object _newValue;

        public GradationDragPanelPositionChangeArgs(
            GradationDragPanelPosition position,
            string propertyName,
            object oldValue,
            object newValue)
        {
            _position = position;
            _property = propertyName;
            _oldValue = oldValue;
            _newValue = newValue;
        }

        #region Properties

        public string PropertyName
        {
            get { return _property; }
        }

        public GradationDragPanelPosition Position
        {
            get { return _position; }
        }

        public object OldValue
        {
            get { return _oldValue; }
        }

        public object NewValue
        {
            get { return _newValue; }
        }

        #endregion
    }
}
