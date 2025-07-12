//  Copyright 2014 Craig Courtney
//  Copyright 2020 Ammo Goettsch
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

namespace GadrocsWorkshop.Helios.Interfaces.DCS.F5E.Functions
{
    using GadrocsWorkshop.Helios.Interfaces.DCS.Common;
    using GadrocsWorkshop.Helios.UDPInterface;
    using GadrocsWorkshop.Helios.Util;
    using System;
    using System.Globalization;
    using System.Text;


    // TacanPanel Knobs values


    public class TACANKnobs : DCSFunction
    {

        private static readonly ExportDataElement[] DataElementsTemplate = {
            new DCSDataElement("2256", null, false) };

        private HeliosValue _knobsAndToggles;

        public TACANKnobs(BaseUDPInterface sourceInterface)
            : base(sourceInterface,
                   "TACAN", "TACAN Panel KnobsArray", "Currently tuned TACAN Panel Knobs."
                  )
        {
            DoBuild();
        }

        // 35step convert
        private static readonly CalibrationPointCollectionDouble calib35Step =
        new CalibrationPointCollectionDouble(0, 0, 1d, 35d)
        {
                new CalibrationPointDouble(0.0d, 0d),
                new CalibrationPointDouble(0.028d, 1d),
                new CalibrationPointDouble(0.057d, 2d),
                new CalibrationPointDouble(0.085d, 3d),
                new CalibrationPointDouble(0.114d, 4d),
                new CalibrationPointDouble(0.143d, 5d),
                new CalibrationPointDouble(0.171d, 6d),
                new CalibrationPointDouble(0.2d, 7d),
                new CalibrationPointDouble(0.229d, 8d),
                new CalibrationPointDouble(0.257d, 9d),
                new CalibrationPointDouble(0.285d, 10d),
                new CalibrationPointDouble(0.314d, 11d),
                new CalibrationPointDouble(0.343d, 12d),
                new CalibrationPointDouble(0.371d, 13d),
                new CalibrationPointDouble(0.4d, 14d),
                new CalibrationPointDouble(0.428d, 15d),
                new CalibrationPointDouble(0.457d, 16d),
                new CalibrationPointDouble(0.485d, 17d),
                new CalibrationPointDouble(0.514d, 18d),
                new CalibrationPointDouble(0.542d, 19d),
                new CalibrationPointDouble(0.571d, 20d),
                new CalibrationPointDouble(0.6d, 21d),
                new CalibrationPointDouble(0.628d, 22d),
                new CalibrationPointDouble(0.657d, 23d),
                new CalibrationPointDouble(0.685d, 24d),
                new CalibrationPointDouble(0.714d, 25d),
                new CalibrationPointDouble(0.742d, 26d),
                new CalibrationPointDouble(0.771d, 27d),
                new CalibrationPointDouble(0.8d, 28d),
                new CalibrationPointDouble(0.829d, 29d),
                new CalibrationPointDouble(0.857d, 30d),
                new CalibrationPointDouble(0.885d, 31d),
                new CalibrationPointDouble(0.914d, 32d),
                new CalibrationPointDouble(0.942d, 33d),
                new CalibrationPointDouble(0.971d, 34d),
                new CalibrationPointDouble(1.0d, 35d)
        };
        // simple convert
        private static readonly CalibrationPointCollectionDouble calibSimple =
        new CalibrationPointCollectionDouble(0, 0, 1d, 3d)
        {
            new CalibrationPointDouble(0.0d, 0d),
            new CalibrationPointDouble(0.1d, 1d),
            new CalibrationPointDouble(0.2d, 2d),
            new CalibrationPointDouble(0.3d, 3d),
        };
        
        // deserialization constructor
        public TACANKnobs(BaseUDPInterface sourceInterface, System.Runtime.Serialization.StreamingContext context)
            : base(sourceInterface, context)
        {
            // no code
        }

        public override void BuildAfterDeserialization()
        {
            DoBuild();
        }

        private void DoBuild()
        {
            _knobsAndToggles = new HeliosValue(SourceInterface, BindingValue.Empty, SerializedDeviceName, SerializedFunctionName,
                SerializedDescription, "", BindingValueUnits.Numeric);
            Values.Add(_knobsAndToggles);
            Triggers.Add(_knobsAndToggles);
        }

        protected override ExportDataElement[] DefaultDataElements => DataElementsTemplate;

        public override void ProcessNetworkData(string id, string value)
        {
            if ("2266".Equals(id))
            {
                string[] parts = Tokenizer.TokenizeAtLeast(value, 5, ';');
                if (parts == null) { return; }

                var tacanXY = ClampedParse(parts[0], 1d);
                var tacanMode = ClampedParse(parts[1], 10d);
                var tacanVolumeKnob = ConvertKnobVolume(parts[2]);
                var navModeKnob = ConvertKnobVolume(parts[3]);
                StringBuilder buf = new StringBuilder();
                buf.Append(tacanXY.ToString("D2"));
                buf.Append(tacanMode.ToString("D2"));
                buf.Append(tacanVolumeKnob.ToString("D2"));
                buf.Append(navModeKnob.ToString("D2"));

                Console.WriteLine(buf.ToString());

                _knobsAndToggles.SetValue(new BindingValue(buf.ToString()), false);
            }
        }

        private int ClampedParse(string value, double scale)
        {
            if (!double.TryParse(value, NumberStyles.Float, CultureInfo.InvariantCulture.NumberFormat,
                out double scaledValue))
            {
                return Convert.ToInt32(scaledValue);
            }

            if (scaledValue < 1.0d)
            {
                scaledValue = Math.Truncate(scaledValue * 10d) * scale;
            }
            else
            {
                scaledValue = 0d;
            }
            return Convert.ToInt32(scaledValue);
        }

        private int ConvertKnobVolume(string value)
        {
            if (!double.TryParse(value, NumberStyles.Float, CultureInfo.InvariantCulture.NumberFormat,
                out double scaledValue))
            {
                return 0;
            }
            return Convert.ToInt32(calib35Step.Interpolate(scaledValue));
        }

        public override void Reset()
        {
            _knobsAndToggles.SetValue(BindingValue.Empty, true);
        }
    }
}
