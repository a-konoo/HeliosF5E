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
    using System.Windows.Media;


     [HeliosControl("Helios.F5E.RightFrontPanelTop", "RightFrontPanelTop", "F-5E", typeof(BackgroundImageRenderer))]
    class RightFrontPanelTop : F5EDevice
    {
        private static readonly Rect SCREEN_RECT = new Rect(0, 0, 550, 816);
        private string _interfaceDeviceName = "RightFrontPanelTop";
        private string _imageAssetLocation = "{F-5E}/Images/RightSidePanel";
        private Rect _scaledScreenRect = SCREEN_RECT;


        public RightFrontPanelTop()
            : base("RightFrontPanelTop", new Size(550, 816))
        {

            AddLiquidOxygenPressurePanel("OxygenPressure", "OxygenPressure",
                new Point(259.5d, 94d), new Size(170d, 170d));

            AddO2BlinkerPanel("O2Blinker", "O2Blinker", new Point(60d, 80d),
                new Size(120d, 88d), new Point(260d, 0d), 1, 170, 0d);

            Children.Add(AddImage($"/Panels/F5E_RIGHT_FRONT_TOP.png", new Point(0d, 0d), 550, 816));

            AddIndicatorPanel("LGenTop", $"{_imageAssetLocation}/AlertPanel/",
                "LGEN", new Point(80d, 530d), new Size(120, 40));
            AddIndicatorPanel("CanopyTop", $"{_imageAssetLocation}/AlertPanel/",
                "CANOPY", new Point(195d, 530d), new Size(150, 40));
            AddIndicatorPanel("RGenTop", $"{_imageAssetLocation}/AlertPanel/",
                "RGEN", new Point(355d, 530d), new Size(120, 40));

            AddIndicatorPanel("UTILTop", $"{_imageAssetLocation}/AlertPanel/",
                "UTIL", new Point(80d, 570d), new Size(120, 40));
            AddIndicatorPanel("SPARETop", $"{_imageAssetLocation}/AlertPanel/",
                "SPARE", new Point(195d, 570d), new Size(150, 40));
            AddIndicatorPanel("FLIHYDTop", $"{_imageAssetLocation}/AlertPanel/",
                "FLIHYD", new Point(355d, 570d), new Size(120, 40));

            AddIndicatorPanel("EXTTANKSTop", $"{_imageAssetLocation}/AlertPanel/",
                "EXTTANKS", new Point(80d, 610d), new Size(120, 40));
            AddIndicatorPanel("IFFTop", $"{_imageAssetLocation}/AlertPanel/",
                "IFF", new Point(195d, 610d), new Size(150, 40));
            AddIndicatorPanel("OXYGENTop", $"{_imageAssetLocation}/AlertPanel/",
                "OXYGEN", new Point(340d, 605d), new Size(150, 40));

            AddIndicatorPanel("LFUELTop", $"{_imageAssetLocation}/AlertPanel/",
                "LFUELLOW", new Point(80d, 650d), new Size(120, 40));
            AddIndicatorPanel("ANTIICETop", $"{_imageAssetLocation}/AlertPanel/",
                "ANTIICE", new Point(200d, 650d), new Size(150, 40));
            AddIndicatorPanel("RFUELTop", $"{_imageAssetLocation}/AlertPanel/",
                "RFUELLOW", new Point(355d, 650d), new Size(120, 40));

            AddIndicatorPanel("LFUELPRSTop", $"{_imageAssetLocation}/AlertPanel/",
                "LFUELPRS", new Point(80d, 685d), new Size(120, 40));
            AddIndicatorPanel("INSTop", $"{_imageAssetLocation}/AlertPanel/",
                "INS", new Point(215d, 690d), new Size(120, 40));
            AddIndicatorPanel("RFUELPRSTop", $"{_imageAssetLocation}/AlertPanel/",
                "RFUELPRS", new Point(355d, 686d), new Size(120, 40));

            AddIndicatorPanel("AOATop", $"{_imageAssetLocation}/AlertPanel/",
                "AOA", new Point(80d, 725d), new Size(120, 40));
            AddIndicatorPanel("ADCTop", $"{_imageAssetLocation}/AlertPanel/",
                "ADAC", new Point(200d, 730d), new Size(140, 40));
            AddIndicatorPanel("DIRTop", $"{_imageAssetLocation}/AlertPanel/",
                "DIRGYRO", new Point(355d, 725d), new Size(120, 40));

            AddIndicatorPanel("SPARE2Top", $"{_imageAssetLocation}/AlertPanel/",
                "SPARE", new Point(80d, 765d), new Size(120, 40));
            AddIndicatorPanel("DCLOADTop", $"{_imageAssetLocation}/AlertPanel/",
                "DCOVER", new Point(210d, 765d), new Size(110, 40));
            AddIndicatorPanel("SPARE3Top", $"{_imageAssetLocation}/AlertPanel/",
                "SPARE", new Point(355d, 765d), new Size(120, 40));

            Add3PosnOxyLever(
                name: "EmergencyLeverTop",
                posn: new Point(123d, 303d),
                size: new Size(30d, 132d),
                imagePath: $"{_imageAssetLocation}/OxyLeverTop/",
                imageBaseName: "OxyLeversRed",
                interfaceDevice: _interfaceDeviceName,
                interfaceElement: "EmergencyLeverSolid",
                toggleType: ThreeWayToggleSwitchType.OnOnMom,
                fromCenter: false);

            Add2PosnOxyLever(
                name: "DiluterLeverTop",
                posn: new Point(247d, 303d),
                size: new Size(30d, 132d),
                imagePath: $"{_imageAssetLocation}/OxyLeverTop/",
                imageBaseName: "OxyLeversWhite",
                interfaceDevice: _interfaceDeviceName,
                toggleType: ToggleSwitchType.OnOn,
                interfaceElement: "DiluterLeverTop",
                fromCenter: false);

            Add2PosnOxyLever(
                name: "OxygenSupplyLeverTop",
                posn: new Point(385d, 303d),
                size: new Size(30d, 132d),
                imagePath: $"{_imageAssetLocation}/OxyLeverTop/",
                imageBaseName: "OxyLeversGreen",
                interfaceDevice: _interfaceDeviceName,
                toggleType: ToggleSwitchType.OnOn,
                interfaceElement: "OxygenSupplyLeverTop",
                fromCenter: false);
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

        private LiquidOxygenPressure AddLiquidOxygenPressurePanel(string name, string actionIdentifier,
            Point posn, Size size)
        {
            LiquidOxygenPressure pressureGauge = AddLiquidOxygenPressure(
                name: name,
                fromCenter: false,
                interfaceDeviceName: _interfaceDeviceName,
                interfaceElementName: actionIdentifier,
                actionIdentifier: actionIdentifier,
                posn: posn,
                size: size);

            return pressureGauge;
        }

        private O2Blinker AddO2BlinkerPanel(string name, string actionIdentifier,
            Point posn, Size size, Point center, int calibMin, int calibMax, double angle)
        {
            O2Blinker blinker = AddO2Blinker(
                name: name,
                fromCenter: false,
                interfaceDeviceName: _interfaceDeviceName,
                interfaceElementName: actionIdentifier,
                actionIdentifier: actionIdentifier,
                posn: posn,
                size: size,
                calibMin: calibMin,
                calibMax: calibMax,
                center: center,
                angle: angle);

            return blinker;
        }

        private void Add3PosnOxyLever(string name, Point posn, Size size,string imagePath, 
            ThreeWayToggleSwitchType toggleType,
            string imageBaseName, string interfaceDevice, string interfaceElement, bool fromCenter)
        {
            AddThreeWayToggle(
                name: name,
            pos: posn,
                size: size,
                positionOneImage: imagePath + imageBaseName + "_01.png",
                positionTwoImage: imagePath + imageBaseName + "_02.png",
                positionThreeImage: imagePath + imageBaseName + "_03.png",
                defaultPosition: ThreeWayToggleSwitchPosition.Two,
                switchType: toggleType,
                interfaceDeviceName: interfaceDevice,
                interfaceElementName: interfaceElement,
                horizontal: false,
                horizontalRender: false,
                clickType: LinearClickType.Touch,
                fromCenter: true
                );
        }
        private void Add2PosnOxyLever(string name, Point posn, Size size, string imagePath,
            string imageBaseName, string interfaceDevice, string interfaceElement,
            ToggleSwitchType toggleType, bool fromCenter)
        {
            AddToggleSwitch(
                name: name,
                posn: posn,
                size: size,
                defaultPosition: ToggleSwitchPosition.Two,
                positionOneImage: imagePath + imageBaseName + "_01.png",
                positionTwoImage: imagePath + imageBaseName + "_03.png",
                defaultType: toggleType,
                interfaceDeviceName: interfaceDevice,
                interfaceElementName: interfaceElement,
                fromCenter: false);
        }

        private void AddIndicatorPanel(string name, string imageBasePath, string indicatorName,
            Point pos, Size size)
        {
            AddIndicator(name: name,
                posn: pos,
                size: size,
                interfaceDeviceName: name,
                interfaceElementName: name,
                fromCenter:false,
                font: "",
                offImage: imageBasePath + "AlertEmpty.png",
                onImage: imageBasePath + indicatorName + "_on.png",
                offTextColor: Color.FromArgb(0 ,0, 0, 0),
                onTextColor: Color.FromArgb(0, 0, 0, 0),
                vertical: false,
                withText: false);
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
