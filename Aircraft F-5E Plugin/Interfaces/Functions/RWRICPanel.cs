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


    // RWR IC Panel

    public class RWRICPanel : DCSFunctionPair
    {

        private HeliosValue _rwrICPanelFlags;
        private HeliosValue _rwrICPanelKnobs;
        private static StringBuilder flags = new StringBuilder(10);
        private static StringBuilder knobBuf = new StringBuilder(4);

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
                   "RWR IC", "RWR IC Panel Button Flags", "current RWRIC Panel status.",
                   "RWR IC", "RWR IC Panel KnobsArray", "current RWRIC Knob status." )
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

            _rwrICPanelKnobs = new HeliosValue(SourceInterface, BindingValue.Empty, SerializedDeviceName2, SerializedFunctionName2,
                SerializedDescription2, "RWR IC Panel KnobsArray", BindingValueUnits.Text);
            Values.Add(_rwrICPanelKnobs);
            Triggers.Add(_rwrICPanelKnobs);
            
        }

        protected override ExportDataElement[] DefaultDataElements => DataElementsTemplate;

        // 35step convert
        private static readonly CalibrationPointCollectionDouble calib35Step =
        new CalibrationPointCollectionDouble(0, 0, 1d, 35d)
        {
                new CalibrationPointDouble(0.0d, 0d),
                new CalibrationPointDouble(0.028d, 1d),
                new CalibrationPointDouble(0.057d, 2d),
                new CalibrationPointDouble(0.085d, 3d),
                new CalibrationPointDouble(0.114d, 4d),
                new CalibrationPointDouble(0.143d, 5d),
                new CalibrationPointDouble(0.171d, 6d),
                new CalibrationPointDouble(0.2d, 7d),
                new CalibrationPointDouble(0.229d, 8d),
                new CalibrationPointDouble(0.257d, 9d),
                new CalibrationPointDouble(0.285d, 10d),
                new CalibrationPointDouble(0.314d, 11d),
                new CalibrationPointDouble(0.343d, 12d),
                new CalibrationPointDouble(0.371d, 13d),
                new CalibrationPointDouble(0.4d, 14d),
                new CalibrationPointDouble(0.428d, 15d),
                new CalibrationPointDouble(0.457d, 16d),
                new CalibrationPointDouble(0.485d, 17d),
                new CalibrationPointDouble(0.514d, 18d),
                new CalibrationPointDouble(0.542d, 19d),
                new CalibrationPointDouble(0.571d, 20d),
                new CalibrationPointDouble(0.6d, 21d),
                new CalibrationPointDouble(0.628d, 22d),
                new CalibrationPointDouble(0.657d, 23d),
                new CalibrationPointDouble(0.685d, 24d),
                new CalibrationPointDouble(0.714d, 25d),
                new CalibrationPointDouble(0.742d, 26d),
                new CalibrationPointDouble(0.771d, 27d),
                new CalibrationPointDouble(0.8d, 28d),
                new CalibrationPointDouble(0.829d, 29d),
                new CalibrationPointDouble(0.857d, 30d),
                new CalibrationPointDouble(0.885d, 31d),
                new CalibrationPointDouble(0.914d, 32d),
                new CalibrationPointDouble(0.942d, 33d),
                new CalibrationPointDouble(0.971d, 34d),
                new CalibrationPointDouble(1.0d, 35d)
        };

        public override void ProcessNetworkData(string id, string value)
        {
            // panels
            if (id.Equals("2551"))
            {
                flags.Clear();

                string[] parts = Tokenizer.TokenizeAtLeast(value, 10, ';');
                int _rwrModeTmp = Convert.ToInt32(Convert.ToDouble(parts[0]));
                int _rwrSearchTmp = Convert.ToInt32(Convert.ToDouble(parts[1]));
                int _rwrHandOff = Convert.ToInt32(Convert.ToDouble(parts[2]));
                int _rwrAlti = Convert.ToInt32(Convert.ToDouble(parts[3]));
                int _rwrT = Convert.ToInt32(Convert.ToDouble(parts[4]));
                int _rwrTest = Convert.ToInt32(Convert.ToDouble(parts[5]));
                int _rwrUnknownTmp = Convert.ToInt32(Convert.ToDouble(parts[6]));
                int _rwrPower = Convert.ToInt32(Convert.ToDouble(parts[7]));
                int _ = Convert.ToInt32(Convert.ToDouble(parts[8]));
                int _rwrActPwr = Convert.ToInt32(Convert.ToDouble(parts[9]));
                int _rwrSearch = 0;
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


                flags.Append(_rwrPower == 0 ? 0 : ((prevModeCount % 2) + 1))    // 1,2 toggle 
                    .Append(_rwrPower == 0 ? 0 : ((prevSearchCount % 2) + 1))   // 2,3
                    .Append(_rwrPower == 0 ? 0 : (_rwrHandOff == 0 ? 1 : 2))    // 3,4 momentary
                    .Append(_rwrPower == 0 ? 0 : (_rwrAlti == 0 ? 1 : 2))       // 4,5
                    .Append(_rwrPower == 0 ? 0 : (_rwrT == 0 ? 1 : 2))          // 5,6
                    .Append(_rwrPower == 0 ? 0 : (_rwrTest == 0 ? 1 : 2))       // 6,7
                    .Append(_rwrPower == 0 ? 0 : ((prevUnknownCount % 2) + 1))  // 7,8 // toggle
                    .Append(_rwrPower == 0 ? 0 : 1)                             // 8,9 
                    .Append(0)                                 // 9,10
                    .Append(_rwrPower == 0 ? 0 : (_rwrActPwr == 0 ? 1 : 2));  // 10,11

                _rwrICPanelFlags.SetValue(new BindingValue(flags.ToString()), false);
            }
            // knobs
            if (id.Equals("2578"))
            {
                knobBuf.Clear();
                string[] parts = Tokenizer.TokenizeAtLeast(value, 1, ';');
                var rwrAudio = Parse35StepKnob(parts[0]).ToString("D2");
                var rwrDim = Parse35StepKnob(parts[1]).ToString("D2");
                knobBuf.Append(rwrAudio).Append(rwrDim);

                _rwrICPanelKnobs.SetValue(new BindingValue(knobBuf.ToString()), false);
            }
        }

        private int Parse35StepKnob(string value)
        {
            if (!double.TryParse(value, NumberStyles.Float, CultureInfo.InvariantCulture.NumberFormat,
                out double scaledValue))
            {
                return 0;
            }

            return Convert.ToInt32(calib35Step.Interpolate(scaledValue));
        }

        public override void Reset()
        {
            _rwrICPanelFlags.SetValue(BindingValue.Empty, true);
            _rwrICPanelKnobs.SetValue(BindingValue.Empty, true);
            prevModeValue = 0;
            prevSearchValue = 0;
            prevUnknownCount = 0;

        }
    }
}
