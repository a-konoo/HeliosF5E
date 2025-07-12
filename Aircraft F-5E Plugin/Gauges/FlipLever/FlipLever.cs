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

namespace GadrocsWorkshop.Helios.Controls.F5E
{
    using ComponentModel;
    using System;
    using System.Collections.Generic;
    using System.Collections.Specialized;
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Windows.Media;
    using System.Windows.Threading;
    using System.Xml;

    using Point = System.Windows.Point;
    using Size = System.Windows.Size;

    [HeliosControl("Helios.F5E.FlipLever", "FlipLever", "F-5E", typeof(FlipLeverRenderer))]
    public class FlipLever : HeliosVisual
    {
        private static readonly NLog.Logger Logger = NLog.LogManager.GetCurrentClassLogger();

        private HeliosTrigger _statusTrigger;
        private HeliosTrigger _selectedTrigger;
        private HeliosTrigger _angleTrigger;

        private int _currentPosition;

        private readonly HeliosValue _positionValue;
        private readonly HeliosValue _positionNameValue;
        private HeliosValue _secondaryInput;
        private bool _isPlaying = false;
        private bool _isRenderReady = false;
        
        private int _prevPosition = 1;
        private int _frameFrom = 1;
        private int _frameTo = 1;

        private int _animationFrameNumber = 0;
        private int _animationFrameCount = 0;
        private string _errorMessage;
        private int _fromButtonInput = 0;
        // Animation parameters

        private DispatcherTimer _animTimer;
        // Image collections of all Steps
        private List<ImageSource> _animationFrames = new List<ImageSource>();
        public FlipLeverStepCollection Positions { get; }
        // bool value for assigned frame loaded successfully
        private bool _isFrameLoaded = false;
        // base filename pattern for animation image
        protected String _animationFrameImageNamePattern;

        private PositionIndexEntry[] _positionIndex;

        public FlipLever() : base("FlipLever", new Size(246, 187))
        {
            Positions = new FlipLeverStepCollection();
            AnimationFrameImageNamePattern = "{F-5E}/Images/DragChute/DragChute_01.png";
            var positionFileName = Path.GetFileName(AnimationFrameImageNamePattern);
            Positions.Add(new FlipLeverPosition(this, 0, "0", 1, 1, 100));
            Positions.Add(new FlipLeverPosition(this, 1, "1", 2, 10, 100));
            Positions.Add(new FlipLeverPosition(this, 2, "2", 10, 14, 100));
            Positions.Add(new FlipLeverPosition(this, 3, "3", 14, 15, 100));

            Positions.CollectionChanged += Positions_CollectionChanged;
            Positions.PositionChanged += PositionChanged;

            _statusTrigger = new HeliosTrigger(this, "", "", "states changed", "Fired when this lever status changed",
                "return current status value.", BindingValueUnits.Numeric);
            Triggers.Add(_statusTrigger);

            _selectedTrigger = new HeliosTrigger(this, "", "", "selected changed", "Fired when this lever selection status changed",
                "return current status value.", BindingValueUnits.Boolean);
            Triggers.Add(_selectedTrigger);

            _angleTrigger = new HeliosTrigger(this, "", "", "angle changed", "Fired when this lever angle status changed",
                "return current status value.", BindingValueUnits.Numeric);
            Triggers.Add(_angleTrigger);

            _currentPosition = 0;
            _animationFrameNumber = MaxPositionCount > 0 ? FirstPositionElement.StartFrame : 1;
            // if value is under decimal position, via interface program, convert value to integer.
            _positionValue = new HeliosValue(this, new BindingValue(0), "", "position",
                "Current Position .", "", BindingValueUnits.Numeric);

            _positionValue.Execute += SetPositionAction_Execute;

            Values.Add(_positionValue);
            Actions.Add(_positionValue);
            Triggers.Add(_positionValue);

            _secondaryInput = new HeliosValue(this, new BindingValue(0), "",
                "secondaryInput", "position change input of the lever(1: someAct/-1: otherAc).", "", BindingValueUnits.Numeric);
            _secondaryInput.Execute += SecondaryInputAction_Execute;

            Values.Add(_secondaryInput);
            Actions.Add(_secondaryInput);

            IsRenderReady = true;
        }

