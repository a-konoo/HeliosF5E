//  Copyright 2014 Craig Courtney
//  Copyright 2022 Helios Contributors
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

using System.Xml;

namespace GadrocsWorkshop.Helios.Controls
{
    using GadrocsWorkshop.Helios.ComponentModel;
    using System;
    using System.Collections.Generic;
    using System.Collections.Specialized;
    using System.Linq;
    using System.Text;
    using System.Windows;
    using System.Windows.Media;
    using System.Windows.Threading;

    /// <summary>
    /// base class for Flip Animation Control
    /// </summary>
    public abstract class FlipAnimationBase : HeliosVisual
    {
        private static readonly NLog.Logger Logger = NLog.LogManager.GetCurrentClassLogger();


        private int _currentPosition;
        private int _defaultPosition;
        private readonly HeliosValue _positionValue;
        private readonly HeliosValue _positionNameValue;

        public FlipAnimationStepCollection Positions { get; }

        // Animation parameters

        private DispatcherTimer _animTimer;
        // Image collections of all Steps
        private List<ImageSource[]> _animationFrames = new List<ImageSource[]>();

        // bool value for assigned frame loaded successfully
        private bool _isFrameLoaded = false;
        // base filename pattern for animation image
        protected String _animationFrameImageNamePattern;  
        // playing frame
        private int _animationFrameNumber = 0;
        //  frameCount
        private int _animationFrameCount = 0;
        protected int _currentPatternNumber = 0;

        private string _exampleFileDescription = String.Empty;
        private bool _animationIsPng = false;
        protected int _patternNumber = 1;
        private bool _isRenderReady = false;
        private bool _isDescriptionOut = false;
        private string _errorMessage;

        private PositionIndexEntry[] _positionIndex;

        /// <summary>
        /// Flip Animation Constructor
        /// </summary>
        /// <param name="name">control name</param>
        /// <param name="defaultSize">size</param>
        /// <param name="positions">Step Collection(Step has it's frame start and end indices.</param>
        protected FlipAnimationBase(string name, Size defaultSize, FlipAnimationStepCollection positions)
            : base(name, defaultSize)
        {
            Positions = positions;

            _currentPosition = 0;
            _defaultPosition = 0;
            _animationFrameNumber = MaxPositionCount > 0 ? FirstPositionElement.Frame : 1;
            // if value is under decimal position, via interface program, convert value to integer.
            _positionValue = new HeliosValue(this, new BindingValue(0), "", "position",
                "Current Position .", "", BindingValueUnits.Numeric);

            _positionValue.Execute += SetPositionAction_Execute;

            Values.Add(_positionValue);
            Actions.Add(_positionValue);
            Triggers.Add(_positionValue);

            _positionNameValue = new HeliosValue(this, new BindingValue("0"), "",
                "position name", "Name of the current step.", "", BindingValueUnits.Text);

            Values.Add(_positionNameValue);
            Triggers.Add(_positionNameValue);

            Positions.CollectionChanged += Positions_CollectionChanged;
            Positions.PositionChanged += PositionChanged;
            ErrorMessage = "foo\r\nbar\r\nhoge";
            AnimationFrameNumber = 1;
            _defaultPosition = 0;
        }

        #region Properties

        public bool IsDescriptionOut
        {
            get => _isDescriptionOut;
            set
            {
                if (_isDescriptionOut == value)
                {
                    return;
                }
                bool oldValue = _isDescriptionOut;
                _isDescriptionOut = value;
                OnPropertyChanged("IsDescriptionOut", oldValue, value, true);
            }
        }

        public int CurrentPosition
        {
            get => _currentPosition;
            set
            {
                if (_currentPosition.Equals(value) || value < 0 || value > MaxIndex)
                {
                    return;
                }

                int oldValue = _currentPosition;
                _currentPosition = value;
                _positionValue.SetValue(new BindingValue((int)_currentPosition), BypassTriggers);
                _positionNameValue.SetValue(new BindingValue(Positions[_currentPosition].Name), BypassTriggers);

                OnPropertyChanged("CurrentPosition", oldValue, value, false);
                OnDisplayUpdate();
            }
        }

