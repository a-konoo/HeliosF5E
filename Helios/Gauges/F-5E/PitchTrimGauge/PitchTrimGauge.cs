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

namespace GadrocsWorkshop.Helios.Gauges.F5E.PitchTrimGauge
{
    using GadrocsWorkshop.Helios.ComponentModel;
    using System.Windows;

    [HeliosControl("Helios.F5E.PitchTrimGauge", "PitchTrimGauge", "F-5E Gauges", typeof(GaugeRenderer))]
    public class PitchTrimGauge : BaseGauge
    {
        private HeliosValue _pitchTrim;
        private GaugeNeedle _pitchTrimNeedle;
        private CalibrationPointCollectionDouble _needleCalibration;
        public PitchTrimGauge()
            : base("PitchTrimGauge", new Size(170d, 170d))
        {
            Point center = new Point(85d, 85d);

            Components.Add(new GaugeImage("{Helios}/Gauges/F-5E/PitchTrimGauge/pitch_faceplate.xaml",
                new Rect(10d, 10d, 150d, 150d)));
            Components.Add(new GaugeImage("{Helios}/Gauges/F-5E/Common/OtherGaugeRing.png", new Rect(0d, 0d, 170d, 170d)));

            _pitchTrimNeedle = new GaugeNeedle("{Helios}/Gauges/F-5E/Common/needle_general.xaml",
                center, new Size(48d, 77d), new Point(24d, 63d), 180d);
            Components.Add(_pitchTrimNeedle);

            _needleCalibration = new CalibrationPointCollectionDouble(-0.1d, -15d, 1d, 180d)
            {
                new CalibrationPointDouble(-0.1d, -15d),
                new CalibrationPointDouble(0d, 0d),
                new CalibrationPointDouble(1d, 180d)
            };
            _pitchTrim = new HeliosValue(this, new BindingValue(0d), "", "indicated Pitch Trim.",
                "Current Pitch Trim.", "(-0.1-1.0)", BindingValueUnits.Numeric);
            _pitchTrim.Execute += new HeliosActionHandler(IndicatedPitchTrim_Execute);
            Actions.Add(_pitchTrim);
        }

        void IndicatedPitchTrim_Execute(object action, HeliosActionEventArgs e)
        {
            _pitchTrimNeedle.Rotation = _needleCalibration.Interpolate(e.Value.DoubleValue);
        }

    }
}
