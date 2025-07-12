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

    public class RoundSolidPullKnobPosition : NotificationObject
    {
        private int _index;
        private bool _canPull;
        private double _sendValue;
        private double _rotation;
        private string _knobRotateImage;
        private string _knobPulledRotateImage;
        public RoundSolidPullKnobPosition(HeliosObject rotarySwitch, int index,
            bool canPull, double sendValue, double rotation,string _image, string _pullImage)
        {
            _index = index;
            _canPull = canPull;
            _sendValue = sendValue;
            _rotation = rotation;
            _knobRotateImage = _image;
            _knobPulledRotateImage = _pullImage;
            EnterTriggger = new HeliosTrigger(rotarySwitch, "", "position " + _index, "entered", "Fires when switch enters the " + _index + " position");
            ExitTrigger = new HeliosTrigger(rotarySwitch, "", "position " + _index, "exited", "Fires when switch exits the " + _index + " position");
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


        public string KnobRotateImage
        {
            get => _knobRotateImage;
            set
            {
                if ((_knobRotateImage == null && value != null)
                    || (_knobRotateImage != null && !_knobRotateImage.Equals(value)))
                {
                    string oldValue = _knobRotateImage;
                    _knobRotateImage = value;
                    OnPropertyChanged("KnobRotateImage", oldValue, value, true);
                }
            }
        }

        public string KnobPulledbRotateImage
        {
            get => _knobPulledRotateImage;
            set
            {
                if ((_knobPulledRotateImage == null && value != null)
                    || (_knobPulledRotateImage != null && !_knobPulledRotateImage.Equals(value)))
                {
                    string oldValue = _knobPulledRotateImage;
                    _knobPulledRotateImage = value;
                    OnPropertyChanged("KnobPulledRotateImage", oldValue, value, true);
                }
            }
        }

        public bool CanPull
        {
            get
            {
                return _canPull;
            }
            set
            {
                if (!_canPull.Equals(value))
                {
                    bool oldValue = _canPull;
                    _canPull = value;
                    OnPropertyChanged("CanPull", oldValue, value, true);
                }
            }
        }

        public double Rotation
        {
            get
            {
                return _rotation;
            }
            set
            {
                if (!_sendValue.Equals(value))
                {
                    double oldValue = _rotation;
                    _rotation = value;
                    OnPropertyChanged("Rotation", oldValue, value, true);
                }
            }
        }


        public double SendValue
        {
            get
            {
                return _sendValue;
            }
            set
            {
                if (!_sendValue.Equals(value))
                {
                    double oldValue = _sendValue;
                    _sendValue = value;
                    OnPropertyChanged("SendValue", oldValue, value, true);
                }
            }
        }

        public HeliosTrigger EnterTriggger { get; }

        public HeliosTrigger ExitTrigger { get; }

        #endregion
    }
}