        public int CurrentPositionIndex { get => CurrentPosition > 0 ? CurrentPosition : 0; }

        public int DefaultPosition
        {
            get => _defaultPosition;
            set
            {
                if (_defaultPosition.Equals(value) || value <= 0 || value > Positions.Count)
                {
                    return;
                }

                int oldValue = _defaultPosition;
                _defaultPosition = value;
                OnPropertyChanged("DefaultPosition", oldValue, value, true);
            }
        }

        public bool AnimationIsPng
        {
            get => _animationIsPng;
            set => _animationIsPng = value;
        }

        public List<ImageSource[]> AnimationFrames
        {
            get => _animationFrames;
            set => _animationFrames = value;
        }

        public virtual int CurrentPatternNumber { get; set; }

        public String AnimationFrameImageNamePattern
        {
            get => _animationFrameImageNamePattern;
            set
            {
                if ((_animationFrameImageNamePattern == null && value != null)
                    || (_animationFrameImageNamePattern != null && !_animationFrameImageNamePattern.Equals(value)))
                {
                    String oldValue = _animationFrameImageNamePattern;
                    _animationFrameImageNamePattern = value;
                    // change file example description
                    if (IsDescriptionOut)
                    {
                        ExampleFileDescription =
                            FlipAnimUtil.CreateExampleFileDescription(_animationFrameImageNamePattern, _patternNumber);
                    }
                        OnPropertyChanged("AnimationFrameImageNamePattern", oldValue, value, true);
                    OnDisplayUpdate();
                }
            }
        }
        public FlipDisplayPatternType PatternNumber
        {
            get => (FlipDisplayPatternType)_patternNumber;
            set
            {
                var oldValue = _patternNumber;
                _patternNumber = Convert.ToInt32(value);
                if (IsDescriptionOut)
                {
                    ExampleFileDescription =
                        FlipAnimUtil.CreateExampleFileDescription(_animationFrameImageNamePattern, _patternNumber);
                }
                OnPropertyChanged("PatternNumber", oldValue, value, true);
            }
        }

        /// <summary>
        /// example file name by AnimationPattern
        /// </summary>
        public String ExampleFileDescription
        {
            get => _exampleFileDescription;
            set
            {
                String oldValue = _exampleFileDescription;
                // GRID remove first "_" for access key
                _exampleFileDescription = "_" + value;
                RefleshPositionsDescription();
                OnPropertyChanged("ExampleFileDescription", oldValue, value, true);
            }
        }

        public void RefleshPositionsDescription()
        {
            if (IsDescriptionOut) { }
            foreach (var positon in Positions)
            {
                var position = positon as FlipAnimationStepBase;
                position.PositionExampleFileDescription =
                    FlipAnimUtil.CreatePositionExampleFor2PH(AnimationFrameImageNamePattern,
                    _patternNumber, position.Frame);
            }
        }

        public bool IsRenderReady
        {
            get => _isRenderReady;
            set
            {
                if (_isRenderReady.Equals(value))
                {
                    return;
                }

                bool oldValue = _isRenderReady;
                _isRenderReady = value;
                OnPropertyChanged("IsRenderReady", oldValue, value, true);
                Refresh();
            }
        }

        /// <summary>
        /// frameLoaded Status 
        /// </summary>
        public bool FrameLoaded
        {
            get => _isFrameLoaded;
            set
            {
                bool oldValue = _isFrameLoaded;
                _isFrameLoaded = value;
                OnPropertyChanged("FrameLoaded", oldValue, value, true);
            }
        }

        public int AnimationFrameNumber
        {
            get => _animationFrameNumber;
            set
            {
                if (_animationFrameNumber != value)
                {
                    int oldValue = _animationFrameNumber;
                    _animationFrameNumber = value;
                    OnPropertyChanged("AnimationFrameNumber", oldValue, value, true);
                    OnDisplayUpdate();
                }
            }
        }

