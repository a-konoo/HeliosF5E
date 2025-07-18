﻿//  Copyright 2014 Craig Courtney
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

namespace GadrocsWorkshop.Helios.Gauges.F5E.FuelFlow
{
    using GadrocsWorkshop.Helios.ComponentModel;
    using System.Windows;

    [HeliosControl("Helios.F5E.FuelFlow", "FuelFlow", "F-5E Gauges", typeof(GaugeRenderer))]
    public class FuelFlow : BaseGauge
    {
        private HeliosValue _fuelflowRight;
        private HeliosValue _fuelflowLeft;
        private GaugeNeedle _needleFlowRight;
        private GaugeNeedle _needleFlowLeft;
        private CalibrationPointCollectionDouble _needleCalibration;
        public FuelFlow()
            : base("FuelFlow", new Size(170d, 170d))
        {
            Point center = new Point(85d, 85d);

            Components.Add(new GaugeImage("{F-5E}/Gauges/FuelFlow/fuelflow_faceplate.xaml", new Rect(10d, 10d, 150d, 150d)));

            _needleFlowLeft = new GaugeNeedle("{F-5E}/Gauges/Common/needle_accl_limit.xaml", center, new Size(48d, 77d), new Point(24d, 63d), 15d);
            Components.Add(_needleFlowLeft);

            _needleFlowRight = new GaugeNeedle("{F-5E}/Gauges/Common/needle_accr_limit.xaml", center, new Size(48d, 77d), new Point(24d, 63d), 15d);
            Components.Add(_needleFlowRight);

            Components.Add(new GaugeImage("{F-5E}/Gauges/Common/OtherGaugeRing.png", new Rect(0d, 0d, 170d, 170d)));

            _needleCalibration = new CalibrationPointCollectionDouble(0d, 0d, 15000d, 345d)
            {
                new CalibrationPointDouble(0d, 0d),
                new CalibrationPointDouble(2400d, 60d),
                new CalibrationPointDouble(4600d, 90d),
                new CalibrationPointDouble(10000d, 195d),
                new CalibrationPointDouble(11000d, 210d),
                new CalibrationPointDouble(12000d, 275d),
                new CalibrationPointDouble(15000d, 345d)
            };

            _fuelflowRight = new HeliosValue(this, new BindingValue(0d), "", "Fuel Flow Right",
                "Current Fuel Flow Right value.", "(0 - 15000)", BindingValueUnits.Numeric);
            _fuelflowRight.Execute += new HeliosActionHandler(IndicatedFuelFlowRight_Execute);
            Actions.Add(_fuelflowRight);

            _fuelflowLeft = new HeliosValue(this, new BindingValue(0d), "", "Fuel Flow Left",
                "Current accelerometer Max value.", "(0 - 15000)", BindingValueUnits.Numeric);
            _fuelflowLeft.Execute += new HeliosActionHandler(IndicatedFuelFlowLeft_Execute);
            Actions.Add(_fuelflowLeft);
        }

        void IndicatedFuelFlowRight_Execute(object action, HeliosActionEventArgs e)
        {
            _needleFlowRight.Rotation = _needleCalibration.Interpolate(e.Value.DoubleValue);
        }

        void IndicatedFuelFlowLeft_Execute(object action, HeliosActionEventArgs e)
        {
            _needleFlowLeft.Rotation = _needleCalibration.Interpolate(e.Value.DoubleValue);
        }
    }
}
