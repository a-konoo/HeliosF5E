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

namespace GadrocsWorkshop.Helios.Gauges.F5E.CabinPressure
{
    using GadrocsWorkshop.Helios.ComponentModel;
    using System.Windows;

    [HeliosControl("Helios.F5E.CabinPressure", "CabinPressure", "F-5E Gauges", typeof(GaugeRenderer))]
    public class CabinPressure : BaseGauge
    {
        private HeliosValue _cabinPressure;
        private GaugeNeedle _needle;
        private CalibrationPointCollectionDouble _needleCalibration;
        public CabinPressure()
            : base("CabinPressure", new Size(170d, 170d))
        {
            Point center = new Point(85d, 85d);

            Components.Add(new GaugeImage("{F-5E}/Gauges/CabinPressure/cabinpress_faceplate.xaml", new Rect(10d, 10d, 150d, 150d)));

            _needle = new GaugeNeedle("{F-5E}/Gauges/CabinPressure/needle_long.xaml", center, new Size(30d, 130d), new Point(15d, 85d), 60d);
            Components.Add(_needle);

            _needleCalibration = new CalibrationPointCollectionDouble(0d, 0d, 0d, 240d)
            {
                new CalibrationPointDouble(0d, 0d),
                new CalibrationPointDouble(10000d, 225d)
            };

            Components.Add(new GaugeImage("{F-5E}/Gauges/Common/OtherGaugeRing.png", new Rect(0d, 0d, 170d, 170d)));

            _cabinPressure = new HeliosValue(this, new BindingValue(0d), "", "Cabin Pressure",
                "Current Cabin Pressure value.", "(0 - 50000)", BindingValueUnits.Numeric);
            _cabinPressure.Execute += new HeliosActionHandler(IndicatedOilPress_Execute);
            Actions.Add(_cabinPressure);

        }

        void IndicatedOilPress_Execute(object action, HeliosActionEventArgs e)
        {
            _needle.Rotation = _needleCalibration.Interpolate(e.Value.DoubleValue);
        }

    }
}
