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
    using System.Windows;
    using System.Windows.Media;

    [HeliosControl("Helios.F5E.ADI", "ADI", "F-5E Gauges", typeof(GaugeRenderer))]
    public class ADI : BaseGauge
    {
        private HeliosValue _pitch;
        private HeliosValue _roll;

        private HeliosValue _offFlag;

        private GaugeImage _offFlagImage;

        private GaugeNeedle _ball;
        private GaugeNeedle _bankNeedle;

        private CalibrationPointCollectionDouble _pitchCalibration;

        public ADI()
            : base("ADI", new Size(350, 350))
        {
            Point center = new Point(175d, 205d);
            Point center2 = new Point(175d, 179d);
            Point center3 = new Point(175d, 179d);
            _pitchCalibration = new CalibrationPointCollectionDouble(-360d, -1066d, 360d, 1066d);
            _ball = new GaugeNeedle("{Helios}/Gauges/F-5E/ADI/adi_ball.xaml", center2, new Size(250d, 1350d), new Point(125d, 677d));
            _ball.Clip = new EllipseGeometry(center3, 130d, 130d);
            Components.Add(_ball);

            Components.Add(new GaugeImage("{Helios}/Gauges/F-5E/ADI/ADIGaugeCover.png", new Rect(0d, 0d, 350d, 350d)));

            Components.Add(new GaugeImage("{Helios}/Gauges/F-5E/ADI/ADIGaugeGrass.png", new Rect(0d, 0d, 350d, 350d)));

            Components.Add(new GaugeImage("{Helios}/Gauges/F-5E/ADI/adi_bank_indicates.xaml", new Rect(30d, 30d, 290d, 290d)));


            Components.Add(new GaugeImage("{Helios}/Gauges/F-5E/ADI/adi_wing.xaml", new Rect(57d, 112d, 240d, 135d)));

            _offFlagImage = new GaugeImage("{Helios}/Gauges/F-5E/ADI/adi_off_flag.xaml", new Rect(58d, 210d, 55d, 56d));
            _offFlagImage.IsHidden = true;
            Components.Add(_offFlagImage);

            _bankNeedle = new GaugeNeedle("{Helios}/Gauges/F-5E/ADI/adi_bank_pointer.xaml", center2, new Size(18d, 250d), new Point(9d, 125d));
            Components.Add(_bankNeedle);

            _offFlag = new HeliosValue(this, new BindingValue(false), "", "Off Flag", "Indicates whether the off flag is displayed.", "True if displayed.", BindingValueUnits.Boolean);
            _offFlag.Execute += new HeliosActionHandler(OffFlag_Execute);
            Actions.Add(_offFlag);

            _pitch = new HeliosValue(this, new BindingValue(0d), "", "Pitch", "Current ptich of the aircraft.", "(0 - 360)", BindingValueUnits.Degrees);
            _pitch.Execute += new HeliosActionHandler(Pitch_Execute);
            Actions.Add(_pitch);

            _roll = new HeliosValue(this, new BindingValue(0d), "", "Bank", "Current bank of the aircraft.", "(0 - 360)", BindingValueUnits.Degrees);
            _roll.Execute += new HeliosActionHandler(Bank_Execute);
            Actions.Add(_roll);


        }

        void OffFlag_Execute(object action, HeliosActionEventArgs e)
        {
            _offFlag.SetValue(e.Value, e.BypassCascadingTriggers);
            _offFlagImage.IsHidden = !e.Value.BoolValue;
        }

        void Pitch_Execute(object action, HeliosActionEventArgs e)
        {
            _pitch.SetValue(e.Value, e.BypassCascadingTriggers);
            _ball.VerticalOffset = _pitchCalibration.Interpolate(e.Value.DoubleValue) / 2;
        }

        void Bank_Execute(object action, HeliosActionEventArgs e)
        {
            _roll.SetValue(e.Value, e.BypassCascadingTriggers);
            _ball.Rotation = e.Value.DoubleValue / 2;
            _bankNeedle.Rotation = e.Value.DoubleValue / 2;
        }
    }
}
