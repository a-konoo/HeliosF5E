//  Copyright 2014 Craig Courtney
//  Copyright 2021 Helios Contributors
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

namespace GadrocsWorkshop.Helios.Gauges.F5E.AnalogueClock
{
    using GadrocsWorkshop.Helios.ComponentModel;
    using System.Windows;

    [HeliosControl("Helios.F5E.AnalogueClock", "AnalogueClock", "F-5E Gauges", typeof(GaugeRenderer))]
    public class AnalogueClock : BaseGauge
    {
        private HeliosValue _second;
        private HeliosValue _min;
        private HeliosValue _hour;
        private GaugeNeedle _secondNeedle;
        private GaugeNeedle _minNeedle;
        private GaugeNeedle _hourNeedle;
        public AnalogueClock()
            : base("AnalogueClock", new Size(160, 160))
        {
            Point center = new Point(80d, 80d);

            Components.Add(new GaugeImage("{F-5E}/Gauges/AnalogueClock/clock_faceplate.xaml", new Rect(0, 0, 160, 160)));



            _minNeedle = new GaugeNeedle("{F-5E}/Gauges/AnalogueClock/clock_long_needle.xaml",
                center, new Size(25d, 75d), new Point(12.5d, 71d), 50d);
            Components.Add(_minNeedle);

            _hourNeedle = new GaugeNeedle("{F-5E}/Gauges/AnalogueClock/clock_short_needle.xaml",
                center, new Size(23.5d, 55d), new Point(11.75d, 50d), 135d);
            Components.Add(_hourNeedle);

            _secondNeedle = new GaugeNeedle("{F-5E}/Gauges/AnalogueClock/clock_second_needle.xaml",
                center, new Size(50d, 135d), new Point(25d, 108d), 100d);
            Components.Add(_secondNeedle);

            _second = new HeliosValue(this, new BindingValue(0d), "", "indicated second",
                "Current second value.", "(0-59)", BindingValueUnits.Numeric);
            _second.Execute += new HeliosActionHandler(IndicatedSec_Execute);
            Actions.Add(_second);

            _min = new HeliosValue(this, new BindingValue(0d), "", "indicated min",
                "Current second value.", "(0-59)", BindingValueUnits.Numeric);
            _min.Execute += new HeliosActionHandler(IndicatedMin_Execute);
            Actions.Add(_min);

            _hour = new HeliosValue(this, new BindingValue(0d), "", "indicated hour",
                "Current second value.", "(0-11)", BindingValueUnits.Numeric);
            _hour.Execute += new HeliosActionHandler(IndicatedHour_Execute);
            Actions.Add(_hour);
        }

        void IndicatedSec_Execute(object action, HeliosActionEventArgs e)
        {
            _secondNeedle.Rotation = e.Value.DoubleValue;
        }

        void IndicatedMin_Execute(object action, HeliosActionEventArgs e)
        {
            _minNeedle.Rotation = e.Value.DoubleValue;
        }
        void IndicatedHour_Execute(object action, HeliosActionEventArgs e)
        {
            _hourNeedle.Rotation = e.Value.DoubleValue;
        }
    }
}
