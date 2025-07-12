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
    using System.Text;


    // RWR IC Panel

    public class RWRICPanel : DCSFunction
    {

        private HeliosValue _rwrICPanelFlags;
        private static StringBuilder flags = new StringBuilder(10);

        private static readonly ExportDataElement[] DataElementsTemplate = {
            new DCSDataElement("2551", null, false), // RWR IC Panel Flags
        };
        private int prevModeValue = 0;
        private int prevModeCount = 0;
        private int prevSearchValue = 0;
        private int prevSearchCount = 0;
        private int prevUnknownValue = 0;
        private int prevUnknownCount = 0;
        public RWRICPanel(BaseUDPInterface sourceInterface)
            : base(sourceInterface,
                   "RWR IC", "RWR IC Panel Button Flags", "current RWRIC Panel status.")
        {
            DoBuild();
        }

        // deserialization constructor
        public RWRICPanel(BaseUDPInterface sourceInterface, System.Runtime.Serialization.StreamingContext context)
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
            _rwrICPanelFlags = new HeliosValue(SourceInterface, BindingValue.Empty, SerializedDeviceName, SerializedFunctionName,
                SerializedDescription, "RWR IC Panel Button Flags", BindingValueUnits.Text);
            Values.Add(_rwrICPanelFlags);
            Triggers.Add(_rwrICPanelFlags);
        }

        protected override ExportDataElement[] DefaultDataElements => DataElementsTemplate;

        public override void ProcessNetworkData(string id, string value)
        {
            if (id.Equals("2551"))
            {
                flags.Clear();

                int _rwrModeTmp = 0;
                int _rwrSearchTmp = 0;
                int _rwrSearch = 0;
                int _rwrHandOff = 0;
                int _rwrAlti = 0;
                int _rwrT = 0;
                int _rwrTest = 0;
                int _rwrUnknownTmp = 0;
                int _rwrPower = 0;
                int _rwrActPwr = 0;

                string[] parts = Tokenizer.TokenizeAtLeast(value, 10, ';');
                _rwrModeTmp = Convert.ToInt32(Convert.ToDouble(parts[0]));
                _rwrSearchTmp = Convert.ToInt32(Convert.ToDouble(parts[1]));
                _rwrHandOff = Convert.ToInt32(Convert.ToDouble(parts[2]));
                _rwrAlti = Convert.ToInt32(Convert.ToDouble(parts[3]));
                _rwrT = Convert.ToInt32(Convert.ToDouble(parts[4]));
                _rwrTest = Convert.ToInt32(Convert.ToDouble(parts[5]));
                _rwrUnknownTmp = Convert.ToInt32(Convert.ToDouble(parts[6]));
                _rwrPower = Convert.ToInt32(Convert.ToDouble(parts[7]));
                _ = Convert.ToInt32(Convert.ToDouble(parts[8]));
                _rwrActPwr = Convert.ToInt32(Convert.ToDouble(parts[9]));

                // toggle ?
                // 0->1 & 1->0 = ON
                prevModeCount += prevModeValue == 1 && _rwrModeTmp == 0 ? 1 : 0;
                prevModeValue = _rwrModeTmp;

                prevSearchCount += prevSearchValue == 1 && _rwrSearch == 0 ? 1 : 0;
                if (prevSearchCount % 2 == 1)
                {
                    prevModeCount = 0;
                }
                prevSearchValue = _rwrSearchTmp;

                prevUnknownCount += prevUnknownValue == 1 && _rwrUnknownTmp == 0 ? 1 : 0;
                prevUnknownValue = _rwrUnknownTmp;

                if (_rwrPower > 0 && _rwrTest > 0)
                {
                    prevModeCount = 1;
                    prevSearchCount = 1;
                    prevUnknownCount = 1;
                    _rwrHandOff = 1;
                    _rwrTest = 1;
                }


                flags.Append(_rwrPower == 0 ? 0 : ((prevModeCount % 2) + 1))    // toggle 
                    .Append(_rwrPower == 0 ? 0 : ((prevSearchCount % 2) + 1))
                    .Append(_rwrPower == 0 ? 0 : (_rwrHandOff == 0 ? 1 : 2))    // momentary
                    .Append(_rwrPower == 0 ? 0 : (_rwrAlti == 0 ? 1 : 2))
                    .Append(_rwrPower == 0 ? 0 : (_rwrT == 0 ? 1 : 2))
                    .Append(_rwrPower == 0 ? 0 : (_rwrTest == 0 ? 1 : 2))
                    .Append(_rwrPower == 0 ? 0 : ((prevUnknownCount % 2) + 1))  // toggle
                    .Append(_rwrPower == 0 ? 0 : 1)
                    .Append(0)
                    .Append(_rwrPower == 0 ? 0 : (_rwrActPwr == 0 ? 1 : 2));

                _rwrICPanelFlags.SetValue(new BindingValue(flags.ToString()), false);
            }
        }

        public override void Reset()
        {
            _rwrICPanelFlags.SetValue(BindingValue.Empty, true);
            prevModeValue = 0;
            prevSearchValue = 0;
            prevUnknownCount = 0;

        }
    }
}
