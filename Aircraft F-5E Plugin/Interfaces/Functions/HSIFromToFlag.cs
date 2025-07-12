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


    public class HSIFromToFlag : DCSFunctionPair
    {
        private static readonly ExportDataElement[]
            DataElementsTemplate = { new DCSDataElement("2146", null, true) };

        private HeliosValue _from;
        private HeliosValue _to;

        public HSIFromToFlag(BaseUDPInterface sourceInterface)
            : base(sourceInterface,
                  "HSI", "FromFlag", "HSI From Flag.",
                  "HSI", "ToFlag", "HSI To Flag.")
        {
            DoBuild();
        }

        // deserialization constructor
        public HSIFromToFlag(BaseUDPInterface sourceInterface,
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
            _from = new HeliosValue(SourceInterface, BindingValue.Empty,
                SerializedDeviceName, SerializedFunctionName,
                SerializedDescription, "Flag for HSI From", BindingValueUnits.Boolean);
            Values.Add(_from);
            Triggers.Add(_from);

            _to = new HeliosValue(SourceInterface, BindingValue.Empty,
                SerializedDeviceName2, SerializedFunctionName2,
                SerializedDescription2, "Flag for HSI To", BindingValueUnits.Boolean);
            Values.Add(_to);
            Triggers.Add(_to);
        }

        protected override ExportDataElement[] DefaultDataElements => DataElementsTemplate;

        public override void ProcessNetworkData(string id, string value)
        {
            string[] parts;
            if (id.Equals("2146"))
            {
                parts = Tokenizer.TokenizeAtLeast(value, 1, ';');
                if (ClampedParse(parts[0], 1d) == 0)
                {
                    _from.SetValue(new BindingValue(false), false);
                    _to.SetValue(new BindingValue(false), false);
                    return;
                }
                if (ClampedParse(parts[0], 1d) > 0)
                {
                    _from.SetValue(new BindingValue(true), false);
                    _to.SetValue(new BindingValue(false), false);
                    return;
                }
                _from.SetValue(new BindingValue(false), false);
                _to.SetValue(new BindingValue(true), false);
                return;
            }
        }

        private double ClampedParse(string value, double scale)
        {
            if (!double.TryParse(value, NumberStyles.Float, CultureInfo.InvariantCulture.NumberFormat,
                out double scaledValue))
            {
                return scaledValue;
            }
            return scaledValue;
        }

        public override void Reset()
        {
            _from.SetValue(BindingValue.Empty, true);
            _to.SetValue(BindingValue.Empty, true);
        }
    }
}
