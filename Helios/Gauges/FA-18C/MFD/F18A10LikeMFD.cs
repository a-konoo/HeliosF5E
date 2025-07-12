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

using System;
using System.Windows;
using GadrocsWorkshop.Helios.ComponentModel;
using GadrocsWorkshop.Helios.Controls;

// ReSharper disable once CheckNamespace
namespace GadrocsWorkshop.Helios.Gauges.F18A10LikeMFD
{
    [HeliosControl("F18A10LikeMPCD", "MPCD", "F/A-18C", typeof(BackgroundImageRenderer))]
    class MFD_A10 : Gauges.MFD
    {
        private static readonly Rect SCREEN_RECT = new Rect(67, 67, 341, 343);
        private Rect _scaledScreenRect = SCREEN_RECT;

        public MFD_A10()
            : base("MFD", new Size(475, 475))
        {
            AddButton("OSB6", 117, 15);
            AddButton("OSB7", 167, 15);
            AddButton("OSB8", 218, 15);
            AddButton("OSB9", 270, 15);
            AddButton("OSB10", 320, 15);

            AddButton("OSB11", 428, 114);
            AddButton("OSB12", 428, 166);
            AddButton("OSB13", 428, 216);
            AddButton("OSB14", 428, 266);
            AddButton("OSB15", 428, 315);

            AddButton("OSB16", 322, 428);
            AddButton("OSB17", 271, 428);
            AddButton("OSB18", 220, 428);
            AddButton("OSB19", 169, 428);
            AddButton("OSB20", 118, 428);

            AddButton("OSB1", 13, 316);
            AddButton("OSB2", 13, 267);
            AddButton("OSB3", 13, 217);
            AddButton("OSB4", 13, 166);
            AddButton("OSB5", 13, 114);
            AddRotarySwitch("Mode Knob", new Point(10, 427), new Size(35, 35));
            AddKnob("Brightness Knob", new Point(420, 420), new Size(50, 50), 1);
            AddKnob("Contrast Knob", new Point(420, 25), new Size(50, 50), 1);
        }

        #region Properties

        public override string DefaultBackgroundImage
        {
            get { return "{FA-18C}/Images/mfd-bezel_a10like.png"; }
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

        private void AddRotarySwitch(string name, Point posn, Size size)
        {
            Helios.Controls.RotarySwitch _knob = new Helios.Controls.RotarySwitch();
            _knob.Name = name;
            _knob.KnobImage = "{AV-8B}/Images/Common Knob.png";
            _knob.DrawLabels = false;
            _knob.DrawLines = false;
            _knob.Positions.Clear();
            _knob.Positions.Add(new RotarySwitchPosition(_knob, 0, "Off",  0d));
            _knob.Positions.Add(new RotarySwitchPosition(_knob, 1, "Night",50d));
            _knob.Positions.Add(new RotarySwitchPosition(_knob, 2, "Day", 110d));
            _knob.CurrentPosition = 0;
            _knob.DefaultPosition = 0;
            _knob.Top = posn.Y;
            _knob.Left = posn.X;
            _knob.Width = size.Width;
            _knob.Height = size.Height;

            Children.Add(_knob);
            AddTrigger(_knob.Triggers["position.changed"], name);
            AddAction(_knob.Actions["set.position"], name);
        }
        private void AddKnob(string name, Point posn, Size size) { AddKnob(name, posn, size, 0); }
        private void AddKnob(string name, Point posn, Size size, Int16 knobType)
        {
            Helios.Controls.Potentiometer _knob = new Helios.Controls.Potentiometer();
            _knob.Name = name;
            switch (knobType)
            {
                case 1:
                    _knob.KnobImage = "{FA-18C}/Images/MPCD Knob.png";
                    break;
                default:
                    _knob.KnobImage = "{AV-8B}/Images/Common Knob.png";
                    break;
            }
            _knob.InitialRotation = 219;
            _knob.RotationTravel = 291;
            _knob.MinValue = 0;
            _knob.MaxValue = 1;
            _knob.InitialValue = 0;
            _knob.StepValue = 0.1;
            _knob.Top = posn.Y;
            _knob.Left = posn.X;
            _knob.Width = size.Width;
            _knob.Height = size.Height;

            Children.Add(_knob);
            foreach (IBindingTrigger trigger in _knob.Triggers)
            {
                AddTrigger(trigger, name);
            }
            foreach (IBindingAction action in _knob.Actions)
            {
                AddAction(action, name);
            }
        }


        private void AddRocker(string name, string imagePrefix, double x, double y, bool horizontal)
        {
            Helios.Controls.ThreeWayToggleSwitch rocker = new Helios.Controls.ThreeWayToggleSwitch();
            rocker.Name = name;
            rocker.SwitchType = Helios.Controls.ThreeWayToggleSwitchType.MomOnMom;
            rocker.ClickType = Helios.Controls.LinearClickType.Touch;
            rocker.PositionTwoImage = "{Helios}/Images/A-10/" + imagePrefix + "-norm.png";

            rocker.Top = y;
            rocker.Left = x;
            if (horizontal)
            {
                rocker.Orientation = Helios.Controls.ToggleSwitchOrientation.Horizontal;
                rocker.PositionOneImage = "{Helios}/Images/A-10/" + imagePrefix + "-left.png";
                rocker.PositionThreeImage = "{Helios}/Images/A-10/" + imagePrefix + "-right.png";
                rocker.Width = 56;
                rocker.Height = 32;
            }
            else
            {
                rocker.Orientation = Helios.Controls.ToggleSwitchOrientation.Vertical;
                rocker.PositionOneImage = "{Helios}/Images/A-10/" + imagePrefix + "-up.png";
                rocker.PositionThreeImage = "{Helios}/Images/A-10/" + imagePrefix + "-down.png";
                rocker.Width = 32;
                rocker.Height = 56;
            }

            Children.Add(rocker);

            foreach (IBindingTrigger trigger in rocker.Triggers)
            {
                AddTrigger(trigger, name);
            }

            AddAction(rocker.Actions["set.position"], name);
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
