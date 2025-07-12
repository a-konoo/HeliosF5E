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

namespace GadrocsWorkshop.Helios.Gauges.F5E
{
    using GadrocsWorkshop.Helios.ComponentModel;
    using System;
    using System.Windows;
    using System.Windows.Media;

    [HeliosControl("Helios.F5E.SAI", "Standby Attitude Indicator", "F-5E Gauges", typeof(GaugeRenderer))]
    public class SAIGauge : BaseGauge
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

        public SAIGauge()
            : base("Standby Attitude Indicator", new Size(200, 200))
        {
            SetupSAIParameters();
        }
        // 0.57
        public SAIGauge(string name, Point posn)
            : base(name, new Size(200, 200))
        {
            Left = posn.X;
            Top = posn.Y;
            SetupSAIParameters();
        }

        private void SetupSAIParameters()
        {
            Point center = new Point(165d * 0.57, 205d * 0.57);
            Point center2 = new Point(165d * 0.57, 179d * 0.57);
            Point center3 = new Point(165d * 0.57, 189d * 0.57);
            Point wingCenter = new Point(170d * 0.57, 185d * 0.57);
            _pitchCalibration = new CalibrationPointCollectionDouble(-360d * 0.57, -1066d * 0.57, 360d * 0.57, 1066d * 0.57);
            _ball = new GaugeNeedle("{F-5E}/Gauges/ADI/adi_backup_ball.xaml", center2, new Size(300d * 0.57, 1350d * 0.57),
                new Point(150d * 0.57, 677d * 0.57));
            _ball.Clip = new EllipseGeometry(center3, 150d * 0.57, 135d * 0.57);
            Components.Add(_ball);

            _offFlagNeedle = new GaugeNeedle("{F-5E}/Gauges/ADI/adi_backup_warning_flag.xaml", new Point(29d * 0.57, 226d * 0.57),
                new Size(31d * 0.57, 127d * 0.57), new Point(0d * 0.57, 127d * 0.57));
            Components.Add(_offFlagNeedle);

            Components.Add(new GaugeImage("{F-5E}/Gauges/ADI/SAIGaugeCover.png", new Rect(0d, 0d, 200d, 200d)));

            Components.Add(new GaugeImage("{F-5E}/Gauges/ADI/ADIGaugeGrass.png", new Rect(0d, 0d, 200d, 200d)));

            Components.Add(new GaugeImage("{F-5E}/Gauges/ADI/adi_bank_indicates.xaml", new Rect(30d * 0.57, 30d * 0.57, 270d * 0.57, 290d * 0.57)));

            _wings = new GaugeNeedle("{F-5E}/Gauges/ADI/sai_wing.xaml", wingCenter, new Size(130d * 0.57, 26d * 0.57),
                new Point(65d * 0.57, 13d * 0.57));
            Components.Add(_wings);

            _bankNeedle = new GaugeNeedle("{F-5E}/Gauges/ADI/adi_bank_pointer.xaml", center2, new Size(32d * 0.57, 235d * 0.57),
                new Point(16d * 0.57, 117.5d * 0.57));
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
            _ball.VerticalOffset = _pitchCalibration.Interpolate(e.Value.DoubleValue) * 0.25;
        }

        void PitchAdjsut_Execute(object action, HeliosActionEventArgs e)
        {
            _pitchAdjustment.SetValue(e.Value, e.BypassCascadingTriggers);

            _wings.VerticalOffset = _pitchAdjustCalibaration.Interpolate(e.Value.DoubleValue);
        }

        void Bank_Execute(object action, HeliosActionEventArgs e)
        {
            _roll.SetValue(e.Value, e.BypassCascadingTriggers);
            _ball.Rotation = e.Value.DoubleValue * 0.5;
            _bankNeedle.Rotation = e.Value.DoubleValue * 0.25;
        }

        void OffFlag_Execute(object action, HeliosActionEventArgs e)
        {
            _offFlag.SetValue(e.Value, e.BypassCascadingTriggers);
            _offFlagNeedle.Rotation = e.Value.BoolValue ? 45 : 0;
        }
    }

}
