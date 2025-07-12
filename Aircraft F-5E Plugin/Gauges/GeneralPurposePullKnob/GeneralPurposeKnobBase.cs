//  Copyright 2014 Craig Courtney
//  Copyright 2020 Helios Contributors
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
    using System.ComponentModel;
    using System.Globalization;
    using System.Linq;
    using System.Text;
    using System.Windows;
    using System.Windows.Media;
    using System.Xml;

    public abstract class GeneralPurposeKnobBase : Rotary
    {
        private static readonly NLog.Logger Logger = NLog.LogManager.GetCurrentClassLogger();

        protected HeliosTrigger _pullReleaseTrigger;
        protected HeliosTrigger _pullableTrigger;
        protected HeliosTrigger _pushedTrigger;
        protected HeliosTrigger _releasedTrigger;
        protected HeliosTrigger _sendValueTrigger;
        protected HeliosTrigger _increseTrigger;
        protected HeliosTrigger _decreaseTrigger;

        protected int _currentPosition;
        protected int _defaultPosition;
        protected bool _currentPullReleaseKnob;
        protected int _pulledPosition = -1;
        protected bool _smootheKnob = true;
        protected DateTime? _lastChanged = DateTime.Now;
        protected DateTime? _lastPushed = DateTime.Now;
        protected int _waitMilisecond = 300;

        private string _errorMessage;

        private string _knobImage;
        private string _frontImage;
        private string _knobImagePulled;
        private string _pullReadyImage;

        // config
        private bool _isContinuous = false;

        private double _frontLeft = 9.5;
        private double _frontTop = 18;
        private double _pullDistanceX = 3;
        private double _pullDistanceY = 6;
        
        private double _pullJudgeDistance = 30;
        private double _pullJudgeAngle = 270;
        private bool _prohibitOperate = false;

        private double _adjustCenterX = 1.0d;
        private double _adjustCenterY = 0.5d;
        private double _incldeclStep = 0.15d;
        protected bool _pullable;             // allow rotary pull action 
        private bool _isReleaseManual;    // if true, dragging up knob will release
        private bool _pullready;
        protected int _fromButtonInput;

        private double _frontRatio = 0.5d;

        // HeliosValue (input parameter)
        protected HeliosValue _positionValue;
        protected HeliosValue _pullReleaseKnob;
        protected HeliosValue _secondaryInput;

        protected bool _releaseLock = false;
        protected Point? _draggingLast;
        // if true, block render method run.(for change property init/move on profiler)
        
        private bool _isRenderReady = false;

        // array ordered by position angles in degrees, so we can dereference to
        // their position index
        protected PositionIndexEntry[] _positionIndex;

        // comparison function to binary search in sorted array
        protected static readonly PositionSortComparer PositionIndexComparer
            = new PositionSortComparer();

        public GeneralPurposeKnobBase(string name, Point posn, Size size)
            : base(name, new Size(size.Width, size.Height))
        {
            Left = posn.X;
            Top = posn.Y;
        }

        protected abstract void InitTriggersAndActions();


        public GeneralPurposeKnobBase(string name, Size size) : base(name, size)
        {

        }

        #region Properties

        public GeneralPurposeKnobBasePositionCollection Positions { get; }
            = new GeneralPurposeKnobBasePositionCollection();

        public Double InclDeclStep
        {
            get => _incldeclStep;
            set
            {
                if (_incldeclStep.Equals(value))
                {
                    return;
                }

                double oldValue = _incldeclStep;
                _incldeclStep = value;
                OnPropertyChanged("InclDeclStep", oldValue, value, true);
                Refresh();
            }
        }

        public bool IsSmoothKnob
        {
            get => _smootheKnob;
            set
            {
                if (_smootheKnob.Equals(value))
                {
                    return;
                }

                _smootheKnob = value;
                OnPropertyChanged("IsSmoothKnob", !value, value, true);
                Refresh();
            }
        }
        public bool PullReady
        {
            get => _pullready;
            set
            {
                if (_pullready.Equals(value))
                {
                    return;
                }

                _pullready = value;
                OnPropertyChanged("PullReady", !value, value, true);
                Refresh();
            }
        }

        public bool ProhibitOperate
        {
            get => _prohibitOperate;
            set
            {
                if (_prohibitOperate.Equals(value))
                {
                    return;
                }

                _prohibitOperate = value;
                OnPropertyChanged("ProhibitOperate", !value, value, true);
                Refresh();
            }
        }

        public double PullJudgeDistance
        {
            get => _pullJudgeDistance;
            set
            {
                if (_pullJudgeDistance.Equals(value))
                {
                    return;
                }
                var oldValue = _pullJudgeDistance;
                _pullJudgeDistance = value;
                OnPropertyChanged("PullJudgeDistance", oldValue, value, true);
                Refresh();
            }
        }

        public double PullJudgeAngle
        {
            get => _pullJudgeAngle;
            set
            {
                if (_pullJudgeAngle.Equals(value))
                {
                    return;
                }
                var oldValue = _pullJudgeAngle;
                _pullJudgeAngle = value;
                OnPropertyChanged("PullJudgeAngle", oldValue, value, true);
                Refresh();
            }
        }

        public int WaitMillisecond
        {
            get => _waitMilisecond;
            set
            {
                if (_waitMilisecond.Equals(value))
                {
                    return;
                }
                var oldValue = _waitMilisecond;
                _waitMilisecond = value;
                OnPropertyChanged("WaitMillisecond", oldValue, value, true);
                Refresh();
            }
        }

        public double FrontRatio
        {
            get => _frontRatio;
            set
            {
                if (_frontRatio.Equals(value))
                {
                    return;
                }
                var oldValue = _frontRatio;
                _frontRatio = value;
                OnPropertyChanged("FrontRatio", oldValue, value, true);
                Refresh();
            }
        }

        public bool IsContinuous
        {
            get => _isContinuous;
            set
            {
                if (_isContinuous.Equals(value))
                {
                    return;
                }

                bool oldValue = _isContinuous;
                _isContinuous = value;
                OnPropertyChanged("IsContinuous", oldValue, value, true);
                Refresh();
            }
        }


        public bool IsReleaseManual
        {
            get => _isReleaseManual;
            set
            {
                if (_isReleaseManual.Equals(value))
                {
                    return;
                }

                bool oldValue = _isReleaseManual;
                _isReleaseManual = value;
                OnPropertyChanged("IsReleaseManual", oldValue, value, true);
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


        public int CurrentIndex
        {
            get => _currentPosition - 1 >= 0 ? _currentPosition - 1 : 0;
        }

        public abstract int CurrentPosition
        {
            get;
            set;
        }

        /// <summary>
        /// message of image load error when initialize control
        /// </summary>
        public String ErrorMessage
        {
            get => _errorMessage;
            set
            {
                String oldValue = _errorMessage;
                _errorMessage = value;
                OnPropertyChanged("ErrorMessage", oldValue, value, true);
            }
        }

        #region Actions

        protected abstract void SetPositionAction_Execute(object action, HeliosActionEventArgs e);

        protected abstract void SecondaryInputAction_Execute(object action, HeliosActionEventArgs e);


        protected abstract void SetPullReleaseKnob_Execute(object action, HeliosActionEventArgs e);



        #endregion
        public abstract int DefaultPosition
        {
            get;
            set;
        }


        public abstract bool PullReleaseKnob
        {
            get;
            set;
        }

        public string KnobImage
        {
            get { return _knobImage; }
            set
            {
                if ((_knobImage == null && value != null)
                    || (_knobImage != null && !_knobImage.Equals(value)))
                {
                    string oldValue = _knobImage;
                    _knobImage = value;
                    OnPropertyChanged("KnobImage", oldValue, value, true);
                    Refresh();
                }
            }
        }

        public string FrontImage
        {
            get { return _frontImage; }
            set
            {
                if ((_frontImage == null && value != null)
                    || _frontImage != null && !_frontImage.Equals(value))
                {
                    string oldValue = _frontImage;
                    _frontImage = value;
                    OnPropertyChanged("FrontImage", oldValue, value, true);
                    Refresh();
                }
            }
        }

        public string PullReadyImage
        {
            get { return _pullReadyImage; }
            set
            {
                if ((_pullReadyImage == null && value != null)
                    || _pullReadyImage != null && !_pullReadyImage.Equals(value))
                {
                    string oldValue = _pullReadyImage;
                    _pullReadyImage = value;
                    OnPropertyChanged("PullReadyImage", oldValue, value, true);
                    Refresh();
                }
            }
        }

        public bool Pullable
        {
            get { return _pullable; }
            set
            {
                if (!_pullable.Equals(value))
                {
                    bool oldValue = _pullable;
                    _pullable = value;
                    OnPropertyChanged("Pullable", oldValue, value, true);
                    Refresh();
                }
            }
        }
        

        public string KnobImagePulled
        {
            get { return _knobImagePulled; }
            set
            {
                if ((_knobImagePulled == null && value != null)
                    || (_knobImagePulled != null && !_knobImagePulled.Equals(value)))
                {
                    string oldValue = _knobImagePulled;
                    _knobImagePulled = value;
                    OnPropertyChanged("KnobImagePulled", oldValue, value, true);
                    Refresh();
                }
            }
        }

        public Double FrontLeft
        {
            get { return _frontLeft; }
            set
            {
                if (_frontLeft != value)
                {
                    Double oldValue = _frontLeft;
                    _frontLeft = value;
                    OnPropertyChanged("FrontLeft", oldValue, value, true);
                    Refresh();
                }
            }
        }

        public Double FrontTop
        {
            get { return _frontTop; }
            set
            {
                if (_frontTop != value)
                {
                    Double oldValue = _frontTop;
                    _frontTop = value;
                    OnPropertyChanged("FrontTop", oldValue, value, true);
                    Refresh();
                }
            }
        }


        public Double PullDistanceX
        {
            get { return _pullDistanceX; }
            set
            {
                if (_pullDistanceX != value)
                {
                    Double oldValue = _pullDistanceX;
                    _pullDistanceX = value;
                    OnPropertyChanged("PullDistanceX", oldValue, value, true);
                    Refresh();
                }
            }
        }

        public Double PullDistanceY
        {
            get { return _pullDistanceY; }
            set
            {
                if (_pullDistanceY != value)
                {
                    Double oldValue = _pullDistanceY;
                    _pullDistanceY = value;
                    OnPropertyChanged("PullDistanceY", oldValue, value, true);
                    Refresh();
                }
            }
        }

        public Double AdjustCenterX
        {
            get { return _adjustCenterX; }
            set
            {
                if (_adjustCenterX != value)
                {
                    Double oldValue = _adjustCenterX;
                    _adjustCenterX = value;
                    OnPropertyChanged("AdjustCenterX", oldValue, value, true);
                    Refresh();
                }
            }
        }

        public Double AdjustCenterY
        {
            get { return _adjustCenterY; }
            set
            {
                if (_adjustCenterY != value)
                {
                    Double oldValue = _adjustCenterY;
                    _adjustCenterY = value;
                    OnPropertyChanged("AdjustCenterY", oldValue, value, true);
                    Refresh();
                }
            }
        }

        #endregion

        protected void Positions_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.OldItems != null)
            {
                foreach (GeneralPurposeKnobBasePosition position in e.OldItems)
                {
                    Triggers.Remove(position.EnterTriggger);
                    Triggers.Remove(position.ExitTrigger);
                }

                if (Positions.Count == 0)
                {
                    _currentPosition = 0;
                }
                else if (_currentPosition > Positions.Count)
                {
                    _currentPosition = Positions.Count;
                }
            }

            if (e.NewItems != null)
            {
                foreach (GeneralPurposeKnobBasePosition position in e.NewItems)
                {
                    Triggers.Add(position.EnterTriggger);
                    Triggers.Add(position.ExitTrigger);
                }
            }

            // Need to do it twice to prevent collisions.  This is
            // just an easy way to do it instead of reordering everything
            // in the loops above.
            int i = 1000000;
            foreach (GeneralPurposeKnobBasePosition position in Positions)
            {
                position.Index = i++;
            }

            i = 1;
            foreach (GeneralPurposeKnobBasePosition position in Positions)
            {
                position.Index = i++;
            }
            UpdateValueHelp();
            UpdatePositionIndex();
        }

        protected void PositionsChanged(object sender, GeneralPurposeKnobBasePositionChangeArgs e)
        {
            PropertyNotificationEventArgs args = new PropertyNotificationEventArgs(
                e.Position, e.PropertyName, e.OldValue, e.NewValue, true);
            OnPropertyChanged("Positions", args);
            UpdateValueHelp();
            if (IsRenderReady) {
                Refresh();
            }
            
        }

        protected void UpdateValueHelp()
        {
            StringBuilder sb = new StringBuilder("");
            sb.Append(" (");
            for (int i = 0; i < Positions.Count; i++)
            {
                if (i > 0)
                {
                    sb.Append(",");
                }
                GeneralPurposeKnobBasePosition position = Positions[i];
                sb.Append(i);
            }
            sb.Append(")");
            _positionValue.ValueDescription = sb.ToString();
        }

        protected class PositionIndexEntry
        {
            public double Rotation { get; set; }
            public int Index { get; set; }
        }

        protected class PositionSortComparer : IComparer<PositionIndexEntry>
        {
            public int Compare(PositionIndexEntry left, PositionIndexEntry right) => (int)(left.Index - right.Index);
        }

        private void UpdatePositionIndex()
        {
            _positionIndex = Positions.OrderBy(p => p.Index)
                .Select(p => new PositionIndexEntry { Index = p.Index }).ToArray();
        }

        #region IPulsedControl

        public override void Pulse(int pulses)
        {
            if (Positions.Count == 0)
            {
                // there are no positions so we cannot move
                return;
            }

            // WARNING: Positions is a zero-based array, but _currentPosition is 1-based
            int newPosition = _currentPosition + pulses;

            if (IsContinuous)
            {
                // wrap around if we have to
                newPosition = 1 + ((newPosition - 1) % Positions.Count);
                if (newPosition < 1)
                {
                    // don't use negative remainder
                    newPosition += Positions.Count;
                }
                CurrentPosition = newPosition;
                return;
            }

            // explicitly check boundaries
            if (newPosition > Positions.Count)
            {
                CurrentPosition = Positions.Count;
            }
            else if (newPosition < 1)
            {
                CurrentPosition = 1;
            }
            else
            {
                CurrentPosition = newPosition;
            }
        }

        public override void MouseWheel(int delta)
        {
            base.MouseWheel(delta);
        }

        #endregion

        #region IRotarySwitch

        public int MinPosition => 1;

        public int MaxPosition => Positions.Count;

        #endregion

        #region IRotaryControl

        // the angle that our control would be at if we were allowed to stop everywhere,
        // required so that IRotaryControl will operate correctly
        // with incremental changes
        private double _controlAngle;

        #endregion

        public override void Reset()
        {
            base.Reset();

            BeginTriggerBypass(true);
            CurrentPosition = DefaultPosition;
            EndTriggerBypass(true);
        }

        public override void WriteXml(XmlWriter writer)
        {
            TypeConverter colorConverter = TypeDescriptor.GetConverter(typeof(Color));

            base.WriteXml(writer);

            writer.WriteElementString("KnobImage", KnobImage);
            writer.WriteElementString("Pullable", Pullable.ToString());
            writer.WriteElementString("KnobImagePulled", KnobImagePulled);
            writer.WriteElementString("FrontImage", FrontImage);
            writer.WriteElementString("PullReadyImage", PullReadyImage);

            writer.WriteElementString("FrontLeft", Convert.ToString(_frontLeft));
            writer.WriteElementString("FrontTop", Convert.ToString(_frontTop));

            writer.WriteElementString("PullDistanceX", Convert.ToString(_pullDistanceX));
            writer.WriteElementString("PullDistanceY", Convert.ToString(_pullDistanceY));

            writer.WriteElementString("AdjustCenterX", Convert.ToString(_adjustCenterX));
            writer.WriteElementString("AdjustCenterY", Convert.ToString(_adjustCenterY));

            writer.WriteElementString("FrontRatio", Convert.ToString(_frontRatio));

            writer.WriteStartElement("Positions");
            foreach (GeneralPurposeKnobBasePosition position in Positions)
            {
                writer.WriteStartElement("Position");
                writer.WriteAttributeString("CanPull", Convert.ToString(position.CanPull));
                writer.WriteAttributeString("SendValue", Convert.ToString(position.SendValue));
                writer.WriteAttributeString("KnobRotateImage", Convert.ToString(position.KnobRotateImage));
                writer.WriteAttributeString("KnobPulledbRotateImage", Convert.ToString(position.KnobPulledbRotateImage));
                writer.WriteAttributeString("Rotation", Convert.ToString(position.Rotation));

                writer.WriteEndElement();
            }
            writer.WriteEndElement();
            writer.WriteElementString("DefaultPosition", DefaultPosition.ToString(CultureInfo.InvariantCulture));

            if (IsContinuous)
            {
                writer.WriteElementString("Continuous", IsContinuous.ToString(CultureInfo.InvariantCulture));
            }
            if (IsReleaseManual)
            {
                writer.WriteElementString("IsReleaseManual", IsReleaseManual.ToString(CultureInfo.InvariantCulture));
            }

            writer.WriteElementString("IsSmoothKnob", IsSmoothKnob.ToString(CultureInfo.InvariantCulture));
            writer.WriteElementString("WaitMillisecond", WaitMillisecond.ToString(CultureInfo.InvariantCulture));
            writer.WriteElementString("InclDeclStep", InclDeclStep.ToString(CultureInfo.InvariantCulture));
            writer.WriteElementString("ProhibitOperate", ProhibitOperate.ToString(CultureInfo.InvariantCulture));
            writer.WriteElementString("PullJudgeAngle", PullJudgeAngle.ToString(CultureInfo.InvariantCulture));
            writer.WriteElementString("PullJudgeDistance", PullJudgeDistance.ToString(CultureInfo.InvariantCulture));

            WriteOptionalXml(writer);
        }

        public override void ReadXml(XmlReader reader)
        {
            IsRenderReady = false;
            TypeConverter bc = TypeDescriptor.GetConverter(typeof(bool));

            base.ReadXml(reader);
            KnobImage = reader.ReadElementString("KnobImage");
            
            if (reader.Name.Equals("Pullable"))
            {
                Pullable = (bool)bc.ConvertFromInvariantString(reader.ReadElementString());
            }
            KnobImagePulled = reader.ReadElementString("KnobImagePulled");

            if (reader.Name.Equals("FrontImage"))
            {
                FrontImage = reader.ReadElementString("FrontImage");
            }
            if (reader.Name.Equals("PullReadyImage"))
            {
                PullReadyImage = reader.ReadElementString("PullReadyImage");
            }

            FrontLeft = Convert.ToDouble(reader.ReadElementString("FrontLeft"));
            FrontTop = Convert.ToDouble(reader.ReadElementString("FrontTop"));

            PullDistanceX = Convert.ToDouble(reader.ReadElementString("PullDistanceX"));
            PullDistanceY = Convert.ToDouble(reader.ReadElementString("PullDistanceY"));

            AdjustCenterX = Convert.ToDouble(reader.ReadElementString("AdjustCenterX"));
            AdjustCenterY = Convert.ToDouble(reader.ReadElementString("AdjustCenterY"));
            if (reader.IsStartElement("FrontRatio"))
            {
                FrontRatio = Convert.ToDouble(reader.ReadElementString("FrontRatio"));
            }

            if (!reader.IsEmptyElement)
            {
                Positions.Clear();
                reader.ReadStartElement("Positions");
                int i = 1;
                while (reader.NodeType != XmlNodeType.EndElement)
                {
                    var canpull = Convert.ToBoolean(reader.GetAttribute("CanPull"));
                    var sendValue = Convert.ToDouble(reader.GetAttribute("SendValue"));
                    var knobRotateImage = reader.GetAttribute("KnobRotateImage");
                    var knobPulledbRotateImage = reader.GetAttribute("KnobPulledbRotateImage");

                    double rotation = Convert.ToDouble(reader.GetAttribute("Rotation"));

                    Positions.Add(new GeneralPurposeKnobBasePosition(this, i++,
                        canpull, sendValue, rotation, knobRotateImage, knobPulledbRotateImage));
                    reader.Read();
                }
                reader.ReadEndElement();
            }
            else
            {
                reader.Read();
            }
            DefaultPosition = int.Parse(reader.ReadElementString("DefaultPosition"), CultureInfo.InvariantCulture);

            
            if (reader.Name.Equals("Continuous"))
            {
                IsContinuous = (bool)bc.ConvertFromInvariantString(reader.ReadElementString());
            }
            if (reader.Name.Equals("IsReleaseManual"))
            {
                IsReleaseManual = (bool)bc.ConvertFromInvariantString(reader.ReadElementString());
            }

            if (reader.Name.Equals("IsSmoothKnob"))
            {
                IsSmoothKnob = (bool)bc.ConvertFromInvariantString(reader.ReadElementString());
            }
            if (reader.Name.Equals("WaitMillisecond"))
            {
                int.TryParse(reader.ReadElementString(), out int waitTime);
                WaitMillisecond = waitTime;
            }

            if (reader.Name.Equals("InclDeclStep"))
            {
                Double.TryParse(reader.ReadElementString(), out double incldeclStep);
                InclDeclStep = incldeclStep;
            }

            if (reader.Name.Equals("ProhibitOperate"))
            {
                ProhibitOperate = Convert.ToBoolean(reader.ReadElementString());
            }

            if (reader.Name.Equals("PullJudgeAngle"))
            {
                Double.TryParse(reader.ReadElementString(), out double pullJudgeAngle);
                PullJudgeAngle = pullJudgeAngle;
            }

            if (reader.Name.Equals("PullJudgeDistance"))
            {
                Double.TryParse(reader.ReadElementString(), out double pullJudgeDistance);
                PullJudgeDistance = pullJudgeDistance;
            }

            ReadOptionalXml(reader);
            _currentPosition = DefaultPosition;
            IsRenderReady = true;
        }
    }
}