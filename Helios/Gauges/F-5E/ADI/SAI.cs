//  Copyright 2014 Craig Courtney
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

namespace GadrocsWorkshop.Helios.Gauges.F5E.ADI
{
    using GadrocsWorkshop.Helios.ComponentModel;
    using System;
    using System.Windows;
    using System.Windows.Media;

    [HeliosControl("Helios.F5E.SAI", "Standby Attitude Indicator", "F-5E Gauges", typeof(GaugeRenderer))]
    public class SAI : BaseGauge
    {
        private HeliosValue _pitch;
        private HeliosValue _roll;
        private HeliosValue _pitchAdjustment;
        private HeliosValue _offFlag;

        private CalibrationPointCollectionDouble _pitchCalibration;
        private CalibrationPointCollectionDouble _pitchAdjustCalibaration;

        private GaugeNeedle _ball;
        private GaugeNeedle _wings;
        private GaugeNeedle _offFlagNeedle;
        private GaugeNeedle _bankNeedle;

        public SAI()
            : base("Standby Attitude Indicator", new Size(350, 350))
        {
            Point center = new Point(165d, 205d);
            Point center2 = new Point(165d, 179d);
            Point center3 = new Point(165d, 189d);
            Point wingCenter = new Point(170d, 185d);
            _pitchCalibration = new CalibrationPointCollectionDouble(-360d, -1066d, 360d, 1066d);
            _ball = new GaugeNeedle("{Helios}/Gauges/F-5E/ADI/adi_backup_ball.xaml", center2, new Size(300d, 1350d), new Point(150d, 677d));
            _ball.Clip = new EllipseGeometry(center3, 150d, 135d);
            Components.Add(_ball);

            _offFlagNeedle = new GaugeNeedle("{Helios}/Gauges/F-5E/ADI/adi_backup_warning_flag.xaml", new Point(29d, 226d), new Size(31d, 127d), new Point(0d, 127d));
            Components.Add(_offFlagNeedle);

            Components.Add(new GaugeImage("{Helios}/Gauges/F-5E/ADI/SAIGaugeCover.png", new Rect(0d, 0d, 350d, 350d)));

            Components.Add(new GaugeImage("{Helios}/Gauges/F-5E/ADI/ADIGaugeGrass.png", new Rect(0d, 0d, 350d, 350d)));

            Components.Add(new GaugeImage("{Helios}/Gauges/F-5E/ADI/adi_bank_indicates.xaml", new Rect(30d, 30d, 270d, 290d)));

            _wings = new GaugeNeedle("{Helios}/Gauges/F-5E/ADI/sai_wing.xaml", wingCenter, new Size(130d, 26d), new Point(65d, 13d));
            Components.Add(_wings);

            _bankNeedle = new GaugeNeedle("{Helios}/Gauges/F-5E/ADI/adi_bank_pointer.xaml", center2, new Size(32d, 235d), new Point(16d, 117.5d));
            Components.Add(_bankNeedle);

            _pitchAdjustCalibaration = new CalibrationPointCollectionDouble(0d, 30d, 1d, -60d);

            _pitch = new HeliosValue(this, new BindingValue(0d), "", "Pitch", "Current ptich of the aircraft.", "(0 - 360)", BindingValueUnits.Degrees);
            _pitch.Execute += new HeliosActionHandler(Pitch_Execute);
            Actions.Add(_pitch);

            _roll = new HeliosValue(this, new BindingValue(0d), "", "Bank", "Current bank of the aircraft.", "(0 - 360)", BindingValueUnits.Degrees);
            _roll.Execute += new HeliosActionHandler(Bank_Execute);
            Actions.Add(_roll);

            // SDI pitch offset was received wrong value when trim knob moves in minus angle.
            // therefore Then ,knob rotate action make will make value for this SDI pitch trim offset additionally
            _pitchAdjustment = new HeliosValue(this, new BindingValue(0d), "", "Current pitch offset value", "pitch offset value from knob.", "(-1 to 1)", BindingValueUnits.Numeric);
            _pitchAdjustment.Execute += new HeliosActionHandler(PitchAdjsut_Execute);
            Actions.Add(_pitchAdjustment);

            _offFlag = new HeliosValue(this, new BindingValue(false), "", "Off Flag", "Indicates whether the off flag is displayed.", "True if displayed.", BindingValueUnits.Boolean);
            _offFlag.Execute += new HeliosActionHandler(OffFlag_Execute);
            Actions.Add(_offFlag);
        }

        void Pitch_Execute(object action, HeliosActionEventArgs e)
        {
            _pitch.SetValue(e.Value, e.BypassCascadingTriggers);
            _ball.VerticalOffset = _pitchCalibration.Interpolate(e.Value.DoubleValue) / 2;
        }

        void PitchAdjsut_Execute(object action, HeliosActionEventArgs e)
        {
            _pitchAdjustment.SetValue(e.Value, e.BypassCascadingTriggers);

            _wings.VerticalOffset = _pitchAdjustCalibaration.Interpolate(e.Value.DoubleValue);
        }

        void Bank_Execute(object action, HeliosActionEventArgs e)
        {
            _roll.SetValue(e.Value, e.BypassCascadingTriggers);
            _ball.Rotation = e.Value.DoubleValue / 2;
            _bankNeedle.Rotation = e.Value.DoubleValue / 2;
        }

        void OffFlag_Execute(object action, HeliosActionEventArgs e)
        {
            _offFlag.SetValue(e.Value, e.BypassCascadingTriggers);
            _offFlagNeedle.Rotation = e.Value.BoolValue ? 45 : 0;
        }
    }

}