        public FlipLever(string name, Point posn, Size size,
            string animationPattern,
            List<Tuple<int, string, int, int, int>> flipLever)
            : base(name, new Size(size.Width, size.Height))
        {
            Left = posn.X;
            Top = posn.Y;
            Positions = new FlipLeverStepCollection();
            flipLever.ForEach(x =>
            {
                Positions.Add(new FlipLeverPosition(this, x.Item1, x.Item2, x.Item3, x.Item4, x.Item5));
            });
            AnimationFrameImageNamePattern = animationPattern;
            var positionFileName = Path.GetFileName(AnimationFrameImageNamePattern);

            Positions.CollectionChanged += Positions_CollectionChanged;
            Positions.PositionChanged += PositionChanged;

            _currentPosition = 0;
            _animationFrameNumber = MaxPositionCount > 0 ? FirstPositionElement.StartFrame : 1;
            // if value is under decimal position, via interface program, convert value to integer.
            _positionValue = new HeliosValue(this, new BindingValue(0), "", "position",
                "Current Position .", "", BindingValueUnits.Numeric);

            _statusTrigger = new HeliosTrigger(this, "", "", "states changed", "Fired when this lever status changed",
                "return current status value.", BindingValueUnits.Numeric);
            Triggers.Add(_statusTrigger);

            _selectedTrigger = new HeliosTrigger(this, "", "", "selected changed", "Fired when this lever selection status changed",
                "return current status value.", BindingValueUnits.Boolean);
            Triggers.Add(_selectedTrigger);

            _angleTrigger = new HeliosTrigger(this, "", "", "angle changed", "Fired when this lever angle status changed",
                "return current status value.", BindingValueUnits.Numeric);
            Triggers.Add(_angleTrigger);

            _positionValue.Execute += SetPositionAction_Execute;

            Values.Add(_positionValue);
            Actions.Add(_positionValue);
            Triggers.Add(_positionValue);

            _secondaryInput = new HeliosValue(this, new BindingValue(0), "",
                "secondaryInput", "position change input of the lever(1: someAct/-1: otherAc).", "", BindingValueUnits.Numeric);
            _secondaryInput.Execute += SecondaryInputAction_Execute;

            IsRenderReady = true;
        }



        #region Properties

        /// <summary>
        /// the current position, in the range MinPosition..MaxPosition (inclusive)
        /// and which can be changed by dcs interface signal only
        /// </summary>
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
                if (_positionNameValue != null)
                {
                    _positionNameValue.SetValue(new BindingValue(Positions[_currentPosition].Name), BypassTriggers);

                }

