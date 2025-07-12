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
    using System.Collections.Generic;
    using System.Linq;
    using System.Windows;
    using System.Windows.Media;

    [HeliosControl("Helios.F5E.DrumCounter", "DrumCounter", "F-5E Gauges", typeof(GaugeRenderer))]
    public class DrumCounter : BaseGauge
    {
        private HeliosValue _count;
        private GaugeDrumCounter[] _Drums = new GaugeDrumCounter[4];
        private Action<Double, GaugeDrumCounter[]> gaugeFunc;
        public DrumCounter()
            : base("DrumCounter", new Size(150, 100))
        {

            Components.Add(new GaugeImage("{F-5E}/Gauges/TenOneCounter/counter_faceplate.xaml", new Rect(0d, 0d, 150d, 100d)));

            var _tensDrum = new GaugeDrumCounter("{F-5E}/Gauges/TenOneCounter/drum_zero_tape.xaml",
                new Point(15.5d, 11.5d), "#", new Size(10d, 15d), new Size(50d, 75d));
            _Drums[0] = _tensDrum;
            _tensDrum.Clip = new RectangleGeometry(new Rect(15.5d, 11.5d, 50d, 75d));
            Components.Add(_tensDrum);

            var _onesDrum = new GaugeDrumCounter("{F-5E}/Gauges/TenOneCounter/drum_zero_tape.xaml",
                new Point(82.5d, 11.5d), "#", new Size(10d, 15d), new Size(50d, 75d));
            _Drums[1] = _onesDrum;
            _onesDrum.Clip = new RectangleGeometry(new Rect(82.5d, 11.5d, 50d, 75d));
            Components.Add(_onesDrum);


            _count = new HeliosValue(this, new BindingValue(String.Empty), "", "count", "TenOneCounter", "Use format '###.##' ", BindingValueUnits.Numeric);
            _count.Execute += new HeliosActionHandler(DrumCounter_Execute);
            Actions.Add(_count);

            gaugeFunc = (p, d) =>
            {
                d[0].Value = Math.Floor(p / 10d);
                p -= Math.Floor(d[0].Value * 10d);
                d[1].Value = p;
            };

        }

        public DrumCounter(string name, Point posn, Size size, Point[] displayPosn, String[] gaugePath, Size digitSize, Size displaySize,
            Action<Double, GaugeDrumCounter[]> argFunc)
            : base(name, new Size(size.Width, size.Height))
        {
            Left = posn.X;
            Top = posn.Y;
            for(var i=0; i< displayPosn.Length; i++)
            {
                var tape = gaugePath[i];
                var drum = new GaugeDrumCounter(tape,
                     new Point(displayPosn[i].X, displayPosn[i].Y), "#", new Size(digitSize.Width, digitSize.Height),
                     new Size(displaySize.Width, displaySize.Height));
                _Drums[i] = drum;
                drum.Clip = new RectangleGeometry(new Rect(displayPosn[i].X, displayPosn[i].Y, displaySize.Width, displaySize.Height));
                Components.Add(drum);
            }
            this.gaugeFunc = argFunc;
            _count = new HeliosValue(this, new BindingValue(String.Empty), "", "count", "TenOneCounter", "Use format '###.##' ", BindingValueUnits.Numeric);
            _count.Execute += new HeliosActionHandler(DrumCounter_Execute);
            Actions.Add(_count);
        }


        void DrumCounter_Execute(object action, HeliosActionEventArgs e)
        {
            double p = e.Value.DoubleValue;
            if (gaugeFunc != null)
            {
                gaugeFunc(p, this._Drums);
            }
        }
    }
}
