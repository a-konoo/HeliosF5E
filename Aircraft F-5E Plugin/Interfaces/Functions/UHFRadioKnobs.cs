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


    // UHFRadioFrequency Knobs values


    public class UHFRadioKnobs : DCSFunction
    {

        private static readonly ExportDataElement[] DataElementsTemplate = {
            new DCSDataElement("2327", null, false) };
        private static int prev0025Pos = 0;

        private HeliosValue _knobsAndToggles;

        public UHFRadioKnobs(BaseUDPInterface sourceInterface)
            : base(sourceInterface,
                   "UHF Radio", "Radio KnobsArray", "Currently tuned UHF Radio Knobs."
                  )
        {
            DoBuild();
        }
        // for preset knobs(20step convert)
        private static readonly CalibrationPointCollectionDouble calib20Step =
        new CalibrationPointCollectionDouble(0, 0, 1d, 20d)
        {
                new CalibrationPointDouble(0.0d,  0d),
                new CalibrationPointDouble(0.05d, 1d),
                new CalibrationPointDouble(0.1d,  2d),
                new CalibrationPointDouble(0.15d, 3d),
                new CalibrationPointDouble(0.2d,  4d),
                new CalibrationPointDouble(0.25d, 5d),
                new CalibrationPointDouble(0.3d,  6d),
                new CalibrationPointDouble(0.35d, 7d),
                new CalibrationPointDouble(0.4d,  8d),
                new CalibrationPointDouble(0.45d, 9d),
                new CalibrationPointDouble(0.5d, 10d),
                new CalibrationPointDouble(0.55d,11d),
                new CalibrationPointDouble(0.6d, 12d),
                new CalibrationPointDouble(0.65d,13d),
                new CalibrationPointDouble(0.7d, 14d),
                new CalibrationPointDouble(0.75d,15d),
                new CalibrationPointDouble(0.8d, 16d),
                new CalibrationPointDouble(0.85d,17d),
                new CalibrationPointDouble(0.9d, 18d),
                new CalibrationPointDouble(0.95d,19d),
                new CalibrationPointDouble(1.0d, 20d)
        };
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
        public UHFRadioKnobs(BaseUDPInterface sourceInterface, System.Runtime.Serialization.StreamingContext context)
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
            if ("2327".Equals(id))
            {
                string[] parts = Tokenizer.TokenizeAtLeast(value, 10, ';');
                var knobs100Mhz = ConvertKnobPosFour(parts[0]);
                var knobs10Mhz = ConvertKnobPosTen(parts[1]);
                var knobs1Mhz = ConvertKnobPosTen(parts[2]);
                var knobs01Mhz = ConvertKnobPosTen(parts[3]);
                var knobs0025Mhz = ConvertKnobPosFour0025(parts[4]);
                var knobPreset = ConvertKnobPreset(parts[5]);
                var knobVolume = ConvertKnobVolume(parts[6]);
                var knobFuncSelector = ConvertKnobSimple(parts[7]);
                var knobFreqSelector = ConvertKnobSimple(parts[8]);
                var squelchToggle = SquelchToggleConvert(parts[9]);

                StringBuilder buf = new StringBuilder();
                buf.Append(knobs100Mhz.ToString("D2"));
                buf.Append(knobs10Mhz.ToString("D2"));
                buf.Append(knobs1Mhz.ToString("D2"));
                buf.Append(knobs01Mhz.ToString("D2"));
                buf.Append(knobs0025Mhz.ToString("D2"));
                buf.Append(knobPreset.ToString("D2"));
                buf.Append(knobVolume.ToString("D2"));
                buf.Append(knobFuncSelector.ToString("D2"));
                buf.Append(knobFreqSelector.ToString("D2"));
                buf.Append(squelchToggle.ToString("D2"));
                
                _knobsAndToggles.SetValue(new BindingValue(buf.ToString()), false);
            }
        }

        private int ConvertKnobPosFour(string value)
        {
            if (!double.TryParse(value, NumberStyles.Float, CultureInfo.InvariantCulture.NumberFormat,
                out double scaledValue))
            {
                return 0;
            }

            if (scaledValue < 0.1d)
            {
                return 3;
            }
            if (0.2 > scaledValue && scaledValue >= 0.1d)
            {
                return 2;
            }

            if (0.3 > scaledValue && scaledValue >= 0.2d)
            {
                return 1;
            }

            return 0;
        }

        private int ConvertKnobPosFour0025(string value)
        {
            if (!double.TryParse(value, NumberStyles.Float, CultureInfo.InvariantCulture.NumberFormat,
                out double scaledValue))
            {
                return 0;
            }
            if (scaledValue == 1)
            {
                prev0025Pos = 0;
            }

            if (scaledValue == 0)
            {
                prev0025Pos = 3;
            }

            if (scaledValue == 0.25d)
            {
                prev0025Pos = 2;
            }

            if (scaledValue == 0.75d)
            {
                prev0025Pos = 1;
            }
            return prev0025Pos;
        }


        private int ConvertKnobPosTen(string value)
        {
            if (!double.TryParse(value, NumberStyles.Float, CultureInfo.InvariantCulture.NumberFormat,
                out double scaledValue))
            {
                return 0;
            }

            if (scaledValue >= 1.0d)
            {
                return 0;
            }
            if (1.0 > scaledValue && scaledValue >= 0.9)
            {
                return 1;
            }
            if (0.9 > scaledValue && scaledValue >= 0.8)
            {
                return 2;
            }
            if (0.8 > scaledValue && scaledValue >= 0.7)
            {
                return 3;
            }
            if (0.7 > scaledValue && scaledValue >= 0.6)
            {
                return 4;
            }
            if (0.6 > scaledValue && scaledValue >= 0.5)
            {
                return 5;
            }
            if (0.5 > scaledValue && scaledValue >= 0.4)
            {
                return 6;
            }
            if (0.4 > scaledValue && scaledValue >= 0.3)
            {
                return 7;
            }
            if (0.3 > scaledValue && scaledValue >= 0.2)
            {
                return 8;
            }
            if (0.2 > scaledValue && scaledValue >= 0.1)
            {
                return 9;
            }
            return 9;
        }
        private int ConvertKnobPreset(string value)
        {
            if (!double.TryParse(value, NumberStyles.Float, CultureInfo.InvariantCulture.NumberFormat,
                out double scaledValue))
            {
                return 0;
            }
            return Convert.ToInt32(calib20Step.Interpolate(scaledValue));
        }
        private int SquelchToggleConvert(string value)
        {
            if (!double.TryParse(value, NumberStyles.Float, CultureInfo.InvariantCulture.NumberFormat,
                out double scaledValue))
            {
                return 0;
            }
            if (scaledValue == 0) { return 0; }
            return 1;
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

        private int ConvertKnobSimple(string value)
        {
            if (!double.TryParse(value, NumberStyles.Float, CultureInfo.InvariantCulture.NumberFormat,
                out double scaledValue))
            {
                return 0;
            }
            return Convert.ToInt32(calibSimple.Interpolate(scaledValue));
        }
        

        public override void Reset()
        {
            _knobsAndToggles.SetValue(BindingValue.Empty, true);
        }
    }
}
