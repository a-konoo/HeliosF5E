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
    using System.Globalization;
    using System.Text;


    // JettisonPanel Levers and Toggles

    public class JettisonPanel : DCSFunction
    {

        private HeliosValue _jettosonValues;
        private static StringBuilder flags = new StringBuilder(2);

        private static readonly ExportDataElement[] DataElementsTemplate = {
            new DCSDataElement("2559", null, false)
        };

        public JettisonPanel(BaseUDPInterface sourceInterface)
            : base(sourceInterface,
                   "Jettison System", "Jettison Values", "current jettison panel values.")
        {
            DoBuild();
        }

        // deserialization constructor
        public JettisonPanel(BaseUDPInterface sourceInterface, 
            System.Runtime.Serialization.StreamingContext context)
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
            _jettosonValues = new HeliosValue(SourceInterface, BindingValue.Empty,
                SerializedDeviceName, SerializedFunctionName,
                SerializedDescription, "Jettison Panel Values", BindingValueUnits.Text);
            Values.Add(_jettosonValues);
            Triggers.Add(_jettosonValues);

        }

        protected override ExportDataElement[] DefaultDataElements => DataElementsTemplate;

        public override void ProcessNetworkData(string id, string value)
        {
            if (id.Equals("2559"))
            {
                flags.Clear();

                string[] parts = Tokenizer.TokenizeAtLeast(value, 2, ';');
                var _jt1 = parseToggleCover(parts[0]);  // Emer Jettison Cover 
                var _jt2 = parseToggleValue(parts[1]);  // Jettison Toggle/ 

                flags.Append(_jt1).Append(_jt2);

                _jettosonValues.SetValue(new BindingValue(flags.ToString()), false);
            }
        }

        private int parseToggleCover(string value)
        {
            if (!int.TryParse(value, NumberStyles.Float, CultureInfo.InvariantCulture.NumberFormat,
                out int scaledValue))
            {
                return 1;
            }

            return scaledValue + 1;
        }

        private int parseToggleValue(string value)
        {
            if (!double.TryParse(value, NumberStyles.Float, CultureInfo.InvariantCulture.NumberFormat,
                out double scaledValue))
            {
                return 1;
            }
            if (scaledValue < 0) { return 0; }
            return 2;
        }

        public override void Reset()
        {
            _jettosonValues.SetValue(BindingValue.Empty, true);
        }
    }
}
