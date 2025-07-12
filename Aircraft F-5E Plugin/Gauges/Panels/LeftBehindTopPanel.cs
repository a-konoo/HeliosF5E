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


    [HeliosControl("Helios.F5E.LeftBehindTopPanel", "LeftBehindTopPanel", "F-5E", typeof(BackgroundImageRenderer))]
    class LeftBehindTopPanel : F5EDevice
    {
        private static readonly Rect SCREEN_RECT = new Rect(0, 0, 620, 800);
        private string _interfaceDeviceName = "LeftBehindTopPanel";
        private string _imageAssetLocation = "{F-5E}/Images";
        private Rect _scaledScreenRect = SCREEN_RECT;
        private static readonly NLog.Logger Logger = NLog.LogManager.GetCurrentClassLogger();


        public LeftBehindTopPanel()
            : base("LeftBehindTopPanel", new Size(620, 800))
        {

            Children.Add(AddImage($"/LeftSidePanel/F5E_LEFT_TOP/F5E_LeftBehindTop.png", new Point(0d, 0d), 620, 800));
            Children.Add(AddImage($"/Misc/TiltScroll.png", new Point(262d, -5d), 50, 90));
            Children.Add(AddImage($"/Misc/CursorRound.png", new Point(328d, 5d), 88, 48));

            List<Tuple<int, bool, double, double, string, string>> lightPostions = new List<Tuple<int, bool, double, double, string, string>>
            {
                new Tuple<int, bool, double, double, string, string>(0, false, -1.0d,   0d, "RoundArrowKnob_06.png", "RoundArrowKnob_06.png"),
                new Tuple<int, bool, double, double, string, string>(1, false, -0.8d,  10d, "RoundArrowKnob_07.png", "RoundArrowKnob_07.png"),
                new Tuple<int, bool, double, double, string, string>(2, false, -0.6d,  20d, "RoundArrowKnob_08.png", "RoundArrowKnob_08.png"),
                new Tuple<int, bool, double, double, string, string>(3, false, -0.4d,  30d, "RoundArrowKnob_09.png", "RoundArrowKnob_09.png"),
                new Tuple<int, bool, double, double, string, string>(4, false, -0.2d,  40d, "RoundArrowKnob_10.png", "RoundArrowKnob_10.png"),
                new Tuple<int, bool, double, double, string, string>(5, false,  0d,    50d, "RoundArrowKnob_11.png", "RoundArrowKnob_11.png"),
                new Tuple<int, bool, double, double, string, string>(6, false,  0.2d,  60d, "RoundArrowKnob_12.png", "RoundArrowKnob_12.png"),
                new Tuple<int, bool, double, double, string, string>(7, false,  0.4d,  70d, "RoundArrowKnob_13.png", "RoundArrowKnob_13.png"),
                new Tuple<int, bool, double, double, string, string>(8, false,  0.6d,  80d, "RoundArrowKnob_14.png", "RoundArrowKnob_14.png"),
                new Tuple<int, bool, double, double, string, string>(9, false, 0.9d,  90d, "RoundArrowKnob_15.png", "RoundArrowKnob_15.png"),
                new Tuple<int, bool, double, double, string, string>(10, false, 1.0d, 100d, "RoundArrowKnob_16.png", "RoundArrowKnob_16.png"),
            };
            AddImageFlipParts("RudderTrim", "RudderTrimVolume",
                new Point(398d, 433d), new Size(130d, 130d),
                "",
                "",
                "",
                "",
                $"{_imageAssetLocation}/FrontKnobs/RoundLightKnob/",
                lightPostions, new Point(0d, 0d), 0, 0, false, false);


            List<Tuple<int, bool, double, double, string, string>> selectorPosition = new List<Tuple<int, bool, double, double, string, string>>
            {
                new Tuple<int, bool, double, double, string, string>(0, false,  0,    0d, "RoundArrowKnob_08.png", "RoundArrowKnob_08.png"),
                new Tuple<int, bool, double, double, string, string>(1, false,  0.1d,    10d, "RoundArrowKnob_10.png", "RoundArrowKnob_10.png"),
                new Tuple<int, bool, double, double, string, string>(2, false,  0.2d,    30d, "RoundArrowKnob_13.png", "RoundArrowKnob_13.png"),
                new Tuple<int, bool, double, double, string, string>(3, false,  0.3d,    50d, "RoundArrowKnob_16.png", "RoundArrowKnob_16.png")
            };
            AddImageFlipParts("RaderModeSelector", "RaderModeSelector",
                new Point(452d, 224d), new Size(110,110),
                "",
                "",
                "",
                "",
                $"{_imageAssetLocation}/FrontKnobs/RoundLightKnob/",
                selectorPosition, new Point(0d, 0d), 0, 0, false, false);

            List<Tuple<int, bool, double, double, string, string>> rangePosition = new List<Tuple<int, bool, double, double, string, string>>
            {
                new Tuple<int, bool, double, double, string, string>(0, false,  0,    0d,  "F5E_RADERKNOB_01.png", "F5E_RADERKNOB_01.png"),
                new Tuple<int, bool, double, double, string, string>(1, false,  0.1d,    10d, "F5E_RADERKNOB_02.png", "F5E_RADERKNOB_02.png"),
                new Tuple<int, bool, double, double, string, string>(2, false,  0.2d,    30d, "F5E_RADERKNOB_03.png", "F5E_RADERKNOB_03.png"),
                new Tuple<int, bool, double, double, string, string>(3, false,  0.3d,    50d, "F5E_RADERKNOB_04.png", "F5E_RADERKNOB_04.png")
            };
            AddImageFlipParts("RaderRangeSelector", "RaderRangeSelector",
                new Point(475d, 82d), new Size(75, 87),
                "",
                "",
                "",
                "",
                $"{_imageAssetLocation}/LeftSidePanel/RangeKnob/",
                rangePosition, new Point(0d, 0d), 0, 0, false, false);



            // DAMPER Toggle
            Add2PosnToggle(
                name: "PitchDamperToggle",
                posn: new Point(209d, 467d),
                size: new Size(50, 110),
                imagePath: $"{_imageAssetLocation}/LeftSidePanel/CMWSToggle/",
                imageBaseName: "JettiToggle",
                interfaceDevice: _interfaceDeviceName,
                interfaceElement: "PitchDamperToggle",
                toggleType: ToggleSwitchType.OnOn,
                fromCenter: false);

            Add2PosnToggle(
                name: "YawDamperToggle",
                posn: new Point(286d, 467d),
                size: new Size(50, 110),
                imagePath: $"{_imageAssetLocation}/LeftSidePanel/CMWSToggle/",
                imageBaseName: "JettiToggle",
                interfaceDevice: _interfaceDeviceName,
                interfaceElement: "YawDamperToggle",
                toggleType: ToggleSwitchType.OnOn,
                fromCenter: false);


            List<Tuple<int, bool, double, double, string, string>> chaffSelector = new List<Tuple<int, bool, double, double, string, string>>
            {
                new Tuple<int, bool, double, double, string, string>(0, false,  0.0,    0d,  "CHAFF_01.png", "CHAFF_01.png"),
                new Tuple<int, bool, double, double, string, string>(1, false,  0.1,    10d, "CHAFF_02.png", "CHAFF_02.png"),
                new Tuple<int, bool, double, double, string, string>(2, false,  0.2,    30d, "CHAFF_03.png", "CHAFF_03.png"),
                new Tuple<int, bool, double, double, string, string>(3, false,  0.3,    50d, "CHAFF_04.png", "CHAFF_04.png")
            };
            List<Tuple<int, bool, double, double, string, string>> flareSelector = new List<Tuple<int, bool, double, double, string, string>>
            {
                new Tuple<int, bool, double, double, string, string>(0, false,  0.0,    0d,  "CHAFF_01.png", "CHAFF_01.png"),
                new Tuple<int, bool, double, double, string, string>(1, false,  0.1,    10d, "CHAFF_02.png", "CHAFF_02.png"),
                new Tuple<int, bool, double, double, string, string>(2, false,  0.2,    50d, "CHAFF_03.png", "CHAFF_03.png")
            };
            AddImageFlipParts("ChaffModeSelector", "ChaffModeSelector",
                new Point(214d, 682d), new Size(60, 72),
                "",
                "",
                "",
                "",
                $"{_imageAssetLocation}/LeftSidePanel/CMWSKnob/",
                chaffSelector, new Point(0d, 0d), 0, 0, false, false);

            AddImageFlipParts("FlareModeSelector", "FlareModeSelector",
                new Point(381d, 682d), new Size(60, 72),
                "",
                "",
                "",
                "",
                $"{_imageAssetLocation}/LeftSidePanel/CMWSKnob/",
                flareSelector, new Point(0d, 0d), 0, 0, false, false);

            // Chaff /Flare TenCounter
            var displayPosnList = new List<Point>();
            var tapePathList = new List<String>();
            displayPosnList.Add(new Point(15.5d, 11.5d));
            tapePathList.Add("{F-5E}/Gauges/TenOneCounter/drum_nozero_tape.xaml");
            displayPosnList.Add(new Point(39.5d, 11.5d));
            tapePathList.Add("{F-5E}/Gauges/TenOneCounter/drum_zero_tape.xaml");

            var gaugeFunc = new Action<double, GaugeDrumCounter[]>((p, d) =>
            {
                d[0].Value = Math.Floor(p / 10d);
                p -= Math.Floor(d[0].Value * 10d);
                d[1].Value = p;
            });

            AddDrumCounterToPanel("ChaffDrumCounter", "ChaffDrumCounter", new Point(264d, 720d),
                new Size(40d, 28d), displayPosnList.ToArray(), tapePathList.ToArray(), new Size(10d, 15d),
                new Size(20d, 28d), gaugeFunc);

            AddDrumCounterToPanel("FlareDrumCounter", "FlareDrumCounter", new Point(427d, 720d),
                new Size(40d, 28d), displayPosnList.ToArray(), tapePathList.ToArray(), new Size(10d, 15d),
                new Size(20d, 28d), gaugeFunc);



            Add2PosnToggle(
                name: "JettisonToggle",
                posn: new Point(521d, 640d),
                size: new Size(40, 90),
                imagePath: $"{_imageAssetLocation}/LeftSidePanel/CMWSToggle/",
                imageBaseName: "JettiToggle",
                interfaceDevice: _interfaceDeviceName,
                interfaceElement: "JettiToggle",
                toggleType: ToggleSwitchType.OnOn,
                fromCenter: false);

            AddGuardCloseOnToggleState(
                name: "CMWSJettisonCover",
                posn: new Point(491d, 610d),
                size: new Size(100, 170),
                imagePath: $"{_imageAssetLocation}/LeftSidePanel/CMWSCover/",
                imageBaseName: "JettiCover",
                direction: 0,
                guardOpenRegion: new Rect(0, 100, 100, 70),
                switchRegion: new Rect(0, 0, 100, 10),
                guardCloseRegion: new Rect(0, 0, 100, 70),
                noUseClosable:true);

            AddPushButton("AcqButton", $"{_imageAssetLocation}/LeftSidePanel/AcqButton/",
                "ACQButtonTop", new Point(415d, 83d), new Size(40, 60));
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

        private DrumCounter AddDrumCounterToPanel(
            string name,
            string actionIdentifier,
            Point posn,
            Size size,
            Point[] displayPosnList,
            String[] tapePathList,
            Size digitSize,
            Size displaySize,
            Action<Double, GaugeDrumCounter[]> argFunc)
        {
            DrumCounter part = AddDrumCounter(
                name: name,
                fromCenter: false,
                interfaceDeviceName: _interfaceDeviceName,
                interfaceElementName: actionIdentifier,
                actionIdentifier: name,
                posn: posn,
                size: size,
                displayPosnList: displayPosnList,
                tapePathList: tapePathList,
                digitSize: digitSize,
                displaySize: displaySize,
                argFunc: argFunc);

            return part;
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
