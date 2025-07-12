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


    [HeliosControl("Helios.F5E.RightPanel", "RightPanel", "F-5E", typeof(BackgroundImageRenderer))]
    class RightPanel : F5EDevice
    {
        private static readonly Rect SCREEN_RECT = new Rect(0, 0, 707, 295);
        private string _interfaceDeviceName = "RightPanel";
        private string _imageAssetLocation = "{F-5E}/Images";
        private Rect _scaledScreenRect = SCREEN_RECT;
        private static readonly NLog.Logger Logger = NLog.LogManager.GetCurrentClassLogger();


        public RightPanel()
            : base("RightPanel", new Size(707, 295))
        {

            Children.Add(AddImage($"/RightPanel/RightPanel.png", new Point(0d, 0d), 707, 295));

            List<Tuple<int, bool, double, double, string, string>> knobPostions = new List<Tuple<int, bool, double, double, string, string>>
            {
                new Tuple<int, bool, double, double, string, string>(0, false,  0,       0d, "ArrowKnob_00.png", "ArrowKnob_00.png"),
                new Tuple<int, bool, double, double, string, string>(1, false,  0.035,  10d, "ArrowKnob_01.png", "ArrowKnob_01.png"),
                new Tuple<int, bool, double, double, string, string>(2, false,  0.071,  20d, "ArrowKnob_02.png", "ArrowKnob_02.png"),
                new Tuple<int, bool, double, double, string, string>(3, false,  0.107,  30d, "ArrowKnob_03.png", "ArrowKnob_03.png"),
                new Tuple<int, bool, double, double, string, string>(4, false,  0.142,  40d, "ArrowKnob_04.png", "ArrowKnob_04.png"),
                new Tuple<int, bool, double, double, string, string>(5, false,  0.178,  50d, "ArrowKnob_05.png", "ArrowKnob_05.png"),
                new Tuple<int, bool, double, double, string, string>(6, false,  0.214,  60d, "ArrowKnob_06.png", "ArrowKnob_06.png"),
                new Tuple<int, bool, double, double, string, string>(7, false,  0.25,   70d, "ArrowKnob_07.png", "ArrowKnob_07.png"),
                new Tuple<int, bool, double, double, string, string>(8, false,  0.285,  80d, "ArrowKnob_08.png", "ArrowKnob_08.png"),
                new Tuple<int, bool, double, double, string, string>(9, false,  0.321,  90d, "ArrowKnob_09.png", "ArrowKnob_09.png"),
                new Tuple<int, bool, double, double, string, string>(10, false, 0.357,  100d, "ArrowKnob_10.png", "ArrowKnob_10.png"),
                new Tuple<int, bool, double, double, string, string>(11, false, 0.392,  110d, "ArrowKnob_11.png", "ArrowKnob_11.png"),
                new Tuple<int, bool, double, double, string, string>(12, false, 0.428,  120d, "ArrowKnob_12.png", "ArrowKnob_12.png"),
                new Tuple<int, bool, double, double, string, string>(13, false, 0.464,  130d, "ArrowKnob_13.png", "ArrowKnob_13.png"),
                new Tuple<int, bool, double, double, string, string>(14, false, 0.5,    140d, "ArrowKnob_14.png", "ArrowKnob_14.png"),
                new Tuple<int, bool, double, double, string, string>(15, false, 0.535,  150d, "ArrowKnob_15.png", "ArrowKnob_15.png"),
                new Tuple<int, bool, double, double, string, string>(16, false, 0.571,  160d, "ArrowKnob_16.png", "ArrowKnob_16.png"),
                new Tuple<int, bool, double, double, string, string>(17, false, 0.607,  170d, "ArrowKnob_17.png", "ArrowKnob_17.png"),
                new Tuple<int, bool, double, double, string, string>(18, false, 0.642,  180d, "ArrowKnob_18.png", "ArrowKnob_18.png"),
                new Tuple<int, bool, double, double, string, string>(19, false, 0.678,  190d, "ArrowKnob_19.png", "ArrowKnob_19.png"),
                new Tuple<int, bool, double, double, string, string>(20, false, 0.714,  200d, "ArrowKnob_20.png", "ArrowKnob_20.png"),
                new Tuple<int, bool, double, double, string, string>(21, false, 0.75,  210d, "ArrowKnob_21.png", "ArrowKnob_21.png"),
                new Tuple<int, bool, double, double, string, string>(22, false, 0.785,  220d, "ArrowKnob_22.png", "ArrowKnob_22.png"),
                new Tuple<int, bool, double, double, string, string>(23, false, 0.821,  230d, "ArrowKnob_23.png", "ArrowKnob_23.png"),
                new Tuple<int, bool, double, double, string, string>(24, false, 0.857,  240d, "ArrowKnob_24.png", "ArrowKnob_24.png"),
                new Tuple<int, bool, double, double, string, string>(25, false, 0.892,  250d, "ArrowKnob_25.png", "ArrowKnob_25.png"),
                new Tuple<int, bool, double, double, string, string>(26, false, 0.928,  260d, "ArrowKnob_26.png", "ArrowKnob_26.png"),
                new Tuple<int, bool, double, double, string, string>(27, false, 0.964,  270d, "ArrowKnob_27.png", "ArrowKnob_27.png"),
                new Tuple<int, bool, double, double, string, string>(28, false, 1.0,    280d, "ArrowKnob_28.png", "ArrowKnob_28.png"),
            };
            AddImageFlipParts("CanopyDefog", "CanopyDefogKnob",
                new Point(27d, 180d), new Size(79, 79),
                "",
                "",
                "",
                "",
                $"{_imageAssetLocation}/FrontKnobs/RoundArrowKnob/",
                knobPostions, new Point(0d, 0d), 0, 0, false, false);

            AddImageFlipParts("CabinTempKnob", "CabinTempKnob",
                new Point(218d, 46d), new Size(75, 75),
                "",
                "",
                "",
                "",
                $"{_imageAssetLocation}/FrontKnobs/RoundArrowKnob/",
                knobPostions, new Point(0d, 0d), 0, 0, false, false);

            Add2PosnToggle(
                name: "Pitot Heater Switch",
                posn: new Point(155d, 188d),
                size: new Size(40, 80),
                imagePath: $"{_imageAssetLocation}/MetalLevers/FrontRedLever/",
                imageBaseName: "FrontRedLever",
                interfaceDevice: _interfaceDeviceName,
                toggleType: ToggleSwitchType.OnOn,
                interfaceElement: "Pitot Heater Switch",
                fromCenter: false);

            Add2PosnToggle(
                name: "Engine Anti-Ice Switch",
                posn: new Point(217d, 188d),
                size: new Size(40, 80),
                imagePath: $"{_imageAssetLocation}/MetalLevers/FrontRedLever/",
                imageBaseName: "FrontRedLever",
                interfaceDevice: _interfaceDeviceName,
                toggleType: ToggleSwitchType.OnOn,
                interfaceElement: "Engine Anti-Ice Switch",
                fromCenter: false);

            Add2PosnToggle(
                name: "CrossFeed Switch",
                posn: new Point(418d, 140d),
                size: new Size(40, 80),
                imagePath: $"{_imageAssetLocation}/MetalLevers/FrontRedLever/",
                imageBaseName: "FrontRedLever",
                interfaceDevice: _interfaceDeviceName,
                toggleType: ToggleSwitchType.OnOn,
                interfaceElement: "CrossFeed Switch",
                fromCenter: false);

            Add2PosnToggleAlt(
                name: "LeftBoostPump Switch",
                posn: new Point(340d, 120d),
                size: new Size(40, 80),
                imagePath: $"{_imageAssetLocation}/MetalLevers/BlackBigFlowLever/",
                imageBaseName: "FuelFlowLever",
                interfaceDevice: _interfaceDeviceName,
                toggleType: ToggleSwitchType.OnOn,
                interfaceElement: "LeftBoostPump Switch",
                fromCenter: false);

            Add2PosnToggleAlt(
                name: "RightBoostPump Switch",
                posn: new Point(500d, 120d),
                size: new Size(40, 80),
                imagePath: $"{_imageAssetLocation}/MetalLevers/BlackBigFlowLever/",
                imageBaseName: "FuelFlowLever",
                interfaceDevice: _interfaceDeviceName,
                toggleType: ToggleSwitchType.OnOn,
                interfaceElement: "RightBoostPump Switch",
                fromCenter: false);

            List<Tuple<int, bool, double, double, string, string>> balanceKnobPositoon = new List<Tuple<int, bool, double, double, string, string>>
            {
                new Tuple<int, bool, double, double, string, string>(0, false,  -1, 30d, "FuelAutoBalance_01.png", "FuelAutoBalance_01.png"),
                new Tuple<int, bool, double, double, string, string>(1, false,  0,  20d, "FuelAutoBalance_02.png", "FuelAutoBalance_02.png"),
                new Tuple<int, bool, double, double, string, string>(2, false,  1,  10d, "FuelAutoBalance_03.png", "FuelAutoBalance_03.png"),
            };
            AddImageFlipParts("AutoBalance Switch", "AutoBalance Switch",
                new Point(560d, 140d), new Size(110, 40),
                "",
                "",
                "",
                "",
                $"{_imageAssetLocation}/MetalLevers/AutoBalanceToggle/",
                balanceKnobPositoon, new Point(0d, 0d), 0, 0, true, false);

            AddPushButton("AutoBalanceL", $"{_imageAssetLocation}/ClearButton/",
                "ClearButton", new Point(570d, 150d), new Size(20, 20));
            AddPushButton("AutoBalanceC", $"{_imageAssetLocation}/ClearButton/",
                "ClearButton", new Point(610d, 150d), new Size(20, 20));
            AddPushButton("AutoBalanceR", $"{_imageAssetLocation}/ClearButton/",
                "ClearButton", new Point(640d, 150d), new Size(20, 20));

            Add2PosnToggleAlt(
                name: "Ext Fuel Pylons Switch",
                posn: new Point(436d, 33d),
                size: new Size(40, 80),
                imagePath: $"{_imageAssetLocation}/MetalLevers/WhiteFrontLever/",
                imageBaseName: "WhiteFrontLever",
                interfaceDevice: _interfaceDeviceName,
                toggleType: ToggleSwitchType.OnOn,
                interfaceElement: "Ext Fuel Pylons Switch",
                fromCenter: false);

            Add2PosnToggleAlt(
                name: "Ext Fuel CL Switch",
                posn: new Point(336d, 33d),
                size: new Size(40, 80),
                imagePath: $"{_imageAssetLocation}/MetalLevers/WhiteFrontLever/",
                imageBaseName: "WhiteFrontLever",
                interfaceDevice: _interfaceDeviceName,
                toggleType: ToggleSwitchType.OnOn,
                interfaceElement: "Ext Fuel CL Switch",
                fromCenter: false);

            List<Tuple<int, bool, double, double, string, string>> aircondPosition = new List<Tuple<int, bool, double, double, string, string>>
            {
                new Tuple<int, bool, double, double, string, string>(1, false, 0,  10d, "AirCondToggle_01.png", "AirCondToggle_01.png"),
                new Tuple<int, bool, double, double, string, string>(2, false, 1,  20d, "AirCondToggle_02.png", "AirCondToggle_02.png"),
                new Tuple<int, bool, double, double, string, string>(3, false, 2,  30d, "AirCondToggle_03.png", "AirCondToggle_03.png"),
                new Tuple<int, bool, double, double, string, string>(4, false, 3,  40d, "AirCondToggle_04.png", "AirCondToggle_04.png"),
                new Tuple<int, bool, double, double, string, string>(5, false, 3,  50d, "AirCondToggle_05.png", "AirCondToggle_05.png"),
                new Tuple<int, bool, double, double, string, string>(6, false, 3,  60d, "AirCondToggle_06.png", "AirCondToggle_06.png"),
                new Tuple<int, bool, double, double, string, string>(7, false, 3,  70d, "AirCondToggle_07.png", "AirCondToggle_07.png")
            };

            AddImageFlipParts("AirCondThreeWayToggle", "AirCondThreeWayToggle",
                new Point(130d, 54d), new Size(70, 70),
                "",
                "",
                "",
                "",
                $"{_imageAssetLocation}/MetalLevers/AirCondThreeWayToggle/",
                aircondPosition, new Point(0d, 0d), 0, 0, true, false);

            AddPushButton("AirCondUp", $"{_imageAssetLocation}/ClearButton/",
                "ClearButton", new Point(157d, 45d), new Size(20, 20));
            AddPushButton("AirCondCenter", $"{_imageAssetLocation}/ClearButton/",
                "ClearButton", new Point(157d, 80d), new Size(20, 20));
            AddPushButton("AirCondLeft", $"{_imageAssetLocation}/ClearButton/",
                "ClearButton", new Point(126d, 100d), new Size(20, 20));
            AddPushButton("AirCondRight", $"{_imageAssetLocation}/ClearButton/",
                "ClearButton", new Point(182d, 100d), new Size(20, 20));

            Add3PosnToggle(
                name: "CabinPressSwitch",
                posn: new Point(81d, 23d),
                size: new Size(41, 120),
                imagePath: $"{_imageAssetLocation}/MetalLevers/CabinPressToggle/",
                imageBaseName: "CabinPressSwitch",
                interfaceDevice: _interfaceDeviceName,
                interfaceElement: "CabinPressSwitch",
                toggleType: ThreeWayToggleSwitchType.OnOnOn,
                horozontal: false,
                fromCenter: false);

            AddGuardCloseOnToggleState(
                name: "CabinPressSwitchCover",
                posn: new Point(79d, 25d),
                size: new Size(45, 130),
                imagePath: $"{_imageAssetLocation}/MetalLevers/AirPressCover/",
                imageBaseName: "AirPressCover",
                direction: 0,
                guardOpenRegion: new Rect(0, 0, 30, 20),
                switchRegion: new Rect(0, 20, 30, 50),
                guardCloseRegion: new Rect(0, 86, 30, 20),
                noUseClosable: true);

            AddPushButton("NormalPosAssist", $"{_imageAssetLocation}/ClearButton/",
                "ClearButton", new Point(62d, 70d), new Size(20, 20));

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
            string frontImagePath,
            string pullReadyImage,
            string basePath,
            List<Tuple<int, bool, double, double, string, string>> knobPostions,
            Point frontAdjust,
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
                frontAdjust: frontAdjust,
                frontPos: new Point(0, 0),
                pullJudgeAngle: pullJudgeAngle,
                pullJudgeDistance: pullJudgeDistance,
                prohibitOperate: prohibitOperate,
                pullable: pullable);

            return part;
        }


        private void Add3PosnToggle(string name, Point posn, Size size, string imagePath,
            ThreeWayToggleSwitchType toggleType,
            string imageBaseName, string interfaceDevice, string interfaceElement, 
            HeliosVisualRotation visualRotation = HeliosVisualRotation.None,
            bool horozontal = false, bool fromCenter = false)
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
                horizontal: horozontal,
                horizontalRender: horozontal,
                clickType: LinearClickType.Touch,
                fromCenter: fromCenter,
                visualRotation: visualRotation
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
                positionOneImage: imagePath + imageBaseName + "_03.png",
                positionTwoImage: imagePath + imageBaseName + "_01.png",
                defaultType: toggleType,
                interfaceDeviceName: interfaceDevice,
                interfaceElementName: interfaceElement,
                fromCenter: false);
        }

        private void Add2PosnToggleAlt(string name, Point posn, Size size, string imagePath,
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

        private void AddGuardCloseOnToggleState(string name, Point posn, Size size, string imagePath,
            string imageBaseName, int direction, Rect guardOpenRegion,
            Rect switchRegion, Rect guardCloseRegion, bool noUseClosable)
        {
            AddGuardCloseOnToggleState(
                name: name,
                posn: posn,
                size: size,
                guardDownImagePath: imagePath + imageBaseName + "_01.png",
                guardUpImagePath: imagePath + imageBaseName + "_03.png",
                direction: direction,
                guardOpenRegion: guardOpenRegion,
                switchRegion: switchRegion,
                guardCloseRegion: guardCloseRegion,
                noUseClosable: noUseClosable,
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
