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


    [HeliosControl("Helios.F5E.TACANPanel", "TACANPanel", "F-5E", typeof(BackgroundImageRenderer))]
    class TACANPanel : F5EDevice
    {
        private static readonly Rect SCREEN_RECT = new Rect(0, 0, 470, 720);
        private string _interfaceDeviceName = "TacanPanel";
        private string _imageAssetLocation = "{F-5E}/Images";
        private Rect _scaledScreenRect = SCREEN_RECT;
        private static readonly NLog.Logger Logger = NLog.LogManager.GetCurrentClassLogger();


        public TACANPanel()
            : base("TACANPanel", new Size(470, 720))
        {

            Children.Add(AddImage($"/TACANPanel/F5ETACANPanel.png", new Point(0d, 0d), 470, 720));

            List<Tuple<int, bool, double, double, string, string>> radioKnobs10 = new List<Tuple<int, bool, double, double, string, string>>
            {
                new Tuple<int, bool, double, double, string, string>(0, false,  1.0,   0d,  "RadioFreqKnob_01.png", "RadioFreqKnob_01.png"),
                new Tuple<int, bool, double, double, string, string>(1, false,  0.9,  30d,  "RadioFreqKnob_02.png", "RadioFreqKnob_02.png"),
                new Tuple<int, bool, double, double, string, string>(2, false,  0.8,  60d,  "RadioFreqKnob_03.png", "RadioFreqKnob_03.png"),
                new Tuple<int, bool, double, double, string, string>(3, false,  0.7,  90d,  "RadioFreqKnob_04.png", "RadioFreqKnob_04.png"),
                new Tuple<int, bool, double, double, string, string>(4, false,  0.6,  120d, "RadioFreqKnob_05.png", "RadioFreqKnob_05.png"),
                new Tuple<int, bool, double, double, string, string>(5, false,  0.5,  150d, "RadioFreqKnob_06.png", "RadioFreqKnob_06.png"),
                new Tuple<int, bool, double, double, string, string>(6, false,  0.4,  180d, "RadioFreqKnob_07.png", "RadioFreqKnob_07.png"),
                new Tuple<int, bool, double, double, string, string>(7, false,  0.3,  210d, "RadioFreqKnob_08.png", "RadioFreqKnob_08.png"),
                new Tuple<int, bool, double, double, string, string>(8, false,  0.2,  240d, "RadioFreqKnob_09.png", "RadioFreqKnob_09.png"),
                new Tuple<int, bool, double, double, string, string>(9, false,  0.1,  270d, "RadioFreqKnob_10.png", "RadioFreqKnob_10.png"),
            };
            List<Tuple<int, bool, double, double, string, string>> radioKnobs100Mhz = new List<Tuple<int, bool, double, double, string, string>>
            {
                new Tuple<int, bool, double, double, string, string>(0, false,  0.3,  0d,   "RadioFreq100MhzKnob_04.png", "RadioFreq100MhzKnob_04.png"),
                new Tuple<int, bool, double, double, string, string>(1, false,  0.2,  60d,  "RadioFreq100MhzKnob_03.png", "RadioFreq100MhzKnob_03.png"),
                new Tuple<int, bool, double, double, string, string>(2, false,  0.1,  90d,  "RadioFreq100MhzKnob_02.png", "RadioFreq100MhzKnob_02.png"),
                new Tuple<int, bool, double, double, string, string>(3, false,  0.0,  120d, "RadioFreq100MhzKnob_01.png", "RadioFreq100MhzKnob_01.png"),
            
            };
            List<Tuple<int, bool, double, double, string, string>> radioKnobs0025Mhz = new List<Tuple<int, bool, double, double, string, string>>
            {
                new Tuple<int, bool, double, double, string, string>(0, false,  1.0,   0d,   "RadioFreq25KhzKnob_01.png", "RadioFreq25KhzKnob_01.png"),
                new Tuple<int, bool, double, double, string, string>(1, false,  0.75,  90d,  "RadioFreq25KhzKnob_02.png", "RadioFreq25KhzKnob_02.png"),
                new Tuple<int, bool, double, double, string, string>(2, false,  0.5,   180d, "RadioFreq25KhzKnob_03.png", "RadioFreq25KhzKnob_03.png"),
                new Tuple<int, bool, double, double, string, string>(3, false,  0.25,  270d, "RadioFreq25KhzKnob_04.png", "RadioFreq25KhzKnob_04.png"),
            };

            List<Tuple<int, bool, double, double, string, string>> radioVolumePosition = new List<Tuple<int, bool, double, double, string, string>>
            {
                new Tuple<int, bool, double, double, string, string>(0, false,  0.0d,    90d,  "KnobLines01.xaml", "KnobLines01.xaml"),
                new Tuple<int, bool, double, double, string, string>(1, false,  0.028d,  100d, "KnobLines02.xaml", "KnobLines02.xaml"),
                new Tuple<int, bool, double, double, string, string>(2, false,  0.057d,  110d, "KnobLines03.xaml", "KnobLines03.xaml"),
                new Tuple<int, bool, double, double, string, string>(3, false,  0.085d,  120d, "KnobLines01.xaml", "KnobLines01.xaml"),
                new Tuple<int, bool, double, double, string, string>(4, false,  0.114d,  130d, "KnobLines02.xaml", "KnobLines02.xaml"),
                new Tuple<int, bool, double, double, string, string>(5, false,  0.143d,  140d, "KnobLines03.xaml", "KnobLines03.xaml"),
                new Tuple<int, bool, double, double, string, string>(6, false,  0.171d,  150d, "KnobLines01.xaml", "KnobLines01.xaml"),
                new Tuple<int, bool, double, double, string, string>(7, false,  0.2d,    160d, "KnobLines02.xaml", "KnobLines02.xaml"),
                new Tuple<int, bool, double, double, string, string>(8, false,  0.229d,  170d, "KnobLines03.xaml", "KnobLines03.xaml"),
                new Tuple<int, bool, double, double, string, string>(9, false,  0.257d,  180d, "KnobLines01.xaml", "KnobLines01.xaml"),
                new Tuple<int, bool, double, double, string, string>(10, false, 0.285d,  190d, "KnobLines02.xaml", "KnobLines02.xaml"),
                new Tuple<int, bool, double, double, string, string>(11, false, 0.314d,  200d, "KnobLines03.xaml", "KnobLines03.xaml"),
                new Tuple<int, bool, double, double, string, string>(12, false, 0.343d,  210d, "KnobLines01.xaml", "KnobLines01.xaml"),
                new Tuple<int, bool, double, double, string, string>(13, false, 0.371d,  220d, "KnobLines02.xaml", "KnobLines02.xaml"),
                new Tuple<int, bool, double, double, string, string>(14, false, 0.4d,    230d, "KnobLines03.xaml", "KnobLines03.xaml"),
                new Tuple<int, bool, double, double, string, string>(15, false, 0.428d,  240d, "KnobLines01.xaml", "KnobLines01.xaml"),
                new Tuple<int, bool, double, double, string, string>(16, false, 0.457d,  250d, "KnobLines02.xaml", "KnobLines02.xaml"),
                new Tuple<int, bool, double, double, string, string>(17, false, 0.485d,  260d, "KnobLines03.xaml", "KnobLines03.xaml"),
                new Tuple<int, bool, double, double, string, string>(18, false, 0.514d,  270d, "KnobLines01.xaml", "KnobLines01.xaml"),
                new Tuple<int, bool, double, double, string, string>(19, false, 0.542d,  280d, "KnobLines02.xaml", "KnobLines02.xaml"),
                new Tuple<int, bool, double, double, string, string>(20, false, 0.571d,  290d, "KnobLines03.xaml", "KnobLines03.xaml"),
                new Tuple<int, bool, double, double, string, string>(21, false, 0.6d,    300d, "KnobLines01.xaml", "KnobLines01.xaml"),
                new Tuple<int, bool, double, double, string, string>(22, false, 0.628d,  310d, "KnobLines02.xaml", "KnobLines02.xaml"),
                new Tuple<int, bool, double, double, string, string>(23, false, 0.657d,  320d, "KnobLines03.xaml", "KnobLines03.xaml"),
                new Tuple<int, bool, double, double, string, string>(24, false, 0.685d,  330d, "KnobLines01.xaml", "KnobLines01.xaml"),
                new Tuple<int, bool, double, double, string, string>(25, false, 0.714d,  340d, "KnobLines02.xaml", "KnobLines02.xaml"),
                new Tuple<int, bool, double, double, string, string>(26, false, 0.742d,  350d, "KnobLines03.xaml", "KnobLines03.xaml"),
                new Tuple<int, bool, double, double, string, string>(27, false, 0.771d,  360d, "KnobLines01.xaml", "KnobLines01.xaml"),
                new Tuple<int, bool, double, double, string, string>(28, false, 0.8d,    370d, "KnobLines02.xaml", "KnobLines02.xaml"),
                new Tuple<int, bool, double, double, string, string>(29, false, 0.829d,  380d, "KnobLines03.xaml", "KnobLines03.xaml"),
                new Tuple<int, bool, double, double, string, string>(30, false, 0.857d,  390d, "KnobLines01.xaml", "KnobLines01.xaml"),
                new Tuple<int, bool, double, double, string, string>(31, false, 0.885d,  400d, "KnobLines02.xaml", "KnobLines02.xaml"),
                new Tuple<int, bool, double, double, string, string>(32, false, 0.914d,  410d, "KnobLines03.xaml", "KnobLines03.xaml"),
                new Tuple<int, bool, double, double, string, string>(33, false, 0.942d,  420d, "KnobLines01.xaml", "KnobLines01.xaml"),
                new Tuple<int, bool, double, double, string, string>(34, false, 0.971d,  430d, "KnobLines02.xaml", "KnobLines02.xaml"),
                new Tuple<int, bool, double, double, string, string>(35, false, 1.0d,    440d, "KnobLines03.xaml", "KnobLines03.xaml")
            };

            List<Tuple<int, bool, double, double, string, string>> radioKnobs20 = new List<Tuple<int, bool, double, double, string, string>>
            {
                new Tuple<int, bool, double, double, string, string>(0, false,  0,      0d,  "PresetKnob_36.png", "PresetKnob_36.png"),
                new Tuple<int, bool, double, double, string, string>(1, false,  0.05,  18d,  "PresetKnob_36.png", "PresetKnob_36.png"),
                new Tuple<int, bool, double, double, string, string>(2, false,  0.1,   36d,  "PresetKnob_34.png", "PresetKnob_34.png"),
                new Tuple<int, bool, double, double, string, string>(3, false,  0.15,  54d,  "PresetKnob_32.png", "PresetKnob_32.png"),
                new Tuple<int, bool, double, double, string, string>(4, false,  0.20,  72d, "PresetKnob_30.png", "PresetKnob_30.png"),
                new Tuple<int, bool, double, double, string, string>(5, false,  0.25,  90d, "PresetKnob_28.png", "PresetKnob_28.png"),
                new Tuple<int, bool, double, double, string, string>(6, false,  0.30, 108d, "PresetKnob_26.png", "PresetKnob_26.png"),
                new Tuple<int, bool, double, double, string, string>(7, false,  0.35, 126d, "PresetKnob_24.png", "PresetKnob_24.png"),
                new Tuple<int, bool, double, double, string, string>(8, false,  0.40, 144d, "PresetKnob_22.png", "PresetKnob_22.png"),
                new Tuple<int, bool, double, double, string, string>(9, false,  0.45, 162d, "PresetKnob_20.png", "PresetKnob_20.png"),
                new Tuple<int, bool, double, double, string, string>(10, false, 0.50, 180d, "PresetKnob_18.png", "PresetKnob_18.png"),
                new Tuple<int, bool, double, double, string, string>(11, false, 0.55, 198d, "PresetKnob_16.png", "PresetKnob_16.png"),
                new Tuple<int, bool, double, double, string, string>(12, false, 0.60, 216d, "PresetKnob_15.png", "PresetKnob_15.png"),
                new Tuple<int, bool, double, double, string, string>(13, false, 0.65, 234d, "PresetKnob_14.png", "PresetKnob_14.png"),
                new Tuple<int, bool, double, double, string, string>(14, false, 0.70, 252d, "PresetKnob_13.png", "PresetKnob_13.png"),
                new Tuple<int, bool, double, double, string, string>(15, false, 0.75, 270d, "PresetKnob_11.png", "PresetKnob_11.png"),
                new Tuple<int, bool, double, double, string, string>(16, false, 0.80, 288d, "PresetKnob_09.png", "PresetKnob_09.png"),
                new Tuple<int, bool, double, double, string, string>(17, false, 0.85, 306d, "PresetKnob_07.png", "PresetKnob_07.png"),
                new Tuple<int, bool, double, double, string, string>(18, false, 0.90, 324d, "PresetKnob_05.png", "PresetKnob_05.png"),
                new Tuple<int, bool, double, double, string, string>(19, false, 0.95, 342d, "PresetKnob_03.png", "PresetKnob_03.png"),
                new Tuple<int, bool, double, double, string, string>(20, false, 1.0,  360d, "PresetKnob_01.png", "PresetKnob_01.png")
            };
            
            AddImageFlipParts("RadioFreqKnob100Mhz", "RadioFreqKnob100Mhz",
                new Point(30d, 163d), new Size(50, 50),
                "",
                "",
                "",
                "",
                $"{_imageAssetLocation}/FrontKnobs/RadioFreqKnob/",
                radioKnobs100Mhz, new Point(0d, 0d), new Point(0d, 0d), 0, 0, 0, 0.1d, false, false);
            
            AddImageFlipParts("RadioFreqKnob10Mhz", "RadioFreqKnob10Mhz",
                new Point(118d, 163d), new Size(50, 50),
                "",
                "",
                "",
                "",
                $"{_imageAssetLocation}/FrontKnobs/RadioFreqKnob/",
                radioKnobs10, new Point(0d, 0d), new Point(0d, 0d), 0, 0, 0, 0.1d, false, false);
            
            AddImageFlipParts("RadioFreqKnob1Mhz", "RadioFreqKnob1Mhz",
                new Point(193d, 163d), new Size(50, 50),
                "",
                "",
                "",
                "",
                $"{_imageAssetLocation}/FrontKnobs/RadioFreqKnob/",
                radioKnobs10, new Point(0d, 0d), new Point(0d, 0d), 0, 0, 0, 0.1d, false, false);
            
            AddImageFlipParts("RadioFreqKnob01Mhz", "RadioFreqKnob01Mhz",
                new Point(271d, 163d), new Size(50, 50),
                "",
                "",
                "",
                "",
                $"{_imageAssetLocation}/FrontKnobs/RadioFreqKnob/",
                radioKnobs10, new Point(0d, 0d), new Point(0d, 0d), 0, 0, 0, 0.1d, false, false);
            
            AddImageFlipParts("RadioFreqKnob0025Mhz", "RadioFreqKnob0025Mhz",
                new Point(352d, 163d), new Size(50, 50),
                "",
                "",
                "",
                "",
                $"{_imageAssetLocation}/FrontKnobs/RadioFreqKnob/",
                radioKnobs0025Mhz, new Point(0d, 0d), new Point(0d, 0d),0, 0, 0, 0.1d, false, false);

            // UHF Radio Frequency Gauge
            var freqNumberPositions = new Point[] {new Point(0.5d, 12d), new Point(87d, 12d),
                    new Point(168d, 12d), new Point(242.5d, 12d), new Point(320d, 12d)};
            
            AddRadioUHFFreqGaugePanel("RadioUHFFreqGauge", _interfaceDeviceName, "RadioUHFFreqGauge",
                new Point(244, 139), freqNumberPositions, new Size(400, 50), new Size(10, 15), new Size(17, 30), false);
            
            AddImageFlipParts("RadioPresetKnob", "RadioPresetKnob",
            new Point(350d, 8d), new Size(85, 85),
            "",
            "",
            "",
            "",
            $"{_imageAssetLocation}/FrontKnobs/PresetKnob/",
            radioKnobs20, new Point(0d, 0d), new Point(0d, 0d), 0, 0, 0, 0.1d, false, false);

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
            
            AddDrumCounterToPanel("PresetDrumCounter", "PresetDrumCounter", new Point(199d, 28d),
                new Size(60d, 30d), displayPosnList.ToArray(), tapePathList.ToArray(), new Size(10d, 15d),
                new Size(20d, 30d),gaugeFunc);


            List<Tuple<int, bool, double, double, string, string>> radioFuncSelectorPos = new List<Tuple<int, bool, double, double, string, string>>
            {
                new Tuple<int, bool, double, double, string, string>(0, false, 0.0,  10d,  "RadioRoundKnob_01.png", "RadioRoundKnob_01.png"),
                new Tuple<int, bool, double, double, string, string>(1, false, 0.1,  90d,  "RadioRoundKnob_02.png", "RadioRoundKnob_02.png"),
                new Tuple<int, bool, double, double, string, string>(2, false, 0.2,  180d, "RadioRoundKnob_03.png", "RadioRoundKnob_03.png"),
                new Tuple<int, bool, double, double, string, string>(3, false, 0.3,  270d, "RadioRoundKnob_04.png", "RadioRoundKnob_04.png")
            };

            List<Tuple<int, bool, double, double, string, string>> radioFreqSelectorPos = new List<Tuple<int, bool, double, double, string, string>>
            {
                new Tuple<int, bool, double, double, string, string>(0, false, 0.0,  10d,  "RadioRoundKnob_01.png", "RadioRoundKnob_01.png"),
                new Tuple<int, bool, double, double, string, string>(1, false, 0.1,  90d,  "RadioRoundKnob_02.png", "RadioRoundKnob_02.png"),
                new Tuple<int, bool, double, double, string, string>(2, false, 0.2,  180d, "RadioRoundKnob_03.png", "RadioRoundKnob_03.png"),
            };
            
            AddImageFlipParts("UHFRadioFuncSelector", "UHFRadioFuncSelector",
                new Point(73d, 259d), new Size(70, 70),
                "",
                "",
                "",
                "",
                $"{_imageAssetLocation}/FrontKnobs/RadioRoundKnob/",
                radioFuncSelectorPos, new Point(0d, 0d), new Point(0d, 0d), 0, 0, 0, 0.1d, false, false);
            
            AddImageFlipParts("UHFRadioFreqSelector", "UHFRadioFreqSelector",
                new Point(329d, 259d), new Size(70, 70),
                "",
                "",
                "",
                "",
                $"{_imageAssetLocation}/FrontKnobs/RadioRoundKnob/",
                radioFreqSelectorPos, new Point(0d, 0d), new Point(0d, 0d), 0, 0, 0, 0.1d, false, false);



            List<Tuple<int, bool, double, double, string, string>> volumeKnob = new List<Tuple<int, bool, double, double, string, string>>
            {
                new Tuple<int, bool, double, double, string, string>(0, false,  0.0d,    0d,  "KnobLines01.xaml", "KnobLines01.xaml"),
                new Tuple<int, bool, double, double, string, string>(1, false,  0.028d,  10d, "KnobLines02.xaml", "KnobLines02.xaml"),
                new Tuple<int, bool, double, double, string, string>(2, false,  0.057d,  20d, "KnobLines03.xaml", "KnobLines03.xaml"),
                new Tuple<int, bool, double, double, string, string>(3, false,  0.085d,  30d, "KnobLines01.xaml", "KnobLines01.xaml"),
                new Tuple<int, bool, double, double, string, string>(4, false,  0.114d,  40d, "KnobLines02.xaml", "KnobLines02.xaml"),
                new Tuple<int, bool, double, double, string, string>(5, false,  0.143d,  50d, "KnobLines03.xaml", "KnobLines03.xaml"),
                new Tuple<int, bool, double, double, string, string>(6, false,  0.171d,  60d, "KnobLines01.xaml", "KnobLines01.xaml"),
                new Tuple<int, bool, double, double, string, string>(7, false,  0.2d,    70d, "KnobLines02.xaml", "KnobLines02.xaml"),
                new Tuple<int, bool, double, double, string, string>(8, false,  0.229d,  80d, "KnobLines03.xaml", "KnobLines03.xaml"),
                new Tuple<int, bool, double, double, string, string>(9, false,  0.257d,   90d, "KnobLines01.xaml", "KnobLines01.xaml"),
                new Tuple<int, bool, double, double, string, string>(10, false, 0.285d,  100d, "KnobLines02.xaml", "KnobLines02.xaml"),
                new Tuple<int, bool, double, double, string, string>(11, false, 0.314d,  110d, "KnobLines03.xaml", "KnobLines03.xaml"),
                new Tuple<int, bool, double, double, string, string>(12, false, 0.343d,  120d, "KnobLines01.xaml", "KnobLines01.xaml"),
                new Tuple<int, bool, double, double, string, string>(13, false, 0.371d,  130d, "KnobLines02.xaml", "KnobLines02.xaml"),
                new Tuple<int, bool, double, double, string, string>(14, false, 0.4d,    140d, "KnobLines03.xaml", "KnobLines03.xaml"),
                new Tuple<int, bool, double, double, string, string>(15, false, 0.428d,  150d, "KnobLines01.xaml", "KnobLines01.xaml"),
                new Tuple<int, bool, double, double, string, string>(16, false, 0.457d,  160d, "KnobLines02.xaml", "KnobLines02.xaml"),
                new Tuple<int, bool, double, double, string, string>(17, false, 0.485d,  170d, "KnobLines03.xaml", "KnobLines03.xaml"),
                new Tuple<int, bool, double, double, string, string>(18, false, 0.514d,  180d, "KnobLines01.xaml", "KnobLines01.xaml"),
                new Tuple<int, bool, double, double, string, string>(19, false, 0.542d,  190d, "KnobLines02.xaml", "KnobLines02.xaml"),
                new Tuple<int, bool, double, double, string, string>(20, false, 0.571d,  200d, "KnobLines03.xaml", "KnobLines03.xaml"),
                new Tuple<int, bool, double, double, string, string>(21, false, 0.6d,    210d, "KnobLines01.xaml", "KnobLines01.xaml"),
                new Tuple<int, bool, double, double, string, string>(22, false, 0.628d,  220d, "KnobLines02.xaml", "KnobLines02.xaml"),
                new Tuple<int, bool, double, double, string, string>(23, false, 0.657d,  230d, "KnobLines03.xaml", "KnobLines03.xaml"),
                new Tuple<int, bool, double, double, string, string>(24, false, 0.685d,  240d, "KnobLines01.xaml", "KnobLines01.xaml"),
                new Tuple<int, bool, double, double, string, string>(25, false, 0.714d,  250d, "KnobLines02.xaml", "KnobLines02.xaml"),
                new Tuple<int, bool, double, double, string, string>(26, false, 0.742d,  260d, "KnobLines03.xaml", "KnobLines03.xaml"),
                new Tuple<int, bool, double, double, string, string>(27, false, 0.771d,  270d, "KnobLines01.xaml", "KnobLines01.xaml"),
                new Tuple<int, bool, double, double, string, string>(28, false, 0.8d,    280d, "KnobLines02.xaml", "KnobLines02.xaml"),
                new Tuple<int, bool, double, double, string, string>(29, false, 0.829d,  290d, "KnobLines03.xaml", "KnobLines03.xaml"),
                new Tuple<int, bool, double, double, string, string>(30, false, 0.857d,  300d, "KnobLines01.xaml", "KnobLines01.xaml"),
                new Tuple<int, bool, double, double, string, string>(31, false, 0.885d,  310d, "KnobLines02.xaml", "KnobLines02.xaml"),
                new Tuple<int, bool, double, double, string, string>(32, false, 0.914d,  320d, "KnobLines03.xaml", "KnobLines03.xaml"),
                new Tuple<int, bool, double, double, string, string>(33, false, 0.942d,  330d, "KnobLines01.xaml", "KnobLines01.xaml"),
                new Tuple<int, bool, double, double, string, string>(34, false, 0.971d,  340d, "KnobLines02.xaml", "KnobLines02.xaml"),
                new Tuple<int, bool, double, double, string, string>(35, false, 1.0d,    350d, "KnobLines03.xaml", "KnobLines03.xaml"),
            };
            
            AddImageFlipParts("RadioVolumeKnob", "RadioVolumeKnob",
                new Point(200d, 276d), new Size(40, 40),
                $"{_imageAssetLocation}/FrontKnobs/BlackMetalKnob/BlackMetalKnobImage.xaml",
                "",
                $"{_imageAssetLocation}/FrontKnobs/BlackMetalKnob/RadioVolumeHead.png",
                "",
                $"{_imageAssetLocation}/FrontKnobs/BlackMetalKnob/",
                volumeKnob, new Point(-19.2d, -18.8d), new Point(-2d, 0d),0.25, 0, 0, 0.1d, false, false);
            // new Point(-19.2d, -18.8d), new Point(-2d, 0d)


            List<Tuple<int, bool, double, double, string, string>> radioSqrchPosition = new List<Tuple<int, bool, double, double, string, string>>
            {
                new Tuple<int, bool, double, double, string, string>(0, false,  1, 30d, "SqlchToggle_01.png", "SqlchToggle_01.png"),
                new Tuple<int, bool, double, double, string, string>(1, false,  2,  20d, "SqlchToggle_03.png", "SqlchToggle_03.png"),
            };
            
            AddImageFlipParts("RadioSquelchToggle", "RadioSquelchToggle",
                new Point(251d, 288d), new Size(50, 20),
                "",
                "",
                "",
                "",
                $"{_imageAssetLocation}/MetalLevers/RadioSquelchToggle/",
                radioSqrchPosition, new Point(0d, 0d), new Point(0d, 0d), 1, 0, 0, 0.1d, true, false);

            // OperateButton
            AddPushButton("SquelchL", $"{_imageAssetLocation}/ClearButton/",
                "ClearButton", new Point(245d, 288d), new Size(20, 20));
            AddPushButton("SquelchR", $"{_imageAssetLocation}/ClearButton/",
                "ClearButton", new Point(282d, 288d), new Size(20, 20));

            AddPushButton(
                "RadioToneButton",
                $"{_imageAssetLocation}/FrontButtons/GrayButton/",
                "GrayButton",
                new Point(177d, 298d), new Size(19, 31));
            
            AddPushButton(
                "PresetRedButton",
                $"{_imageAssetLocation}/FrontButtons/RedButton/",
                "RedButton",
                new Point(77d, 50d), new Size(15, 15));

            AddGuardCloseOnToggleState(
                name: "HingedAccessDoor",
                posn: new Point(30d, 6d),
                size: new Size(155, 110),
                imagePath: $"{_imageAssetLocation}/FrontCover/HingedAccessDoor/",
                imageBaseName: "HingedAccessDoor",
                direction: 0,
                guardOpenRegion: new Rect(0, 0, 155, 30),
                switchRegion: new Rect(10, 10, 145, 80),
                guardCloseRegion: new Rect(0, 0, 155, 110),
                noUseClosable: false);

            // TACAN Panel
            var tacanDispPosnList = new List<Point>();
            var tacanTapePathList = new List<String>();

            tacanDispPosnList.Add(new Point(15.5d, 11.5d));
            tacanTapePathList.Add("{F-5E}/Gauges/TenOneCounter/drum_nozero_tape.xaml");
            tacanDispPosnList.Add(new Point(39.5d, 11.5d));
            tacanTapePathList.Add("{F-5E}/Gauges/TenOneCounter/drum_zero_tape.xaml");
            tacanDispPosnList.Add(new Point(63.5d, 11.5d));
            tacanTapePathList.Add("{F-5E}/Gauges/TenOneCounter/drum_zero_tape.xaml");
            tacanDispPosnList.Add(new Point(85.5d, 11.5d));
            tacanTapePathList.Add("{F-5E}/Gauges/TenOneCounter/drum_tacan_xy.xaml");

            var tacanGaugeFunc = new Action<double, GaugeDrumCounter[]>((p, d) =>
            {
                d[0].Value = Math.Floor(p / 1000d);
                p -= Math.Floor(d[0].Value * 1000d);
                d[1].Value = Math.Floor(p / 100d);
                p -= Math.Floor(d[1].Value * 100d);
                d[2].Value = Math.Floor(p / 10d);
                p -= Math.Floor(d[2].Value * 10d);
                d[3].Value = p;
            });

            AddDrumCounterToPanel("TacanDrumCounter", "TacanDrumCounter", new Point(99d, 385d),
                new Size(100d, 20d), tacanDispPosnList.ToArray(), tacanTapePathList.ToArray(), new Size(10d, 15d),
                new Size(17d, 26d), tacanGaugeFunc);

            AddIndicator("TacanTestLight",
                 $"{_imageAssetLocation}/TACANPanel/TacanTestLight/",
                "TacanTestLight",
                new Point(150d, 455d),
                new Size(20d, 30d));

            AddPushButton(
                "TacanTestButton",
                $"{_imageAssetLocation}/FrontButtons/TacanTestButton/",
                "TacanTestButton",
                new Point(140d, 494d), new Size(19, 31));

            List<Tuple<int, bool, double, double, string, string>> TacanLeftKnobPos = new List<Tuple<int, bool, double, double, string, string>>
            {
                new Tuple<int, bool, double, double, string, string>(0, false, -1,   10d, "TacanLeftKnob_12.png", "TacanLeftKnob_12.png"),
                new Tuple<int, bool, double, double, string, string>(1, false, -1,   20d, "TacanLeftKnob_11.png", "TacanLeftKnob_11.png"),
                new Tuple<int, bool, double, double, string, string>(2, false, -1,   30d, "TacanLeftKnob_10.png", "TacanLeftKnob_10.png"),
                new Tuple<int, bool, double, double, string, string>(3, false, -1,   40d, "TacanLeftKnob_09.png", "TacanLeftKnob_09.png"),
                new Tuple<int, bool, double, double, string, string>(4, false, -1,   50d, "TacanLeftKnob_08.png", "TacanLeftKnob_08.png"),
                new Tuple<int, bool, double, double, string, string>(5, false, -1,   60d, "TacanLeftKnob_07.png", "TacanLeftKnob_07.png"),
                new Tuple<int, bool, double, double, string, string>(6, false, -1,   70d, "TacanLeftKnob_06.png", "TacanLeftKnob_06.png"),
                new Tuple<int, bool, double, double, string, string>(7, false, -1,   80d, "TacanLeftKnob_05.png", "TacanLeftKnob_05.png"),
                new Tuple<int, bool, double, double, string, string>(8, false, -1,   80d, "TacanLeftKnob_04.png", "TacanLeftKnob_04.png"),
                new Tuple<int, bool, double, double, string, string>(9, false, -1,   100d, "TacanLeftKnob_03.png", "TacanLeftKnob_03.png"),
                new Tuple<int, bool, double, double, string, string>(10, false, -1,  110d, "TacanLeftKnob_02.png", "TacanLeftKnob_02.png"),
                new Tuple<int, bool, double, double, string, string>(11, false, -1,  120d, "TacanLeftKnob_01.png", "TacanLeftKnob_01.png"),
            };

            List<Tuple<int, bool, double, double, string, string>> TacanRightKnobPos = new List<Tuple<int, bool, double, double, string, string>>
            {
                new Tuple<int, bool, double, double, string, string>(0, false, -1,  10d,  "TacanRightKnob_10.png", "TacanRightKnob_10.png"),
                new Tuple<int, bool, double, double, string, string>(1, false, -1,  20d,  "TacanRightKnob_09.png", "TacanRightKnob_09.png"),
                new Tuple<int, bool, double, double, string, string>(2, false, -1,  30d,  "TacanRightKnob_08.png", "TacanRightKnob_08.png"),
                new Tuple<int, bool, double, double, string, string>(3, false, -1,  40d,  "TacanRightKnob_07.png", "TacanRightKnob_07.png"),
                new Tuple<int, bool, double, double, string, string>(4, false, -1,  50d,  "TacanRightKnob_06.png", "TacanRightKnob_06.png"),
                new Tuple<int, bool, double, double, string, string>(5, false, -1,  60d,  "TacanRightKnob_05.png", "TacanRightKnob_05.png"),
                new Tuple<int, bool, double, double, string, string>(6, false, -1,  70d,  "TacanRightKnob_04.png", "TacanRightKnob_04.png"),
                new Tuple<int, bool, double, double, string, string>(7, false, -1,  80d,  "TacanRightKnob_03.png", "TacanRightKnob_03.png"),
                new Tuple<int, bool, double, double, string, string>(8, false, -1,  90d,  "TacanRightKnob_02.png", "TacanRightKnob_02.png"),
                new Tuple<int, bool, double, double, string, string>(9, false, -1,  100d, "TacanRightKnob_01.png", "TacanRightKnob_01.png")
            };

            List<Tuple<int, bool, double, double, string, string>> TacanXYRing = new List<Tuple<int, bool, double, double, string, string>>
            {
                new Tuple<int, bool, double, double, string, string>(0, false, 1,  10d,  "TacanRing_01.png", "TacanRing_01.png"),
                new Tuple<int, bool, double, double, string, string>(1, false, 1,  20d,  "TacanRing_01.png", "TacanRing_01.png"),
                new Tuple<int, bool, double, double, string, string>(2, false, 2,  30d,  "TacanRing_02.png", "TacanRing_02.png")
            };
            AddImageFlipParts("TacanXYRing", "TacanXYRing",
                new Point(202d, 432d), new Size(62, 62),
                "",
                "",
                "",
                "",
                $"{_imageAssetLocation}/FrontKnobs/TacanXYRing/",
                TacanXYRing, new Point(0d, 0d), new Point(0d, 0d), 0, 0, 0, 0.1, true, false);

            AddPushButton("TacanX", $"{_imageAssetLocation}/ClearButton/",
                "ClearButton", new Point(262d, 435d), new Size(20, 20));
            AddPushButton("TacanY", $"{_imageAssetLocation}/ClearButton/",
                "ClearButton", new Point(262d, 472d), new Size(20, 20));

            AddFlipRotaryEncoder("TacanLeftKnob", "TacanLeftKnob",
                new Point(50d, 440d), new Size(50, 50),
                "",
                "",
                "",
                "",
                $"{_imageAssetLocation}/FrontKnobs/TacanLeftKnob/",
                TacanLeftKnobPos, 0, 0, 40d, 45d, false);

            AddFlipRotaryEncoder("TacanRightKnob", "TacanRightKnob",
                new Point(208d, 440d), new Size(50, 50),
                "",
                "",
                "",
                "",
                $"{_imageAssetLocation}/FrontKnobs/TacanRightKnob/",
                TacanRightKnobPos, 0, 0, 40d, 45d, false);

            List<Tuple<int, bool, double, double, string, string>> tacanVolumeKnobPos = new List<Tuple<int, bool, double, double, string, string>>
            {
                new Tuple<int, bool, double, double, string, string>(0, false,  0.0d,    0d,   "TacanVolume_01.png", "TacanVolume_01.png"),
                new Tuple<int, bool, double, double, string, string>(1, false,  0.028d,  10d,  "TacanVolume_02.png", "TacanVolume_02.png"),
                new Tuple<int, bool, double, double, string, string>(2, false,  0.057d,  20d,  "TacanVolume_03.png", "TacanVolume_03.png"),
                new Tuple<int, bool, double, double, string, string>(3, false,  0.085d,  30d,  "TacanVolume_04.png", "TacanVolume_04.png"),
                new Tuple<int, bool, double, double, string, string>(4, false,  0.114d,  40d,  "TacanVolume_01.png", "TacanVolume_01.png"),
                new Tuple<int, bool, double, double, string, string>(5, false,  0.143d,  50d,  "TacanVolume_02.png", "TacanVolume_02.png"),
                new Tuple<int, bool, double, double, string, string>(6, false,  0.171d,  60d,  "TacanVolume_03.png", "TacanVolume_03.png"),
                new Tuple<int, bool, double, double, string, string>(7, false,  0.2d,    70d,  "TacanVolume_04.png", "TacanVolume_04.png"),
                new Tuple<int, bool, double, double, string, string>(8, false,  0.229d,  80d,  "TacanVolume_01.png", "TacanVolume_01.png"),
                new Tuple<int, bool, double, double, string, string>(9, false,  0.257d,   90d, "TacanVolume_02.png", "TacanVolume_02.png"),
                new Tuple<int, bool, double, double, string, string>(10, false, 0.285d,  100d, "TacanVolume_03.png", "TacanVolume_03.png"),
                new Tuple<int, bool, double, double, string, string>(11, false, 0.314d,  110d, "TacanVolume_04.png", "TacanVolume_04.png"),
                new Tuple<int, bool, double, double, string, string>(12, false, 0.343d,  120d, "TacanVolume_01.png", "TacanVolume_01.png"),
                new Tuple<int, bool, double, double, string, string>(13, false, 0.371d,  130d, "TacanVolume_02.png", "TacanVolume_02.png"),
                new Tuple<int, bool, double, double, string, string>(14, false, 0.4d,    140d, "TacanVolume_03.png", "TacanVolume_03.png"),
                new Tuple<int, bool, double, double, string, string>(15, false, 0.428d,  150d, "TacanVolume_04.png", "TacanVolume_04.png"),
                new Tuple<int, bool, double, double, string, string>(16, false, 0.457d,  160d, "TacanVolume_01.png", "TacanVolume_01.png"),
                new Tuple<int, bool, double, double, string, string>(17, false, 0.485d,  170d, "TacanVolume_02.png", "TacanVolume_02.png"),
                new Tuple<int, bool, double, double, string, string>(18, false, 0.514d,  180d, "TacanVolume_03.png", "TacanVolume_03.png"),
                new Tuple<int, bool, double, double, string, string>(19, false, 0.542d,  190d, "TacanVolume_04.png", "TacanVolume_04.png"),
                new Tuple<int, bool, double, double, string, string>(20, false, 0.571d,  200d, "TacanVolume_01.png", "TacanVolume_01.png"),
                new Tuple<int, bool, double, double, string, string>(21, false, 0.6d,    210d, "TacanVolume_02.png", "TacanVolume_02.png"),
                new Tuple<int, bool, double, double, string, string>(22, false, 0.628d,  220d, "TacanVolume_03.png", "TacanVolume_03.png"),
                new Tuple<int, bool, double, double, string, string>(23, false, 0.657d,  230d, "TacanVolume_04.png", "TacanVolume_04.png"),
                new Tuple<int, bool, double, double, string, string>(24, false, 0.685d,  240d, "TacanVolume_01.png", "TacanVolume_01.png"),
                new Tuple<int, bool, double, double, string, string>(25, false, 0.714d,  250d, "TacanVolume_02.png", "TacanVolume_02.png"),
                new Tuple<int, bool, double, double, string, string>(26, false, 0.742d,  260d, "TacanVolume_03.png", "TacanVolume_03.png"),
                new Tuple<int, bool, double, double, string, string>(27, false, 0.771d,  270d, "TacanVolume_04.png", "TacanVolume_04.png"),
                new Tuple<int, bool, double, double, string, string>(28, false, 0.8d,    280d, "TacanVolume_01.png", "TacanVolume_01.png"),
                new Tuple<int, bool, double, double, string, string>(29, false, 0.829d,  290d, "TacanVolume_02.png", "TacanVolume_02.png"),
                new Tuple<int, bool, double, double, string, string>(30, false, 0.857d,  300d, "TacanVolume_03.png", "TacanVolume_03.png"),
                new Tuple<int, bool, double, double, string, string>(31, false, 0.885d,  310d, "TacanVolume_04.png", "TacanVolume_04.png"),
                new Tuple<int, bool, double, double, string, string>(32, false, 0.914d,  320d, "TacanVolume_01.png", "TacanVolume_01.png"),
                new Tuple<int, bool, double, double, string, string>(33, false, 0.942d,  330d, "TacanVolume_02.png", "TacanVolume_02.png"),
                new Tuple<int, bool, double, double, string, string>(34, false, 0.971d,  340d, "TacanVolume_03.png", "TacanVolume_03.png"),
                new Tuple<int, bool, double, double, string, string>(35, false, 1.0d,    350d, "TacanVolume_04.png", "TacanVolume_04.png"),
            };

            AddImageFlipParts("TacanVolumeKnob", "TacanVolumeKnob",
                new Point(236d, 380d), new Size(50, 50),
                "",
                "",
                "",
                "",
                $"{_imageAssetLocation}/FrontKnobs/TacanVolume/",
                tacanVolumeKnobPos, new Point(0d, 0d), new Point(0d, 0d), 0, 0, 0, 0.1d, false, false);

            List<Tuple<int, bool, double, double, string, string>> TacanModeKnobPos = new List<Tuple<int, bool, double, double, string, string>>
            {
                new Tuple<int, bool, double, double, string, string>(0, false, 0.0,  0d,   "TacanRadioMode_01.png", "TacanRadioMode_01.png"),
                new Tuple<int, bool, double, double, string, string>(1, false, 0.1,  30d,  "TacanRadioMode_02.png", "TacanRadioMode_02.png"),
                new Tuple<int, bool, double, double, string, string>(2, false, 0.2,  60d,  "TacanRadioMode_03.png", "TacanRadioMode_03.png"),
                new Tuple<int, bool, double, double, string, string>(3, false, 0.3,  90d,  "TacanRadioMode_04.png", "TacanRadioMode_04.png"),
                new Tuple<int, bool, double, double, string, string>(4, false, 0.4,  120d, "TacanRadioMode_05.png", "TacanRadioMode_05.png"),
            };

            AddImageFlipParts("TacanRadioModeKnob", "TacanRadioModeKnob",
                new Point(312d, 423d), new Size(80, 80),
                "",
                "",
                "",
                "",
                $"{_imageAssetLocation}/FrontKnobs/TacanMode/",
                TacanModeKnobPos, new Point(0d, 0d), new Point(0d, 0d), 0, 0, 0, 0.1d, false, false);

            List<Tuple<int, bool, double, double, string, string>> DFTacanKnobPos = new List<Tuple<int, bool, double, double, string, string>>
            {
                new Tuple<int, bool, double, double, string, string>(0, false, 0.0,  0d,   "DFTacanKnob_01.png", "DFTacanKnob_01.png"),
                new Tuple<int, bool, double, double, string, string>(1, false, 0.1,  30d,  "DFTacanKnob_02.png", "DFTacanKnob_02.png")
            };
            /*
            AddImageFlipParts("DFTacanKnob", "DFTacanKnob",
                new Point(112d, 600d), new Size(70, 70),
                "",
                "",
                "",
                "",
                $"{_imageAssetLocation}/FrontKnobs/DFTacanKnob/",
                DFTacanKnobPos, new Point(0d, 0d), new Point(0d, 0d), 0, 0, 0,0.1d, false, false);
            */
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
            Point frontPos,
            double frontRatio,
            double pullJudgeAngle,
            double pullJudgeDistance,
            double stepValue,
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
                frontRatio: frontRatio,
                pullJudgeAngle: pullJudgeAngle,
                pullJudgeDistance: pullJudgeDistance,
                stepValue: stepValue,
                prohibitOperate: prohibitOperate,
                pullable: pullable);

            return part;
        }

        private FlipRotaryEncoder AddFlipRotaryEncoder (string name, string actionIdentifier,
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
                frontAdjust: new Point(0,0),
                frontPos: new Point(0,0),
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


        private void AddRadioUHFFreqGaugePanel(string name, string interfaceDevice, string interfaceElement,
            Point posn,Point[] displayPosn, Size size, Size digitSize, Size displaySize, 
            bool fromCenter)
        {
            AddRadioUHFFreqGauge(
                name: name,
                posn: posn,
                displayPosn:displayPosn,
                size: size,
                digitSize: digitSize,
                actionIdentifier: name,
                displaySize: displaySize,
                interfaceDeviceName: interfaceDevice,
                interfaceElementName: interfaceElement,
                fromCenter: true
                );
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


        private void AddIndicator(string name, string imageBasePath, string imageFileName, Point pos, Size size)
        {
            AddIndicator(name: name,
                posn: pos,
                size: size,
                offImage: imageBasePath + imageFileName + "_OFF.png",
                onImage: imageBasePath + imageFileName + "_ON.png",
                offTextColor: Color.FromArgb(0xff, 0x7e, 0xde, 0x72),
                onTextColor: Color.FromArgb(0xff, 0x7e, 0xde, 0x72),
                font: "",
                interfaceDeviceName: _interfaceDeviceName,
                interfaceElementName: name,
                fromCenter: true,
                withText: false,
                vertical: false);
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
