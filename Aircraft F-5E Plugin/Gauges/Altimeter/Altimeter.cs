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

namespace GadrocsWorkshop.Helios.Gauges.F5E.ADI
{
    using GadrocsWorkshop.Helios.ComponentModel;
    using System.Windows;
    using System.Windows.Media;

    [HeliosControl("Helios.F5E.Altimeter", "Altimeter", "F-5E Gauges", typeof(GaugeRenderer))]
    public class Altimeter : BaseGauge
    {
        private HeliosValue _altitdue;
        private HeliosValue _airPressure;
        private HeliosValue _preuValue;
        private GaugeNeedle _needle;
        private CalibrationPointCollectionDouble _needleCalibration;
        private GaugeDrumCounter _tensDrum;
        private GaugeDrumCounter _drum;
        private GaugeDrumCounter _airPressureDrum;
        private CalibrationPointCollectionDouble _pneuTapeCalibration;
        private GaugeNeedle _pneuTape;

        public Altimeter()
            : base("Altimeter", new Size(364, 376))
        {
            Components.Add(new GaugeImage("{F-5E}/Gauges/Altimeter/altimeter_backplate.xaml", new Rect(32d, 38d, 300d, 300d)));

            _pneuTapeCalibration = new CalibrationPointCollectionDouble(0d, 0d, 50d, 50d);
            _pneuTape = new GaugeNeedle("{F-5E}/Gauges/Altimeter/pneu_tape.xaml", new Point(85d, 120d), new Size(80d, 20d), new Point(0d, 0d));
            _pneuTape.HorizontalOffset = -_pneuTapeCalibration.Interpolate(0d);
            _pneuTape.Clip = new RectangleGeometry(new Rect(85d, 120d, 45d, 20d));
            Components.Add(_pneuTape);

            _tensDrum = new GaugeDrumCounter("{F-5E}/Gauges/Altimeter/alt_drum_tape.xaml", new Point(71d, 164d), "#", new Size(10d, 15d), new Size(30d, 45d));
            _tensDrum.Clip = new RectangleGeometry(new Rect(71d, 144d, 150d, 81d));
            Components.Add(_tensDrum);

            _drum = new GaugeDrumCounter("{F-5E}/Gauges/Common/drum_tape.xaml", new Point(101d, 164d), "#%00", new Size(10d, 15d), new Size(30d, 45d));
            _drum.Clip = new RectangleGeometry(new Rect(101d, 144d, 150d, 81d));
            Components.Add(_drum);

            _airPressureDrum = new GaugeDrumCounter("{F-5E}/Gauges/Common/drum_tape.xaml", new Point(214d, 233d), "###%", new Size(10d, 15d), new Size(15d, 20d));
            _airPressureDrum.Value = 2992d;
            _airPressureDrum.Clip = new RectangleGeometry(new Rect(214d, 233d, 60d, 20d));
            Components.Add(_airPressureDrum);

            Components.Add(new GaugeImage("{F-5E}/Gauges/Altimeter/altimeter_faceplate.xaml", new Rect(32d, 38d, 300d, 300d)));

            _needleCalibration = new CalibrationPointCollectionDouble(0d, 0d, 1000d, 360d);
            _needle = new GaugeNeedle("{F-5E}/Gauges/Altimeter/altimeter_needle.xaml", new Point(182d, 188d), new Size(16d, 257d), new Point(8d, 138.5d));
            Components.Add(_needle);

            Components.Add(new GaugeImage("{F-5E}/Gauges/Altimeter/AltiGaugeCover.png", new Rect(20d, 20d, 364d * 0.9, 376d * 0.88)));

            _airPressure = new HeliosValue(this, new BindingValue(0d), "", "air pressure", "Current air pressure calibaration setting for the altimeter.", "", BindingValueUnits.InchesOfMercury);
            _airPressure.SetValue(new BindingValue(29.92), true);
            _airPressure.Execute += new HeliosActionHandler(AirPressure_Execute);
            Actions.Add(_airPressure);

            _altitdue = new HeliosValue(this, new BindingValue(0d), "", "altitude", "Current altitude of the aricraft.", "", BindingValueUnits.Feet);
            _altitdue.Execute += new HeliosActionHandler(Altitude_Execute);
            Actions.Add(_altitdue);

            _preuValue = new HeliosValue(this, new BindingValue(0d), "", "pneuValue", "Current Mode of altimeter(elec/pneu)", "", BindingValueUnits.Numeric);
            _preuValue.Execute += new HeliosActionHandler(PneuFlagDisplay_Execute);
            Actions.Add(_preuValue);
        }

        void Altitude_Execute(object action, HeliosActionEventArgs e)
        {
            _needle.Rotation = _needleCalibration.Interpolate(e.Value.DoubleValue % 1000d);
            _tensDrum.Value = e.Value.DoubleValue / 10000d;

            double thousands = (e.Value.DoubleValue / 100d) % 100d;
            if (thousands >= 99)
            {
                _tensDrum.StartRoll = thousands % 1d;
            }
            else
            {
                _tensDrum.StartRoll = -1d;
            }
            _drum.Value = e.Value.DoubleValue;
        }

        void AirPressure_Execute(object action, HeliosActionEventArgs e)
        {
            _airPressureDrum.Value = e.Value.DoubleValue * 100d;
        }

        void PneuFlagDisplay_Execute(object action, HeliosActionEventArgs e)
        {
            var flagToOffset = e.Value.DoubleValue < 1 ? 0 : 40;
            _pneuTape.HorizontalOffset = -_pneuTapeCalibration.Interpolate(flagToOffset);
        }
    }
}
