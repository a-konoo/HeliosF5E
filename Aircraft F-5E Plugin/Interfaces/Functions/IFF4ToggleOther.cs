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

    public class IFF4ToggleOther : DCSFunction
    {

        private HeliosValue _iffToggles;
        private static StringBuilder flags = new StringBuilder(10);

        private static readonly ExportDataElement[] DataElementsTemplate = {
            new DCSDataElement("2555", null, false)
        };

        public IFF4ToggleOther(BaseUDPInterface sourceInterface)
            : base(sourceInterface,
                   "IFF", "IFF Toggle Other Array", "current IFF Toggle position array")
        {
            DoBuild();
        }

        // deserialization constructor
        public IFF4ToggleOther(BaseUDPInterface sourceInterface, System.Runtime.Serialization.StreamingContext context)
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
            _iffToggles = new HeliosValue(SourceInterface, BindingValue.Empty, SerializedDeviceName, SerializedFunctionName,
                SerializedDescription, "IFF Code Toggle array", BindingValueUnits.Text);
            Values.Add(_iffToggles);
            Triggers.Add(_iffToggles);
        }

        protected override ExportDataElement[] DefaultDataElements => DataElementsTemplate;

        public override void ProcessNetworkData(string id, string value)
        {
            if (id.Equals("2555"))
            {
                flags.Clear();

                string[] parts = Tokenizer.TokenizeAtLeast(value, 10, ';');
                var tgl3MonCntr = ThreeWayToggleParse(parts[0]);
                var tgl3IDENT = ThreeWayToggleParse(parts[1]);
                var tgl2Cntr = TwoWayToggleParse(parts[2]);
                
                flags.Append(tgl3MonCntr).Append(tgl3IDENT).Append(tgl2Cntr);

                _iffToggles.SetValue(new BindingValue(flags.ToString()), false);
            }
        }

        private int TwoWayToggleParse(string value)
        {
            if (double.TryParse(value, NumberStyles.Float, CultureInfo.InvariantCulture.NumberFormat,
                out double scaledValue)) {
                int n = Convert.ToInt32(scaledValue);

                return 2 - n;
            }
            return 1; 
        }


        private int ThreeWayToggleParse(string value)
        {
            if (!double.TryParse(value, NumberStyles.Float, CultureInfo.InvariantCulture.NumberFormat,
                out double scaledValue))
            {
                return 2;
            }
            int converted = Convert.ToInt32(Math.Floor(scaledValue * 1d));
            switch (converted)
            {
                case 1:
                    return 1;
                case 0:
                    return 2;
                case -1:
                    return 3;
                default:
                    return 2;
            }
        }

        public override void Reset()
        {
            _iffToggles.SetValue(BindingValue.Empty, true);
        }
    }
}
