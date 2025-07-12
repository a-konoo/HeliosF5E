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

namespace GadrocsWorkshop.Helios.Gauges.F5E
{
    using GadrocsWorkshop.Helios.ComponentModel;
    using GadrocsWorkshop.Helios.Controls.F5E;
    using System;
    using System.Windows;

    [HeliosControl("Helios.F5E.LiquidOxygenPressureSolid", "LiquidOxygenPressure Solid",
        "F-5E Gauges", typeof(GaugeRenderer))]
    public class LiquidOxygenPressureSolid : BaseGauge
    {
        private HeliosValue _liquidOxyPress;
        private F5EDistortedGaugeNeedle _liquidOxyPressNeedle;
        private CalibrationPointCollectionDouble _needleCalibration;

        private GaugeImage _component;
        public LiquidOxygenPressureSolid()
            : base("LiquidOxygenPressureSolid", new Size(170d, 120d))
        {
            Point center = new  Point(85d, 65d);
            _component = new GaugeImage(
                "{F-5E}/Gauges/LiquidOxygenPressureSolid/liquidoxypress_distort_faceplate.xaml",
                new Rect(10d, 10d, 150d, 110d));

            Components.Add(_component);

            _liquidOxyPressNeedle = new F5EDistortedGaugeNeedle(
                "{F-5E}/Gauges/LiquidOxygenPressureSolid/needle_liquidoxypress_distort.xaml",
                center, new Size(48d, 77d), new Point(24d, 70d), 270d);
            Components.Add(_liquidOxyPressNeedle);

            
            Components.Add(_liquidOxyPressNeedle);

            _needleCalibration = new CalibrationPointCollectionDouble(0d, 0d, 1d, 160d)
            {
                new CalibrationPointDouble(0d, 0d),
                new CalibrationPointDouble(1d, 170d)
            };
            Components.Add(new GaugeImage("{F-5E}/Gauges/LiquidOxygenPressureSolid/needle_axis_liquidoxypress_distort.xaml", 
                new Rect(center.X -10d, center.Y - 7.5d, 20d, 15d)));
            Components.Add(new GaugeImage("{F-5E}/Gauges/Common/OtherGaugeRing.png", new Rect(0d, 5d, 170d, 120d)));

            _liquidOxyPress = new HeliosValue(this, new BindingValue(0d), "", "indicated Liquid Oxygen Pressure",
                "Current Oxygen Liquid Pressure.", "(0-1)", BindingValueUnits.Numeric);
            _liquidOxyPress.Execute += new HeliosActionHandler(IndicatedHydraulic_Execute);
            Actions.Add(_liquidOxyPress);
        }

        public LiquidOxygenPressureSolid(string name, Point posn,Size size)
            : base(name, new Size(162, 110))
        {
            // 170,120
            Left = posn.X;
            Top = posn.Y;
            Width = 162;
            Height = 110;
            Point center = new Point(size.Width * 0.5d, size.Height * 0.53);
            _component = new GaugeImage(
                "{F-5E}/Gauges/LiquidOxygenPressureSolid/liquidoxypress_distort_faceplate.xaml",
                new Rect(0.058d * size.Width, 0.083d * size.Height, 0.88d * size.Width, 0.833d * size.Height));

            Components.Add(_component);

            _liquidOxyPressNeedle = new F5EDistortedGaugeNeedle(
                "{F-5E}/Gauges/LiquidOxygenPressureSolid/needle_liquidoxypress_distort.xaml",
                center, new Size(0.282d * size.Width, 0.614d * size.Height),
                        new Point(0.141d * size.Width, 0.583d * size.Height), 270d);
            Components.Add(_liquidOxyPressNeedle);


            Components.Add(_liquidOxyPressNeedle);

            _needleCalibration = new CalibrationPointCollectionDouble(0d, 0d, 1d, 160d)
            {
                new CalibrationPointDouble(0d, 0d),
                new CalibrationPointDouble(1d, 170d)
            };
            Components.Add(new GaugeImage("{F-5E}/Gauges/LiquidOxygenPressureSolid/needle_axis_liquidoxypress_distort.xaml",
                new Rect(center.X - (0.058d * size.Width), center.Y - 0.75d * (0.058d * size.Width), 
                0.117d * size.Width, 0.125d * size.Height)));
            //Components.Add(new GaugeImage("{F-5E}/Gauges/Common/OtherGaugeRing.png", new Rect(0d, 0d , size.Width, size.Height)));

            _liquidOxyPress = new HeliosValue(this, new BindingValue(0d), "", "indicated Liquid Oxygen Pressure",
                "Current Oxygen Liquid Pressure.", "(0-1)", BindingValueUnits.Numeric);
            _liquidOxyPress.Execute += new HeliosActionHandler(IndicatedHydraulic_Execute);
            Actions.Add(_liquidOxyPress);
        }

        void IndicatedHydraulic_Execute(object action, HeliosActionEventArgs e)
        {
            var needleValue = e.Value.DoubleValue;
            var scaleAngle = 1.0d;
            _liquidOxyPressNeedle.Rotation = _needleCalibration.Interpolate(needleValue) - 30;
            scaleAngle = 1.0d - Math.Sin(Math.PI * (needleValue - 0.3 )) * 0.2;
            _liquidOxyPressNeedle.NeedleScaleOnAngle = scaleAngle;
        }
    }
}
