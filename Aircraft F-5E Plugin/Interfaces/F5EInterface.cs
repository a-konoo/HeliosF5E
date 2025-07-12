// Copyright 2020 Helios Contributors
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

namespace GadrocsWorkshop.Helios.Interfaces.DCS.F5E
{
    using Common;
    using ComponentModel;
    using GadrocsWorkshop.Helios.Interfaces.DCS.F5E.Functions;
    using System.Windows.Controls.Primitives;
    using System;

    /// <summary>
    /// Interface for original DCS F-5E-3, including devices which are unique to the original A-10C.
    /// </summary>
    [HeliosInterface("Helios.F5E", "DCS F-5E-3", typeof(DCSInterfaceEditor), typeof(UniqueHeliosInterfaceFactory), UniquenessKey = "Helios.DCSInterface")]
    public class F5EInterface : DCSInterface
    {

        #region Devices
        // ReSharper disable InconsistentNaming
        private const string CONTROL_SYS = "2";
        private const string ELEC_SYS = "3";
        private const string FUEL_SYS = "4";
        private const string ENGINE_SYS = "6";
        private const string GEAR_SYS = "7";
        private const string OXYGEN_SYS = "8";
        private const string CABIN_CONTROL_SYS = "9";
        private const string COCKPIT_SYS = "10";
        private const string EXT_LIGHT_SYS = "11";
        private const string INTR_LIGHT_SYS = "12";
        private const string CNTR_MSR_SYS = "13";
        private const string JETSON_SYS = "14";
        private const string FIRE_CONTRL_SYS = "15";
        private const string AHRS_SYS = "16";
        private const string RADER_APQ_SYS = "17";
        private const string SIGHT_SYS = "18";
        private const string RWR_SYS = "19";
        private const string RADER_ALR_SYS = "20";
        //private const string GUN_CAMERA = "21";
        private const string IFF = "22";
        private const string UHF_RADIO_SYS = "23";
        private const string UHF_RADIO_MIC = "24";
        private const string ACC_METER = "27";
        private const string IAS_METER = "28";
        private const string ALTI_METER = "31";
        private const string ATTI_INDIX = "32";
        private const string HSI_SYS = "33";
        private const string STBY_ATTI_INDX = "34";
        private const string CLOCK_SYS = "35";
        private const string TACAN_SYS = "41";
        #endregion

        #region Buttons
        private const string BUTTON_1 = "3001";
        private const string BUTTON_2 = "3002";
        private const string BUTTON_3 = "3003";
        private const string BUTTON_4 = "3004";
        private const string BUTTON_5 = "3005";
        private const string BUTTON_6 = "3006";
        private const string BUTTON_7 = "3007";
        private const string BUTTON_8 = "3008";
        private const string BUTTON_9 = "3009";
        private const string BUTTON_10 = "3010";
        private const string BUTTON_11 = "3011";
        private const string BUTTON_12 = "3012";
        private const string BUTTON_13 = "3013";
        private const string BUTTON_14 = "3014";
        private const string BUTTON_15 = "3015";
        private const string BUTTON_16 = "3016";
        private const string BUTTON_17 = "3017";
        private const string BUTTON_18 = "3018";
        private const string BUTTON_19 = "3019";
        private const string BUTTON_20 = "3020";
        private const string BUTTON_21 = "3021";
        private const string BUTTON_22 = "3022";
        private const string BUTTON_23 = "3023";
        private const string BUTTON_24 = "3024"; 
        #endregion


