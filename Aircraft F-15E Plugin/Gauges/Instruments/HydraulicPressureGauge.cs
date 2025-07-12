﻿//  Copyright 2014 Craig Courtney
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

namespace GadrocsWorkshop.Helios.Gauges.F15E.Instruments.HydraulicPressure
{
    using GadrocsWorkshop.Helios.ComponentModel;
    using System;
    using System.Windows;

    [HeliosControl("Helios.F15E.Instruments.HydraulicPressure", "Hydraulic Pressure gauge", "F-15E Strike Eagle", typeof(GaugeRenderer),HeliosControlFlags.NotShownInUI)]
    public class HydraulicPressureGauge : BaseGauge
    {
        private HeliosValue _pressure;
        private GaugeNeedle _needle;
        private CalibrationPointCollectionDouble _calibrationPoints;

        public HydraulicPressureGauge(string name, Size size, string device)
            : base(name, size)
        {
            Components.Add(new GaugeImage("{F-15E}/Gauges/Instruments/HydraulicPressureDial.xaml", new Rect(0d, 0d, 300d, 300d)));

            _needle = new GaugeNeedle("{F-15E}/Gauges/Instruments/HydraulicPressureNeedle.xaml", new Point(150d, 150d), new Size(58d, 142d), new Point(29d, 111d), 120d);
            Components.Add(_needle);

            _pressure = new HeliosValue(this, new BindingValue(0d), $"{device}_{name}", name, $"{name} value.", "(0 to 4,000)", BindingValueUnits.PoundsPerSquareInch);
            _pressure.Execute += new HeliosActionHandler(pressure_Execute);
            Actions.Add(_pressure);

            _calibrationPoints = new CalibrationPointCollectionDouble(0d, 0d, 4000d, 315d);
        }

        void pressure_Execute(object action, HeliosActionEventArgs e)
        {
            _needle.Rotation = _calibrationPoints.Interpolate(e.Value.DoubleValue);
        }
    }
}
