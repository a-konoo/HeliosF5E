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


namespace GadrocsWorkshop.Helios.Gauges.F5E
{
    using GadrocsWorkshop.Helios.ComponentModel;
    using GadrocsWorkshop.Helios.Controls;
    using System.Windows;
    using System.Xml;
    using System.Collections.Generic;
    using System;
    using GadrocsWorkshop.Helios.Controls.F5E;

    [HeliosControl("Helios.F5E.SAIGaugeSet", "SAIGaugeSet", "F-5E", typeof(BackgroundImageRenderer))]
    class SAIGaugeSet : F5EDevice
    {
        private static readonly Rect SCREEN_RECT = new Rect(0, 0, 220, 220);
        private string _interfaceDeviceName = "SAIGaugeSet";
        private string _imageAssetLocation = "{F-5E}/Images/GeneralPurposePullKnob";
        private Rect _scaledScreenRect = SCREEN_RECT;


        public SAIGaugeSet()
            : base("SAIGaugeSet", new Size(240, 240))
        {
            AddSAIGaugeToPanel("Stanby Attitude Indicator", _interfaceDeviceName,
                new Point(0d, 0d), new Size(180, 180));

            AddSAINeedleToPanel("SAINeedle", _interfaceDeviceName,
                new Point(152d, 152d), new Size(70, 70));    
        }

        #region Properties

        public override string DefaultBackgroundImage
        {
            get { return $""; }
        }

        public string ImageAssetLocation
        {
            get => _imageAssetLocation;
            set
            {
                if (value != null && !_imageAssetLocation.Equals(value))
                {
                    string oldValue = _imageAssetLocation;
                    _imageAssetLocation = value;
                    OnPropertyChanged("ImageAssetLocation", oldValue, value, false);
                    Refresh();
                }
            }
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

        private void AddSAIGaugeToPanel(string name,
            string _interfaceDeviceName, Point posn, Size size)
        {

            AddSAIBody(name: name,
                posn: posn,
                fromCenter: false,
                interfaceDeviceName: _interfaceDeviceName,
                interfaceElementName: name,
                actionIdentifier: name,
                size: size);
        }

        private void AddSAINeedleToPanel(string name,
            string _interfaceDeviceName, Point posn, Size size)
        {

            AddSAINeedle(name: name,
                posn: posn,
                fromCenter: false,
                interfaceDeviceName: _interfaceDeviceName,
                interfaceElementName: name,
                actionIdentifier: name,
                size: size);
        }

        private GeneralPurposePullKnob AddImageFlipParts(
            string name,
            string actionIdentifier,
            Point posn, Size size,
            string knobImagePath,
            string knobImagePulledPath,
            string frontImagePath,
            string pullReadyImage,
            string basePath,
            List<Tuple<int, bool, double, double, string, string>> knobPostions,
            double pullJudgeAngle,
            double pullJudgeDistance,
            Point frontPos,
            Point frontAdjust,
            double frontRatio,
            bool prohibitOperate,
            bool pullable)
        {
            GeneralPurposePullKnob part = AddGeneralPurposePullKnob(
                name: name,
                fromCenter: false,
                posn: posn,
                size: size,
                interfaceDeviceName: _interfaceDeviceName,
                interfaceElementName: actionIdentifier,
                knobImagePath: knobImagePath,
                knobImagePulledPath: knobImagePulledPath,
                frontImagePath: frontImagePath,
                pullReadyImage: pullReadyImage,
                basePath: basePath,
                knobPostions: knobPostions,
                frontAdjust: frontAdjust,
                pullJudgeAngle: pullJudgeAngle,
                pullJudgeDistance: pullJudgeDistance,
                frontPos: frontPos,
                prohibitOperate: prohibitOperate,
                frontRatio: frontRatio,
                pullable: pullable);

            return part;
        }

        public override void ReadXml(XmlReader reader)
        {
            base.ReadXml(reader);
        }
        public override void WriteXml(XmlWriter writer)
        {
            base.WriteXml(writer);
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
