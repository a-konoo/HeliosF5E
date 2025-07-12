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
    using System;
    using System.Windows;
    using System.Windows.Media;

    [HeliosControl("Helios.F5E.UHFRadioFreqGauge", "UHFRadioFreqGauge", "F-5E Gauges", typeof(GaugeRenderer), HeliosControlFlags.NotShownInUI)]
    public class UHFRadioFreqGauge : BaseGauge
    {
        private HeliosValue _frequency;
        private GaugeDrumCounter _hundredsDrum;
        private GaugeDrumCounter _tensDrum;
        private GaugeDrumCounter _onesDrum;
        private GaugeDrumCounter _zeroOneDrum;
        private GaugeDrumCounter _zeroQrtDrum;

        public UHFRadioFreqGauge()
            : base("UHFRadioFreqGauge", new Size(400, 50))
        {
            _hundredsDrum = new GaugeDrumCounter("{F-5E}/Gauges/UHFRadioFreqGauge/drum_radio_tape_a23t.xaml",
                new Point(0.5d, 11.5d), "#", new Size(10d, 15d), new Size(22d, 35d));

            _hundredsDrum.Clip = new RectangleGeometry(new Rect(0.5d, 11.5d, 22d, 35d));
            Components.Add(_hundredsDrum);

            _tensDrum = new GaugeDrumCounter("{F-5E}/Gauges/UHFRadioFreqGauge/drum_radio_tape.xaml",
                new Point(96d, 11.5d), "#", new Size(10d, 15d), new Size(22d, 35d));
            _tensDrum.Clip = new RectangleGeometry(new Rect(96d, 11.5d, 22d, 35d));
            Components.Add(_tensDrum);

            _onesDrum = new GaugeDrumCounter("{F-5E}/Gauges/UHFRadioFreqGauge/drum_radio_tape.xaml",
                new Point(180d, 11.5d), "#", new Size(10d, 15d), new Size(22d, 35d));
            _onesDrum.Clip = new RectangleGeometry(new Rect(180d, 11.5d, 22d, 35d));
            Components.Add(_onesDrum);

            _zeroOneDrum = new GaugeDrumCounter("{F-5E}/Gauges/UHFRadioFreqGauge/drum_radio_tape.xaml",
                new Point(265.5d, 11.5d), "#", new Size(10d, 15d), new Size(22d, 35d));
            _zeroOneDrum.Clip = new RectangleGeometry(new Rect(265.5d, 11.5d, 22d, 35d));
            Components.Add(_zeroOneDrum);

            _zeroQrtDrum = new GaugeDrumCounter("{F-5E}/Gauges/UHFRadioFreqGauge/drum_radio_tape_oohz.xaml",
                new Point(349d, 11.5d), "#", new Size(10d, 15d), new Size(22d, 35d));
            _zeroQrtDrum.Clip = new RectangleGeometry(new Rect(349d, 11.5d, 22d, 35d));
            Components.Add(_zeroQrtDrum);

            _frequency = new HeliosValue(this, new BindingValue(String.Empty), "", "frequency",
                "UHFRadioFreqDisplay", "format '###.##' ", BindingValueUnits.Numeric);

            _frequency.Execute += new HeliosActionHandler(FrequencyReflection_Execute);
            Actions.Add(_frequency);
        }

        public UHFRadioFreqGauge(string name, Point posn, Size size, Point[] displayPosn, Size digitSize, Size displaySize)
            : base(name, new Size(size.Width, size.Height))
        {
            Left = posn.X;
            Top = posn.Y;
            if (displayPosn.Length != 5)
            {
                displayPosn = new Point[] {new Point(0.5d, 11.5d), new Point(96d, 11.5d), 
                    new Point(180d, 11.5d), new Point(265.5d, 11.5d),
                new Point(349d, 11.5d)};
            }

            _hundredsDrum = new GaugeDrumCounter("{F-5E}/Gauges/UHFRadioFreqGauge/drum_radio_tape_a23t.xaml",
                new Point(displayPosn[0].X, displayPosn[0].Y), "#", new Size(digitSize.Width, digitSize.Height),
                new Size(displaySize.Width, displaySize.Height));
            _hundredsDrum.Clip = new RectangleGeometry(new Rect(displayPosn[0].X, displayPosn[0].Y, displaySize.Width, displaySize.Height));
            Components.Add(_hundredsDrum);

            _tensDrum = new GaugeDrumCounter("{F-5E}/Gauges/UHFRadioFreqGauge/drum_radio_tape.xaml",
                new Point(displayPosn[1].X, displayPosn[1].Y), "#", new Size(digitSize.Width, digitSize.Height),
                new Size(displaySize.Width, displaySize.Height));
            _tensDrum.Clip = new RectangleGeometry(new Rect(displayPosn[1].X, displayPosn[1].Y, displaySize.Width, displaySize.Height));
            Components.Add(_tensDrum);

            _onesDrum = new GaugeDrumCounter("{F-5E}/Gauges/UHFRadioFreqGauge/drum_radio_tape.xaml",
                new Point(displayPosn[2].X, displayPosn[2].Y), "#", new Size(digitSize.Width, digitSize.Height),
                new Size(displaySize.Width, displaySize.Height));
            _onesDrum.Clip = new RectangleGeometry(new Rect(displayPosn[2].X, displayPosn[2].Y, displaySize.Width, displaySize.Height));
            Components.Add(_onesDrum);

            _zeroOneDrum = new GaugeDrumCounter("{F-5E}/Gauges/UHFRadioFreqGauge/drum_radio_tape.xaml",
                new Point(displayPosn[3].X, displayPosn[3].Y), "#", new Size(digitSize.Width, digitSize.Height),
                new Size(displaySize.Width, displaySize.Height));
            _zeroOneDrum.Clip = new RectangleGeometry(new Rect(displayPosn[3].X, displayPosn[3].Y, displaySize.Width, displaySize.Height));
            Components.Add(_zeroOneDrum);

            _zeroQrtDrum = new GaugeDrumCounter("{F-5E}/Gauges/UHFRadioFreqGauge/drum_radio_tape_oohz.xaml",
                new Point(displayPosn[4].X, displayPosn[4].Y), "#", new Size(digitSize.Width, digitSize.Height),
                new Size(displaySize.Width, displaySize.Height));
            _zeroQrtDrum.Clip = new RectangleGeometry(new Rect(displayPosn[4].X, displayPosn[4].Y, displaySize.Width, displaySize.Height));
            Components.Add(_zeroQrtDrum);

            _frequency = new HeliosValue(this, new BindingValue(String.Empty), "", "frequency", "UHFRadioFreqGauge", "format '###.##' ",
                BindingValueUnits.Numeric);
            _frequency.Execute += new HeliosActionHandler(FrequencyReflection_Execute);
            Actions.Add(_frequency);
        }


        void FrequencyReflection_Execute(object action, HeliosActionEventArgs e)
        {
            _hundredsDrum.Value = Math.Floor(e.Value.DoubleValue / 10000d);

            var p = e.Value.DoubleValue - _hundredsDrum.Value * 10000d;
            _tensDrum.Value = Math.Floor(p / 1000d);

            p -= _tensDrum.Value * 1000d;
            _onesDrum.Value = Math.Floor(p / 100d);

            p -= _onesDrum.Value * 100d;
            _zeroOneDrum.Value = Math.Floor(p / 10d);
            p -= _zeroOneDrum.Value * 10d;
            _zeroQrtDrum.Value = p;
        }
    }
}
