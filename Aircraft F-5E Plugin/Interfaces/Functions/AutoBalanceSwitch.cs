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


    // AutoBalanceSwitch and Toggles

    public class AutoBalanceSwitch : DCSFunction
    {

        private HeliosValue _autoBalanceValue;
        private static StringBuilder flags = new StringBuilder(10);
        private static StringBuilder flagLamps = new StringBuilder(10);

        private static readonly ExportDataElement[] DataElementsTemplate = {
            new DCSDataElement("2558", null, false)
        };

        public AutoBalanceSwitch(BaseUDPInterface sourceInterface)
            : base(sourceInterface,
                   "Fuel System", "AutoBalanceLever", "current AutoBalance axis to position.")
        {
            DoBuild();
        }

        // deserialization constructor
        public AutoBalanceSwitch(BaseUDPInterface sourceInterface, System.Runtime.Serialization.StreamingContext context)
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
            _autoBalanceValue = new HeliosValue(SourceInterface, BindingValue.Empty,
                SerializedDeviceName, SerializedFunctionName,
                SerializedDescription, "AutoBalance value", BindingValueUnits.Text);
            Values.Add(_autoBalanceValue);
            Triggers.Add(_autoBalanceValue);

        }

        protected override ExportDataElement[] DefaultDataElements => DataElementsTemplate;

        public override void ProcessNetworkData(string id, string value)
        {
            if (id.Equals("2558"))
            {
                flags.Clear();
                flagLamps.Clear();

                string[] parts = Tokenizer.TokenizeAtLeast(value, 1, ';');
                var _value = SimpleLeverParse(parts[0]);


                _autoBalanceValue.SetValue(new BindingValue(_value.ToString()), false);
            }
        }

        private int SimpleLeverParse(string value)
        {
            if (!double.TryParse(value, NumberStyles.Float, CultureInfo.InvariantCulture.NumberFormat,
                out double scaledValue))
            {
                return 1;
            }

            if (scaledValue < 0) { return 0; }
            if (scaledValue > 0) { return 2; }
            return 1;
        }

        public override void Reset()
        {
            _autoBalanceValue.SetValue(BindingValue.Empty, true);
        }
    }
}
