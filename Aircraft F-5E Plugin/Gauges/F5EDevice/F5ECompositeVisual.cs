//  Copyright 2014 Craig Courtney
//    
//  Helios is free software: you can redistribute it and/or modify
//  it under the terms of the GNU General Public License as published by
//  the Free Software Foundation, either version 3 of the License, or
//  (at your option) any later versionCannot find interface trigger
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
    using System;
    using System.Collections.Generic;

    using System.Windows;
    using System.Windows.Media;
    using GadrocsWorkshop.Helios.Controls;
    using GadrocsWorkshop.Helios.Controls.F5E;

    public abstract class F5ECompositeVisual : CompositeVisualWithBackgroundImage
    {
        private Dictionary<HeliosVisual, Rect> _nativeSizes = new Dictionary<HeliosVisual, Rect>();

        protected F5ECompositeVisual(string name, Size nativeSize)
            : base(name, nativeSize)
        {
            PersistChildren = false;
            Children.CollectionChanged += Children_CollectionChanged;
            _defaultBindingName = "";
            _defaultInterface = null;
        }

        void Children_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            if ((e.Action == System.Collections.Specialized.NotifyCollectionChangedAction.Add) ||
                (e.Action == System.Collections.Specialized.NotifyCollectionChangedAction.Replace))
            {
                foreach (HeliosVisual control in e.NewItems)
                {
                    if (!_nativeSizes.ContainsKey(control))
                    {
                        _nativeSizes.Add(control, new Rect(control.Left, control.Top, control.Width, control.Height));
                    }
                }
            }

            if ((e.Action == System.Collections.Specialized.NotifyCollectionChangedAction.Remove) ||
                (e.Action == System.Collections.Specialized.NotifyCollectionChangedAction.Replace))
            {
                foreach (HeliosVisual control in e.OldItems)
                {
                    if (_nativeSizes.ContainsKey(control))
                    {
                        _nativeSizes.Remove(control);
                    }
                }
            }
        }

        #region Properties
 
        #endregion


        private Point FromCenter(Point pos, Size size) {
            return new Point(pos.X - size.Width / 2.0, pos.Y - size.Height / 2.0);
        }

        protected RotarySwitch AddRotarySwitch(string name, Point posn, Size size,
            string knobImage, int defaultPosition, RotaryClickType clickType,
            string interfaceDeviceName, string interfaceElementName, bool fromCenter, NonClickableZone[] nonClickableZones = null)
        {
            if (fromCenter)
                posn = FromCenter(posn, size);
            string componentName = GetComponentName(name);
            RotarySwitch knob = new RotarySwitch
            {
                Name = componentName,
                KnobImage = knobImage,
                DrawLabels = false,
                DrawLines = false,
                Top = posn.Y,
                Left = posn.X,
                Width = size.Width,
                Height = size.Height,
                DefaultPosition = defaultPosition,
                ClickType = clickType,
                NonClickableZones = nonClickableZones,
            };
            knob.Positions.Clear();

            Children.Add(knob);

            foreach (IBindingTrigger trigger in knob.Triggers)
            {
                AddTrigger(trigger, componentName);
            }
            foreach (IBindingAction action in knob.Actions)
            {
                AddAction(action, componentName);
            }

            AddDefaultInputBinding(
                childName: componentName,
                interfaceTriggerName: interfaceDeviceName + "." + interfaceElementName + ".changed",
                deviceActionName: "set.position");
            AddDefaultOutputBinding(
                childName: componentName,
                deviceTriggerName: "position.changed",
                interfaceActionName: interfaceDeviceName + ".set." + interfaceElementName);

            return knob;
        }

        protected PushButton AddToggledButton(string name, Point posn, Size size, string image, string pushedImage,
            string buttonText, string interfaceDeviceName, PushButtonType pushButtonType,string interfaceElementName, bool fromCenter)
        {
            if (fromCenter)
                posn = FromCenter(posn, size);
            string componentName = GetComponentName(name);
            PushButton button = new PushButton
            {
                Top = posn.Y,
                Left = posn.X,
                Width = size.Width,
                Height = size.Height,
                Image = image,
                PushedImage = pushedImage,
                ButtonType = pushButtonType,
                Text = buttonText,
                Name = componentName
            };
            button.TextFormat.ConfiguredFontSize = button.TextFormat.FontSize;
            Children.Add(button);

            AddTrigger(button.Triggers["pushed"], componentName);
            AddTrigger(button.Triggers["released"], componentName);
            AddTrigger(button.Triggers["closed"], componentName);
            AddTrigger(button.Triggers["open"], componentName);

            AddAction(button.Actions["push"], componentName);
            AddAction(button.Actions["release"], componentName);

            AddAction(button.Actions["set.physical state"], componentName);

            // add the default actions
            AddDefaultOutputBinding(
                childName: componentName,
                deviceTriggerName: "pushed",
                interfaceActionName: interfaceDeviceName + ".push." + interfaceElementName
                );
            AddDefaultOutputBinding(
                childName: componentName,
                deviceTriggerName: "released",
                interfaceActionName: interfaceDeviceName + ".release." + interfaceElementName
                );
            AddDefaultOutputBinding(
                childName: componentName,
                deviceTriggerName: "closed",
                interfaceActionName: interfaceDeviceName + ".closed." + interfaceElementName
                );
            AddDefaultOutputBinding(
                childName: componentName,
                deviceTriggerName: "open",
                interfaceActionName: interfaceDeviceName + ".open." + interfaceElementName
                );
            AddDefaultInputBinding(
                childName: componentName,
                interfaceTriggerName: interfaceDeviceName + "." + interfaceElementName + ".changed",
                deviceActionName: "set.physical state");

            return button;
        }



        protected new Indicator AddIndicator(string name, Point posn, Size size,
            string onImage, string offImage, Color onTextColor, Color offTextColor, string font,
            bool vertical, string interfaceDeviceName, string interfaceElementName, bool fromCenter, bool withText = true)
        {
            if (fromCenter)
                posn = FromCenter(posn, size);
            string componentName = GetComponentName(name);
            Indicator indicator = new Helios.Controls.Indicator
            {
                Top = posn.Y,
                Left = posn.X,
                Width = size.Width,
                Height = size.Height,
                OnImage = onImage,
                OffImage = offImage,
                Name = componentName
            };

            if (withText)
            {
                indicator.Text = name;
                indicator.OnTextColor = onTextColor;
                indicator.OffTextColor = offTextColor;
                indicator.TextFormat.FontStyle = FontStyles.Normal;
                indicator.TextFormat.FontWeight = FontWeights.Normal;
                if (vertical)
                {
                    indicator.TextFormat.FontSize = 8;
                }
                else
                {
                    indicator.TextFormat.FontSize = 12;
                }
                indicator.TextFormat.FontFamily = ConfigManager.FontManager.GetFontFamilyByName(font);
                indicator.TextFormat.PaddingLeft = 0;
                indicator.TextFormat.PaddingRight = 0;
                indicator.TextFormat.PaddingTop = 0;
                indicator.TextFormat.PaddingBottom = 0;
                indicator.TextFormat.VerticalAlignment = TextVerticalAlignment.Center;
                indicator.TextFormat.HorizontalAlignment = TextHorizontalAlignment.Center;
            }
            else
            {
                indicator.Text = "";
            }

            Children.Add(indicator);
            foreach (IBindingTrigger trigger in indicator.Triggers)
            {
                AddTrigger(trigger, componentName);
            }
            AddAction(indicator.Actions["set.indicator"], componentName);

            AddDefaultInputBinding(
                childName: componentName,
                interfaceTriggerName: interfaceDeviceName + "." + interfaceElementName + ".changed",
                deviceActionName: "set.indicator");

            return indicator;
        }

        protected FlipThrottle AddFlipThrottle(string name, bool fromCenter,string animationPattenBasePath, Point posn, Size size,
            string interfaceDeviceName, string interfaceElementName, string actionIdentifier,
            int flipDisplayPatternNumber, List<Tuple<int, string, int, int>> flipThrottlePostions)
        {
            if (fromCenter)
                posn = FromCenter(posn, size);
            string componentName = GetComponentName(name);

            FlipThrottle throttle = new FlipThrottle(componentName, posn, size,  animationPattenBasePath, flipDisplayPatternNumber,flipThrottlePostions);

            Children.Add(throttle);
            foreach (IBindingTrigger trigger in throttle.Triggers)
            {
                AddTrigger(trigger, componentName);
            }
            foreach (IBindingAction action in throttle.Actions)
            {
                AddAction(action, componentName);
            }

            AddDefaultInputBinding(
                childName: componentName,
                interfaceTriggerName: interfaceDeviceName + "." + interfaceElementName + ".changed",
                deviceActionName: "set." + actionIdentifier);

            return throttle;
        }

        protected FlipButton AddFlipButton(string name, bool fromCenter, string animationPattenBasePath, Point posn, Size size,
            string interfaceDeviceName, string interfaceElementName,
            int flipDisplayPatternNumber, List<Tuple<int, string, int, double, double, double>> flipButtonPostions)
        {
            if (fromCenter)
                posn = FromCenter(posn, size);
            string componentName = GetComponentName(name);


            FlipButton flipButton = new FlipButton(componentName, posn, size, animationPattenBasePath, flipDisplayPatternNumber, flipButtonPostions);
            flipButton.Positions.Clear();

            Children.Add(flipButton);

            foreach (IBindingTrigger trigger in flipButton.Triggers)
            {
                AddTrigger(trigger, componentName);
            }
            foreach (IBindingAction action in flipButton.Actions)
            {
                AddAction(action, componentName);
            }

            AddDefaultInputBinding(
                childName: componentName,
                interfaceTriggerName: interfaceDeviceName + "." + interfaceElementName + ".changed",
                deviceActionName: "set.position");
            AddDefaultOutputBinding(
                childName: componentName,
                deviceTriggerName: "position.changed",
                interfaceActionName: interfaceDeviceName + ".set." + interfaceElementName);

            return flipButton;
        }

        protected FlipLever AddFlipLever(string name, Point posn, Size size,
            string animationPattern,
            string interfaceDeviceName, string interfaceElementName,
            List<Tuple<int, string, int, int, int>> leverPositions,
            bool fromCenter)
        {
            if (fromCenter)
                posn = FromCenter(posn, size);
            string componentName = GetComponentName(name);

            FlipLever flipLever = new FlipLever(componentName, posn, size,
                animationPattern, leverPositions);

            Children.Add(flipLever);

            foreach (IBindingTrigger trigger in flipLever.Triggers)
            {
                AddTrigger(trigger, componentName);
            }
            foreach (IBindingAction action in flipLever.Actions)
            {
                AddAction(action, componentName);
            }

            AddDefaultOutputBinding(
                childName: componentName,
                deviceTriggerName: "position.changed",
                interfaceActionName: interfaceDeviceName + ".set." + interfaceElementName
            );

            return flipLever;
        }

        protected SlipGauge AddSlipGauge(string name, Point posn, Size size,
            string interfaceDeviceName, string interfaceElementName)
        {
            string componentName = GetComponentName(name);

            SlipGauge slipGauge = new SlipGauge(componentName, posn, size);

            Children.Add(slipGauge);

            foreach (IBindingTrigger trigger in slipGauge.Triggers)
            {
                AddTrigger(trigger, componentName);
            }
            foreach (IBindingAction action in slipGauge.Actions)
            {
                AddAction(action, componentName);
            }

            AddDefaultOutputBinding(
                childName: componentName,
                deviceTriggerName: "position.changed",
                interfaceActionName: interfaceDeviceName + ".set." + interfaceElementName
            );

            return slipGauge;
        }



        protected GeneralPurposePullKnob AddGeneralPurposePullKnob(string name, bool fromCenter, Point posn, Size size,
            string interfaceDeviceName, string interfaceElementName,
            string knobImagePath, string knobImagePulledPath,
            string frontImagePath, string basePath, string pullReadyImage,
            List<Tuple<int, bool, double, double, string, string>> knobPostions,
            Point frontAdjust,double pullJudgeAngle, double pullJudgeDistance,
            Point frontPos,double frontRatio = 1.0d,
            double stepValue = 0.25,
            bool prohibitOperate = false, bool pullable = false)
        {
            if (fromCenter)
                posn = FromCenter(posn, size);
            string componentName = GetComponentName(name);

            GeneralPurposePullKnob knob = new GeneralPurposePullKnob(componentName, posn, size,
                knobImagePath, knobImagePulledPath, 
                frontImagePath, pullReadyImage, basePath, knobPostions,
                frontAdjust,frontPos,
                pullJudgeAngle, pullJudgeDistance,
                frontRatio, stepValue, prohibitOperate, pullable);
            
            Children.Add(knob);
            BindTriggerForFlipControl(knob, componentName, interfaceDeviceName, interfaceElementName);

            return knob;
        }

        protected FlipRotaryEncoder AddFlipRotaryEncoder(string name, bool fromCenter, Point posn, Size size,
            string interfaceDeviceName, string interfaceElementName,
            string knobImagePath, string knobImagePulledPath,
            string frontImagePath, string basePath, string pullReadyImage,
            List<Tuple<int, bool, double, double, string, string>> knobPostions,
            Point frontAdjust, Point frontPos, double frontRatio = 1.0d,
            double pullJudgeAngle = 270d, double pullJudgeDistance = 20d,
            double thresholdAngle = 45d, double thresholdDistance = 40d,
            bool pullable = false)
        {
            if (fromCenter)
                posn = FromCenter(posn, size);
            string componentName = GetComponentName(name);

            FlipRotaryEncoder knob = new FlipRotaryEncoder(componentName, posn, size,
                knobImagePath, knobImagePulledPath,
                frontImagePath, pullReadyImage, basePath, knobPostions,
                frontAdjust, frontPos, frontRatio,
                pullJudgeAngle, pullJudgeDistance,
                thresholdAngle, thresholdDistance, pullable);

            Children.Add(knob);
            BindTriggerForFlipControl(knob, componentName, interfaceDeviceName, interfaceElementName);

            return knob;
        }

        protected IFFCodeSelector AddCodeSelector(string name, bool fromCenter, Point posn, Size size,
            string interfaceDeviceName, string interfaceElementName,
            string knobImagePath, string knobImagePulledPath,
            string frontImagePath, string basePath, string pullReadyImage,
            List<Tuple<int, bool, double, double, string, string>> knobPostions,
            double pullJudgeAngle, double pullJudgeDistance,
            bool prohibitOperate = false, bool pullable = false)
        {
            if (fromCenter)
                posn = FromCenter(posn, size);
            string componentName = GetComponentName(name);

            IFFCodeSelector knob = new IFFCodeSelector(componentName, posn, size,
                knobImagePath, knobImagePulledPath,
                frontImagePath, pullReadyImage, basePath, knobPostions, pullJudgeAngle, pullJudgeDistance,
                prohibitOperate, pullable);

            Children.Add(knob);
            BindTriggerForFlipControl(knob, componentName, interfaceDeviceName, interfaceElementName);

            return knob;
        }

        protected IFFMasterSelector AddMasterSelector(string name, bool fromCenter, Point posn, Size size,
            string interfaceDeviceName, string interfaceElementName,
            string knobImagePath, string knobImagePulledPath,
            string frontImagePath, string basePath, string pullReadyImage,
            List<Tuple<int, bool, double, double, string, string>> knobPostions,
            double pullJudgeAngle, double pullJudgeDistance,
            bool prohibitOperate = false, bool pullable = false)
        {
            if (fromCenter)
                posn = FromCenter(posn, size);
            string componentName = GetComponentName(name);

            IFFMasterSelector knob = new IFFMasterSelector(componentName, posn, size,
                knobImagePath, knobImagePulledPath,
                frontImagePath, pullReadyImage, basePath, knobPostions, pullJudgeAngle, pullJudgeDistance,
                prohibitOperate, pullable);

            Children.Add(knob);
            BindTriggerForFlipControl(knob, componentName, interfaceDeviceName, interfaceElementName);

            return knob;
        }


        private void BindTriggerForFlipControl(GeneralPurposeKnobBase knob, string componentName, string interfaceDeviceName, 
            string interfaceElementName)
        {
            foreach (IBindingTrigger trigger in knob.Triggers)
            {
                AddTrigger(trigger, componentName);
            }
            foreach (IBindingAction action in knob.Actions)
            {
                AddAction(action, componentName);
            }

            AddDefaultOutputBinding(
                childName: componentName,
                deviceTriggerName: "position.changed",
                interfaceActionName: interfaceDeviceName + ".set." + interfaceElementName
            );
            AddDefaultOutputBinding(
                childName: componentName,
                deviceTriggerName: "pushed",
                interfaceActionName: interfaceDeviceName + ".push." + interfaceElementName + " Button");
            AddDefaultOutputBinding(
                childName: componentName,
                deviceTriggerName: "released",
                interfaceActionName: interfaceDeviceName + ".release." + interfaceElementName + " Button");
        }

        protected GuardCloseOnToggleState AddGuardCloseOnToggleState(
            string name,bool fromCenter,
            Point posn,Size size,
            string guardUpImagePath, string guardDownImagePath,
            int direction, Rect guardOpenRegion, Rect switchRegion,  
            Rect guardCloseRegion, bool noUseClosable = false)
        {
            if (fromCenter) posn = FromCenter(posn, size);

            string componentName = GetComponentName(name);

            GuardCloseOnToggleState guard = new GuardCloseOnToggleState(componentName,posn,
                size, guardUpImagePath, guardDownImagePath, noUseClosable, direction, guardOpenRegion,
                switchRegion, guardCloseRegion);

            Children.Add(guard);

            foreach (IBindingTrigger trigger in guard.Triggers)
            {
                AddTrigger(trigger, componentName);
            }
            foreach (IBindingAction action in guard.Actions)
            {
                AddAction(action, componentName);
            }

            return guard;
        }

        protected StatePushPanel AddStatePushPanel(string name, bool fromCenter, Point posn, Size size,
            string interfaceDeviceName, string interfaceElementName,
            string animationImagePath, List<Tuple<int, string>> stateList)
        {
            if (fromCenter)
                posn = FromCenter(posn, size);
            string componentName = GetComponentName(name);

            StatePushPanel statePanel = new StatePushPanel(componentName, posn, size,
                animationImagePath, stateList);
            Children.Add(statePanel);

            foreach (IBindingTrigger trigger in statePanel.Triggers)
            {
                AddTrigger(trigger, componentName);
            }
            foreach (IBindingAction action in statePanel.Actions)
            {
                AddAction(action, componentName);
            }

            AddDefaultOutputBinding(
                childName: componentName,
                deviceTriggerName: "position.changed",
                interfaceActionName: interfaceDeviceName + ".set." + interfaceElementName
            );
            AddDefaultOutputBinding(
                childName: componentName,
                deviceTriggerName: "pushed",
                interfaceActionName: interfaceDeviceName + ".push." + interfaceElementName + " Button");
            AddDefaultOutputBinding(
                childName: componentName,
                deviceTriggerName: "released",
                interfaceActionName: interfaceDeviceName + ".release." + interfaceElementName + " Button");

            return statePanel;
        }

        protected UHFRadioFreqGauge AddRadioUHFFreqGauge(string name, bool fromCenter, 
            string interfaceDeviceName, string interfaceElementName, string actionIdentifier,
            Point posn, Size size, Point[] displayPosn,Size digitSize, Size displaySize)
        {
            if (fromCenter)
                posn = FromCenter(posn, size);
            string componentName = GetComponentName(name);

            UHFRadioFreqGauge radioFreqDisplay = new UHFRadioFreqGauge(
                componentName, posn, size, displayPosn, digitSize, displaySize);

            Children.Add(radioFreqDisplay);
            foreach (IBindingAction action in radioFreqDisplay.Actions)
            {
                AddAction(action, componentName);
            }

            AddDefaultInputBinding(
                childName: componentName,
                interfaceTriggerName: interfaceDeviceName + "." + interfaceElementName + ".changed",
                deviceActionName: "set." + actionIdentifier);

            return radioFreqDisplay;
        }



        protected DrumCounter AddDrumCounter(string name, bool fromCenter,
            string interfaceDeviceName, string interfaceElementName, string actionIdentifier,
            Point posn, Size size, Point[] displayPosnList, string[] tapePathList, Size digitSize,
            Size displaySize, Action<Double, GaugeDrumCounter[]> argFunc)
        {
            if (fromCenter)
                posn = FromCenter(posn, size);
            string componentName = GetComponentName(name);

            DrumCounter counter = new DrumCounter(componentName, posn, size, displayPosnList, tapePathList,
                digitSize, displaySize, argFunc);

            Children.Add(counter);
            foreach (IBindingAction action in counter.Actions)
            {
                AddAction(action, componentName);
            }

            AddDefaultInputBinding(
                childName: componentName,
                interfaceTriggerName: interfaceDeviceName + "." + interfaceElementName + ".changed",
                deviceActionName: "set." + actionIdentifier);

            return counter;
        }
    
        protected O2Blinker AddO2Blinker(string name, bool fromCenter,
            string interfaceDeviceName, string interfaceElementName, string actionIdentifier,
            Point posn, Size size, int calibMin, int calibMax, Point center, double angle)
        {
            if (fromCenter)
                posn = FromCenter(posn, size);
            string componentName = GetComponentName(name);

            O2Blinker blinker = new O2Blinker(componentName, posn, size, calibMin, calibMax, center, angle);

            Children.Add(blinker);
            foreach (IBindingAction action in blinker.Actions)
            {
                AddAction(action, componentName);
            }

            AddDefaultInputBinding(
                childName: componentName,
                interfaceTriggerName: interfaceDeviceName + "." + interfaceElementName + ".changed",
                deviceActionName: "set." + actionIdentifier);

            return blinker;
        }

        protected RaderPanelBody AddRaderPanelBody(string name, bool fromCenter,
            string interfaceDeviceName, string interfaceElementName, string actionIdentifier,
            Point posn, Size size)
        {
            if (fromCenter)
                posn = FromCenter(posn, size);
            string componentName = GetComponentName(name);

            RaderPanelBody raderPanelBody = new RaderPanelBody(componentName, posn, size);

            Children.Add(raderPanelBody);
            foreach (IBindingAction action in raderPanelBody.Actions)
            {
                AddAction(action, componentName);
            }

            AddDefaultInputBinding(
                childName: componentName,
                interfaceTriggerName: interfaceDeviceName + "." + interfaceElementName + ".changed",
                deviceActionName: "set." + actionIdentifier);

            return raderPanelBody;
        }



        protected LiquidOxygenPressure AddLiquidOxygenPressure(string name, bool fromCenter,
            string interfaceDeviceName, string interfaceElementName, string actionIdentifier,
            Point posn, Size size)
        {
            if (fromCenter)
                posn = FromCenter(posn, size);
            string componentName = GetComponentName(name);

            LiquidOxygenPressure gauge = new LiquidOxygenPressure(componentName, posn);

            Children.Add(gauge);
            foreach (IBindingAction action in gauge.Actions)
            {
                AddAction(action, componentName);
            }

            AddDefaultInputBinding(
                childName: componentName,
                interfaceTriggerName: interfaceDeviceName + "." + interfaceElementName + ".changed",
                deviceActionName: "set." + actionIdentifier);

            return gauge;
        }
         
        protected LiquidOxygenPressureSolid AddLiquidOxygenPressureSolid(string name, bool fromCenter,
            string interfaceDeviceName, string interfaceElementName, string actionIdentifier,
            Point posn, Size size)
        {
            if (fromCenter)
                posn = FromCenter(posn, size);
            string componentName = GetComponentName(name);

            LiquidOxygenPressureSolid gauge = new LiquidOxygenPressureSolid(componentName, posn, size);

            Children.Add(gauge);
            foreach (IBindingAction action in gauge.Actions)
            {
                AddAction(action, componentName);
            }

            AddDefaultInputBinding(
                childName: componentName,
                interfaceTriggerName: interfaceDeviceName + "." + interfaceElementName + ".changed",
                deviceActionName: "set." + actionIdentifier);

            return gauge;
        }

        protected LiquidOxygenQtySolid AddLiquidOxygenQtySolid(string name, bool fromCenter,
            string interfaceDeviceName, string interfaceElementName, string actionIdentifier,
            Point posn, Size size)
        {
            if (fromCenter)
                posn = FromCenter(posn, size);
            string componentName = GetComponentName(name);

            LiquidOxygenQtySolid gauge = new LiquidOxygenQtySolid(componentName, posn, size);

            Children.Add(gauge);
            foreach (IBindingAction action in gauge.Actions)
            {
                AddAction(action, componentName);
            }

            AddDefaultInputBinding(
                childName: componentName,
                interfaceTriggerName: interfaceDeviceName + "." + interfaceElementName + ".changed",
                deviceActionName: "set." + actionIdentifier);

            return gauge;
        }

        protected SAINeedle AddSAINeedle(string name, bool fromCenter,
            string interfaceDeviceName, string interfaceElementName, string actionIdentifier,
            Point posn, Size size)
        {
            if (fromCenter)
                posn = FromCenter(posn, size);
            string componentName = GetComponentName(name);

            SAINeedle gauge = new SAINeedle(componentName, posn);

            Children.Add(gauge);
            foreach (IBindingAction action in gauge.Actions)
            {
                AddAction(action, componentName);
            }

            AddDefaultInputBinding(
                childName: componentName,
                interfaceTriggerName: interfaceDeviceName + "." + interfaceElementName + ".changed",
                deviceActionName: "set." + actionIdentifier);

            return gauge;
        }

        protected SAIGauge AddSAIBody(string name, bool fromCenter,
            string interfaceDeviceName, string interfaceElementName, string actionIdentifier,
            Point posn, Size size)
        {
            if (fromCenter)
                posn = FromCenter(posn, size);
            string componentName = GetComponentName(name);

            SAIGauge gauge = new SAIGauge(componentName, posn);

            Children.Add(gauge);
            
            foreach (IBindingAction action in gauge.Actions)
            {
                AddAction(action, componentName);
            }
            AddDefaultInputBinding(
                childName: componentName,
                interfaceTriggerName: interfaceDeviceName + "." + interfaceElementName + ".changed",
                deviceActionName: "set." + actionIdentifier);
            
            return gauge;
        }


        protected ToggleSwitch AddToggleSwitch(string name, Point posn, Size size, ToggleSwitchPosition defaultPosition,
            string positionOneImage, string positionTwoImage, ToggleSwitchType defaultType, string interfaceDeviceName, string interfaceElementName,
            bool fromCenter, NonClickableZone[] nonClickableZones = null, bool horizontal = false, bool horizontalRender = false, LinearClickType clickType = LinearClickType.Touch)
        {
            return AddToggleSwitch(name, posn, size, defaultPosition,
            positionOneImage, positionTwoImage, "", "", defaultType, interfaceDeviceName, interfaceElementName,
             fromCenter, nonClickableZones, horizontal, horizontalRender, false, "", clickType);
        }
        
        protected ToggleSwitch AddToggleSwitch(string name, Point posn, Size size, ToggleSwitchPosition defaultPosition, 
            string positionOneImage, string positionTwoImage,
            string positionOneIndicatorImage, string positionTwoIndicatorImage, ToggleSwitchType defaultType, string interfaceDeviceName, string interfaceElementName, 
            bool fromCenter, NonClickableZone[] nonClickableZones = null, bool horizontal = false, bool horizontalRender = false, bool indicator = false, string interfaceElementNameIndicator = "", LinearClickType clickType = LinearClickType.Touch)
        {
            if (fromCenter)
                posn = FromCenter(posn, size);
            string componentName = GetComponentName(name);

            ToggleSwitch newSwitch = new ToggleSwitch();
            newSwitch.Name = componentName;
            newSwitch.SwitchType = defaultType;
            newSwitch.ClickType = clickType;
            newSwitch.DefaultPosition = defaultPosition;
            newSwitch.PositionOneImage = positionOneImage;
            newSwitch.PositionTwoImage = positionTwoImage;
            newSwitch.Width = size.Width;
            newSwitch.Height = size.Height;
            newSwitch.HasIndicator = indicator;
            newSwitch.NonClickableZones = nonClickableZones;
            if (indicator)
            {
                newSwitch.PositionOneIndicatorOnImage = positionOneIndicatorImage;
                newSwitch.PositionTwoIndicatorOnImage = positionTwoIndicatorImage;
            }
            if (horizontal)
            {
                newSwitch.Orientation = ToggleSwitchOrientation.Horizontal;
            }
            else
            {
                newSwitch.Orientation = ToggleSwitchOrientation.Vertical;
            }

            newSwitch.Top = posn.Y;
            newSwitch.Left = posn.X;
            if (horizontalRender)
            {
                newSwitch.Rotation = HeliosVisualRotation.CW;
                newSwitch.Orientation = ToggleSwitchOrientation.Horizontal;
            }

            Children.Add(newSwitch);

            foreach (IBindingTrigger trigger in newSwitch.Triggers)
            {
                AddTrigger(trigger, componentName);
            }

            AddAction(newSwitch.Actions["set.position"], componentName);
            
            if(indicator) AddAction(newSwitch.Actions["set.indicator"], componentName);

            AddDefaultOutputBinding(
                childName: componentName,
                deviceTriggerName: "position.changed",
                interfaceActionName: interfaceDeviceName + ".set." + interfaceElementName
            );
            AddDefaultInputBinding(
                childName: componentName,
                interfaceTriggerName: interfaceDeviceName + "." + interfaceElementName + ".changed",
                deviceActionName: "set.position");

            if (indicator)
            {
                AddDefaultInputBinding(
                    childName: componentName,
                    interfaceTriggerName: interfaceDeviceName + "." + interfaceElementNameIndicator + ".changed",
                    deviceActionName: "set.indicator");
            }
            return newSwitch;
        }


        protected IndicatorPushButton AddIndicatorPushButton(string name, Point pos, Size size, string image, string pushedImage, Color textColor, Color onTextColor, string font, 
            string interfaceDeviceName = "", string interfaceElementName = "", string onImage = "", bool fromCenter = false, bool withText = true)
        {
            if (fromCenter)
                pos = FromCenter(pos, size);
            string componentName = GetComponentName(name);

            IndicatorPushButton indicator = new Helios.Controls.IndicatorPushButton
            {
                Top = pos.Y,
                Left = pos.X,
                Width = size.Width,
                Height = size.Height,
                Image = image,
                PushedImage = pushedImage,
                IndicatorOnImage = onImage,
                PushedIndicatorOnImage = onImage,
                Name = componentName,
                OnTextColor = onTextColor,
                TextColor = textColor
            };
            if(withText)
            {
                indicator.TextFormat.FontStyle = FontStyles.Normal;
                indicator.TextFormat.FontWeight = FontWeights.Normal;
                indicator.TextFormat.FontSize = 18;
                indicator.TextFormat.FontFamily = ConfigManager.FontManager.GetFontFamilyByName(font);
                indicator.TextFormat.PaddingLeft = 0;
                indicator.TextFormat.PaddingRight = 0;
                indicator.TextFormat.PaddingTop = 0;
                indicator.TextFormat.PaddingBottom = 0;
                indicator.TextFormat.VerticalAlignment = TextVerticalAlignment.Center;
                indicator.TextFormat.HorizontalAlignment = TextHorizontalAlignment.Center;
                indicator.Text = name;
            }
            else
            {
                indicator.Text = "";
            }

            Children.Add(indicator);
            foreach (IBindingTrigger trigger in indicator.Triggers)
            {
                AddTrigger(trigger, componentName);
            }
            foreach (IBindingAction action in indicator.Actions)
            {
                AddAction(action, componentName);
            }
            
            AddDefaultInputBinding(
                childName: componentName,
                interfaceTriggerName: interfaceDeviceName + "." + interfaceElementName + ".changed",
                deviceActionName: "set.indicator");
            AddDefaultInputBinding(
                childName: componentName,
                interfaceTriggerName: interfaceDeviceName + "." + interfaceElementName + " Button.changed",
                deviceActionName: "set.physical state");
            AddDefaultOutputBinding(
                childName: componentName,
                deviceTriggerName: "pushed",
                interfaceActionName: interfaceDeviceName + ".push." + interfaceElementName + " Button");
            AddDefaultOutputBinding(
                childName: componentName,
                deviceTriggerName: "released",
                interfaceActionName: interfaceDeviceName + ".release." + interfaceElementName + " Button");

            return indicator;
        }

        protected ThreeWayToggleSwitch AddThreeWayToggle(string name, Point pos, Size size,
            ThreeWayToggleSwitchPosition defaultPosition, ThreeWayToggleSwitchType switchType,
            string interfaceDeviceName, string interfaceElementName, bool fromCenter,
            string positionOneImage = "{Helios}/Images/Toggles/round-up.png",
            string positionTwoImage = "{Helios}/Images/Toggles/round-norm.png",
            string positionThreeImage = "{Helios}/Images/Toggles/round-down.png",
            LinearClickType clickType = LinearClickType.Swipe,
            bool horizontal = false,
            HeliosVisualRotation visualRotation = HeliosVisualRotation.None,
            bool horizontalRender = false)
        {
            string componentName = GetComponentName(name);
            ThreeWayToggleSwitch toggle = new ThreeWayToggleSwitch
            {
                Top = pos.Y,
                Left =  pos.X,
                Width = size.Width,
                Height = size.Height,
                DefaultPosition = defaultPosition,
                PositionOneImage = positionOneImage,
                PositionTwoImage = positionTwoImage,
                PositionThreeImage = positionThreeImage,
                SwitchType = switchType,
                Name = componentName
            };
            toggle.ClickType = clickType;
            if (horizontal)
            {
                toggle.Orientation = ToggleSwitchOrientation.Horizontal;
            }
            else
            {
                toggle.Orientation = ToggleSwitchOrientation.Vertical;
            }
            if (horizontalRender)
            {
                toggle.Rotation = visualRotation;
                toggle.Orientation = ToggleSwitchOrientation.Horizontal;
            }

            Children.Add(toggle);
            foreach (IBindingTrigger trigger in toggle.Triggers)
            {
                AddTrigger(trigger, componentName);
            }
            AddAction(toggle.Actions["set.position"], componentName);

            AddDefaultOutputBinding(
                childName: componentName,
                deviceTriggerName: "position.changed",
                interfaceActionName: interfaceDeviceName + ".set." + interfaceElementName
            );
            AddDefaultInputBinding(
                childName: componentName,
                interfaceTriggerName: interfaceDeviceName + "." + interfaceElementName + ".changed",
                deviceActionName: "set.position");

            return toggle;
        }
    }
}
