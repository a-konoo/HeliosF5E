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

    // create original for view Throttle lever position

    public class ThrottlePosition : DCSFunctionPair
    {
        private static readonly ExportDataElement[] DataElementsTemplate = { new DCSDataElement("2343", null, false) };

        private static readonly BindingValue ChuteValue = new BindingValue(1);

        private HeliosValue _leftThrottlePositon;
        private HeliosValue _rightThrottlePositon;

        private int _leftPosition;
        private int _rightPosition;
        private CalibrationPointCollectionDouble calib1;
        private CalibrationPointCollectionDouble calib2;
        public ThrottlePosition(BaseUDPInterface sourceInterface)
            : base(sourceInterface,
                   "Engine Interface", "Left Throttle position", "Currentry Left Throttle Position",
                   "Engine Interface", "Right Throttle position", "Currentry Right Throttle Position"
                  )
        {
            DoBuild();
        }

        // deserialization constructor
        public ThrottlePosition(BaseUDPInterface sourceInterface, System.Runtime.Serialization.StreamingContext context)
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
            _leftThrottlePositon = new HeliosValue(SourceInterface, BindingValue.Empty, SerializedDeviceName, SerializedFunctionName,
                SerializedDescription, "off=0,idle=1 - max=34", BindingValueUnits.Numeric);
            Values.Add(_leftThrottlePositon);
            Triggers.Add(_leftThrottlePositon);

            _rightThrottlePositon = new HeliosValue(SourceInterface, BindingValue.Empty, SerializedDeviceName2, SerializedFunctionName2,
                SerializedDescription2, "off=0,idle=1 - max=34", BindingValueUnits.Numeric);
            Values.Add(_rightThrottlePositon);
            Triggers.Add(_rightThrottlePositon);

            calib1 = new CalibrationPointCollectionDouble(0.48d, 1d, 0.87d, 18d)
            {
                new CalibrationPointDouble(0.48d, 2d),
                new CalibrationPointDouble(0.87d, 18d)
            };
            calib2 = new CalibrationPointCollectionDouble(480d, 19d, 1480d, 34d)
            {
                new CalibrationPointDouble(480d, 19d),
                new CalibrationPointDouble(550d, 20d),
                new CalibrationPointDouble(800d, 21d),
                new CalibrationPointDouble(850d, 22d),
                new CalibrationPointDouble(1000d, 24d),
                new CalibrationPointDouble(1080d, 26d),
                new CalibrationPointDouble(1100d, 28d),
                new CalibrationPointDouble(1200d, 30d),
                new CalibrationPointDouble(1260d, 32d),
                new CalibrationPointDouble(1300d, 33d),
                new CalibrationPointDouble(1330d, 34d),
            };

        }

        protected override ExportDataElement[] DefaultDataElements => DataElementsTemplate;

        public override void ProcessNetworkData(string id, string value)
        {
            switch (id)
            {
                case "2343":
                    string[] parts = Tokenizer.TokenizeAtLeast(value, 4, ';');
                    double leftEngineRPM = ClampedParse(parts[0], 100d);
                    double rightEngineRPM = ClampedParse(parts[1], 100d);
                    double leftFuelFlow = ClampedParse(parts[2], 100d) * 1500;
                    double rightFuelFlow = ClampedParse(parts[3], 100d) * 1500;

                    _leftPosition = CalcThrottlePosion(leftEngineRPM, leftFuelFlow);
                    _rightPosition = CalcThrottlePosion(rightEngineRPM, rightFuelFlow);

                    _leftThrottlePositon.SetValue(new BindingValue(_leftPosition), false);
                    _rightThrottlePositon.SetValue(new BindingValue(_rightPosition), false);
                    break;
            }
        }

        private double ClampedParse(string value, double scale)
        {
            if (!double.TryParse(value, NumberStyles.Float, 
                CultureInfo.InvariantCulture.NumberFormat,
                out double scaledValue))
            {
                return scaledValue;
            }

            scaledValue = Math.Truncate(scaledValue * scale);
            return scaledValue / 100.0d;
        }

        private int CalcThrottlePosion(double rpm, double flow)
        {
            if (rpm >= 0.25d && rpm <= 0.47d)
            {
                return 1;
            }
            else if (rpm > 0.47d)
            {
                var rpmPosition = calib1.Interpolate(rpm);
                if (rpmPosition > 17)
                {
                    var flowPosition = calib2.Interpolate(flow);
                    return Convert.ToInt32(flowPosition);
                } else
                {
                    return Convert.ToInt32(rpmPosition);
                }
            }
            return 0;
        }

        public override void Reset()
        {
            _leftThrottlePositon.SetValue(BindingValue.Empty, true);
            _rightThrottlePositon.SetValue(BindingValue.Empty, true);
        }
    }
}
