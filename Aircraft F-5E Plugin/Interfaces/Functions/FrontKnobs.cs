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

    public class FrontKnobs : DCSFunction
    {

        private HeliosValue _frontKnobs;
        private static StringBuilder flags = new StringBuilder(20);

        private static CalibrationPointCollectionDouble calib35Step =
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

        private static CalibrationPointCollectionDouble calib28Step =
            new CalibrationPointCollectionDouble(0, 0, 1d, 28d)
            {
                new CalibrationPointDouble(0.0d, 0d),
                new CalibrationPointDouble(0.035d, 1d),
                new CalibrationPointDouble(0.071d, 2d),
                new CalibrationPointDouble(0.107d, 3d),
                new CalibrationPointDouble(0.142d, 4d),
                new CalibrationPointDouble(0.178d, 5d),
                new CalibrationPointDouble(0.214d, 6d),
                new CalibrationPointDouble(0.25d, 7d),
                new CalibrationPointDouble(0.285d, 8d),
                new CalibrationPointDouble(0.321d, 9d),
                new CalibrationPointDouble(0.357d, 10d),
                new CalibrationPointDouble(0.392d, 11d),
                new CalibrationPointDouble(0.428d, 12d),
                new CalibrationPointDouble(0.464d, 13d),
                new CalibrationPointDouble(0.5d, 14d),
                new CalibrationPointDouble(0.535d, 15d),
                new CalibrationPointDouble(0.571d, 16d),
                new CalibrationPointDouble(0.607d, 17d),
                new CalibrationPointDouble(0.642d, 18d),
                new CalibrationPointDouble(0.678d, 19d),
                new CalibrationPointDouble(0.714d, 20d),
                new CalibrationPointDouble(0.75d, 21d),
                new CalibrationPointDouble(0.785d, 22d),
                new CalibrationPointDouble(0.821d, 23d),
                new CalibrationPointDouble(0.857d, 24d),
                new CalibrationPointDouble(0.892d, 25d),
                new CalibrationPointDouble(0.928d, 26d),
                new CalibrationPointDouble(0.964d, 27d),
                new CalibrationPointDouble(1.0d, 28d)
            };

        private static readonly ExportDataElement[] DataElementsTemplate = {
            new DCSDataElement("2560", null, false)
        };

        // missile volumes:0-1
        // cabin temp knob:2-3
        // defog knob:4-5

        public FrontKnobs(BaseUDPInterface sourceInterface)
            : base(sourceInterface,
                   "FrontPanelKnobs", "FrontPanelKnobs PositionArray", "current FrontPanel Knobs position")
        {
            DoBuild();
        }

        // deserialization constructor
        public FrontKnobs(BaseUDPInterface sourceInterface, System.Runtime.Serialization.StreamingContext context)
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
            _frontKnobs = new HeliosValue(SourceInterface, BindingValue.Empty, SerializedDeviceName, SerializedFunctionName,
                SerializedDescription, "Front Panel Knobs position Array", BindingValueUnits.Text);
            Values.Add(_frontKnobs);
            Triggers.Add(_frontKnobs);
        }

        protected override ExportDataElement[] DefaultDataElements => DataElementsTemplate;

        public override void ProcessNetworkData(string id, string value)
        {
            if (id.Equals("2560"))
            {
                flags.Clear();

                string[] parts = Tokenizer.TokenizeAtLeast(value, 3, ';');
                var mslVolumes = Parse35StepKnob(parts[0]).ToString("D2");
                var cabinTemps = Parse28StepKnob(parts[1]).ToString("D2");
                var defogKnobs = Parse28StepKnob(parts[2]).ToString("D2");
                flags.Append(mslVolumes).Append(cabinTemps).Append(defogKnobs);

                _frontKnobs.SetValue(new BindingValue(flags.ToString()), false);
            }
        }

        private int Parse35StepKnob(string value)
        {
            if (!double.TryParse(value, NumberStyles.Float, CultureInfo.InvariantCulture.NumberFormat,
                out double scaledValue)) {
                return 0;
            }

            return Convert.ToInt32(calib35Step.Interpolate(scaledValue));
        }

        private int Parse28StepKnob(string value)
        {
            if (!double.TryParse(value, NumberStyles.Float, CultureInfo.InvariantCulture.NumberFormat,
                out double scaledValue))
            {
                return 0;
            }

            return Convert.ToInt32(calib28Step.Interpolate(scaledValue));
        }

        public override void Reset()
        {
            _frontKnobs.SetValue(BindingValue.Empty, true);
        }
    }
}
