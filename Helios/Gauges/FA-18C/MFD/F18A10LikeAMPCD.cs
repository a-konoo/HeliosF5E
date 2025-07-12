﻿//  Copyright 2014 Craig Courtney
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

// ReSharper disable once CheckNamespace

using GadrocsWorkshop.Helios.Controls;

namespace GadrocsWorkshop.Helios.Gauges.FA_18C.MFD
{
    using GadrocsWorkshop.Helios.ComponentModel;
    using System;
    using System.Globalization;
    using System.Windows;

    [HeliosControl("FA18C.F18A10LikeAMPCD", "AMPCD", "F/A-18C", typeof(BackgroundImageRenderer))]
    class F18A10LikeAMPCD : Gauges.MFD
    {
        private static readonly Rect SCREEN_RECT = new Rect(88, 161, 551, 532);
        private Rect _scaledScreenRect = SCREEN_RECT;

        public F18A10LikeAMPCD()
            : base("AMPCD", new Size(500, 500))
        {
            AddButton("OSB6", 131, 40);
            AddButton("OSB7", 182, 40);
            AddButton("OSB8", 233, 40);
            AddButton("OSB9", 284, 40);
            AddButton("OSB10", 335, 40);

            AddButton("OSB11", 438, 114);
            AddButton("OSB12", 438, 166);
            AddButton("OSB13", 438, 216);
            AddButton("OSB14", 438, 266);
            AddButton("OSB15", 438, 315);

            AddButton("OSB16", 335, 448);
            AddButton("OSB17", 284, 448);
            AddButton("OSB18", 233, 448);
            AddButton("OSB19", 182, 448);
            AddButton("OSB20", 131, 448);

            AddButton("OSB1", 23, 316);
            AddButton("OSB2", 23, 267);
            AddButton("OSB3", 23, 217);
            AddButton("OSB4", 23, 166);
            AddButton("OSB5", 23, 114);
            AddRocker("Day / Night", "MFD Rocker", "L", 15, 35);
            AddRocker("Symbols", "MFD Rocker", "R", 420, 30);
            AddRocker("Gain", "MFD Rocker", "V", 30, 408);
            AddRocker("Contrast", "MFD Rocker", "V", 445, 408);

            AddThreeWayToggle("Heading", 60, 5, new Size(30, 60));
            AddThreeWayToggle("Course", 375, 5, new Size(30, 60));

            AddKnob("Mode Knob",new Point(235,5),new Size(30,30));
        }

        #region Properties

        public override string DefaultBackgroundImage
        {
            get { return "{FA-18C}/Images/a10likeampcd.png"; }
        }

        #endregion

        protected override void OnPropertyChanged(PropertyNotificationEventArgs args)
        {
            if (args.PropertyName.Equals("Width") || args.PropertyName.Equals("Height"))
            {
                double scaleX = Width / NativeSize.Width;
                double scaleY = Height / NativeSize.Height;
                _scaledScreenRect.Scale(scaleX, scaleY);
            }
            base.OnPropertyChanged(args);
        }

        private void AddButton(string name, double x, double y)
        {
            Helios.Controls.PushButton button = new Helios.Controls.PushButton();
            button.Top = y;
            button.Left = x;
            button.Width = 36;
            button.Height = 36;

            button.Image = "{Helios}/Images/A-10/mfd-out.png";
            button.PushedImage = "{Helios}/Images/A-10/mfd-in.png";

            button.Name = name;

            Children.Add(button);

            AddTrigger(button.Triggers["pushed"], name);
            AddTrigger(button.Triggers["released"], name);

            AddAction(button.Actions["push"], name);
            AddAction(button.Actions["release"], name);
            AddAction(button.Actions["set.physical state"], name);
        }
        private void AddKnob(string name, Point posn, Size size)
        {
            Helios.Controls.Potentiometer knob = new Helios.Controls.Potentiometer
            {
                Name = name,
                KnobImage = "{AV-8B}/Images/Common Knob.png",
                InitialRotation = 219,
                RotationTravel = 291,
                MinValue = 0,
                MaxValue = 1,
                InitialValue = 0,
                StepValue = 0.1,
                Top = posn.Y,
                Left = posn.X,
                Width = size.Width,
                Height = size.Height
            };

            Children.Add(knob);
            foreach (IBindingTrigger trigger in knob.Triggers)
            {
                AddTrigger(trigger, name);
            }
            AddAction(knob.Actions["set.value"], name);
        }

