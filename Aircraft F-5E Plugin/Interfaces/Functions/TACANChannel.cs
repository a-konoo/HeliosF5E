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

    // copy from A-10C Function

    public class TACANChannel : DCSFunctionPair
    {
        private static readonly ExportDataElement[] DataElementsTemplate = {
            new DCSDataElement("2263", null, false), new DCSDataElement("2666", "%0.1f", false) };

        private static readonly BindingValue XValue = new BindingValue(1);
        private static readonly BindingValue YValue = new BindingValue(2);

        private HeliosValue _channel;
        private HeliosValue _mode;

        public TACANChannel(BaseUDPInterface sourceInterface)
            : base(sourceInterface,
                   "TACAN", "Channel", "Currently tuned TACAN channel.",
                   "TACAN", "Channel Mode", "Current TACAN channel mode."
                  )
        {
            DoBuild();
        }

        // deserialization constructor
        public TACANChannel(BaseUDPInterface sourceInterface, System.Runtime.Serialization.StreamingContext context)
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
            _channel = new HeliosValue(SourceInterface, BindingValue.Empty, SerializedDeviceName, SerializedFunctionName,
                SerializedDescription, "", BindingValueUnits.Numeric);
            Values.Add(_channel);
            Triggers.Add(_channel);

            _mode = new HeliosValue(SourceInterface, BindingValue.Empty, SerializedDeviceName2, SerializedFunctionName2,
                SerializedDescription2, "1=X, 2=Y", BindingValueUnits.Numeric);
            Values.Add(_mode);
            Triggers.Add(_mode);
        }

        protected override ExportDataElement[] DefaultDataElements => DataElementsTemplate;

        public override void ProcessNetworkData(string id, string value)
        {
            if(id.Equals("2263"))
            {
                string[] parts = Tokenizer.TokenizeAtLeast(value, 4, ';');
                var _thounds = ClampedParse(parts[0], 1000d);
                var _hundreds = ClampedParse(parts[1], 100d);
                var _tens = ClampedParse(parts[2], 10d);
                var _ones = ClampedParse(parts[3], 1d);

                double channel = _thounds + _hundreds + _tens + _ones;
                _channel.SetValue(new BindingValue(channel), false);
                _mode.SetValue(new BindingValue(_ones), false);
            }
        }

        private double ClampedParse(string value, double scale)
        {
            if (!double.TryParse(value, NumberStyles.Float, CultureInfo.InvariantCulture.NumberFormat,
                out double scaledValue))
            {
                return scaledValue;
            }

            if (scaledValue < 1.0d)
            {
                scaledValue = Math.Truncate(scaledValue * 10d) * scale;
            }
            else
            {
                scaledValue = 0d;
            }
            return scaledValue;
        }

        public override void Reset()
        {
            _channel.SetValue(BindingValue.Empty, true);
            _mode.SetValue(BindingValue.Empty, true);
        }
    }
}
