-- Exports.Lua from Helios F-5E-3(Simple) Interface
function driver.processHighImportance(mainPanelDevice)

	-- Send Altimeter Values
	local al10000 = mainPanelDevice:get_argument_value(11)
	local al1000 = mainPanelDevice:get_argument_value(520)
	local al100 = mainPanelDevice:get_argument_value(521)
	local al100p = mainPanelDevice:get_argument_value(10)
	local ap1 = mainPanelDevice:get_argument_value(59)
	local ap2 = mainPanelDevice:get_argument_value(58)
	local ap3 = mainPanelDevice:get_argument_value(57)
	local ap4 = mainPanelDevice:get_argument_value(56)
	helios.send(2051, string.format("%0.2f;%0.2f;%0.2f;%0.2f", al10000, al1000, al100, al100p))
	helios.send(2059, string.format("%0.2f;%0.2f;%0.2f;%0.3f",  ap1, ap2, ap3, ap4))

	-- HSI Distance
	local ds1 = mainPanelDevice:get_argument_value(268)
	local ds2 = mainPanelDevice:get_argument_value(269)
	local ds3 = mainPanelDevice:get_argument_value(270)
	helios.send(2268, string.format("%0.2f;%0.2f;%0.2f;", ds1, ds2, ds3))
	
	helios.send(2146, string.format("%0.2f;", mainPanelDevice:get_argument_value(146)))

	-- Send Attitude Indicator Values
	helios.send(2081, string.format("%0.2f;%0.2f;", mainPanelDevice:get_argument_value(81), mainPanelDevice:get_argument_value(50)))
	-- Weapn System Guard Action
	helios.send(2342, string.format("%1d;%0.2f;0.0;",mainPanelDevice:get_argument_value(342),mainPanelDevice:get_argument_value(343)))

	-- Calclate Throttle Position From RPM & Fuel Flow
	helios.send(2343, string.format("%0.2f;%0.2f;%0.2f;%0.2f;",mainPanelDevice:get_argument_value(16),mainPanelDevice:get_argument_value(17),mainPanelDevice:get_argument_value(525), mainPanelDevice:get_argument_value(526)))

end

function driver.processLowImportance(mainPanelDevice)
	-- Get Radio Frequencies
	local lUHFRadio = GetDevice(23)

	-- ILS Frequency
	--helios.send(2251, string.format("%0.1f;%0.1f", mainPanelDevice:get_argument_value(251), mainPanelDevice:get_argument_value(252)))
	-- TACAN Channel
	helios.send(2263, string.format("%0.2f;%0.2f;%0.2f", mainPanelDevice:get_argument_value(263), mainPanelDevice:get_argument_value(264), mainPanelDevice:get_argument_value(265)))
	helios.send(2326, string.format("%0.2f;",mainPanelDevice:get_argument_value(326)))
	-- Weapon Levers(not master arm sw and guard)
	local wpL3 = mainPanelDevice:get_argument_value(346)
	local wpL2 = mainPanelDevice:get_argument_value(347)
	local wpL1 = mainPanelDevice:get_argument_value(348)
	local wpCn = mainPanelDevice:get_argument_value(349)
	local wpR1 = mainPanelDevice:get_argument_value(350)
	local wpR2 = mainPanelDevice:get_argument_value(351)
	local wpR3 = mainPanelDevice:get_argument_value(352)
	local wpItv = mainPanelDevice:get_argument_value(340)
	local wpbm = mainPanelDevice:get_argument_value(341)
	local wpes = mainPanelDevice:get_argument_value(344)
	helios.send(3346, string.format("%0.2f;%0.2f;%0.2f;%0.2f;%0.2f;%0.2f;%0.2f;%0.2f;%0.2f;%0.2f",wpL3,wpL2,wpL1,wpCn,wpR1,wpR2,wpR3,wpItv,wpbm,wpes))
	-- DRAGCHUTE
	helios.send(2091, string.format("%0.1f", mainPanelDevice:get_argument_value(91)))
	-- CMDS
	helios.send(3401, string.format("%0.2f;%0.2f", mainPanelDevice:get_argument_value(401), mainPanelDevice:get_argument_value(402)))
	helios.send(3405, string.format("%0.2f;%0.2f", mainPanelDevice:get_argument_value(405), mainPanelDevice:get_argument_value(406)))
	-- radio test
	helios.send(2302, string.format("%.2f;%.2f;%.2f;%.2f;%.2f",mainPanelDevice:get_argument_value(302), mainPanelDevice:get_argument_value(303), mainPanelDevice:get_argument_value(304), mainPanelDevice:get_argument_value(305), mainPanelDevice:get_argument_value(306)))
	-- Send StandBy Attitude Indicator Trim Values
	helios.send(2442, string.format("%0.3f", mainPanelDevice:get_argument_value(443)))
	
	-- RWR IC Panel Buttons
	local rwrMode = mainPanelDevice:get_argument_value(551)
	local rwrSearch = mainPanelDevice:get_argument_value(554)
	local rwrHandoff = mainPanelDevice:get_argument_value(556)
	local rwrAlti = mainPanelDevice:get_argument_value(561)
	local rwrT = mainPanelDevice:get_argument_value(564)
	local rwrTest = mainPanelDevice:get_argument_value(567)
	local rwrUnknown = mainPanelDevice:get_argument_value(570)
	local rwrPwr = mainPanelDevice:get_argument_value(575)
	local rwrLnch = mainPanelDevice:get_argument_value(573)
	local rwrActPwr = mainPanelDevice:get_argument_value(578)
	helios.send(2551, string.format("%0.2f;%0.2f;%0.2f;%0.2f;%0.2f;%0.2f;%0.2f;%0.2f;%0.2f;%0.2f",rwrMode,rwrSearch,rwrHandoff,rwrAlti,rwrT,rwrTest,rwrUnknown,rwrPwr,rwrLnch,rwrActPwr))
end