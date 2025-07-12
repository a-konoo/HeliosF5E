﻿//  Copyright 2023 Helios Contributors
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

namespace GadrocsWorkshop.Helios.Gauges.F15E.EngineMonitorPanel
{
    using GadrocsWorkshop.Helios.Gauges.F15E;
    using GadrocsWorkshop.Helios.ComponentModel;
    using GadrocsWorkshop.Helios.Controls;
    using System;
    using System.Windows;
    using System.Windows.Media;
    using System.Globalization;
    using System.Xml;
    using static GadrocsWorkshop.Helios.Interfaces.DCS.Common.NetworkTriggerValue;

    [HeliosControl("Helios.F15E.EngineMonitorPanel", "Engine Monitor Panel", "F-15E Strike Eagle", typeof(BackgroundImageRenderer),HeliosControlFlags.NotShownInUI)]
    class EngineMonitorPanel : CompositeVisualWithBackgroundImage
    {
        private string _interfaceDeviceName = "Engine Monitor Panel";
        private string _font = "Helios Virtual Cockpit F/A-18C Hornet IFEI";
        private EngineMonitorNozzleGauge _display;
        private FontStyle _fontStyle = FontStyles.Normal;
        private FontWeight _fontWeight = FontWeights.Normal;
        public const double GLASS_REFLECTION_OPACITY_DEFAULT = 0.30d;
        private double _glassReflectionOpacity = GLASS_REFLECTION_OPACITY_DEFAULT;
        private HeliosPanel _frameGlassPanel;

        public EngineMonitorPanel()
            : base("Engine Monitor Panel", new Size(470,437))
        {
            SupportedInterfaces = new[] { typeof(Interfaces.DCS.F15E.F15EInterface) };
            double dispHeight = 36;
            double fontSize = 36;
            AddGauge("Engine Nozzle Gauge", new Point(0d, 0d), new Size(470d, 437d), _interfaceDeviceName, "Engine Nozzle Gauge");

            double RPMWidth = 64;
            AddNumericTextDisplay("Left Engine RPM", new Point(108, 88), new Size(RPMWidth, dispHeight), fontSize, "888", _interfaceDeviceName, "Left Engine RPM", TextHorizontalAlignment.Right, BindingValueUnits.RPMPercent);
            AddNumericTextDisplay("Right Engine RPM", new Point(278, 88), new Size(RPMWidth, dispHeight), fontSize, "888", _interfaceDeviceName, "Right Engine RPM", TextHorizontalAlignment.Right, BindingValueUnits.RPMPercent);

            double TempWidth = 88;
            AddNumericTextDisplay("Left Engine Temperature", new Point(84, 129), new Size(TempWidth, dispHeight), fontSize, "8888", _interfaceDeviceName, "Left Engine Temperature", TextHorizontalAlignment.Right, BindingValueUnits.Celsius);
            AddNumericTextDisplay("Right Engine Temperature", new Point(271, 129), new Size(TempWidth, dispHeight), fontSize, "8888", _interfaceDeviceName, "Right Engine Temperature", TextHorizontalAlignment.Right, BindingValueUnits.Celsius);

            double FFWidth = 125;
            AddNumericTextDisplay("Left Engine Fuel Flow", new Point(63, 171), new Size(FFWidth, dispHeight), fontSize, "88888", _interfaceDeviceName, "Left Engine Fuel Flow", TextHorizontalAlignment.Right, BindingValueUnits.PoundsPerHour);
            AddNumericTextDisplay("Right Engine Fuel Flow", new Point(250, 171), new Size(FFWidth, dispHeight), fontSize, "88888", _interfaceDeviceName, "Right Engine Fuel Flow", TextHorizontalAlignment.Right, BindingValueUnits.PoundsPerHour);

            double oilWidth = 64;
            AddNumericTextDisplay("Left Engine Oil Pressure", new Point(108, 319), new Size(oilWidth, dispHeight), fontSize, "88", _interfaceDeviceName, "Left Engine Oil Pressure", TextHorizontalAlignment.Right, BindingValueUnits.PoundsPerSquareInch);
            AddNumericTextDisplay("Right Engine Oil Pressure", new Point(276, 319), new Size(oilWidth, dispHeight), fontSize, "88", _interfaceDeviceName, "Right Engine Oil Pressure", TextHorizontalAlignment.Right, BindingValueUnits.PoundsPerSquareInch);

            _frameGlassPanel = AddPanel("Fuel Panel Glass", new Point(56, 53), new Size(361d, 325d), "{A-10C}/Images/A-10C/Pilot_Reflection_25.png", _interfaceDeviceName);
            _frameGlassPanel.Opacity = GLASS_REFLECTION_OPACITY_DEFAULT;
            _frameGlassPanel.DrawBorder = false;
            _frameGlassPanel.FillBackground = false;

            ImageDecoration iD = new ImageDecoration()
            {
                Name = "Engine Monitor Glass Mask",
                Left = 56d,
                Top = 53d,
                Width = 361d,
                Height = 325d,
                Alignment = ImageAlignment.Stretched,
                Image = "{F-15E}/Images/EngineMonitorPanel/EngineMonitorBackgroundMask.png",
                IsHidden = false
            };
            iD.Width = 361d;
            iD.Height = 325d;
            Children.Add(iD);

        }

