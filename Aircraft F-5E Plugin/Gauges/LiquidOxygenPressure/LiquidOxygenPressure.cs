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
    using System.Windows;

    [HeliosControl("Helios.F5E.LiquidOxygenPressure", "LiquidOxygenPressure",
        "F-5E Gauges", typeof(GaugeRenderer))]
    public class LiquidOxygenPressure : BaseGauge
    {
        private HeliosValue _liquidOxyPress;
        private GaugeNeedle _liquidOxyPressNeedle;
        private CalibrationPointCollectionDouble _needleCalibration;
        public LiquidOxygenPressure()
            : base("LiquidOxygenPressure", new Size(170d, 170d))
        {
            Point center = new Point(85d, 85d);

            Components.Add(new GaugeImage(
                "{F-5E}/Gauges/LiquidOxygenPressure/liquidoxypress_faceplate.xaml",
                new Rect(10d, 10d, 150d, 150d)));

            _liquidOxyPressNeedle = new GaugeNeedle(
                "{F-5E}/Gauges/Common/needle_general_black.xaml",
                center, new Size(48d, 77d), new Point(24d, 70d), 270d);
            Components.Add(_liquidOxyPressNeedle);

            _needleCalibration = new CalibrationPointCollectionDouble(0d, 0d, 1d, 160d)
            {
                new CalibrationPointDouble(0d, 0d),
                new CalibrationPointDouble(1d, 180d)
            };

            Components.Add(new GaugeImage("{F-5E}/Gauges/Common/OtherGaugeRing.png", new Rect(0d, 0d, 170d, 170d)));

            _liquidOxyPress = new HeliosValue(this, new BindingValue(0d), "", "indicated Liquid Oxygen Pressure",
                "Current Oxygen Liquid Pressure.", "(0-1)", BindingValueUnits.Numeric);
            _liquidOxyPress.Execute += new HeliosActionHandler(IndicatedHydraulic_Execute);
            Actions.Add(_liquidOxyPress);
        }

        public LiquidOxygenPressure(string name, Point posn)
            : base(name, new Size(170d, 170d))
        {
            Left = posn.X;
            Top = posn.Y;
            Point center = new Point(85d, 85d);

            Components.Add(new GaugeImage(
                "{F-5E}/Gauges/LiquidOxygenPressure/liquidoxypress_faceplate.xaml",
                new Rect(10d, 10d, 150d, 150d)));

            _liquidOxyPressNeedle = new GaugeNeedle(
                "{F-5E}/Gauges/Common/needle_general_black.xaml",
                center, new Size(48d, 77d), new Point(24d, 70d), 270d);
            Components.Add(_liquidOxyPressNeedle);

            _needleCalibration = new CalibrationPointCollectionDouble(0d, 0d, 1d, 160d)
            {
                new CalibrationPointDouble(0d, 0d),
                new CalibrationPointDouble(1d, 180d)
            };
            _liquidOxyPress = new HeliosValue(this, new BindingValue(0d), "", "indicated Liquid Oxygen Pressure",
                "Current Oxygen Liquid Pressure.", "(0-1)", BindingValueUnits.Numeric);
            _liquidOxyPress.Execute += new HeliosActionHandler(IndicatedHydraulic_Execute);
            Actions.Add(_liquidOxyPress);
        }

        void IndicatedHydraulic_Execute(object action, HeliosActionEventArgs e)
        {
            _liquidOxyPressNeedle.Rotation = _needleCalibration.Interpolate(e.Value.DoubleValue);
        }

    }
}
