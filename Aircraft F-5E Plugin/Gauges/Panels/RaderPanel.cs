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

    [HeliosControl("Helios.F5E.RaderPanel", "RaderPanel", "F-5E", typeof(BackgroundImageRenderer))]
    class RaderPanel : F5EDevice
    {
        private static readonly Rect SCREEN_RECT = new Rect(0, 0, 450, 436);
        private string _interfaceDeviceName = "RaderPanel";
        private string _imageAssetLocation = "{F-5E}/Images/RaderPanel";
        private Rect _scaledScreenRect = SCREEN_RECT;


        public RaderPanel()
            : base("RaderPanel", new Size(450, 436))
        {

            Children.Add(AddImage($"/RaderPanel.png", new Point(0d, 0d), 450, 436));

            AddRaderPanelBody("RaderPanelBody", false, "RaderPanelBody", "RaderPanelBody",
                "RaderPanelBody", new Point(0d, 0d), new Size(450d, 436d));
            List<Tuple<int, bool, double, double, string, string>> knobPostions = new List<Tuple<int, bool, double, double, string, string>>
            {
                new Tuple<int, bool, double, double, string, string>(0, false,  -1d,      0d,  "RaderPitchKnob_54.png", "RaderPitchKnob_54.png"),
                new Tuple<int, bool, double, double, string, string>(1, false,  -0.962d,  5d,  "RaderPitchKnob_53.png", "RaderPitchKnob_53.png"),
                new Tuple<int, bool, double, double, string, string>(2, false,  -0.926d, 10d,  "RaderPitchKnob_52.png", "RaderPitchKnob_52.png"),
                new Tuple<int, bool, double, double, string, string>(3, false,  -0.888d, 15d,  "RaderPitchKnob_51.png", "RaderPitchKnob_51.png"),
                new Tuple<int, bool, double, double, string, string>(4, false,  -0.851d, 20d,  "RaderPitchKnob_50.png", "RaderPitchKnob_50.png"),
                new Tuple<int, bool, double, double, string, string>(5, false,  -0.814d, 25d,  "RaderPitchKnob_49.png", "RaderPitchKnob_49.png"),
                new Tuple<int, bool, double, double, string, string>(6, false,  -0.777d, 30d,  "RaderPitchKnob_48.png", "RaderPitchKnob_48.png"),
                new Tuple<int, bool, double, double, string, string>(7, false,  -0.740d, 35d,  "RaderPitchKnob_47.png", "RaderPitchKnob_47.png"),
                new Tuple<int, bool, double, double, string, string>(8, false,  -0.703d, 40d,  "RaderPitchKnob_46.png", "RaderPitchKnob_46.png"),
                new Tuple<int, bool, double, double, string, string>(9, false,  -0.666d, 45d,  "RaderPitchKnob_45.png", "RaderPitchKnob_45.png"),
                new Tuple<int, bool, double, double, string, string>(10, false, -0.629d, 50d,  "RaderPitchKnob_44.png", "RaderPitchKnob_44.png"),
                new Tuple<int, bool, double, double, string, string>(11, false, -0.593d, 55d,  "RaderPitchKnob_43.png", "RaderPitchKnob_43.png"),
                new Tuple<int, bool, double, double, string, string>(12, false, -0.555d, 60d,  "RaderPitchKnob_42.png", "RaderPitchKnob_42.png"),
                new Tuple<int, bool, double, double, string, string>(13, false, -0.518d, 70d,  "RaderPitchKnob_41.png", "RaderPitchKnob_41.png"),
                new Tuple<int, bool, double, double, string, string>(14, false, -0.481d, 75d,  "RaderPitchKnob_40.png", "RaderPitchKnob_40.png"),
                new Tuple<int, bool, double, double, string, string>(15, false, -0.444d, 80d,  "RaderPitchKnob_39.png", "RaderPitchKnob_39.png"),
                new Tuple<int, bool, double, double, string, string>(16, false, -0.407d, 85d,  "RaderPitchKnob_38.png", "RaderPitchKnob_38.png"),
                new Tuple<int, bool, double, double, string, string>(17, false, -0.37d,  90d,  "RaderPitchKnob_37.png", "RaderPitchKnob_37.png"),
                new Tuple<int, bool, double, double, string, string>(18, false, -0.333d, 95d,  "RaderPitchKnob_36.png", "RaderPitchKnob_36.png"),
                new Tuple<int, bool, double, double, string, string>(19, false, -0.296d, 100d, "RaderPitchKnob_35.png", "RaderPitchKnob_35.png"),
                new Tuple<int, bool, double, double, string, string>(20, false, -0.259d, 105d, "RaderPitchKnob_34.png", "RaderPitchKnob_34.png"),
                new Tuple<int, bool, double, double, string, string>(21, false, -0.222d, 110d, "RaderPitchKnob_33.png", "RaderPitchKnob_33.png"),
                new Tuple<int, bool, double, double, string, string>(22, false, -0.185d, 115d, "RaderPitchKnob_32.png", "RaderPitchKnob_32.png"),
                new Tuple<int, bool, double, double, string, string>(23, false, -0.148d, 120d, "RaderPitchKnob_31.png", "RaderPitchKnob_31.png"),
                new Tuple<int, bool, double, double, string, string>(24, false, -0.111d, 125d, "RaderPitchKnob_30.png", "RaderPitchKnob_30.png"),
                new Tuple<int, bool, double, double, string, string>(25, false, -0.074d, 130d, "RaderPitchKnob_29.png", "RaderPitchKnob_29.png"),
                new Tuple<int, bool, double, double, string, string>(26, false, -0.037d, 135d, "RaderPitchKnob_28.png", "RaderPitchKnob_28.png"),
                new Tuple<int, bool, double, double, string, string>(27, false,  0.0d,   140d, "RaderPitchKnob_27.png", "RaderPitchKnob_27.png"),
                new Tuple<int, bool, double, double, string, string>(28, false,  0.037d, 145d, "RaderPitchKnob_26.png", "RaderPitchKnob_26.png"),
                new Tuple<int, bool, double, double, string, string>(29, false,  0.074d, 150d, "RaderPitchKnob_25.png", "RaderPitchKnob_25.png"),
                new Tuple<int, bool, double, double, string, string>(30, false,  0.111d, 155d, "RaderPitchKnob_24.png", "RaderPitchKnob_24.png"),
                new Tuple<int, bool, double, double, string, string>(31, false,  0.148d, 160d, "RaderPitchKnob_23.png", "RaderPitchKnob_23.png"),
                new Tuple<int, bool, double, double, string, string>(32, false,  0.185d, 165d, "RaderPitchKnob_22.png", "RaderPitchKnob_22.png"),
                new Tuple<int, bool, double, double, string, string>(33, false,  0.222d, 170d, "RaderPitchKnob_21.png", "RaderPitchKnob_21.png"),
                new Tuple<int, bool, double, double, string, string>(34, false,  0.259d, 175d, "RaderPitchKnob_20.png", "RaderPitchKnob_20.png"),
                new Tuple<int, bool, double, double, string, string>(35, false,  0.296d, 180d, "RaderPitchKnob_19.png", "RaderPitchKnob_19.png"),
                new Tuple<int, bool, double, double, string, string>(36, false,  0.333d, 185d, "RaderPitchKnob_18.png", "RaderPitchKnob_18.png"),
                new Tuple<int, bool, double, double, string, string>(37, false,  0.370d, 190d, "RaderPitchKnob_17.png", "RaderPitchKnob_17.png"),
                new Tuple<int, bool, double, double, string, string>(38, false,  0.407d, 195d, "RaderPitchKnob_16.png", "RaderPitchKnob_16.png"),
                new Tuple<int, bool, double, double, string, string>(39, false,  0.444d, 200d, "RaderPitchKnob_15.png", "RaderPitchKnob_15.png"),
                new Tuple<int, bool, double, double, string, string>(40, false,  0.481d, 205d, "RaderPitchKnob_14.png", "RaderPitchKnob_14.png"),
                new Tuple<int, bool, double, double, string, string>(41, false,  0.518d, 210d, "RaderPitchKnob_13.png", "RaderPitchKnob_13.png"),
                new Tuple<int, bool, double, double, string, string>(42, false,  0.555d, 215d, "RaderPitchKnob_12.png", "RaderPitchKnob_12.png"),
                new Tuple<int, bool, double, double, string, string>(43, false,  0.592d, 220d, "RaderPitchKnob_11.png", "RaderPitchKnob_11.png"),
                new Tuple<int, bool, double, double, string, string>(44, false,  0.629d, 225d, "RaderPitchKnob_10.png", "RaderPitchKnob_10.png"),
                new Tuple<int, bool, double, double, string, string>(45, false,  0.666d, 230d, "RaderPitchKnob_09.png", "RaderPitchKnob_09.png"),
                new Tuple<int, bool, double, double, string, string>(46, false,  0.703d, 235d, "RaderPitchKnob_08.png", "RaderPitchKnob_08.png"),
                new Tuple<int, bool, double, double, string, string>(47, false,  0.740d, 240d, "RaderPitchKnob_07.png", "RaderPitchKnob_07.png"),
                new Tuple<int, bool, double, double, string, string>(48, false,  0.777d, 245d, "RaderPitchKnob_06.png", "RaderPitchKnob_06.png"),
                new Tuple<int, bool, double, double, string, string>(49, false,  0.814d, 180d, "RaderPitchKnob_05.png", "RaderPitchKnob_05.png"),
                new Tuple<int, bool, double, double, string, string>(50, false,  0.851d, 225d, "RaderPitchKnob_04.png", "RaderPitchKnob_04.png"),
                new Tuple<int, bool, double, double, string, string>(51, false,  0.888d, 230d, "RaderPitchKnob_03.png", "RaderPitchKnob_03.png"),
                new Tuple<int, bool, double, double, string, string>(52, false,  0.925d, 235d, "RaderPitchKnob_02.png", "RaderPitchKnob_02.png"),
                new Tuple<int, bool, double, double, string, string>(53, false,  1.0d,   240d, "RaderPitchKnob_01.png", "RaderPitchKnob_01.png")
            };
            AddImageFlipParts("RaderPitchControlKnob", "RaderPitchControlKnob", 
                new Point(10d, 83d), new Size(55, 60),
                "",
                "",
                "",
                "",
                $"{_imageAssetLocation}/../FrontKnobs/RaderPitchKnob/",
                knobPostions, new Point(0d, 0d), 0, 0, 1.0, false, false);

            List<Tuple<int, bool, double, double, string, string>> knobPostions2 = new List<Tuple<int, bool, double, double, string, string>>
            {
                new Tuple<int, bool, double, double, string, string>(0, false,  0.0d,    0d,  "RaderGrayKnob_01.png", "RaderGrayKnob_01.png"),
                new Tuple<int, bool, double, double, string, string>(1, false,  0.028d,  10d, "RaderGrayKnob_02.png", "RaderGrayKnob_02.png"),
                new Tuple<int, bool, double, double, string, string>(2, false,  0.057d,  20d, "RaderGrayKnob_03.png", "RaderGrayKnob_03.png"),
                new Tuple<int, bool, double, double, string, string>(3, false,  0.085d,  30d, "RaderGrayKnob_04.png", "RaderGrayKnob_04.png"),
                new Tuple<int, bool, double, double, string, string>(4, false,  0.114d,  40d, "RaderGrayKnob_05.png", "RaderGrayKnob_05.png"),
                new Tuple<int, bool, double, double, string, string>(5, false,  0.143d,  50d, "RaderGrayKnob_06.png", "RaderGrayKnob_06.png"),
                new Tuple<int, bool, double, double, string, string>(6, false,  0.171d,  60d, "RaderGrayKnob_07.png", "RaderGrayKnob_07.png"),
                new Tuple<int, bool, double, double, string, string>(7, false,  0.2d,    70d, "RaderGrayKnob_08.png", "RaderGrayKnob_08.png"),
                new Tuple<int, bool, double, double, string, string>(8, false,  0.229d,  80d, "RaderGrayKnob_09.png", "RaderGrayKnob_09.png"),
                new Tuple<int, bool, double, double, string, string>(9, false,  0.257d,   90d, "RaderGrayKnob_10.png", "RaderGrayKnob_10.png"),
                new Tuple<int, bool, double, double, string, string>(10, false, 0.285d,  100d, "RaderGrayKnob_11.png", "RaderGrayKnob_11.png"),
                new Tuple<int, bool, double, double, string, string>(11, false, 0.314d,  110d, "RaderGrayKnob_12.png", "RaderGrayKnob_12.png"),
                new Tuple<int, bool, double, double, string, string>(12, false, 0.343d,  120d, "RaderGrayKnob_13.png", "RaderGrayKnob_13.png"),
                new Tuple<int, bool, double, double, string, string>(13, false, 0.371d,  130d, "RaderGrayKnob_14.png", "RaderGrayKnob_14.png"),
                new Tuple<int, bool, double, double, string, string>(14, false, 0.4d,    140d, "RaderGrayKnob_15.png", "RaderGrayKnob_15.png"),
                new Tuple<int, bool, double, double, string, string>(15, false, 0.428d,  150d, "RaderGrayKnob_16.png", "RaderGrayKnob_16.png"),
                new Tuple<int, bool, double, double, string, string>(16, false, 0.457d,  160d, "RaderGrayKnob_17.png", "RaderGrayKnob_17.png"),
                new Tuple<int, bool, double, double, string, string>(17, false, 0.485d,  170d, "RaderGrayKnob_18.png", "RaderGrayKnob_18.png"),
                new Tuple<int, bool, double, double, string, string>(18, false, 0.514d,  180d, "RaderGrayKnob_19.png", "RaderGrayKnob_19.png"),
                new Tuple<int, bool, double, double, string, string>(19, false, 0.542d,  190d, "RaderGrayKnob_20.png", "RaderGrayKnob_20.png"),
                new Tuple<int, bool, double, double, string, string>(20, false, 0.571d,  200d, "RaderGrayKnob_21.png", "RaderGrayKnob_21.png"),
                new Tuple<int, bool, double, double, string, string>(21, false, 0.6d,    210d, "RaderGrayKnob_22.png", "RaderGrayKnob_22.png"),
                new Tuple<int, bool, double, double, string, string>(22, false, 0.628d,  220d, "RaderGrayKnob_23.png", "RaderGrayKnob_23.png"),
                new Tuple<int, bool, double, double, string, string>(23, false, 0.657d,  230d, "RaderGrayKnob_24.png", "RaderGrayKnob_24.png"),
                new Tuple<int, bool, double, double, string, string>(24, false, 0.685d,  240d, "RaderGrayKnob_25.png", "RaderGrayKnob_25.png"),
                new Tuple<int, bool, double, double, string, string>(25, false, 0.714d,  250d, "RaderGrayKnob_26.png", "RaderGrayKnob_26.png"),
                new Tuple<int, bool, double, double, string, string>(26, false, 0.742d,  260d, "RaderGrayKnob_27.png", "RaderGrayKnob_27.png"),
                new Tuple<int, bool, double, double, string, string>(27, false, 0.771d,  270d, "RaderGrayKnob_28.png", "RaderGrayKnob_28.png"),
                new Tuple<int, bool, double, double, string, string>(28, false, 0.8d,    280d, "RaderGrayKnob_29.png", "RaderGrayKnob_29.png"),
                new Tuple<int, bool, double, double, string, string>(29, false, 0.829d,  290d, "RaderGrayKnob_30.png", "RaderGrayKnob_30.png"),
                new Tuple<int, bool, double, double, string, string>(30, false, 0.857d,  300d, "RaderGrayKnob_31.png", "RaderGrayKnob_31.png"),
                new Tuple<int, bool, double, double, string, string>(31, false, 0.885d,  310d, "RaderGrayKnob_32.png", "RaderGrayKnob_32.png"),
                new Tuple<int, bool, double, double, string, string>(32, false, 0.914d,  320d, "RaderGrayKnob_33.png", "RaderGrayKnob_33.png"),
                new Tuple<int, bool, double, double, string, string>(33, false, 0.942d,  330d, "RaderGrayKnob_34.png", "RaderGrayKnob_34.png"),
                new Tuple<int, bool, double, double, string, string>(34, false, 0.971d,  340d, "RaderGrayKnob_35.png", "RaderGrayKnob_35.png"),
                new Tuple<int, bool, double, double, string, string>(35, false, 1.0d,    350d, "RaderGrayKnob_36.png", "RaderGrayKnob_36.png"),
            };

            AddImageFlipParts("RaderScaleBrightKnob", "RaderScaleBrightKnob",
                new Point(65d, 6d), new Size(60, 60),
                "",
                "",
                "",
                "",
                $"{_imageAssetLocation}/../FrontKnobs/RoundMetalGrayKnob/",
                knobPostions2, new Point(0d, 0d), 0, 0, 0.7, false, false);

            AddImageFlipParts("RaderCursorBrightKnob", "RaderCursorBrightKnob",
                new Point(13d, 303d), new Size(60, 60),
                "",
                "",
                "",
                "",
                $"{_imageAssetLocation}/../FrontKnobs/RoundMetalGrayKnob/",
                knobPostions2, new Point(0d, 0d), 0, 0, 0.7, false, false);

            AddImageFlipParts("RaderVideoIntensityKnob", "RaderVideoIntensityKnob",
                new Point(100d, 375d), new Size(60, 60),
                "",
                "",
                "",
                "",
                $"{_imageAssetLocation}/../FrontKnobs/RoundMetalGrayKnob/",
                knobPostions2, new Point(0d, 0d), 0, 0, 0.7, false, false);

            AddImageFlipParts("RaderPersistenceKnob", "RaderPersistenceKnob",
                new Point(315d, 375d), new Size(60, 60),
                "",
                "",
                "",
                "",
                $"{_imageAssetLocation}/../FrontKnobs/RoundMetalGrayKnob/",
                knobPostions2, new Point(0d, 0d), 0, 0, 0.7, false, false);

            AddImageFlipParts("RaderBrightnessKnob", "RaderBrightnessKnob",
                new Point(365d, 295d), new Size(60, 60),
                "",
                "",
                "",
                "",
                $"{_imageAssetLocation}/../FrontKnobs/RoundMetalGrayKnob/",
                knobPostions2, new Point(0d, 0d), 0, 0, 0.7, false, false);
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
