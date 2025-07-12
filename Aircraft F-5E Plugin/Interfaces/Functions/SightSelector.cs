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

    public class SightSelector : DCSFunction
    {
        private static readonly ExportDataElement[] DataElementsTemplate = {
            new DCSDataElement("2040", null, false) };

        private HeliosValue _knobsArray;
        public SightSelector(BaseUDPInterface sourceInterface)
            : base(sourceInterface, "Sight", "SightKnobsArray", "Currently Sight Knobs Array."                  )
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
        
        private static readonly CalibrationPointCollectionDouble calip5Step =
        new CalibrationPointCollectionDouble(0, 0, 1d, 3d)
        {
            new CalibrationPointDouble(0.0d, 0d),
            new CalibrationPointDouble(0.1d, 1d),
            new CalibrationPointDouble(0.2d, 2d),
            new CalibrationPointDouble(0.3d, 3d),
            new CalibrationPointDouble(0.4d, 4d)
        };

        // deserialization constructor
        public SightSelector(BaseUDPInterface sourceInterface, System.Runtime.Serialization.StreamingContext context)
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
            _knobsArray = new HeliosValue(SourceInterface, BindingValue.Empty, SerializedDeviceName, SerializedFunctionName,
                SerializedDescription, "", BindingValueUnits.Numeric);
            Values.Add(_knobsArray);
            Triggers.Add(_knobsArray);
        }

        protected override ExportDataElement[] DefaultDataElements => DataElementsTemplate;

        public override void ProcessNetworkData(string id, string value)
        {
            if(id.Equals("2040"))
            {
                string[] parts = Tokenizer.TokenizeAtLeast(value, 3, ';');
                
                if (parts.Length != 4) { return; }

                var selector = ConvertSelectorValue(parts[0]);
                var intencity = Convert35StepValue(parts[1]);
                var bitToggle = ConvertBitLever(parts[2]);
                var lightToggle = ConvertLightToggle(parts[3]);

                StringBuilder buf = new StringBuilder();
                buf.Append(selector.ToString("D2"));
                buf.Append(intencity.ToString("D2"));
                buf.Append(bitToggle.ToString("D2"));
                buf.Append(lightToggle.ToString("D2"));
                _knobsArray.SetValue(new BindingValue(buf.ToString()), false);
            }

        }

        private int ConvertSelectorValue(string value)
        {
            if (!double.TryParse(value, NumberStyles.Float, CultureInfo.InvariantCulture.NumberFormat,
                out double scaledValue))
            {
                return 0;
            }
            return Convert.ToInt32(calip5Step.Interpolate(scaledValue));
        }
        


        private int Convert35StepValue(string value)
        {
            if (!double.TryParse(value, NumberStyles.Float, CultureInfo.InvariantCulture.NumberFormat,
                out double scaledValue))
            {
                return 0;
            }
            return Convert.ToInt32(calib35Step.Interpolate(scaledValue));
        }


        private int ConvertBitLever(string value)
        {
            if (!double.TryParse(value, NumberStyles.Float, CultureInfo.InvariantCulture.NumberFormat,
                out double scaledValue))
            {
                return 0;
            }
            if (scaledValue == -1.0)
            {
                return 0;
            }
            if (scaledValue == 0.0)
            {
                return 1;
            }
            return 2;
        }

        private int ConvertLightToggle(string value)
        {
            if (!double.TryParse(value, NumberStyles.Float, CultureInfo.InvariantCulture.NumberFormat,
                out double scaledValue))
            {
                return 0;
            }
            return scaledValue == 1 ? 1 : 0;
        }

        public override void Reset()
        {
            _knobsArray.SetValue(BindingValue.Empty, true);
        }
    }
}
