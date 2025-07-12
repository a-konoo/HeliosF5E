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

    public class LightKnobs : DCSFunction
    {

        private HeliosValue _lightArray;
        private static StringBuilder flags = new StringBuilder(10);
        private static CalibrationPointCollectionDouble calibLightKnob =
            new CalibrationPointCollectionDouble(0.0d, 1.0d, 1d, 20d)
            {
                new CalibrationPointDouble(0.05d, 1d),
                new CalibrationPointDouble(0.1d,  2d),
                new CalibrationPointDouble(0.15d, 3d),
                new CalibrationPointDouble(0.2d,  4d),
                new CalibrationPointDouble(0.25d, 5d),
                new CalibrationPointDouble(0.3d,  6d),
                new CalibrationPointDouble(0.35d, 7d),
                new CalibrationPointDouble(0.4d,  8d),
                new CalibrationPointDouble(0.45d, 9d),
                new CalibrationPointDouble(0.5d,  10d),
                new CalibrationPointDouble(0.55d, 11d),
                new CalibrationPointDouble(0.6d,  12d),
                new CalibrationPointDouble(0.65d, 13d),
                new CalibrationPointDouble(0.7d,  14d),
                new CalibrationPointDouble(0.75d, 15d),
                new CalibrationPointDouble(0.8d,  16d),
                new CalibrationPointDouble(0.85d, 17d),
                new CalibrationPointDouble(0.9d,  18d),
                new CalibrationPointDouble(0.95d, 19d),
                new CalibrationPointDouble(1.0d,  20d)
            };

        private static readonly ExportDataElement[] DataElementsTemplate = {
            new DCSDataElement("2556", null, false)
        };

        public LightKnobs(BaseUDPInterface sourceInterface)
            : base(sourceInterface,
                   "Internal Lights", "Internal LightsArray", "current Light Knob position array")
        {
            DoBuild();
        }

        // deserialization constructor
        public LightKnobs(BaseUDPInterface sourceInterface, System.Runtime.Serialization.StreamingContext context)
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
            _lightArray = new HeliosValue(SourceInterface, BindingValue.Empty, SerializedDeviceName, SerializedFunctionName,
                SerializedDescription, "Int/Ext Lights Array", BindingValueUnits.Text);
            Values.Add(_lightArray);
            Triggers.Add(_lightArray);
        }

        protected override ExportDataElement[] DefaultDataElements => DataElementsTemplate;

        public override void ProcessNetworkData(string id, string value)
        {
            if (id.Equals("2556"))
            {
                flags.Clear();

                string[] parts = Tokenizer.TokenizeAtLeast(value, 10, ';');
                var flood = ConvertRawValueToPosition(parts[0]).ToString("D2");
                var flight = ConvertRawValueToPosition(parts[1]).ToString("D2");
                var engine = ConvertRawValueToPosition(parts[2]).ToString("D2");
                var arm = ConvertRawValueToPosition(parts[3]).ToString("D2");
                var nav = ConvertRawValueToPosition(parts[4]).ToString("D2");
                var formation = ConvertRawValueToPosition(parts[5]).ToString("D2");
                flags.Append(flood).Append(flight).Append(engine)
                    .Append(arm).Append(nav).Append(formation);

                _lightArray.SetValue(new BindingValue(flags.ToString()), false);
            }
        }


        private int ConvertRawValueToPosition(string value)
        {
            if (!double.TryParse(value, NumberStyles.Float, CultureInfo.InvariantCulture.NumberFormat,
                out double scaledValue))
            {
                return 0;
            }



            return Convert.ToInt32(calibLightKnob.Interpolate(scaledValue));
        }

        public override void Reset()
        {
            _lightArray.SetValue(BindingValue.Empty, true);
        }
    }
}
