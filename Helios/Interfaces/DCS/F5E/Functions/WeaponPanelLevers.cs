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

    public class WeaponPanelLevers : DCSFunctionPair
    {

        private HeliosValue _weaponLeverFlagsP;
        private HeliosValue _weaponLeverFlagsS;
        private static StringBuilder flags = new StringBuilder(10);
        private static StringBuilder flagLamps = new StringBuilder(10);

        private static readonly ExportDataElement[] DataElementsTemplate = {
            new DCSDataElement("3346", null, false)
        };

        public WeaponPanelLevers(BaseUDPInterface sourceInterface)
            : base(sourceInterface,
                   "Weapons Control", "Weapon Lever Values", "current weapon Lever status.",
                   "Weapons Control", "Weapon Lever Indicator Lamp Flags", "current weapon Lever Lamp status."
                  )
        {
            DoBuild();
        }

        // deserialization constructor
        public WeaponPanelLevers(BaseUDPInterface sourceInterface, System.Runtime.Serialization.StreamingContext context)
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
            _weaponLeverFlagsP = new HeliosValue(SourceInterface, BindingValue.Empty, SerializedDeviceName, SerializedFunctionName,
                SerializedDescription, "Weapon Center Lever falgs pri", BindingValueUnits.Text);
            Values.Add(_weaponLeverFlagsP);
            Triggers.Add(_weaponLeverFlagsP);

            _weaponLeverFlagsS = new HeliosValue(SourceInterface, BindingValue.Empty, SerializedDeviceName, SerializedFunctionName2,
                SerializedDescription2, "Weapon Center Lever falgs sec", BindingValueUnits.Text);
            Values.Add(_weaponLeverFlagsS);
            Triggers.Add(_weaponLeverFlagsS);

        }

        protected override ExportDataElement[] DefaultDataElements => DataElementsTemplate;

        public override void ProcessNetworkData(string id, string value)
        {
            if (id.Equals("3346"))
            {
                flags.Clear();
                flagLamps.Clear();

                int[] _leverLeft3 = { 2, 0 };
                int[] _leverLeft2 = { 2, 0 };
                int[] _leverLeft1 = { 2, 0 };
                int[] _leverCenter = { 2, 0 };
                int[] _leverRight1 = { 2, 0 };
                int[] _leverRight2 = { 2, 0 };
                int[] _leverRight3 = { 2, 0 };
                int[] _bombIntervalTgl = { 1, 0 };
                int[] _bombSafeTgl = { 1, 0 };
                int[] _extStoreTgl = { 1, 0 };

                string[] parts = Tokenizer.TokenizeAtLeast(value, 10, ';');
                _leverLeft3 = SimpleLeverParse(parts[0], 1d);
                _leverLeft2 = SimpleLeverParse(parts[1], 1d);
                _leverLeft1 = SimpleLeverParse(parts[2], 1d);
                _leverCenter = SimpleLeverParse(parts[3], 1d);
                _leverRight1 = SimpleLeverParse(parts[4], 1d);
                _leverRight2 = SimpleLeverParse(parts[5], 1d);
                _leverRight3 = SimpleLeverParse(parts[6], 1d);
                _bombIntervalTgl = ThreeWayToggleParse(parts[7], 1d);
                _bombSafeTgl = BombSafeFourWayToggleParse(parts[8], 1d);      //0.2->1 0.4->2 0.6->3 0.8->4
                _extStoreTgl = ExtStoreFourWayToggleParse(parts[9], 1d);      //0.0->1 0.1->2 0.2->3 0.3->4

                flags.Append(_leverLeft3[0]).Append(_leverLeft2[0]).Append(_leverLeft1[0]).Append(_leverCenter[0])
                    .Append(_leverRight1[0]).Append(_leverRight2[0]).Append(_leverRight3[0]).Append(_bombIntervalTgl[0])
                    .Append(_bombSafeTgl[0]).Append(_extStoreTgl[0]);

                flagLamps.Append(_leverLeft3[1]).Append(_leverLeft2[1]).Append(_leverLeft1[1]).Append(_leverCenter[1])
                     .Append(_leverRight1[1]).Append(_leverRight2[1]).Append(_leverRight3[1]).Append(_bombIntervalTgl[1])
                     .Append(_bombSafeTgl[1]).Append(_extStoreTgl[1]);

                _weaponLeverFlagsP.SetValue(new BindingValue(flags.ToString()), false);
                _weaponLeverFlagsS.SetValue(new BindingValue(flags.ToString()), false);
            }
        }

        private int[] SimpleLeverParse(string value, double scale)
        {
            if (!int.TryParse(value, NumberStyles.Float, CultureInfo.InvariantCulture.NumberFormat,
                out int scaledValue))
            {
                return new int[] { 1, 0 };
            }

            return scaledValue == 0 ? new int[] { 2, 0 } : new int[] { 1, 1 };
        }

        private int[] BombSafeFourWayToggleParse(string value, double scale)
        {
            if (!double.TryParse(value, NumberStyles.Float, CultureInfo.InvariantCulture.NumberFormat,
                out double scaledValue))
            {
                return new int[] { 1, 0 };
            }
            int floornum = Convert.ToInt32(Math.Floor(scaledValue * 10d));
            switch (floornum)
            {
                case 2:
                    return new int[] { 1, 0 };
                case 4:
                    return new int[] { 2, 1 };
                case 6:
                    return new int[] { 3, 2 };
                case 8:
                    return new int[] { 4, 3 };
                default:
                    return new int[] { 1, 0 };
            }
        }

        private int[] ExtStoreFourWayToggleParse(string value, double scale)
        {
            if (!double.TryParse(value, NumberStyles.Float, CultureInfo.InvariantCulture.NumberFormat,
                out double scaledValue))
            {
                return new int[] { 3, 0 };
            }
            int converted = Convert.ToInt32(Math.Floor(scaledValue * 10d));
            switch (converted)
            {
                case 0:
                    return new int[] { 1, 0 };
                case 1:
                    return new int[] { 2, 1 };
                case 2:
                    return new int[] { 3, 2 };
                case 3:
                    return new int[] { 4, 3 };
                default:
                    return new int[] { 3, 0 };
            }
        }

        private int[] ThreeWayToggleParse(string value, double scale)
        {
            if (!double.TryParse(value, NumberStyles.Float, CultureInfo.InvariantCulture.NumberFormat,
                out double scaledValue))
            {
                return new int[] { 2, 0 };
            }
            int converted = Convert.ToInt32(Math.Floor(scaledValue * 1d));
            switch (converted)
            {
                case 1:
                    return new int[] { 1, 1 };
                case 0:
                    return new int[] { 2, 0 };
                case -1:
                    return new int[] { 3, 1 };
                default:
                    return new int[] { 2, 1 };
            }
        }

        public override void Reset()
        {
            _weaponLeverFlagsP.SetValue(BindingValue.Empty, true);
            _weaponLeverFlagsS.SetValue(BindingValue.Empty, true);
        }
    }
}
