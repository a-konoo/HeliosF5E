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

namespace GadrocsWorkshop.Helios.Gauges.F5E.HSI
{
    using GadrocsWorkshop.Helios.ComponentModel;
    using System;
    using System.Threading;
    using System.Timers;
    using System.Windows;
    using System.Windows.Media;
    using System.Windows.Threading;

    [HeliosControl("Helios.F5E.HSI", "HSI", "F-5E Gauges", typeof(GaugeRenderer))]
    public class HSI : BaseGauge
    {
        private HeliosValue _bearing;
        private HeliosValue _desiredHeading;
        private HeliosValue _desiredCourse;
        private HeliosValue _currentHeading;
        private HeliosValue _distance;
        private HeliosValue _offFlag;
        private HeliosValue _tacanDfValue;
        private HeliosValue _toFlag;
        private HeliosValue _fromFlag;
        // Demo Action
        private HeliosValue _demoFlag;
        private DispatcherTimer _demoTimer;
        private int _workBearing = 0;
        
        private GaugeImage _offFlagImage;
        private GaugeNeedle _courseFlagNeedle;
        private GaugeNeedle _toFlagNeedle;
        private GaugeNeedle _fromFlagNeedle;
        private GaugeNeedle _dfModeImage;
        private GaugeImage _invalidDistImage;
        private GaugeNeedle _deviationNeedle;
        private GaugeNeedle _deviationCard;
        private GaugeNeedle _desiredCourseNeedle;
        private GaugeNeedle _headingBug;
        private GaugeNeedle _compassNeedle;
        private GaugeDrumCounter _milesDrum;
        private GaugeDrumCounter _courseDrum;
        private GaugeNeedle _bearingNeedle;

        private CalibrationPointCollectionDouble _deviationCallibration;

        private bool _relativeDesiredHeading = true;
        private bool _relativeBearing = true;
        private bool _relativeCourse = true;

