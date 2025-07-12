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

namespace GadrocsWorkshop.Helios.Gauges.F5E.OilPressure
{
    using GadrocsWorkshop.Helios.ComponentModel;
    using System.Windows;

    [HeliosControl("Helios.F5E.OilPressure", "OilPressure", "F-5E Gauges", typeof(GaugeRenderer))]
    public class OilPressure : BaseGauge
    {
        private HeliosValue _leftOilPressure;
        private HeliosValue _rightOilPressure;
        private GaugeNeedle _leftNeedle;
        private GaugeNeedle _rightNeedle;
        private CalibrationPointCollectionDouble _needleCalibration;
        public OilPressure()
            : base("OilPressure", new Size(170d, 170d))
        {
            Point center = new Point(85d, 85d);

            Components.Add(new GaugeImage("{Helios}/Gauges/F-5E/OilPressure/oilpress_faceplate.xaml", new Rect(10d, 10d, 150d, 150d)));

            _leftNeedle = new GaugeNeedle("{Helios}/Gauges/F-5E/Common/needle_accl_limit.xaml", center, new Size(48d, 77d), new Point(24d, 63d), 180d);
            Components.Add(_leftNeedle);

            _rightNeedle = new GaugeNeedle("{Helios}/Gauges/F-5E/Common/needle_accr_limit.xaml", center, new Size(48d, 77d), new Point(24d, 63d), 180d);
            Components.Add(_rightNeedle);

            _needleCalibration = new CalibrationPointCollectionDouble(0d, 0d, 100d, 315d)
            {
                new CalibrationPointDouble(0d, 0d),
                new CalibrationPointDouble(40d, 120d)
            };

            Components.Add(new GaugeImage("{Helios}/Gauges/F-5E/OilPressure/OilPressureRing.png", new Rect(0d, 0d, 170d, 170d)));

            _leftOilPressure = new HeliosValue(this, new BindingValue(0d), "", "Left Oil Pressure",
                "Current Left Oil Pressure value.", "(0 - 100)", BindingValueUnits.Numeric);
            _leftOilPressure.Execute += new HeliosActionHandler(IndicatedLeftOilPress_Execute);
            Actions.Add(_leftOilPressure);

            _rightOilPressure = new HeliosValue(this, new BindingValue(0d), "", "Right Oil Pressure",
                "Current Right Oil Pressure value.", "(0 - 100)", BindingValueUnits.Numeric);
            _leftOilPressure.Execute += new HeliosActionHandler(IndicatedRightOilPress_Execute);
            Actions.Add(_rightOilPressure);
        }

        void IndicatedLeftOilPress_Execute(object action, HeliosActionEventArgs e)
        {
            _leftNeedle.Rotation = _needleCalibration.Interpolate(e.Value.DoubleValue);
        }
        void IndicatedRightOilPress_Execute(object action, HeliosActionEventArgs e)
        {
            _rightNeedle.Rotation = _needleCalibration.Interpolate(e.Value.DoubleValue);
        }
    }
}
