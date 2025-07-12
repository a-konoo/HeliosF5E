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

    public class Flip3WayToggleRenderer : FlipAnimationRenderer
    {

        private static readonly NLog.Logger Logger = NLog.LogManager.GetCurrentClassLogger();
        private int prevPosition = 0;

        protected override void OnRender(System.Windows.Media.DrawingContext drawingContext)
        {
            Flip3WayToggle toggle = Visual as Flip3WayToggle;
            if (toggle != null)
            {
                if (prevPosition != toggle.CurrentPosition)
                {
                    var position = toggle.Positions[toggle.CurrentPosition] as Flip3WayTogglePosition;
                    if (!toggle.DesignMode || !toggle.IsPositionLock)
                    {
                        toggle.Left = position.Dx + toggle.OriginPoint.X;
                        toggle.Top = position.Dy + toggle.OriginPoint.Y;
                    }
                    prevPosition = toggle.CurrentPosition;
                }
                base.OnRender(drawingContext);
            }
        }

        protected override void OnRefresh()
        {
            base.OnRefresh();
        }
    }
}