        public F5EInterface() : base(
            "DCS F-5E",
            "F-5E-3",
            "pack://application:,,,/F-5E;component/Interfaces/ExportFunctions.lua")
        {
            if (LoadFunctionsFromJson())
            {
                return;
            }

            #region Indexers
            AddFunction(new FlagValue(this, "530", "Caution Indicators", "L_GEN", "L Generator Lamp"));
            AddFunction(new FlagValue(this, "531", "Caution Indicators", "CANOPY_OPEN", "Canopy Open"));
            AddFunction(new FlagValue(this, "532", "Caution Indicators", "R_GEN", "R Generator Lamp"));
            AddFunction(new FlagValue(this, "533", "Caution Indicators", "UTIL_HYDRAULIC", "Utility Hydr"));
            AddFunction(new FlagValue(this, "534", "Caution Indicators", "SPARE", "Spare_1"));
            AddFunction(new FlagValue(this, "535", "Caution Indicators", "FLGT_HYDRAULIC", "Flight Hydr"));
            AddFunction(new FlagValue(this, "536", "Caution Indicators", "EXT_TANKS", "External Tanks Empty"));
            AddFunction(new FlagValue(this, "537", "Caution Indicators", "IFF", "IFF"));
            AddFunction(new FlagValue(this, "538", "Caution Indicators", "OXYGEN", "Oxygen"));
            AddFunction(new FlagValue(this, "539", "Caution Indicators", "LEFT_FUEL_LOW", "Left Flow Indicator"));
            AddFunction(new FlagValue(this, "540", "Caution Indicators", "ENGINE_ANTI_ICE", "Anti Ice"));
            AddFunction(new FlagValue(this, "541", "Caution Indicators", "RIGHT_FUEL_LOW", "Right Flow Indicator"));
            AddFunction(new FlagValue(this, "542", "Caution Indicators", "L_FUEL_PRESS", "L Fuel Press"));
            AddFunction(new FlagValue(this, "543", "Caution Indicators", "INS", "Inertial Navigation System"));
            AddFunction(new FlagValue(this, "544", "Caution Indicators", "R_FUEL_PRESS", "R Fuel Press"));
            AddFunction(new FlagValue(this, "545", "Caution Indicators", "AOA_FLAPS", "AoA Flaps"));
            AddFunction(new FlagValue(this, "546", "Caution Indicators", "AIR_DATA_COMPUTER", "Air Data Computer"));
            AddFunction(new FlagValue(this, "547", "Caution Indicators", "DIR_GYRO", "Directional Gyroscope"));
            AddFunction(new FlagValue(this, "548", "Caution Indicators", "SPARE_2", "Spare 2"));
            AddFunction(new FlagValue(this, "549", "Caution Indicators", "DC_OVERLOAD", "DC Overload"));
            AddFunction(new FlagValue(this, "550", "Caution Indicators", "SPARE_3", "Spare 3"));
            AddFunction(new FlagValue(this, "169", "Caution Indicators", "MC_LIGHT", "MasterCaution Lamp"));
            AddFunction(new FlagValue(this, "167", "Caution Indicators", "L_FIRE", "Left Fire Lamp"));
            AddFunction(new FlagValue(this, "168", "Caution Indicators", "R_FIRE", "Right Fire Lamp"));
            AddFunction(new FlagValue(this, "48", "Caution Indicators", "AOA_R", "AOA (red)"));
            AddFunction(new FlagValue(this, "49", "Caution Indicators", "AOA_G", "AOA (green)"));
            AddFunction(new FlagValue(this, "50", "Caution Indicators", "AOA_Y", "AOA (yellow)"));
            #endregion

            #region Control System

            // defineToggleSwitch("YAW_DAMPER", 2, 3001, 323,"Control Interface" , "Yaw Damper Switch, YAW/OFF")
            AddFunction(Switch.CreateToggleSwitch(this, CONTROL_SYS, BUTTON_1, "323",
                "1", "yaw", "0", "off", "Control Interface", "Yaw Damper Switch, YAW/OFF", "%1d"));

            // defineToggleSwitch("PITCH_DAMPER", 2, 3002, 322,"Control Interface" , "Pitch Damper Switch, PITCH/OFF")
            AddFunction(Switch.CreateToggleSwitch(this, CONTROL_SYS, BUTTON_2, "322",
                "1", "pitch", "0", "off", "Control Interface", "Pitch Damper Switch, PITCH/OFF", "%1d"));

            // definePotentiometer("RUDDER_TRIM", 2, 3003, 324, {-1, 1}, "Control Interface", "Rudder Trim Knob")
            AddFunction(new Axis(this, CONTROL_SYS, BUTTON_18, "324", 0.1d, -1.0d, 1.0d, "Control Interface", "Rudder Trim Knob"));
            AddFunction(new PushButton(this, CONTROL_SYS, BUTTON_4, "132", "Control Interface", "Pitch Damper Cutoff Switch"));

            // define3PosTumb("FLAPS", 2, 3005, 116, "Control Interface", "Flaps Lever, EMER UP/THUMB SW/FULL")
            AddFunction(Switch.CreateThreeWaySwitch(this, CONTROL_SYS, BUTTON_5, "116",
                "-1.0", "EmerUp", "0.0", "Thumb", "1.0", "full",
                "Control Interface", "Flaps Lever, EMER UP/THUMB SW/FULL", "%0.1f"));
            //AddFunction(new ScaledNetworkValue(this, "116", 1d,
            //    "Control Interface", "Flaps Lever", "EMER UP/THUMB SW/FULL", "", BindingValueUnits.Numeric));

            // define3PosTumb("A_FLAPS", 2, 3005, 116, "Control Interface", "Auto Flap Switch, UP/FIXED/AUTO")
            AddFunction(Switch.CreateThreeWaySwitch(this, CONTROL_SYS, BUTTON_6, "115",
                "-1.0", "up", "0.0", "fixed", "1.0", "auto",
                "Control Interface", "Auto Flap Switch, UP/FIXED/AUTO", "%0.1f"));
            //AddFunction(new ScaledNetworkValue(this, "115", 1d,
            //    "Control Interface", "Auto Flap Switch", "UP/FIXED/AUTO", "", BindingValueUnits.Numeric));
            // define3PosTumb("SPEED", 2, 3007, 101, "Control Interface", "Speed Brake Switch, OUT/OFF/IN")
            AddFunction(Switch.CreateThreeWaySwitch(this, CONTROL_SYS, BUTTON_7, "101",
                "-1.0", "out", "0.0", "off", "1.0", "in",
                "Control Interface", "Speed Brake Switch, OUT/OFF/IN", "%0.1f"));

            // defineToggleSwitch("RUDDER_T", 2, 3014, 278, "Control Interface" , "Rudder Pedal Adjust T-Handle, PULL/STOW")
            AddFunction(Switch.CreateToggleSwitch(this, CONTROL_SYS, BUTTON_8, "278",
                "1", "pull", "0", "stow", "Control Interface", "Rudder Pedal Adjust T-Handle, PULL/STOW", "%1d"));

            // defineFloat("TRIM_INDICATOR", 52, {1.0, 0.0, -0.1}, "Gauges", "Trim Position")
            AddFunction(new ScaledNetworkValue(this, "52", 1d, "Control Interface", "Trim Index", "Trim Position", "", BindingValueUnits.Numeric));

            // defineFloat("FLAP_INDICATOR", 51, {0.0, 0.4}, "Gauges", "Flap Indicator")
            AddFunction(new ScaledNetworkValue(this, "51", 30d, "Control Interface", "Flap Index", "Flap Indicator", "", BindingValueUnits.Numeric));

            // defineFloat("SLIPBALL", 3, { -1.0, 1.0}, "Gauges", "Slipball")
            AddFunction(new ScaledNetworkValue(this, "3", -1.0, "Gauges", "Slipball", "Slipball.", "(-1 to 1)", BindingValueUnits.Numeric));

            AddFunction(new ScaledNetworkValue(this, "7", 30d, "Control Interface",
                "AOA Indicator", "AOA Indicator", "", BindingValueUnits.Degrees));

            // defineFloat("VARIOMETER", 24, { -1.0, -0.64, -0.5, -0.29, 0.0, 0.29, 0.5, 0.64, 1.0}
            //  "Gauges", "Variometer")
            AddFunction(new ScaledNetworkValue(this, "24", 6000d, "Control Interface",
                "VVI Variometer", "VVI Variometer", "", BindingValueUnits.Numeric));

            #endregion


            #region Electric system
            // defineToggleSwitch("SW_BATTERY", 3, 3001, 387,"Electric" , "Battery Switch")
            AddFunction(Switch.CreateToggleSwitch(this, ELEC_SYS, BUTTON_1, "387", "1", "on", "0", "off", "Electric", "Battery Switch", " %1d"));

            // defineToggleSwitch("L_GENERATOR", 3, 3002, 388,"Electric" , "Generator Left")
            AddFunction(Switch.CreateToggleSwitch(this, ELEC_SYS, BUTTON_2, "388", "1", "on", "0", "off", "Electric", "Generator Left", "%1d"));

            // defineToggleSwitch("R_GENERATOR", 3, 3004, 389,"Electric" , "Generator Right")
            AddFunction(Switch.CreateToggleSwitch(this, ELEC_SYS, BUTTON_3, "389", "1", "on", "0", "off", "Electric", "Generator Right", "%1d"));

            // defineToggleSwitch("PITOT_HEATER", 3, 3006, 375,"Electric" , "Pitot Heater")
            AddFunction(Switch.CreateToggleSwitch(this, ELEC_SYS, BUTTON_4, "375", "1", "on", "0", "off", "Electric", "Pitot Heater Switch", "%1d"));

            // define3PosTumb("F_O2_SW", 3, 3007, 230, "Electric", "GaugeTest Switch, GAGE TEST/OFF/QTY CHECK")

            AddFunction(new Switch(this, ELEC_SYS, "230",
                    new SwitchPosition[] {
                    new SwitchPosition("1.0", "Test", BUTTON_7),
                    new SwitchPosition("0.0", "Off", BUTTON_7),
                    new SwitchPosition("-1.0", "Qty Check", BUTTON_8)},
                "Electric", "Gage Test/Norm/Qty Check", "%0.1f"));

            #endregion

            #region Fuel system
            // defineToggleSwitch("L_SHUTOFF_S", 4, 3001, 360,"Fuel" , "Left Fuel Shutoff Switch, OPEN/CLOSED")
            AddFunction(Switch.CreateToggleSwitch(this, FUEL_SYS, BUTTON_1, "360",
                "1", "on", "0", "off", "Fuel System", "Left Fuel Shutoff Switch, OPEN/CLOSED", " %1d"));

            // defineToggleSwitch("R_SHUTOFF_S", 4, 3002, 362,"Fuel" , "Right Fuel Shutoff Switch, OPEN/CLOSED")
            AddFunction(Switch.CreateToggleSwitch(this, FUEL_SYS, BUTTON_2, "362",
                "1", "on", "0", "off", "Fuel System", "Right Fuel Shutoff Switch, OPEN/CLOSED", "%1d"));

            // defineToggleSwitch("EXT_PYLON_SW", 4, 3003, 378,"Fuel" , "Ext Fuel Pylons Switch, ON/OFF")
            AddFunction(Switch.CreateToggleSwitch(this, FUEL_SYS, BUTTON_3, "378",
                "1", "on", "0", "off", "Fuel System", "Ext Fuel Pylons Switch", "%1d"));

            // defineToggleSwitch("EXT_CENTER_SW", 4, 3004, 377,"Fuel" , "Ext Fuel Cl Switch, ON/OFF")
            AddFunction(Switch.CreateToggleSwitch(this, FUEL_SYS, BUTTON_4, "377",
                "1", "on", "0", "off", "Fuel System", "Ext Fuel CL Switch", "%1d"));

            // defineToggleSwitch("CROSSFEED", 4, 3005, 381,"Fuel" , "Crossfeed Switch, OPEN/CLOSED")
            AddFunction(Switch.CreateToggleSwitch(this, FUEL_SYS, BUTTON_5, "381",
                "1", "on", "0", "off", "Fuel System", "Crossfeed Switch, OPEN / CLOSED", " %1d"));


            // in command_defs.lua

            // ExtFuelTransfer_Pylon = counter();     // 3003
            // ExtFuelTransfer_Cl = counter();        // 3004
            // FuelCrossfeed = counter();             // 3005
            // FuelAutoLeft = counter();              // 3006
            // FuelAutoRight = counter();             // 3007

            // define3PosTumb("AUTOBAL", 4, 3006, 383, "Fuel", "Autobalance Switch, LEFT/NEUT/RIGHT")

            AddFunction(new Switch(this, FUEL_SYS, "383",
                    new SwitchPosition[] {
                    new SwitchPosition("-1.0", "LEFT", BUTTON_6),
                    new SwitchPosition("0.0", "NEUT", BUTTON_7),
                    new SwitchPosition("1.0", "RIGHT", BUTTON_7)},
                "Fuel System", "autobalance Switch", "%0.1f"));

            // defineToggleSwitch("L_BOOSTPUMP", 4, 3008, 380,"Fuel" , "Left Boost Pump Switch, ON/OFF")
            AddFunction(Switch.CreateToggleSwitch(this, FUEL_SYS, BUTTON_8, "380",
                "1", "on", "0", "off", "Fuel System", "Left Boost Pump Switch, ON/OFF", " %1d"));

            // defineToggleSwitch("R_BOOSTPUMP", 4, 3009, 382,"Fuel" , "Right Boost Pump Switch, ON/OFF")
            AddFunction(Switch.CreateToggleSwitch(this, FUEL_SYS, BUTTON_9, "382",
                "1", "on", "0", "off", "Fuel System", "Right Boost Pump Switch, ON/OF", " %1d"));

            // defineToggleSwitch("L_SHUTOFF_C", 4, 3010, 359,"Fuel" , "Left Fuel Shutoff Switch Cover, OPEN/CLOSED")
            AddFunction(Switch.CreateToggleSwitch(this, FUEL_SYS, BUTTON_10, "359",
                "1", "on", "0", "off", "Fuel System", "Left Fuel Shutoff Switch Cover, OPEN/CLOSED", " %1d"));

            // defineToggleSwitch("R_SHUTOFF_C", 4, 3011, 361,"Fuel" , "Right Fuel Shutoff Switch Cover, OPEN/CLOSED")
            AddFunction(Switch.CreateToggleSwitch(this, FUEL_SYS, BUTTON_11, "361",
                "1", "on", "0", "off", "Fuel System", "Right Fuel Shutoff Switch Cover, OPEN/CLOSED", " %1d"));

            // defineFloat("FUELQUANTITY_L", 22, {0.0, 1.0}, "Gauges", "Fuel Quantity Left");
            AddFunction(new ScaledNetworkValue(this, "22", 24000d,
                "Fuel System", "Fuel Quantity Left", "Fuel Quantity Left", "", BindingValueUnits.Numeric));

            // defineFloat("FUELQUANTITY_R", 23, {0.0, 1.0}, "Gauges", "Fuel Quantity Right")
            AddFunction(new ScaledNetworkValue(this, "23", 24000d,
                "Fuel System", "Fuel Quantity Right", "Fuel Quantity Right", "", BindingValueUnits.Numeric));

            // defineFloat("FUELFLOW_L", 525, {0.0,   0.67,   0.75,    0.83,     1.0}, "Gauges", "Fuel Flow Left")
            AddFunction(new ScaledNetworkValue(this, "525", 15000d,
                "Left Engine", "Fuel Flow", "Fuel flow to the engine", "", BindingValueUnits.Numeric));

            // defineFloat("FUELFLOW_R", 526, {0.0,   0.67,   0.75,    0.83,     1.0}, "Gauges", "Fuel Flow Right")
            AddFunction(new ScaledNetworkValue(this, "526", 15000d,
                "Right Engine", "Fuel Flow", "Fuel flow to the engine", "", BindingValueUnits.Numeric));

            AddFunction(new AutoBalanceSwitch(this));

            #endregion

            #region Engine Control
            // definePushButton("L_START", 6, 3001, 357, "Engine Interface", "Left Engine Start Button")
            AddFunction(new PushButton(this, ENGINE_SYS, BUTTON_1, "357", "Engine Interface", "Left Engine Start"));

            // definePushButton("R_START", 6, 3002, 358, "Engine Interface", "Right Engine Start Button")
            AddFunction(new PushButton(this, ENGINE_SYS, BUTTON_2, "358", "Engine Interface", "Right Engine Start"));

            // defineToggleSwitch("INLET_HEATER", 6, 3003, 376, "Engine Interface", "Engine Anti-Ice Switch, ENGINE/OFF")
            AddFunction(Switch.CreateToggleSwitch(this, ENGINE_SYS, BUTTON_3, "376", "1", "on", "0", "off",
                "Control Interface", "Engine Anti-Ice Switch", "%1d"));

            // defineFloat("NOZZLEPOS_L", 107, { 0.0, 1.0}, "Gauges", "Nozzle Position Left")
            AddFunction(new ScaledNetworkValue(this, "107", 100d, "Engine Interface",
                "Nozzle Position Left", "Nozzle Position Left", "", BindingValueUnits.Numeric));

            // defineFloat("NOZZLEPOS_R", 108, { 0.0, 1.0}, "Gauges", "Nozzle Position Right")
            AddFunction(new ScaledNetworkValue(this, "108", 100d, "Engine Interface",
                "Nozzle Position Right", "Nozzle Position Right", "", BindingValueUnits.Numeric));

            // defineFloat("AUXINT_DOOR", 111, { 0.0, 0.2}, "Gauges", "Aux Intake Doors")
            AddFunction(new ScaledNetworkValue(this, "111", 1d, "Engine Interface",
                "Aux Intake Doors", "Aux Intake Doors", "", BindingValueUnits.Numeric));
            // defineFloat("OILPRESS_L", 112, { 0.0, 1.0}, "Gauges", "Oil Pressure Left")
            AddFunction(new ScaledNetworkValue(this, "112", 100d, "Engine Interface",
                "Oil Pressure Left", "Oil pressure in engine", "", BindingValueUnits.PoundsPerSquareInch));

            // defineFloat("OILPRESS_R", 113, { 0.0, 1.0}, "Gauges", "Oil Pressure Right")
            AddFunction(new ScaledNetworkValue(this, "113", 100d, "Engine Interface",
                "Oil Pressure Right", "Oil pressure in engine", "", BindingValueUnits.PoundsPerSquareInch));

            // defineFloat("EXHAUST_TEMP_L", 12, { 0.0,  0.03,  0.1, 0.274,  0.78, 1.0}, "Gauges", "Exhaust Gas Temp Left")
            AddFunction(new ScaledNetworkValue(this, "12", 1000d, "Engine Interface",
                "Exhaust Gas Temp Left", "Exhaust Gas Temp Left", "", BindingValueUnits.Celsius));
            // defineFloat("EXHAUST_TEMP_R", 14, { 0.0,  0.03,  0.1, 0.274,  0.78, 1.0}, "Gauges", "Exhaust Gas Temp Right")

            AddFunction(new ScaledNetworkValue(this, "14", 1000d, "Engine Interface",
                "Exhaust Gas Temp Right", "Exhaust Gas Temp Right", "", BindingValueUnits.Celsius));

            AddFunction(new ScaledNetworkValue(this, "16", 100d, "Engine Interface",
                "Left RPM Percent MainGauge", "Left RPM Percent MainGauge", "", BindingValueUnits.RPMPercent));

            AddFunction(new ScaledNetworkValue(this, "425", 100d, "Engine Interface",
                "Left RPM Percent SubGauge", "Left RPM Percent SubGauge", "", BindingValueUnits.RPMPercent));

            AddFunction(new ScaledNetworkValue(this, "17", 100d, "Engine Interface",
                "Right RPM Percent MainGauge", "Right RPM Percent MainGauge", "", BindingValueUnits.RPMPercent));

            AddFunction(new ScaledNetworkValue(this, "426", 100d, "Engine Interface",
                "Right RPM Percent SubGauge", "Right RPM Percent SubGauge", "", BindingValueUnits.RPMPercent));

            // defineFloat("UTIL_PRESS", 109, { 0.0, 1.0}, "Gauges", "Utility Pressure")
            AddFunction(new ScaledNetworkValue(this, "109", 1d, "Utility Pressure",
                "Utility Pressure", "Utility Pressure", "(0-1)", BindingValueUnits.PoundsPerSquareInch));

            // defineFloat("FLIGHT_PRESS", 110, { 0.0, 1.0}, "Gauges", "Flight Pressure")
            AddFunction(new ScaledNetworkValue(this, "110", 1d, "Flight Pressure",
                "Flight Pressure", "Flight Pressure", "(0-1)", BindingValueUnits.PoundsPerSquareInch));

            // Throttle Position Func
            AddFunction(new ThrottlePosition(this));

            #endregion

            #region Gear System
            // defineIndicatorLight("GW_LIGHT", 96, "Gear Interface", "Gear Warning Lamp (red)")
            AddFunction(new FlagValue(this, "96", "Gear Interface", "GW_LIGHT", "Gear Warning Lamp (red)"));

            // defineIndicatorLight("NOSE_LIGHT", 54, "Gear Interface", "Gear Nose Lamp (green)")
            AddFunction(new FlagValue(this, "54", "Gear Interface", "NOSE_LIGHT", "Gear Nose Lamp (green)"));

            // defineIndicatorLight("LEFT_LIGHT", 53, "Gear Interface", "Gear Left Lamp (green)")
            AddFunction(new FlagValue(this, "53", "Gear Interface", "LEFT_LIGHT", "Gear Left Lamp (green)"));

            // defineIndicatorLight("RIGHT_LIGHT", 55, "Gear Interface", "Gear Right Lamp (green)")
            AddFunction(new FlagValue(this, "55", "Gear Interface", "RIGHT_LIGHT", "Gear Right Lamp (green)"));

            // defineIndicatorLight("HOOK_LIGHT", 90, "Gear Interface", "Hook Warning Lamp (red)")
            AddFunction(new FlagValue(this, "90", "Gear Interface", "HOOK_LIGHT", "Hook Warning Lamp (red)"));

            // defineToggleSwitch("LG_LEVER_SWITCH", 7, 3001, 83, "Gear Interface", "Gear Lever")
            AddFunction(Switch.CreateToggleSwitch(this, GEAR_SYS, BUTTON_1, "83",
                "0", "down", "1", "up", "Gear Interface", "Gear Lever", "%1d"));

            // definePushButton("LG_ALT_REL", 7, 3002, 95, "Gear Interface",
            //    "Landing Gear Alternate Release Handle, Pull and Hold")
            AddFunction(new PushButton(this, GEAR_SYS, BUTTON_2, "95",
                "Gear Interface", "Landing Gear Alternate Release Handle"));

            // definePushButton("LG_DOWN_L", 7, 3003, 88, "Gear Interface", 
            //    "Landing Gear Downlock Override Button - Push and Hold")
            AddFunction(new PushButton(this, GEAR_SYS, BUTTON_3, "88",
                "Gear Interface", "Landing Gear Downlock Override Button"));

            // definePushButton("NWS", 7, 3004, 131, "Gear Interface", 
            //   "Nosewheel Steering Button - Pull and Hold")
            AddFunction(new PushButton(this, GEAR_SYS, BUTTON_4, "131",
                "Gear Interface", "Nosewheel Steering Button"));

            // defineToggleSwitch("LG_ALT_RES", 7, 3005, 98, "Gear Interface",
            //    "Landing Gear Alternate Release Reset Control, OFF/RESET")
            AddFunction(Switch.CreateToggleSwitch(this, GEAR_SYS, BUTTON_5, "98",
                "1", "off", "0", "reset", "Gear Interface",
                "Landing Gear Alternate Release Reset Control", "%1d"));

            // defineToggleSwitch("NS_STRUCT", 7, 3006, 250, "Gear Interface",
            //    "Nose Strut Switch, EXTEND/RETRACT")
            AddFunction(Switch.CreateToggleSwitch(this, GEAR_SYS, BUTTON_6, "250",
                "1", "extend", "0", "retract", "Gear Interface",
                "Nose Strut Switch", "%1d"));

            // definePushButton("LGF_SILENCE", 7, 3007, 87,
            //   "Gear Interface", "Landing Gear And Flap Warning Silence Button")
            AddFunction(new PushButton(this, GEAR_SYS, BUTTON_7, "87",
                "Gear Interface", "Landing Gear And Flap Warning Silence Button"));

            // definePushButton("LG_TEST", 7, 3008, 92, "Gear Interface", "Left Landing Gear Test Lamp")]
            AddFunction(new PushButton(this, GEAR_SYS, BUTTON_8, "92",
                "Gear Interface", "Left Landing Gear Test Lamp"));

            // definePushButton("NG_TEST", 7, 3009, 93, "Gear Interface", "Nose Landing Gear Test Lamp")
            AddFunction(new PushButton(this, GEAR_SYS, BUTTON_9, "93",
                "Gear Interface", "Nose Landing Gear Test Lamp"));

            // definePushButton("RG_TEST", 7, 3010, 94, "Gear Interface",
            //   "Right Landing Gear Test Lamp")
            AddFunction(new PushButton(this, GEAR_SYS, BUTTON_10, "94",
                "Gear Interface", "Right Landing Gear Test Lamp"));

            // definePushButton("HOOK", 7, 3014, 89, "Gear Interface", "Arresting Hook Button")
            AddFunction(new PushButton(this, GEAR_SYS, BUTTON_14, "89",
                "Gear Interface", "Arresting Hook Button"));
            AddFunction(new GearLever(this));
            #endregion

            #region Oxygen System

            // defineToggleSwitch("O2_LEVER", 8, 3001, 603,
            //      "O2 Interface", "Oxygen Supply Lever, ON/OFF")
            AddFunction(Switch.CreateToggleSwitch(this, OXYGEN_SYS, BUTTON_1, "603",
                "1", "on", "0", "off", "O2 Interface",
                "Oxygen Supply Lever", "%1d"));

            // defineToggleSwitch("DILUTER_LEVER", 8, 3002, 602,
            //      "O2 Interface", "Diluter Lever")
            AddFunction(Switch.CreateToggleSwitch(this, OXYGEN_SYS, BUTTON_2, "602",
                "1", "on", "0", "off", "Diluter Lever",
                "Diluter Lever", "%1d"));

            // define3PosTumb("EMER_L", 8, 3003, 601, "O2 Interface",
            //      "Emergency Lever, EMERGENCY/NORMAL/TEST MASK")
            //defineFloat("O2QUANTITY", 390, { 0.0, 1.0}, "Gauges", "O2 Quantity")
            /*
            AddFunction(Switch.CreateThreeWaySwitch(this, OXYGEN_SYS, BUTTON_3, "601",
                "1.0", "emergency", "0.0", "normal", "-1.0", "testmask",
                "O2 Interface", "Emergency Lever", "%0.1f"));

            AddFunction(new Axis(this, OXYGEN_SYS, BUTTON_4, "601",
                0.1d, -1.0d, 0.0d, "O2 Interface", "Emergency Lever(Test)"));
            */
            AddFunction(new Switch(this, OXYGEN_SYS, "601",
                new SwitchPosition[] {
                    new SwitchPosition("1.0", "emergency", BUTTON_3),     // *** important
                    new SwitchPosition("0.0", "normal", BUTTON_3),
                    new SwitchPosition("-1.0", "testmask", BUTTON_4)},
             "O2 Interface", "Emergency Lever, Emer,Normal,test", "%1d"));

            AddFunction(new ScaledNetworkValue(this, "390", 1d, "O2 Interface",
                "O2 Quantity", "O2 Quantity", "", BindingValueUnits.Numeric));

            // defineFloat("O2FLOWINDICATOR", 600, { 0.0, 1.0}, "Gauges", "O2 Flow Blinker")
            AddFunction(new ScaledNetworkValue(this, "600", 1d, "O2 Interface",
                "O2 Flow Blinker", "O2 Flow Blinker", "", BindingValueUnits.Numeric));

            // defineFloat("O2FLOWPRESSURE", 604, { 0.0, 0.5, 1.0}, "Gauges", "O2 FlowPressure")
            AddFunction(new ScaledNetworkValue(this, "604", 1d, "O2 Interface",
                "O2 FlowPressure", "O2 FlowPressure", "", BindingValueUnits.Numeric));
            #endregion

            #region EC System
            // define3PosTumb("CABIN_P_SW", 9, 3001, 371, "EC Interface",
            //   "Cabin Press Switch, DEFOG ONLY/NORMAL/RAM DUMP")
            AddFunction(Switch.CreateThreeWaySwitch(this, CABIN_CONTROL_SYS, BUTTON_1, "371",
                "1.0", "defog", "0.5", "normal", "0.0", "ram dump",
                "EC Interface", "Cabin Press Switch", "%0.1f"));

            // defineToggleSwitch("CABIN_P_C", 9, 3002, 370, "EC Interface",
            //   "Cabin Press Switch Cover, OPEN/CLOSED")
            AddFunction(Switch.CreateToggleSwitch(this, CABIN_CONTROL_SYS, BUTTON_2, "370",
                "1", "open", "0", "closed", "EC Interface",
                "Cabin Press Switch Cover", "%1d"));

            // defineMultipositionSwitch("CABIN_TEMP_SW", 9, 3003, 372,
            //    3, 0.1, "EC Interface", "Cabin Temp Switch, AUTO/CENTER/MAN COLD/MAN HOT")

            AddFunction(new Axis(this, CABIN_CONTROL_SYS, BUTTON_3, "372",
                0.1d, 0.0d, 1.0d, "EC Interface", "Cabin Temp 3wayToggle"));

            // definePotentiometer("CABIN_TEMP", 9, 3004, 373, { -1, 1},
            //   "EC Interface" , "Cabin Temperature Knob")
            AddFunction(new Axis(this, CABIN_CONTROL_SYS, BUTTON_4, "373",
                0.1d, -1.0d, 1.0d, "EC Interface", "Cabin Temperature Knob"));

            // definePotentiometer("CABIN_DEFOG", 9, 3005, 374, { 0, 1},"EC Interface" ,
            //  "Canopy Defog Knob")
            AddFunction(new Axis(this, CABIN_CONTROL_SYS, BUTTON_5, "374",
                0.1d, 0.0d, 1.0d, "EC Interface", "Canopy Defog Knob"));

            AddFunction(new FrontKnobs(this));
            #endregion

            #region Cockpit Mechanics
            // defineToggleSwitch("CANOPY_LEVER", 10, 3001, 712, "Cockpit", "Canopy Handle, OPEN/CLOSE")
            AddFunction(Switch.CreateToggleSwitch(this, COCKPIT_SYS, BUTTON_1, "712",
                "1", "on", "0", "close", "Cockpit",
                "Canopy Handle", "%1d"));

            // define3PosTumb("DRAG_CHUTE", 10, 3002, 91, "Cockpit", "Chute Handle")
            //AddFunction(Switch.CreateThreeWaySwitch(this, COCKPIT_SYS, BUTTON_2, "91",
            //    "1.0", "chute", "0.5", "dragging", "0.0", "retract",
            //    "Cockpit", "Chute Handle", "%0.1f"));
            AddFunction(new Switch(this, COCKPIT_SYS, "91",
                new SwitchPosition[] {
                    new SwitchPosition("0.0", "retract", BUTTON_2),
                    new SwitchPosition("0.5", "dragging", BUTTON_2),
                    new SwitchPosition("1.0", "drop", BUTTON_2) },
                "Cockpit", "Chute Handle", "%0.1f"));
            // defineToggleSwitch("CANOPY_JETTISON", 10, 3005, 384, "Cockpit",
            //   "Canopy Jettison T-Handle, PULL/PUSH")
            AddFunction(Switch.CreateToggleSwitch(this, COCKPIT_SYS, BUTTON_3, "384",
                "1", "pull", "0", "push", "Cockpit",
                "Canopy Jettison T-Handle", "%1d"));

            // defineFloat("CABIN_PRESS", 114, { 0.0, 1.0}, "Gauges", "Cabin Pressure")
            AddFunction(new ScaledNetworkValue(this, "114", 50000d, "Cockpit",
                "Cabin Pressure", "Cabin Pressure", "", BindingValueUnits.Numeric));

            AddFunction(new DragChuteHandle(this));
            #endregion

            #region Countermeasures Dispensing System(CMDS)
            // Countermeasures Dispensing System
            // defineTumb("CHAFF_MODE", 13, 3001, 400, 0.1, { 0.0, 0.3}, nil, false,
            //   "CMDS", "Chaff Mode Selector, OFF/SINGLE/PRGM/MULT")
            AddFunction(new Axis(this, CNTR_MSR_SYS, BUTTON_1, "400", 0.1,0d, 1.0d,
                "CMDS", "Chaff Mode Selector"));

            // defineTumb("FLARE_MODE_SEL", 13, 3002, 404, 0.1, { 0.0, 0.2}, nil, false,
            //   "CMDS", "Flare Mode Selector, OFF/SINGLE/PRGM")

            AddFunction(new Axis(this, CNTR_MSR_SYS, BUTTON_2, "404", 0.1, 0d, 1.0d,
                "CMDS", "Flare Mode Selector"));

            // defineToggleSwitch("FL_JETT_COVER", 13, 3003, 408, "CMDS",
            //   "Flare Jettison Switch Cover, OPEN/CLOSED")
            AddFunction(Switch.CreateToggleSwitch(this, CNTR_MSR_SYS, BUTTON_3, "408",
                "1", "open", "0", "closed", "CMDS", "Flare Jettison Switch Cover, OPEN/CLOSED", "%1d"));

            // defineToggleSwitch("FL_JETT_SW", 13, 3004, 409, "CMDS", "Flare Jettison Switch, OFF/UP")
            AddFunction(Switch.CreateToggleSwitch(this, CNTR_MSR_SYS, BUTTON_4, "409",
                "0", "off", "1", "up", "CMDS", "Flare Jettison Switch, OFF/UP", "%1d"));

            // definePushButton("CHAFF_COUNT", 13, 3005, 403, "CMDS", "Chaff Counter Reset Button - Push to reset")
            AddFunction(new PushButton(this, CNTR_MSR_SYS, BUTTON_5, "403", "CMDS", "Chaff Counter Reset Button"));

            // definePushButton("FL_COUNT", 13, 3006, 407, "CMDS", "Flare Counter Reset Button - Push to reset")
            AddFunction(new PushButton(this, CNTR_MSR_SYS, BUTTON_6, "407", "CMDS", "Flare Counter Reset Button"));

            // definePushButton("FL_CHAFF_BT", 13, 3007, 117, "CMDS", "Flare-Chaff Button - Push to dispense")
            AddFunction(new PushButton(this, CNTR_MSR_SYS, BUTTON_7, "117", "CMDS", "CMDS dispence"));

            // implimented 2digit counter for chaff and flare

            // defineFloat("CHAFF_COUNT_10", 401, { 0.0, 1.0}, "Gauges", "Chaff Drum Counter 10")
            // AddFunction(new ScaledNetworkValue(this, "401", 1d, "CMDS", 
            //    "Chaff Drum Counter 10", "Chaff Drum Counter 10", "", BindingValueUnits.Numeric));

            // defineFloat("CHAFF_COUNT_1", 402, { 0.0, 1.0}, "Gauges", "Chaff Drum Counter 1")
            // AddFunction(new ScaledNetworkValue(this, "402", 1d, "CMDS", 
            //    "Chaff Drum Counter 1", "Chaff Drum Counter 1", "", BindingValueUnits.Numeric));

            // defineFloat("FL_COUNT_10", 405, { 0.0, 1.0}, "Gauges", "Flare Drum Counter 10")
            // AddFunction(new ScaledNetworkValue(this, "405", 1d, "CMDS", 
            //    "Flare Drum Counter 10", "Flare Drum Counter 10", "", BindingValueUnits.Numeric));

            // defineFloat("FL_COUNT_1", 406, { 0.0, 1.0}, "Gauges", "Flare Drum Counter 1")
            //AddFunction(new ScaledNetworkValue(this, "406", 1d, "CMDS", 
            //    "Flare Drum Counter 1", "Flare Drum Counter 1", "", BindingValueUnits.Numeric));
            // replaced this and export 2digit count
            AddFunction(new CMDSTwoCounter(this));
            AddFunction(new CMDSPanelToggle(this));

            #endregion

            #region AHRS_SYS

            // definePushButton("AHRS_ERECT", 16, 3001, 166, "AHRS", "Fast Erect Button - Push to erect")
            AddFunction(new PushButton(this, AHRS_SYS, BUTTON_1, "166",
                "AHRS", "Fast Erect Button - Push to erect"));

            // define3PosTumb("AHRS_FAST_SLAVE", 16, 3003, 220, "AHRS", "Compass Switch, DIR GYRO/MAG/FAST SLAVE")
            // ** i found command_defs.lua and default.lua AHRS_CMD_COMPASS_EXT ***

            // in command_defs.lua  
            // AHRS_CMD_FAST_ERECT = counter();                 // 3001
            // AHRS_CMD_COMPASS = counter(); /// <-- Dir Gyro   // 3002 <- 2way but 3way toggle
            // AHRS_CMD_COMPASS_FAST_SLAVE = counter();         // 3003 <- 2way but 3way toggle
            // AHRS_NAV_MODE = counter();                       // 3004
            AddFunction(new Switch(this, AHRS_SYS, "220",
                    new SwitchPosition[] {
                    new SwitchPosition("1.0", "Dir Gyro", BUTTON_2),     // *** important
                    new SwitchPosition("0.0", "Mag", BUTTON_3),
                    new SwitchPosition("-1.0", "Fast Slave", BUTTON_3)},
                "AHRS", "Compass Switch, DIR GYRO/MAG/FAST SLAVE", "%0.1f"));
            //AddFunction(new Axis(this, AHRS_SYS, BUTTON_2, "220", 0.1d, -1.0d, 1.0d, "AHRS", "C1"));


            // defineTumb("AHRS_NAV_MODE", 16, 3004, 273, 0.1, { 0.0, 0.1}, nil, false, "AHRS", "Nav Mode Selector Switch, DF/TACAN")
            AddFunction(new Switch(this, AHRS_SYS, "273",
                new SwitchPosition[] {
                new SwitchPosition("0.0", "DF", BUTTON_4),
                new SwitchPosition("0.1", "TACAN", BUTTON_4)},
                "AHRS", "Nav Mode Selector Switch, DF/TACAN", "%0.1f"));
            #endregion

            #region Jettison System
            // Jettison System

            // defineToggleSwitch("EMER_JETT_COVER", 14, 3001, 364, "Jettison", "Emergency All Jettison Button Cover, OPEN")
            AddFunction(Switch.CreateToggleSwitch(this, JETSON_SYS, BUTTON_1, "364",
                "0", "off", "1", "up", "Jettison System", "All Jettison Button Cover", "%1d"));

            // definePushButton("EMER_JETT_B", 14, 3002, 365, "Jettison", "Emergency All Jettison Button - Push to jettison")
            AddFunction(new PushButton(this, JETSON_SYS, BUTTON_2, "365", "Jettison System", "All Jettison Button"));

            // define3PosTumb("JETT_SW", 14, 3003, 367, "Jettison", "Select Jettison Switch, SELECT POSITION/OFF/ALL PYLONS")

            AddFunction(new Axis(this, JETSON_SYS, BUTTON_3, "367",
                0.1d, -1.0d, 1.0d, "Jettison System", "Select Jettison Switch(select/off/all)"));

            // definePushButton("JETT_B", 14, 3004, 366, "Jettison", "Select Jettison Button - Push to jettison")
            AddFunction(new PushButton(this, JETSON_SYS, BUTTON_4, "366", "Jettison System", "Select Jettison Button"));

            AddFunction(new JettisonPanel(this));

            #endregion

            #region Weapons Control
            // defineToggleSwitch("ARMPOS1", 15, 3001, 346, "Weapons Control", "Armament Position Selector - L WINGTIP, ON/OFF")
            AddFunction(Switch.CreateToggleSwitch(this, FIRE_CONTRL_SYS, BUTTON_1,
                "346", "1", "on", "0", "off", "Weapons Control", "L WINGTIP, ON/OFF", " %1d"));

            // defineToggleSwitch("ARMPOS2", 15, 3002, 347, "Weapons Control", "Armament Position Selector - L OUTBD, ON/OFF")
            AddFunction(Switch.CreateToggleSwitch(this, FIRE_CONTRL_SYS, BUTTON_2,
                "347", "1", "on", "0", "off", "Weapons Control", "L OUTBD, ON/OFF", " %1d"));

            // defineToggleSwitch("ARMPOS3", 15, 3003, 348, "Weapons Control", "Armament Position Selector - L INBD, ON/OFF")
            AddFunction(Switch.CreateToggleSwitch(this, FIRE_CONTRL_SYS, BUTTON_3,
                "348", "1", "on", "0", "off", "Weapons Control", "L INBD, ON/OFF", " %1d"));

            // defineToggleSwitch("ARMPOS4", 15, 3004, 349, "Weapons Control", "Armament Position Selector - CENTER, ON/OFF")
            AddFunction(Switch.CreateToggleSwitch(this, FIRE_CONTRL_SYS, BUTTON_4,
                "349", "1", "on", "0", "off", "Weapons Control", "CENTER, ON/OFF", " %1d"));

            // defineToggleSwitch("ARMPOS5", 15, 3005, 350, "Weapons Control", "Armament Position Selector - R INBD, ON/OFF")
            AddFunction(Switch.CreateToggleSwitch(this, FIRE_CONTRL_SYS, BUTTON_5,
                "350", "1", "on", "0", "off", "Weapons Control", "R INBD, ON/OFF", " %1d"));

            // defineToggleSwitch("ARMPOS6", 15, 3006, 351, "Weapons Control", "Armament Position Selector - R OUTBD, ON/OFF")
            AddFunction(Switch.CreateToggleSwitch(this, FIRE_CONTRL_SYS, BUTTON_6,
                "351", "1", "on", "0", "off", "Weapons Control", "R OUTBD, ON/OFF", " %1d"));

            // defineToggleSwitch("ARMPOS7", 15, 3007, 352, "Weapons Control", "Armament Position Selector - R WINGTIP, ON/OFF")
            AddFunction(Switch.CreateToggleSwitch(this, FIRE_CONTRL_SYS, BUTTON_7,
                "352", "1", "on", "0", "off", "Weapons Control", "R WINGTIP, ON/OFF", " %1d"));

            // define3PosTumb("INT_SW", 15, 3008, 340, "Weapons Control", "Interval Switch [sec], .06/.10/.14")
            AddFunction(Switch.CreateThreeWaySwitch(this, FIRE_CONTRL_SYS, BUTTON_8, "340",
                "1.0", ".14",
                "0.0", ".10",
                "-1.0", ".06",
                "Weapons Control", "Interval Switch [sec], .06/.10/.14", "%0.1f"));

            // defineTumb("BOMBS_ARM_SW", 15, 3009, 341, 0.2, { 0.2, 0.8}, nil, false,
            //   "Weapons Control" ,"Bombs Arm Switch, SAFE/TAIL/NOSE & TAIL/NOSE")
            AddFunction(new Axis(this, FIRE_CONTRL_SYS, BUTTON_9,
                 "341", 0.1d, 0.0d, 1.0d, "Weapons Control", "Arm Switch, SAFE/TAIL/NOSE & TAIL/NOSE"));

            // defineToggleSwitch("MASTER_ARM_GUARD", 15, 3010, 342, "Weapons Control", "Master Arm Safe Guard")
            AddFunction(new Switch(this, FIRE_CONTRL_SYS, "342",
                new SwitchPosition[] {
                    new SwitchPosition("1", "open", BUTTON_10),
                    new SwitchPosition("0", "close", BUTTON_10)
                }, "Weapons Control", "Master Arm Safe Guard ", "%1d", true, true));

            // define3PosTumb("MASTER_ARM", 15, 3011, 343, "Weapons Control", "Master Arm Guns, GUNS MSL & CAMR/OFF/CAMR ONLY")
            AddFunction(Switch.CreateThreeWaySwitch(this, FIRE_CONTRL_SYS, BUTTON_11, "343",
                "1.0", "CAMR ONLY",
                "0.0", "OFF",
                "-1.0", "GUNS MSL & CAMR",
                "Weapons Control", "Master Arm Switch", "%0.1f"));

            // defineTumb("EXT_STORE", 15, 3012, 344, 0.1, { 0.0, 0.3}, nil, false,
            //  "Weapons Control", "External Stores Selector RIPL/BOMB/SAFE/RKT DISP")
            AddFunction(new Axis(this, FIRE_CONTRL_SYS, BUTTON_12,
             "344", 0.1d, 0.0d, 1.0d, "Weapons Control", "External Stores Selector RIPL/BOMB/SAFE/RKT DISP"));

            // definePushButton("MISSILE_UNCAGE", 15, 3014, 136, "Weapons Control",
            //  "Missile Uncage Switch - Press and hold to uncage missile seeker head")
            AddFunction(new PushButton(this, FIRE_CONTRL_SYS, BUTTON_14,
                "136", "Weapons Control", "Missile Uncage Switch"));

            // definePotentiometer("MISSILE_VOL", 15, 3015, 345, { 0, 1}, "Weapons Control", "Missile Volume Knob")
            AddFunction(new Axis(this, FIRE_CONTRL_SYS, BUTTON_15,
                "345", 0.1d, 0.0d, 1.0d, "Weapons Control", "Missile Volume Knob"));

            // definePushButton("WEAPON_RELEASE", 15, 3018, 128, "Weapons Control", "Weapon Release Button")
            AddFunction(new PushButton(this, FIRE_CONTRL_SYS, BUTTON_16,
                "128", "Weapons Control", "Weapon Release Button"));
            AddFunction(new WeaponGuardLock(this));
            AddFunction(new WeaponPanelSelectors(this));
            #endregion

            #region AN/APQ-159
            // AN/APQ-159 Radar Control Panel
            // definePotentiometer("RADAR_ELEVATION", 17, 3001, 321, { -1, 1},
            // "Radar", "AN/APQ-159 Radar Elevation Antenna Tilt Control Knob")
            AddFunction(new Axis(this, RADER_APQ_SYS, BUTTON_1,
                "321", 0.1d, 0.0d, 1.0d, "Radar", "Antenna Tilt Knob"));

            // defineTumb("RADAR_RANGE", 17, 3004, 315, 0.1, { 0.0, 0.3}, nil, false,
            // "Radar", "AN/APQ-159 Radar Range Selector Switch [nm], 5/10/20/40")
            AddFunction(new Axis(this, RADER_APQ_SYS, BUTTON_4,
                "315", 0.1d, 0.0d, 0.4d, "Radar", "Radar Range Selector  5/10/20/40"));

            // defineTumb("RADAR_MODE", 17, 3005, 316, 0.1, { 0.0, 0.3}, nil, false,
            // "Radar", "AN/APQ-159 Radar Mode Selector Switch, OFF/STBY/OPER/TEST")
            AddFunction(new Axis(this, RADER_APQ_SYS, BUTTON_5,
                "316", 0.1d, 0.0d, 0.3d, "Radar", "Radar Mode Selector OFF/STBY/OPER/TEST"));

            // definePushButton("RADAR_ACQUIS", 17, 3006, 317, "Radar", "AN/APQ-159 Radar Acquisition Button")
            AddFunction(new PushButton(this, RADER_APQ_SYS, BUTTON_6,
                "317", "Weapons Control", "Radar Acquisition"));

            // definePotentiometer("RADAR_SCALE", 17, 3007, 65, { 0, 1}, "Radar", "AN/APQ-159 Radar Scale Knob")
            AddFunction(new Axis(this, RADER_APQ_SYS, BUTTON_7, "65", 0.1d, 0.0d, 1.0d, "Radar",
                "AN/APQ-159 Radar Scale Knob"));

            // definePotentiometer("RADAR_BRIGHT", 17, 3008, 70, { 0, 1}, "Radar", "AN/APQ-159 Radar Bright Knob")
            AddFunction(new Axis(this, RADER_APQ_SYS, BUTTON_8, "70", 0.1d, 0.0d, 1.0d, "Radar",
                "AN/APQ-159 Radar Bright Knob"));

            // definePotentiometer("RADAR_PERSIS", 17, 3009, 69, { 0, 1}, "Radar", "AN/APQ-159 Radar Persistence Knob")
            AddFunction(new Axis(this, RADER_APQ_SYS, BUTTON_9, "69", 0.1d, 0.0d, 1.0d, "Radar",
                "AN/APQ-159 Radar Persistence Knob"));

            // definePotentiometer("RADAR_VIDEO", 17, 3010, 68, { 0, 1}, "Radar", "AN/APQ-159 Radar Video Knob")
            AddFunction(new Axis(this, RADER_APQ_SYS, BUTTON_10, "68", 0.1d, 0.0d, 1.0d, "Radar",
                "AN/APQ-159 Radar Video Knob"));

            // definePotentiometer("RADAR_CURSOR", 17, 3011, 67, { 0, 1}, "Radar", "AN/APQ-159 Radar Cursor Knob")
            AddFunction(new Axis(this, RADER_APQ_SYS, BUTTON_11, "67", 0.1d, 0.0d, 1.0d, "Radar",
                "AN/APQ-159 Radar Cursor Knob"));

            // definePotentiometer("RADAR_PITCH", 17, 3012, 66, { -0.75,0.75}, "Radar", "AN/APQ-159 Radar Pitch Knob")
            AddFunction(new Axis(this, RADER_APQ_SYS, BUTTON_12, "66", 0.1d, -1.0d, 1.0d, "Radar",
                "AN/APQ-159 Radar Pitch Knob"));

            // defineIndicatorLight("RADAR_SCALE_5", 155, "Warning, Caution and IndicatorLights", "Radar Range Scale 5")
            AddFunction(new FlagValue(this, "155", "Rader Indicators", "RADAR_SCALE_5",
                "Radar Range Scale 5"));

            // defineIndicatorLight("RADAR_SCALE_10", 156, "Warning, Caution and IndicatorLights", "Radar Range Scale 10")
            AddFunction(new FlagValue(this, "156", "Rader Indicators", "RADAR_SCALE_10",
                "Radar Range Scale 10"));

            // defineIndicatorLight("RADAR_SCALE_20", 157, "Warning, Caution and IndicatorLights", "Radar Range Scale 20")
            AddFunction(new FlagValue(this, "157", "Rader Indicators", "RADAR_SCALE_20",
                "Radar Range Scale 20"));

            // defineIndicatorLight("RADAR_SCALE_40", 158, "Warning, Caution and IndicatorLights",
            //   "Radar Range Scale 40")
            AddFunction(new FlagValue(this, "158", "Rader Indicators", "RADAR_SCALE_40",
                "Radar Range Scale 40"));

            // defineIndicatorLight("RADAR_INRANGE", 159, "Warning, Caution and IndicatorLights",
            //  "Radar InRange Light")
            AddFunction(new FlagValue(this, "159", "Rader Indicators", "Radar InRange Light",
                "Radar InRange Light"));

            // defineIndicatorLight("RADAR_FAIL", 160, "Warning, Caution and IndicatorLights",
            //  "Radar Fail Light")
            AddFunction(new FlagValue(this, "160", "Rader Indicators", "Radar Fail Light",
                "Radar Fail Light"));

            // defineIndicatorLight("RADAR_LOCKON", 161, "Warning, Caution and IndicatorLights",
            //  "Radar LockOn Light")
            AddFunction(new FlagValue(this, "161", "Rader Indicators", "Radar LockOn Light",
                "Radar LockOn Light"));

            // defineIndicatorLight("RADAR_EXCESS", 162, "Warning, Caution and IndicatorLights",
            //   "Radar ExcessG Light")
            AddFunction(new FlagValue(this, "162", "Rader Indicators", "Radar ExcessG Light",
                "Radar ExcessG Light"));

            // defineIndicatorLight("RADAR_SCALE_BRIGHT", 163, "Warning, Caution and IndicatorLights",
            //   "Radar Scale Brightness")
            AddFunction(new FlagValue(this, "163", "Rader Indicators", "Radar Scale Brightness",
                "Radar Scale Brightness"));
             
            // defineFloat("TDC_RANGE", 319, { -1.0, 1.0}, "Gauges", "TdcControlRange")
            AddFunction(new ScaledNetworkValue(this, "319", 1d,
                 "Rader Indicators", "TdcControlRange", "TdcControlRange", "", BindingValueUnits.Numeric));

            // defineFloat("TDC_AZIMUTH", 318, { -1.0, 1.0}, "Gauges", "TdcControlAzimuth")
            AddFunction(new ScaledNetworkValue(this, "318", 1d,
                 "Rader Indicators", "TdcControlAzimuth", "TdcControlAzimuth", "", BindingValueUnits.Numeric));

            AddFunction(new RaderPanelKnobs(this));
            AddFunction(new RaderOperateKnobs(this));

            #endregion

            #region ASG-31 Sight

            AddFunction(new Switch(this, SIGHT_SYS, "40",
                new SwitchPosition[] {
                    new SwitchPosition("0.0", "OFF", BUTTON_1),
                    new SwitchPosition("0.1", "MSL", BUTTON_1),
                    new SwitchPosition("0.2", "A/A1", BUTTON_1),
                    new SwitchPosition("0.3", "A/A2", BUTTON_1),
                    new SwitchPosition("0.4", "MAN", BUTTON_1)},
                "Sight", "Sight Mode Selector OFF/MSL/A/A1 GUNS/A/A2 GUNS/MAN", "%0.1f"));

            AddFunction(new Axis(this, SIGHT_SYS, BUTTON_2, "42", 0.04d, 0.0d, 1.0d,
                "Sight", "AN/ASG-31 Sight Reticle Depression Knob"));

            AddFunction(new Axis(this, SIGHT_SYS, BUTTON_3, "41", 0.005d, 0.0d, 1.0d,
                "Sight", "AN/ASG-31 Sight Reticle Intensity Knob"));

            AddFunction(new Axis(this, SIGHT_SYS, BUTTON_4,
               "47", 0.1d, -1.0d, 1.0d, "Sight", "AN/ASG-31 Sight BIT Switch, BIT 1/OFF/BIT 2"));


            AddFunction(new PushButton(this, SIGHT_SYS, BUTTON_7, "137", "Sight", "AN/ASG-31 Sight Cage Switch"));

            //AddFunction(new ScaledNetworkValue(this, "43", 1d,
            //     "Sight", "Depression Drum 100", "Depression Drum 100", "", BindingValueUnits.Numeric));
            //AddFunction(new ScaledNetworkValue(this, "44", 1d,
            //     "Sight", "Depression Drum 10", "Depression Drum 10", "", BindingValueUnits.Numeric));
            //AddFunction(new ScaledNetworkValue(this, "45", 1d,
            //     "Sight", "Depression Drum 1", "Depression Drum 1", "", BindingValueUnits.Numeric));

            AddFunction(new SightDepressionDrums(this));
            AddFunction(new SightSelector(this));

            #endregion

            #region RWR - IC
            // definePushButton("RWR_MODE", 19, 3001, 551, "RWR IC", "RWR MODE Button")
            AddFunction(new PushButton(this, RWR_SYS, BUTTON_1, "551",
                "RWR IC", "RWR MODE Button"));

            // definePushButton("RWR_SEARCH", 19, 3002, 554, "RWR IC", "RWR SEARCH Button")
            AddFunction(new PushButton(this, RWR_SYS, BUTTON_2, "554",
                "RWR IC", "RWR SEARCH Button"));

            // definePushButton("RWR_HAND", 19, 3003, 556, "RWR IC", "RWR HANDOFF Button")
            AddFunction(new PushButton(this, RWR_SYS, BUTTON_3, "556",
                "RWR IC", "RWR HANDOFF Button"));

            // definePushButton("RWR_ALT", 19, 3004, 561, "RWR IC", "RWR ALTITUDE Button")
            AddFunction(new PushButton(this, RWR_SYS, BUTTON_4, "561",
                "RWR IC", "RWR ALTITUDE Button"));

            // definePushButton("RWR_T", 19, 3005, 564, "RWR IC", "RWR T Button")
            AddFunction(new PushButton(this, RWR_SYS, BUTTON_5, "564",
                "RWR IC", "RWR T Button"));

            // definePushButton("RWR_TEST", 19, 3006, 567, "RWR IC", "RWR SYS TEST Button")
            AddFunction(new PushButton(this, RWR_SYS, BUTTON_6, "567",
                "RWR IC", "RWR SYS TEST Button"));

            // definePushButton("RWR_SHIP", 19, 3007, 570, "RWR IC", "RWR UNKNOWN SHIP Button")
            AddFunction(new PushButton(this, RWR_SYS, BUTTON_7, "570",
                "RWR IC", "RWR UNKNOWN SHIP Button"));

            // definePushButton("RWR_PWR", 19, 3008, 575, "RWR IC", "RWR POWER Button")
            AddFunction(new PushButton(this, RWR_SYS, BUTTON_8, "575",
                "RWR IC", "RWR POWER Button"));

            // definePushButton("RWR_LAUNCH", 19, 3009, 559, "RWR IC", "RWR LAUNCH Button")
            AddFunction(new PushButton(this, RWR_SYS, BUTTON_9, "559",
                "RWR IC", "RWR LAUNCH Button"));

            // definePushButton("RWR_ACT", 19, 3010, 573, "RWR IC", "RWR ACT/PWR Button")
            AddFunction(new PushButton(this, RWR_SYS, BUTTON_10, "573",
                "RWR IC", "RWR ACT/PWR Button"));

            // definePotentiometer("RWR_DIM", 19, 3011, 578, { 0, 1}, "RWR IC", "RWR DIM Knob")
            AddFunction(new Axis(this, RWR_SYS, BUTTON_11, "578", 0.1d, 0.0d, 1.0d,
                "RWR IC", "RWR DIM Knob"));

            // definePotentiometer("RWR_VOL", 19, 3012, 577, { 0, 1}, "RWR IC", "RWR AUDIO Knob")
            AddFunction(new Axis(this, RWR_SYS, BUTTON_12, "577", 0.1d, 0.0d, 1.0d,
                "RWR IC", "RWR AUDIO Knob"));

            AddFunction(new RWRICPanel(this));
            #endregion

            #region IFF
            // i checked send/receive value with dcs-bios.I found some fields have inconsistent input/output mapping.

            // defineIndicatorLight("IFF_REPLY_L", 216, "IFF", "IFF Reply Lamp (green)")
            // -> consitent
            AddFunction(new FlagValue(this, "216", "IFF", "IFF Reply Lamp (green)", "IFF Reply Lamp (green)"));

            //defineIndicatorLight("IFF_TEST_L", 218, "IFF", "IFF Test Lamp (green)")
            // -> consitent 
            AddFunction(new FlagValue(this, "218", "IFF", "IFF Test Lamp (green)", "IFF Test Lamp (green)"));

            //defineTumb("IFF4_CODE_SEL", 22, 3001, 199, 0.1, { 0.0, 0.3}, nil, false,"IFF" , "IFF MODE 4 CODE Selector, ZERO/B/A/HOLD")
            // inner -> outer (helios input)  : (IFF4_CODE_SEL)
            // outer -> inner (helios output) : CODE WHEEL 1

            AddFunction(new Axis(this, IFF, BUTTON_1, "199", 0.1d, -1.0d, 3.0d,
                "IFF", "IFF CODE WHEEL 1"));

            //defineTumb("IFF4_MASTER", 22, 3002, 200, 0.1, { 0.0, 0.4}, nil, false,"IFF" , "IFF MASTER Control Selector, EMER/NORM/LOW/STBY/OFF")
            // inner -> outer (helios input)  : IFF4 MASTER Control
            // outer -> inner (helios output) : CODE WHEEL 2
            AddFunction(new Axis(this, IFF, BUTTON_2, "200", 0.1d, -1.0d, 3.0d,
                "IFF", "IFF CODE WHEEL 2"));

            //define3PosTumb("IFF4_MON_CONTR", 22, 3003, 201, "IFF", "IFF MODE 4 Monitor Control Switch, AUDIO/OUT/LIGHT")
            // inner -> outer (helios input)  : IFF MODE 4 Monitor Control
            // outer -> inner (helios output) : CODE WHEEL 3
            AddFunction(new Axis(this, IFF, BUTTON_3, "201", 0.1d, -1.0d, 2.0d,
                "IFF", "IFF CODE WHEEL 3"));
            // define3PosTumb("IFF4_M1", 22, 3004, 202, "IFF", "IFF Mode Select/TEST Switch, M-1 /ON/OUT")
            // inner -> outer (helios input)  : IFF Mode(M-1)
            // outer -> inner (helios output) : CODE WHEEL 4
            AddFunction(new Axis(this, IFF, BUTTON_4, "202", 0.1d, -1.0d, 2.0d,
                "IFF", "IFF CODE WHEEL 4"));
            //define3PosTumb("IFF4_M2", 22, 3005, 203, "IFF", "IFF Mode Select/TEST Switch, M-2 /ON/OUT")
            // inner -> outer (helios input)  : IFF Mode(M-2)
            // outer -> inner (helios output) : CODE WHEEL 5
            AddFunction(new Axis(this, IFF, BUTTON_5, "203", 0.1d, -1.0d, 2.0d,
                "IFF", "IFF CODE WHEEL 5"));

            //define3PosTumb("IFF4_M3", 22, 3006, 204, "IFF", "IFF Mode Select/TEST Switch, M-3/A /ON/OUT")
            // inner -> outer (helios input)  : IFF Mode(M-3)
            // outer -> inner (helios output) : CODE WHEEL 6
            AddFunction(new Axis(this, IFF, BUTTON_6, "204", 0.1d, -1.0d, 2.0d,
                "IFF", "IFF CODE WHEEL 6"));

            //define3PosTumb("IFF4_MC", 22, 3007, 205, "IFF", "IFF Mode Select/TEST Switch, M-C /ON/OUT")
            // inner -> outer (helios input)  : IFF Mode (M-C)
            // outer -> inner (helios output) : IFF MODE 4 CODE Selector, ZERO/B/A/HOLD
            // outer -> inner values:( (<-1 2:A 3->) )
            AddFunction(new Axis(this, IFF, BUTTON_7, "205", 0.1d, -1.0d, 2.0d,
                "IFF", "IFF CODE Selector"));

            //define3PosTumb("IFF4_RAD", 22, 3008, 206, "IFF", "IFF RAD TEST/MON Switch, RAD TEST/OUT/MON")
            // inner -> outer (helios input)  : IFF Mode (RAD)
            // outer -> inner (helios output) : IFF MASTER Control Selector, EMER/NORM/LOW/STBY/OFF") 
            // outer -> inner values:((<-? ?:? 3->))
            AddFunction(new Axis(this, IFF, BUTTON_8, "206", 0.1d, -1.0d, 2.0d,
                "IFF", "IFF MASTER Selector"));

            //define3PosTumb("IFF4_IDENT", 22, 3009, 207, "IFF", "IFF Identification of Position (IP) Switch, IDENT/OUT/MIC")
            // inner -> outer (helios input)  : IFF Mode (IDENT)
            // outer -> inner (helios output) : IFF MODE 4 Monitor Control Switch, AUDIO/OUT/LIGHT
            // outer -> inner (values): 0:LIGHT/1:OUT/2:AUDIO
            AddFunction(new Axis(this, IFF, BUTTON_9, "207", 0.1d, -1.0d, 2.0d,
                "IFF", "IFF MODE 4 Monitor Cntrl"));

            //defineToggleSwitch("IFF4_CONTROL", 22, 3010, 208, "IFF", "IFF MODE 4 Control Switch, ON/OUT")
            // inner -> outer (helios input)  : IFF MODE 4 Control Switch, ON/OUT
            // outer -> inner (helios output) : M-1 Toggle 
            AddFunction(new Axis(this, IFF, BUTTON_10, "208", 0.1d, -1.0d, 2.0d,
                "IFF", "IFF M-1 Toggle"));

            //defineTumb("IFF4_MASTER", 22, 3011, 209, 0.1, { 0.0, 1.0}, nil, false,"IFF" , "IFF MODE 1 Code Selector Wheel 1")
            // inner -> outer (helios input)  : IFF MODE 1 Code Wheel
            // outer -> inner (helios output) : M-2 Toggle
            AddFunction(new Axis(this, IFF, BUTTON_11, "209", 0.1d, -1.0d, 2.0d,
                "IFF", "IFF M-2 Toggle"));

            //defineTumb("IFF4_MASTER", 22, 3012, 210, 0.1, { 0.0, 1.0}, nil, false,"IFF" , "IFF MODE 1 Code Selector Wheel 2")
            // inner -> outer (helios input)  : IFF MODE 2 Code Wheel
            // outer -> inner (helios output) : M-3/A Toggle
            AddFunction(new Axis(this, IFF, BUTTON_12, "210", 0.1d, -1.0d, 2.0d,
                "IFF", "IFF M-3/A Toggle"));

            //defineTumb("IFF4_MASTER", 22, 3013, 211, 0.1, { 0.0, 1.0}, nil, false,"IFF" , "IFF MODE 3/A Code Selector Wheel 1")
            // inner -> outer (helios input)  : IFF MODE 3A1 Code Wheel
            // outer -> inner (helios output) :  M-C Toggle
            AddFunction(new Axis(this, IFF, BUTTON_13, "211", 0.1d, -1.0d, 2.0d,
                "IFF", "IFF M-C Toggle"));

            // defineTumb("IFF4_MASTER", 22, 3014, 212, 0.1, { 0.0, 1.0}, nil, false,"IFF" , "IFF MODE 3/A Code Selector Wheel 2")
            // inner -> outer (helios input)  : IFF MODE 3A2 Code Wheel
            // outer -> inner (helios output) : IFF Mode (RAD)
            AddFunction(new Axis(this, IFF, BUTTON_14, "212", 0.1d, -1.0d, 2.0d,
                "IFF", "IFF RAD Toggle"));
            // defineTumb("IFF4_MASTER", 22, 3015, 213, 0.1, { 0.0, 1.0}, nil, false,"IFF" , "IFF MODE 3/A Code Selector Wheel 3")
            // inner -> outer (helios input)  : IFF MODE 3A3 Code Wheel
            // outer -> inner (helios output) : FF Identification of Position (IP) Switch, IDENT/OUT/MIC
            AddFunction(new Axis(this, IFF, BUTTON_15, "213", 0.1d, -1.0d, 2.0d,
                "IFF", "IFF IDENT Toggle"));
            // defineTumb("IFF4_MASTER", 22, 3016, 214, 0.1, { 0.0, 1.0}, nil, false,"IFF" , "IFF MODE 3/A Code Selector Wheel 4")
            // inner -> outer (helios input)  : IFF MODE 3/A Code Selector Wheel 4
            // outer -> inner (helios output) : "IFF MODE 4 Control Switch, ON/OUT
            // outer -> inner (helios output) : 0 OUT/ 2/ON
            AddFunction(new Axis(this, IFF, BUTTON_16, "214", 0.1d, -1.0d, 2.0d,
                "IFF", "IFF ON/OUT Toggle"));

            //definePushButton("IFF4_REPLY_L", 22, 3017, 215, "IFF", "MODE 4 REPLY Light - Press to test(LMB)")
            // consistent ?
            AddFunction(new PushButton(this, IFF, BUTTON_17, "215",
                "IFF", "MODE 4 REPLY Light(PushButton)"));

            // definePushButton("IFF4_RAD_TEST_L", 22, 3018, 217, "IFF", "Radiation TEST and Monitor Light - Press to test(LMB)")
            // inner -> outer (helios input)  : IFF4_RAD_TEST_L
            // outer -> inner (helios output) : ???

            AddFunction(new PushButton(this, IFF, BUTTON_18, "217",
                "IFF", "Radiation TEST and Monitor Light(PushButton)"));

            // defineToggleSwitch("IFF4_CODE_SEL_PULL", 22, 3019, 197, "IFF", "IFF MODE 4 CODE Selector Pull")
            // inner -> outer (helios input)  : IFF MODE 4(Pull)
            // outer -> inner (helios output) : ???

            AddFunction(new PushButton(this, IFF, BUTTON_22, "197",
                "IFF", "IFF MODE Selector(Pull)"));
            // defineToggleSwitch("IFF4_MASTER_PULL", 22, 3020, 198, "IFF", "IFF MASTER Control Selector Pull")
            // inner -> outer (helios input)  : IFF MASTER Control(Pull)
            // outer -> inner (helios output) : ???
            AddFunction(new PushButton(this, IFF, BUTTON_23, "198",
                "IFF", "IFF MASTER Selector(Pull)"));
            AddFunction(new IFF4CodeSelector(this));
            AddFunction(new IFF4Wheels(this));
            AddFunction(new IFF4Toggle(this));
            AddFunction(new IFF4ToggleOther(this));

            #endregion

            #region AN/ALR-87 RWR
            // definePotentiometer("RWR_INT", 20, 3001, 140, { 0, 1}, "AN ALR87", "RWR INT Knob")
            //AddFunction(new Axis(this, RADER_ALR_SYS, BUTTON_1, "140", 0.1d, 0.0d, 1.0d,
            //    "RWR_ALR", "RWR INT Knob"));

            #endregion

            #region Accelerometer
            // definePushButton("ACC", 27, 3001, 904, "Accelerometer", "Accelerometer - Push to set")
            AddFunction(new PushButton(this, ACC_METER, BUTTON_1, "904",
                "Accelerometer", "Accelerometer - Push to set"));
            // defineFloat("ACCELEROMETER", 6, { 0.0, 0.323, 0.653, 1.0}, "Gauges", "Accelerometer")
            CalibrationPointCollectionDouble accelerometerScale =
                new CalibrationPointCollectionDouble(0.0d, -5.0d, 1.0d, 10d);

            AddFunction(new DualNetworkValue(this, "6", accelerometerScale,
               "Accelerometer", "Current gs", "Current gs", "", BindingValueUnits.Numeric));

            // defineFloat("ACCELEROMETER_MIN", 902, { 0.0, 0.323, 0.653, 1.0}, "Gauges", "Accelerometer Min")
            AddFunction(new DualNetworkValue(this, "902", accelerometerScale,
                "Accelerometer", "Accelerometer Min", "Min Gs attained.", "", BindingValueUnits.Numeric));

            // defineFloat("ACCELEROMETER_MAX", 903, { 0.0, 0.323, 0.653, 1.0}, "Gauges", "Accelerometer Max")
            AddFunction(new DualNetworkValue(this, "903", accelerometerScale,
                "Accelerometer", "Accelerometer Max", "Max Gs attained.", "", BindingValueUnits.Numeric));

            #endregion

            #region AirSpeed / Mach Indicator
            // defineRotary("IAS_SET", 28, 3001, 180, "AirSpeed Indicator", "Index Setting Pointer Knob")
            AddFunction(new RotaryEncoder(this, IAS_METER, BUTTON_1, "180", 0.1,
                "AirSpeed Indicator", "Index Setting Pointer Knob"));

            // defineFloat("AIRSPEED", 8, { 0.0, 0.0435, 0.1, 0.318, 0.3745,
            //   0.397, 0.4495, 0.482,  0.54, 0.553, 0.6145, 0.658, 0.668,
            //   0.761, 0.801, 0.877, 0.909, 0.942, 0.972,   1.0}, "Gauges", "Airspeed")
            CalibrationPointCollectionDouble airspeedScale =
                new CalibrationPointCollectionDouble(0.0d, 0.0d, 1.0d, 550d)
                {
                    // non linear section at start
                    new CalibrationPointDouble(0.0488d, 50d),
                    new CalibrationPointDouble(0.14d, 100d)
                };
            AddFunction(new DualNetworkValue(this, "8", airspeedScale, "IAS",
              "Airspeed", "Airspeed", "", BindingValueUnits.Numeric));

            AddFunction(new NetworkValue(this, "178", "IAS",
               "Max Airspeed", "Max Airspeed", "", BindingValueUnits.Numeric));

            // defineFloat("AIRSPEED_SET", 177, { 0.0, 1.0}, "Gauges", "Set Airspeed")
            AddFunction(new NetworkValue(this, "177", "IAS",
               "Limit Airspeed", "Limit Airspeed", "", BindingValueUnits.Numeric));

            // defineFloat("MACH", 179, { 1.0, 0.929, 0.871, 0.816, 0.765, 0.727,
            //    0.683, 0.643, 0.611, 0.582, 0.551, 0.525, 0.5, 0.4}, "Gauges", "MachIndicator")
            AddFunction(new NetworkValue(this, "179", "IAS",
               "MachIndicator", "MachIndicator", "", BindingValueUnits.Numeric));
            #endregion

            #region Altimeter AAU - 34 / A
            // defineToggleSwitch("ALT_ELECT", 31, 3002, 60, "Altimeter",
            //   "Altimeter Mode Control Lever, ELECT(rical)/PNEU(matic)")
            AddFunction(Switch.CreateToggleSwitch(this, ALTI_METER, BUTTON_2, "60",
                "1", "electric", "0", "pneumatic", "Altimeter",
                "Altimeter Mode Control Lever, ELECT(rical)/PNEU(matic)", "%1d"));

            // defineRotary("ALT_ZERO", 31, 3003, 62, "Altimeter", "Altimeter Zero Setting Knob")
            AddFunction(new RotaryEncoder(this, ALTI_METER, BUTTON_3, "62", 0.1,
                "Altimeter", "Altimeter Zero Setting Knob"));

            // defineFloat("ALT_100_P", 10, { 0.0, 1.0}, "Gauges", "Altimeter 100ft Pointer")
            AddFunction(new ScaledNetworkValue(this, "10", 1d,
                 "Altimeter", "Altimeter 100ft Pointer",
                 "Altimeter 100ft Pointer", "", BindingValueUnits.Numeric));

            // defineFloat("ALT_10000_C", 11, { 0.0, 1.0}, "Gauges", "Altimeter 10000ft Count")
            AddFunction(new ScaledNetworkValue(this, "11", 1d,
                 "Altimeter", "Altimeter 10000ft Count",
                 "Altimeter 10000ft Count", "", BindingValueUnits.Numeric));

            // defineFloat("ALT_1000_C", 520, { 0.0, 1.0}, "Gauges", "Altimeter 1000ft Count")
            AddFunction(new ScaledNetworkValue(this, "520", 1d,
                 "Altimeter", "Altimeter 1000ft Count",
                 "Altimeter 1000ft Count", "", BindingValueUnits.Numeric));

            // defineFloat("ALT_100_C", 521, { 0.0, 1.0}, "Gauges", "Altimeter 100ft Count")
            AddFunction(new ScaledNetworkValue(this, "521", 1d,
                 "Altimeter", "Altimeter 100ft Count",
                 "Altimeter 100ft Count", "", BindingValueUnits.Numeric));

            // defineFloat("PRESS_0", 59, { 0.0, 1.0}, "Gauges", "Pressure Setting 0")
            AddFunction(new ScaledNetworkValue(this, "59", 1d,
                 "Altimeter", "Pressure Setting 0",
                 "Pressure Setting 0", "", BindingValueUnits.Numeric));
            // defineFloat("PRESS_1", 58, { 0.0, 1.0}, "Gauges", "Pressure Setting 1")
            AddFunction(new ScaledNetworkValue(this, "58", 1d,
                 "Altimeter", "Pressure Setting 1",
                 "Pressure Setting 1", "", BindingValueUnits.Numeric));
            // defineFloat("PRESS_2", 57, { 0.0, 1.0}, "Gauges", "Pressure Setting 2")
            AddFunction(new ScaledNetworkValue(this, "57", 1d,
                 "Altimeter", "Pressure Setting 2",
                 "Pressure Setting 2", "", BindingValueUnits.Numeric));
            // defineFloat("PRESS_3", 56, { 0.0, 1.0}, "Gauges", "Pressure Setting 3")
            AddFunction(new ScaledNetworkValue(this, "56", 1d,
                 "Altimeter", "Pressure Setting 3",
                 "Pressure Setting 3", "", BindingValueUnits.Numeric));
            // defineFloat("ALT_PNEU_FLAG", 9, { 0.0, 1.0}, "Gauges", "Altimeter PNEU Flag")
            AddFunction(new ScaledNetworkValue(this, "9", 1d,
                 "Altimeter", "Altimeter PNEU Flag",
                 "Altimeter PNEU Flag", "", BindingValueUnits.Numeric));
            AddFunction(new Altimeter(this));
            #endregion

            #region Instruments
            // Instruments--------------------------

            // defineFloat("MOTOR_RUN", 85, { 1.0, 0.0},
            // "Gauges", "Motor Run Knob")
            AddFunction(new ScaledNetworkValue(this, "85", 1d, "Gauges",
                "Motor Run Knob", "Motor Run Knob", "", BindingValueUnits.Numeric));

            // defineFloat("CANOPY_POS", 181, { 0, 1}, "Gauges", "Canopy Position")
            #endregion

            #region ARU-20 / A
            // Attitude Indicator ARU-20 / A
            // definePotentiometer("AI_PITCH_TRIM", 32, 3001, 150,
            //  { 0, 1}, "Attitude Indicator", "Attitude Indicator Pitch Trim Knob")
            AddFunction(new RotaryEncoder(this, ATTI_INDIX, BUTTON_1, "150", 0.04d,
                "Attitude Indicator", "Attitude Indicator Pitch Trim Knob"));

            // defineFloat("AI_PITCH", 81, { -0.507, 0.0, 0.507}, "Gauges", "Attitude Indicator Pitch")
            AddFunction(new ScaledNetworkValue(this, "81", 360d, "Attitude Indicator",
                "Attitude Indicator Pitch", "Attitude Indicator Pitch", "", BindingValueUnits.Degrees));

            // defineFloat("AI_BANK", 30, { -1.0, 1.0}, "Gauges", "Attitude Indicator Bank")
            AddFunction(new ScaledNetworkValue(this, "30", 360d, "Attitude Indicator",
                "Attitude Indicator Bank", "Attitude Indicator Bank", "", BindingValueUnits.Degrees));

            // defineFloat("AI_OFF_FLAG", 149, { 0.0, 1.0}, "Gauges", "Attitude Indicator OFF Flag")
            AddFunction(new ScaledNetworkValue(this, "149", 1d, "Attitude Indicator",
                "Attitude Indicator OFF Flag", "Attitude Indicator OFF Flag", "", BindingValueUnits.Numeric));
            #endregion

            #region HSI
            // Headding -> apply Teremetory Yaw Value to HSI
            // Horizontal Situation Indicator
            // defineVariableStepTumb("HSI_HDG_KNOB", 33, 3001, 271, 1.0, "HSI", "HSI Heading Set Knob")
            //AddFunction(new RotaryEncoder(this, HSI_SYS, BUTTON_1, "271", 0.005, "HSI", "HSI Heading Set Knob"));
            
            AddFunction(new Axis(this, HSI_SYS, BUTTON_1, "271", 0.005d, 0.0d, 60d,
                "HSI", "HSI Heading Set Knob"));
            // defineVariableStepTumb("HSI_CRS_KNOB", 33, 3002, 272, 1.0, "HSI", "HSI Course Set Knob")
            // AddFunction(new RotaryEncoder(this, HSI_SYS, BUTTON_2, "272", 0.005, "HSI", "HSI Course Set Knob"));
            AddFunction(new Axis(this, HSI_SYS, BUTTON_2, "272", 0.005d, 0.0d, 60d,
                "HSI", "HSI Course Set Knob"));
            // defineFloat("HSI_CRS", 35, { 0.0, 1.0}, "Gauges", "HSI Course Arrow")
            AddFunction(new ScaledNetworkValue(this, "35", 360d, "HSI",
                "HSI Course Arrow", "HSI Course Arrow", "", BindingValueUnits.Degrees));

            // defineFloat("HSI_HDG", 144, { 0.0, 1.0}, "Gauges", "HSI Heading Mark")
            AddFunction(new ScaledNetworkValue(this, "144", 360d, "HSI",
                "HSI Heading Mark", "HSI Heading Mark", "", BindingValueUnits.Degrees));
            // defineFloat("HSI_COMP", 32, { 0.0, 1.0}, "Gauges", "HSI Compass Card")

            // defineFloat("HSI_BER_POINT", 139, { 0.0, 1.0}, "Gauges", "HSI Bearing Pointer")
            AddFunction(new ScaledNetworkValue(this, "139", 360d, "HSI",
                "HSI Bearing Pointer", "HSI Bearing Pointer", "", BindingValueUnits.Degrees));

            // defineFloat("HSI_CRS_DEV", 36, { 0.0, 1.0}, "Gauges", "HSI Course Dev Indicator")
            AddFunction(new ScaledNetworkValue(this, "36", 1d, "HSI",
                "HSI Course Dev Indicator", "HSI Course Dev Indicator", "", BindingValueUnits.Numeric));

            // defineFloat("HSI_Range_100", 268, { 0.0, 1.0}, "Gauges", "HSI Range 100")
            //AddFunction(new ScaledNetworkValue(this, "268", 1d, "Gauges",
            //    "HSI Range 100", "HSI Range 100", "", BindingValueUnits.Numeric));
            // defineFloat("HSI_Range_10", 269, { 0.0, 1.0}, "Gauges", "HSI Range 10")
            //AddFunction(new ScaledNetworkValue(this, "269", 1d, "Gauges",
            //    "HSI Range 10", "HSI Range 10", "", BindingValueUnits.Numeric));
            // defineFloat("HSI_Range_1", 270, { 0.0, 1.0}, "Gauges", "HSI Range 1")
            //AddFunction(new ScaledNetworkValue(this, "270", 1d, "Gauges",
            //    "HSI Range 1", "HSI Range 1", "", BindingValueUnits.Numeric));
            // defineIndicatorLight("HSI_Range_FLAG", 142, "Warning, Caution and IndicatorLights", "HSI Range Flag")
            AddFunction(new FlagValue(this, "142", "HSI", "HSI Range Flag", "HSI Range Flag"));

            // defineFloat("HSI_CRS_10010", 276, { 0.0, 1.0}, "Gauges", "HSI CourseSel 100 10")
            AddFunction(new ScaledNetworkValue(this, "276", 1d, "HSI",
                "HSI_CRS_10010", "HSI_CRS_10010", "", BindingValueUnits.Numeric));

            // defineFloat("HSI_CRS_1", 277, { 0.0, 1.0}, "Gauges", "HSI CourseSel 1")
            AddFunction(new ScaledNetworkValue(this, "277", 1d, "HSI",
                "HSI_CRS_1", "HSI_CRS_1", "", BindingValueUnits.Numeric));

            // defineFloat("HSI_TO_FROM", 146, { 0.0, 1.0}, "Gauges", "HSI To From")
            AddFunction(new ScaledNetworkValue(this, "146", 1d, "HSI",
                "HSI To From", "HSI To From", "", BindingValueUnits.Numeric));

            // defineFloat("HSI_OFF", 143, { 0.0, 1.0}, "Gauges", "HSI OFF Flag")
            AddFunction(new ScaledNetworkValue(this, "143", 1d, "HSI",
                "HSI OFF Flag", "HSI OFF Flag", "", BindingValueUnits.Numeric));
            // defineFloat("HSI_DEV", 141, { 0.0, 1.0}, "Gauges", "HSI DevDF Window")
            AddFunction(new ScaledNetworkValue(this, "141", 1d, "HSI",
                "HSI_DEV", "HSI DevDF Window", "", BindingValueUnits.Numeric));

            // HSI MILE VALUE Func
            AddFunction(new HSICourseMile(this));
            // HSI FROMTO VALUE Func
            AddFunction(new HSIFromToFlag(this));
            #endregion

            #region Clock

            // definePushButton("CLOCK_WIND", 35, 3001, 510, "Clock", "ABU-11 CLOCK Winding Knob")
            AddFunction(new PushButton(this, CLOCK_SYS, BUTTON_1, "510",
                "Clock System", "CLOCK Winding Button"));

            // defineRotary("CLOCK_WIND_ROTATRY", 35, 3002, 510, "Clock", "ABU-11 CLOCK Winding Rotary")
            //AddFunction(new RotaryEncoder(this, CLOCK_SYS, BUTTON_2, "510", 1d,
            //    "Clock System", "CLOCK Winding Rotary"));

            AddFunction(new Axis(this, CLOCK_SYS, BUTTON_2, "510", 1d, 0.0d, 60d,
                "Clock System", "CLOCK Winding Rotary"));

            // definePushButton("CLOCK_SET", 35, 3001, 511, "Clock", "ABU-11 CLOCK Setting Knob")
            AddFunction(new PushButton(this, CLOCK_SYS, BUTTON_1, "511",
                "Clock System", "CLOCK Setting Button"));

            // definePushButton("ELAP_TIME", 35, 3003, 512, "Clock", "ABU-11 CLOCK Elapsed Time Knob")
            AddFunction(new PushButton(this, CLOCK_SYS, BUTTON_3, "512",
                "Clock System", " CLOCK Elapsed Time Button"));

            // defineFloat("CLOCK_CURR_H", 19, { 0.0, 1.0}, "Gauges", "CLOCK Currtime Hours")
            AddFunction(new ScaledNetworkValue(this, "19", 24d, "Clock System",
                "CLOCK Currtime Hours", "CLOCK Currtime Hours", "", BindingValueUnits.Numeric));

            // defineFloat("CLOCK_CURR_M", 18, { 0.0, 1.0}, "Gauges", "CLOCK Currtime Minutes")
            AddFunction(new ScaledNetworkValue(this, "18", 60d, "Clock System",
                "CLOCK Currtime Minutes", "CLOCK Currtime Minutes", "", BindingValueUnits.Numeric));

            // defineFloat("CLOCK_ELAP_M", 509, { 0.0, 1.0}, "Gauges", "CLOCK Elapsed Time Minutes")
            AddFunction(new ScaledNetworkValue(this, "509", 60d, "Clock System",
                "CLOCK Elapsed Time Minutes", "CLOCK Elapsed Time Minutes", "", BindingValueUnits.Numeric));

            // defineFloat("CLOCK_ELAP_S", 37, { 0.0, 1.0}, "Gauges", "CLOCK Elapsed Time Seconds")
            AddFunction(new ScaledNetworkValue(this, "37", 60d, "Clock System",
                "CLOCK Elapsed Time Seconds", "CLOCK Elapsed Time Seconds", "", BindingValueUnits.Numeric));

            #endregion


            #region Standby Attitude Indicator
            // Standby Attitude Indicator
            AddFunction(new PushButton(this, STBY_ATTI_INDX, BUTTON_1, "441", "Standby Attitude Indicator", "Cage SAI"));

            AddFunction(new Axis(this, STBY_ATTI_INDX, BUTTON_2, "442", 0.1d, -1.0d, 1.0d,
                "Standby Attitude Indicator", "SAI Pitch Trim"));
            // defineFloat("SAI_PITCH", 438, { 0.0, 1.0}, "Gauges", "SAI Pitch")
            AddFunction(new ScaledNetworkValue(this, "438", 360d, "Standby Attitude Indicator",
                "SAI Pitch", "SAI Pitch(for gauge)", "", BindingValueUnits.Degrees));

            // defineFloat("SAI_BANK", 439, { 0.0, 1.0}, "Gauges", "SAI Bank")
            AddFunction(new ScaledNetworkValue(this, "439", 360d, "Standby Attitude Indicator",
                "SAI Bank", "SAI Bank(for gauge)", "", BindingValueUnits.Degrees));

            // defineFloat("SAI_OFF", 440, { 0.0, 1.0}, "Standby Attitude Indicator", "SAI OFF Flag")
            AddFunction(new FlagValue(this, "440", "Standby Attitude Indicator", "SAI OFF Flag(for both)",
                "SAI OFF Flag"));

            AddFunction(new Axis(this, STBY_ATTI_INDX, BUTTON_3, "443", 0.15d, -1.0d, 1.0d,
                "Standby Attitude Indicator", 
                "SAI Knob Arrow"));

            AddFunction(new SAIKnob(this));
            #endregion

            #region TACAN System
            // defineIndicatorLight("TACAN_TEST", 260, "TACAN Panel", "TACAN Test Indicator Light (green)")
            AddFunction(new FlagValue(this, "260", "TACAN", "TACAN Test Light", "TACAN Test Indicator Light"));

            // defineFixedStepTumb("TACAN_10", 41, 3001, 256, 0.1, { 0, 1}, { -0.1, 0.1},
            //   nil, "TACAN Panel", "Left Channel Selector")
            
            AddFunction(new Axis(this, TACAN_SYS, BUTTON_1, "256", 0.1,-1.0d,1.0d,
                "TACAN", "Left Channel Selector"));
            AddFunction(new Axis(this, TACAN_SYS, BUTTON_2, "257", 0.1, -1.0d, 1.0d,
                "TACAN", "Right Channel Selector"));

            // defineToggleSwitch("TACAN_XY", 41, 3003, 266, "TACAN Panel", "TACAN Channel X/Y Toggle")

            AddFunction(new Axis(this, TACAN_SYS, BUTTON_3, "266", 0.1d, 0.0d, 1.0d, "TACAN", "TACAN Channel X/Y Toggle"));
           

            // defineMultipositionSwitch("TACAN_SYS", 41, 3006, 262, 4, 0.1, "TACAN Panel", "TACAN Mode Dial")
            
            AddFunction(new Axis(this, TACAN_SYS, BUTTON_6, "262", 0.1d, 0.0d, 1.0d, "TACAN", "TACAN Mode Selector"));

            // definePotentiometer("TACAN_VOL", 41, 3005, 261, { 0, 1}, "TACAN Panel", "TACAN Signal Volume")
            AddFunction(new Axis(this, TACAN_SYS, BUTTON_5, "261", 0.1d, 0.0d, 1.0d, "TACAN", "TACAN Signal Volume"));

            // definePushButton("TACAN_HSI", 41, 3004, 259, "TACAN Panel", "TACAN Signal on HSI Test Button")
            AddFunction(new PushButton(this, TACAN_SYS, BUTTON_4, "259", "TACAN", "TACAN Signal Test Button"));

            // defineString("TACAN_CHANNEL", getTacanChannel, 4, "TACAN Panel", "TACAN Channel")
            AddFunction(new TACANChannel(this));
            AddFunction(new TACANKnobs(this));
            #endregion

            #region UHF Radio AN/ARC-164
            //  UHF Radio AN/ ARC - 164
            //  definePushButton("UHF_TONE_BT", 23, 3009, 310, "UHF Radio", "UHF Radio Tone Button")
            AddFunction(new PushButton(this, UHF_RADIO_SYS, BUTTON_9, "310",
                "UHF Radio", "UHF Radio Tone Button"));

            // definePotentiometer("TACAN_VOL", 41, 3005, 261, { 0, 1}, "TACAN Panel", "TACAN Signal Volume")

            AddFunction(new Axis(this, UHF_RADIO_SYS, BUTTON_2, "327", 0.1d, 0.0d, 0.3d, "UHF Radio", "UHF Radio Knob 100 MHz"));

            AddFunction(new Axis(this, UHF_RADIO_SYS, BUTTON_3, "328", 0.1d, 0.0d, 1.0d, "UHF Radio", "UHF Radio Knob 10 MHz"));

            AddFunction(new Axis(this, UHF_RADIO_SYS, BUTTON_4, "329", 0.1d, 0.0d, 1.0d, "UHF Radio", "UHF Radio Knob 1 MHz"));

            AddFunction(new Axis(this, UHF_RADIO_SYS, BUTTON_5, "330", 0.1d, 0.0d, 1.0d, "UHF Radio", "UHF Radio Knob 0.1 MHz"));

            AddFunction(new Axis(this, UHF_RADIO_SYS, BUTTON_6, "331", 0.1d, 0.0d, 1.0d, "UHF Radio", "UHF Radio Knob 0.025 MHz"));

            AddFunction(new Axis(this, UHF_RADIO_SYS, BUTTON_1, "300", 0.05d, 0.0d, 1.0d, "UHF Radio", "UHF Radio Preset Channel Selector Knob"));

            AddFunction(new Axis(this, UHF_RADIO_SYS, BUTTON_11, "309", 0.05d, 0.0d, 1.0d, "UHF Radio", "UHF Radio Volume Knob"));

            AddFunction(new Axis(this, UHF_RADIO_SYS, BUTTON_8, "311", 0.1d, 0.0d, 0.3d, "UHF Radio", "UHF Radio Function Selector"));

            AddFunction(new Axis(this, UHF_RADIO_SYS, BUTTON_7, "307", 0.1d, 0.0d, 0.2d, "UHF Radio", "UHF Radio Freq Selector"));

            AddFunction(new Axis(this, UHF_RADIO_SYS, BUTTON_10, "308", 0.1d, 0.0d, 1.0d, "UHF Radio", "UHF Radio Squelch Toggle"));

            AddFunction(new Switch(this, UHF_RADIO_SYS, "336",
                 new SwitchPosition[] {
                    new SwitchPosition("1.0", "LOWER", BUTTON_16),
                    new SwitchPosition("0.5", "AUTO", BUTTON_16),
                    new SwitchPosition("0.0", "UPPER", BUTTON_16)},
                 "UHF Radio", "UHF Radio Antenna Selector", "%0.1f"));

            AddFunction(new PushButton(this, UHF_RADIO_MIC, BUTTON_1, "135",
                "UHF Radio", "UHF Radio Microphone Button"));


            AddFunction(new Switch(this, UHF_RADIO_SYS, "335",
                new SwitchPosition[] {
                    new SwitchPosition("1", "open", BUTTON_22),
                    new SwitchPosition("0", "close", BUTTON_22)
                }, "UHF Radio", "Hinged Access Door", "%1d", true, true));

            AddFunction(new PushButton(this, UHF_RADIO_SYS, BUTTON_24, "314",
                "UHF Radio", "UHF Preset Button(RedBtn)"));

            // Preset Display Value
            AddFunction(new ScaledNetworkValue(this, "326", 1d, "UHF Radio",
                "UHF Preset Radio Gauge", "UHF Preset Value", "", BindingValueUnits.Text));
            // Frequency Display Value
            AddFunction(new ScaledNetworkValue(this, "302", 1d, "UHF Radio",
                "UHF Radio Gauge 100MHz", "UHF Radio 100MHz Value", "", BindingValueUnits.Text));
            AddFunction(new ScaledNetworkValue(this, "303", 1d, "UHF Radio",
                "UHF Radio Gauge 10aMHz", "UHF Radio 10MHz Value", "", BindingValueUnits.Text));

            AddFunction(new ScaledNetworkValue(this, "304", 1d, "UHF Radio",
                "UHF Radio Gauge 1MHz", "UHF Radio 1MHz Value", "", BindingValueUnits.Text));

            AddFunction(new ScaledNetworkValue(this, "305", 1d, "UHF Radio",
                "UHF Radio Gauge 0.1MHz", "UHF Radio 0.1MHz Value", "", BindingValueUnits.Text));

            AddFunction(new ScaledNetworkValue(this, "306", 1d, "UHF Radio",
                "UHF Radio Gauge 0.025MHz", "UHF Radio  0.025MHz Value", "", BindingValueUnits.Text));

            AddFunction(new UHFRadioFreqGauges(this));
            AddFunction(new UHFRadioKnobs(this));

            #endregion

            #region Internal Lights

            // defineToggleSwitch("PANEL_L_SW", 12, 3001, 46,"Internal Lights" , "AN/ASG-31 Sight Panel Light Button, ON/OFF")
            //AddFunction(Switch.CreateToggleSwitch(this, INTR_LIGHT_SYS, BUTTON_1, "46",
            //    "1", "on", "0", "off", "Internal Lights", "AN/ASG-31 Light Switch", "%1d"));
            AddFunction(new PushButton(this, INTR_LIGHT_SYS, BUTTON_1, "46", "Internal Lights", "AN/ASG-31 Light Switch"));
            // defineToggleSwitch("COMPAS_L_SW", 12, 3002, 613,"Internal Lights" , "Magnetic Compass Light Switch, LIGHT/OFF")
            AddFunction(Switch.CreateToggleSwitch(this, INTR_LIGHT_SYS, BUTTON_2, "613",
                "1", "on", "0", "off", "Internal Lights", "Magnetic Compass Light Switch", "%1d"));

            // definePotentiometer("FLOOD_LIGHTS", 12, 3003, 221, {0, 1},"Internal Lights" , "Flood Lights Knob")
            AddFunction(new Axis(this, INTR_LIGHT_SYS, BUTTON_3, "221", 0.01d, 0.0d, 1.0d, "Internal Lights", "Flood Lights Knob"));

            // definePotentiometer("FLIGHT_LIGHTS", 12, 3004, 222, { 0, 1},"Internal Lights" , "Flight Instruments Lights Knob")
            AddFunction(new Axis(this, INTR_LIGHT_SYS, BUTTON_4, "222", 0.01d, 0.0d, 1.0d, "Internal Lights", "Flight Lights Knob"));

            // definePotentiometer("ENGINE_LIGHTS", 12, 3005, 223, { 0, 1},"Internal Lights" , "Engine Instruments Lights Knob")
            AddFunction(new Axis(this, INTR_LIGHT_SYS, BUTTON_5, "223", 0.01d, 0.0d, 1.0d, "Internal Lights", "Engine Lights Knob"));

            // definePotentiometer("CONSOLE_LIGHTS", 12, 3006, 224, { 0, 1},"Internal Lights" , "Console Lights Knob")
            AddFunction(new Axis(this, INTR_LIGHT_SYS, BUTTON_6, "224", 0.01d, 0.0d, 1.0d, "Internal Lights", "Console Lights Knob"));

            // definePotentiometer("ARM_LIGHTS", 12, 3007, 363, { 0, 1},"Internal Lights" , "Armament Panel Lights Knob")
            AddFunction(new Axis(this, INTR_LIGHT_SYS, BUTTON_7, "363", 0.01d, 0.0d, 1.0d, "Internal Lights", "Armament Panel Knob"));

            // definePushButton("WARN_T_SW", 12, 3008, 226, "Internal Lights", "Warning Test Switch")
            AddFunction(Switch.CreateToggleSwitch(this, INTR_LIGHT_SYS, BUTTON_8, "226",
                "1", "test", "0", "off", "Internal Lights", "Warning Test Switch", "%1d"));

            // define3PosTumb("BRI_DIM_SW", 12, 3009, 225, "Internal Lights", "Bright/Dim Switch, BRT/NEUT/DIM")

            AddFunction(new Switch(this, INTR_LIGHT_SYS, "225",
                    new SwitchPosition[] {
                    new SwitchPosition("1.0", "Bright", BUTTON_9),
                    new SwitchPosition("0.0", "Norm", BUTTON_9),
                    new SwitchPosition("-1.0", "Dim", BUTTON_10)},
                "Internal Lights", "Bright/Norm/Dim Switch", "%0.1f"));

            // AddFunction(new Axis(this, INTR_LIGHT_SYS, BUTTON_9, "225", 1.0d, -6144.0d, 6144.0d, "Internal Lights", "Bright/Dim Switch"));
            // definePushButton("MC_RESET_BTN", 12, 3011, 172, "Internal Lights", "Master Caution Button")
            AddFunction(new PushButton(this, INTR_LIGHT_SYS, BUTTON_11, "172", "Internal Lights", "Master Caution Button"));


            #endregion

            #region External Light
            // definePotentiometer("NAV_LIGHTS", 11, 3001, 227, { 0, 1},"External Lights" , "Nav Lights Knob")
            AddFunction(new Axis(this, EXT_LIGHT_SYS, BUTTON_1, "227", 0.01d, 0.0d, 1.0d, "External Lights", "Nav Lights Knob"));

            // definePotentiometer("FORMATION_LIGHTS", 11, 3002, 228, { 0, 1},"External Lights" , "Formation Lights Knob")
            AddFunction(new Axis(this, EXT_LIGHT_SYS, BUTTON_2, "228", 0.01d, 0.0d, 1.0d, "External Lights", "Formation Light Knob"));

            // defineToggleSwitch("BEACON", 11, 3003, 229, "External Lights", "Beacon Light Switch")
            AddFunction(Switch.CreateToggleSwitch(this, EXT_LIGHT_SYS, BUTTON_3, "229",
                "1", "on", "0", "off", "External Lights", "Beacon Light Switch", "%1d"));

            // defineToggleSwitch("LG_LIGHT", 11, 3004, 353, "External Lights", "Landing & Taxi Light Switch, ON/OFF")
            AddFunction(Switch.CreateToggleSwitch(this, EXT_LIGHT_SYS, BUTTON_4, "353",
                "1", "on", "0", "off", "External Lights", "LandTaxi Light Switch", "%1d"));
            AddFunction(new LightKnobs(this));
            #endregion

            #region stanby-compass
            // Standby Compass Heading
            AddFunction(new ScaledNetworkValue(this, "610", 360d, "Standby Compass",
                "Compass Heading", "Standby Compass Heading", "", BindingValueUnits.Degrees));
            #endregion


        }
    }
}