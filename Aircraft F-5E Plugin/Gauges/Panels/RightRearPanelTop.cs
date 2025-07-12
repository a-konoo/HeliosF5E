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


    [HeliosControl("Helios.F5E.RightRearPanelTop", "RightRearPanelTop", "F-5E", typeof(BackgroundImageRenderer))]
    class RightRearPanelTop : F5EDevice
    {
        private static readonly Rect SCREEN_RECT = new Rect(0, 0, 950, 730);
        private string _interfaceDeviceName = "RightRearPanelTop";
        private string _imageAssetLocation = "{F-5E}/Images";
        private Rect _scaledScreenRect = SCREEN_RECT;
        private static readonly NLog.Logger Logger = NLog.LogManager.GetCurrentClassLogger();


        public RightRearPanelTop()
            : base("RightRearPanelTop", new Size(700, 1000))
        {

            Children.Add(AddImage($"/RightSidePanel/Panels/F5E_RIGHT_REAR_TOP.png", new Point(0d, 0d), 700, 1000));

            List<Tuple<int, bool, double, double, string, string>> knobPostions = new List<Tuple<int, bool, double, double, string, string>>
            {
                new Tuple<int, bool, double, double, string, string>(0, false, 0,   0d,   "IFFWheelTop_01.png", "IFFWheelTop_01.png"),
                new Tuple<int, bool, double, double, string, string>(1, false, 0.1,  30d, "IFFWheelTop_02.png", "IFFWheelTop_02.png"),
                new Tuple<int, bool, double, double, string, string>(2, false, 0.2,  60d, "IFFWheelTop_03.png", "IFFWheelTop_03.png"),
                new Tuple<int, bool, double, double, string, string>(3, false, 0.3,  90d, "IFFWheelTop_04.png", "IFFWheelTop_04.png"),
                new Tuple<int, bool, double, double, string, string>(4, false, 0.4, 120d, "IFFWheelTop_05.png", "IFFWheelTop_05.png"),
                new Tuple<int, bool, double, double, string, string>(5, false, 0.5, 150d, "IFFWheelTop_06.png", "IFFWheelTop_06.png"),
                new Tuple<int, bool, double, double, string, string>(6, false, 0.6, 180d, "IFFWheelTop_07.png", "IFFWheelTop_07.png"),
                new Tuple<int, bool, double, double, string, string>(7, false, 0.7, 210d, "IFFWheelTop_08.png", "IFFWheelTop_08.png")
            };
            List<Tuple<int, bool, double, double, string, string>> knobPostions2 = new List<Tuple<int, bool, double, double, string, string>>
            {
                new Tuple<int, bool, double, double, string, string>(0, false, 0,    0d,  "IFFWheelTop_01.png", "IFFWheelTop_01.png"),
                new Tuple<int, bool, double, double, string, string>(1, false, 0.1,  30d, "IFFWheelTop_02.png", "IFFWheelTop_02.png"),
                new Tuple<int, bool, double, double, string, string>(2, false, 0.2,  60d, "IFFWheelTop_03.png", "IFFWheelTop_03.png"),
                new Tuple<int, bool, double, double, string, string>(3, false, 0.3,  90d, "IFFWheelTop_04.png", "IFFWheelTop_04.png"),
            };// 275d, 210d-->3,82
            AddImageFlipParts("IFF Selector 1", "IFF Selector1", new Point(155d, 380d), new Size(25, 100),
                "",
                "",
                "",
                "",
                "",
                $"{_imageAssetLocation}/RightSidePanel/IFFWheelTop/",
                knobPostions, 0, 0, false, false);
            AddImageFlipParts("IFF Selector 2", "IFF Selector2", new Point(205d, 380d), new Size(25, 100),
                "",
                "",
                "",
                "",
                "",
                $"{_imageAssetLocation}/RightSidePanel/IFFWheelTop/",
                knobPostions2, 0, 0, false, false);
            AddImageFlipParts("IFF Selector 3", "IFF Selector3", new Point(255d, 380d), new Size(25, 100),
                "",
                "",
                "",
                "",
                "",
                $"{_imageAssetLocation}/RightSidePanel/IFFWheelTop/",
                knobPostions, 0, 0, false, false);
            AddImageFlipParts("IFF Selector 4", "IFF Selector4", new Point(305d, 380d), new Size(25, 100),
                "",
                "",
                "",
                "",
                "",
                $"{_imageAssetLocation}/RightSidePanel/IFFWheelTop/",
                knobPostions, 0, 0, false, false);
            AddImageFlipParts("IFF Selector 5", "IFF Selector5", new Point(355d, 380d), new Size(25, 100),
                "",
                "",
                "",
                "",
                "",
                $"{_imageAssetLocation}/RightSidePanel/IFFWheelTop/",
                knobPostions, 0, 0, false, false);
            AddImageFlipParts("IFF Selector 6", "IFF Selector6", new Point(405d, 380d), new Size(25, 100),
                "",
                "",
                "",
                "",
                "",
                $"{_imageAssetLocation}/RightSidePanel/IFFWheelTop/",
                knobPostions, 0, 0, false, false);
            AddPushButton("IFF GreenLightTop Left", $"{_imageAssetLocation}/RightSidePanel/IFFGreenLightTop/",
                "F5E_GREEN_LIGHT_TOP", new Point(232d, 69d), new Size(60, 60));
            AddPushButton("IFF GreenLightTop Right", $"{_imageAssetLocation}/RightSidePanel/IFFGreenLightTop/",
                "F5E_GREEN_LIGHT_TOP", new Point(310d, 67d), new Size(60, 60));


            AddPushButton("IFF Wheel1Up", $"{_imageAssetLocation}/RightSidePanel/../ClearButton/",
                "ClearButton", new Point(165d, 390d), new Size(20, 20));
            AddPushButton("IFF Wheel1Down", $"{_imageAssetLocation}/RightSidePanel/../ClearButton/",
                "ClearButton", new Point(165d, 460d), new Size(20, 20));
            AddPushButton("IFF Wheel2Up", $"{_imageAssetLocation}/RightSidePanel/../ClearButton/",
                "ClearButton", new Point(215d, 390d), new Size(20, 20));
            AddPushButton("IFF Wheel2Down", $"{_imageAssetLocation}/RightSidePanel/../ClearButton/",
                "ClearButton", new Point(215d, 460d), new Size(20, 20));
            AddPushButton("IFF Wheel3Up", $"{_imageAssetLocation}/RightSidePanel/../ClearButton/",
                "ClearButton", new Point(265d, 390d), new Size(20, 20));
            AddPushButton("IFF Wheel3Down", $"{_imageAssetLocation}/RightSidePanel/../ClearButton/",
                "ClearButton", new Point(265d, 460d), new Size(20, 20));
            AddPushButton("IFF Wheel4Up", $"{_imageAssetLocation}/RightSidePanel/../ClearButton/",
                "ClearButton", new Point(315d, 390d), new Size(20, 20));
            AddPushButton("IFF Wheel4Down", $"{_imageAssetLocation}/RightSidePanel/../ClearButton/",
                "ClearButton", new Point(315d, 460d), new Size(20, 20));
            AddPushButton("IFF Wheel5Up", $"{_imageAssetLocation}/RightSidePanel/../ClearButton/",
                "ClearButton", new Point(365d, 390d), new Size(20, 20));
            AddPushButton("IFF Wheel5Down", $"{_imageAssetLocation}/RightSidePanel/../ClearButton/",
                "ClearButton", new Point(365d, 460d), new Size(20, 20));
            AddPushButton("IFF Wheel6Up", $"{_imageAssetLocation}/RightSidePanel/../ClearButton/",
                "ClearButton", new Point(415d, 390d), new Size(20, 20));
            AddPushButton("IFF Wheel6Down", $"{_imageAssetLocation}/RightSidePanel/../ClearButton/",
                "ClearButton", new Point(415d, 460d), new Size(20, 20));



            Add3PosnToggle(
                name: "IFF MODE 4 Monitor Toggle",
                posn: new Point(86d, 210d),
                size: new Size(40, 90),
                imagePath: $"{_imageAssetLocation}/MetalLevers/WhiteFrontLever/",
                imageBaseName: "WhiteFrontLever",
                interfaceDevice: _interfaceDeviceName,
                interfaceElement: "IFF MODE 4 Monitor Cntrl",
                toggleType: ThreeWayToggleSwitchType.OnOnOn,
                fromCenter: false);
            Add3PosnToggle(
                name: "IFF Mode(M-1)",
                posn: new Point(150d, 210d),
                size: new Size(40, 90),
                imagePath: $"{_imageAssetLocation}/MetalLevers/WhiteFrontLever/",
                imageBaseName: "WhiteFrontLever",
                interfaceDevice: _interfaceDeviceName,
                interfaceElement: "IFF M-1 Toggle",
                toggleType: ThreeWayToggleSwitchType.MomOnOn,
                fromCenter: false);
            Add3PosnToggle(
                name: "IFF Mode(M-2)",
                posn: new Point(220d, 210d),
                size: new Size(40, 90),
                imagePath: $"{_imageAssetLocation}/MetalLevers/WhiteFrontLever/",
                imageBaseName: "WhiteFrontLever",
                interfaceDevice: _interfaceDeviceName,
                interfaceElement: "IFF M-2 Toggle",
                toggleType: ThreeWayToggleSwitchType.MomOnOn,
                fromCenter: false);
            Add3PosnToggle(
                name: "IFF Mode(M-3/A)",
                posn: new Point(290d, 210d),
                size: new Size(40, 90),
                imagePath: $"{_imageAssetLocation}/MetalLevers/WhiteFrontLever/",
                imageBaseName: "WhiteFrontLever",
                interfaceDevice: _interfaceDeviceName,
                interfaceElement: "IFF M-3A Toggle",
                toggleType: ThreeWayToggleSwitchType.MomOnOn,
                fromCenter: false);
            Add3PosnToggle(
                name: "IFF M-C Toggle",
                posn: new Point(362d, 210d),
                size: new Size(40, 90),
                imagePath: $"{_imageAssetLocation}/MetalLevers/WhiteFrontLever/",
                imageBaseName: "WhiteFrontLever",
                interfaceDevice: _interfaceDeviceName,
                interfaceElement: "IFF M-C Toggle",
                toggleType: ThreeWayToggleSwitchType.MomOnOn,
                fromCenter: false);
            Add3PosnToggle(
                name: "IFF RAD Toggle",
                posn: new Point(445d, 210d),
                size: new Size(40, 90),
                imagePath: $"{_imageAssetLocation}/MetalLevers/WhiteFrontLever/",
                imageBaseName: "WhiteFrontLever",
                interfaceDevice: _interfaceDeviceName,
                interfaceElement: "IFF RAD Toggle",
                toggleType: ThreeWayToggleSwitchType.MomOnOn,
                fromCenter: false);
            Add3PosnToggleRev(
                name: "IFF IDENT Toggle",
                posn: new Point(448d, 340d),
                size: new Size(35, 82),
                imagePath: $"{_imageAssetLocation}/MetalLevers/RedMetalLever/",
                imageBaseName: "RedMetalLever",
                interfaceDevice: _interfaceDeviceName,
                interfaceElement: "IFF IDENT Toggle",
                toggleType: ThreeWayToggleSwitchType.MomOnOn,
                fromCenter: false);
            Add2PosnToggle(
                name: "IFF ON/ OUT Toggle",
                posn: new Point(86d, 363d),
                size: new Size(40, 90),
                imagePath: $"{_imageAssetLocation}/MetalLevers/BlackBigFlowLever/",
                imageBaseName: "FuelFlowLever",
                interfaceDevice: _interfaceDeviceName,
                toggleType: ToggleSwitchType.OnOn,
                interfaceElement: "IFF ON/ OUT Toggle",
                fromCenter: false);

            List<Tuple<int, bool, double, double, string, string>> selectorPosition = new List<Tuple<int, bool, double, double, string, string>>
            {
                new Tuple<int, bool, double, double, string, string>(1, true, 0,   0d, "F5E_IFF_SELECTOR_TOP_01.png", "F5E_IFF_SELECTOR_PULLED_TOP_01.png"),
                new Tuple<int, bool, double, double, string, string>(2, true, 1,  30d, "F5E_IFF_SELECTOR_TOP_02.png", "F5E_IFF_SELECTOR_PULLED_TOP_02.png"),
                new Tuple<int, bool, double, double, string, string>(3, true, 2,  60d, "F5E_IFF_SELECTOR_TOP_03.png", "F5E_IFF_SELECTOR_PULLED_TOP_03.png"),
                new Tuple<int, bool, double, double, string, string>(4, true, 3,  90d, "F5E_IFF_SELECTOR_TOP_04.png", "F5E_IFF_SELECTOR_PULLED_TOP_04.png")
            };

            AddCodeSelector("IFF Code Selector", "IFF Code Selector", new Point(65d, 45d), new Size(120, 120),
                "",
                "",
                "",
                "",
                "{F-5E}/Images/GeneralPurposePullKnob/PullReady.xaml",
                $"{_imageAssetLocation}/RightSidePanel/IFFSelectorTop/",
                selectorPosition, 70, 20, false, true);

            List<Tuple<int, bool, double, double, string, string>> masterSelectorPosition = new List<Tuple<int, bool, double, double, string, string>>
            {
                new Tuple<int, bool, double, double, string, string>(1, true, 0.4,   0d, "F5E_IFF_MASTER_TOP_01.png", "F5E_IFF_MASTER_PULLED_TOP_01.png"),
                new Tuple<int, bool, double, double, string, string>(2, true, 0.3,  30d, "F5E_IFF_MASTER_TOP_02.png", "F5E_IFF_MASTER_PULLED_TOP_02.png"),
                new Tuple<int, bool, double, double, string, string>(3, true, 0.2,  60d, "F5E_IFF_MASTER_TOP_03.png", "F5E_IFF_MASTER_PULLED_TOP_03.png"),
                new Tuple<int, bool, double, double, string, string>(4, true, 0.1,  90d, "F5E_IFF_MASTER_TOP_04.png", "F5E_IFF_MASTER_PULLED_TOP_04.png"),
                new Tuple<int, bool, double, double, string, string>(5, true, 0.0, 120d, "F5E_IFF_MASTER_TOP_05.png", "F5E_IFF_MASTER_PULLED_TOP_05.png")
            };


            AddMasterSelector("IFF Master Selector", "IFF Master Selector", new Point(385d, 45d), new Size(120, 120),
                "",
                "",
                "",
                "",
                "{F-5E}/Images/GeneralPurposePullKnob/PullReady.xaml",
                $"{_imageAssetLocation}/RightSidePanel/IFFMasterTop/",
                masterSelectorPosition, 90, 20, false, true);

            AddPushButton("IFF CODE Left", $"{_imageAssetLocation}/RightSidePanel/../ClearButton/",
                "ClearButton", new Point(50d, 70d), new Size(40, 40));
            AddPushButton("IFF CODE Right", $"{_imageAssetLocation}/RightSidePanel/../ClearButton/",
                "ClearButton", new Point(200d, 70d), new Size(40, 40));

            AddPushButton("IFF Master Left", $"{_imageAssetLocation}/RightSidePanel/../ClearButton/",
                "ClearButton", new Point(380d, 70d), new Size(30, 30));
            AddPushButton("IFF Master Right", $"{_imageAssetLocation}/RightSidePanel/../ClearButton/",
                "ClearButton", new Point(510d, 70d), new Size(30, 30));

            Add3PosnToggleRev(
                name: "AHRS Compass Switch",
                posn: new Point(102d, 530d),
                size: new Size(35, 82),
                imagePath: $"{_imageAssetLocation}/MetalLevers/RedMetalLever/",
                imageBaseName: "RedMetalLever",
                interfaceDevice: _interfaceDeviceName,
                interfaceElement: "Compass Switch Toggle",
                toggleType: ThreeWayToggleSwitchType.OnOnOn,
                fromCenter: false);

            Add3PosnToggle(
                name: "Bright/Dim Switch",
                posn: new Point(102d, 770d),
                size: new Size(40, 90),
                imagePath: $"{_imageAssetLocation}/MetalLevers/WhiteFrontLever/",
                imageBaseName: "WhiteFrontLever",
                interfaceDevice: _interfaceDeviceName,
                interfaceElement: "Bright/Dim Switch",
                toggleType: ThreeWayToggleSwitchType.MomOnOn,
                fromCenter: false);

            Add2PosnToggleRev(
                name: "Warning Test Switch",
                posn: new Point(102d, 887d),
                size: new Size(35, 82),
                imagePath: $"{_imageAssetLocation}/MetalLevers/RedMetalLever/",
                imageBaseName: "RedMetalLever",
                interfaceDevice: _interfaceDeviceName,
                toggleType: ToggleSwitchType.MomOn,
                interfaceElement: "Warning Test Switch",
                fromCenter: false);

            Add2PosnToggleRev(
                name: "Beacon Light Toggle",
                posn: new Point(355d, 875d),
                size: new Size(35, 82),
                imagePath: $"{_imageAssetLocation}/MetalLevers/RedMetalLever/",
                imageBaseName: "RedMetalLever",
                interfaceDevice: _interfaceDeviceName,
                toggleType: ToggleSwitchType.OnOn,
                interfaceElement: "Beacon Light Toggle",
                fromCenter: false);

            Add3PosnToggleRev(
                name: "Fuel Oxygen Switch",
                posn: new Point(593d, 727d),
                size: new Size(35, 82),
                imagePath: $"{_imageAssetLocation}/MetalLevers/RedMetalLever/",
                imageBaseName: "RedMetalLever",
                interfaceDevice: _interfaceDeviceName,
                interfaceElement: "Fuel Oxygen Switch",
                toggleType: ThreeWayToggleSwitchType.MomOnMom,
                fromCenter: false);

            List<Tuple<int, bool, double, double, string, string>> lightPostions = new List<Tuple<int, bool, double, double, string, string>>
            {
                new Tuple<int, bool, double, double, string, string>(0, false, 0,      0d,  "RoundArrowKnob_01.png", "RoundArrowKnob_01.png"),
                new Tuple<int, bool, double, double, string, string>(1, false, 0.05d, 13.5d,  "RoundArrowKnob_02.png", "RoundArrowKnob_02.png"),
                new Tuple<int, bool, double, double, string, string>(2, false, 0.1d,  27d,  "RoundArrowKnob_03.png", "RoundArrowKnob_03.png"),
                new Tuple<int, bool, double, double, string, string>(3, false, 0.15d, 40.5d,  "RoundArrowKnob_04.png", "RoundArrowKnob_04.png"),
                new Tuple<int, bool, double, double, string, string>(4, false, 0.2d,  54d,  "RoundArrowKnob_05.png", "RoundArrowKnob_05.png"),
                new Tuple<int, bool, double, double, string, string>(5, false, 0.25d, 67.5d,  "RoundArrowKnob_06.png", "RoundArrowKnob_06.png"),
                new Tuple<int, bool, double, double, string, string>(6, false, 0.3d,  81d,  "RoundArrowKnob_07.png", "RoundArrowKnob_07.png"),
                new Tuple<int, bool, double, double, string, string>(7, false, 0.35d, 94.5d, "RoundArrowKnob_08.png", "RoundArrowKnob_08.png"),
                new Tuple<int, bool, double, double, string, string>(8, false, 0.4d,  108d, "RoundArrowKnob_09.png", "RoundArrowKnob_09.png"),
                new Tuple<int, bool, double, double, string, string>(9, false, 0.45d, 121.5d, "RoundArrowKnob_10.png", "RoundArrowKnob_10.png"),
                new Tuple<int, bool, double, double, string, string>(10, false,0.5d,  135d, "RoundArrowKnob_11.png", "RoundArrowKnob_11.png"),
                new Tuple<int, bool, double, double, string, string>(11, false,0.55d, 148.5d, "RoundArrowKnob_12.png", "RoundArrowKnob_12.png"),
                new Tuple<int, bool, double, double, string, string>(12, false,0.6d,  162d, "RoundArrowKnob_13.png", "RoundArrowKnob_13.png"),
                new Tuple<int, bool, double, double, string, string>(13, false,0.65d, 175.5d, "RoundArrowKnob_14.png", "RoundArrowKnob_14.png"),
                new Tuple<int, bool, double, double, string, string>(14, false,0.7d,  189d, "RoundArrowKnob_15.png", "RoundArrowKnob_15.png"),
                new Tuple<int, bool, double, double, string, string>(15, false,0.75d, 202.5d, "RoundArrowKnob_16.png", "RoundArrowKnob_16.png"),
                new Tuple<int, bool, double, double, string, string>(16, false,0.8d,  216d, "RoundArrowKnob_17.png", "RoundArrowKnob_17.png"),
                new Tuple<int, bool, double, double, string, string>(17, false,0.85d, 229.5d, "RoundArrowKnob_18.png", "RoundArrowKnob_18.png"),
                new Tuple<int, bool, double, double, string, string>(18, false,0.9d,  243d, "RoundArrowKnob_19.png", "RoundArrowKnob_19.png"),
                new Tuple<int, bool, double, double, string, string>(19, false,0.95d, 256.5d, "RoundArrowKnob_20.png", "RoundArrowKnob_20.png"),
                new Tuple<int, bool, double, double, string, string>(20, false,1d,    270d, "RoundArrowKnob_21.png", "RoundArrowKnob_21.png"),
            };
            AddImageFlipParts("Flood Light Knob", "Flood Light Knob", new Point(94d, 662d), new Size(76, 76),
                "",
                "",
                "",
                "",
                "",
                $"{_imageAssetLocation}/RoundArrowKnob/",
                lightPostions, 0, 0, false, false);

            AddImageFlipParts("Flight Light Knob", "Flight Light Knob", new Point(213d, 664d), new Size(76, 76),
                "",
                "",
                "",
                "",
                "",
                $"{_imageAssetLocation}/RoundArrowKnob/",
                lightPostions, 0, 0, false, false);

            AddImageFlipParts("Engine Light Knob", "Engine Light Knob", new Point(213d, 776d), new Size(76, 76),
                "",
                "",
                "",
                "",
                "",
                $"{_imageAssetLocation}/RoundArrowKnob/",
                lightPostions, 0, 0, false, false);

            AddImageFlipParts("Console Lights Knob", "Console Lights Knob", new Point(213d, 877d), new Size(76, 76),
                "",
                "",
                "",
                "",
                "",
                $"{_imageAssetLocation}/RoundArrowKnob/",
                lightPostions, 0, 0, false, false);

            AddImageFlipParts("Navigation Light Knob", "Navigation Light Knob", new Point(347d, 666d), new Size(76, 76),
                "",
                "",
                "",
                "",
                "",
                $"{_imageAssetLocation}/RoundArrowKnob/",
                lightPostions, 0, 0, false, false);

            AddImageFlipParts("Formation Light Knob", "Formation Light Knob", new Point(343d, 781d), new Size(76, 76),
                "",
                "",
                "",
                "",
                "",
                $"{_imageAssetLocation}/RoundArrowKnob/",
                lightPostions, 0, 0, false, false);

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
                frontAdjust: new Point(0, 0),
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




        private void Add3PosnToggle(string name, Point posn, Size size, string imagePath,
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

        private void Add3PosnToggleRev(string name, Point posn, Size size, string imagePath,
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

        private void Add2PosnToggle(string name, Point posn, Size size, string imagePath,
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

        private void Add2PosnToggleRev(string name, Point posn, Size size, string imagePath,
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
        private void Add2PosnTogglePtn(string name, Point posn, Size size, string imagePath,
            string imageBaseName, string interfaceDevice, string interfaceElement,
            ToggleSwitchType toggleType, bool fromCenter)
        {
            AddToggleSwitch(
                name: name,
                posn: posn,
                size: size,
                defaultPosition: ToggleSwitchPosition.Two,
                positionOneImage: imagePath + imageBaseName + "_01.png",
                positionTwoImage: imagePath + imageBaseName + "_02.png",
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
