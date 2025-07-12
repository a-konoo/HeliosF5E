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
    using System.Windows.Media;

    // Annotation Indicating the unique name for this control, display, toolbox group, and control renderer.
    [HeliosControl("Helios.F5E.O2BlinkerSolid", "O2 Blinker Solid", "F-5E Gauges", typeof(GaugeRenderer), HeliosControlFlags.NotShownInUI)]
    public class O2BlinkerSolid : BaseGauge
    {
        private HeliosValue _airValue;
        private GaugeNeedle _oxyNeedle;
        private CalibrationPointCollectionDouble _oxyNeedleCalibration;

        private double MaxValue = 0.0d;
        public O2BlinkerSolid() : base("O2BlinkerSolid", new Size(133d, 117d))
        {

            _oxyNeedleCalibration = new CalibrationPointCollectionDouble(0d, 0d, 1d, 200d);
            _oxyNeedle = new GaugeNeedle("{F-5E}/Gauges/Common/o2blinker.xaml", new Point(0d,0d), new Size(400d, 88d), new Point(220d, 0d));
            _oxyNeedle.Clip = new RectangleGeometry(new Rect(0d, 0d, 133d, 117d));
            Components.Add(_oxyNeedle);

            _airValue = new HeliosValue(this, new BindingValue(0d), "", "O2 Blinker Flow(Solid) Value", "O2 Blinker Flow(Solid) Value(1:on,0:off)", "(0 to 1)", BindingValueUnits.Numeric);
            _airValue.Execute += new HeliosActionHandler(OxyAirGauge_Execute);
            Actions.Add(_airValue);
        }

        // Event callback for angle updates
        void OxyAirGauge_Execute(object action, HeliosActionEventArgs e)
        {
            MaxValue = MaxValue < e.Value.DoubleValue ? e.Value.DoubleValue : MaxValue;
            _oxyNeedle.Rotation = -30d;
            _oxyNeedle.HorizontalOffset = _oxyNeedleCalibration.Interpolate(Math.Abs(e.Value.DoubleValue));
        }
    }
}
