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
    using System.Globalization;
    using System.IO;
    using System.Windows.Media;
    using System.Xml;

    using Point = System.Windows.Point;
    using Size = System.Windows.Size;

    [HeliosControl("Helios.F5E.FlipThrottle", "FlipThrottle", "F-5E", typeof(FlipThrottleRenderer))]
    public class FlipThrottle : FlipAnimationBase
    {
        private static readonly NLog.Logger Logger = NLog.LogManager.GetCurrentClassLogger();

        //  throttle position image.
        private List<ImageSource[]> _throttleImages = new List<ImageSource[]>();

        private HeliosTrigger _statusTrigger;
        private HeliosTrigger _positionTrigger;
        private HeliosValue _mainNumber;
        private HeliosValue _branchNumber;
        private HeliosValue _patternValue;
        private int _maxPatternNumber = 1;
        private int _equipment1Value = 1;
        private int _equipment2Value = 1;

        public FlipThrottle()
            : base("FlipThrottle", new Size(432, 264), new FlipAnimationStepCollection())
        {

            AnimationFrameImageNamePattern = "{F-5E}/Images/LeftSidePanel/Throttle/RightThrottle/Rth_01_01.png";
            var positionFileName = Path.GetFileName(AnimationFrameImageNamePattern);
            PatternNumber = (FlipDisplayPatternType)1;
            Positions.Add(new FlipThrottlePosition(this, 0, "0", 1, 2));
            Positions.Add(new FlipThrottlePosition(this, 1, "1", 2, 3));
            Positions.Add(new FlipThrottlePosition(this, 2, "2", 3, 4));
            Positions.Add(new FlipThrottlePosition(this, 3, "3", 4, 1));

            // init Triggers and Actions
            InitTriggersAndActions();

            _throttleImages = new List<ImageSource[]>();

            // baseImage: Rth_01_01.png
            // when position 3 and toggle value is 2,then use image "Rth_03_02.png" with checking _useToggleStatus = true.
            // when _useToggleStatus = false, Rth_03_01.png require.and ignore toggle value;

            CurrentPatternNumber = 1;
            IsRenderReady = true;
        }

        public FlipThrottle(string name,Point posn, Size size,
            string animationPattern,
            int flipDisplayPatternNumber, List<Tuple<int, string, int, int>> flipThrottlePostions)
            : base(name, new Size(size.Width, size.Height), new FlipAnimationStepCollection())
        {
            Left = posn.X;
            Top = posn.Y;
            AnimationFrameImageNamePattern = animationPattern ?? "{F-5E}/Images/LeftSidePanel/Throttle/RightThrottle/Rth_01_01.png";
            var positionFileName = Path.GetFileName(AnimationFrameImageNamePattern);
            PatternNumber = (FlipDisplayPatternType)flipDisplayPatternNumber;
            flipThrottlePostions.ForEach(x =>
            {
                Positions.Add(new FlipThrottlePosition(this, x.Item1, x.Item2, x.Item3, x.Item4));
            });
            
            // init Triggers and Actions
            InitTriggersAndActions();

            _throttleImages = new List<ImageSource[]>();

            CurrentPatternNumber = 1;
            IsRenderReady = true;
        }

        private void InitTriggersAndActions()
        {

            _statusTrigger = new HeliosTrigger(this, "", "", "states changed", "Fired when this throttle status changed",
                "return current status value.", BindingValueUnits.Numeric);

            Triggers.Add(_statusTrigger);

            _mainNumber = new HeliosValue(this, new BindingValue(0), "", "main pattern number",
                "combine the main number and branch number to implement the toggle state of the throttle",
                "main pattern num(1,2,3) -> (1,4,7)", BindingValueUnits.Numeric);
            _mainNumber.Execute += new HeliosActionHandler(ChangeEquipment1Value_Execute);

            Values.Add(_mainNumber);
            Actions.Add(_mainNumber);

            _branchNumber = new HeliosValue(this, new BindingValue(0), "", "branch pattern number",
                "combine the main number and branch number to implement the toggle state of the throttle",
                "branch number(1,2,3) -> (main is 2,then convert 4,5,6)", BindingValueUnits.Numeric);
            _branchNumber.Execute += new HeliosActionHandler(ChangeEquipment2Value_Execute);

            Values.Add(_branchNumber);
            Actions.Add(_branchNumber);


            _patternValue = new HeliosValue(this, new BindingValue(0), "", "pattern number",
                "specifying directly without using the main number and branch number",
                "(1,2,3) -> (1,2,3)", BindingValueUnits.Numeric);
            _patternValue.Execute += new HeliosActionHandler(ChangePatternValue_Execute);

            Values.Add(_patternValue);
            Actions.Add(_patternValue);

            _positionTrigger = new HeliosTrigger(this, "", "", "position changed", "Fired when this throttle position changed",
                "return current position value.", BindingValueUnits.Numeric);
            Triggers.Add(_positionTrigger);
        }

        #region Properties

        #endregion


        public override void MouseDown(Point location)
        {
            Console.WriteLine("Console:"+PatternNumber + "/" + CurrentPosition);
            // not clickable
        }

        public override void MouseUp(Point location)
        {
            // not clickable
        }


        #region Actions

        void ChangePatternValue_Execute(object action, HeliosActionEventArgs e)
        {
            BeginTriggerBypass(e.BypassCascadingTriggers);

            if (Int32.TryParse(e.Value.StringValue, out int pattern))
            {
                CurrentPatternNumber = pattern;
            }

            EndTriggerBypass(e.BypassCascadingTriggers);
        }

        void ChangeEquipment1Value_Execute(object action, HeliosActionEventArgs e)
        {
            BeginTriggerBypass(e.BypassCascadingTriggers);

            if (Int32.TryParse(e.Value.StringValue, out int eqp1))
            {
                _equipment1Value = eqp1;
                if (_equipment2Value > 0)
                {
                    CurrentPatternNumber = (_equipment1Value - 1) * 3 + _equipment2Value;
                }
                Refresh();
            }

            EndTriggerBypass(e.BypassCascadingTriggers);
        }

        void ChangeEquipment2Value_Execute(object action, HeliosActionEventArgs e)
        {
            BeginTriggerBypass(e.BypassCascadingTriggers);

            if (Int32.TryParse(e.Value.StringValue, out int eqp2))
            {
                _equipment2Value = eqp2;
                if (_equipment1Value > 0)
                {
                    CurrentPatternNumber = (_equipment1Value - 1) * 3 + _equipment2Value;
                }
                Refresh();
            }

            EndTriggerBypass(e.BypassCascadingTriggers);
        }


        public void DrivePositionAfterRender()
        {
            _positionTrigger.FireTrigger(new BindingValue(CurrentPositionIndex));
        }

        #endregion



        public override void WriteXml(XmlWriter writer)
        {
            base.WriteXml(writer);
            writer.WriteElementString("AnimationFrameImageNamePattern", AnimationFrameImageNamePattern);
            
            writer.WriteStartElement("Positions");
            foreach (FlipThrottlePosition position in Positions)
            {
                writer.WriteStartElement("Position");
                writer.WriteAttributeString("Name", position.Index.ToString());
                writer.WriteAttributeString("Frame", position.Frame.ToString(CultureInfo.InvariantCulture));
                writer.WriteAttributeString("SendValue", position.SendValue.ToString(CultureInfo.InvariantCulture));

                writer.WriteEndElement();
            }
            writer.WriteEndElement();
            writer.WriteElementString("PatternNumber", (Convert.ToInt32(PatternNumber)).ToString(CultureInfo.InvariantCulture));
            WriteOptionalXml(writer);
        }

        public override void ReadXml(XmlReader reader)
        {
            IsRenderReady = false;
            base.ReadXml(reader);
            AnimationFrameImageNamePattern = reader.ReadElementString("AnimationFrameImageNamePattern");

            if (!reader.IsEmptyElement)
            {
                Positions.Clear();
                reader.ReadStartElement("Positions");
                int i = 0;
                while (reader.NodeType != XmlNodeType.EndElement)
                {
                    Int32.TryParse(reader.GetAttribute("Name"), out int name);
                    Int32.TryParse(reader.GetAttribute("Frame"), out int frame);
                    Int32.TryParse(reader.GetAttribute("FrameSpan"), out int frameSpan);
                    Int32.TryParse(reader.GetAttribute("SendValue"), out int sendValue);

                    Positions.Add(new FlipThrottlePosition(
                        this,
                        i++,
                        name.ToString(),
                        frame,
                        sendValue));

                    reader.Read();
                }
                reader.ReadEndElement();
            }
            else
            {
                reader.Read();
                reader.ReadEndElement();
            }
            if (reader.Name.Equals("PatternNumber"))
            {
                var readPtnNum = int.Parse(reader.ReadElementString("PatternNumber"), CultureInfo.InvariantCulture);
                PatternNumber = (FlipDisplayPatternType)readPtnNum;
            }

            ReadOptionalXml(reader);

            BeginTriggerBypass(true);
            CurrentPosition = DefaultPosition;
            EndTriggerBypass(true);
            IsRenderReady = true;
        }

        public override void ReadOptionalXml(XmlReader reader)
        {
            // NOTHING
        }

        public override void WriteOptionalXml(XmlWriter writer)
        {
            // NOTHING
        }

        #region Overrides of HeliosVisual

        public override void ScaleChildren(double scaleX, double scaleY)
        {
            // NOTHING
        }

        #endregion
    }
}