        public HSI()
            : base("HSI", new Size(350, 350))
        {
            Point center = new Point(173, 180);

            Components.Add(new GaugeRectangle(Colors.Black, new Rect(10, 10, 330, 330)));

            _milesDrum = new GaugeDrumCounter("{F-5E}/Gauges/Common/drum_tape.xaml", new Point(46d, 38d), "##%", new Size(10d, 15d), new Size(13d, 18d));
            _milesDrum.Clip = new RectangleGeometry(new Rect(46d, 38d, 39d, 30d));
            Components.Add(_milesDrum);

            _courseDrum = new GaugeDrumCounter("{F-5E}/Gauges/Common/drum_tape.xaml", new Point(266d, 38d), "##%", new Size(10d, 15d), new Size(13d, 18d));
            _courseDrum.Clip = new RectangleGeometry(new Rect(266d, 38d, 39d, 18d));
            Components.Add(_courseDrum);

            _compassNeedle = new GaugeNeedle("{F-5E}/Gauges/HSI/hsi_compass.xaml", center, new Size(243d, 243d), new Point(121.5, 121.5));
            Components.Add(_compassNeedle);

            _courseFlagNeedle = new GaugeNeedle("{F-5E}/Gauges/HSI/hsi_bearing_flag.xaml", center, new Size(350, 350), center);
            _courseFlagNeedle.IsHidden = true;
            Components.Add(_courseFlagNeedle);

            _invalidDistImage = new GaugeImage("{F-5E}/Gauges/HSI/hsi_invalid_dist.xaml", new Rect(44d, 36d, 50d, 16d));
            _invalidDistImage.IsHidden = true;
            Components.Add(_invalidDistImage);

            _dfModeImage = new GaugeNeedle("{F-5E}/Gauges/HSI/hsi_df_mode.xaml", center, new Size(350, 350), center);
            _dfModeImage.IsHidden = false;
            Components.Add(_dfModeImage);

            _toFlagNeedle = new GaugeNeedle("{F-5E}/Gauges/HSI/hsi_to_flag.xaml", center, new Size(350, 350), center);
            _toFlagNeedle.IsHidden = true;
            Components.Add(_toFlagNeedle);

            _fromFlagNeedle = new GaugeNeedle("{F-5E}/Gauges/HSI/hsi_from_flag.xaml", center, new Size(350, 350), center);
            _fromFlagNeedle.IsHidden = true;
            Components.Add(_fromFlagNeedle);

            _deviationCard = new GaugeNeedle("{F-5E}/Gauges/HSI/hsi_deviation_card.xaml", center, new Size(183, 183), new Point(91.5, 91.5));
            Components.Add(_deviationCard);

            _deviationNeedle = new GaugeNeedle("{F-5E}/Gauges/HSI/hsi_deviation_needle.xaml", center, new Size(6, 140), new Point(3, 70));
            Components.Add(_deviationNeedle);

            _headingBug = new GaugeNeedle("{F-5E}/Gauges/HSI/hsi_heading_bug.xaml", center, new Size(22, 9), new Point(11, 120));
            Components.Add(_headingBug);

            _desiredCourseNeedle = new GaugeNeedle("{F-5E}/Gauges/HSI/hsi_course_needle.xaml", center, new Size(12, 210), new Point(6, 105));
            Components.Add(_desiredCourseNeedle);



            Components.Add(new GaugeImage("{F-5E}/Gauges/HSI/HSIGaugeCover.png", new Rect(0, 0, 350, 350)));

            _bearingNeedle = new GaugeNeedle("{F-5E}/Gauges/HSI/hsi_bearing_needle.xaml", center, new Size(20, 265), new Point(10, 132.5));
            Components.Add(_bearingNeedle);

            _offFlagImage = new GaugeImage("{F-5E}/Gauges/HSI/hsi_off_flag.xaml", new Rect(266d, 116d, 77d, 131d));
            _offFlagImage.IsHidden = true;
            Components.Add(_offFlagImage);


            _offFlag = new HeliosValue(this, new BindingValue(false), "", "off flag", "off flag is displayed.", "True if displayed.", BindingValueUnits.Boolean);
            _offFlag.Execute += new HeliosActionHandler(OffFlag_Execute);
            Actions.Add(_offFlag);

            _tacanDfValue = new HeliosValue(this, new BindingValue(false), "", "TACAN/DF Value", "DF=1,TACAN=2", "if DF Selected, display DF plate.", BindingValueUnits.Numeric);
            _tacanDfValue.Execute += new HeliosActionHandler(TACANDFMode_Execute);
            Actions.Add(_tacanDfValue);

            _toFlag = new HeliosValue(this, new BindingValue(false), "", "to flag", "Indicates whether the to flag is displayed.", "True if displayed.", BindingValueUnits.Boolean);
            _toFlag.Execute += new HeliosActionHandler(ToFlag_Execute);
            Actions.Add(_toFlag);

            _fromFlag = new HeliosValue(this, new BindingValue(false), "", "from flag", "Indicates whether the from flag is displayed.", "True if displayed.", BindingValueUnits.Boolean);
            _fromFlag.Execute += new HeliosActionHandler(FromFlag_Execute);
            Actions.Add(_fromFlag);



            _distance = new HeliosValue(this, new BindingValue(0d), "", "distance to beacon", "Miles to the beacon.", "", BindingValueUnits.NauticalMiles);
            _distance.Execute += new HeliosActionHandler(Distance_Execute);
            Actions.Add(_distance);

            _bearing = new HeliosValue(this, new BindingValue(0d), "", "bearing", "bearing to the beacon.", "(0 - 360)", BindingValueUnits.Degrees);
            _bearing.Execute += new HeliosActionHandler(Bearing_Execute);
            Actions.Add(_bearing);

            _desiredHeading = new HeliosValue(this, new BindingValue(0d), "", "desired heading", "Current desired heading.", "(0 - 360)", BindingValueUnits.Degrees);
            _desiredHeading.Execute += new HeliosActionHandler(DesiredHeading_Execute);
            Actions.Add(_desiredHeading);

            _desiredCourse = new HeliosValue(this, new BindingValue(0d), "", "desired course", "Current desired course.", "(0 - 360)", BindingValueUnits.Degrees);
            _desiredCourse.Execute += new HeliosActionHandler(DesiredCourse_Execute);
            Actions.Add(_desiredCourse);

            _currentHeading = new HeliosValue(this, new BindingValue(0d), "", "heading", "Current heading.", "(0 - 360)", BindingValueUnits.Degrees);
            _currentHeading.Execute += new HeliosActionHandler(CurrentHeading_Execute);
            Actions.Add(_currentHeading);

            _deviationCallibration = new CalibrationPointCollectionDouble(-1d, -70d, 1d, 70d);
            // demoAction
            _demoFlag = new HeliosValue(this, new BindingValue(false), "", "demo flag", "demo action displayed.", "True if displayed.", BindingValueUnits.Boolean);
            _demoFlag.Execute += new HeliosActionHandler(DemoFlag_Execute);
            Actions.Add(_demoFlag);

        }

