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

    public class FlipAnimationStepBase : NotificationObject
    {
        private int _index;
        private string _name;
        protected int _frame;
        protected int _sendValue;
        private String _positionExampleFile;
        protected FlipAnimationBase _animbase;

        public FlipAnimationStepBase(FlipAnimationBase animbase, int index, string name, int frame, int sendValue)
        {
            _animbase = animbase;
            _index = index;
            _name = name;
            _frame = frame;
            _sendValue = sendValue;
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

        public int Frame
        {
            get => _frame;
            set
            {
                if (_frame != value)
                {
                    int oldValue = _frame;
                    _frame = value;
                    if (_animbase.IsDescriptionOut)
                    {
                        PositionExampleFileDescription = FlipAnimUtil.CreatePositionExampleFor2PH(
                            _animbase.AnimationFrameImageNamePattern,
                            (int)_animbase.PatternNumber, _frame);
                    }
                    OnPropertyChanged("StartFrame", oldValue, value, true);
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
        #endregion
    }
}
