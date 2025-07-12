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
    using System.Windows.Media;
    using System.Windows;

    public class GradationDragPanelRenderer : HeliosVisualRenderer
    {
        private ImageSource _backgroundImage;
        private ImageBrush _backgroundBrush;

        protected override void OnRender(DrawingContext drawingContext)
        {
            GradationDragPanel panel = Visual as GradationDragPanel;
            if (panel != null)
            {
                double width = panel.Width;
                double height = panel.Height;
                if (panel.Opacity >= 1.0) drawingContext.PushOpacity(1.0); else drawingContext.PushOpacity(panel.Opacity);

                foreach (var geometry in panel.GeometryCollections)
                {
                    drawingContext.PushClip(geometry.Geometry);

                    if (_backgroundBrush != null)
                    {
                        if (panel.CurrentPosition < 0 ||
                            panel.PositionCollections.Count < panel.CurrentPosition)
                        {
                            return;
                        }
                        
                        var idx = panel.CurrentPosition;
                        if(idx >= panel.PositionCollections.Count)
                        {
                            idx = panel.PositionCollections.Count - 1;
                        }
                        if (idx < 0)
                        {
                            idx = 0;
                        }
                        var dx = panel.PositionCollections[idx].X;
                        var dy = panel.PositionCollections[idx].Y;

                        drawingContext.DrawRectangle(_backgroundBrush, null,
                            new Rect(dx , dy, panel.BackImageSizeX, panel.BackImageSizeY));
                    }
                    drawingContext.Pop();
                }
            }
        }

        protected override void OnRefresh()
        {
            GradationDragPanel panel = Visual as GradationDragPanel;

            if (panel != null)
            {
                _backgroundImage = ConfigManager.ImageManager.LoadImage(panel.BackgroundImage);
                _backgroundBrush = null;
                if (_backgroundImage != null)
                {
                    _backgroundBrush = new ImageBrush(_backgroundImage);
                    switch (panel.BackgroundAlignment)
                    {
                        case ImageAlignment.Centered:
                            _backgroundBrush.Stretch = Stretch.None;
                            _backgroundBrush.TileMode = TileMode.None;
                            _backgroundBrush.Viewport = new Rect(0d, 0d, 1d, 1d);
                            _backgroundBrush.ViewportUnits = BrushMappingMode.RelativeToBoundingBox;
                            break;

                        case ImageAlignment.Stretched:
                            _backgroundBrush.Stretch = Stretch.Fill;
                            _backgroundBrush.TileMode = TileMode.None;
                            _backgroundBrush.Viewport = new Rect(0d, 0d, 1d, 1d);
                            _backgroundBrush.ViewportUnits = BrushMappingMode.RelativeToBoundingBox;
                            break;

                        case ImageAlignment.Tiled:
                            _backgroundBrush.Stretch = Stretch.None;
                            _backgroundBrush.TileMode = TileMode.Tile;
                            _backgroundBrush.Viewport = new Rect(0d, 0d, _backgroundImage.Width, _backgroundImage.Height);
                            _backgroundBrush.ViewportUnits = BrushMappingMode.Absolute;
                            break;
                    }
                }
            }
        }
    }
}