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

    public class IFF4Wheels : DCSFunction
    {

        private HeliosValue _iffWheels;
        private static StringBuilder flags = new StringBuilder(10);
        private static StringBuilder flagLamps = new StringBuilder(10);

        private static readonly ExportDataElement[] DataElementsTemplate = {
            new DCSDataElement("2552", null, false)
        };

        public IFF4Wheels(BaseUDPInterface sourceInterface)
            : base(sourceInterface,
                   "IFF", "IFF Wheels Array", "current IFF wheels position array")
        {
            DoBuild();
        }

        // deserialization constructor
        public IFF4Wheels(BaseUDPInterface sourceInterface, System.Runtime.Serialization.StreamingContext context)
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
            _iffWheels = new HeliosValue(SourceInterface, BindingValue.Empty, SerializedDeviceName, SerializedFunctionName,
                SerializedDescription, "IFF wheels position number array", BindingValueUnits.Text);
            Values.Add(_iffWheels);
            Triggers.Add(_iffWheels);
        }

        protected override ExportDataElement[] DefaultDataElements => DataElementsTemplate;

        public override void ProcessNetworkData(string id, string value)
        {
            if (id.Equals("2552"))
            {
                flags.Clear();

                string[] parts = Tokenizer.TokenizeAtLeast(value, 10, ';');
                var _iffWheel1 = ParseWheelValue(parts[0]);
                var _iffWheel2 = ParseWheelValue(parts[1]);
                var _iffWheel3 = ParseWheelValue(parts[2]);
                var _iffWheel4 = ParseWheelValue(parts[3]);
                var _iffWheel5 = ParseWheelValue(parts[4]);
                var _iffWheel6 = ParseWheelValue(parts[5]);

                flags.Append(_iffWheel1).Append(_iffWheel2).Append(_iffWheel3).Append(_iffWheel4)
                    .Append(_iffWheel5).Append(_iffWheel6);

                _iffWheels.SetValue(new BindingValue(flags.ToString()), false);
            }
        }

        private int ParseWheelValue(string value)
        {
            if (double.TryParse(value, NumberStyles.Float, CultureInfo.InvariantCulture.NumberFormat,
                out double scaledValue)) {
                int n = Convert.ToInt32(scaledValue * 10);
                return n;
            }
            return 0; 
        }

        public override void Reset()
        {
            _iffWheels.SetValue(BindingValue.Empty, true);
        }
    }
}
