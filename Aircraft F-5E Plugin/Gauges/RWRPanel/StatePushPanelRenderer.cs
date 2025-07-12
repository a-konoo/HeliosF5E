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
    using System.Text.RegularExpressions;
    using System.Text;
    using System.Windows.Media;
    using System.Windows;

    public class StatePushPanelRenderer : HeliosVisualRenderer
    {
        private ImageBrush _imageBrush;
        private Rect _imageRect;

        private static readonly NLog.Logger Logger = NLog.LogManager.GetCurrentClassLogger();

        protected override void OnRender(System.Windows.Media.DrawingContext drawingContext)
        {
            var statePanel = Visual as StatePushPanel;
            if (statePanel != null)
            {
                var _images = statePanel.Images;
                _imageRect.Width = statePanel.Width;
                _imageRect.Height = statePanel.Height;
                int currentState = statePanel.CurrentState;
                if (currentState < 0 || _images.Length <= currentState ||
                    statePanel.Positions.Count < currentState)
                {
                    Logger.Warn($"state {statePanel.CurrentState} image is missing:");
                    return;
                }
                
                drawingContext.DrawImage(_images[currentState], _imageRect);

            }
        }

        protected override void OnRefresh()
        {
            var statePanel = Visual as StatePushPanel;

            if (statePanel == null || !statePanel.IsRenderReady) { return; }

            statePanel.Images = new ImageSource[] { };

            // load pushed image frames
            var flCount = statePanel.Positions?.Count;
            var positions = statePanel.Positions;
            if (flCount == 0) { return; }

            StringBuilder sb = new StringBuilder();

            var isCapable = ConfigManager.ImageManager is IImageManager3;
            var _namePattern = statePanel.AnimationFrameImageNamePattern;

            if (String.IsNullOrEmpty(_namePattern) || !isCapable)
            {
                statePanel.Images = new ImageSource[] { };
                return;
            }
            var imgMgr = ConfigManager.ImageManager as IImageManager3;

            (bool ptnNameChecked, MatchCollection ptnMatches) =
                FlipAnimUtil.CheckReplaceNumberFormat(_namePattern , 1);

            if (!ptnNameChecked)
            {
                statePanel.ErrorMessage += $"first image filename must be XXXX_01.png format.\t\n";
                return;
            }

            var baseFilePath = statePanel.AnimationFrameImageNamePattern;
            (string baseDirPath, string _) = FlipAnimUtil.GetDirPathAndFileName(baseFilePath);
            List<ImageSource> positionImages = new List<ImageSource>();
            for (int i = 0; i < flCount; i++)
            {
                var positionIdx = positions[i].Index + 1;
                var imageFile = CreatePatternPath(positionIdx, baseFilePath, ptnMatches);
                try
                {
                    var image = LoadPatternImage(positionIdx, baseDirPath, imageFile, imgMgr);
                    if (image == null)
                    {
                        sb.Append($"load {imageFile} load fail.");
                    }
                    positionImages.Add(image);
                }
                catch
                {
                    sb.Append($"load {imageFile} load fail.");
                }
            }
            if (sb.Length > 0)
            {
                statePanel.ErrorMessage += sb.ToString();
            }
            statePanel.Images = positionImages.ToArray();
        }

        private String CreatePatternPath(int current, String path, MatchCollection matches)
        {
            (string _, string filename) = FlipAnimUtil.GetDirPathAndFileName(path);

            string replacement = ExtractReplacement(filename, matches);

            return $"{replacement}_{FlipAnimUtil.ZeroPadNum(current)}.png";
        }

        public static String ExtractReplacement(String filename, MatchCollection matches)
        {
            if (matches == null || matches.Count != 1) { return String.Empty; }
            var target = filename.Substring(0, matches[0].Index);
            return target;
        }

        private ImageSource LoadPatternImage(int current, String basepath,
            String filename, IImageManager3 imgMgr)
        {
            String loadImageFilePath = String.Empty;

            loadImageFilePath = System.IO.Path.Combine(basepath + "\\" + filename);
            var image = imgMgr.LoadImage(loadImageFilePath);
            return image;
        }

    }
}
