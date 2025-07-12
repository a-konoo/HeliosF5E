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

namespace GadrocsWorkshop.Helios.Gauges.F5E.NozzleIndicator
{
    using GadrocsWorkshop.Helios.ComponentModel;
    using System.Windows;

    [HeliosControl("Helios.F5E.NozzleIndicator", "NozzleIndicator", "F-5E Gauges", typeof(GaugeRenderer))]
    public class NozzleIndicator : BaseGauge
    {
        private HeliosValue _nozzlePosition;
        private GaugeNeedle _needle;
        private CalibrationPointCollectionDouble _needleCalibration;

        public NozzleIndicator()
            : base("Nozzle Indicator", new Size(170d, 170d))
        {
            Point center = new Point(85d, 85d);
            Components.Add(new GaugeImage("{F-5E}/Gauges/NozzleIndicator/nozzle_faceplate.xaml", new Rect(10d, 10d, 150d, 150d)));

            _needleCalibration = new CalibrationPointCollectionDouble(0d, 0d, 100d, 240d) {
                new CalibrationPointDouble(0d, 0d),
                new CalibrationPointDouble(40d, 80d),
                new CalibrationPointDouble(60d, 110d),
                new CalibrationPointDouble(80d, 170d),
                new CalibrationPointDouble(100d, 240d)

            };
            Components.Add(new GaugeImage("{F-5E}/Gauges/Common/OtherGaugeRing.png", new Rect(0d, 0d, 170d, 170d)));
            _needle = new GaugeNeedle("{F-5E}/Gauges/Common/needle_general.xaml", center, new Size(48d, 77d), new Point(24d, 63d), 70d);
            Components.Add(_needle);

            _nozzlePosition = new HeliosValue(this, new BindingValue(0d), "", "percent", "Percent of the nozzle position.", "(0 - 100)", BindingValueUnits.Numeric);
            _nozzlePosition.Execute += new HeliosActionHandler(NozzleIndicator_Execute);
            Actions.Add(_nozzlePosition);
        }

        void NozzleIndicator_Execute(object action, HeliosActionEventArgs e)
        {
            _needle.Rotation = _needleCalibration.Interpolate(e.Value.DoubleValue);
        }
    }
}

