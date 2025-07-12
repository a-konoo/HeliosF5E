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

namespace GadrocsWorkshop.Helios.Gauges.F5E
{
    using GadrocsWorkshop.Helios.Controls;
    using System.Drawing;
    using System.Windows;
    using System.Windows.Media;
    using Brush = System.Windows.Media.Brush;
    using Pen = System.Windows.Media.Pen;

    public class GuardCloseOnToggleStateRenderer : HeliosVisualRenderer
    {
        private ImageSource _imageOneGuardUp;
        private ImageSource _imageTwoGuardDown;
        private Rect _imageRect;

        protected override void OnRender(DrawingContext drawingContext)
        {
            GuardCloseOnToggleState toggleSwitch = Visual as GuardCloseOnToggleState;
            if (toggleSwitch != null)
            {
                if (toggleSwitch.GuardPosition == GuardPosition.Up)
                {
                    drawingContext.DrawImage(_imageOneGuardUp, _imageRect);
                }
                else
                {
                    drawingContext.DrawImage(_imageTwoGuardDown, _imageRect);
                }

            }
            if (Visual.DesignMode)
            {
                //255, 165, 0
                /*
                Brush brushOrange = new SolidColorBrush(System.Windows.Media.Color.FromArgb(60,255, 165, 0));
                Pen penO = new Pen(brushOrange, 1);
                drawingContext.DrawRectangle(brushOrange, penO, new Rect(
                    toggleSwitch._guardDownRegion.X, toggleSwitch._guardDownRegion.Y,
                    toggleSwitch._guardDownRegion.Width, toggleSwitch._guardDownRegion.Height));

                Brush brushGreen = new SolidColorBrush(System.Windows.Media.Color.FromArgb(60, 10, 251, 10));
                Pen penG = new Pen(brushGreen, 1);
                drawingContext.DrawRectangle(brushGreen, penG, new Rect(
                    toggleSwitch._switchRegion.X, toggleSwitch._switchRegion.Y,
                    toggleSwitch._switchRegion.Width, toggleSwitch._switchRegion.Height));

                Brush brushBlue = new SolidColorBrush(System.Windows.Media.Color.FromArgb(60, 20, 20, 240));
                Pen penB = new Pen(brushBlue, 1);
                drawingContext.DrawRectangle(brushBlue, penB, new Rect(
                    toggleSwitch._guardUpRegion.X, toggleSwitch._guardUpRegion.Y,
                    toggleSwitch._guardUpRegion.Width, toggleSwitch._guardUpRegion.Height));
                */
            }

        }

        protected override void OnRefresh()
        {
            GuardCloseOnToggleState toggleSwitch = Visual as GuardCloseOnToggleState;
            if (toggleSwitch != null)
            {
                _imageRect.Width = toggleSwitch.Width;
                _imageRect.Height = toggleSwitch.Height;
                _imageOneGuardUp = ConfigManager.ImageManager.LoadImage(toggleSwitch.PositionOneGuardUpImage);
                _imageTwoGuardDown = ConfigManager.ImageManager.LoadImage(toggleSwitch.PositionTwoGuardDownImage);
            }
            else
            {
                _imageOneGuardUp = null;
                _imageTwoGuardDown = null;
            }
        }
    }
}
