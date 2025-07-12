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

    public class SAIPitchTrimAdjuster : DCSFunctionPair
    {
        private static readonly ExportDataElement[]
            DataElementsTemplate = {
                new DCSDataElement("2442", null, true) };

        private HeliosValue _pitchTrimForKnob;
        private HeliosValue _pitchTrimForIndicator;
        private int prevPosition = 0;

        public SAIPitchTrimAdjuster(BaseUDPInterface sourceInterface)
            : base(sourceInterface,
                   "Standby Attitude Indicator", "SAI pitchtrim position for PullableKnob", "current position of SAI Trim Knob.",
                   "Standby Attitude Indicator", "SAI pitch gauge angle for SAI gauge", "current SAI display value.")
        {
            DoBuild();
        }

        // deserialization constructor
        public SAIPitchTrimAdjuster(BaseUDPInterface sourceInterface,
            System.Runtime.Serialization.StreamingContext context)
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
            _pitchTrimForKnob = new HeliosValue(SourceInterface, BindingValue.Empty, SerializedDeviceName, SerializedFunctionName,
                SerializedDescription, "SAI trim knob position.(0-14)", BindingValueUnits.Numeric);
            Values.Add(_pitchTrimForKnob);
            Triggers.Add(_pitchTrimForKnob);

            _pitchTrimForIndicator = new HeliosValue(SourceInterface, BindingValue.Empty, SerializedDeviceName2, SerializedFunctionName2,
                SerializedDescription2, "SAI offset position.(0 To -1).", BindingValueUnits.Numeric);
            Values.Add(_pitchTrimForIndicator);
            Triggers.Add(_pitchTrimForIndicator);
        }

        protected override ExportDataElement[] DefaultDataElements => DataElementsTemplate;

        public override void ProcessNetworkData(string id, string value)
        {
            string[] parts;
            if (id.Equals("2442"))
            {
                parts = Tokenizer.TokenizeAtLeast(value, 1, ';');
                int knobRotation = 0;
                double trimAngle = 0;
                double dblValue = Math.Round(Convert.ToDouble(parts[0]), 3);
                var isValidInput = true;
                switch (dblValue)
                {
                    case double judge when judge <= -1:
                        knobRotation = 0;
                        trimAngle = 0;
                        break;
                    case double judge when judge >= -0.99 && judge < -0.85:
                        knobRotation = 1;
                        trimAngle = 0.07d;
                        break;
                    case double judge when  judge >= -0.85 && judge <= -0.7:
                        knobRotation = 2;
                        trimAngle = 0.13;
                        break;
                    case double judge when judge >= -0.7 &&  judge < -0.5:
                        knobRotation = 3;
                        trimAngle = 0.2d;
                        break;
                    case double judge when judge >= -0.5 && judge < -0.39:
                        knobRotation = 4;
                        trimAngle = 0.26d;
                        break;
                    case double judge when judge >= -0.39 && judge < -0.3:
                        knobRotation = 5;
                        trimAngle = 0.33d;
                        break;
                    case double judge when judge >= -0.3 && judge < -0.05:
                        knobRotation = 6;
                        trimAngle = 0.46d;
                        break;
                    case double judge when judge >= -0.05 && judge <= 0.05:
                        knobRotation = 7;
                        trimAngle = 0.53d;
                        break;
                    case double judge when judge > 0.05 && judge < 0.25:
                        knobRotation = 8;
                        trimAngle = 0.59d;
                        break;
                    case double judge when judge >= 0.25 && judge <= 0.3:
                        knobRotation = 9;
                        trimAngle = 0.66d;
                        break;
                    case double judge when judge > 0.3 && judge < 0.45:
                        knobRotation = 10;
                        trimAngle = 0.73d;
                        break;
                    case double judge when judge >= 0.45 && judge < 0.65:
                        knobRotation = 11;
                        trimAngle = 0.79d;
                        break;
                    case double judge when judge >= 0.65 && judge < 0.8:
                        knobRotation = 12;
                        trimAngle = 0.85d;
                        break;
                    case double judge when judge >= 0.8 && judge <= 0.95:
                        knobRotation = 13;
                        trimAngle = 0.95d;
                        break;
                    case double judge when judge >= 1:
                        knobRotation = 14;
                        trimAngle = 1.0d;
                        break;
                    default:
                        isValidInput = false;
                        break;
                }
                if (isValidInput)
                {
                    _pitchTrimForKnob.SetValue(new BindingValue(knobRotation), false);
                    _pitchTrimForIndicator.SetValue(new BindingValue(trimAngle), false);

                }
                else
                {
                    _pitchTrimForKnob.SetValue(BindingValue.Empty, false);
                    _pitchTrimForIndicator.SetValue(new BindingValue(trimAngle), false);
                }

            }
        }

        public override void Reset()
        {
            _pitchTrimForKnob.SetValue(BindingValue.Empty, true);
            _pitchTrimForIndicator.SetValue(BindingValue.Empty, true);
        }
    }
}
