﻿//  Copyright 2014 Craig Courtney
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
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace GadrocsWorkshop.Helios.Controls.F5E
{

    public class FlipButtonPosition : FlipAnimationStepBase
    {
        private FlipButton _button;
        private Double _dx;
        private Double _dy;
        private Double _dragAngle;

        public FlipButtonPosition(FlipButton button, int index, string name, int frame,
            double dx, double dy, double dragAngle) :
            base(button, index, name, frame, 0)
        {
            _button = button;
            _dx = dx;
            _dy = dy;
            _dragAngle = dragAngle;
            PositionExampleFileDescription = FlipAnimUtil.CreatePositionExampleFor2PH(
                _button.AnimationFrameImageNamePattern, 
                (int)_button.PatternNumber, this.Frame);

        }

        #region Properties

        public double Dx
        {
            get => _dx;
            set
            {
                if (_dx != value)
                {
                    double oldValue = _dx;
                    _dx = value;
                    OnPropertyChanged("Dx", oldValue, value, true);
                }
            }
        }

        public double Dy
        {
            get => _dy;
            set
            {
                if (_dy != value)
                {
                    double oldValue = _dy;
                    _dy = value;
                    OnPropertyChanged("Dy", oldValue, value, true);
                }
            }
        }
        // toggle inclement direction of each position
        public double DragAngle
        {
            get => _dragAngle;
            set
            {
                value %= 360;
                if (_dragAngle != value)
                {
                    double oldValue = _dragAngle;
                    _dragAngle = value;
                    OnPropertyChanged("DragAngle", oldValue, value, true);
                }
            }
        }
        #endregion
    }
}