        private void AddGauge(string name, Point pos, Size size, string interfaceDevice, string interfaceElement)
        {
            _display = new EngineMonitorNozzleGauge
            {
                Top = pos.Y,
                Left = pos.X,
                Height = size.Height,
                Width = size.Width,
                Name = GetComponentName(name)
            };
            Children.Add(_display);
            // Note:  we have the actions against the new gauge but to expose those
            // actions in the interface, we copy the actions to the Parent.  This is a new 
            // HeliosActionCollection with the keys equal to the new ActionIDs, however the original
            // HeliosActionCollection which is on the child part will have the original keys, even though
            // we might have changed the values of the ActionIDs.  This has the result that autobinding
            // in CompositeVisual (OnProfileChanged) might not be able to find the actions when doing
            // the "ContainsKey()" for the action.
            // This is why the _display.Name is in the deviceActionName of the AddDefaultInputBinding
            // and *MUST* match the BindingValue device parameter for the gauge being added.

            //foreach (IBindingTrigger trigger in _display.Triggers)
            //{
            //    AddTrigger(trigger, trigger.Name);
            //}
            foreach (IBindingAction action in _display.Actions)
            {
                if (action.Name != "hidden")
                {

                    AddAction(action, _display.Name);
                    //Create the automatic input bindings for the sub component
                    AddDefaultInputBinding(
                       childName: _display.Name,
                       deviceActionName: _display.Name + "." + action.ActionVerb + "." + action.Name,
                       interfaceTriggerName: interfaceDevice + "." + action.Name + ".changed"
                       );
                }

            }
            //_display.Actions.Clear();
        }
        private void AddNumericTextDisplay(string name, Point posn, Size size,
     double baseFontsize, string testDisp, string interfaceDevice, string interfaceElement, TextHorizontalAlignment hTextAlign, BindingValueUnit valueType)
        {
            NumericTextDisplay display = new NumericTextDisplay()
            {
                Name = $"{name}",
                Width = size.Width,
                Height = size.Height,
                Top = posn.Y,
                Left = posn.X,
                ParserDictionary = "",
                UseParseDictionary = false,
                OnTextColor = Color.FromArgb(0xe0, 0xed, 0xed, 0xed),
                ScalingMode = TextScalingMode.Height,
                UseBackground = false,
                TextTestValue = testDisp,
                Unit = valueType,
                TextFormat = new TextFormat
                {
                    FontFamily = ConfigManager.FontManager.GetFontFamilyByName(_font),
                    FontStyle = _fontStyle,
                    FontWeight = _fontWeight,
                    HorizontalAlignment = hTextAlign,
                    VerticalAlignment = TextVerticalAlignment.Center,
                    FontSize = baseFontsize,
                    ConfiguredFontSize = baseFontsize,
                    PaddingRight = 0.006,
                    PaddingLeft = 0,
                    PaddingTop = 0,
                    PaddingBottom = 0
                }
            };
            display.IsHidden = false;
            Children.Add(display);
            foreach (IBindingAction action in display.Actions)
            {
                if (action.Name != "hidden")
                {

                    AddAction(action, name);
                    //Create the automatic input bindings for the sub component
                    AddDefaultInputBinding(display.Name,
                       deviceActionName: action.ActionVerb + "." + action.Name,
                       interfaceTriggerName: $"{interfaceDevice}.{name}.changed"
                       );
                }
            }
        }
        protected HeliosPanel AddPanel(string name, Point posn, Size size, string background, string interfaceDevice)
        {
            HeliosPanel panel = AddPanel
                (
                name: name,
                posn: posn,
                size: size,
                background: background
                );
            // in this instance, we want to all the panels to be hide-able so the actions need to be added
            IBindingAction panelAction = panel.Actions["toggle.hidden"];
            panelAction.Device = $"{Name}_{name}";
            panelAction.Name = "hidden";
            if (!Actions.ContainsKey(panel.Actions.GetKeyForItem(panelAction)))
            {
                Actions.Add(panelAction);
                //string addedKey = Actions.GetKeyForItem(panelAction);
            }
            panelAction = panel.Actions["set.hidden"];
            panelAction.Device = $"{Name}_{name}";
            panelAction.Name = "hidden";
            if (!Actions.ContainsKey(panel.Actions.GetKeyForItem(panelAction)))
            {
                Actions.Add(panelAction);
                //string addedKey = Actions.GetKeyForItem(panelAction);
            }
            return panel;
        }

        #region properties
        public double GlassReflectionOpacity
        {
            get
            {
                return _glassReflectionOpacity;
            }
            set
            {
                double oldValue = _glassReflectionOpacity;
                if (value != oldValue)
                {
                    _glassReflectionOpacity = value;
                    OnPropertyChanged("GlassReflectionOpacity", oldValue, value, true);
                    _frameGlassPanel.IsHidden = value == 0d ? true : false;
                    _frameGlassPanel.Opacity = _glassReflectionOpacity;
                }
            }
        }
        #endregion

        public override bool HitTest(Point location)
        {

            return true;  // nothing to press on the fuel so return false.
        }
        public override void MouseDown(Point location)
        {
            // No-Op
        }

        public override void MouseDrag(Point location)
        {
            // No-Op
        }

        public override void MouseUp(Point location)
        {
            // No-Op
        }
        public override string DefaultBackgroundImage
        {
            get { return "{F-15E}/Images/EngineMonitorPanel/EngineMonitorBackground.png"; }
        }

        public override void WriteXml(XmlWriter writer)
        {
            base.WriteXml(writer);
            if (_glassReflectionOpacity > 0d)
            {
                writer.WriteElementString("GlassReflectionOpacity", GlassReflectionOpacity.ToString(CultureInfo.InvariantCulture));
            }
        }

        public override void ReadXml(XmlReader reader)
        {
            base.ReadXml(reader);
            GlassReflectionOpacity = reader.Name.Equals("GlassReflectionOpacity") ? double.Parse(reader.ReadElementString("GlassReflectionOpacity"), CultureInfo.InvariantCulture) : 0d;
        }
    }
}
