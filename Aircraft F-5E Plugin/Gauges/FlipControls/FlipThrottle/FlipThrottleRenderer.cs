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

namespace GadrocsWorkshop.Helios.Controls.F5E
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Text.RegularExpressions;
    using System.Windows.Media;

    public class FlipThrottleRenderer : FlipAnimationRenderer
    {
        private ImageSource[] _images;
        private static readonly NLog.Logger Logger = NLog.LogManager.GetCurrentClassLogger();
        private bool _isPushedFrameLoaded = false;
        private Dictionary<String, String> errPushImagePathDic = new Dictionary<string, string>();

        protected override void OnRender(System.Windows.Media.DrawingContext drawingContext)
        {
            base.OnRender(drawingContext);
            FlipThrottle throttle = Visual as FlipThrottle;
            throttle.DrivePositionAfterRender();
        }

        protected override void OnRefresh()
        {
            base.OnRefresh();
        }


        private List<String[]> CreatePatternPath(int current, int pattern,
            FlipAnimationStepCollection positions, String path, MatchCollection matches)
        {
            (string _, string filename) = FlipAnimUtil.GetDirPathAndFileName(path);

            FlipAnimationStepBase position = positions[current];
            var frame = position.Frame;
            (string prev, string aft) = FlipAnimUtil.GetFilenameParts(filename, matches,2);

            var xx = FlipAnimUtil.GetPathArrayFor2PH(frame, pattern, prev, aft);
            return xx.ToList();
        }

        private List<ImageSource[]> LoadPatternImages(int positionIdx, String basepath,
            List<String[]> fileList, IImageManager3 imgMgr, StringBuilder sb)
        {
            String fileName = String.Empty;
            List<ImageSource[]> loadImages = new List<ImageSource[]>();
            for (var i=0; i< fileList.Count; i++)
            {
                String[] patternPaths = fileList[i];
                List<ImageSource> imagePatterns = new List<ImageSource>();
                try
                {
                    for (var j = 0; j < patternPaths.Length; j++)
                    {
                        fileName = System.IO.Path.Combine(basepath +"\\" + fileList[i][j]);
                        var image = imgMgr.LoadImage(fileName);
                        if (image == null)
                        {
                            sb.Append($"position {positionIdx} file:{fileName} load fail.");
                        }
                        imagePatterns.Add(image);
                    }
                }catch
                {
                    sb.Append($"position {positionIdx} file:{fileName} load fail.");
                }
                loadImages.Add(imagePatterns.ToArray());
            }

            return loadImages;
        }
    }
}
