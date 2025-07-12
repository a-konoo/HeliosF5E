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
    using System.Windows;

    [HeliosControl("Helios.F5E.VVI", "VVI", "F-5E Gauges", typeof(GaugeRenderer))]
    public class VVI : BaseGauge
    {
        private HeliosValue _verticalVelocity;
        private GaugeNeedle _needle;
        private CalibrationPointCollectionDouble _calibrationPoints;

        public VVI()
            : base("Flight Instruments", new Size(220, 220))
        {
            Point center2 = new Point(110d, 116d);
            Components.Add(new GaugeImage("{Helios}/Gauges/A-10/VVI/vvi_faceplate.xaml", new Rect(22d, 27.5d, 174d, 174d)));

            _needle = new GaugeNeedle("{Helios}/Gauges/F-5E/VVI/needle_vvi.xaml",
                                      center2,
                                      new Size(20d, 85d),
                                      new Point(10d, 80d),
                                      -90d);
            Components.Add(_needle);

            _verticalVelocity = new HeliosValue(this, new BindingValue(0d), "Flight Instruments", "vertical velocity", "Veritcal velocity of the aircraft", "(-6,000 to 6,000)", BindingValueUnits.FeetPerMinute);
            _verticalVelocity.Execute += new HeliosActionHandler(VerticalVelocity_Execute);
            Actions.Add(_verticalVelocity);

            Components.Add(new GaugeImage("{Helios}/Gauges/F-5E/VVI/VVIGaugeCover.png", new Rect(0d, 0d, 220d, 220d)));

            _calibrationPoints = new CalibrationPointCollectionDouble(-6000d, -169d, 6000d, 169d);
            _calibrationPoints.Add(new CalibrationPointDouble(-2000d, -81d));
            _calibrationPoints.Add(new CalibrationPointDouble(-1000d, -45d));
            _calibrationPoints.Add(new CalibrationPointDouble(0d, 0d));
            _calibrationPoints.Add(new CalibrationPointDouble(1000d, 45d));
            _calibrationPoints.Add(new CalibrationPointDouble(2000d, 81d));
        }

        void VerticalVelocity_Execute(object action, HeliosActionEventArgs e)
        {
            _needle.Rotation = _calibrationPoints.Interpolate(e.Value.DoubleValue);
        }
    }
}
