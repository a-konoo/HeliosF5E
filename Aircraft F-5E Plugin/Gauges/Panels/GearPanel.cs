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
    using GadrocsWorkshop.Helios.ComponentModel;
    using GadrocsWorkshop.Helios.Controls;
    using System.Globalization;
    using System.Windows;
    using System.Xml;
    using System.Collections.Generic;
    using System;
    using GadrocsWorkshop.Helios.Controls.F5E;
    using System.Windows.Media;

    [HeliosControl("Helios.F5E.GearPanel", "GearPanel", "F-5E", typeof(BackgroundImageRenderer))]
    class GearPanel : F5EDevice
    {
        private static readonly Rect SCREEN_RECT = new Rect(0, 0, 240, 360);
        private string _interfaceDeviceName = "GearPanel";
        private string _imageAssetLocation = "{F-5E}/Images/GearPanel";
        private Rect _scaledScreenRect = SCREEN_RECT;


        public GearPanel()
            : base("GearPanel", new Size(240, 360))
        {

            Children.Add(AddImage($"/GearPanel.png", new Point(0d, 0d), 240, 360));

            AddIndicatorPushButton("Gear GreenLight Left", $"{_imageAssetLocation}/",
                "GearGreenLight", new Point(53d, 86d), new Size(80, 80));

            AddIndicatorPushButton("Gear GreenLight Center", $"{_imageAssetLocation}/",
                "GearGreenLight", new Point(119d, 62d), new Size(80, 80));

            AddIndicatorPushButton("Gear GreenLight Right", $"{_imageAssetLocation}/",
                "GearGreenLight", new Point(188d, 86d), new Size(80, 80));
            


            AddButton("WarnSilence", new Point(15d, 209d), new Size(68, 68),
                $"{_imageAssetLocation}/WarnSilence_NORMAL.png",
                $"{_imageAssetLocation}/WarnSilence_PUSHED.png",
                "", "WarnSilence", "WarnSilence",false);

            AddButton("DownLock", new Point(156d, 215d), new Size(68, 68),
                 $"{_imageAssetLocation}/DownLock_NORMAL.png",
                 $"{_imageAssetLocation}/DownLock_PUSHED.png",
                 "", "DownLock", "DownLock", false);

            List<Tuple<int, string, int, int, int>> flipPositions = new List<Tuple<int, string, int, int, int>>
            {
                new Tuple<int, string, int, int, int>(0, "0", 1, 1, 100),
                new Tuple<int, string, int, int, int>(1, "1", 2, 8, 100),
                new Tuple<int, string, int, int, int>(2, "2", 9, 13, 100),
            };

            AddFlipLeverToPanel("GearLever", new Point(92d, 30d), new Size(56, 307),
                "GearLever", "GearLever", $"{_imageAssetLocation}/GearLever/GearLever_01.png",
                flipPositions, false);

            AddPushButton("UpLeverInput", $"{_imageAssetLocation}/../ClearButton/",
                "ClearButton", new Point(99d, 280d), new Size(30, 30));
            AddPushButton("DownLeverInput", $"{_imageAssetLocation}/../ClearButton/",
                "ClearButton", new Point(99d, 140d), new Size(30, 30));
        }

        #region Properties

        public override string DefaultBackgroundImage
        {
            get { return $""; }
        }

        public string ImageAssetLocation
        {
            get => _imageAssetLocation;
            set
            {
                if (value != null && !_imageAssetLocation.Equals(value))
                {
                    string oldValue = _imageAssetLocation;
                    _imageAssetLocation = value;
                    OnPropertyChanged("ImageAssetLocation", oldValue, value, false);
                    Refresh();
                }
            }
        }

        #endregion

        protected override void OnPropertyChanged(PropertyNotificationEventArgs args)
        {
            if (args.PropertyName.Equals("Width") || args.PropertyName.Equals("Height"))
            {
                double scaleX = Width / NativeSize.Width;
                double scaleY = Height / NativeSize.Height;
                _scaledScreenRect.Scale(scaleX, scaleY);
            }
            base.OnPropertyChanged(args);
        }


        private FlipLever AddFlipLeverToPanel(string name,
            Point posn, Size size,
            string _interfaceDeviceName,
            string _interfaceElementName,
            string animationPatern,
            List<Tuple<int, string, int, int, int>> leverPostions,
            bool fromCenter)
        {
            FlipLever flipLever = AddFlipLever(
                name: name,
                posn: posn,
                size: size,
                interfaceDeviceName: _interfaceDeviceName,
                interfaceElementName: _interfaceElementName,
                animationPattern: animationPatern,
                leverPositions: leverPostions,
                fromCenter: fromCenter);

            return flipLever;
        }

        private void AddIndicatorPushButton(string name, string imageBasePath, string imageFileName, Point pos, Size size)
        {
            AddIndicatorPushButton(name: name,
                pos: pos,
                size: size,
                image: imageBasePath + imageFileName + "_OFF.png",
                pushedImage: imageBasePath + imageFileName + "_PUSHED.png",
                textColor: Color.FromArgb(0xff, 0x7e, 0xde, 0x72),
                onTextColor: Color.FromArgb(0xff, 0x7e, 0xde, 0x72),
                font: "",
                onImage: imageBasePath + imageFileName + "_ON.png",
                interfaceDeviceName: _interfaceDeviceName,
                interfaceElementName: name,
                fromCenter: true,
                withText: false);
        }

        private void AddPushButton(string name, string imageBasePath, string imageFileName,
            Point posn, Size size)
        {
            AddButton(name: name,
                posn: posn,
                size: size,
                image: imageBasePath + imageFileName + "_OFF.png",
                pushedImage: imageBasePath + imageFileName + "_PUSHED.png",
                buttonText: "",
                interfaceDeviceName: _interfaceDeviceName,
                interfaceElementName: name,
                fromCenter: false);
        }

        private ImageDecoration AddImage(string name, Point posn, int width, int height)
        {
            return (new ImageDecoration()
            {
                Name = name,
                Image = $"{_imageAssetLocation}{name}",
                Alignment = ImageAlignment.Stretched,
                Left = posn.X,
                Top = posn.Y,
                Width = width,
                Height = height,
                IsHidden = false
            });

        }

        public override void ReadXml(XmlReader reader)
        {
            base.ReadXml(reader);
            if (reader.Name.Equals("ImageAssetLocation"))
            {
                ImageAssetLocation = reader.ReadElementString("ImageAssetLocation");
            }
        }
        public override void WriteXml(XmlWriter writer)
        {
            base.WriteXml(writer);
            writer.WriteElementString("ImageAssetLocation", _imageAssetLocation.ToString(CultureInfo.InvariantCulture));
        }
        public override bool HitTest(Point location)
        {
            if (_scaledScreenRect.Contains(location))
            {
                return false;
            }

            return true;
        }

        public override void MouseDown(Point location)
        {
            // No-Op
        }

        public override void MouseDrag(Point location)
        {
            // No-Op
        }

        public override void MouseUp(Point location)
        {
            // No-Op
        }
    }
}
