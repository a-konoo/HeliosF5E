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


    // UHFRadioFrequency values on packet is formatted for FreqDisplayControl 

    public class UHFRadioFreqPrepare : DCSFunctionPair
    {
        private static readonly ExportDataElement[] DataElementsTemplate = {
            new DCSDataElement("2302", null, false), new DCSDataElement("2326", null, false) };

        private double _tenthousands;
        private double _thousands;
        private double _hundreds;
        private double _tens;
        private double _ones;
        private HeliosValue _freq;
        private HeliosValue _presetChannel;

        public UHFRadioFreqPrepare(BaseUDPInterface sourceInterface)
            : base(sourceInterface,
                   "UHF Radio", "frequency", "Currently tuned UHF Radio Frequency.",
                   "UHF Radio", "presetChannel", "Current UHF Radio channel."
                  )
        {
            DoBuild();
        }

        // deserialization constructor
        public UHFRadioFreqPrepare(BaseUDPInterface sourceInterface, System.Runtime.Serialization.StreamingContext context)
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
            _freq = new HeliosValue(SourceInterface, BindingValue.Empty, SerializedDeviceName, SerializedFunctionName,
                SerializedDescription, "", BindingValueUnits.Numeric);
            Values.Add(_freq);
            Triggers.Add(_freq);

            _presetChannel = new HeliosValue(SourceInterface, BindingValue.Empty, SerializedDeviceName2, SerializedFunctionName2,
                SerializedDescription2, "0,1,2,3,4,5...19", BindingValueUnits.Numeric);
            Values.Add(_presetChannel);
            Triggers.Add(_presetChannel);
        }

        protected override ExportDataElement[] DefaultDataElements => DataElementsTemplate;

        public override void ProcessNetworkData(string id, string value)
        {
            switch (id)
            {
                case "2302": // 2302
                    string[] parts = Tokenizer.TokenizeAtLeast(value, 5, ';');
                    // add 0.03 and Math.Round
                    double picked = 0d;
                    Double.TryParse(parts[0], out picked);

                    double adjust = (Math.Round(picked * 10d + 0.3d) * 10d) / 10d;
                    double td = Math.Floor(adjust);

                    _tenthousands = td * 10000d;
                    _thousands = ClampedParse(parts[1], 1000d);
                    _hundreds = ClampedParse(parts[2], 100d);
                    _tens = ClampedParse(parts[3], 10d);
                    // 3:0.60 4:0.70 -> 7 or add 0.5 and Multiply 10
                    double onesTwo = ClampedParse(parts[4], 1d) + 0.5d;
                    _ones = Math.Floor(onesTwo / 2.5d);
                    double channel = _tenthousands + _thousands + _hundreds + _tens + _ones;
                    _freq.SetValue(new BindingValue(channel), false);
                    // Console.WriteLine("freq:" + channel);
                    break;
                case "302":
                    string[] partsx = Tokenizer.TokenizeAtLeast(value, 1, ';');
                    break;
                case "2326":
                    double dblValue = 0d;
                    string[] splited = Tokenizer.TokenizeAtLeast(value, 1, ';');
                    var enableParse = double.TryParse(splited[0], out dblValue);

                    if (enableParse && dblValue >= 0 && 1d >= dblValue)
                    {
                        int p = (int)Math.Floor(dblValue * 20) + 1;
                        Console.WriteLine("channel:" + dblValue);
                        _presetChannel.SetValue(new BindingValue(p), false);
                    }
                    break;
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
            _freq.SetValue(BindingValue.Empty, true);
            _presetChannel.SetValue(BindingValue.Empty, true);
        }
    }
}
