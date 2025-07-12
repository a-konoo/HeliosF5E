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
    using System.Windows.Media;
    using Pen = System.Windows.Media.Pen;
    using Point = System.Windows.Point;

    public class FloatPopLineRenderer : HeliosVisualRenderer
    {
        private Pen _pen;
        
        protected override void OnRender(System.Windows.Media.DrawingContext drawingContext)
        {
            FloatPopLine floatPopLine = Visual as FloatPopLine;
            if (floatPopLine != null)
            {
                var pointS1 = new Point(floatPopLine.FirstX, floatPopLine.FirstY);
                var pointS2 = new Point(floatPopLine.SecondX, floatPopLine.SecondY);
                var curIndex = floatPopLine.CurrentPosition - 1 < 0 ?
                    0 : floatPopLine.CurrentPosition - 1;
                var currentPosition = floatPopLine.Positions[curIndex];
                var pointS3 = new Point(floatPopLine.ThirdX, floatPopLine.ThirdY);
                if (currentPosition.UseMotionVertex)
                {
                    pointS3 = new Point(currentPosition.MotionVertexX, currentPosition.MotionVertexY);
                }

                var radius = floatPopLine.Radius;
                var angle = currentPosition.ConnectAngle;
                var centerPoint = new Point(currentPosition.X, currentPosition.Y);
                var px = radius * Math.Cos(angle / 180f * Math.PI);
                var py = radius * Math.Sin(angle / 180f * Math.PI);
                PathGeometry pg = new EllipseGeometry(centerPoint, radius, radius)
                    .GetFlattenedPathGeometry();
                drawingContext.DrawGeometry(null, _pen, pg);

                LineGeometry thirdPg = new LineGeometry(
                    new Point(centerPoint.X + px, centerPoint.Y - py), pointS3);
                drawingContext.DrawGeometry(null, _pen, thirdPg);

                LineGeometry secondPg = new LineGeometry(pointS3, pointS2);
                drawingContext.DrawGeometry(null, _pen, secondPg);

                LineGeometry firstPg = new LineGeometry(pointS2, pointS1);
                drawingContext.DrawGeometry(null, _pen, firstPg);
            }
        }

        protected override void OnRefresh()
        {
            FloatPopLine floatPopLine = Visual as FloatPopLine;
            if (floatPopLine != null)
            {
                _pen = new Pen(new SolidColorBrush(floatPopLine.LineColor), floatPopLine.LineThickness);
            }
            else
            {
                _pen = new Pen(new SolidColorBrush(Color.FromRgb(255,255,255)), 1d);
            }
        }
    }
}
