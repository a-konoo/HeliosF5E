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

namespace GadrocsWorkshop.Helios.Gauges.F5E.EngineRPM
{
    using GadrocsWorkshop.Helios.ComponentModel;
    using System.Windows;

    [HeliosControl("Helios.F5E.EngineRPM", "Engine RPM", "F-5E Gauges", typeof(GaugeRenderer))]
    public class EngineRPM : BaseGauge
    {
        private HeliosValue _rpmMainGauge;
        private HeliosValue _rpmSubGauge;
        private GaugeNeedle _needle;
        private GaugeNeedle _smallNeedle;
        private CalibrationPointCollectionDouble _needleCalibration;
        private CalibrationPointCollectionDouble _smallNeedleCalibration;

        public EngineRPM()
            : base("Engine RPM", new Size(170d, 170d))
        {
            // 160->170,160->170
            Point center = new Point(85d, 85d);
            // 80->85,80->85
            Point center2 = new Point(50d, 45d);
            // 45->50,40->45
            // 0d->5d,0d->5d,160d,160d
            Components.Add(new GaugeImage("{Helios}/Gauges/F-5E/EngineRPM/eng_rpm_faceplate.xaml", new Rect(5d, 5d, 160d, 160d)));
            _needleCalibration = new CalibrationPointCollectionDouble(0d, 0d, 100d, 270d)
            {
                new CalibrationPointDouble(0d, 2d),
                new CalibrationPointDouble(50d, 150d),
                new CalibrationPointDouble(55d, 170d),
                new CalibrationPointDouble(80d, 260d)
            };

            _needle = new GaugeNeedle("{Helios}/Gauges/F-5E/Common/needle_general.xaml", center, new Size(48d, 77d), new Point(24d, 63d), 2d);
            Components.Add(_needle);

            _smallNeedleCalibration = new CalibrationPointCollectionDouble(0d, 0d, 100d, 360d);
            _smallNeedle = new GaugeNeedle("{Helios}/Gauges/F-5E/Common/eng_small_needle.xaml", center2, new Size(15d, 40d), new Point(7.5d, 20d), 0d);
            Components.Add(_smallNeedle);

            _rpmMainGauge = new HeliosValue(this, new BindingValue(0d), "", "main gauge rpm percent", "Current MainGauge of the aircraft Enigne RPM Percent.", "(0 - 100)", BindingValueUnits.RPMPercent);
            _rpmMainGauge.Execute += new HeliosActionHandler(RPM_Main_Execute);
            Actions.Add(_rpmMainGauge);

            _rpmSubGauge = new HeliosValue(this, new BindingValue(0d), "", "sub gauge rpm percent", "Current SubGauge of the aircraft Enigne RPM Percent.", "(0 - 100)", BindingValueUnits.RPMPercent);
            _rpmSubGauge.Execute += new HeliosActionHandler(RPM_Sub_Execute);
            Actions.Add(_rpmSubGauge);

            Components.Add(new GaugeImage("{Helios}/Gauges/F-5E/EngineRPM/EngineRPMRing.png", new Rect(0d, 0d, 170d, 170d)));
        }

        void RPM_Main_Execute(object action, HeliosActionEventArgs e)
        {
            _needle.Rotation = _needleCalibration.Interpolate(e.Value.DoubleValue);
        }
        void RPM_Sub_Execute(object action, HeliosActionEventArgs e)
        {
            _smallNeedle.Rotation = _smallNeedleCalibration.Interpolate(e.Value.DoubleValue);
        }
    }
}

