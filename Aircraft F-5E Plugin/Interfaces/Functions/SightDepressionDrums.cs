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

    public class SightDepressionDrums : DCSFunction
    {
        private static readonly ExportDataElement[] DataElementsTemplate = {
            new DCSDataElement("2043", null, false) };

        private HeliosValue _gauge;

        public SightDepressionDrums(BaseUDPInterface sourceInterface)
            : base(sourceInterface, "Sight", "SightGauge", "Currently Sight GaugeValue.")
        {
            DoBuild();
        }

        // deserialization constructor
        public SightDepressionDrums(BaseUDPInterface sourceInterface, System.Runtime.Serialization.StreamingContext context)
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
            _gauge = new HeliosValue(SourceInterface, BindingValue.Empty, SerializedDeviceName, SerializedFunctionName,
                SerializedDescription, "", BindingValueUnits.Numeric);
            Values.Add(_gauge);
            Triggers.Add(_gauge);
        }

        protected override ExportDataElement[] DefaultDataElements => DataElementsTemplate;

        public override void ProcessNetworkData(string id, string value)
        {
            if(id.Equals("2043"))
            {
                string[] parts = Tokenizer.TokenizeAtLeast(value, 2, ';');
                
                if (parts.Length != 3) { return; }

                var gauge100 = ClampedParse(parts[0], 1d);
                var gauge10 = ClampedParse(parts[1], 1d);
                var gauge1 = ClampedParse(parts[2], 1d);
                var gaugeValue = gauge100 * 100 + gauge10 * 10 + gauge1;
                _gauge.SetValue(new BindingValue(gaugeValue.ToString()), false);
            }

        }

        private int ClampedParse(string value, double scale)
        {
            if (!double.TryParse(value, NumberStyles.Float, CultureInfo.InvariantCulture.NumberFormat,
                out double scaledValue))
            {
                return 0;
            }

            if (scaledValue < 1.0d)
            {
                scaledValue = Math.Truncate(scaledValue * 10d) * scale;
            }
            else
            {
                scaledValue = 0d;
            }
            return Convert.ToInt32(scaledValue);
        }


        public override void Reset()
        {
            _gauge.SetValue(BindingValue.Empty, true);
        }
    }
}
