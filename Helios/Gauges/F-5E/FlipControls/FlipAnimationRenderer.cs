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
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Text.RegularExpressions;
    using System.Windows;
    using System.Windows.Media;

    public class FlipAnimationRenderer : HeliosVisualRenderer
    {
        protected ImageBrush _imageBrush;
        protected Rect _imageRect;

        protected bool _isBaseFrameLoaded = false;
        private static readonly NLog.Logger Logger = NLog.LogManager.GetCurrentClassLogger();
        protected Dictionary<String, String> errPathDic = new Dictionary<string, string>();

        protected override void OnRender(System.Windows.Media.DrawingContext drawingContext)
        {
            FlipAnimationBase flipControl = Visual as FlipAnimationBase;
            if (flipControl != null)
            {

                _imageRect.Width = flipControl.Width;
                _imageRect.Height = flipControl.Height;
                var currentPosition = flipControl.Positions[flipControl.CurrentPositionIndex];
                var current = currentPosition.Frame > 0 ? currentPosition.Frame : 1;
                if (flipControl.AnimationFrames.Count < current)
                {
                    Logger.Warn($"position {current}th image is missing:");
                    return;
                }
                int currentPattern = flipControl.CurrentPatternNumber;
                // first frame image
                if (flipControl.AnimationFrames.Count > 0 &&
                    flipControl.AnimationFrameNumber <= flipControl.AnimationFrames.Count)
                {
                    var _images = flipControl.AnimationFrames[current-1];
                    if (currentPattern > 0 && currentPattern - 1 < _images.Length)
                    {
                        drawingContext.DrawImage(_images[currentPattern - 1], _imageRect);
                    }
                    else
                    {
                        Logger.Warn($"image pattern {currentPattern} of position {current}th is missing");
                        return;
                    }
                }
            }
        }

        protected override void OnRefresh()
        {
            FlipAnimationBase animBase = Visual as FlipAnimationBase;

            if (animBase == null || !animBase.IsRenderReady) { return; }

            animBase.AnimationFrames.Clear();
            animBase.AnimationFrames = new List<ImageSource[]>();

            // load pushed image frames
            var flCount = animBase.Positions?.Count;
            var positions = animBase.Positions;
            if (flCount == 0) { return; }

            StringBuilder sb = new StringBuilder();

            var isCapable = ConfigManager.ImageManager is IImageManager3;
            var _namePattern = animBase.AnimationFrameImageNamePattern;

            if (String.IsNullOrEmpty(_namePattern) || !isCapable)
            {
                animBase.AnimationFrames.Clear();
                return;
            }
            var imgMgr = ConfigManager.ImageManager as IImageManager3;
            var isImageLoaded = false;
            int patternNumber = Convert.ToInt32(animBase.PatternNumber);

            if (patternNumber < ((Int32)FlipDisplayPatternType.singlePattern) &&
                patternNumber > ((Int32)FlipDisplayPatternType.threePattern))
            {
                return;
            }
            if (animBase.AnimationFrames.Count == 0 && animBase.AnimationFrames.Count > 0)
            {
                animBase.AnimationFrames.Clear();
            }
            (bool ptnNameChecked, MatchCollection ptnMatches) =
                FlipAnimUtil.CheckReplaceNumberFormat(_namePattern, 2);

            if (!ptnNameChecked)
            {
                animBase.ErrorMessage += $"first image filename must be XXXX_01_01.png format.\t\n";
                return;
            }

            var baseFilePath = animBase.AnimationFrameImageNamePattern;
            (string baseDirPath, string _) = FlipAnimUtil.GetDirPathAndFileName(baseFilePath);
            for (int i = 0; i < flCount; i++)
            {
                try
                {
                    List<ImageSource> positionImages = new List<ImageSource>();
                    var pathOfEachFrames = 
                        CreatePatternPath(i, patternNumber, positions, baseFilePath, ptnMatches);
                    var images = LoadPatternImages(i, baseDirPath, pathOfEachFrames, imgMgr, sb);
                    foreach (var frames in images)
                    {
                        animBase.AnimationFrames.Add(frames);
                    }
                }
                catch
                {
                    isImageLoaded = false;
                }
            }
            if (sb.Length > 0)
            {
                animBase.ErrorMessage += sb.ToString();
                isImageLoaded = false;
            }

            animBase.FrameLoaded = isImageLoaded;
        }

        private List<String[]> CreatePatternPath(int current, int pattern,
            FlipAnimationStepCollection positions, String path, MatchCollection matches)
        {
            (string _, string filename) = FlipAnimUtil.GetDirPathAndFileName(path);

            FlipAnimationStepBase position = positions[current];
            var frame = position.Frame;
            (string prev, string aft) = FlipAnimUtil.GetFilenameParts(filename, matches, 2);

            var resultArrays = FlipAnimUtil.GetPathArrayFor2PH(frame, pattern, prev, aft);
            var result = new List<String[]>();
            return resultArrays;
        }

        private List<ImageSource[]> LoadPatternImages(int positionIdx, String basepath,
            List<String[]> fileList, IImageManager3 imgMgr, StringBuilder sb)
        {
            String fileName = String.Empty;
            List<ImageSource[]> loadImages = new List<ImageSource[]>();
            for (var i = 0; i < fileList.Count; i++)
            {
                String[] patternPaths = fileList[i];
                List<ImageSource> imagePatterns = new List<ImageSource>();
                try
                {
                    for (var j = 0; j < patternPaths.Length; j++)
                    {
                        fileName = System.IO.Path.Combine(basepath + "\\" + fileList[i][j]);
                        var image = imgMgr.LoadImage(fileName);
                        if (image == null)
                        {
                            sb.Append($"position {positionIdx} file:{fileName} load fail.");
                        }
                        imagePatterns.Add(image);
                    }
                }
                catch
                {
                    sb.Append($"position {positionIdx} file:{fileName} load fail.");
                }
                loadImages.Add(imagePatterns.ToArray());
            }

            return loadImages;
        }
    }
}
