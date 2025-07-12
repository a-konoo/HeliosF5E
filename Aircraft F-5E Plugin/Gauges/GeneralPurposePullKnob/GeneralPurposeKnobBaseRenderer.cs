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
    using System.Windows;
    using System.Windows.Media;
    using System.Windows.Media.Imaging;
    using Point = System.Windows.Point;
    using Size = System.Drawing.Size;

    public class GeneralPurposeKnobBaseRenderer : HeliosVisualRenderer
    {

        private ImageSource _imageKnobIm;
        private ImageSource _imageKnobPulledIm;
        private List<ImageSource> _imageLinesImList;
        private List<ImageSource> _imageLinesPulledImList;
        private IImageManager3 _imgLoader = ConfigManager.ImageManager as IImageManager3;
        private ImageSource _imageFrontImage;
        private ImageSource _imagePullReadyImage;

        private Dictionary<String, String> _errPathDic = new Dictionary<string, string>();

        private Rect _imageKnobRect;

        private Point _center;
        private Point _frontCenter;

        private bool isLoadImage = true;

        protected override void OnRender(System.Windows.Media.DrawingContext drawingContext)
        {
            GeneralPurposeKnobBase rotary = Visual as GeneralPurposeKnobBase;
            var currentFrame = rotary.CurrentPosition;
            GeneralPurposeKnobBasePosition positions = new GeneralPurposeKnobBasePosition(null, 1, false,
                1, 0d, "dummy", "dummy");

            if (currentFrame < rotary.Positions.Count)
            {
                positions = rotary.Positions[currentFrame];
            }
            var currentAngle = positions?.Rotation ?? 0;

            // knob (not pulled)
            if (!rotary.PullReleaseKnob)
            {
                if (_imageKnobIm != null)
                {
                    drawingContext.DrawImage(_imageKnobIm, _imageKnobRect);
                }

                if (currentFrame > _imageLinesImList.Count)
                {
                    return;
                }

                if (_imageLinesImList != null &&_imageLinesImList.Count > 0 && 
                        _imageLinesImList.Count > currentFrame && _imageLinesImList[currentFrame] != null)
                {
                    drawingContext.DrawImage(
                    _imageLinesImList[currentFrame], _imageKnobRect);
                }
            }
            else
            {
                // knob (pulled)
                if (_imageKnobPulledIm != null)
                {
                    drawingContext.DrawImage(_imageKnobPulledIm, _imageKnobRect);
                }

                if (!(_imageLinesPulledImList == null || currentFrame > _imageLinesPulledImList.Count))
                {

                    if (_imageLinesPulledImList.Count > 0 && _imageLinesPulledImList.Count > currentFrame
                        && _imageLinesPulledImList[currentFrame] != null)
                    {
                        drawingContext.DrawImage(
                        _imageLinesPulledImList[currentFrame], _imageKnobRect);
                    }
                }
            }
            // knob front face drawing
            if (_imageFrontImage != null)
            {
                var distX = !rotary.PullReleaseKnob ? 0 : rotary.PullDistanceX;
                var distY = !rotary.PullReleaseKnob ? 0 : rotary.PullDistanceY;

                var frontLeft = rotary.FrontLeft;
                var frontTop = rotary.FrontTop;

                int drawWidth = Convert.ToInt32(_imageFrontImage.Width * rotary.FrontRatio) + 2;  // rotate margin
                int drawHeight = Convert.ToInt32(_imageFrontImage.Height * rotary.FrontRatio) + 2;
                double adjustcenterX = rotary.AdjustCenterX;
                double adjustcenterY = rotary.AdjustCenterY;

                var _imBlush = MakeFrontRoundBursh(_imageFrontImage, currentAngle,
                    drawWidth, drawHeight, adjustcenterX, adjustcenterY);

                drawingContext.PushTransform(new TranslateTransform(frontLeft + distX, frontTop + distY));
                drawingContext.DrawRectangle(_imBlush, null, new Rect(0,0, drawWidth, drawHeight));
            }

            if (rotary.PullReady && _imagePullReadyImage != null)
            {
                int pullReadyWidth = Convert.ToInt32(_imagePullReadyImage.Width);
                int pullReadyHeight = Convert.ToInt32(_imagePullReadyImage.Height);
                drawingContext.DrawImage(_imagePullReadyImage, new Rect(-rotary.Width * 0.5, -rotary.Height * 0.5, pullReadyWidth, pullReadyHeight));
            }

        }
        protected override void OnRefresh()
        {
            GeneralPurposeKnobBase  rotary = Visual as GeneralPurposeKnobBase;

            if (rotary == null || !rotary.IsRenderReady) {
                return;
            }
            _imageKnobRect.Width = rotary.Width;
            _imageKnobRect.Height = rotary.Height;
            _imageLinesImList = new List<ImageSource>();
            _imageLinesPulledImList = new List<ImageSource>();
            _errPathDic = new Dictionary<string, string>();


            // error clear
            rotary.ErrorMessage = String.Empty;

            _imageKnobIm = null;
            _imageKnobPulledIm = null;
            isLoadImage = true;
            // imagePath loading
            try
            {
                _imageKnobIm = LoadImageFile(rotary.KnobImage);
                _imageKnobPulledIm = LoadImageFile(rotary.KnobImagePulled);
                _imageFrontImage = LoadImageFile(rotary.FrontImage);
                _imagePullReadyImage = LoadImageFile(rotary.PullReadyImage);

                if (_imageFrontImage != null)
                {
                    _frontCenter.X = _imageFrontImage.Width * 0.5 / 2;
                    _frontCenter.Y = _imageFrontImage.Height * 0.5 / 2;
                }
            }
            catch
            {
                rotary.ErrorMessage =
                    _errPathDic.Select(x => $"load error:{x.Key}\r\n").ToString();
                return;
            }


            // Lines loading
            foreach (var position in rotary.Positions)
            {
                try
                {
                    var _image = LoadImageFile(position.KnobRotateImage);
                    if (_image != null)
                    {
                        _imageLinesImList.Add(_image);
                    }
                    var _imageRotate = LoadImageFile(position.KnobPulledbRotateImage);
                    if (_imageRotate != null)
                    {
                        _imageLinesPulledImList.Add(_imageRotate);
                    }
                }
                catch
                {
                    isLoadImage = false;
                }
            }
            if (!isLoadImage)
            {
                rotary.ErrorMessage =
                    _errPathDic.Select(x => $"load error:{x.Key}\r\n").ToString();
                return;
            }
        }

        private ImageSource LoadImageFile(string path)
        {
            ImageSource result = null;
            if (String.IsNullOrEmpty(path))
            {
                return result;
            }
            result = _imgLoader.LoadImage(path,
                LoadImageOptions.SuppressMissingImageMessages);


            if (result == null)
            {
                if (!_errPathDic.ContainsKey(path))
                {
                    _errPathDic.Add(path, path);
                }
                isLoadImage &= false;
            }

            return result;
        }

        protected ImageSource RotateCenterImage(ImageSource im, double angle, Point center, Rotary rotary)
        {
            DrawingVisual drawingVisual = new DrawingVisual();
            DrawingContext drawingContextWk = drawingVisual.RenderOpen();

            drawingContextWk.PushTransform(
                new RotateTransform(angle, center.X, center.Y));

            int drawWidth = Convert.ToInt32(_center.X * 2);
            int drawHeight = Convert.ToInt32(_center.Y * 2);
            int bmpWidth = Convert.ToInt32(rotary.Width);
            int bmpHeight = Convert.ToInt32(rotary.Height);

            var bitmap = CreateTargetBitmap(drawingContextWk, im,
                new Size(drawWidth, drawHeight),
                new Size(bmpWidth, bmpHeight));

            bitmap.Render(drawingVisual);
            return bitmap;
        }
        protected ImageSource TranslateImage(ImageSource im, double left, double top, Rotary rotary)
        {
            DrawingVisual drawingVisual = new DrawingVisual();
            DrawingContext drawingContextWk = drawingVisual.RenderOpen();

            drawingContextWk.PushTransform(
                new TranslateTransform(left, top));

            int drawWidth  = Convert.ToInt32(rotary.Width);
            int drawHeight = Convert.ToInt32(rotary.Height);

            var bitmap = CreateTargetBitmap(drawingContextWk, im,
                new Size(drawWidth, drawHeight),
                new Size(drawWidth, drawHeight));

            bitmap.Render(drawingVisual);
            return bitmap;
        }

        protected RenderTargetBitmap CreateTargetBitmap(DrawingContext drawingContext,
            ImageSource imageSrc, Size size, Size bmpSize)
        {
            drawingContext.DrawImage(imageSrc,
                    new Rect(0, 0, size.Width, size.Height));
            drawingContext.Close();
            RenderTargetBitmap wkBmp = new RenderTargetBitmap(
                Convert.ToInt32(bmpSize.Width),
                Convert.ToInt32(bmpSize.Height),
                101, 101, PixelFormats.Default);
            return wkBmp;
        }

        protected ImageBrush MakeFrontRoundBursh(ImageSource im, double angle,
            int drawWidth, int drawHeight, double centerAdjustX, double ceneterAdjustY)
        {

            DrawingVisual drawingVisual = new DrawingVisual();
            DrawingContext drawingContextWk = drawingVisual.RenderOpen();

            drawingContextWk.PushTransform(
                new RotateTransform(angle, _frontCenter.X + centerAdjustX, _frontCenter.Y + ceneterAdjustY));

            var bitmap = CreateTargetBitmap(drawingContextWk, im,
                new Size(drawWidth, drawHeight),
                new Size(drawWidth, drawHeight));

            bitmap.Render(drawingVisual);

            return new ImageBrush(bitmap);
            
        }
    }
}
