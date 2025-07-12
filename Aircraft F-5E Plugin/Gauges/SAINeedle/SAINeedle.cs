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

    [HeliosControl("Helios.F5E.SAINeedle", "SAINeedle", "F-5E Gauges", typeof(GaugeRenderer), HeliosControlFlags.NotShownInUI)]
    public class SAINeedle : BaseGauge
    {
        private HeliosValue _saiPitchValue;
        private GaugeNeedle _saiNeedle;
        private CalibrationPointCollectionDouble _needleCalibration;
        public SAINeedle()
            : base("SAINeedle", new Size(45d, 45d))
        {
            Point center = new Point(75d, 75d);

            Components.Add(new GaugeImage("{F-5E}/Gauges/SAINeedle/fuelqty_faceplate.xaml", new Rect(0d, 0d, 45d, 45d)));

            _saiNeedle = new GaugeNeedle("{F-5E}/Gauges/SAINeedle/sai_needle.xaml", center, new Size(75d, 75d), new Point(37.5d, 37.5d), 180d);
            Components.Add(_saiNeedle);


            _needleCalibration = new CalibrationPointCollectionDouble(-1d, 180d, 1d, 270d);

            _saiPitchValue = new HeliosValue(this, new BindingValue(0d), "", "SAI Knob Needle",
                "Current Pitch Trim value.", "(-1 - 1)", BindingValueUnits.Numeric);
            _saiPitchValue.Execute += new HeliosActionHandler(IndicatedSAINeedle_Execute);
            Actions.Add(_saiPitchValue);
        }

        public SAINeedle(string name, Point posn)
            : base(name, new Size(40d, 40d))
        {
            Left = posn.X;
            Top = posn.Y;
            Point center = new Point(34d, 34d);

            Components.Add(new GaugeImage("{F-5E}/Gauges/SAINeedle/fuelqty_faceplate.xaml", new Rect(0d, 0d, 40d, 40d)));

            _saiNeedle = new GaugeNeedle("{F-5E}/Gauges/SAINeedle/sai_needle.xaml", center, new Size(60d, 60d), new Point(30d, 30d), 180d);
            Components.Add(_saiNeedle);


            _needleCalibration = new CalibrationPointCollectionDouble(-1d, 180d, 1d, 270d);

            _saiPitchValue = new HeliosValue(this, new BindingValue(0d), "", "SAI Knob Needle",
                "Current Pitch Trim value.", "(-1 - 1)", BindingValueUnits.Numeric);
            _saiPitchValue.Execute += new HeliosActionHandler(IndicatedSAINeedle_Execute);
            Actions.Add(_saiPitchValue);
        }

        void IndicatedSAINeedle_Execute(object action, HeliosActionEventArgs e)
        {
            _saiNeedle.Rotation = _needleCalibration.Interpolate(e.Value.DoubleValue);
        }

    }
}
