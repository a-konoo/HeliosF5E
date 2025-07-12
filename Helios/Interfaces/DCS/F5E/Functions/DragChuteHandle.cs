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

    public class DragChuteHandle : DCSFunctionPair
    {
        private static readonly ExportDataElement[] DataElementsTemplate = { new DCSDataElement("2091", null, false) };

        private static readonly BindingValue ChuteValue = new BindingValue(1);

        private HeliosValue _showmode;
        private HeliosValue _oprmode;
        public DragChuteHandle(BaseUDPInterface sourceInterface)
            : base(sourceInterface,
                   "Cockpit", "Chute Handle Mode(Show)", "Currently Chute Handle mode(ShowOnly Graphic Rotary Handle).",
                   "Cockpit", "Chute Handle Mode(Opr)", "Currently Chute Handle mode(Rotary Knob)."
                  )
        {
            DoBuild();
        }

        // deserialization constructor
        public DragChuteHandle(BaseUDPInterface sourceInterface, System.Runtime.Serialization.StreamingContext context)
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
            _showmode = new HeliosValue(SourceInterface, BindingValue.Empty, SerializedDeviceName, SerializedFunctionName,
                SerializedDescription, "max=3,  middle=2, ready=1, off=0", BindingValueUnits.Numeric);
            Values.Add(_showmode);
            Triggers.Add(_showmode);

            _oprmode = new HeliosValue(SourceInterface, BindingValue.Empty, SerializedDeviceName2, SerializedFunctionName2,
                SerializedDescription2, "extend=1, ready=2, off=3", BindingValueUnits.Numeric);
            Values.Add(_oprmode);
            Triggers.Add(_oprmode);
        }

        protected override ExportDataElement[] DefaultDataElements => DataElementsTemplate;

        public override void ProcessNetworkData(string id, string value)
        {
            switch (id)
            {
                case "2091":
                    string[] parts = Tokenizer.TokenizeAtLeast(value, 1, ';');
                    double dblValue = ClampedParse(parts[0], 1d);
                    int showonlyMode = 3;
                    int oprMode = 1;
                    if (dblValue <= 1)
                    {
                        showonlyMode = 0;
                        oprMode = 3;
                    }
                    else if (dblValue < 4)
                    {
                        showonlyMode = 1;
                        oprMode = 2;
                    }
                    else if (dblValue < 8)
                    {
                        showonlyMode = 2;
                        oprMode = 2;
                    }
                    Console.WriteLine("ChuteHandle:" + showonlyMode + "/" + _oprmode + "/" + dblValue);
                    _showmode.SetValue(new BindingValue(showonlyMode), false);
                    _oprmode.SetValue(new BindingValue(oprMode), false);
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

            scaledValue = Math.Truncate(scaledValue * 10d) * scale;
            return scaledValue;
        }

        public override void Reset()
        {
            _showmode.SetValue(BindingValue.Empty, true);
            _oprmode.SetValue(BindingValue.Empty, true);
        }
    }
}
