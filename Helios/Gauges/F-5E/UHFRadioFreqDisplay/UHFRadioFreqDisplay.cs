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

namespace GadrocsWorkshop.Helios.Gauges.F5E.UHFRadioFreqDisplay
{
    using GadrocsWorkshop.Helios.ComponentModel;
    using System;
    using System.Windows;
    using System.Windows.Media;

    [HeliosControl("Helios.F5E.UHFRadioFreqDisplay", "UHFRadioFreqDisplay", "F-5E Gauges", typeof(GaugeRenderer))]
    public class UHFRadioFreqDisplay : BaseGauge
    {
        private HeliosValue _frequency;
        private GaugeDrumCounter _hundredsDrum;
        private GaugeDrumCounter _tensDrum;
        private GaugeDrumCounter _onesDrum;
        private GaugeDrumCounter _zeroOneDrum;
        private GaugeDrumCounter _zeroQrtDrum;

        public UHFRadioFreqDisplay()
            : base("UHFRadioFreqDisplay", new Size(375, 100))
        {
            Components.Add(new GaugeImage("{Helios}/Gauges/F-5E/UHFRadioFreqDisplay/uhf_radio_channel_faceplate.xaml", new Rect(0d, 0d, 375d, 100d)));

            _hundredsDrum = new GaugeDrumCounter("{Helios}/Gauges/F-5E/UHFRadioFreqDisplay/drum_radio_tape_a23t.xaml",
                new Point(15.5d, 11.5d), "#", new Size(10d, 15d), new Size(50d, 75d));

            _hundredsDrum.Clip = new RectangleGeometry(new Rect(15.5d, 11.5d, 50d, 75d));
            Components.Add(_hundredsDrum);

            _tensDrum = new GaugeDrumCounter("{Helios}/Gauges/F-5E/UHFRadioFreqDisplay/drum_radio_tape.xaml",
                new Point(82.5d, 11.5d), "#", new Size(10d, 15d), new Size(50d, 75d));
            _tensDrum.Clip = new RectangleGeometry(new Rect(79.5d, 11.5d, 50d, 75d));
            Components.Add(_tensDrum);

            _onesDrum = new GaugeDrumCounter("{Helios}/Gauges/F-5E/UHFRadioFreqDisplay/drum_radio_tape.xaml",
                new Point(148.5d, 11.5d), "#", new Size(10d, 15d), new Size(50d, 75d));
            _onesDrum.Clip = new RectangleGeometry(new Rect(145.5d, 11.5d, 50d, 75d));
            Components.Add(_onesDrum);

            _zeroOneDrum = new GaugeDrumCounter("{Helios}/Gauges/F-5E/UHFRadioFreqDisplay/drum_radio_tape.xaml",
                new Point(235.5d, 11.5d), "#", new Size(10d, 15d), new Size(50d, 75d));
            _zeroOneDrum.Clip = new RectangleGeometry(new Rect(237.5d, 11.5d, 50d, 75d));
            Components.Add(_zeroOneDrum);

            _zeroQrtDrum = new GaugeDrumCounter("{Helios}/Gauges/F-5E/UHFRadioFreqDisplay/drum_radio_tape_oohz.xaml",
                new Point(304.5d, 11.5d), "#", new Size(10d, 15d), new Size(50d, 75d));
            _zeroQrtDrum.Clip = new RectangleGeometry(new Rect(300.5d, 11.5d, 50d, 75d));
            Components.Add(_zeroQrtDrum);

            _frequency = new HeliosValue(this, new BindingValue(String.Empty), "", "frequency", "UHFRadioFreqDisplay", "format '###.##' ", BindingValueUnits.Numeric);
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