        #region Properties

        public bool IsDesiredHeadingRelative
        {
            get
            {
                return _relativeDesiredHeading;
            }
            set
            {
                if (!_relativeDesiredHeading.Equals(value))
                {
                    bool oldValue = _relativeDesiredHeading;
                    _relativeDesiredHeading = value;
                    OnPropertyChanged("IsDesiredHeadingRelative", oldValue, value, true);
                }
            }
        }

        public bool IsBearingRelative
        {
            get
            {
                return _relativeBearing;
            }
            set
            {
                if (!_relativeBearing.Equals(value))
                {
                    bool oldValue = _relativeBearing;
                    _relativeBearing = value;
                    OnPropertyChanged("IsBearingRelative", oldValue, value, true);
                }
            }
        }

        public bool IsDesiredCourseRelative
        {
            get
            {
                return _relativeCourse;
            }
            set
            {
                if (!_relativeCourse.Equals(value))
                {
                    bool oldValue = _relativeCourse;
                    _relativeCourse = value;
                    OnPropertyChanged("IsDesiredCourseRelative", oldValue, value, true);
                }
            }
        }

        #endregion

        void DemoFlag_Execute(object action, HeliosActionEventArgs e)
        {
            
            BeginTriggerBypass(e.BypassCascadingTriggers);

            TimeSpan internalTimeSpan =TimeSpan.FromMilliseconds(100);
            _demoTimer = new DispatcherTimer(
                internalTimeSpan,
                DispatcherPriority.Input,
                (sender, e2) =>
                {
                    OnElapsedTimer(1);
                },
                Dispatcher.CurrentDispatcher);
            _demoTimer.Start();
            EndTriggerBypass(e.BypassCascadingTriggers);
        }

        void OffFlag_Execute(object action, HeliosActionEventArgs e)
        {
            _offFlag.SetValue(e.Value, e.BypassCascadingTriggers);
            _offFlagImage.IsHidden = !e.Value.BoolValue;
        }

        void TACANDFMode_Execute(object action, HeliosActionEventArgs e)
        {
            bool isDFMode = true;
            _tacanDfValue.SetValue(e.Value, e.BypassCascadingTriggers);
            int dfValue = 0;
            int.TryParse(_tacanDfValue?.Value?.StringValue, out dfValue);
            if (dfValue == 2)
            {
                isDFMode = false;
            }

            _dfModeImage.IsHidden = !isDFMode;
            _invalidDistImage.IsHidden = !isDFMode;
        }



