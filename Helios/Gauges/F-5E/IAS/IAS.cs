//  Copyright 2014 Craig Courtney
//  Copyright 2021 Helios Contributors
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

namespace GadrocsWorkshop.Helios.Gauges.F5E.IAS
{
    using GadrocsWorkshop.Helios.ComponentModel;
    using System;
    using System.Windows;

    [HeliosControl("Helios.F5E.IAS", "IAS", "F-5E Gauges", typeof(GaugeRenderer))]
    public class IAS : BaseGauge
    {
        private HeliosValue _indicatedAirSpeed;
        private HeliosValue _machIndicator;
        private HeliosValue _airlimitSpeed;
        private GaugeNeedle _needlePlate;
        private GaugeNeedle _needle;
        private GaugeNeedle _machPlate;
        private GaugeNeedle _limitSpeedIndicator;
        private CalibrationPointCollectionDouble _airSpeedNeedleCalibration;
        private CalibrationPointCollectionDouble _machNeedleCalibration;
        private double currentMachAngle = 0d;
        private double currentSpeedAngle = 0d;
        private double currentSpeed = 0d;
        public IAS()
            : base("IAS", new Size(175d, 175d))
        {
            Point center = new Point(87d, 87d);
            Components.Add(new GaugeImage("{Helios}/Gauges/F-5E/IAS/ias_faceplate.xaml", new Rect(7.5d, 7.5d, 162.5d, 162.5d)));

            _machPlate = new GaugeNeedle("{Helios}/Gauges/F-5E/IAS/ias_mach_plate.xaml", center, new Size(100d, 100d), new Point(50d, 50d), 0d);
            Components.Add(_machPlate);

            _needlePlate = new GaugeNeedle("{Helios}/Gauges/F-5E/IAS/ias_needle_plate.xaml", center, new Size(105d, 105d), new Point(52.5d, 52.5d), 0d);
            Components.Add(_needlePlate);

            _needle = new GaugeNeedle("{Helios}/Gauges/F-5E/IAS/ias_needle.xaml", center, new Size(20d, 76d), new Point(10d, 70d), 0d);
            Components.Add(_needle);

            _limitSpeedIndicator = new GaugeNeedle("{Helios}/Gauges/F-5E/IAS/ias_limit_needle.xaml", center, new Size(10d, 80d), new Point(5d, 80d), 0d);
            Components.Add(_limitSpeedIndicator);

            Components.Add(new GaugeImage("{Helios}/Gauges/F-5E/IAS/IASGaugeCover.png", new Rect(0d, 0d, 175d, 175d)));

            _airSpeedNeedleCalibration = new CalibrationPointCollectionDouble(0d, 0d, 1000d, 360d)
            {
                new CalibrationPointDouble(0d, 0d),
                new CalibrationPointDouble(50d, 10d),
                new CalibrationPointDouble(150d, 90d),
                new CalibrationPointDouble(200d, 135d),
                new CalibrationPointDouble(270d, 175d),
                new CalibrationPointDouble(300d, 185d),
                new CalibrationPointDouble(370d, 205d),
                new CalibrationPointDouble(400d, 235d),
                new CalibrationPointDouble(422d, 247d),
                new CalibrationPointDouble(450d, 255d),
                new CalibrationPointDouble(495d, 295d),
                new CalibrationPointDouble(520d, 310d),
                new CalibrationPointDouble(900d, 355d),
            };

            _machNeedleCalibration = new CalibrationPointCollectionDouble(0d, 0d, 1d, 180d)
            {
                new CalibrationPointDouble(0d, 0d),
                new CalibrationPointDouble(0.037d, 10d),
                new CalibrationPointDouble(0.067d, 20d),
                new CalibrationPointDouble(0.17d, 50d),
                new CalibrationPointDouble(0.23d, 80d),
                new CalibrationPointDouble(0.25d, 95d),
                new CalibrationPointDouble(0.31d, 105d),
                new CalibrationPointDouble(0.33d, 120d),
                new CalibrationPointDouble(0.36d, 128d),

            };
            _indicatedAirSpeed = new HeliosValue(this, new BindingValue(0d), "", "indicated airspeed",
                "current indicated airspeed of the aircraft.", "(0 - 900)", BindingValueUnits.Knots);
            _indicatedAirSpeed.Execute += new HeliosActionHandler(IndicatedAirSpeed_Execute);
            Actions.Add(_indicatedAirSpeed);


            _machIndicator = new HeliosValue(this, new BindingValue(0d), "", "mach indicator",
                "current indicated mach value.", "(1 - 0)", BindingValueUnits.Numeric);
            _machIndicator.Execute += new HeliosActionHandler(MachIndicator_Execute);
            Actions.Add(_machIndicator);

            _airlimitSpeed = new HeliosValue(this, new BindingValue(0d), "", "limit air speed indicator",
                "current limit speed.", "(0 - 900)", BindingValueUnits.Numeric);
            _airlimitSpeed.Execute += new HeliosActionHandler(AirSpeedLimit_Execute);
            Actions.Add(_airlimitSpeed);
        }

        void IndicatedAirSpeed_Execute(object action, HeliosActionEventArgs e)
        {
            currentSpeed = e.Value.DoubleValue;
            currentSpeedAngle = _airSpeedNeedleCalibration.Interpolate(e.Value.DoubleValue);
            _needlePlate.Rotation = currentSpeedAngle;
            _needle.Rotation = currentSpeedAngle;
            _machPlate.Rotation = currentSpeedAngle - currentMachAngle;
        }

        void MachIndicator_Execute(object action, HeliosActionEventArgs e)
        {
            var machValue = e.Value.DoubleValue;
            var machRatio = 1 - machValue;
            if (currentSpeed > 190)
            {
                
                currentMachAngle = _machNeedleCalibration.Interpolate(machRatio);
            }
            else
            {
                currentMachAngle = 0d;
            }


            _machPlate.Rotation = currentSpeedAngle - currentMachAngle;
            _needle.Rotation = currentSpeedAngle;
            Console.WriteLine("IAS mach:" + machRatio + "/" + currentMachAngle + "/" + currentSpeed + "/" + currentSpeedAngle);
        }
        void AirSpeedLimit_Execute(object action, HeliosActionEventArgs e)
        {
            _limitSpeedIndicator.Rotation = _airSpeedNeedleCalibration.Interpolate(e.Value.DoubleValue);
        }
    }
}
