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

    public class RaderOperateKnobs : DCSFunction
    {
        private static readonly ExportDataElement[] DataElementsTemplate = {
            new DCSDataElement("2316", null, false) };

        private HeliosValue _knobsArray;
        public RaderOperateKnobs(BaseUDPInterface sourceInterface)
            : base(sourceInterface, "Rader", "RaderOprKnobsArray", "Currently Rader Operate Knobs Array.")
        {
            DoBuild();
        }

        // simple convert
        
        private static readonly CalibrationPointCollectionDouble calibStep =
        new CalibrationPointCollectionDouble(0, 0, 1d, 3d)
        {
            new CalibrationPointDouble(0.0d, 0d),
            new CalibrationPointDouble(0.1d, 1d),
            new CalibrationPointDouble(0.2d, 2d),
            new CalibrationPointDouble(0.3d, 3d),
            new CalibrationPointDouble(0.4d, 4d)
        };

        // deserialization constructor
        public RaderOperateKnobs(BaseUDPInterface sourceInterface, System.Runtime.Serialization.StreamingContext context)
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
            _knobsArray = new HeliosValue(SourceInterface, BindingValue.Empty, SerializedDeviceName, SerializedFunctionName,
                SerializedDescription, "", BindingValueUnits.Numeric);
            Values.Add(_knobsArray);
            Triggers.Add(_knobsArray);
        }

        protected override ExportDataElement[] DefaultDataElements => DataElementsTemplate;

        public override void ProcessNetworkData(string id, string value)
        {
            if(id.Equals("2316"))
            {
                string[] parts = Tokenizer.TokenizeAtLeast(value, 5, ';');

                var p1 = ConvertSelectorValue(parts[0]);
                var p2 = ConvertSelectorValue(parts[1]);

                StringBuilder buf = new StringBuilder();
                buf.Append(p1).Append(p2);
                _knobsArray.SetValue(new BindingValue(buf.ToString()), false);
            }

        }

        private int ConvertSelectorValue(string value)
        {
            if (!double.TryParse(value, NumberStyles.Float, CultureInfo.InvariantCulture.NumberFormat,
                out double scaledValue))
            {
                return 0;
            }
            return Convert.ToInt32(calibStep.Interpolate(scaledValue));
        }
        

        public override void Reset()
        {
            _knobsArray.SetValue(BindingValue.Empty, true);
        }
    }
}
