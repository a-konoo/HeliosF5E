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

namespace GadrocsWorkshop.Helios.Gauges.F5E
{
    using GadrocsWorkshop.Helios.ComponentModel;
    using System;
    using System.Windows;

    // Annotation Indicating the unique name for this control, display, toolbox group, and control renderer.
    [HeliosControl("Helios.F5E.AoA", "AoA", "F-5E Gauges", typeof(GaugeRenderer))]
    public class AOA : BaseGauge
    {
        private HeliosValue _aoa;
        private GaugeNeedle _needle;
        private CalibrationPointCollectionDouble _needleCalibration;

        private HeliosValue _offFlag;
        private GaugeNeedle _offFlagNeedle;
        private double MaxValue = 0.0d;
        public AOA()
            : base("Flight Instuments", new Size(220d, 220d))
        {
            Point center = new Point(110d, 110d);
            Point center2 = new Point(86d, 126d);



            Components.Add(new GaugeImage("{F-5E}/Gauges/AOA/aoa_backplate.xaml",
                new Rect(10d, 10d, 200d, 200d)));

            _offFlagNeedle = new GaugeNeedle("{F-5E}/Gauges/AOA/aoa_off_flag.xaml",
                center2, new Size(28d, 46d), new Point(10d, 68d), -44d);
            _offFlagNeedle.Rotation = -90;
            Components.Add(_offFlagNeedle);

            Components.Add(new GaugeImage("{F-5E}/Gauges/AOA/aoa_faceplate.xaml",
                new Rect(10d, 10d, 200d, 200d)));

            _needleCalibration = new CalibrationPointCollectionDouble(0d, 0d, 30d, 272d)
            {
                new CalibrationPointDouble(0d, 2d),
                new CalibrationPointDouble(30d, 272d)
            };

            _needle = new GaugeNeedle("{F-5E}/Gauges/AOA/needle_aoa.xaml",
                                      center,
                                      new Size(20d, 100d),
                                      new Point(10d, 80d),
                                      220d);
            Components.Add(_needle);


            _offFlag = new HeliosValue(this, new BindingValue(false),
                "Flight Instruments", "AoA Off Flag",
                "Indicates whether the AoA off flag is displayed.",
                "True if displayed.", BindingValueUnits.Boolean);
            _offFlag.Execute += new HeliosActionHandler(OffFlag_Execute);
            Actions.Add(_offFlag);

            _aoa = new HeliosValue(this,
                                   new BindingValue(0d),
                                   "Flight Instruments",
                                   "angle of attack",
                                   "Current angle of attack of the aircraft.",
                                   "(0 to 30)",
                                   BindingValueUnits.Degrees);
            _aoa.Execute += new HeliosActionHandler(AOA_Execute);
            Actions.Add(_aoa);

            Components.Add(new GaugeImage("{F-5E}/Gauges/AOA/AOAGaugeCover.png", new Rect(0d, 0d, 220d, 220d)));
        }

        // Event callback for angle updates
        void AOA_Execute(object action, HeliosActionEventArgs e)
        {
            Console.WriteLine("AOA" + e.Value.DoubleValue);
            MaxValue = MaxValue < e.Value.DoubleValue ? e.Value.DoubleValue : MaxValue;
            _needle.Rotation = -_needleCalibration.Interpolate(e.Value.DoubleValue);
        }
        void OffFlag_Execute(object action, HeliosActionEventArgs e)
        {
            _offFlag.SetValue(e.Value, e.BypassCascadingTriggers);
            _offFlagNeedle.Rotation = !e.Value.BoolValue ? 0 : -90;
            _offFlagNeedle.IsHidden = !e.Value.BoolValue;

        }
    }
}
