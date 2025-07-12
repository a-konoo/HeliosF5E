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


    public class AttitudeIndecator : DCSFunctionPair
    {
        private static readonly ExportDataElement[] DataElementsTemplate = { new DCSDataElement("2081", null, true) };

        private HeliosValue _bank;
        private HeliosValue _pitch;

        public AttitudeIndecator(BaseUDPInterface sourceInterface)
            : base(sourceInterface,
                  "Attitude Indecator", "Bank", "Bank of the aircraft.",
                  "Attitude Indecator", "Pitch", "Pitch of the aircraft.")
        {
            DoBuild();
        }

        // deserialization constructor
        public AttitudeIndecator(BaseUDPInterface sourceInterface, System.Runtime.Serialization.StreamingContext context)
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
            _bank = new HeliosValue(SourceInterface, BindingValue.Empty, SerializedDeviceName, SerializedFunctionName,
                SerializedDescription, "", BindingValueUnits.Degrees);
            Values.Add(_bank);
            Triggers.Add(_bank);

            _pitch = new HeliosValue(SourceInterface, BindingValue.Empty, SerializedDeviceName2, SerializedFunctionName2,
                SerializedDescription2, "", BindingValueUnits.Degrees);
            Values.Add(_pitch);
            Triggers.Add(_pitch);
        }

        protected override ExportDataElement[] DefaultDataElements => DataElementsTemplate;

        public override void ProcessNetworkData(string id, string value)
        {
            string[] parts;
            if (id.Equals("2081"))
            {
                parts = Tokenizer.TokenizeAtLeast(value, 2, ';');
                double bank = ClampedParse(parts[0], 360d);
                double pitch = ClampedParse(parts[1], 360d);
                _bank.SetValue(new BindingValue(bank), false);
                _pitch.SetValue(new BindingValue(pitch), false);
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
            _bank.SetValue(BindingValue.Empty, true);
            _pitch.SetValue(BindingValue.Empty, true);
        }
    }
}
