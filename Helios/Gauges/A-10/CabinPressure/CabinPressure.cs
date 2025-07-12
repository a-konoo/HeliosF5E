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

namespace GadrocsWorkshop.Helios.Gauges.A_10.CabinPressure
{
    using GadrocsWorkshop.Helios.ComponentModel;
    using System;
    using System.Windows;

    [HeliosControl("Helios.A10.CabinPressure", "Cabin Pressure", "A-10 Gauges", typeof(GaugeRenderer), HeliosControlFlags.NotShownInUI)]
    public class CabinPressure : BaseGauge
    {
        private HeliosValue _pressure;
        private GaugeNeedle _needle;
        private CalibrationPointCollectionDouble _calibrationPoints;

        public CabinPressure()
            : base("Cabin Pressure", new Size(364, 376))
        {
            Components.Add(new GaugeImage("{Helios}/Gauges/A-10/CabinPressure/cabin_pressure_faceplate.xaml", new Rect(32d, 38d, 300d, 300d)));

            _needle = new GaugeNeedle("{Helios}/Gauges/A-10/CabinPressure/cabin_pressure_needle.xaml", new Point(182d, 188d), new Size(53d, 158d), new Point(26.5d, 26.5d), 0d);
            Components.Add(_needle);

            Components.Add(new GaugeImage("{Helios}/Gauges/A-10/Common/gauge_bezel.png", new Rect(0d, 0d, 364d, 376d)));

            _pressure = new HeliosValue(this, new BindingValue(0d), "", "cabin pressure", "Cabin pressure of the aircraft.", "(0 to 50,000)", BindingValueUnits.Numeric);
            _pressure.Execute += new HeliosActionHandler(CabinPressure_Execute);
            Actions.Add(_pressure);

            _calibrationPoints = new CalibrationPointCollectionDouble(0d, 0d, 50000d, 315d);
        }

        void CabinPressure_Execute(object action, HeliosActionEventArgs e)
        {
            _needle.Rotation = _calibrationPoints.Interpolate(e.Value.DoubleValue);
        }
    }
}