        public int AnimationFrameCount
        {
            get => _animationFrames.Count;
            set
            {
                int oldValue = _animationFrameCount;
                _animationFrameCount = value;
                OnPropertyChanged("AnimationFrameCount", oldValue, value, true);
            }
        }

        /// <summary>
        /// message of image load error when initialize control
        /// </summary>
        public String ErrorMessage
        {
            get => "_" + _errorMessage;
            set
            {
                String oldValue = _errorMessage;
                _errorMessage = value;
                OnPropertyChanged("ErrorMessage", oldValue, value, true);
            }
        }

        public int MinIndex => 0;
        public int MaxIndex => Positions.Count > 0 ? Positions.Max(x => x.Index) : 0;

        public FlipAnimationStepBase FirstPositionElement => Positions.Count > 0 ? Positions[0] : null;

        public FlipAnimationStepBase LastPositionElement =>
            Positions.Count > 0 ? Positions[MaxPositionCount - 1] : null;
        public int MaxPositionCount => Positions.Count;
        #endregion

        public Point GenerateCenterPoint() => new Point(this.Width / 2, this.Height / 2);

        #region HeliosVisual method
        public override void MouseWheel(int delta)
        {
            // NOTHING
        }

        public override void MouseDrag(Point location)
        {
            // NOTHING
        }
        public override void Reset()
        {
            base.Reset();

            BeginTriggerBypass(true);
            CurrentPosition = DefaultPosition;
            EndTriggerBypass(true);
        }

        // read XML for configuration
        // helper to read XML for optional configuration that is shared across descendants
        public abstract void ReadOptionalXml(XmlReader reader);

        // helper to writer XML for optional configuration that is shared across descendants
        public abstract void WriteOptionalXml(XmlWriter writer);

        #endregion

        void Positions_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.OldItems != null)
            {

                if (Positions.Count == 0)
                {
                    _currentPosition = 0;
                }
                else if (_currentPosition > Positions.Count)
                {
                    _currentPosition = Positions.Count;
                }
            }
            UpdateValueHelp();
            UpdatePositionIndex();
        }

        private void PositionChanged(object sender, FlipAnimationStepChangeArgs e)
        {
            PropertyNotificationEventArgs args =
                new PropertyNotificationEventArgs(e.Position, e.PropertyName, e.OldValue, e.NewValue, true);

            OnPropertyChanged("Positions", args);
            UpdateValueHelp();
            Refresh();
        }

        private void UpdateValueHelp()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(" (");
            for (int i = 0; i < Positions.Count; i++)
            {
                if (i > 0)
                {
                    sb.Append(",");
                }
                FlipAnimationStepBase position = Positions[i];
                sb.Append(i + 1);
                if (position.Name != null && position.Name.Length > 0)
                {
                    sb.Append("=");
                    sb.Append(position.Name);
                }
            }
            sb.Append(")");
            _positionValue.ValueDescription = sb.ToString();
        }

        private class PositionIndexEntry
        {
            public int Index { get; set; }
        }

        private void UpdatePositionIndex()
        {
            _positionIndex = Positions.OrderBy(p => p.Index).Select(
                p => new PositionIndexEntry { Index = p.Index }).ToArray();
        }
        /// <summary>
        /// receive input value to convert frame number of received step.
        /// then set purpose frame and play current frame to frame that converted frame.
        /// </summary>
        /// <param name="action"></param>
        /// <param name="e"></param>
        public void SetPositionAction_Execute(object action, HeliosActionEventArgs e)
        {
            BeginTriggerBypass(e.BypassCascadingTriggers);
            if (int.TryParse(e.Value.StringValue, out int _index))
            {
                
                Console.WriteLine("changed index:" + _index);
                if (_index >= 0 && (_index) <= Positions.Count && (_index) != (_currentPosition))
                {
                    CurrentPosition = _index;
                }
            }
            EndTriggerBypass(e.BypassCascadingTriggers);
        }
    }
}