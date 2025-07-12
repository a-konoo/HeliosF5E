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

    // Annotation Indicating the unique name for this control, display, toolbox group, and control renderer.
    [HeliosControl("Helios.F5E.SlipGauge", "SlipGauge", "F-5E Gauges", typeof(GaugeRenderer), HeliosControlFlags.NotShownInUI)]
    public class SlipGauge : BaseGauge
    {
        private HeliosValue _slipball;
        private GaugeNeedle _slipBallNeedle;
        private static CalibrationPointCollectionDouble _slipBallCalibration;
        public SlipGauge()
            : base("Flight Instuments", new Size(110d, 22d))
        {
            Point center = new Point(55d, 11d);


            Components.Add(new GaugeImage("{F-5E}/Gauges/Slipball/slipgauge.xaml",
                new Rect(0d, 0d, 110d, 22d)));


            _slipBallNeedle = new GaugeNeedle("{F-5E}/Gauges/Slipball/slipball.xaml", new Point(52.5d, 8d), new Size(16d, 16d), new Point(5d, 5d));
            Components.Add(_slipBallNeedle);

            _slipball = new HeliosValue(this, new BindingValue(0d), "", 
                "Slip Ball Offset",
                "Side slip indicator offset from the center of the tube.", 
                "(-1 to 1) -1 full left and 1 is full right.", BindingValueUnits.Numeric);
            _slipball.Execute += new HeliosActionHandler(SlipBall_Execute);
            Actions.Add(_slipball);
        }

        public SlipGauge(string name, Point posn, Size size)
            : base(name, new Size(size.Width, size.Height))
        {
            Left = posn.X;
            Top = posn.Y;

            var ballInitPos = Convert.ToInt32(size.Width * 0.5d -2.5d);

            Components.Add(new GaugeImage("{F-5E}/Gauges/Slipball/slipgauge.xaml",
                new Rect(0d, 0d, size.Width, size.Height)));

            _slipBallCalibration = new CalibrationPointCollectionDouble(-1d, 1d, -50d, 50d);
            _slipBallNeedle = new GaugeNeedle("{F-5E}/Gauges/Slipball/slipball.xaml", new Point(ballInitPos, 8d), new Size(16d, 16d), new Point(5d, 5d));
            Components.Add(_slipBallNeedle);

            _slipball = new HeliosValue(this, new BindingValue(0d), "",
                "Slip Ball Offset",
                "Side slip indicator offset from the center of the tube.",
                "(-1 to 1) -1 full left and 1 is full right.", BindingValueUnits.Numeric);
            _slipball.Execute += new HeliosActionHandler(SlipBall_Execute);
            Actions.Add(_slipball);
        }

        // Event callback for angle updates
        void SlipBall_Execute(object action, HeliosActionEventArgs e)
        {
            _slipBallNeedle.HorizontalOffset = -1 * (e.Value.DoubleValue* 40);
        }
    }
}
