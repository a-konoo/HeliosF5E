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


    // Weapon Panel Levers and Toggles

    public class WeaponPanelSelectors : DCSFunction
    {

        private HeliosValue _weaponLeverFlags;
        private static StringBuilder flags = new StringBuilder(10);
        private static StringBuilder flagLamps = new StringBuilder(10);

        private static readonly ExportDataElement[] DataElementsTemplate = {
            new DCSDataElement("3346", null, false)
        };

        public WeaponPanelSelectors(BaseUDPInterface sourceInterface)
            : base(sourceInterface,
                   "Weapons Control", "Weapon Lever Selectors", "current weapon selectors status.")
        {
            DoBuild();
        }

        // deserialization constructor
        public WeaponPanelSelectors(BaseUDPInterface sourceInterface, System.Runtime.Serialization.StreamingContext context)
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
            _weaponLeverFlags = new HeliosValue(SourceInterface, BindingValue.Empty, SerializedDeviceName, SerializedFunctionName,
                SerializedDescription, "Weapon Center Lever flags", BindingValueUnits.Text);
            Values.Add(_weaponLeverFlags);
            Triggers.Add(_weaponLeverFlags);

        }

        protected override ExportDataElement[] DefaultDataElements => DataElementsTemplate;

        public override void ProcessNetworkData(string id, string value)
        {
            if (id.Equals("3346"))
            {
                flags.Clear();
                flagLamps.Clear();

                string[] parts = Tokenizer.TokenizeAtLeast(value, 3, ';');
                var _bombIntervalTgl = ThreeWayToggleParse(parts[0]);
                var _bombSafeTgl = BombSafeFourWayToggleParse(parts[1]);      //0.2->1 0.4->2 0.6->3 0.8->4
                var _extStoreTgl = ExtStoreFourWayToggleParse(parts[2]);      //0.0->1 0.1->2 0.2->3 0.3->4

                flags.Append(_bombIntervalTgl).Append(_bombSafeTgl).Append(_extStoreTgl);

                _weaponLeverFlags.SetValue(new BindingValue(flags.ToString()), false);
            }
        }

        private int BombSafeFourWayToggleParse(string value)
        {
            if (!double.TryParse(value, NumberStyles.Float, CultureInfo.InvariantCulture.NumberFormat,
                out double scaledValue))
            {
                return 1;
            }
            int floornum = Convert.ToInt32(Math.Floor(scaledValue * 10d));
            switch (floornum)
            {
                case 2:
                    return 0;
                case 4:
                    return 1;
                case 6:
                    return 2;
                case 8:
                    return 3;
                default:
                    return 1;
            }
        }

        private int ExtStoreFourWayToggleParse(string value)
        {
            if (!double.TryParse(value, NumberStyles.Float, CultureInfo.InvariantCulture.NumberFormat,
                out double scaledValue))
            {
                return 3;
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
                    return 3;
            }
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
            _weaponLeverFlags.SetValue(BindingValue.Empty, true);
        }
    }
}
