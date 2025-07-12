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

namespace GadrocsWorkshop.Helios.Gauges.F5E.EngineTemp
{
    using GadrocsWorkshop.Helios.ComponentModel;
    using System.Windows;

    [HeliosControl("Helios.F5E.EngineTemp", "Engine Temp", "F-5E Gauges", typeof(GaugeRenderer))]
    public class EngineTemp : BaseGauge
    {
        private HeliosValue _temperature;
        private GaugeNeedle _needle;
        private CalibrationPointCollectionDouble _needleCalibration;

        public EngineTemp()
            : base("Engine Temp", new Size(170d, 170d))
        {
            Point center = new Point(85d, 85d);
            Components.Add(new GaugeImage("{F-5E}/Gauges/EngineTemp/eng_temp_faceplate.xaml", new Rect(5d, 5d, 160d, 160d)));
            _needleCalibration = new CalibrationPointCollectionDouble(0d, 0d, 1200d, 275d)
            {
                new CalibrationPointDouble(0d, 2d),
                new CalibrationPointDouble(165d, 15d),
                new CalibrationPointDouble(210d, 30d),
                new CalibrationPointDouble(272d, 80d),
                new CalibrationPointDouble(480d, 145d),
                new CalibrationPointDouble(500d, 175d),
                new CalibrationPointDouble(600d, 180d),
                new CalibrationPointDouble(750d, 215d)
            };

            Components.Add(new GaugeImage("{F-5E}/Gauges/EngineTemp/TempGaugeRing.png", new Rect(0d, 0d, 170d, 170d)));
            _needle = new GaugeNeedle("{F-5E}/Gauges/Common/needle_general.xaml", center, new Size(48d, 77d), new Point(24d, 63d), 95d);
            Components.Add(_needle);
            _temperature = new HeliosValue(this, new BindingValue(0d), "", "temperature", "Current temperature of the aircraft enigne.", "(0 - 1200)", BindingValueUnits.Celsius);
            _temperature.Execute += new HeliosActionHandler(Temperature_Execute);
            Actions.Add(_temperature);
        }

        void Temperature_Execute(object action, HeliosActionEventArgs e)
        {
            _needle.Rotation = _needleCalibration.Interpolate(e.Value.DoubleValue);
        }
    }
}