        private new void AddTrigger(IBindingTrigger trigger, string device)
        {
            trigger.Device = device;
            Triggers.Add(trigger);
        }

        private new void AddAction(IBindingAction action, string device)
        {
            action.Device = device;
            Actions.Add(action);
        }

        private void AddRocker(string name, string imagePrefix, string imageOrientation, double x, double y)
        {
            Helios.Controls.ThreeWayToggleSwitch rocker = new Helios.Controls.ThreeWayToggleSwitch();
            rocker.Name = name;
            rocker.SwitchType = Helios.Controls.ThreeWayToggleSwitchType.MomOnMom;
            rocker.ClickType = Helios.Controls.LinearClickType.Touch;
            rocker.PositionTwoImage = "{AV-8B}/Images/" + imagePrefix + " " + imageOrientation + " Mid.png";

            rocker.Top = y;
            rocker.Left = x;
            switch (imageOrientation)
            {
                case ("V"):
                    //rocker.Orientation = Helios.Controls.ToggleSwitchOrientation.Horizontal;
                    rocker.PositionOneImage = "{AV-8B}/Images/" + imagePrefix + " " + imageOrientation + " Up.png";
                    rocker.PositionThreeImage = "{AV-8B}/Images/" + imagePrefix + " " + imageOrientation + " Dn.png";
                    rocker.Height = 63;
                    rocker.Width = 30;
                    break;
                case ("L"):
                    rocker.PositionOneImage = "{AV-8B}/Images/" + imagePrefix + " " + imageOrientation + " Up.png";
                    rocker.PositionThreeImage = "{AV-8B}/Images/" + imagePrefix + " " + imageOrientation + " Dn.png";
                    rocker.Width = 66;
                    rocker.Height = 57;
                    break;
                case ("R"):
                    rocker.PositionOneImage = "{AV-8B}/Images/" + imagePrefix + " " + imageOrientation + " Up.png";
                    rocker.PositionThreeImage = "{AV-8B}/Images/" + imagePrefix + " " + imageOrientation + " Dn.png";
                    rocker.Width = 66;
                    rocker.Height = 57;
                    break;
                default:
                    break;
            }

            Children.Add(rocker);
            foreach (IBindingTrigger trigger in rocker.Triggers)
            {
                AddTrigger(trigger, name);
            }

            AddAction(rocker.Actions["set.position"], name);
        }

        private void AddThreeWayToggle(string name, double x, double y, Size size)
        {
            Helios.Controls.ThreeWayToggleSwitch toggle = new Helios.Controls.ThreeWayToggleSwitch();
            toggle.Top = y;
            toggle.Left = x;
            toggle.Width = size.Width;
            toggle.Height = size.Height;
            toggle.DefaultPosition = ThreeWayToggleSwitchPosition.Two;
            toggle.Orientation = ToggleSwitchOrientation.Vertical; // this seems to just control the swipe direction
            toggle.Rotation = HeliosVisualRotation.CW;
            toggle.PositionOneImage = "{Helios}/Images/Toggles/orange-round-up.png";
            toggle.PositionTwoImage = "{Helios}/Images/Toggles/orange-round-norm.png";
            toggle.PositionThreeImage = "{Helios}/Images/Toggles/orange-round-down.png";
            toggle.SwitchType = ThreeWayToggleSwitchType.MomOnMom;
            toggle.Name = name;
            Children.Add(toggle);
            foreach (IBindingTrigger trigger in toggle.Triggers)
            {
                AddTrigger(trigger, name);
            }
            foreach (IBindingAction action in toggle.Actions)
            {
                AddAction(action, name);
            }

            //AddTrigger(toggle.Triggers["pushed"], name);
            //AddTrigger(toggle.Triggers["released"], name);

            //AddAction(toggle.Actions["push"], name);
            //AddAction(toggle.Actions["release"], name);
            //AddAction(toggle.Actions["set.physical state"], name);
        }

        public override bool HitTest(Point location)
        {
            if (_scaledScreenRect.Contains(location))
            {
                return false;
            }

            return true;
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
    }
}
