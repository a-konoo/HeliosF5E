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
//5
//  You should have received a copy of the GNU General Public License
//  along with this program.  If not, see <http://www.gnu.org/licenses/>.

namespace GadrocsWorkshop.Helios.Gauges.F5E
{
    using GadrocsWorkshop.Helios.ComponentModel;
    using System;
    using System.Windows;
    using System.Windows.Media;

    // Annotation Indicating the unique name for this control, display, toolbox group, and control renderer.
    [HeliosControl("Helios.F5E.O2Blinker", "O2 Blinker", "F-5E Gauges", typeof(GaugeRenderer),HeliosControlFlags.NotShownInUI)]
    public class O2Blinker : BaseGauge
    {
        private HeliosValue _airValue;
        private GaugeNeedle _oxyNeedle;
        private CalibrationPointCollectionDouble _oxyNeedleCalibration;
        private double _angle = 0d;
        private double MaxValue = 0.0d;
        public O2Blinker() : base("O2 Blinker", new Size(100d, 88d))
        {

            _oxyNeedleCalibration = new CalibrationPointCollectionDouble(0d, 0d, 1d, 150d);
            _oxyNeedle = new GaugeNeedle("{F-5E}/Gauges/Common/o2blinker.xaml", new Point(0d,0d), new Size(400d, 88d), new Point(220d, 0d));
            _oxyNeedle.Clip = new RectangleGeometry(new Rect(0d, 0d, 100d, 88d));
            Components.Add(_oxyNeedle);

            _airValue = new HeliosValue(this, new BindingValue(0d), "", "O2 Blinker Flow Value", "O2 Blinker Flow Value(1:on,0:off)", "(0 to 1)", BindingValueUnits.Numeric);
            _airValue.Execute += new HeliosActionHandler(OxyAirGauge_Execute);
            _angle = 0d;
            Actions.Add(_airValue);
        }


        public O2Blinker(string name, Point posn, Size size, int calibMin, int calibMax, Point center, double angle)
            : base(name, new Size(size.Width, size.Height))
        {
            Left = posn.X;
            Top = posn.Y;

            _oxyNeedleCalibration = new CalibrationPointCollectionDouble(0d, 0d, calibMin, calibMax);
            _oxyNeedle = new GaugeNeedle("{F-5E}/Gauges/Common/o2blinker.xaml", new Point(0d, 0d),
                new Size(size.Width * 4, size.Height), new Point(center.X, center.Y));
            _oxyNeedle.Clip = new RectangleGeometry(new Rect(0d, 0d, size.Width, size.Height));
            /*
            _oxyNeedleCalibration = new CalibrationPointCollectionDouble(0d, 0d, 1d, 150d);
            _oxyNeedle = new GaugeNeedle("{F-5E}/Gauges/Common/o2blinker.xaml", new Point(0d, 0d), new Size(400d, 88d), new Point(220d, 0d));
            _oxyNeedle.Clip = new RectangleGeometry(new Rect(0d, 0d, 100d, 88d));
            */
            Components.Add(_oxyNeedle);

            _airValue = new HeliosValue(this, new BindingValue(0d), "", 
                "O2 Blinker Flow Value", "O2 Blinker Flow Value(1:on,0:off)", "(0 to 1)", BindingValueUnits.Numeric);
            _airValue.Execute += new HeliosActionHandler(OxyAirGauge_Execute);
            _angle = angle;
            Actions.Add(_airValue);
        }


        // Event callback for angle updates
        void OxyAirGauge_Execute(object action, HeliosActionEventArgs e)
        {
            MaxValue = MaxValue < e.Value.DoubleValue ? e.Value.DoubleValue : MaxValue;
            _oxyNeedle.Rotation = _angle;
            _oxyNeedle.HorizontalOffset = _oxyNeedleCalibration.Interpolate(Math.Abs(e.Value.DoubleValue));
        }
    }
}
