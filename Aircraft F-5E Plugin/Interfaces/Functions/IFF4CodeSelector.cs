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

    public class IFF4CodeSelector : DCSFunction
    {

        private HeliosValue _codeSelector;
        private static StringBuilder flags = new StringBuilder(10);

        private static readonly ExportDataElement[] DataElementsTemplate = {
            new DCSDataElement("2553", null, false)
        };

        public IFF4CodeSelector(BaseUDPInterface sourceInterface)
            : base(sourceInterface,
                   "IFF", "IFF CodeSelectorArray", "current IFF CodeSelector status")
        {
            DoBuild();
        }

        // deserialization constructor
        public IFF4CodeSelector(BaseUDPInterface sourceInterface, System.Runtime.Serialization.StreamingContext context)
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
            _codeSelector = new HeliosValue(SourceInterface, BindingValue.Empty, SerializedDeviceName, SerializedFunctionName,
                SerializedDescription, "IFF Code Selector", BindingValueUnits.Text);
            Values.Add(_codeSelector);
            Triggers.Add(_codeSelector);
        }

        protected override ExportDataElement[] DefaultDataElements => DataElementsTemplate;

        public override void ProcessNetworkData(string id, string value)
        {
            if (id.Equals("2553"))
            {
                flags.Clear();

                string[] parts = Tokenizer.TokenizeAtLeast(value, 10, ';');
                var _iffSelector1 = ParseToggleValue(parts[0]);
                var _iffSelector2 = CodeSelectorParse(parts[1]);
                var _iffSelectorPull1 = ParseFlagValue(parts[2]);                
                var _iffSelectorPull2 = ParseFlagValue(parts[3]);

                flags.Append(_iffSelector1).Append(_iffSelector2).Append(_iffSelectorPull1).Append(_iffSelectorPull2);

                _codeSelector.SetValue(new BindingValue(flags.ToString()), false);
            }
        }

        private int ParseToggleValue(string value)
        {
            if (double.TryParse(value, NumberStyles.Float, CultureInfo.InvariantCulture.NumberFormat,
                out double scaledValue)) {
                int n = Convert.ToInt32(scaledValue * 10);
                return n;
            }
            return 0; 
        }

        private int CodeSelectorParse(string value)
        {
            if (!double.TryParse(value, NumberStyles.Float, CultureInfo.InvariantCulture.NumberFormat,
                out double scaledValue))
            {
                return 2;
            }
            int converted = Convert.ToInt32(Math.Floor(scaledValue * 10d));
            switch (converted)
            {
                case 4:
                    return 0;
                case 3:
                    return 1;
                case 2:
                    return 2;
                case 1:
                    return 3;
                case 0:
                    return 4;
                default:
                    return 2;
            }
        }



        private int ParseFlagValue(string value)
        {
            if (double.TryParse(value, NumberStyles.Float, CultureInfo.InvariantCulture.NumberFormat,
                out double scaledValue))
            {
                return scaledValue > 0.5 ? 1 : 0;
            }
            return 0;
        }

        public override void Reset()
        {
            _codeSelector.SetValue(BindingValue.Empty, true);
        }
    }
}
