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
    using GadrocsWorkshop.Helios.Controls.F5E;
    using System.Collections.Generic;
    using System;
    using System.Globalization;
    using System.Windows;
    using System.Xml;
    using System.Windows.Media;


    [HeliosControl("Helios.F5E.RightRearPanelSolid", "RightRearPanelSolid", "F-5E", typeof(BackgroundImageRenderer))]
    class RightRearPanelSolid : F5EDevice
    {
        private static readonly Rect SCREEN_RECT = new Rect(0, 0, 950, 730);
        private string _interfaceDeviceName = "RightRearPanelSolid";
        private string _imageAssetLocation = "{F-5E}/Images/RightSidePanel";
        private Rect _scaledScreenRect = SCREEN_RECT;
        private static readonly NLog.Logger Logger = NLog.LogManager.GetCurrentClassLogger();


        public RightRearPanelSolid()
            : base("RightRearPanelSolid", new Size(950, 730))
        {

            Children.Add(AddImage($"/Panels/F5E_RIGHT_REAR_SOLID.png", new Point(0d, 0d), 950, 730));

            List<Tuple<int, bool, double, double, string, string>> knobPostions = new List<Tuple<int, bool, double, double, string, string>>
            {
                new Tuple<int, bool, double, double, string, string>(0, false, 0,   0d,   "IFFWheelSolid_01.png", "IFFWheelSolid_01.png"),
                new Tuple<int, bool, double, double, string, string>(1, false, 0.1,  30d, "IFFWheelSolid_02.png", "IFFWheelSolid_02.png"),
                new Tuple<int, bool, double, double, string, string>(2, false, 0.2,  60d, "IFFWheelSolid_03.png", "IFFWheelSolid_03.png"),
                new Tuple<int, bool, double, double, string, string>(3, false, 0.3,  90d, "IFFWheelSolid_04.png", "IFFWheelSolid_04.png"),
                new Tuple<int, bool, double, double, string, string>(4, false, 0.4, 120d, "IFFWheelSolid_05.png", "IFFWheelSolid_05.png"),
                new Tuple<int, bool, double, double, string, string>(5, false, 0.5, 150d, "IFFWheelSolid_06.png", "IFFWheelSolid_06.png"),
                new Tuple<int, bool, double, double, string, string>(6, false, 0.6, 180d, "IFFWheelSolid_07.png", "IFFWheelSolid_07.png"),
                new Tuple<int, bool, double, double, string, string>(7, false, 0.7, 210d, "IFFWheelSolid_08.png", "IFFWheelSolid_08.png")
            };
            List<Tuple<int, bool, double, double, string, string>> knobPostions2 = new List<Tuple<int, bool, double, double, string, string>>
            {
                new Tuple<int, bool, double, double, string, string>(0, false, 0,   0d, "IFFWheelSolid_01.png", "IFFWheelSolid_01.png"),
                new Tuple<int, bool, double, double, string, string>(1, false, 0.1,  30d, "IFFWheelSolid_02.png", "IFFWheelSolid_02.png"),
                new Tuple<int, bool, double, double, string, string>(2, false, 0.2,  60d, "IFFWheelSolid_03.png", "IFFWheelSolid_03.png"),
                new Tuple<int, bool, double, double, string, string>(3, false, 0.3,  90d, "IFFWheelSolid_04.png", "IFFWheelSolid_04.png"),
            };// 275d, 210d-->3,82
            AddImageFlipParts("IFF Selector 1", "IFF Selector1", new Point(280d, 292d), new Size(80,60),
                "",
                "",
                "",
                "",
                "",
                $"{_imageAssetLocation}/IFFWheelSolid/",
                knobPostions, 0, 0, false, false);
            AddImageFlipParts("IFF Selector 2", "IFF Selector2", new Point(316d, 277d), new Size(80, 60),
                "",
                "",
                "",
                "",
                "",
                $"{_imageAssetLocation}/IFFWheelSolid/",
                knobPostions2, 0, 0, false, false);
            AddImageFlipParts("IFF Selector 3", "IFF Selector3", new Point(362d, 256d), new Size(80, 60),
                "",
                "",
                "",
                "",
                "",
                $"{_imageAssetLocation}/IFFWheelSolid/",
                knobPostions, 0, 0, false, false);
            AddImageFlipParts("IFF Selector 4", "IFF Selector4", new Point(398d, 239d), new Size(80, 60),
                "",
                "",
                "",
                "",
                "",
                $"{_imageAssetLocation}/IFFWheelSolid/",
                knobPostions, 0, 0, false, false);
            AddImageFlipParts("IFF Selector 5", "IFF Selector5", new Point(434d, 225d), new Size(80, 60),
                "",
                "",
                "",
                "",
                "",
                $"{_imageAssetLocation}/IFFWheelSolid/",
                knobPostions, 0, 0, false, false);
            AddImageFlipParts("IFF Selector 6", "IFF Selector6", new Point(469d, 209d), new Size(80, 60),
                "",
                "",
                "",
                "",
                "",
                $"{_imageAssetLocation}/IFFWheelSolid/",
                knobPostions, 0, 0, false, false);

            AddPushButton("IFF GreenLight Left", $"{_imageAssetLocation}/IFFGreenLightSolid/",
                "F5E_GREEN_LIGHT", new Point(160d, 102d), new Size(55, 55));
            AddPushButton("IFF GreenLight Right", $"{_imageAssetLocation}/IFFGreenLightSolid/",
                "F5E_GREEN_LIGHT", new Point(236d, 68d), new Size(55, 55));
            AddPushButton("IFF Wheel1Up", $"{_imageAssetLocation}/../ClearButton/",
                "ClearButton", new Point(295d, 300d), new Size(20, 20));
            AddPushButton("IFF Wheel1Down", $"{_imageAssetLocation}/../ClearButton/",
                "ClearButton", new Point(340d, 332d), new Size(20, 20));
            AddPushButton("IFF Wheel2Up", $"{_imageAssetLocation}/../ClearButton/",
                "ClearButton", new Point(331d, 285d), new Size(20, 20));
            AddPushButton("IFF Wheel2Down", $"{_imageAssetLocation}/../ClearButton/",
                "ClearButton", new Point(376d, 317d), new Size(20, 20));
            AddPushButton("IFF Wheel3Up", $"{_imageAssetLocation}/../ClearButton/",
                "ClearButton", new Point(377d, 264d), new Size(20, 20));
            AddPushButton("IFF Wheel3Down", $"{_imageAssetLocation}/../ClearButton/",
                "ClearButton", new Point(415d, 296d), new Size(20, 20));
            AddPushButton("IFF Wheel4Up", $"{_imageAssetLocation}/../ClearButton/",
                "ClearButton", new Point(413d, 247d), new Size(20, 20));
            AddPushButton("IFF Wheel4Down", $"{_imageAssetLocation}/../ClearButton/",
                "ClearButton", new Point(459d, 279d), new Size(20, 20));
            AddPushButton("IFF Wheel5Up", $"{_imageAssetLocation}/../ClearButton/",
                "ClearButton", new Point(449d, 233d), new Size(20, 20));
            AddPushButton("IFF Wheel5Down", $"{_imageAssetLocation}/../ClearButton/",
                "ClearButton", new Point(494d, 265d), new Size(20, 20));
            AddPushButton("IFF Wheel6Up", $"{_imageAssetLocation}/../ClearButton/",
                "ClearButton", new Point(484d, 217d), new Size(20, 20));
            AddPushButton("IFF Wheel6Down", $"{_imageAssetLocation}/../ClearButton/",
                "ClearButton", new Point(529d, 249d), new Size(20, 20));

            Add3PosnToggle(
                name: "IFF MODE 4 Monitor Toggle",
                posn: new Point(141d, 205d),
                size: new Size(60, 70),
                imagePath: $"{_imageAssetLocation}/IFFLeverBlackSolid/",
                imageBaseName: "F5E_RIGHT_SOLID_LEVER_BL",
                interfaceDevice: _interfaceDeviceName,
                interfaceElement: "IFF MODE 4 Monitor Cntrl",
                toggleType: ThreeWayToggleSwitchType.OnOnOn,
                fromCenter: false);
            Add3PosnToggle(
                name: "IFF Mode(M-1)",
                posn: new Point(205d, 180d),
                size: new Size(60, 70),
                imagePath: $"{_imageAssetLocation}/IFFLeverBlackSolid/",
                imageBaseName: "F5E_RIGHT_SOLID_LEVER_BL",
                interfaceDevice: _interfaceDeviceName,
                interfaceElement: "IFF M-1 Toggle",
                toggleType: ThreeWayToggleSwitchType.MomOnOn,
                fromCenter: false);
            Add3PosnToggle(
                name: "IFF Mode(M-2)",
                posn: new Point(245d, 163d),
                size: new Size(60, 70),
                imagePath: $"{_imageAssetLocation}/IFFLeverBlackSolid/",
                imageBaseName: "F5E_RIGHT_SOLID_LEVER_BL",
                interfaceDevice: _interfaceDeviceName,
                interfaceElement: "IFF M-2 Toggle",
                toggleType: ThreeWayToggleSwitchType.MomOnOn,
                fromCenter: false);
            Add3PosnToggle(
                name: "IFF Mode(M-3/A)",
                posn: new Point(296d, 140d),
                size: new Size(60, 70),
                imagePath: $"{_imageAssetLocation}/IFFLeverBlackSolid/",
                imageBaseName: "F5E_RIGHT_SOLID_LEVER_BL",
                interfaceDevice: _interfaceDeviceName,
                interfaceElement: "IFF M-3A Toggle",
                toggleType: ThreeWayToggleSwitchType.MomOnOn,
                fromCenter: false);
            Add3PosnToggle(
                name: "IFF M-C Toggle",
                posn: new Point(342d, 120d),
                size: new Size(60, 70),
                imagePath: $"{_imageAssetLocation}/IFFLeverBlackSolid/",
                imageBaseName: "F5E_RIGHT_SOLID_LEVER_BL",
                interfaceDevice: _interfaceDeviceName,
                interfaceElement: "IFF M-C Toggle",
                toggleType: ThreeWayToggleSwitchType.MomOnOn,
                fromCenter: false);
            Add3PosnToggle(
                name: "IFF RAD Toggle",
                posn: new Point(390d, 100d),
                size: new Size(60, 70),
                imagePath: $"{_imageAssetLocation}/IFFLeverBlackSolid/",
                imageBaseName: "F5E_RIGHT_SOLID_LEVER_BL",
                interfaceDevice: _interfaceDeviceName,
                interfaceElement: "IFF RAD Toggle",
                toggleType: ThreeWayToggleSwitchType.MomOnOn,
                fromCenter: false);
            Add3PosnToggle(
                name: "IFF IDENT Toggle",
                posn: new Point(488d, 135d),
                size: new Size(60, 70),
                imagePath: $"{_imageAssetLocation}/IFFLeverRedSolid/",
                imageBaseName: "F5E_RIGHT_SOLID_LEVER_RD",
                interfaceDevice: _interfaceDeviceName,
                interfaceElement: "IFF IDENT Toggle",
                toggleType: ThreeWayToggleSwitchType.MomOnOn,
                fromCenter: false);
            Add2PosnToggle(
                name: "IFF ON/ OUT Toggle",
                posn: new Point(224d, 283d),
                size: new Size(70, 70),
                imagePath: $"{_imageAssetLocation}/IFFLeverBoldSolid/",
                imageBaseName: "F5E_RIGHT_SOLID_LEVER_BBL",
                interfaceDevice: _interfaceDeviceName,
                toggleType: ToggleSwitchType.OnOn,
                interfaceElement: "IFF ON/ OUT Toggle",
                fromCenter: false);

            List<Tuple<int, bool, double, double, string, string>> selectorPosition = new List<Tuple<int, bool, double, double, string, string>>
            {
                new Tuple<int, bool, double, double, string, string>(1, true, 0,   0d, "F5E_IFF_SELECTOR_SOLID_01.png", "F5E_IFF_SELECTOR_SOLID_PULLED_01.png"),
                new Tuple<int, bool, double, double, string, string>(2, true, 1,  30d, "F5E_IFF_SELECTOR_SOLID_02.png", "F5E_IFF_SELECTOR_SOLID_PULLED_02.png"),
                new Tuple<int, bool, double, double, string, string>(3, true, 2,  60d, "F5E_IFF_SELECTOR_SOLID_03.png", "F5E_IFF_SELECTOR_SOLID_PULLED_03.png"),
                new Tuple<int, bool, double, double, string, string>(4, true, 3,  90d, "F5E_IFF_SELECTOR_SOLID_04.png", "F5E_IFF_SELECTOR_SOLID_PULLED_04.png")
            };

            AddCodeSelector("IFF Code Selector", "IFF Code Selector", new Point(66d, 108d), new Size(90, 100),
                "",
                "",
                "",
                "",
                "{F-5E}/Images/GeneralPurposePullKnob/PullReady.xaml",
                $"{_imageAssetLocation}/IFFSelectorSolid/",
                selectorPosition, 70, 20, false, true);

            List<Tuple<int, bool, double, double, string, string>> masterSelectorPosition = new List<Tuple<int, bool, double, double, string, string>>
            {
                new Tuple<int, bool, double, double, string, string>(1, true, 0.4,   0d, "F5E_IFF_MASTER_SOLID_01.png", "F5E_IFF_MASTER_PULLED_SOLID_01.png"),
                new Tuple<int, bool, double, double, string, string>(2, true, 0.3,  30d, "F5E_IFF_MASTER_SOLID_02.png", "F5E_IFF_MASTER_PULLED_SOLID_02.png"),
                new Tuple<int, bool, double, double, string, string>(3, true, 0.2,  60d, "F5E_IFF_MASTER_SOLID_03.png", "F5E_IFF_MASTER_PULLED_SOLID_03.png"),
                new Tuple<int, bool, double, double, string, string>(4, true, 0.1,  90d, "F5E_IFF_MASTER_SOLID_04.png", "F5E_IFF_MASTER_PULLED_SOLID_04.png"),
                new Tuple<int, bool, double, double, string, string>(5, true, 0.0, 120d, "F5E_IFF_MASTER_SOLID_05.png", "F5E_IFF_MASTER_PULLED_SOLID_05.png")
            };


            AddMasterSelector("IFF Master Selector", "IFF Master Selector", new Point(294d, 23d), new Size(80, 90),
                "",
                "",
                "",
                "{F-5E}/Images/GeneralPurposePullKnob/PullReady.xaml",
                $"{_imageAssetLocation}/IFFMasterSolid/",
                masterSelectorPosition, 90, 20, false, true);

            AddPushButton("IFF CODE Left", $"{_imageAssetLocation}/../ClearButton/",
                "ClearButton", new Point(57d, 160d), new Size(30, 30));
            AddPushButton("IFF CODE Right", $"{_imageAssetLocation}/../ClearButton/",
                "ClearButton", new Point(138d, 140d), new Size(30, 30));

            AddPushButton("IFF Master Left", $"{_imageAssetLocation}/../ClearButton/",
                "ClearButton", new Point(287d, 70d), new Size(25, 25));
            AddPushButton("IFF Master Right", $"{_imageAssetLocation}/../ClearButton/",
                "ClearButton", new Point(364d, 50d), new Size(25, 25));

            Add3PosnToggle(
                name: "AHRS Compass Switch",
                posn: new Point(334d, 367d),
                size: new Size(80, 70),
                imagePath: $"{_imageAssetLocation}/IFFLeverRedSolid/",
                imageBaseName: "F5E_RIGHT_SOLID_LEVER_RD",
                interfaceDevice: _interfaceDeviceName,
                interfaceElement: "Compass Switch Toggle",
                toggleType: ThreeWayToggleSwitchType.OnOnOn,
                fromCenter: false);


            Add3PosnToggle(
                name: "Bright/Dim Switch",
                posn: new Point(500d, 520d),
                size: new Size(80, 70),
                imagePath: $"{_imageAssetLocation}/IFFLeverBlackSolid/",
                imageBaseName: "F5E_RIGHT_SOLID_LEVER_BL",
                interfaceDevice: _interfaceDeviceName,
                interfaceElement: "Bright/Dim Switch",
                toggleType: ThreeWayToggleSwitchType.MomOnOn,
                fromCenter: false);
            Add2PosnToggle(
                name: "Warning Test Switch",
                posn: new Point(596d, 600d),
                size: new Size(80, 70),
                imagePath: $"{_imageAssetLocation}/IFFLeverRedSolid/",
                imageBaseName: "F5E_RIGHT_SOLID_LEVER_RD",
                interfaceDevice: _interfaceDeviceName,
                toggleType:ToggleSwitchType.MomOn,
                interfaceElement: "Warning Test Switch",
                fromCenter: false);

            Add2PosnToggle(
                name: "Beacon Light Toggle",
                posn: new Point(785d, 460d),
                size: new Size(80, 70),
                imagePath: $"{_imageAssetLocation}/IFFLeverRedSolid/",
                imageBaseName: "F5E_RIGHT_SOLID_LEVER_RD",
                interfaceDevice: _interfaceDeviceName,
                toggleType: ToggleSwitchType.OnOn,
                interfaceElement: "Beacon Light Toggle",
                fromCenter: false);

            Add3PosnToggle(
                name: "Fuel Oxygen Switch",
                posn: new Point(832d, 258d),
                size: new Size(80, 70),
                imagePath: $"{_imageAssetLocation}/IFFLeverRedSolid/",
                imageBaseName: "F5E_RIGHT_SOLID_LEVER_RD",
                interfaceDevice: _interfaceDeviceName,
                interfaceElement: "Fuel Oxygen Switch",
                toggleType: ThreeWayToggleSwitchType.MomOnMom,
                fromCenter: false);

            List<Tuple<int, bool, double, double, string, string>> lightPostions = new List<Tuple<int, bool, double, double, string, string>>
            {
                new Tuple<int, bool, double, double, string, string>(0, false, 0,      0d,  "RightLightKnobSolid_01.png", "RightLightKnobSolid_01.png"),
                new Tuple<int, bool, double, double, string, string>(1, false, 0.05d, 13.5d,  "RightLightKnobSolid_02.png", "RightLightKnobSolid_02.png"),
                new Tuple<int, bool, double, double, string, string>(2, false, 0.1d,  27d,  "RightLightKnobSolid_03.png", "RightLightKnobSolid_03.png"),
                new Tuple<int, bool, double, double, string, string>(3, false, 0.15d, 40.5d,  "RightLightKnobSolid_04.png", "RightLightKnobSolid_04.png"),
                new Tuple<int, bool, double, double, string, string>(4, false, 0.2d,  54d,  "RightLightKnobSolid_05.png", "RightLightKnobSolid_05.png"),
                new Tuple<int, bool, double, double, string, string>(5, false, 0.25d, 67.5d,  "RightLightKnobSolid_06.png", "RightLightKnobSolid_06.png"),
                new Tuple<int, bool, double, double, string, string>(6, false, 0.3d,  81d,  "RightLightKnobSolid_07.png", "RightLightKnobSolid_07.png"),
                new Tuple<int, bool, double, double, string, string>(7, false, 0.35d, 94.5d, "RightLightKnobSolid_08.png", "RightLightKnobSolid_08.png"),
                new Tuple<int, bool, double, double, string, string>(8, false, 0.4d,  108d, "RightLightKnobSolid_09.png", "RightLightKnobSolid_09.png"),
                new Tuple<int, bool, double, double, string, string>(9, false, 0.45d, 121.5d, "RightLightKnobSolid_10.png", "RightLightKnobSolid_10.png"),
                new Tuple<int, bool, double, double, string, string>(10, false,0.5d,  135d, "RightLightKnobSolid_11.png", "RightLightKnobSolid_11.png"),
                new Tuple<int, bool, double, double, string, string>(11, false,0.55d, 148.5d, "RightLightKnobSolid_12.png", "RightLightKnobSolid_12.png"),
                new Tuple<int, bool, double, double, string, string>(12, false,0.6d,  162d, "RightLightKnobSolid_13.png", "RightLightKnobSolid_13.png"),
                new Tuple<int, bool, double, double, string, string>(13, false,0.65d, 175.5d, "RightLightKnobSolid_14.png", "RightLightKnobSolid_14.png"),
                new Tuple<int, bool, double, double, string, string>(14, false,0.7d,  189d, "RightLightKnobSolid_15.png", "RightLightKnobSolid_15.png"),
                new Tuple<int, bool, double, double, string, string>(15, false,0.75d, 202.5d, "RightLightKnobSolid_16.png", "RightLightKnobSolid_16.png"),
                new Tuple<int, bool, double, double, string, string>(16, false,0.8d,  216d, "RightLightKnobSolid_17.png", "RightLightKnobSolid_17.png"),
                new Tuple<int, bool, double, double, string, string>(17, false,0.85d, 229.5d, "RightLightKnobSolid_18.png", "RightLightKnobSolid_18.png"),
                new Tuple<int, bool, double, double, string, string>(18, false,0.9d,  243d, "RightLightKnobSolid_19.png", "RightLightKnobSolid_19.png"),
                new Tuple<int, bool, double, double, string, string>(19, false,0.95d, 256.5d, "RightLightKnobSolid_20.png", "RightLightKnobSolid_20.png"),
                new Tuple<int, bool, double, double, string, string>(20, false,1d,    270d, "RightLightKnobSolid_21.png", "RightLightKnobSolid_21.png"),
            };
            AddImageFlipParts("Flood Light Knob", "Flood Light Knob", new Point(430d, 452d), new Size(96, 68),
                "",
                "",
                "",
                "",
                "",
                $"{_imageAssetLocation}/LightKnobSolid/",
                lightPostions, 0, 0, false, false);

            AddImageFlipParts("Flight Light Knob", "Flight Light Knob", new Point(525d, 402d), new Size(92, 64),
                "",
                "",
                "",
                "",
                "",
                $"{_imageAssetLocation}/LightKnobSolid/",
                lightPostions, 0, 0, false, false);

            AddImageFlipParts("Engine Light Knob", "Engine Light Knob", new Point(608d, 470d), new Size(94, 68),
                "",
                "",
                "",
                "",
                "",
                $"{_imageAssetLocation}/LightKnobSolid/",
                lightPostions, 0, 0, false, false);

            AddImageFlipParts("Console Lights Knob", "Console Lights Knob", new Point(689d, 529d), new Size(100, 72),
                "",
                "",
                "",
                "",
                "",
                $"{_imageAssetLocation}/LightKnobSolid/",
                lightPostions, 0, 0, false, false);

            AddImageFlipParts("Navigation Light Knob", "Navigation Light Knob", new Point(626d, 344d), new Size(88, 70),
                "",
                "",
                "",
                "",
                "",
                $"{_imageAssetLocation}/LightKnobSolid/",
                lightPostions, 0, 0, false, false);

            AddImageFlipParts("Formation Light Knob", "Formation Light Knob", new Point(710d, 412d), new Size(88, 70),
                "",
                "",
                "",
                "",
                "",
                $"{_imageAssetLocation}/LightKnobSolid/",
                lightPostions, 0, 0, false, false);

        }

        #region Properties

        public override string DefaultBackgroundImage
        {
            get { return $"{_imageAssetLocation}/Panels/F5E_RIGHT_REAR_SOLID.png"; }
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

        private GeneralPurposePullKnob AddImageFlipParts(string name, string actionIdentifier,
            Point posn, Size size,
            string knobImagePath,
            string knobImagePulledPath,
            string backGroundKnobImagePath,
            string frontImagePath,
            string pullReadyImage,
            string basePath,
            List<Tuple<int, bool, double, double, string, string>> knobPostions,
            double pullJudgeAngle,
            double pullJudgeDistance,
            bool prohibitOperate,
            bool pullable)
        {
            GeneralPurposePullKnob part = AddGeneralPurposePullKnob(
                name: name,
                fromCenter: false,
                posn: posn,
                size: size,
                interfaceDeviceName: _interfaceDeviceName,
                interfaceElementName: actionIdentifier,
                knobImagePath: knobImagePath,
                knobImagePulledPath: knobImagePulledPath,
                frontImagePath: frontImagePath,
                pullReadyImage: pullReadyImage,
                basePath: basePath,
                knobPostions: knobPostions,
                frontAdjust: new Point(0,0),
                frontPos: new Point(0, 0),
                pullJudgeAngle: pullJudgeAngle,
                pullJudgeDistance: pullJudgeDistance,
                prohibitOperate: prohibitOperate,
                pullable: pullable);

            return part;
        }

        private IFFCodeSelector AddCodeSelector(string name, string actionIdentifier,
            Point posn, Size size,
            string knobImagePath,
            string knobImagePulledPath,
            string backGroundKnobImagePath,
            string frontImagePath,
            string pullReadyImage,
            string basePath,
            List<Tuple<int, bool, double, double, string, string>> knobPostions,
            double pullJudgeAngle,
            double pullJudgeDistance,
            bool prohibitOperate,
            bool pullable)
        {
            IFFCodeSelector part = AddCodeSelector(
                name: name,
                fromCenter: false,
                posn: posn,
                size: size,
                interfaceDeviceName: _interfaceDeviceName,
                interfaceElementName: actionIdentifier,
                knobImagePath: knobImagePath,
                knobImagePulledPath: knobImagePulledPath,
                frontImagePath: frontImagePath,
                pullReadyImage: pullReadyImage,
                basePath: basePath,
                knobPostions: knobPostions,
                pullJudgeAngle: pullJudgeAngle,
                pullJudgeDistance: pullJudgeDistance,
                prohibitOperate: prohibitOperate,
                pullable: pullable);

            return part;
        }

        private IFFMasterSelector AddMasterSelector(string name, string actionIdentifier,
            Point posn, Size size,
            string knobImagePath,
            string knobImagePulledPath,
            string frontImagePath,
            string pullReadyImage,
            string basePath,
            List<Tuple<int, bool, double, double, string, string>> knobPostions,
            double pullJudgeAngle,
            double pullJudgeDistance,
            bool prohibitOperate,
            bool pullable)
        {
            IFFMasterSelector part = AddMasterSelector(
                name: name,
                fromCenter: false,
                posn: posn,
                size: size,
                interfaceDeviceName: _interfaceDeviceName,
                interfaceElementName: actionIdentifier,
                knobImagePath: knobImagePath,
                knobImagePulledPath: knobImagePulledPath,
                frontImagePath: frontImagePath,
                pullReadyImage: pullReadyImage,
                basePath: basePath,
                knobPostions: knobPostions,
                pullJudgeAngle: pullJudgeAngle,
                pullJudgeDistance: pullJudgeDistance,
                prohibitOperate: prohibitOperate,
                pullable: pullable);

            return part;
        }




        private void Add3PosnToggle(string name, Point posn, Size size,string imagePath, 
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

        private void AddPushButton(string name, string imageBasePath, string imageFileName, Point posn, Size size)
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
