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

    public class CMDSTwoCounter : DCSFunctionPair
    {
        private static readonly ExportDataElement[] DataElementsTemplate = { new DCSDataElement("3401", null, false), new DCSDataElement("3405", null, false) };

        private HeliosValue _chaffCounter;
        private HeliosValue _flareCounter;

        public CMDSTwoCounter(BaseUDPInterface sourceInterface)
            : base(sourceInterface,
                   "CMDS", "Chaff Drum Counters", "Chaff Drum Counters(2DrumTape).",
                   "CMDS", "Flare Drum Counters", "Flare Drum Counters(2DrumTape)."
                  )
        {
            DoBuild();
        }

        // deserialization constructor
        public CMDSTwoCounter(BaseUDPInterface sourceInterface, System.Runtime.Serialization.StreamingContext context)
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
            _chaffCounter = new HeliosValue(SourceInterface, BindingValue.Empty, SerializedDeviceName, SerializedFunctionName,
                SerializedDescription, "2digits count", BindingValueUnits.Numeric);
            Values.Add(_chaffCounter);
            Triggers.Add(_chaffCounter);

            _flareCounter = new HeliosValue(SourceInterface, BindingValue.Empty, SerializedDeviceName2, SerializedFunctionName2,
                SerializedDescription2, "2digits count", BindingValueUnits.Numeric);
            Values.Add(_flareCounter);
            Triggers.Add(_flareCounter);
        }

        protected override ExportDataElement[] DefaultDataElements => DataElementsTemplate;

        public override void ProcessNetworkData(string id, string value)
        {
            switch (id)
            {
                case "3401":
                case "3405":
                    double dblValue = 0d;
                    string[] splited = Tokenizer.TokenizeAtLeast(value, 2, ';');
                    var enableParse = double.TryParse(splited[0], out dblValue);

                    if (!(enableParse))
                    {
                        return;
                    }
                    double _tens = ClampedParse(splited[0], 10d);

                    double _ones = Math.Floor(ClampedParse(splited[1], 1d));
                    double count = _tens + _ones;
                    if (id.Equals("3401"))
                    {
                        _chaffCounter.SetValue(new BindingValue(count), false);
                        return;
                    }
                    if (id.Equals("3405"))
                    {
                        _flareCounter.SetValue(new BindingValue(count), false);
                        return;
                    }
                    return;
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
            _chaffCounter.SetValue(BindingValue.Empty, true);
            _flareCounter.SetValue(BindingValue.Empty, true);
        }
    }
}
