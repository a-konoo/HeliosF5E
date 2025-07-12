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

    [HeliosControl("Helios.F5E.TenOneCounter", "TenOneCounter", "F-5E Gauges", typeof(GaugeRenderer))]
    public class TenOneCounter : BaseGauge
    {
        private HeliosValue _count;
        private GaugeDrumCounter _tensDrum;
        private GaugeDrumCounter _onesDrum;

        public TenOneCounter()
            : base("TenOneCounter", new Size(150, 100))
        {
            Components.Add(new GaugeImage("{Helios}/Gauges/F-5E/TenOneCounter/ten_one_counter_faceplate.xaml", new Rect(0d, 0d, 150d, 100d)));

            _tensDrum = new GaugeDrumCounter("{Helios}/Gauges/F-5E/TenOneCounter/drum_radio_tape.xaml",
                new Point(15.5d, 11.5d), "#", new Size(10d, 15d), new Size(50d, 75d));

            _tensDrum.Clip = new RectangleGeometry(new Rect(15.5d, 11.5d, 50d, 75d));
            Components.Add(_tensDrum);

            _onesDrum = new GaugeDrumCounter("{Helios}/Gauges/F-5E/TenOneCounter/drum_radio_tape.xaml",
                new Point(82.5d, 11.5d), "#", new Size(10d, 15d), new Size(50d, 75d));
            _onesDrum.Clip = new RectangleGeometry(new Rect(79.5d, 11.5d, 50d, 75d));
            Components.Add(_onesDrum);


            _count = new HeliosValue(this, new BindingValue(String.Empty), "", "count", "TenOneCounter", "Use format '###.##' ", BindingValueUnits.Numeric);
            _count.Execute += new HeliosActionHandler(TenOneCount_Execute);
            Actions.Add(_count);
        }

        void TenOneCount_Execute(object action, HeliosActionEventArgs e)
        {
            double p = e.Value.DoubleValue;
            _tensDrum.Value = Math.Floor(p / 10d);
            p -= Math.Floor(_tensDrum.Value * 10d);
            _onesDrum.Value = p;
        }
    }
}
