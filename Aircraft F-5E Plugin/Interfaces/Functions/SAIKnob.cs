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

    public class SAIKnob : DCSFunction
    {
        private static readonly ExportDataElement[]
            DataElementsTemplate = {
                new DCSDataElement("2442", null, true) };

        private HeliosValue _pitchTrimForKnob;
        private HeliosValue _pitchTrimForIndicator;
        private int prevPosition = 0;

        public SAIKnob(BaseUDPInterface sourceInterface)
            : base(sourceInterface,
                   "Standby Attitude Indicator", "SAI pitchtrim position for PullableKnob",
                   "current position of SAI Trim Knob.")
        {
            DoBuild();
        }

        // deserialization constructor
        public SAIKnob(BaseUDPInterface sourceInterface,
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
                SerializedDescription, "SAI trim knob position.(0-15)", BindingValueUnits.Numeric);
            Values.Add(_pitchTrimForKnob);
            Triggers.Add(_pitchTrimForKnob);
        }

        protected override ExportDataElement[] DefaultDataElements => DataElementsTemplate;

        public override void ProcessNetworkData(string id, string value)
        {
            string[] parts;
            if (id.Equals("2442"))
            {
                parts = Tokenizer.TokenizeAtLeast(value, 1, ';');
                int knobRotation = 0;
                double dblValue = Math.Round(Convert.ToDouble(parts[0]), 3);
                var isValidInput = true;
                switch (dblValue)
                {
                    case double judge when judge <= 0:
                        knobRotation = 0;
                        break;
                    case double judge when (judge == 0.075 || judge == 0.025):
                        knobRotation = 1;
                        break;
                    case double judge when (judge == 0.15 && judge == 0.1):
                        knobRotation = 2;
                        break;
                    case double judge when (judge == 0.225 || judge < 0.175):
                        knobRotation = 3;
                        break;
                    case double judge when (judge == 0.3 || judge == 0.25):
                        knobRotation = 4;
                        break;
                    case double judge when (judge == 0.375 || judge == 0.325):
                        knobRotation = 5;
                        break;
                    case double judge when (judge == 0.45 || judge == 0.4):
                        knobRotation = 6;
                        break;
                    case double judge when (judge == 0.525 || judge == 0.475):
                        knobRotation = 7;
                        break;
                    case double judge when (judge == 0.6 || judge == 0.55):
                        knobRotation = 8;
                        break;
                    case double judge when (judge == 0.675 || judge == 0.625):
                        knobRotation = 9;
                        break;
                    case double judge when (judge == 0.75 || judge == 0.7):
                        knobRotation = 10;
                        break;
                    case double judge when (judge == 0.825 || judge == 0.775):
                        knobRotation = 11;
                        break;
                    case double judge when (judge == 0.9 || judge == 0.85):
                        knobRotation = 12;
                        break;
                    case double judge when (judge == 0.975 || judge == 0.925):
                        knobRotation = 13;
                        break;
                    case double judge when judge >= 1:
                        knobRotation = 14;
                        break;
                    default:
                        isValidInput = false;
                        break;
                }
                if (isValidInput)
                {
                    _pitchTrimForKnob.SetValue(new BindingValue(knobRotation), false);

                }
                else
                {
                    _pitchTrimForKnob.SetValue(BindingValue.Empty, false);
                }

            }
        }

        public override void Reset()
        {
            _pitchTrimForKnob.SetValue(BindingValue.Empty, true);
            if (_pitchTrimForIndicator != null)
            {
                _pitchTrimForIndicator.SetValue(BindingValue.Empty, true);
            }
        }
    }
}
