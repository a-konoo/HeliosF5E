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

    public class FlipLeverRenderer : HeliosVisualRenderer
    {
        protected ImageBrush _imageBrush;
        protected Rect _imageRect;

        private ImageSource[] _images;
        private static readonly NLog.Logger Logger = NLog.LogManager.GetCurrentClassLogger();
        private Dictionary<String, String> errPushImagePathDic = new Dictionary<string, string>();

        protected override void OnRender(System.Windows.Media.DrawingContext drawingContext)
        {
            FlipLever flipControl = Visual as FlipLever;
            if (flipControl != null)
            {
                _imageRect.Width = flipControl.Width;
                _imageRect.Height = flipControl.Height;

                var current = flipControl.AnimationFrameNumber;
                if (flipControl.AnimationFrames.Count < current)
                {
                    Logger.Warn($"position {current}th image is missing:");
                    return;
                }

                // first frame image
                if (flipControl.AnimationFrames.Count > 0 &&
                    flipControl.AnimationFrameNumber <= flipControl.AnimationFrames.Count)
                {
                    var _image = flipControl.AnimationFrames[current - 1];
                    drawingContext.DrawImage(_image, _imageRect);
                }
            }
        }

        protected override void OnRefresh()
        {
            FlipLever flipLever = Visual as FlipLever;

            if (flipLever == null || !flipLever.IsRenderReady) { return; }

            flipLever.AnimationFrames.Clear();
            flipLever.AnimationFrames = new List<ImageSource>();

            // load pushed image frames
            var flCount = flipLever.Positions?.Count;
            var positions = flipLever.Positions;
            if (flCount == 0) { return; }

            StringBuilder sb = new StringBuilder();

            var isCapable = ConfigManager.ImageManager is IImageManager3;
            var _namePattern = flipLever.AnimationFrameImageNamePattern;

            if (String.IsNullOrEmpty(_namePattern) || !isCapable)
            {
                flipLever.AnimationFrames.Clear();
                return;
            }
            var imgMgr = ConfigManager.ImageManager as IImageManager3;
            var isImageLoaded = false;

            if (flipLever.AnimationFrames.Count == 0 && flipLever.AnimationFrames.Count > 0)
            {
                flipLever.AnimationFrames.Clear();
            }
            (bool ptnNameChecked, MatchCollection ptnMatches) =
                FlipAnimUtil.CheckReplaceNumberFormat(_namePattern, 1);

            if (!ptnNameChecked)
            {
                flipLever.ErrorMessage += $"first image filename must be XXXX_01_01.png format.\t\n";
                return;
            }

            var baseFilePath = flipLever.AnimationFrameImageNamePattern;
            (string baseDirPath, string _) = FlipAnimUtil.GetDirPathAndFileName(baseFilePath);
            for (int i = 0; i < flCount; i++)
            {
                try
                {
                    var pathOfEachFrames =
                        CreatePatternPath(i, positions, baseFilePath, ptnMatches);
                    var images = LoadPatternImages(i, baseDirPath, pathOfEachFrames, imgMgr, sb);
                    flipLever.AnimationFrames.AddRange(images);
                }
                catch
                {
                    isImageLoaded = false;
                }
            }
            if (sb.Length > 0)
            {
                flipLever.ErrorMessage += sb.ToString();
                isImageLoaded = false;
            }

            flipLever.FrameLoaded = isImageLoaded;
        }


        private List<String> CreatePatternPath(int current,FlipLeverStepCollection positions,
            String path, MatchCollection matches)
        {
            (string _, string filename) = FlipAnimUtil.GetDirPathAndFileName(path);

            FlipLeverPosition position = positions[current];
            var startFrame = position.StartFrame;
            var lastFrame = position.EndFrame;
            (string prev, string aft) = FlipAnimUtil.GetFilenameParts(filename, matches, 1);

            var xx = FlipAnimUtil.GetAnimPathArrayFor1PH(startFrame, lastFrame, prev, aft);
            return xx.ToList();
        }

        private List<ImageSource> LoadPatternImages(int positionIdx, String basepath,
            List<String> fileList, IImageManager3 imgMgr, StringBuilder sb)
        {
            String fileName = String.Empty;
            List<ImageSource> images = new List<ImageSource>();
            for (var i=0; i< fileList.Count; i++)
            {
                try
                {
                    fileName = System.IO.Path.Combine(basepath + "\\" + fileList[i]);
                    var image = imgMgr.LoadImage(fileName);
                    if (image == null)
                    {
                        sb.Append($"position {positionIdx} file:{fileName} load fail.");
                    }
                    images.Add(image);
                }
                catch
                {
                    sb.Append($"position {positionIdx} file:{fileName} load fail.");
                }
            }

            return images;
        }
    }
}
