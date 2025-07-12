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

    [HeliosControl("Helios.F5E.LiquidOxygenQtySolid", "LiquidOxygenQtySolid", "F-5E Gauges", typeof(GaugeRenderer))]
    public class LiquidOxygenQtySolid : BaseGauge
    {
        private HeliosValue _liquidOxyQty;
        private CalibrationPointCollectionDouble _needleCalibration;
        private CalibrationPointCollectionDouble _scaleCalibration;
        private GaugeImage _component;
        private F5EDistortedGaugeNeedle _liquidOxyQtyNeedle;
        public LiquidOxygenQtySolid()
            : base("LiquidOxygenQtySolid", new Size(120d, 170d))
        {
            Point center = new Point(65d, 85d);

            _component = new GaugeImage(
                "{F-5E}/Gauges/LiquidOxygenQtySolid/liquidoxyqty_distort_faceplate.xaml",
                new Rect(5d, 10d, 109d, 160d));

            Components.Add(_component);

            _liquidOxyQtyNeedle = new F5EDistortedGaugeNeedle(
                "{F-5E}/Gauges/LiquidOxygenQtySolid/needle_liquidoxyqty_distort.xaml",
                new Point(center.X + 2.5, center.Y + 12d), new Size(48d, 77d), new Point(24d, 70d), 180d);
            Components.Add(_liquidOxyQtyNeedle);

            _needleCalibration = new CalibrationPointCollectionDouble(0d, 0d, 180d, 360d)
            {
                new CalibrationPointDouble(0d, 0d),
                new CalibrationPointDouble(1d, 180d)
            };

            Components.Add(new GaugeImage("{F-5E}/Gauges/LiquidOxygenQtySolid/needle_axis_liquidoxyqty_distort.xaml",
                new Rect(center.X - 12.5d, center.Y-2.0d, 25d, 30d)));

            Components.Add(new GaugeImage("{F-5E}/Gauges/Common/OtherGaugeRing.png", new Rect(0d, 5d, 120d, 170d)));

            _liquidOxyQty = new HeliosValue(this, new BindingValue(0d), "", "indicated Liquid Oxygen Pressure",
                "Current Oxygen Liquid Pressure.", "(0-1)", BindingValueUnits.Numeric);
            _liquidOxyQty.Execute += new HeliosActionHandler(IndicatedLiquidOxygenQty_Execute);
            Actions.Add(_liquidOxyQty);
        }

        public LiquidOxygenQtySolid(string name, Point posn, Size size)
            : base(name, new Size(size.Width, size.Height))
        {
            Left = posn.X;
            Top = posn.Y;
            Width = size.Width;
            Height = size.Height;
            Point center = new Point(size.Width * 0.54d, size.Height * 0.5);
            _component = new GaugeImage(
                "{F-5E}/Gauges/LiquidOxygenQtySolid/liquidoxyqty_distort_faceplate.xaml",
                new Rect(0.041d * size.Width, 0.058d * size.Height, 0.91d * size.Width, 0.941d * size.Height));

            Components.Add(_component);

            _liquidOxyQtyNeedle = new F5EDistortedGaugeNeedle(
                "{F-5E}/Gauges/LiquidOxygenQtySolid/needle_liquidoxyqty_distort.xaml",
                new Point(center.X + 0.02d * size.Width, center.Y + 0.071d * size.Height),
                new Size(0.4d * size.Width, 0.453d * size.Height),
                        new Point(0.2d * size.Width, 0.411d * size.Height), 180d);
            Components.Add(_liquidOxyQtyNeedle);

            _needleCalibration = new CalibrationPointCollectionDouble(0d, 0d, 0d, 185d)
            {
                new CalibrationPointDouble(0d, 0d),
                new CalibrationPointDouble(1d, 185d)
            };

            Components.Add(new GaugeImage("{F-5E}/Gauges/LiquidOxygenQtySolid/needle_axis_liquidoxyqty_distort.xaml",
                new Rect(
                    center.X - 0.1d * size.Width,
                    center.Y - 0.012d * size.Height, 0.153d * size.Width, 0.133d * size.Height)));

            Components.Add(new GaugeImage("{F-5E}/Gauges/Common/OtherGaugeRing.png",
                new Rect(0d, 0.041d * size.Height, size.Width, size.Height)));

            _liquidOxyQty = new HeliosValue(this, new BindingValue(0d), "", "indicated Liquid Oxygen Pressure",
                "Current Oxygen Liquid Pressure.", "(0-1)", BindingValueUnits.Numeric);
            _liquidOxyQty.Execute += new HeliosActionHandler(IndicatedLiquidOxygenQty_Execute);
            Actions.Add(_liquidOxyQty);
        }



        void IndicatedLiquidOxygenQty_Execute(object action, HeliosActionEventArgs e)
        {
            var needleValue = e.Value.DoubleValue;
            var scaleAngle = 1.0d;
            _liquidOxyQtyNeedle.Rotation = _needleCalibration.Interpolate(needleValue)+56;
            scaleAngle = 0.9d;
            _liquidOxyQtyNeedle.NeedleScaleOnAngle = scaleAngle;
        }

    }
}
