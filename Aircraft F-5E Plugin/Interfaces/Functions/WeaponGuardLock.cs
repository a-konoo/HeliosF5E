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


    // WeaponGuardLock values on packet is formatted for Guard Action 

    public class WeaponGuardLock : DCSFunctionPair
    {
        private static readonly ExportDataElement[] DataElementsTemplate = { new DCSDataElement("2342", null, false) };

        private static double armSwitchReadonly;
        private HeliosValue _adjustGuardOnOff;
        public WeaponGuardLock(BaseUDPInterface sourceInterface)
            : base(sourceInterface,
                   "Weapons Control", "Master Arm Safe Guard correct inconsistent", "current Guard Lock Status.",
                   "Weapons Control", "Arm/Norm/Cam only Switch for Reflect", "current arm/off/cam switch status."
                  )
        {
            DoBuild();
        }

        // deserialization constructor
        public WeaponGuardLock(BaseUDPInterface sourceInterface, System.Runtime.Serialization.StreamingContext context)
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
            _adjustGuardOnOff = new HeliosValue(SourceInterface, BindingValue.Empty, SerializedDeviceName, SerializedFunctionName,
                SerializedDescription, "2=Open,1=Close", BindingValueUnits.Numeric);
            Values.Add(_adjustGuardOnOff);
            Triggers.Add(_adjustGuardOnOff);
        }

        protected override ExportDataElement[] DefaultDataElements => DataElementsTemplate;

        public static bool CanGuardClose()
        {
            return armSwitchReadonly == 0;
        }
        public override void ProcessNetworkData(string id, string value)
        {
            if (id.Equals("2342"))
            {
                string[] parts = Tokenizer.TokenizeAtLeast(value, 3, ';');
                // guardStatus
                double guardStatus = 0d;
                Double.TryParse(parts[0], out guardStatus);

                // arm/off/cam switch
                double armSwitch = 0d;
                Double.TryParse(parts[1], out armSwitch);
                armSwitchReadonly = armSwitch;

                // ClosableValue
                double ClosableValue = 0d;
                Double.TryParse(parts[2], out ClosableValue);
                if (guardStatus == 0 && armSwitch != ClosableValue)
                {
                    guardStatus = 1d;
                }
                else
                {
                    guardStatus = 2d;
                }
                _adjustGuardOnOff.SetValue(new BindingValue(guardStatus), false);

            }
        }

        public override void Reset()
        {
            _adjustGuardOnOff.SetValue(BindingValue.Empty, true);
        }
    }
}
