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


    [HeliosControl("Helios.F5E.ArmPanel", "ArmPanel", "F-5E", typeof(BackgroundImageRenderer))]
    class ArmPanel : F5EDevice
    {
        private static readonly Rect SCREEN_RECT = new Rect(0, 0, 720, 295);
        private string _interfaceDeviceName = "ArmPanel";
        private string _imageAssetLocation = "{F-5E}/Images";
        private Rect _scaledScreenRect = SCREEN_RECT;
        private static readonly NLog.Logger Logger = NLog.LogManager.GetCurrentClassLogger();


        public ArmPanel()
            : base("ArmPanel", new Size(720, 295))
        {

            Children.Add(AddImage($"/ArmPanel/ArmPanel.png", new Point(0d, 0d), 720, 295));

            List<Tuple<int, bool, double, double, string, string>> knobPostions = new List<Tuple<int, bool, double, double, string, string>>
            {
                new Tuple<int, bool, double, double, string, string>(0, false,  0.0d,    0d,  "MissileVolumeKnob_01.png", "MissileVolumeKnob_01.png"),
                new Tuple<int, bool, double, double, string, string>(1, false,  0.028d,  10d, "MissileVolumeKnob_02.png", "MissileVolumeKnob_02.png"),
                new Tuple<int, bool, double, double, string, string>(2, false,  0.057d,  20d, "MissileVolumeKnob_03.png", "MissileVolumeKnob_03.png"),
                new Tuple<int, bool, double, double, string, string>(3, false,  0.085d,  30d, "MissileVolumeKnob_04.png", "MissileVolumeKnob_04.png"),
                new Tuple<int, bool, double, double, string, string>(4, false,  0.114d,  40d, "MissileVolumeKnob_05.png", "MissileVolumeKnob_05.png"),
                new Tuple<int, bool, double, double, string, string>(5, false,  0.143d,  50d, "MissileVolumeKnob_06.png", "MissileVolumeKnob_06.png"),
                new Tuple<int, bool, double, double, string, string>(6, false,  0.171d,  60d, "MissileVolumeKnob_07.png", "MissileVolumeKnob_07.png"),
                new Tuple<int, bool, double, double, string, string>(7, false,  0.2d,    70d, "MissileVolumeKnob_08.png", "MissileVolumeKnob_08.png"),
                new Tuple<int, bool, double, double, string, string>(8, false,  0.229d,  80d, "MissileVolumeKnob_09.png", "MissileVolumeKnob_09.png"),
                new Tuple<int, bool, double, double, string, string>(9, false,  0.257d,   90d, "MissileVolumeKnob_10.png", "MissileVolumeKnob_10.png"),
                new Tuple<int, bool, double, double, string, string>(10, false, 0.285d,  100d, "MissileVolumeKnob_11.png", "MissileVolumeKnob_11.png"),
                new Tuple<int, bool, double, double, string, string>(11, false, 0.314d,  110d, "MissileVolumeKnob_12.png", "MissileVolumeKnob_12.png"),
                new Tuple<int, bool, double, double, string, string>(12, false, 0.343d,  120d, "MissileVolumeKnob_13.png", "MissileVolumeKnob_13.png"),
                new Tuple<int, bool, double, double, string, string>(13, false, 0.371d,  130d, "MissileVolumeKnob_14.png", "MissileVolumeKnob_14.png"),
                new Tuple<int, bool, double, double, string, string>(14, false, 0.4d,    140d, "MissileVolumeKnob_15.png", "MissileVolumeKnob_15.png"),
                new Tuple<int, bool, double, double, string, string>(15, false, 0.428d,  150d, "MissileVolumeKnob_16.png", "MissileVolumeKnob_16.png"),
                new Tuple<int, bool, double, double, string, string>(16, false, 0.457d,  160d, "MissileVolumeKnob_17.png", "MissileVolumeKnob_17.png"),
                new Tuple<int, bool, double, double, string, string>(17, false, 0.485d,  170d, "MissileVolumeKnob_18.png", "MissileVolumeKnob_18.png"),
                new Tuple<int, bool, double, double, string, string>(18, false, 0.514d,  180d, "MissileVolumeKnob_19.png", "MissileVolumeKnob_19.png"),
                new Tuple<int, bool, double, double, string, string>(19, false, 0.542d,  190d, "MissileVolumeKnob_20.png", "MissileVolumeKnob_20.png"),
                new Tuple<int, bool, double, double, string, string>(20, false, 0.571d,  200d, "MissileVolumeKnob_21.png", "MissileVolumeKnob_21.png"),
                new Tuple<int, bool, double, double, string, string>(21, false, 0.6d,    210d, "MissileVolumeKnob_22.png", "MissileVolumeKnob_22.png"),
                new Tuple<int, bool, double, double, string, string>(22, false, 0.628d,  220d, "MissileVolumeKnob_23.png", "MissileVolumeKnob_23.png"),
                new Tuple<int, bool, double, double, string, string>(23, false, 0.657d,  230d, "MissileVolumeKnob_24.png", "MissileVolumeKnob_24.png"),
                new Tuple<int, bool, double, double, string, string>(24, false, 0.685d,  240d, "MissileVolumeKnob_25.png", "MissileVolumeKnob_25.png"),
                new Tuple<int, bool, double, double, string, string>(25, false, 0.714d,  250d, "MissileVolumeKnob_26.png", "MissileVolumeKnob_26.png"),
                new Tuple<int, bool, double, double, string, string>(26, false, 0.742d,  260d, "MissileVolumeKnob_27.png", "MissileVolumeKnob_27.png"),
                new Tuple<int, bool, double, double, string, string>(27, false, 0.771d,  270d, "MissileVolumeKnob_28.png", "MissileVolumeKnob_28.png"),
                new Tuple<int, bool, double, double, string, string>(28, false, 0.8d,    280d, "MissileVolumeKnob_29.png", "MissileVolumeKnob_29.png"),
                new Tuple<int, bool, double, double, string, string>(29, false, 0.829d,  290d, "MissileVolumeKnob_30.png", "MissileVolumeKnob_30.png"),
                new Tuple<int, bool, double, double, string, string>(30, false, 0.857d,  300d, "MissileVolumeKnob_31.png", "MissileVolumeKnob_31.png"),
                new Tuple<int, bool, double, double, string, string>(31, false, 0.885d,  310d, "MissileVolumeKnob_32.png", "MissileVolumeKnob_32.png"),
                new Tuple<int, bool, double, double, string, string>(32, false, 0.914d,  320d, "MissileVolumeKnob_33.png", "MissileVolumeKnob_33.png"),
                new Tuple<int, bool, double, double, string, string>(33, false, 0.942d,  330d, "MissileVolumeKnob_34.png", "MissileVolumeKnob_34.png"),
                new Tuple<int, bool, double, double, string, string>(34, false, 0.971d,  340d, "MissileVolumeKnob_35.png", "MissileVolumeKnob_35.png"),
                new Tuple<int, bool, double, double, string, string>(35, false, 1.0d,    350d, "MissileVolumeKnob_36.png", "MissileVolumeKnob_36.png"),
            };
            AddImageFlipParts("MissileVolumeKnob", "MissileVolumeKnob",
                new Point(9d, 178d), new Size(60, 60),
                "",
                "",
                "",
                "",
                $"{_imageAssetLocation}/FrontKnobs/MissileVolumeKnob/",
                knobPostions, new Point(0d, 0d), 0, 0, false, false);

            Add2PosnToggle(
                name: "L WINGTIP(ON/OFF)",
                posn: new Point(82d, 172d),
                size: new Size(63, 90),
                imagePath: $"{_imageAssetLocation}/MetalLevers/YellowToggle/",
                imageBaseName: "YellowKnob",
                interfaceDevice: _interfaceDeviceName,
                toggleType: ToggleSwitchType.OnOn,
                interfaceElement: "L WINGTIP(ON/OFF)",
                fromCenter: false);
            Add2PosnToggle(
                name: "L OUTBD(ON/OFF)",
                posn: new Point(154d, 170d),
                size: new Size(42, 92),
                imagePath: $"{_imageAssetLocation}/MetalLevers/FrontRedLever/",
                imageBaseName: "FrontRedLever",
                interfaceDevice: _interfaceDeviceName,
                toggleType: ToggleSwitchType.OnOn,
                interfaceElement: "L OUTBD(ON/OFF)",
                fromCenter: false);
            Add2PosnToggle(
                name: "L INBD(ON/OFF)",
                posn: new Point(216d, 170d),
                size: new Size(42, 92),
                imagePath: $"{_imageAssetLocation}/MetalLevers/FrontRedLever/",
                imageBaseName: "FrontRedLever",
                interfaceDevice: _interfaceDeviceName,
                toggleType: ToggleSwitchType.OnOn,
                interfaceElement: "L INBD(ON/OFF)",
                fromCenter: false);
            Add2PosnToggle(
                name: "CENTER(ON/OFF)",
                posn: new Point(278d, 172d),
                size: new Size(63, 90),
                imagePath: $"{_imageAssetLocation}/MetalLevers/YellowToggle/",
                imageBaseName: "YellowKnob",
                interfaceDevice: _interfaceDeviceName,
                toggleType: ToggleSwitchType.OnOn,
                interfaceElement: "CENTER(ON/OFF)",
                fromCenter: false);
            Add2PosnToggle(
                name: "R INBD(ON/OFF)",
                posn: new Point(350d, 170d),
                size: new Size(42, 92),
                imagePath: $"{_imageAssetLocation}/MetalLevers/FrontRedLever/",
                imageBaseName: "FrontRedLever",
                interfaceDevice: _interfaceDeviceName,
                toggleType: ToggleSwitchType.OnOn,
                interfaceElement: "R INBD(ON/OFF)",
                fromCenter: false);
            Add2PosnToggle(
                name: "R OUTBD(ON/OFF)",
                posn: new Point(412d, 170d),
                size: new Size(42, 92),
                imagePath: $"{_imageAssetLocation}/MetalLevers/FrontRedLever/",
                imageBaseName: "FrontRedLever",
                interfaceDevice: _interfaceDeviceName,
                toggleType: ToggleSwitchType.OnOn,
                interfaceElement: "R OUTBD(ON/OFF)",
                fromCenter: false);
            Add2PosnToggle(
                name: "R WINGTIP(ON/OFF)",
                posn: new Point(465d, 172d),
                size: new Size(63, 90),
                imagePath: $"{_imageAssetLocation}/MetalLevers/YellowToggle/",
                imageBaseName: "YellowKnob",
                interfaceDevice: _interfaceDeviceName,
                toggleType: ToggleSwitchType.OnOn,
                interfaceElement: "R WINGTIP(ON/OFF)",
                fromCenter: false);

            Add3PosnToggle(
                name: "IntervalSwitch",
                posn: new Point(75d, 58d),
                size: new Size(42, 90),
                imagePath: $"{_imageAssetLocation}/MetalLevers/FrontRedLever/",
                imageBaseName: "FrontRedLever",
                interfaceDevice: _interfaceDeviceName,
                interfaceElement: "IntervalSwitch",
                toggleType: ThreeWayToggleSwitchType.OnOnOn,
                fromCenter: false);

            List<Tuple<int, bool, double, double, string, string>> fusePosition = new List<Tuple<int, bool, double, double, string, string>>
            {
                // position:0 is dummy
                new Tuple<int, bool, double, double, string, string>(0, false, 0.2,  10d, "Fuse4wayLever_01.png", "Fuse4wayLever_01.png"),
                new Tuple<int, bool, double, double, string, string>(1, false, 0.4,  90d, "Fuse4wayLever_02.png", "Fuse4wayLever_02.png"),
                new Tuple<int, bool, double, double, string, string>(2, false, 0.6,  180d, "Fuse4wayLever_03.png", "Fuse4wayLever_03.png"),
                new Tuple<int, bool, double, double, string, string>(3, false, 0.8,  270d, "Fuse4wayLever_04.png", "Fuse4wayLever_04.png")
            };

            AddImageFlipParts("BombsArmSwitch", "BombsArmSwitch",
                new Point(168d, 47d), new Size(90, 90),
                "",
                "",
                "",
                "",
                $"{_imageAssetLocation}/MetalLevers/FourWayToggle/",
                fusePosition, new Point(0d, 0d), 0, 0, false, false);


            List<Tuple<int, bool, double, double, string, string>> selectorPosition = new List<Tuple<int, bool, double, double, string, string>>
            {
                // position:0 is dummy 
                new Tuple<int, bool, double, double, string, string>(0, false, 0,  180d, "ArrowKnob_05.png", "ArrowKnob_05.png"),
                new Tuple<int, bool, double, double, string, string>(1, false, 0.1,  135d, "ArrowKnob_10.png", "ArrowKnob_10.png"),
                new Tuple<int, bool, double, double, string, string>(2, false, 0.2,  90d,  "ArrowKnob_14.png", "ArrowKnob_14.png"),
                new Tuple<int, bool, double, double, string, string>(3, false, 0.3,  45d,  "ArrowKnob_19.png", "ArrowKnob_19.png")
            };
            AddImageFlipParts("ExternalStoresSelector", "ExternalStoresSelector",
                new Point(445d, 60d), new Size(75, 75),
                "",
                "",
                "",
                "",
                $"{_imageAssetLocation}/FrontKnobs/RoundArrowKnob/",
                selectorPosition, new Point(0d, 0d), 0, 0, false, false);

            Add3PosnToggle(
                name: "MasterArmSwitch",
                posn: new Point(290d, 15d),
                size: new Size(41, 142),
                imagePath: $"{_imageAssetLocation}/MetalLevers/MasterArmLever/",
                imageBaseName: "MasterArmLever",
                interfaceDevice: _interfaceDeviceName,
                interfaceElement: "MasterArmLever",
                toggleType: ThreeWayToggleSwitchType.OnOnOn,
                fromCenter: false);

            AddGuardCloseOnToggleState(
                name: "MasterArmCover",
                posn: new Point(290d, 20d),
                size: new Size(45, 130),
                imagePath: $"{_imageAssetLocation}/MetalLevers/MasterArmCover/",
                imageBaseName: "MasterArmCover",
                direction: 0,
                guardOpenRegion: new Rect(0, 0, 30, 20),
                switchRegion: new Rect(0, 20, 30, 50),
                guardCloseRegion: new Rect(0, 86, 30, 20),
                noUseClosable:true);

            AddPushButton("IntervalMidAssist", $"{_imageAssetLocation}/ClearButton/",
                "ClearButton", new Point(122d, 90d), new Size(20, 20));
            AddPushButton("MasterMidAssist", $"{_imageAssetLocation}/ClearButton/",
                "ClearButton", new Point(332d, 80d), new Size(20, 20));
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
                positionOneImage: imagePath + imageBaseName + "_03.png",
                positionTwoImage: imagePath + imageBaseName + "_01.png",
                defaultType: toggleType,
                interfaceDeviceName: interfaceDevice,
                interfaceElementName: interfaceElement,
                fromCenter: false);
        }

        private void AddGuardCloseOnToggleState(string name, Point posn, Size size, string imagePath,
            string imageBaseName,int direction,Rect guardOpenRegion,
            Rect switchRegion, Rect guardCloseRegion, bool noUseClosable)
        {
            AddGuardCloseOnToggleState(
                name: name,
                posn: posn,
                size: size,
                guardDownImagePath: imagePath + imageBaseName + "_01.png",
                guardUpImagePath: imagePath + imageBaseName + "_03.png",
                noUseClosable: noUseClosable,
                direction: direction,
                guardOpenRegion: guardOpenRegion,
                switchRegion: switchRegion,
                guardCloseRegion: guardCloseRegion,
                fromCenter: false);
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
