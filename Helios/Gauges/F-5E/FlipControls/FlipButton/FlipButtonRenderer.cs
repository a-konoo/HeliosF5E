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

    public class FlipButtonRenderer : FlipAnimationRenderer
    {
        private static readonly NLog.Logger Logger = NLog.LogManager.GetCurrentClassLogger();
        private int prevPosition = 0;

        protected override void OnRender(System.Windows.Media.DrawingContext drawingContext)
        {
            FlipButton button = Visual as FlipButton;
            if (button != null)
            {
                if (prevPosition != button.CurrentPosition)
                {
                    prevPosition = button.CurrentPosition;
                }
                _imageRect.Width = button.Width;
                _imageRect.Height = button.Height;
                int current = button.AnimationFrameNumber;
                if (button.AnimationFrames.Count < current)
                {
                    Logger.Warn($"position {current}th image is missing:");
                    return;
                }
                // range check
                if (button.AnimationFrames.Count <= 0 ||
                    button.AnimationFrameNumber > button.AnimationFrames.Count)
                { return;  }

                var _images = button.AnimationFrames[current - 1];
                drawingContext.DrawImage(_images[button.CurrentPatternNumber - 1], _imageRect);
                var position = button.Positions[button.CurrentPosition] as FlipButtonPosition;
                if (!button.DesignMode || !button.IsPositionLock) {
                    button.Left = position.Dx + button.OriginPoint.X;
                    button.Top = position.Dy + button.OriginPoint.Y;
                }

                //base.OnRender(drawingContext);
            }
        }

        protected override void OnRefresh()
        {
            base.OnRefresh();
        }
    }
}
