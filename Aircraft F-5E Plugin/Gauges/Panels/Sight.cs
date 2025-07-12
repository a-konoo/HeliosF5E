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

    [HeliosControl("Helios.F5E.Sight", "Sight", "F-5E", typeof(BackgroundImageRenderer))]
    class Sight : F5EDevice
    {
        private static readonly Rect SCREEN_RECT = new Rect(0, 0, 428, 294);
        private string _interfaceDeviceName = "Sight";
        private string _imageAssetLocation = "{F-5E}/Images";
        private Rect _scaledScreenRect = SCREEN_RECT;


        public Sight()
            : base("Sight", new Size(428, 294))
        {
            Children.Add(AddImage($"/Sight/SightBody.png", new Point(0d, 0d), 428, 294));

            List<Tuple<int, bool, double, double, string, string>> reticleKnobPos = new List<Tuple<int, bool, double, double, string, string>>
            {
                new Tuple<int, bool, double, double, string, string>(0, false,  0.005d,    0d, "RaderBlackKnob_36.png", "RaderBlackKnob_36.png"),
                new Tuple<int, bool, double, double, string, string>(1, false,  0.005d,   10d, "RaderBlackKnob_35.png", "RaderBlackKnob_35.png"),
                new Tuple<int, bool, double, double, string, string>(2, false,  0.005d,   20d, "RaderBlackKnob_34.png", "RaderBlackKnob_34.png"),
                new Tuple<int, bool, double, double, string, string>(3, false,  0.005d,   30d, "RaderBlackKnob_33.png", "RaderBlackKnob_33.png"),
                new Tuple<int, bool, double, double, string, string>(4, false,  0.005d,   40d, "RaderBlackKnob_32.png", "RaderBlackKnob_32.png"),
                new Tuple<int, bool, double, double, string, string>(5, false,  0.005d,   50d, "RaderBlackKnob_31.png", "RaderBlackKnob_31.png"),
                new Tuple<int, bool, double, double, string, string>(6, false,  0.005d,   60d, "RaderBlackKnob_30.png", "RaderBlackKnob_30.png"),
                new Tuple<int, bool, double, double, string, string>(7, false,  0.005d,   70d, "RaderBlackKnob_29.png", "RaderBlackKnob_29.png"),
                new Tuple<int, bool, double, double, string, string>(8, false,  0.005d,   80d, "RaderBlackKnob_28.png", "RaderBlackKnob_28.png"),
                new Tuple<int, bool, double, double, string, string>(9, false,  0.005d,   90d, "RaderBlackKnob_27.png", "RaderBlackKnob_27.png"),
                new Tuple<int, bool, double, double, string, string>(10, false, 0.005d,  100d, "RaderBlackKnob_26.png", "RaderBlackKnob_26.png"),
                new Tuple<int, bool, double, double, string, string>(11, false, 0.005d,  110d, "RaderBlackKnob_25.png", "RaderBlackKnob_25.png"),
                new Tuple<int, bool, double, double, string, string>(12, false, 0.005d,  120d, "RaderBlackKnob_24.png", "RaderBlackKnob_24.png"),
                new Tuple<int, bool, double, double, string, string>(13, false, 0.005d,  130d, "RaderBlackKnob_23.png", "RaderBlackKnob_23.png"),
                new Tuple<int, bool, double, double, string, string>(14, false, 0.005d,  140d, "RaderBlackKnob_22.png", "RaderBlackKnob_22.png"),
                new Tuple<int, bool, double, double, string, string>(15, false, 0.005d,  150d, "RaderBlackKnob_21.png", "RaderBlackKnob_21.png"),
                new Tuple<int, bool, double, double, string, string>(16, false, 0.005d,  160d, "RaderBlackKnob_20.png", "RaderBlackKnob_20.png"),
                new Tuple<int, bool, double, double, string, string>(17, false, 0.005d,  170d, "RaderBlackKnob_19.png", "RaderBlackKnob_19.png"),
                new Tuple<int, bool, double, double, string, string>(18, false, 0.005d,  180d, "RaderBlackKnob_18.png", "RaderBlackKnob_18.png"),
                new Tuple<int, bool, double, double, string, string>(19, false, 0.005d,  190d, "RaderBlackKnob_17.png", "RaderBlackKnob_17.png"),
                new Tuple<int, bool, double, double, string, string>(20, false, 0.005d,  200d, "RaderBlackKnob_16.png", "RaderBlackKnob_16.png"),
                new Tuple<int, bool, double, double, string, string>(21, false, 0.005d,  210d, "RaderBlackKnob_15.png", "RaderBlackKnob_15.png"),
                new Tuple<int, bool, double, double, string, string>(22, false, 0.005d,  220d, "RaderBlackKnob_14.png", "RaderBlackKnob_14.png"),
                new Tuple<int, bool, double, double, string, string>(23, false, 0.005d,  230d, "RaderBlackKnob_13.png", "RaderBlackKnob_13.png"),
                new Tuple<int, bool, double, double, string, string>(24, false, 0.005d,  240d, "RaderBlackKnob_12.png", "RaderBlackKnob_12.png"),
                new Tuple<int, bool, double, double, string, string>(25, false, 0.005d,  250d, "RaderBlackKnob_11.png", "RaderBlackKnob_11.png"),
                new Tuple<int, bool, double, double, string, string>(26, false, 0.005d,  260d, "RaderBlackKnob_10.png", "RaderBlackKnob_10.png"),
                new Tuple<int, bool, double, double, string, string>(27, false, 0.005d,  270d, "RaderBlackKnob_09.png", "RaderBlackKnob_09.png"),
                new Tuple<int, bool, double, double, string, string>(28, false, 0.005d,  280d, "RaderBlackKnob_08.png", "RaderBlackKnob_08.png"),
                new Tuple<int, bool, double, double, string, string>(29, false, 0.005d,  290d, "RaderBlackKnob_07.png", "RaderBlackKnob_07.png"),
                new Tuple<int, bool, double, double, string, string>(30, false, 0.005d,  300d, "RaderBlackKnob_06.png", "RaderBlackKnob_06.png"),
                new Tuple<int, bool, double, double, string, string>(31, false, 0.005d,  310d, "RaderBlackKnob_05.png", "RaderBlackKnob_05.png"),
                new Tuple<int, bool, double, double, string, string>(32, false, 0.005d,  320d, "RaderBlackKnob_04.png", "RaderBlackKnob_04.png"),
                new Tuple<int, bool, double, double, string, string>(33, false, 0.005d,  330d, "RaderBlackKnob_03.png", "RaderBlackKnob_03.png"),
                new Tuple<int, bool, double, double, string, string>(34, false, 0.005d,  340d, "RaderBlackKnob_02.png", "RaderBlackKnob_02.png"),
                new Tuple<int, bool, double, double, string, string>(35, false, 0.005d,  350d, "RaderBlackKnob_01.png", "RaderBlackKnob_01.png"),
            };

            AddFlipRotaryEncoder("ReticleDeprKnob","ReticleDeprKnob",
                new Point(280d, 108d), new Size(55, 55),
                "",
                "",
                "",
                "",
                $"{_imageAssetLocation}/FrontKnobs/RoundMetalBlackKnob/",
                reticleKnobPos, 0, 0, 40d, 45d, false);

            List<Tuple<int, bool, double, double, string, string>> intentKnobPos = new List<Tuple<int, bool, double, double, string, string>>
            {
                new Tuple<int, bool, double, double, string, string>(0, false,  0.005d,    0d, "RaderBlackKnob_36.png", "RaderBlackKnob_36.png"),
                new Tuple<int, bool, double, double, string, string>(1, false,  0.005d,   10d, "RaderBlackKnob_35.png", "RaderBlackKnob_35.png"),
                new Tuple<int, bool, double, double, string, string>(2, false,  0.005d,   20d, "RaderBlackKnob_34.png", "RaderBlackKnob_34.png"),
                new Tuple<int, bool, double, double, string, string>(3, false,  0.005d,   30d, "RaderBlackKnob_33.png", "RaderBlackKnob_33.png"),
                new Tuple<int, bool, double, double, string, string>(4, false,  0.005d,   40d, "RaderBlackKnob_32.png", "RaderBlackKnob_32.png"),
                new Tuple<int, bool, double, double, string, string>(5, false,  0.005d,   50d, "RaderBlackKnob_31.png", "RaderBlackKnob_31.png"),
                new Tuple<int, bool, double, double, string, string>(6, false,  0.005d,   60d, "RaderBlackKnob_30.png", "RaderBlackKnob_30.png"),
                new Tuple<int, bool, double, double, string, string>(7, false,  0.005d,   70d, "RaderBlackKnob_29.png", "RaderBlackKnob_29.png"),
                new Tuple<int, bool, double, double, string, string>(8, false,  0.005d,   80d, "RaderBlackKnob_28.png", "RaderBlackKnob_28.png"),
                new Tuple<int, bool, double, double, string, string>(9, false,  0.005d,   90d, "RaderBlackKnob_27.png", "RaderBlackKnob_27.png"),
                new Tuple<int, bool, double, double, string, string>(10, false, 0.005d,  100d, "RaderBlackKnob_26.png", "RaderBlackKnob_26.png"),
                new Tuple<int, bool, double, double, string, string>(11, false, 0.005d,  110d, "RaderBlackKnob_25.png", "RaderBlackKnob_25.png"),
                new Tuple<int, bool, double, double, string, string>(12, false, 0.005d,  120d, "RaderBlackKnob_24.png", "RaderBlackKnob_24.png"),
                new Tuple<int, bool, double, double, string, string>(13, false, 0.005d,  130d, "RaderBlackKnob_23.png", "RaderBlackKnob_23.png"),
                new Tuple<int, bool, double, double, string, string>(14, false, 0.005d,  140d, "RaderBlackKnob_22.png", "RaderBlackKnob_22.png"),
                new Tuple<int, bool, double, double, string, string>(15, false, 0.005d,  150d, "RaderBlackKnob_21.png", "RaderBlackKnob_21.png"),
                new Tuple<int, bool, double, double, string, string>(16, false, 0.005d,  160d, "RaderBlackKnob_20.png", "RaderBlackKnob_20.png"),
                new Tuple<int, bool, double, double, string, string>(17, false, 0.005d,  170d, "RaderBlackKnob_19.png", "RaderBlackKnob_19.png"),
                new Tuple<int, bool, double, double, string, string>(18, false, 0.005d,  180d, "RaderBlackKnob_18.png", "RaderBlackKnob_18.png"),
                new Tuple<int, bool, double, double, string, string>(19, false, 0.005d,  190d, "RaderBlackKnob_17.png", "RaderBlackKnob_17.png"),
                new Tuple<int, bool, double, double, string, string>(20, false, 0.005d,  200d, "RaderBlackKnob_16.png", "RaderBlackKnob_16.png"),
                new Tuple<int, bool, double, double, string, string>(21, false, 0.005d,  210d, "RaderBlackKnob_15.png", "RaderBlackKnob_15.png"),
                new Tuple<int, bool, double, double, string, string>(22, false, 0.005d,  220d, "RaderBlackKnob_14.png", "RaderBlackKnob_14.png"),
                new Tuple<int, bool, double, double, string, string>(23, false, 0.005d,  230d, "RaderBlackKnob_13.png", "RaderBlackKnob_13.png"),
                new Tuple<int, bool, double, double, string, string>(24, false, 0.005d,  240d, "RaderBlackKnob_12.png", "RaderBlackKnob_12.png"),
                new Tuple<int, bool, double, double, string, string>(25, false, 0.005d,  250d, "RaderBlackKnob_11.png", "RaderBlackKnob_11.png"),
                new Tuple<int, bool, double, double, string, string>(26, false, 0.005d,  260d, "RaderBlackKnob_10.png", "RaderBlackKnob_10.png"),
                new Tuple<int, bool, double, double, string, string>(27, false, 0.005d,  270d, "RaderBlackKnob_09.png", "RaderBlackKnob_09.png"),
                new Tuple<int, bool, double, double, string, string>(28, false, 0.005d,  280d, "RaderBlackKnob_08.png", "RaderBlackKnob_08.png"),
                new Tuple<int, bool, double, double, string, string>(29, false, 0.005d,  290d, "RaderBlackKnob_07.png", "RaderBlackKnob_07.png"),
                new Tuple<int, bool, double, double, string, string>(30, false, 0.005d,  300d, "RaderBlackKnob_06.png", "RaderBlackKnob_06.png"),
                new Tuple<int, bool, double, double, string, string>(31, false, 0.005d,  310d, "RaderBlackKnob_05.png", "RaderBlackKnob_05.png"),
                new Tuple<int, bool, double, double, string, string>(32, false, 0.005d,  320d, "RaderBlackKnob_04.png", "RaderBlackKnob_04.png"),
                new Tuple<int, bool, double, double, string, string>(33, false, 0.005d,  330d, "RaderBlackKnob_03.png", "RaderBlackKnob_03.png"),
                new Tuple<int, bool, double, double, string, string>(34, false, 0.005d,  340d, "RaderBlackKnob_02.png", "RaderBlackKnob_02.png"),
                new Tuple<int, bool, double, double, string, string>(35, false, 0.005d,  350d, "RaderBlackKnob_01.png", "RaderBlackKnob_01.png"),
            };


            AddImageFlipParts("ReticleIntensityKnob", "ReticleIntensityKnob",
                new Point(195d, 140d), new Size(55, 55),
                "",
                "",
                "",
                "",
                $"{_imageAssetLocation}/FrontKnobs/RoundMetalBlackKnob/",
                intentKnobPos, new Point(0, 0), 0, 0, 1.0, false, false);

            List<Tuple<int, bool, double, double, string, string>> selectorKnob = new List<Tuple<int, bool, double, double, string, string>>
            {
                new Tuple<int, bool, double, double, string, string>(0, false,  1d,    0d, "SightModeSelector_01.png", "SightModeSelector_01.png"),
                new Tuple<int, bool, double, double, string, string>(1, false,  2d,   45d, "SightModeSelector_02.png", "SightModeSelector_02.png"),
                new Tuple<int, bool, double, double, string, string>(2, false,  3d,   90d, "SightModeSelector_03.png", "SightModeSelector_03.png"),
                new Tuple<int, bool, double, double, string, string>(3, false,  4d,  135d, "SightModeSelector_04.png", "SightModeSelector_04.png"),
                new Tuple<int, bool, double, double, string, string>(4, false,  5d,  180d, "SightModeSelector_05.png", "SightModeSelector_05.png")
            };
            AddImageFlipParts("SightModeSelector", "SightModeSelector",
                new Point(128d, 140d), new Size(45, 45),
                "",
                "",
                "",
                "",
                $"{_imageAssetLocation}/FrontKnobs/SightModeSelector/",
                selectorKnob, new Point(0,0), 0, 0, 1.0, false, false);

            AddToggledButton(
                "AN/ASG-31 Light Switch",
                $"{_imageAssetLocation}/Sight/PnltButton/",
                "pnltButton",
                PushButtonType.Toggle,
                new Point(130d, 235d), new Size(45, 50)
            );

            List<Tuple<int, bool, double, double, string, string>> bitLeverKnob = new List<Tuple<int, bool, double, double, string, string>>
            {
                new Tuple<int, bool, double, double, string, string>(0, false, -0.1d,    0d,  "BitLever_01.png", "BitLever_01.png"),
                new Tuple<int, bool, double, double, string, string>(1, false,  0.0d,   45d,  "BitLever_02.png", "BitLever_02.png"),
                new Tuple<int, bool, double, double, string, string>(2, false,  0.1d,   90d,  "BitLever_03.png", "BitLever_03.png"),
            };

            AddImageFlipParts("BitLever", "BitLever",
                new Point(280d, 235d), new Size(45, 50),
                "",
                "",
                "",
                "",
                $"{_imageAssetLocation}/Sight/BitLever/",
                bitLeverKnob, new Point(0, 0), 0, 0, 1.0, false, true);
            AddPushButton("Bit1Button", $"{_imageAssetLocation}/ClearButton/",
              "ClearButton", new Point(279d, 220d), new Size(20, 20));
            AddPushButton("BitOFFButton", $"{_imageAssetLocation}/ClearButton/",
              "ClearButton", new Point(279d, 245d), new Size(20, 20));
            AddPushButton("Bit2Button", $"{_imageAssetLocation}/ClearButton/",
              "ClearButton", new Point(279d, 270d), new Size(20, 20));
           

            var displayPosnList = new List<Point>();
            var tapePathList = new List<String>();
            displayPosnList.Add(new Point(0.5d, 11.5d));
            tapePathList.Add("{F-5E}/Gauges/TenOneCounter/drum_zero_tape.xaml");
            displayPosnList.Add(new Point(16.5d, 11.5d));
            tapePathList.Add("{F-5E}/Gauges/TenOneCounter/drum_zero_tape.xaml");
            displayPosnList.Add(new Point(32.5d, 11.5d));
            tapePathList.Add("{F-5E}/Gauges/TenOneCounter/drum_zero_tape.xaml");

            var gaugeFunc = new Action<double, GaugeDrumCounter[]>((p, d) =>
            {
                d[0].Value = Math.Floor(p / 100d);
                p -= Math.Floor(d[0].Value * 100d);
                d[1].Value = Math.Floor(p / 10d);
                p -= Math.Floor(d[1].Value * 10d);
                d[2].Value = p;
            });

            AddDrumCounterToPanel("DepressionDrums", "DepressionDrums", new Point(274d, 73d),
                new Size(36d, 30d), displayPosnList.ToArray(), tapePathList.ToArray(), new Size(10d, 15d),
                new Size(13d, 20d), gaugeFunc);
            
            AddSlipGauge("SlipGauge", new Point(155d, 87d), new Size(110, 22), _interfaceDeviceName, "SlipGauge");

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
            double frontRatio,
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
                frontRatio: frontRatio,
                prohibitOperate: prohibitOperate,
                pullable: pullable);

            return part;
        }


        private FlipRotaryEncoder AddFlipRotaryEncoder(string name, string actionIdentifier,
            Point posn, Size size,
            string knobImagePath,
            string knobImagePulledPath,
            string frontImagePath,
            string pullReadyImage,
            string basePath,
            List<Tuple<int, bool, double, double, string, string>> knobPostions,
            double pullJudgeAngle,
            double pullJudgeDistance,
            double thresholdAngle,
            double thresholdDistance,
            bool pullable)
        {
            FlipRotaryEncoder part = AddFlipRotaryEncoder(
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
                frontRatio: 1.0d,
                pullJudgeAngle: pullJudgeAngle,
                pullJudgeDistance: pullJudgeDistance,
                thresholdAngle: thresholdAngle,
                thresholdDistance: thresholdDistance,
                pullable: pullable);

            return part;
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

        private void AddToggledButton(string name, string imageBasePath, string imageFileName,
            PushButtonType pushButtonType ,Point posn, Size size)
        {
            AddToggledButton(name: name,
                posn: posn,
                size: size,
                image: imageBasePath + imageFileName + "_OFF.png",
                pushedImage: imageBasePath + imageFileName + "_PUSHED.png",
                buttonText: "",
                interfaceDeviceName: _interfaceDeviceName,
                interfaceElementName: name,
                pushButtonType: pushButtonType,
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
