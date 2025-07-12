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
    using System.Timers;
    public class GearLever : DCSFunction
    {
        private static readonly ExportDataElement[] DataElementsTemplate = { new DCSDataElement("2557", null, false) };
        private int oldValue = 0;
        private static readonly BindingValue GearLeverValue = new BindingValue(1);

        private HeliosValue _value;
        public GearLever(BaseUDPInterface sourceInterface)
            : base(sourceInterface,
                   "Gear Interface", "Gear Lever(Adjust)", "Currently GearLever Value.")
        {
            DoBuild();
        }

        // deserialization constructor
        public GearLever(BaseUDPInterface sourceInterface, System.Runtime.Serialization.StreamingContext context)
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
            _value = new HeliosValue(SourceInterface, BindingValue.Empty, SerializedDeviceName, SerializedFunctionName,
                SerializedDescription, "alert=2, up=1, down=0", BindingValueUnits.Numeric);
            Values.Add(_value);
            Triggers.Add(_value);
        }

        protected override ExportDataElement[] DefaultDataElements => DataElementsTemplate;

        public override void ProcessNetworkData(string id, string value)
        {
            switch (id)
            {
                case "2557":
                    string[] parts = Tokenizer.TokenizeAtLeast(value, 1, ';');
                    double dblValue = ClampedParse(parts[0], 1d);
                    if (dblValue <= 0)
                    {
                        oldValue = 0;
                    }
                    else if (dblValue > 5)
                    {
                        if (oldValue == 0)
                        {
                            System.Timers.Timer timer = new System.Timers.Timer();
                            timer.Elapsed += new ElapsedEventHandler(OnTimer);
                            timer.Interval = 2000;
                            timer.Enabled = true;
                            timer.AutoReset = false;
                        }
                        oldValue = 2;
                    }
                    _value.SetValue(new BindingValue(oldValue), false);
                    break;
            }
        }

        public void OnTimer(Object source, ElapsedEventArgs e)
        {
            System.Timers.Timer theTimer = (System.Timers.Timer)source;
            theTimer.Enabled = false;
            _value.SetValue(new BindingValue(1), false);
            oldValue = 1;
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
            _value.SetValue(BindingValue.Empty, true);
        }
    }
}
