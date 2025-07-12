﻿// Copyright 2020 Ammo Goettsch
// 
// Helios is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
// 
// Helios is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
// 
// You should have received a copy of the GNU General Public License
// along with this program.  If not, see <http://www.gnu.org/licenses/>.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;
using GadrocsWorkshop.Helios.ComponentModel;

namespace GadrocsWorkshop.Helios.Interfaces.ControlRouter
{
    [Serializable]
    [XmlRoot("Configuration", Namespace = XML_NAMESPACE)]
    public class ControlRouterModel : HeliosXmlModel
    {
        public const string XML_NAMESPACE = "http://Helios.local/Base/Interfaces/ControlRouter/Configuration";

        [XmlElement("Port")] public List<ControlRouterPort> Ports { get; set; } = new List<ControlRouterPort>();

        // deserialization constructor
        public ControlRouterModel() : base(XML_NAMESPACE)
        {
            // no code
        }

        public void CreateDefaultPorts(int numPorts)
        {
            List<ControlRouterPort> newPorts = Enumerable.Range(0, numPorts).Select(n => new ControlRouterPort($"Port {n}")).ToList();
            newPorts.ForEach(p => p.PropertyChanged += Port_PropertyChanged);
            Ports.AddRange(newPorts);
        }

        private void Port_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            // forward changes to configuration properties as child property events so we get Undo support
            OnPropertyChanged("Port", (PropertyNotificationEventArgs)e);
        }

        protected override void AfterDeserialization()
        {
            base.AfterDeserialization();
            Ports.ForEach(p => p.PropertyChanged += Port_PropertyChanged);
        }
    }
}