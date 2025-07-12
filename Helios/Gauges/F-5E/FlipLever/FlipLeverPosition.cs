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

using System;

namespace GadrocsWorkshop.Helios.Controls
{

    public class FlipLeverPosition : NotificationObject
    {
        private FlipLever _lever;
        private int _index;
        private string _name;
        protected int _startFrame;
        protected int _endFrame;
        protected int _frameSpan; // msec
        protected int _sendValue;
        private String _positionExampleFile;
        private double _dragAngle;
        private int _dragDistance;

        public FlipLeverPosition(FlipLever lever, int index, string name, int startFrame,
            int endFrame, int frameSpan,  int sendValue, double dragAngle, int dragDistance)
        {
            _lever = lever;
            _index = index;
            _name = name;
            _startFrame = startFrame;
            _endFrame = endFrame;
            _frameSpan = frameSpan;

            _dragAngle = dragAngle;
            _dragDistance = dragDistance;
            _sendValue = sendValue;
        }

        public double DragAngle
        {
            get => _dragAngle;
            set
            {
                if (_dragAngle != value)
                {
                    double oldValue = _dragAngle;
                    _dragAngle = value;
                    OnPropertyChanged("DragAngle", oldValue, value, true);
                }
            }
        }

        public int DragDistance
        {
            get => _dragDistance;
            set
            {
                if (_dragDistance != value)
                {
                    double oldValue = _dragDistance;
                    _dragDistance = value;
                    OnPropertyChanged("DragDistance", oldValue, value, true);
                }
            }
        }

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


        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                if ((_name == null && value != null)
                    || (_name != null && !_name.Equals(value)))
                {
                    string oldValue = _name;
                    _name = value;
                    OnPropertyChanged("Name", oldValue, value, true);
                }
            }
        }

        public int StartFrame
        {
            get => _startFrame;
            set
            {
                if (_startFrame != value)
                {
                    int oldValue = _startFrame;
                    _startFrame = value;
                    OnPropertyChanged("StartFrame", oldValue, value, true);
                }
            }
        }

        public int EndFrame
        {
            get => _endFrame;
            set
            {
                if (_endFrame != value)
                {
                    int oldValue = _endFrame;
                    _endFrame = value;
                    OnPropertyChanged("EndFrame", oldValue, value, true);
                }
            }
        }

        public int FrameSpan
        {
            get => _frameSpan;
            set
            {
                if (_frameSpan != value)
                {
                    int oldValue = _frameSpan;
                    _frameSpan = value;
                    OnPropertyChanged("FrameSpan", oldValue, value, true);
                }
            }
        }

        public int SendValue
        {
            get
            {
                return _sendValue;
            }
            set
            {
                if (!_sendValue.Equals(value))
                {
                    int oldValue = _sendValue;
                    _sendValue = value;
                    OnPropertyChanged("SendValue", oldValue, value, true);
                }
            }
        }

        public String PositionExampleFileDescription
        {
            get => _positionExampleFile;
            set
            {
                if (_positionExampleFile != null && _positionExampleFile.Equals(value))
                {
                    return;
                }
                String oldValue = _positionExampleFile;
                // GRID remove first "_" for access key
                _positionExampleFile = "_" + value;
                OnPropertyChanged("PositionExampleFileDescription", oldValue, value, true);
            }
        }
    }


}
