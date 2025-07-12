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

namespace GadrocsWorkshop.Helios.Gauges.F5E.Hydraulic
{
    using GadrocsWorkshop.Helios.ComponentModel;
    using System.Windows;

    [HeliosControl("Helios.F5E.Hydraulic", "Hydraulic", "F-5E Gauges", typeof(GaugeRenderer))]
    public class LiquidOxygenQty : BaseGauge
    {
        private HeliosValue _oxygenQty;
        private GaugeNeedle _oxygenGauge;
        private CalibrationPointCollectionDouble _needleCalibration;
        public LiquidOxygenQty()
            : base("Hydraulic", new Size(170, 170))
        {
            Point center = new Point(85d, 85d);

            Components.Add(new GaugeImage("{F-5E}/Gauges/Hydraulic/hydraulic_faceplate.xaml", new Rect(10d, 10d, 150, 150)));
            Components.Add(new GaugeImage("{F-5E}/Gauges/Common/OtherGaugeRing.png", new Rect(0d, 0d, 170d, 170d)));

            _oxygenGauge = new GaugeNeedle("{F-5E}/Gauges/Common/needle_general.xaml", center, new Size(48d, 77d), new Point(24d, 63d), 135d);
            Components.Add(_oxygenGauge);

            _needleCalibration = new CalibrationPointCollectionDouble(0d, 0d, 1d, 300d)
            {
                new CalibrationPointDouble(0d, 0d),
                new CalibrationPointDouble(1d, 300d),
            };
            _oxygenQty = new HeliosValue(this, new BindingValue(0d), "", "indicated Hydraulic",
                "Current Hydraulic of the aircraft.", "(0-1)", BindingValueUnits.Numeric);
            _oxygenQty.Execute += new HeliosActionHandler(IndicatedHydraulic_Execute);
            Actions.Add(_oxygenQty);
        }

        void IndicatedHydraulic_Execute(object action, HeliosActionEventArgs e)
        {
            _oxygenGauge.Rotation = _needleCalibration.Interpolate(e.Value.DoubleValue);
        }

    }
}
