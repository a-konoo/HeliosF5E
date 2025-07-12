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

    public class CMDSPanelToggle : DCSFunction
    {

        private HeliosValue _cmdsToggles;
        private static StringBuilder flags = new StringBuilder(10);

        private static readonly ExportDataElement[] DataElementsTemplate = {
            new DCSDataElement("3406", null, false)
        };


        // simple convert
        private static readonly CalibrationPointCollectionDouble calibSimple =
        new CalibrationPointCollectionDouble(0, 0, 0d, 10d)
        {
            new CalibrationPointDouble(-1.0d, 0d),
            new CalibrationPointDouble(-0.8d, 1d),
            new CalibrationPointDouble(-0.6d, 2d),
            new CalibrationPointDouble(-0.4d, 3d),
            new CalibrationPointDouble(-0.2d, 4d),
            new CalibrationPointDouble( 0d,   5d),
            new CalibrationPointDouble(0.2d,  6d),
            new CalibrationPointDouble(0.4d,  7d),
            new CalibrationPointDouble(0.6d,  8d),
            new CalibrationPointDouble(0.8d,  9d),
            new CalibrationPointDouble(1.0d, 10d),
        };

        public CMDSPanelToggle(BaseUDPInterface sourceInterface)
            : base(sourceInterface,
                   "CMDS", "CMDS ToggleArray", "current CMDS Toggle position array")
        {
            DoBuild();
        }

        // deserialization constructor
        public CMDSPanelToggle(BaseUDPInterface sourceInterface, System.Runtime.Serialization.StreamingContext context)
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
            _cmdsToggles = new HeliosValue(SourceInterface, BindingValue.Empty, SerializedDeviceName, SerializedFunctionName,
                SerializedDescription, "CMDS Toggle array", BindingValueUnits.Text);
            Values.Add(_cmdsToggles);
            Triggers.Add(_cmdsToggles);
        }

        protected override ExportDataElement[] DefaultDataElements => DataElementsTemplate;

        public override void ProcessNetworkData(string id, string value)
        {
            if (id.Equals("3406"))
            {
                flags.Clear();

                string[] parts = Tokenizer.TokenizeAtLeast(value, 3, ';');
                var chaff = ToggleParse(parts[0]).ToString("D2");
                var flare = ToggleParse(parts[1]).ToString("D2");
                var rudderTrim = Parse10StepKnob(parts[2]).ToString("D2");
                if (rudderTrim == "")
                {
                    // if empty,default trim center
                    rudderTrim = "05";
                }
                flags.Append(chaff).Append(flare).Append(rudderTrim);

                _cmdsToggles.SetValue(new BindingValue(flags.ToString()), false);
            }
        }


        private int ToggleParse(string value)
        {
            if (!double.TryParse(value, NumberStyles.Float, CultureInfo.InvariantCulture.NumberFormat,
                out double scaledValue))
            {
                return 2;
            }
            int converted = Convert.ToInt32(Math.Floor(scaledValue * 10d));
            switch (converted)
            {
                case 0:
                    return 0;
                case 1:
                    return 1;
                case 2:
                    return 2;
                case 3:
                    return 3;
                default:
                    return 0;
            }
        }

        private int Parse10StepKnob(string value)
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
            _cmdsToggles.SetValue(BindingValue.Empty, true);
        }
    }
}
