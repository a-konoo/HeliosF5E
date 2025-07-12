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

    [HeliosControl("Helios.F5E.RWRPanel", "RWRPanel", "F-5E", typeof(BackgroundImageRenderer))]
    class RWRPanel : F5EDevice
    {
        private static readonly Rect SCREEN_RECT = new Rect(0, 0, 600, 250);
        private string _interfaceDeviceName = "RaderPanel";
        private string _imageAssetLocation = "{F-5E}/Images/RWRPanel";
        private Rect _scaledScreenRect = SCREEN_RECT;


        public RWRPanel()
            : base("RWRPanel", new Size(600, 250))
        {

            Children.Add(AddImage($"/RWRPanel.png", new Point(0d, 0d), 600, 250));

            List<Tuple<int, string>> pwrList = new List<Tuple<int, string>>();
            pwrList.Add(new Tuple<int, string>(0, "0"));
            pwrList.Add(new Tuple<int, string>(1, "1"));
            pwrList.Add(new Tuple<int, string>(2, "2"));
            AddStatePushPanelToRWR("PWRPanel", "PWRPanel", new Point(387d, 146d), new Size(82, 82), "PWRPanel",
                "PWR_01", pwrList);

            AddStatePushPanelToRWR("ALTPanel", "ALTPanel", new Point(387d, 46d), new Size(82, 82), "ALTPanel",
                "ALT_01", pwrList);

            AddStatePushPanelToRWR("ACTPWRPanel", "ACTPWRPanel", new Point(305d, 146d), new Size(82, 82), "ACTPWRPanel",
                "ACTPWR_01", pwrList);

            AddStatePushPanelToRWR("LAUNCHPanel", "LAUNCHPanel", new Point(305d, 46d), new Size(82, 82), "LAUNCHPanel",
                "LAUNCH_01", pwrList);

            AddStatePushPanelToRWR("UNKNOWNPanel", "UNKNOWNPanel", new Point(223d, 146d), new Size(82, 82), "UNKNOWNPanel",
                "UNKNOWN_01", pwrList);

            AddStatePushPanelToRWR("HANDOFFPanel", "HANDOFFPanel", new Point(223d, 46d), new Size(82, 82), "HANDOFFPanel",
                "HANDOFF_01", pwrList);

            AddStatePushPanelToRWR("SYSTESTPanel", "SYSTESTPanel", new Point(141d, 146d), new Size(82, 82), "SYSTESTPanel",
                "SYSTEST_01", pwrList);
            AddStatePushPanelToRWR("SEARCHPanel", "SEARCHPanel", new Point(141d, 46d), new Size(82, 82), "SEARCHPanel",
                "SEARCH_01", pwrList);

            AddStatePushPanelToRWR("TGTSEPPanel", "TGTSEPPanel", new Point(59d, 146d), new Size(82, 82), "TGTSEPPanel",
                "TGTSEP_01", pwrList);

            AddStatePushPanelToRWR("MODEPanel", "MODEPanel", new Point(59d, 46d), new Size(82, 82), "MODEPanel",
                "MODE_01", pwrList);

            List<Tuple<int, bool, double, double, string, string>> knobPostions = new List<Tuple<int, bool, double, double, string, string>>
            {
     new Tuple<int, bool, double, double, string, string>(0, false,  0.0d,    0d,  "RaderBlackKnob_01.png", "RaderBlackKnob_01.png"),
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

            AddImageFlipParts("RWRAudioKnob", "RWRAudioKnob",
                new Point(485d, 160d), new Size(70, 70),
                "",
                "",
                "",
                "",
                $"{_imageAssetLocation}/../FrontKnobs/RoundMetalBlackKnob/",
                knobPostions, new Point(2d, 3.8d), new Point(-12.4d, -12.2d), 0.28, 0, 0, false, false);

            AddImageFlipParts("RWRDimKnob", "RWRDimKnob",
                new Point(485d, 50d), new Size(70, 70),
                "",
                "",
                "",
                "",
                $"{_imageAssetLocation}/../FrontKnobs/RoundMetalBlackKnob/",
                knobPostions, new Point(2, 3.8), new Point(-12.4d, -12.4d), 0.28, 0, 0, false, false);

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
            Point frontPos,
            Point frontAdjust,
            double frontRatio,
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
                frontPos: frontPos,
                pullJudgeAngle: pullJudgeAngle,
                frontRatio: frontRatio,
                pullJudgeDistance: pullJudgeDistance,
                prohibitOperate: prohibitOperate,
                pullable: pullable);

            return part;
        }

        private StatePushPanel AddStatePushPanelToRWR(string name, string actionIdentifier,
            Point posn, Size size, string interfaceDeviceName,
            string animationImagePath, List<Tuple<int, string>> stateList)
        {
            StatePushPanel panel = AddStatePushPanel(
                name: name,
                fromCenter: false,
                posn: posn,
                size: size,
                interfaceDeviceName: interfaceDeviceName,
                interfaceElementName: actionIdentifier,
                animationImagePath: $"{ _imageAssetLocation}/{animationImagePath}",
                stateList: stateList);
            return panel;
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