        /// <summary>
        /// demo timer event
        /// </summary>
        /// <param name="source"></param>
        /// <param name="e"></param>
        private void OnElapsedTimer(int index)
        {
            _demoTimer.Stop();
            var wkDegree = Convert.ToInt32(_bearing.Value.DoubleValue);
            var wkDegree2 = Convert.ToInt32(_desiredHeading.Value.DoubleValue);
            var wkDegree3 = Convert.ToInt32(_compassNeedle.Rotation);
            var calcDeg = 0;
            var nextPlay = false;
            if (wkDegree >= 0 && wkDegree < 170)
            {
                wkDegree = 230;
            }
            if (wkDegree < 290)
            {
                calcDeg = 3;
                _workBearing = Convert.ToInt32(wkDegree.ToString());
                _milesDrum.Value = ClampDegrees(101 - index);
                _bearing.SetValue(new BindingValue((wkDegree + calcDeg)), true);
                _bearingNeedle.Rotation = _bearing.Value.DoubleValue + _compassNeedle.Rotation;
                _courseDrum.Value = ClampDegrees(_bearing.Value.DoubleValue);
                TimeSpan internalTimeSpan =
                        TimeSpan.FromMilliseconds(100);
                _demoTimer = new DispatcherTimer(
                    internalTimeSpan,
                    DispatcherPriority.Input,
                    (sender, e2) =>
                    {
                        OnElapsedTimer(index+1);
                    },
                    Dispatcher.CurrentDispatcher);
                _demoTimer.Start();
                return;
            }
            if (wkDegree >= 290 && wkDegree2 >= 0 && wkDegree2 < 170)
            {
                wkDegree2 = 359;
                index = 0;
            }
            if (wkDegree2 > 290)
            {
                calcDeg = 3;
                _workBearing = Convert.ToInt32(wkDegree2.ToString());
                _desiredHeading.SetValue(new BindingValue((_workBearing - calcDeg)), true);
                _desiredCourseNeedle.Rotation = _desiredHeading.Value.DoubleValue;
                nextPlay = true;
            }

            if (wkDegree3 >= 0 && wkDegree3 < 170)
            {
                wkDegree3 = 180;
                index = 0;
            }
            if (wkDegree3 < 290)
            {
                calcDeg = 3;

                _workBearing = Convert.ToInt32(wkDegree3.ToString());
                _compassNeedle.Rotation = calcDeg + _compassNeedle.Rotation;
                nextPlay = true;
            }

            if (nextPlay)
            {
                TimeSpan internalTimeSpan =
                    TimeSpan.FromMilliseconds(100);
                _demoTimer = new DispatcherTimer(
                    internalTimeSpan,
                    DispatcherPriority.Input,
                    (sender, e2) =>
                    {
                        OnElapsedTimer(index + 1);
                    },
                    Dispatcher.CurrentDispatcher);
                _demoTimer.Start();
                return;
            }

            else
            {
                _demoFlag.SetValue(new BindingValue(false),false);
            }
        }


        void ToFlag_Execute(object action, HeliosActionEventArgs e)
        {
            _toFlag.SetValue(e.Value, e.BypassCascadingTriggers);
            _toFlagNeedle.IsHidden = !e.Value.BoolValue;
        }

        void FromFlag_Execute(object action, HeliosActionEventArgs e)
        {
            _fromFlag.SetValue(e.Value, e.BypassCascadingTriggers);
            _fromFlagNeedle.IsHidden = !e.Value.BoolValue;
        }

        void Bearing_Execute(object action, HeliosActionEventArgs e)
        {
            _bearing.SetValue(new BindingValue(ClampDegrees(e.Value.DoubleValue)), e.BypassCascadingTriggers);

            if (_relativeBearing)
            {
                _bearingNeedle.Rotation = _bearing.Value.DoubleValue;
            }
            else
            {
                _bearingNeedle.Rotation = _bearing.Value.DoubleValue + _compassNeedle.Rotation;
            }
        }

        void Distance_Execute(object action, HeliosActionEventArgs e)
        {
            _distance.SetValue(e.Value, e.BypassCascadingTriggers);
            _milesDrum.Value = e.Value.DoubleValue;
        }

        void DesiredHeading_Execute(object action, HeliosActionEventArgs e)
        {
            _desiredHeading.SetValue(new BindingValue(ClampDegrees(e.Value.DoubleValue)), e.BypassCascadingTriggers);
            if (_relativeDesiredHeading)
            {
                _headingBug.Rotation = _desiredHeading.Value.DoubleValue;
            }
            else
            {
                _headingBug.Rotation = _desiredHeading.Value.DoubleValue + _compassNeedle.Rotation;
            }
        }

