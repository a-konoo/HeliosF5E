﻿// Copyright 2023 Helios Contributors
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
// 
//#define CREATEINTERFACE
namespace GadrocsWorkshop.Helios.Interfaces.DCS.MIRAGEF1.CE
{
    using ComponentModel;
    using Common;
    using GadrocsWorkshop.Helios.Interfaces.DCS.MIRAGEF1.Tools;
    using GadrocsWorkshop.Helios.UDPInterface;
    using System;

    /// <summary>
    /// Interface for DCS Mirage F1CE, including devices which are unique to this variant.
    /// </summary>
    [HeliosInterface("Helios.MIRAGEF1CE", "DCS Mirage F1CE", typeof(DCSInterfaceEditor), typeof(UniqueHeliosInterfaceFactory), UniquenessKey = "Helios.DCSInterface")]
    public class MirageF1CEInterface: MirageF1Interface
    {
        public MirageF1CEInterface() : base(
            "DCS Mirage F1CE",
            "Mirage-F1CE",
            "pack://application:,,,/MirageF1;component/Interfaces/ExportFunctionsMirageF1CE.lua")
        {
#if (CREATEINTERFACE && DEBUG)
            InterfaceCreation ic = new InterfaceCreation();
            foreach (string path in new string[] { $@"{DCSAircraft}\Cockpit\Mirage-F1\Mirage-F1_Common\clickabledata_common_F1CE_BE.lua", $@"{DCSAircraft}\Cockpit\Mirage-F1\Mirage-F1CE\clickabledata.lua" })
            {
                foreach (NetworkFunction nf in ic.CreateFunctionsFromClickable(this, path))
                {
                    AddFunction(nf);
                }
            }
            return;
#endif

            // see if we can restore from JSON
#if (!DEBUG)
                        if (LoadFunctionsFromJson())
                        {
                            return;
                        }
#endif

            // * * * Creating Interface functions from file: Cockpit\Mirage-F1\Mirage-F1_Common\clickabledata_common_F1CE_BE.lua
            #region Navigation indicator
#pragma warning disable CS0162 // Unreachable code detected
            AddFunction(new Switch(this, "1", "1204", SwitchPositions.Create(4, 0d, 0.3333d, "3555", "Posn", "%0.4f"), "Navigation indicator", "Mode selector switch", "%0.4f"));
            AddFunction(new Switch(this, "1", "1205", SwitchPositions.Create(2, 0d, 1d, "3557", "Posn", "%0.1f"), "Navigation indicator", "Normal/Additional vector selector switch", "%0.1f"));
            AddFunction(new Switch(this, "1", "1206", new SwitchPosition[] { new SwitchPosition("1.0", "Posn 1", "3558"), new SwitchPosition("0.0", "Posn 2", "3558") }, "Navigation indicator", "Additional target selector switch", "%0.1f"));
            AddFunction(new Axis(this, "1", "3559", "1207", 0.1d, 0.0d, 1d, "Navigation indicator", "Bearing/Distance selector knob", false, "%0.1f"));
            AddFunction(new PushButton(this, "1", "3560", "1208", "Navigation indicator", "Test button", "%1d"));
            #endregion Navigation indicator
            #region MATRA 550 or Sidewinder jettisoning
            AddFunction(new Switch(this, "1", "962", new SwitchPosition[] { new SwitchPosition("0.0", "Posn 1", "3561"), new SwitchPosition("1.0", "Posn 2", "3561") }, "MATRA 550 or Sidewinder jettisoning", "MATRA 550 or Sidewinder jettison button guard", "%0.1f"));
            AddFunction(new PushButton(this, "1", "3562", "963", "MATRA 550 or Sidewinder jettisoning", "MATRA 550 or Sidewinder jettison button", "%1d"));
            #endregion MATRA 550 or Sidewinder jettisoning
            #region Jammer detection light
            AddFunction(new PushButton(this, "1", "3291", "194", "Jammer detection", "Button Jammer detection light", "%1d"));
            AddFunction(new Axis(this, "1", "3292", "195", 0.5d, 0.0d, 1.0d, "Jammer detection", "Lamp Jammer detection light", false, "%0.1f"));
            #endregion Jammer detection light

            // * * * Creating Interface functions from file: Cockpit\Mirage-F1\Mirage-F1CE\clickabledata.lua
            // No functions added
            base.AddFunctions();
#pragma warning restore CS0162 // Unreachable code detected
        }
    }
}
