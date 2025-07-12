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
using NLog;

namespace GadrocsWorkshop.Helios.Gauges.F5E
{
    using GadrocsWorkshop.Helios.ComponentModel;
    using System;
    using System.Collections.Generic;
    using System.Windows;
    using System.Windows.Media;
    using System.Windows.Threading;


    // Annotation Indicating the unique name for this control, display, toolbox group, and control renderer.
    [HeliosControl("Helios.F5E.FlapIndicator", "FlapIndicator", "F-5E Gauges", typeof(GaugeRenderer))]
    public class FlapIndicator : BaseGauge
    {

        private static readonly NLog.Logger Logger = NLog.LogManager.GetCurrentClassLogger();

        private HeliosValue _flapIndex;

        private GaugeDrumCounter _fullcardNeedle;


        public FlapIndicator()
            : base("Flap Indicator", new Size(113d, 133d))
        {
            _fullcardNeedle = new GaugeDrumCounter("{F-5E}/Gauges/FlapIndicator/indicator_card.xaml", new Point(13d, 27d),
                "#", new Size(20d, 27d), new Size(30d, 27d));
            _fullcardNeedle.Clip = new RectangleGeometry(new Rect(13d, 27d, 90d, 54d));
            Components.Add(_fullcardNeedle);
            Components.Add(new GaugeImage("{F-5E}/Gauges/FlapIndicator/LeftFlapGauge.png", new Rect(0d, 0d, 113d, 113d)));

            _flapIndex = new HeliosValue(this, new BindingValue(0d), "", "flap index", "flap index", "", BindingValueUnits.Degrees);
            _flapIndex.Execute += new HeliosActionHandler(FlapStatusChange_Execute);
            Actions.Add(_flapIndex);
            Values.Add(_flapIndex);
        }

        public override void Reset()
        {
            base.Reset();
        }


        private void FlapStatusChange_Execute(object action, HeliosActionEventArgs e)
        {
            double p = e.Value.DoubleValue * 0.083;
            _fullcardNeedle.Value = (p * 10) -1;

        }
    }
}