        void DesiredCourse_Execute(object action, HeliosActionEventArgs e)
        {
            _desiredCourse.SetValue(new BindingValue(ClampDegrees(e.Value.DoubleValue)), e.BypassCascadingTriggers);
            _desiredCourseNeedle.Rotation = _desiredCourse.Value.DoubleValue;
            _courseDrum.Value = ClampDegrees(_desiredCourse.Value.DoubleValue + _currentHeading.Value.DoubleValue);


            _deviationNeedle.Rotation = _desiredCourseNeedle.Rotation;
            _deviationCard.Rotation = _desiredCourseNeedle.Rotation;
            _courseFlagNeedle.Rotation = _deviationCard.Rotation;
            _toFlagNeedle.Rotation = _deviationCard.Rotation;
            _fromFlagNeedle.Rotation = _deviationCard.Rotation;
            _dfModeImage.Rotation = _deviationCard.Rotation;
        }

        void CurrentHeading_Execute(object action, HeliosActionEventArgs e)
        {
            Double heading = (ClampDegrees(e.Value.DoubleValue) + 180 / 360);
            _currentHeading.SetValue(new BindingValue(heading), e.BypassCascadingTriggers);
            _compassNeedle.Rotation = -_currentHeading.Value.DoubleValue;

            if (!_relativeDesiredHeading)
            {
                _headingBug.Rotation = _desiredHeading.Value.DoubleValue + _compassNeedle.Rotation;
            }

            if (!_relativeBearing)
            {
                _bearingNeedle.Rotation = _bearing.Value.DoubleValue + _compassNeedle.Rotation;
            }

            if (!_relativeCourse)
            {
                _desiredCourseNeedle.Rotation = _desiredCourse.Value.DoubleValue + _compassNeedle.Rotation;
                _deviationNeedle.Rotation = _desiredCourseNeedle.Rotation;
                _deviationCard.Rotation = _desiredCourseNeedle.Rotation;
                _courseFlagNeedle.Rotation = _deviationCard.Rotation;
                _toFlagNeedle.Rotation = _deviationCard.Rotation;
                _fromFlagNeedle.Rotation = _deviationCard.Rotation;
                _dfModeImage.Rotation = _deviationCard.Rotation;
            }
            else
            {
                _courseDrum.Value = ClampDegrees(_desiredCourse.Value.DoubleValue + _currentHeading.Value.DoubleValue);
            }
        }

        private double ClampDegrees(double input)
        {
            while (input < 0d)
            {
                input += 360d;
            }
            while (input >= 360d)
            {
                input -= 360d;
            }
            return input;
        }

        #region Persistance

        public override void WriteXml(System.Xml.XmlWriter writer)
        {
            writer.WriteElementString("IsDesiredHeadingRelative", IsDesiredHeadingRelative.ToString());
            writer.WriteElementString("IsBearingRelative", IsBearingRelative.ToString());
            writer.WriteElementString("IsDesiredCourseRelative", IsDesiredCourseRelative.ToString());
            base.WriteXml(writer);
        }

        public override void ReadXml(System.Xml.XmlReader reader)
        {
            if (reader.Name.Equals("IsDesiredHeadingRelative"))
            {
                IsDesiredHeadingRelative = bool.Parse(reader.ReadElementString("IsDesiredHeadingRelative"));
            }

            if (reader.Name.Equals("IsBearingRelative"))
            {
                IsBearingRelative = bool.Parse(reader.ReadElementString("IsBearingRelative"));
            }

            if (reader.Name.Equals("IsDesiredCourseRelative"))
            {
                IsDesiredCourseRelative = bool.Parse(reader.ReadElementString("IsDesiredCourseRelative"));
            }
            base.ReadXml(reader);
        }

        #endregion
    }
}
