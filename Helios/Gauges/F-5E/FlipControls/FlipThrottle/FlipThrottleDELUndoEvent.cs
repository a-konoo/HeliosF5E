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
    public class FlipThrottleDELUndoEvent : IUndoItem
    {
        private int _index;
        private FlipThrottle _throttle;
        private FlipThrottlePosition _position;
          
        public FlipThrottleDELUndoEvent(FlipThrottle throttle, FlipThrottlePosition position, int index)
        {
            _index = index;
            _throttle = throttle;
            _position = position;
        }

        public void Undo()
        {
            _throttle.Positions.Insert(_index, _position);
        }

        public void Do()
        {
            _throttle.Positions.Remove(_position);
        }
    }
}
