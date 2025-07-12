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
    using System.Windows;

    [HeliosControl("Helios.F5E.RaderPanelBody", "RaderPanelBody", "F-5E Gauges", typeof(GaugeRenderer))]
    public class RaderPanelBody : BaseGauge
    {
        private HeliosValue _scale5Flag;
        private HeliosValue _scale10Flag;
        private HeliosValue _scale20Flag;
        private HeliosValue _scale40Flag;

        private HeliosValue _inRangeFlag;
        private HeliosValue _failFlag;
        private HeliosValue _lockOnFlag;
        private HeliosValue _excessGravityFlag;

        private GaugeImage _range40off;
        private GaugeImage _range40on;
        private GaugeImage _range20off;
        private GaugeImage _range20on;
        private GaugeImage _range10off;
        private GaugeImage _range10on;
        private GaugeImage _range5off;
        private GaugeImage _range5on;
        private GaugeImage _inrangeoff;
        private GaugeImage _inrangeon;
        private GaugeImage _failoff;
        private GaugeImage _failon;
        private GaugeImage _excessgoff;
        private GaugeImage _excessgon;
        private GaugeImage _lockabboff;
        private GaugeImage _lockabbon;


    public RaderPanelBody(string name, Point posn, Size size)
    : base(name, new Size(size.Width, size.Height))
        {
            Left = posn.X;
            Top = posn.Y;

            Components.Add(new GaugeImage("{F-5E}/Images/RaderPanel/RaderPanel.png", new Rect(
                0d, 0d, size.Width, size.Height)));

            _range40off = new GaugeImage("{F-5E}/Gauges/RaderPanel/range40off.xaml", new Rect(
                size.Width * 0.044d,
                size.Height * 0.344d, 
                0.033d * size.Width,
                0.033d * size.Width));
            _range40on = new GaugeImage("{F-5E}/Gauges/RaderPanel/range40on.xaml", new Rect(
                size.Width * 0.044d,
                size.Height * 0.344d,
                0.033d * size.Width,
                0.033d * size.Width));
            _range40on.IsHidden = true;

            Components.Add(_range40off);
            Components.Add(_range40on);

            _range20off = new GaugeImage("{F-5E}/Gauges/RaderPanel/range20off.xaml", new Rect(
                size.Width * 0.04d, 
                size.Height * 0.4816d,
                0.033d * size.Width,
                0.033d * size.Width));
            _range20on = new GaugeImage("{F-5E}/Gauges/RaderPanel/range20on.xaml", new Rect(
                size.Width * 0.04d,
                size.Height * 0.4816d,
                0.033d * size.Width,
                0.033d * size.Width));
            _range20on.IsHidden = true;

            Components.Add(_range20off);
            Components.Add(_range20on);

            _range10off = new GaugeImage("{F-5E}/Gauges/RaderPanel/range10off.xaml", new Rect(
                size.Width * 0.0356d,
                size.Height * 0.5734d,
                0.033d * size.Width,
                0.033d * size.Width));
            _range10on = new GaugeImage("{F-5E}/Gauges/RaderPanel/range10on.xaml", new Rect(
                size.Width * 0.0356d,
                size.Height * 0.5734d,
                0.033d * size.Width,
                0.033d * size.Width));
            _range10on.IsHidden = true;

            Components.Add(_range10off);
            Components.Add(_range10on);

            _range5off = new GaugeImage("{F-5E}/Gauges/RaderPanel/range5off.xaml", new Rect(
                size.Width * 0.0356d,
                size.Height * 0.665d,
                0.033d * size.Width,
                0.033d * size.Width));
            _range5on = new GaugeImage("{F-5E}/Gauges/RaderPanel/range5on.xaml", new Rect(
                size.Width * 0.0356d,
                size.Height * 0.665d,
                0.033d * size.Width,
                0.033d * size.Width));
            _range5on.IsHidden = true;

            Components.Add(_range5off);
            Components.Add(_range5on);

            _inrangeoff = new GaugeImage("{F-5E}/Gauges/RaderPanel/inrangeoff.xaml", new Rect(
                size.Width * 0.555d,
                size.Height * 0.068d,
                size.Width * 0.377d,
                size.Height * 0.114d));
            _inrangeon = new GaugeImage("{F-5E}/Gauges/RaderPanel/inrangeon.xaml", new Rect(
                size.Width * 0.555d,
                size.Height * 0.068d,
                size.Width * 0.377d,
                size.Height * 0.114d));
            _inrangeon.IsHidden = true;

            Components.Add(_inrangeoff);
            Components.Add(_inrangeon);

            _failoff = new GaugeImage("{F-5E}/Gauges/RaderPanel/failoff.xaml", new Rect(
                size.Width * 0.822d,
                size.Height * 0.252d,
                size.Width * 0.288d,
                size.Height * 0.161d));
            _failon = new GaugeImage("{F-5E}/Gauges/RaderPanel/failon.xaml", new Rect(
                size.Width * 0.822d,
                size.Height * 0.252d,
                size.Width * 0.288d,
                size.Height * 0.161d));
            _failon.IsHidden = true;

            Components.Add(_failon);
            Components.Add(_failoff);

            _excessgoff = new GaugeImage("{F-5E}/Gauges/RaderPanel/excessgoff.xaml", new Rect(
                size.Width * 0.8d,
                size.Height * 0.596d,
                size.Width * 0.288d,
                size.Height * 0.161d));
            _excessgon = new GaugeImage("{F-5E}/Gauges/RaderPanel/excessgon.xaml", new Rect(
                size.Width * 0.8d,
                size.Height * 0.596d,
                size.Width * 0.288d,
                size.Height * 0.161d));
            _excessgon.IsHidden = true;

            Components.Add(_excessgon);
            Components.Add(_excessgoff);


            _lockabboff = new GaugeImage("{F-5E}/Gauges/RaderPanel/lockabboff.xaml", new Rect(
                size.Width * 0.888d,
                size.Height * 0.39d,
                size.Width * 0.088d,
                size.Height * 0.206d));
            _lockabbon = new GaugeImage("{F-5E}/Gauges/RaderPanel/lockabbon.xaml", new Rect(
                size.Width * 0.888d,
                size.Height * 0.39d,
                size.Width * 0.088d,
                size.Height * 0.206d));
            _lockabbon.IsHidden = true;

            Components.Add(_lockabbon);
            Components.Add(_lockabboff);


            _scale5Flag = new HeliosValue(this, new BindingValue(0d), "", "range 5 Light Flag", "range 5 Flag Light on/off.", "", BindingValueUnits.Boolean);
            _scale5Flag.Execute += new HeliosActionHandler(RangeFlag5_Execute);
            Actions.Add(_scale5Flag);

            _scale10Flag = new HeliosValue(this, new BindingValue(0d), "", "range 10 Light Flag", "range 10 Flag Light on/off.", "", BindingValueUnits.Boolean);
            _scale10Flag.Execute += new HeliosActionHandler(RangeFlag10_Execute);
            Actions.Add(_scale10Flag);

            _scale20Flag = new HeliosValue(this, new BindingValue(0d), "", "range 20 Light Flag", "range 20 Flag Light on/off.", "", BindingValueUnits.Boolean);
            _scale20Flag.Execute += new HeliosActionHandler(RangeFlag20_Execute);
            Actions.Add(_scale20Flag);

            _scale40Flag = new HeliosValue(this, new BindingValue(0d), "", "range 40 Light Flag", "range 40 Flag Light on/off.", "", BindingValueUnits.Boolean);
            _scale40Flag.Execute += new HeliosActionHandler(RangeFlag40_Execute);
            Actions.Add(_scale40Flag);

            _inRangeFlag = new HeliosValue(this, new BindingValue(0d), "", "in Range Light Flag", "in Range Light on/off.", "", BindingValueUnits.Boolean);
            _inRangeFlag.Execute += new HeliosActionHandler(InRangeFlag_Execute);
            Actions.Add(_inRangeFlag);

            _failFlag = new HeliosValue(this, new BindingValue(0d), "", "Fail Light Flag", "Fail Light on/off.", "", BindingValueUnits.Boolean);
            _failFlag.Execute += new HeliosActionHandler(FailFlag_Execute);
            Actions.Add(_failFlag);

            _lockOnFlag = new HeliosValue(this, new BindingValue(0d), "", "LockOn Light Flag", "LockOn Light on/off.", "", BindingValueUnits.Boolean);
            _lockOnFlag.Execute += new HeliosActionHandler(LockOnFlag_Execute);
            Actions.Add(_lockOnFlag);

            _excessGravityFlag = new HeliosValue(this, new BindingValue(0d), "", "ExcessG Light Flag", "ExcessG Light on/off.", "", BindingValueUnits.Boolean);
            _excessGravityFlag.Execute += new HeliosActionHandler(ExcessGravityFlag_Execute);
            Actions.Add(_excessGravityFlag);
        }
        public RaderPanelBody()
            : base("RaderPanel", new Size(450, 436))
        {

            Components.Add(new GaugeImage("{F-5E}/Images/RaderPanel/RaderPanel.png", new Rect(0d, 0d, 450d, 436d)));

            _range40off = new GaugeImage("{F-5E}/Gauges/RaderPanel/range40off.xaml", new Rect(20d, 170d, 15d, 15d));
            _range40on = new GaugeImage("{F-5E}/Gauges/RaderPanel/range40on.xaml", new Rect(20d, 170d, 15d, 15d));
            _range40on.IsHidden = true;

            Components.Add(_range40off);
            Components.Add(_range40on);

            _range20off = new GaugeImage("{F-5E}/Gauges/RaderPanel/range20off.xaml", new Rect(18d, 210d, 15d, 15d));
            _range20on = new GaugeImage("{F-5E}/Gauges/RaderPanel/range20on.xaml", new Rect(18d, 210d, 15d, 15d));
            _range20on.IsHidden = true;

            Components.Add(_range20off);
            Components.Add(_range20on);

            _range10off = new GaugeImage("{F-5E}/Gauges/RaderPanel/range10off.xaml", new Rect(16d, 250d, 15d, 15d));
            _range10on = new GaugeImage("{F-5E}/Gauges/RaderPanel/range10on.xaml", new Rect(16d, 250d, 15d, 15d));
            _range10on.IsHidden = true;

            Components.Add(_range10off);
            Components.Add(_range10on);

            _range5off = new GaugeImage("{F-5E}/Gauges/RaderPanel/range5off.xaml", new Rect(18d, 290d, 15d, 15d));
            _range5on = new GaugeImage("{F-5E}/Gauges/RaderPanel/range5on.xaml", new Rect(18d, 290d, 15d, 15d));
            _range5on.IsHidden = true;

            Components.Add(_range5off);
            Components.Add(_range5on);

            _inrangeoff = new GaugeImage("{F-5E}/Gauges/RaderPanel/inrangeoff.xaml", new Rect(250d, 30d, 170d, 50d));
            _inrangeon = new GaugeImage("{F-5E}/Gauges/RaderPanel/inrangeon.xaml", new Rect(250d, 30d, 170d, 50d));
            _inrangeon.IsHidden = true;

            Components.Add(_inrangeoff);
            Components.Add(_inrangeon);

            _failoff = new GaugeImage("{F-5E}/Gauges/RaderPanel/failoff.xaml", new Rect(370d, 110d, 130d, 70d));
            _failon = new GaugeImage("{F-5E}/Gauges/RaderPanel/failon.xaml", new Rect(370d, 110d, 130d, 70d));
            _failon.IsHidden = true;

            Components.Add(_failon);
            Components.Add(_failoff);

            _excessgoff = new GaugeImage("{F-5E}/Gauges/RaderPanel/excessgoff.xaml", new Rect(360d, 260d, 130d, 70d));
            _excessgon = new GaugeImage("{F-5E}/Gauges/RaderPanel/excessgon.xaml", new Rect(360d, 260d, 130d, 70d));
            _excessgon.IsHidden = true;

            Components.Add(_excessgon);
            Components.Add(_excessgoff);


            _lockabboff = new GaugeImage("{F-5E}/Gauges/RaderPanel/lockabboff.xaml", new Rect(400d, 170d, 40d, 90d));
            _lockabbon = new GaugeImage("{F-5E}/Gauges/RaderPanel/lockabbon.xaml", new Rect(400d, 170d, 40d, 90d));
            _lockabbon.IsHidden = true;

            Components.Add(_lockabbon);
            Components.Add(_lockabboff);


            _scale5Flag = new HeliosValue(this, new BindingValue(0d), "", "range 5 Light Flag", "range 5 Flag Light on/off.", "", BindingValueUnits.Boolean);
            _scale5Flag.Execute += new HeliosActionHandler(RangeFlag5_Execute);
            Actions.Add(_scale5Flag);

            _scale10Flag = new HeliosValue(this, new BindingValue(0d), "", "range 10 Light Flag", "range 10 Flag Light on/off.", "", BindingValueUnits.Boolean);
            _scale10Flag.Execute += new HeliosActionHandler(RangeFlag10_Execute);
            Actions.Add(_scale10Flag);

            _scale20Flag = new HeliosValue(this, new BindingValue(0d), "", "range 20 Light Flag", "range 20 Flag Light on/off.", "", BindingValueUnits.Boolean);
            _scale20Flag.Execute += new HeliosActionHandler(RangeFlag20_Execute);
            Actions.Add(_scale20Flag);

            _scale40Flag = new HeliosValue(this, new BindingValue(0d), "", "range 40 Light Flag", "range 40 Flag Light on/off.", "", BindingValueUnits.Boolean);
            _scale40Flag.Execute += new HeliosActionHandler(RangeFlag40_Execute);
            Actions.Add(_scale40Flag);

            _inRangeFlag = new HeliosValue(this, new BindingValue(0d), "", "in Range Light Flag", "in Range Light on/off.", "", BindingValueUnits.Boolean);
            _inRangeFlag.Execute += new HeliosActionHandler(InRangeFlag_Execute);
            Actions.Add(_inRangeFlag);

            _failFlag = new HeliosValue(this, new BindingValue(0d), "", "Fail Light Flag", "Fail Light on/off.", "", BindingValueUnits.Boolean);
            _failFlag.Execute += new HeliosActionHandler(FailFlag_Execute);
            Actions.Add(_failFlag);

            _lockOnFlag = new HeliosValue(this, new BindingValue(0d), "", "LockOn Light Flag", "LockOn Light on/off.", "", BindingValueUnits.Boolean);
            _lockOnFlag.Execute += new HeliosActionHandler(LockOnFlag_Execute);
            Actions.Add(_lockOnFlag);

            _excessGravityFlag = new HeliosValue(this, new BindingValue(0d), "", "ExcessG Light Flag", "ExcessG Light on/off.", "", BindingValueUnits.Boolean);
            _excessGravityFlag.Execute += new HeliosActionHandler(ExcessGravityFlag_Execute);
            Actions.Add(_excessGravityFlag);
        }

        void RangeFlag5_Execute(object action, HeliosActionEventArgs e)
        {

        }
        void RangeFlag10_Execute(object action, HeliosActionEventArgs e)
        {

        }
        void RangeFlag20_Execute(object action, HeliosActionEventArgs e)
        {

        }
        void RangeFlag40_Execute(object action, HeliosActionEventArgs e)
        {

        }
        void InRangeFlag_Execute(object action, HeliosActionEventArgs e)
        {

        }
        void FailFlag_Execute(object action, HeliosActionEventArgs e)
        {

        }
        void LockOnFlag_Execute(object action, HeliosActionEventArgs e)
        {

        }

        void ExcessGravityFlag_Execute(object action, HeliosActionEventArgs e)
        {

        }
    }
}
