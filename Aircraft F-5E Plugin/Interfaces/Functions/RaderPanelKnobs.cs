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

    public class RaderPanelKnobs : DCSFunction
    {

        private HeliosValue _raderPanelKnobs;
        private static StringBuilder flags = new StringBuilder(20);

        private static CalibrationPointCollectionDouble calib35Step =
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

        private static CalibrationPointCollectionDouble calib53Step =
            new CalibrationPointCollectionDouble(-1, 0, 1d, 53d)
            {
                new CalibrationPointDouble(-1.0d,   0d),
                new CalibrationPointDouble(-0.962d, 1d),
                new CalibrationPointDouble(-0.926d, 2d),
                new CalibrationPointDouble(-0.888d, 3d),
                new CalibrationPointDouble(-0.851d, 4d),
                new CalibrationPointDouble(-0.814d, 5d),
                new CalibrationPointDouble(-0.777d, 6d),
                new CalibrationPointDouble(-0.74d,  7d),
                new CalibrationPointDouble(-0.703d, 8d),
                new CalibrationPointDouble(-0.666d, 9d),
                new CalibrationPointDouble(-0.629d, 10d),
                new CalibrationPointDouble(-0.593d, 11d),
                new CalibrationPointDouble(-0.555d, 12d),
                new CalibrationPointDouble(-0.518d, 13d),
                new CalibrationPointDouble(-0.481d, 14d),
                new CalibrationPointDouble(-0.444d, 15d),
                new CalibrationPointDouble(-0.407d, 16d),
                new CalibrationPointDouble(-0.37d,  17d),
                new CalibrationPointDouble(-0.333d, 18d),
                new CalibrationPointDouble(-0.296d, 19d),
                new CalibrationPointDouble(-0.259d, 20d),
                new CalibrationPointDouble(-0.222d, 21d),
                new CalibrationPointDouble(-0.185d, 22d),
                new CalibrationPointDouble(-0.148d, 23d),
                new CalibrationPointDouble(-0.111d, 24d),
                new CalibrationPointDouble(-0.074d, 25d),
                new CalibrationPointDouble(-0.037d, 26d),
                new CalibrationPointDouble(0.0d,    27d),
                new CalibrationPointDouble(0.037d,  28d),
                new CalibrationPointDouble(0.74d,   29d),
                new CalibrationPointDouble(0.111d,  30d),
                new CalibrationPointDouble(0.148d,  31d),
                new CalibrationPointDouble(0.185d,  32d),
                new CalibrationPointDouble(0.222d,  33d),
                new CalibrationPointDouble(0.259d,  34d),
                new CalibrationPointDouble(0.296d,  35d),
                new CalibrationPointDouble(0.333d,  36d),
                new CalibrationPointDouble(0.370d,  37d),
                new CalibrationPointDouble(0.407d,  38d),
                new CalibrationPointDouble(0.444d,  39d),
                new CalibrationPointDouble(0.481d,  40d),
                new CalibrationPointDouble(0.518d,  41d),
                new CalibrationPointDouble(0.555d,  42d),
                new CalibrationPointDouble(0.592d,  43d),
                new CalibrationPointDouble(0.629d,  44d),
                new CalibrationPointDouble(0.666d,  45d),
                new CalibrationPointDouble(0.703d,  46d),
                new CalibrationPointDouble(0.740d,  47d),
                new CalibrationPointDouble(0.777d,  48d),
                new CalibrationPointDouble(0.814d,  49d),
                new CalibrationPointDouble(0.851d,  50d),
                new CalibrationPointDouble(0.888d,  51d),
                new CalibrationPointDouble(0.925d,  52d),
                new CalibrationPointDouble(1.0d,    53d)
            };

        private static readonly ExportDataElement[] DataElementsTemplate = {
            new DCSDataElement("2315", null, false)
        };


        public RaderPanelKnobs(BaseUDPInterface sourceInterface)
            : base(sourceInterface,
                   "RaderPanelKnobs", "RaderPanelKnobs PositionArray", "current RaderPanels Knobs position")
        {
            DoBuild();
        }

        // deserialization constructor
        public RaderPanelKnobs(BaseUDPInterface sourceInterface, System.Runtime.Serialization.StreamingContext context)
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
            _raderPanelKnobs = new HeliosValue(SourceInterface, BindingValue.Empty, SerializedDeviceName, SerializedFunctionName,
                SerializedDescription, "Rader Panel Knobs position Array", BindingValueUnits.Text);
            Values.Add(_raderPanelKnobs);
            Triggers.Add(_raderPanelKnobs);
        }

        protected override ExportDataElement[] DefaultDataElements => DataElementsTemplate;

        public override void ProcessNetworkData(string id, string value)
        {
            if (id.Equals("2315"))
            {
                flags.Clear();
                string[] parts = Tokenizer.TokenizeAtLeast(value, 7, ';');
                var raderRange = ClampedParse(parts[0], 1d).ToString("D2");
                var raderMode  = ClampedParse(parts[1], 1d).ToString("D2");
                var raderScale = Parse35StepKnob(parts[2]).ToString("D2");
                var raderBright = Parse35StepKnob(parts[3]).ToString("D2");
                var raderPersist = Parse35StepKnob(parts[4]).ToString("D2");
                var raderVideo = Parse35StepKnob(parts[5]).ToString("D2");
                var raderCursor = Parse35StepKnob(parts[6]).ToString("D2");
                var raderPitch = Parse53StepKnob(parts[7]).ToString("D2");
                flags.Append(raderRange).Append(raderMode).Append(raderScale)
                    .Append(raderBright).Append(raderPersist).Append(raderVideo)
                    .Append(raderCursor).Append(raderPitch);

                _raderPanelKnobs.SetValue(new BindingValue(flags.ToString()), false);
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

        private int Parse35StepKnob(string value)
        {
            if (!double.TryParse(value, NumberStyles.Float, CultureInfo.InvariantCulture.NumberFormat,
                out double scaledValue)) {
                return 0;
            }

            return Convert.ToInt32(calib35Step.Interpolate(scaledValue));
        }

        private int Parse53StepKnob(string value)
        {
            if (!double.TryParse(value, NumberStyles.Float, CultureInfo.InvariantCulture.NumberFormat,
                out double scaledValue))
            {
                return 0;
            }

            return Convert.ToInt32(calib53Step.Interpolate(scaledValue));
        }

        public override void Reset()
        {
            _raderPanelKnobs.SetValue(BindingValue.Empty, true);
        }
    }
}
