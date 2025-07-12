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

    [HeliosControl("Helios.F5E.DragChutePanel", "DragChutePanel", "F-5E", typeof(BackgroundImageRenderer))]
    class DragChutePanel : F5EDevice
    {
        private static readonly Rect SCREEN_RECT = new Rect(0, 0, 246, 187);
        private string _interfaceDeviceName = "GearPanel";
        private string _imageAssetLocation = "{F-5E}/Images";
        private Rect _scaledScreenRect = SCREEN_RECT;


        public DragChutePanel()
            : base("DragChutePanel", new Size(246, 187))
        {

            List<Tuple<int, string, int, int, int>> flipPositions = new List<Tuple<int, string, int, int, int>>
            {
                new Tuple<int, string, int, int, int>(0, "0", 1, 1, 100),
                new Tuple<int, string, int, int, int>(1, "1", 2, 10, 100),
                new Tuple<int, string, int, int, int>(2, "2", 11, 14, 100),
                new Tuple<int, string, int, int, int>(3, "3", 14, 15, 100),
            };

            AddFlipLeverToPanel("DragChute", new Point(0, 0d), new Size(246, 187),
                "DragChute", "DragChute", $"{_imageAssetLocation}/DragChute/DragChute_01.png",
                flipPositions, false);
            AddIndicatorPushButton("DragExecButton", $"{_imageAssetLocation}/ClearButton/",
                "ClearButton", new Point(150d, 50d), new Size(50, 50));
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