                OnPropertyChanged("CurrentPosition", oldValue, value, false);
                OnDisplayUpdate();
            }
        }

        public List<ImageSource> AnimationFrames
        {
            get => _animationFrames;
            set
            {
                if (_animationFrames.Equals(value))
                {
                    return;
                }

                List<ImageSource> oldValue = _animationFrames;
                _animationFrames = value;
                OnPropertyChanged("AnimationFrames", oldValue, value, true);
            }
        }

        public int CurrentPositionIndex { get => CurrentPosition > 0 ? CurrentPosition : 0; }

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

        public String AnimationFrameImageNamePattern
        {
            get => _animationFrameImageNamePattern;
            set
            {
                if ((_animationFrameImageNamePattern == null && value != null)
                    || (_animationFrameImageNamePattern != null && 
                    !_animationFrameImageNamePattern.Equals(value)))
                {
                    String oldValue = _animationFrameImageNamePattern;
                    _animationFrameImageNamePattern = value;

                    OnPropertyChanged("AnimationFrameImageNamePattern", oldValue, value, true);
                    OnDisplayUpdate();
                }
            }
        }

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

        public FlipLeverPosition FirstPositionElement => Positions.Count > 0 ? Positions[0] : null;

        public FlipLeverPosition LastPositionElement =>
            Positions.Count > 0 ? Positions[MaxPositionCount - 1] : null;
        public int MaxPositionCount => Positions.Count;

        public int DefaultPosition
        {
            get => 0;
            set
            {
                int oldValue = value;
                _ = value;
                OnPropertyChanged("DefaultPosition", oldValue, value, true);
            }
        }

        #endregion

        public override void MouseDown(Point location)
        {
            // nothing
        }

        public override void MouseUp(Point location)
        {
            // nothing
        }

        public override void MouseDrag(Point location)
        {
            // nothing
        }

        #region Actions

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
                _fromButtonInput = 0;
                if (_index >= 0 && (_index) <= Positions.Count && (_index) != (_currentPosition))
                {
                    CurrentPosition = _index;
                    var isMinus = CheckRoutableMinus(_index, _prevPosition);

                    SetImageIndexFromTo(Positions, _index, _prevPosition, isMinus);

                    TimeSpan internalTimeSpan =
                        TimeSpan.FromMilliseconds(Positions[_index].FrameSpan);
                    _animTimer = new DispatcherTimer(
                        internalTimeSpan,
                        DispatcherPriority.Input,
                        (sender, e2) =>
                        {
                            OnElapsedTimer(_index);
                        },
                        Dispatcher.CurrentDispatcher);
                    _animTimer.Start();
                }
            }
            EndTriggerBypass(e.BypassCascadingTriggers);
        }

        protected void SecondaryInputAction_Execute(object action, HeliosActionEventArgs e)
        {
            BeginTriggerBypass(e.BypassCascadingTriggers);

            if (int.TryParse(e.Value.StringValue, out int buttonInput))
            {
                // input value from dcs-interface 
                _fromButtonInput = 0;
                if (buttonInput == 1 && CurrentPosition < Positions.Count)
                {
                    _fromButtonInput = 1;
                    CurrentPosition += 1;
                }
                if (buttonInput == -1 && CurrentPosition > 0)
                {
                    _fromButtonInput = -1;
                    CurrentPosition -= 1;
                }
            }
            EndTriggerBypass(e.BypassCascadingTriggers);
        }


        #endregion



        public override void WriteXml(XmlWriter writer)
        {
            base.WriteXml(writer);
            writer.WriteElementString("AnimationFrameImageNamePattern", AnimationFrameImageNamePattern);
            writer.WriteElementString("DefaultPosition", Convert.ToString(DefaultPosition));
            writer.WriteStartElement("Positions");
            foreach (FlipLeverPosition position in Positions)
            {
                writer.WriteStartElement("Position");
                writer.WriteAttributeString("Name", position.Index.ToString());
                writer.WriteAttributeString("StartFrame", position.StartFrame.ToString(CultureInfo.InvariantCulture));
                writer.WriteAttributeString("EndFrame", position.EndFrame.ToString(CultureInfo.InvariantCulture));
                writer.WriteAttributeString("FrameSpan", position.FrameSpan.ToString(CultureInfo.InvariantCulture));
                writer.WriteEndElement();
            }
            writer.WriteEndElement();

        }

        public override void ReadXml(XmlReader reader)
        {
            IsRenderReady = false;
            base.ReadXml(reader);
            AnimationFrameImageNamePattern = reader.ReadElementString("AnimationFrameImageNamePattern");
            Int32.TryParse(reader.ReadElementString("DefaultPosition"), out int defaultPosition);
            DefaultPosition = defaultPosition;
            if (!reader.IsEmptyElement)
            {
                Positions.Clear();
                reader.ReadStartElement("Positions");
                int i = 0;
                while (reader.NodeType != XmlNodeType.EndElement)
                {
                    Int32.TryParse(reader.GetAttribute("Name"), out int name);
                    Int32.TryParse(reader.GetAttribute("StartFrame"), out int startFrame);
                    Int32.TryParse(reader.GetAttribute("EndFrame"), out int endFrame);
                    Int32.TryParse(reader.GetAttribute("FrameSpan"), out int frameSpan);
                    Positions.Add(new FlipLeverPosition(
                        this,
                        i++,
                        name.ToString(),
                        startFrame, endFrame, frameSpan));

                    reader.Read();
                }
                reader.ReadEndElement();
            }
            else
            {
                reader.Read();
                reader.ReadEndElement();
            }

            BeginTriggerBypass(true);
            CurrentPosition = DefaultPosition;
            EndTriggerBypass(true);
            IsRenderReady = true;
        }

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

        private void PositionChanged(object sender, FlipLeverStepChangeArgs e)
        {
            PropertyNotificationEventArgs args =
                new PropertyNotificationEventArgs(e.Position, e.PropertyName, e.OldValue, e.NewValue, true);

            OnPropertyChanged("Positions", args);
            UpdateValueHelp();
            Refresh();
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
                FlipLeverPosition position = Positions[i];
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

        #region Overrides of HeliosVisual

        public override void ScaleChildren(double scaleX, double scaleY)
        {
            // NOTHING
        }

        #endregion

        #region flipAnimation timer functions

        /// <summary>
        /// play each frame at that interval(frame span)  
        /// </summary>
        /// <param name="index">assigned index</param>
        private void OnElapsedTimer(int index)
        {
            _animTimer.Stop();
            _isPlaying = _animationFrameNumber != _frameTo;

            if (_isPlaying)
            {
                AnimationFrameNumber = GetNextFrameIndex();
                TimeSpan internalTimeSpan =
                    TimeSpan.FromMilliseconds(Positions[index].FrameSpan);
                _animTimer = new DispatcherTimer(
                    internalTimeSpan,
                    DispatcherPriority.Input,
                    (sender, e2) =>
                    {
                        OnElapsedTimer(index);
                    },
                    Dispatcher.CurrentDispatcher);
                _animTimer.Start();
            }
            else
            {
                _isPlaying = false;
                _prevPosition = index;
            }
        }

        /// <summary>
        /// get toward of change position 
        /// </summary>
        /// <param name="index">input index value</param>
        /// <param name="prev">old index value</param>
        /// <returns>true: descend/false: ascend</returns>
        private bool CheckRoutableMinus(int index, int prev)
        {
            var _maxIndex = MaxIndex;

            return (index) < prev;
        }

        /// <summary>
        /// calculate frame number of index of purpose
        /// </summary>
        /// <param name="collection">steps</param>
        /// <param name="index">assigned index</param>
        /// <param name="prev">old index</param>
        /// <param name="isMinus">toward</param>
        public void SetImageIndexFromTo(FlipLeverStepCollection collection,
            int index, int prev, bool isMinus)
        {
            if (!(index >= 0 && index < collection.Count))
            {
                return;
            }
            var playPosition = collection[index];
            var _maxIndex = MaxIndex;
            if (!isMinus)
            {
                _frameFrom = collection[prev].StartFrame;
                _frameTo = playPosition.EndFrame;
            }else
            {
                _frameFrom = collection[prev].EndFrame;
                _frameTo = playPosition.StartFrame;
            }
        }

        public int GetNextFrameIndex()
        {

            if (_frameFrom <= _frameTo)
            {
                return AnimationFrameNumber < _frameTo ? AnimationFrameNumber + 1 : _frameTo;
            }
            return AnimationFrameNumber > _frameTo ? AnimationFrameNumber - 1 : _frameTo;

        }

        #endregion
    }
}