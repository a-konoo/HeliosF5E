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

namespace GadrocsWorkshop.Helios.Gauges.F5E.Accelerometer
{
    using GadrocsWorkshop.Helios.ComponentModel;
    using System.Windows;

    [HeliosControl("Helios.F5E.Accelerometer", "Accelerometer", "F-5E Gauges", typeof(GaugeRenderer))]
    public class Accelerometer : BaseGauge
    {
        private HeliosValue _currentGS;
        private HeliosValue _accelerometerMin;
        private HeliosValue _accelerometerMax;
        private GaugeNeedle _needleGS;
        private GaugeNeedle _needleAccMin;
        private GaugeNeedle _needleAccMax;
        private CalibrationPointCollectionDouble _needleCalibration;
        public Accelerometer()
            : base("Accelerometer", new Size(160, 160))
        {
            Point center = new Point(80d, 80d);

            Components.Add(new GaugeImage("{Helios}/Gauges/F-5E/Accelerometer/acc_faceplate.xaml", new Rect(16.5, 20.5, 125, 125)));

            _needleGS = new GaugeNeedle("{Helios}/Gauges/F-5E/Accelerometer/acc_needle.xaml", center, new Size(20d, 76d), new Point(10d, 65.5d), 135d);
            Components.Add(_needleGS);

            _needleAccMin = new GaugeNeedle("{Helios}/Gauges/F-5E/Accelerometer/acc_needle.xaml", center, new Size(20d, 76d), new Point(10d, 65.5d), 135d);
            Components.Add(_needleAccMin);

            _needleAccMax = new GaugeNeedle("{Helios}/Gauges/F-5E/Accelerometer/acc_needle.xaml", center, new Size(20d, 76d), new Point(10d, 65.5d), 135d);
            Components.Add(_needleAccMax);

            _needleCalibration = new CalibrationPointCollectionDouble(-5d, 0d, 10d, 210d)
            {
                new CalibrationPointDouble(-5d, 0d),
                new CalibrationPointDouble(0d, 112.5d),
                new CalibrationPointDouble(10d, 337.5d)
            };
            _currentGS = new HeliosValue(this, new BindingValue(0d), "", "indicated GS",
                "Current gs of the aircraft.", "(-5 ... +10)", BindingValueUnits.Numeric);
            _currentGS.Execute += new HeliosActionHandler(IndicatedGS_Execute);
            Actions.Add(_currentGS);

            _accelerometerMin = new HeliosValue(this, new BindingValue(0d), "", "Accelerometer Min",
                "Current accelerometer Min value.", "(-4 ... +10)", BindingValueUnits.Numeric);
            _accelerometerMin.Execute += new HeliosActionHandler(IndicatedAccMax_Execute);
            Actions.Add(_accelerometerMin);

            _accelerometerMax = new HeliosValue(this, new BindingValue(0d), "", "Accelerometer Max",
                "Current accelerometer Max value.", "(-4 ... +10)", BindingValueUnits.Numeric);
            _accelerometerMax.Execute += new HeliosActionHandler(IndicatedAccMin_Execute);
            Actions.Add(_accelerometerMax);

            Components.Add(new GaugeImage("{Helios}/Gauges/F-5E/Accelerometer/ACCLGaugeCover.png", new Rect(0d, 0d, 160d, 160d)));
        }

        void IndicatedGS_Execute(object action, HeliosActionEventArgs e)
        {
            _needleGS.Rotation = _needleCalibration.Interpolate(e.Value.DoubleValue);
        }

        void IndicatedAccMax_Execute(object action, HeliosActionEventArgs e)
        {
            _needleAccMin.Rotation = _needleCalibration.Interpolate(e.Value.DoubleValue);
        }
        void IndicatedAccMin_Execute(object action, HeliosActionEventArgs e)
        {
            _needleAccMax.Rotation = _needleCalibration.Interpolate(e.Value.DoubleValue);
        }
    }
}
