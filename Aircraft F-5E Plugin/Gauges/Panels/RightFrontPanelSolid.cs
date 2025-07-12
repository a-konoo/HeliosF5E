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


     [HeliosControl("Helios.F5E.RightFrontPanelSolid", "RightFrontPanelSolid", "F-5E", typeof(BackgroundImageRenderer))]
    class RightFrontPanelSolid : F5EDevice
    {
        private static readonly Rect SCREEN_RECT = new Rect(0, 0, 755, 690);
        private string _interfaceDeviceName = "RightFrontPanelSolid";
        private string _imageAssetLocation = "{F-5E}/Images/RightSidePanel";
        private Rect _scaledScreenRect = SCREEN_RECT;


        public RightFrontPanelSolid()
            : base("RightFrontPanelSolid", new Size(755, 690))
        {

            AddLiquidOxygenPressureSolidPanel("OxygenPressureSolid", "OxygenPressureSolid",
                new Point(277d, 226d), new Size(140d, 129d * 0.66d));

            AddO2BlinkerSolidPanel("O2BlinkerSolid", "O2BlinkerSolid", new Point(120d, 250d),
                new Size(100d, 88d), new Point(220d, 0d), 1, 150, 30d);

            Children.Add(AddImage($"/Panels/F5E_RIGHT_FRONT_SOLID.png", new Point(0d, 0d), 755, 690));

            AddLiquidOxygenQtySolidPanel("OxygenQtySolid", "OxygenQtySolid",
                new Point(235d, 26d), new Size(134d * 0.8d, 134d));

            AddIndicatorPanel("LGenSolid", $"{_imageAssetLocation}/AlertPanelSolid/",
                "LGEN", new Point(311d, 437d), new Size(124, 62));
            AddIndicatorPanel("CanopySolid", $"{_imageAssetLocation}/AlertPanelSolid/",
                "CANOPY", new Point(411d, 393d), new Size(124, 62));
            AddIndicatorPanel("RGenSolid", $"{_imageAssetLocation}/AlertPanelSolid/",
                "RGEN", new Point(502d, 350d), new Size(124, 62));

            AddIndicatorPanel("UTILSolid", $"{_imageAssetLocation}/AlertPanelSolid/",
                "UTIL", new Point(313d+16d, 439d+17d), new Size(124, 62));
            AddIndicatorPanel("SPARESolid", $"{_imageAssetLocation}/AlertPanelSolid/",
                "SPARE", new Point(413d + 16d, 392d + 17d), new Size(124, 62));
            AddIndicatorPanel("FLIHYDSolid", $"{_imageAssetLocation}/AlertPanelSolid/",
                "FLIHYD", new Point(509d + 16d, 348d + 17d), new Size(124, 62));

            AddIndicatorPanel("EXTTANKSSolid", $"{_imageAssetLocation}/AlertPanelSolid/",
                "EXTTANKS", new Point(313d + 32d, 434d + 34d), new Size(140, 70));
            AddIndicatorPanel("IFFSolid", $"{_imageAssetLocation}/AlertPanelSolid/",
                "IFF", new Point(418d + 32d, 395d + 34d), new Size(124, 62));
            AddIndicatorPanel("OXYGENSolid", $"{_imageAssetLocation}/AlertPanelSolid/",
                "OXYGEN", new Point(514d + 32d, 348d + 34d), new Size(124, 62));

            AddIndicatorPanel("LFUELSolid", $"{_imageAssetLocation}/AlertPanelSolid/",
                "LFUEL", new Point(315d + 48d, 445d + 51d), new Size(122, 62));
            AddIndicatorPanel("ANTIICESolid", $"{_imageAssetLocation}/AlertPanelSolid/",
                "ANTIICE", new Point(401d + 48d, 391d + 51d), new Size(140, 76));
            AddIndicatorPanel("RFUELSolid", $"{_imageAssetLocation}/AlertPanelSolid/",
                "RFUEL", new Point(514d + 48d, 353d + 51d), new Size(126, 62));

            AddIndicatorPanel("LFUELPRSSolid", $"{_imageAssetLocation}/AlertPanelSolid/",
                "LFUELPRS", new Point(319d + 64d, 450d + 68d), new Size(122, 62));
            AddIndicatorPanel("INSSolid", $"{_imageAssetLocation}/AlertPanelSolid/",
                "INS", new Point(418d + 64d, 400d + 68d), new Size(124, 62));
            AddIndicatorPanel("RFUELPRSSolid", $"{_imageAssetLocation}/AlertPanelSolid/",
                "RFUELPRS", new Point(517d + 64d, 354d + 68d), new Size(126, 62));

            AddIndicatorPanel("AOASolid", $"{_imageAssetLocation}/AlertPanelSolid/",
                "AOA", new Point(317d + 80d, 453d + 85d), new Size(122, 62));
            AddIndicatorPanel("ADCSolid", $"{_imageAssetLocation}/AlertPanelSolid/",
                "ADC", new Point(418d + 80d, 395d + 85d), new Size(140, 72));
            AddIndicatorPanel("DIRSolid", $"{_imageAssetLocation}/AlertPanelSolid/",
                "DIR", new Point(514d + 80d, 358d + 85d), new Size(126, 62));

            AddIndicatorPanel("SPARESolid2", $"{_imageAssetLocation}/AlertPanelSolid/",
                "SPARE", new Point(314d + 96d, 449d + 102d), new Size(140, 70));
            AddIndicatorPanel("DCLOADSolid", $"{_imageAssetLocation}/AlertPanelSolid/",
                "DCLOAD", new Point(428d + 96d, 402d + 102d), new Size(124, 62));
            AddIndicatorPanel("SPARESolid3", $"{_imageAssetLocation}/AlertPanelSolid/",
                "SPARE", new Point(514d + 96d, 352d + 102d), new Size(140, 70));

            Add2PosnToggle(
                name: "LeftGeneratorToggleSolid",
                posn: new Point(85d, 153d),
                size: new Size(65, 90),
                imagePath: $"{_imageAssetLocation}/RightFrontBlackBigToggle/",
                imageBaseName: "RightFrontBlackToggle",
                interfaceDevice: _interfaceDeviceName,
                toggleType:ToggleSwitchType.OnOn,
                interfaceElement: "LeftGeneratorToggleSolid",
                fromCenter: false);
            Add2PosnToggle(
                name: "RightGeneratorToggleSolid",
                posn: new Point(150d, 130d),
                size: new Size(65, 90),
                imagePath: $"{_imageAssetLocation}/RightFrontBlackBigToggle/",
                imageBaseName: "RightFrontBlackToggle",
                interfaceDevice: _interfaceDeviceName,
                toggleType: ToggleSwitchType.OnOn,
                interfaceElement: "RightGeneratorToggleSolid",
                fromCenter: false);
            Add2PosnToggle(
                name: "BatterySwitchSolid",
                posn: new Point(5d, 12d),
                size: new Size(54, 90),
                imagePath: $"{_imageAssetLocation}/RightFrontRedToggle/",
                imageBaseName: "RightFrontRedToggle",
                interfaceDevice: _interfaceDeviceName,
                toggleType: ToggleSwitchType.OnOn,
                interfaceElement: "BatterySwitchSolid",
                fromCenter: false);

            Add3PosnOxyLever(
                name: "EmergencyLeverSolid",
                posn: new Point(213d, 333d),
                size: new Size(142d, 102d),
                imagePath: $"{_imageAssetLocation}/OxyLeverSolid/",
                imageBaseName: "OxyLeversRed",
                interfaceDevice: _interfaceDeviceName,
                interfaceElement: "EmergencyLeverSolid",
                toggleType: ThreeWayToggleSwitchType.OnOnMom,
                fromCenter: false);

            Add2PosnOxyLever(
                name: "DiluterLeverSolid",
                posn: new Point(301d, 293d),
                size: new Size(133d, 96d),
                imagePath: $"{_imageAssetLocation}/OxyLeverSolid/",
                imageBaseName: "OxyLeversWhite",
                interfaceDevice: _interfaceDeviceName,
                toggleType: ToggleSwitchType.OnOn,
                interfaceElement: "DiluterLeverSolid",
                fromCenter: false);

            Add2PosnOxyLever(
                name: "OxygenSupplyLeverSolid",
                posn: new Point(400d, 253d),
                size: new Size(129d, 94d),
                imagePath: $"{_imageAssetLocation}/OxyLeverSolid/",
                imageBaseName: "OxyLeversGreen",
                interfaceDevice: _interfaceDeviceName,
                toggleType: ToggleSwitchType.OnOn,
                interfaceElement: "OxygenSupplyLeverSolid",
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

        private LiquidOxygenPressureSolid AddLiquidOxygenPressureSolidPanel(string name, string actionIdentifier,
            Point posn, Size size)
        {
            LiquidOxygenPressureSolid pressureGauge = AddLiquidOxygenPressureSolid(
                name: name,
                fromCenter: false,
                interfaceDeviceName: _interfaceDeviceName,
                interfaceElementName: actionIdentifier,
                actionIdentifier: actionIdentifier,
                posn: posn,
                size: size);

            return pressureGauge;
        }

        private LiquidOxygenQtySolid AddLiquidOxygenQtySolidPanel(string name, string actionIdentifier,
            Point posn, Size size)
        {
            LiquidOxygenQtySolid qtyGauge = AddLiquidOxygenQtySolid(
                name: name,
                fromCenter: false,
                interfaceDeviceName: _interfaceDeviceName,
                interfaceElementName: actionIdentifier,
                actionIdentifier: actionIdentifier,
                posn: posn,
                size: size);

            return qtyGauge;
        }

        private O2Blinker AddO2BlinkerSolidPanel(string name, string actionIdentifier,
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
                positionOneImage: imagePath + imageBaseName + "_03.png",
                positionTwoImage: imagePath + imageBaseName + "_02.png",
                positionThreeImage: imagePath + imageBaseName + "_01.png",
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
                positionOneImage: imagePath + imageBaseName + "_03.png",
                positionTwoImage: imagePath + imageBaseName + "_01.png",
                defaultType: toggleType,
                interfaceDeviceName: interfaceDevice,
                interfaceElementName: interfaceElement,
                fromCenter: false);
        }

        private void Add2PosnToggle(string name, Point posn, Size size, string imagePath,
            string imageBaseName, string interfaceDevice, string interfaceElement, 
            ToggleSwitchType toggleType,bool fromCenter)
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
                offImage: imageBasePath + "AlertSolidEmpty.png",
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
