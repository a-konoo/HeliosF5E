-- Exports.Lua from Helios F-5E-3 Interface
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
	-- Radio Frequencies Gauge
	helios.send(2302, string.format("%.2f;%.2f;%.2f;%.2f;%.2f",mainPanelDevice:get_argument_value(302), mainPanelDevice:get_argument_value(303), mainPanelDevice:get_argument_value(304), mainPanelDevice:get_argument_value(305), mainPanelDevice:get_argument_value(306)))
	helios.send(2326, string.format("%.2f",mainPanelDevice:get_argument_value(326)))
	-- Radio Panel Knobs And toggles
	helios.send(2327, string.format("%.2f;%.2f;%.2f;%.2f;%.2f;%.2f;%.2f;%.2f;%.2f;%.2f",mainPanelDevice:get_argument_value(327), mainPanelDevice:get_argument_value(328), mainPanelDevice:get_argument_value(329), mainPanelDevice:get_argument_value(330),mainPanelDevice:get_argument_value(331), mainPanelDevice:get_argument_value(300),mainPanelDevice:get_argument_value(309),mainPanelDevice:get_argument_value(311),mainPanelDevice:get_argument_value(307),mainPanelDevice:get_argument_value(308)))

	-- TACAN Channel
	helios.send(2263, string.format("%0.2f;%0.2f;%0.2f;%0.2f", mainPanelDevice:get_argument_value(263), mainPanelDevice:get_argument_value(264), mainPanelDevice:get_argument_value(265),mainPanelDevice:get_argument_value(266)))
	
	-- TACAN Panel Knobs
	helios.send(2266, string.format("%0.2f;%0.2f;%0.2f;%0.2f", mainPanelDevice:get_argument_value(266),mainPanelDevice:get_argument_value(262),mainPanelDevice:get_argument_value(261),mainPanelDevice:get_argument_value(273)))


	-- Weapon Panel Selectors
	local wpItv = mainPanelDevice:get_argument_value(340)
	local wpbm = mainPanelDevice:get_argument_value(341)
	local wpes = mainPanelDevice:get_argument_value(344)
	helios.send(3346, string.format("%0.2f;%0.2f;%0.2f",wpItv,wpbm,wpes))

	-- DragChute
	helios.send(2091, string.format("%0.1f", mainPanelDevice:get_argument_value(91)))
	-- CMDS
	helios.send(3401, string.format("%0.2f;%0.2f", mainPanelDevice:get_argument_value(401), mainPanelDevice:get_argument_value(402)))
	helios.send(3405, string.format("%0.2f;%0.2f", mainPanelDevice:get_argument_value(405), mainPanelDevice:get_argument_value(406)))
	helios.send(3406, string.format("%0.2f;%0.2f", mainPanelDevice:get_argument_value(400), mainPanelDevice:get_argument_value(404),mainPanelDevice:get_argument_value(324)))
	

	-- Sight depress drums
	helios.send(2040, string.format("%0.2f;%0.2f;%0.2f;%0.2f", mainPanelDevice:get_argument_value(40),mainPanelDevice:get_argument_value(41),mainPanelDevice:get_argument_value(47),mainPanelDevice:get_argument_value(46)))
	-- Sight knobs Buttons
	helios.send(2043, string.format("%0.2f;%0.2f;%0.2f", mainPanelDevice:get_argument_value(43),mainPanelDevice:get_argument_value(44),mainPanelDevice:get_argument_value(45)))

	-- StandBy Attitude Indicator Trim Value
	helios.send(2442, string.format("%0.3f", mainPanelDevice:get_argument_value(442)))
	
	-- RWR IC Panel Buttons + Knobs
	local rwrMode = mainPanelDevice:get_argument_value(551)
	local rwrSearch = mainPanelDevice:get_argument_value(554)
	local rwrHandoff = mainPanelDevice:get_argument_value(556)
	local rwrAlti = mainPanelDevice:get_argument_value(561)
	local rwrT = mainPanelDevice:get_argument_value(564)
	local rwrTest = mainPanelDevice:get_argument_value(567)
	local rwrUnknown = mainPanelDevice:get_argument_value(570)
	local rwrPwr = mainPanelDevice:get_argument_value(575)
	local rwrLnch = mainPanelDevice:get_argument_value(559)
	local rwrActPwr = mainPanelDevice:get_argument_value(573)
	helios.send(2551, string.format("%0.2f;%0.2f;%0.2f;%0.2f;%0.2f;%0.2f;%0.2f;%0.2f;%0.2f;%0.2f",rwrMode,rwrSearch,rwrHandoff,rwrAlti,rwrT,rwrTest,rwrUnknown,rwrPwr,rwrLnch,rwrActPwr))
	helios.send(2578, string.format("%0.2f;%0.2f",mainPanelDevice:get_argument_value(578),mainPanelDevice:get_argument_value(577)))

	-- RaderPanelKnob And Rader Screen Knob
	local rdRnge = mainPanelDevice:get_argument_value(315)
	local rdMode = mainPanelDevice:get_argument_value(316)
	local rdScle = mainPanelDevice:get_argument_value(65)
	local rdBrig = mainPanelDevice:get_argument_value(70)
	local rdPrst = mainPanelDevice:get_argument_value(69)
	local rdVid = mainPanelDevice:get_argument_value(68)
	local rdCurs = mainPanelDevice:get_argument_value(67)
	local rdPitch = mainPanelDevice:get_argument_value(66)
	helios.send(2315, string.format("%0.2f;%0.2f;%0.2f;%0.2f;%0.2f;%0.2f;%0.2f;%0.2f",rdRnge,rdMode,rdScle,rdBrig,rdPrst,rdVid,rdCurs,rdPitch))
	-- Rader Operate Knob
	helios.send(2316, string.format("%0.2f;%0.2f",mainPanelDevice:get_argument_value(315),mainPanelDevice:get_argument_value(316)))

	-- IIF Wheels

	-- F-5 IFF I/O mapping is wrong,so I had no choice but to do applying "potentiometer" to input of IFF toggles.
	-- IFF Wheel input and IFF toggles output mapping are crossed wrongly 
	-- and Code Selector of Master(get_argument_value(200)) can't work well on any values for putting to output bindings.

	local wheel1 = mainPanelDevice:get_argument_value(209) -- IFF MODE 1 Wheel1
	local wheel2 = mainPanelDevice:get_argument_value(210) -- IFF MODE 1 Wheel2
	local wheel2 = mainPanelDevice:get_argument_value(210) -- IFF MODE 1 Wheel2
	local wheel3 = mainPanelDevice:get_argument_value(211) -- IFF MODE 3/A Wheel1
	local wheel4 = mainPanelDevice:get_argument_value(212) -- IFF MODE 3/A Wheel2
	local wheel5 = mainPanelDevice:get_argument_value(213) -- IFF MODE 3/A Wheel3
	local wheel6 = mainPanelDevice:get_argument_value(214) -- IFF MODE 3/A Wheel4
	helios.send(2552, string.format("%0.2f;%0.2f;%0.2f;%0.2f;%0.2f;%0.2f",wheel1,wheel2,wheel3,wheel4,wheel5,wheel6))

	-- IFF MODE 4 CODE Selector
	local selectorL = mainPanelDevice:get_argument_value(199)     -- IFF MODE 4 CODE Selector
	local selectorR = mainPanelDevice:get_argument_value(200)     -- IFF MASTER Control Selector
	local selectorLPull = mainPanelDevice:get_argument_value(197) -- IFF MODE 4 CODE Selector Pull Toggle
	local selectorRPull = mainPanelDevice:get_argument_value(198) -- IFF MASTER Control Selector Pull Toggle
	helios.send(2553, string.format("%0.2f;%0.2f;%0.2f;%0.2f",selectorL,selectorR,selectorLPull,selectorRPull))

	-- IFF Toggles
	local tgl3MonCntr = mainPanelDevice:get_argument_value(201) or 0    -- IFF MODE 4 Monitor Control Switch, AUDIO/OUT/LIGHT
	local tgl3M1 = mainPanelDevice:get_argument_value(202) or 0	        -- IFF Mode Select/TEST Switch, M-1 /ON/OUT
	local tgl3M2 = mainPanelDevice:get_argument_value(203) or 0         -- IFF Mode Select/TEST Switch, M-2 /ON/OUT
	local tgl3M3 = mainPanelDevice:get_argument_value(204) or 0         -- IFF Mode Select/TEST Switch, M-3 /ON/OUT
	local tgl3MC = mainPanelDevice:get_argument_value(205) or 0         -- IFF Mode Select/TEST Switch, M-C /ON/OUT
	local tgl3RAD = mainPanelDevice:get_argument_value(206) or 0        -- IFF RAD TEST/MON Switch, RAD TEST/OUT/MON
	local tgl3IDENT = mainPanelDevice:get_argument_value(207) or 0      -- IFF Identification of Position (IP) Switch, IDENT/OUT/MIC
	local tgl2Cntr = mainPanelDevice:get_argument_value(208) or 0       -- IFF MODE 4 Control Switch, ON/OUT
	
	helios.send(2554, string.format("%d;%d;%d;%d;%d",tgl3M1,tgl3M2,tgl3M3,tgl3MC,tgl3RAD))
	helios.send(2555, string.format("%d;%d;%d",tgl3MonCntr,tgl3IDENT,tgl2Cntr))

	-- Lights Axis
	local li1 = mainPanelDevice:get_argument_value(221) or 0    -- Flood Lights Knob
	local li2 = mainPanelDevice:get_argument_value(222) or 0	-- Flight Instruments Lights Knob
	local li3 = mainPanelDevice:get_argument_value(223) or 0    -- Engine Instruments Lights Knob
	local li4 = mainPanelDevice:get_argument_value(224) or 0	-- Armament Panel Lights Knob
	local li5 = mainPanelDevice:get_argument_value(363) or 0	-- Nav Lights Knob
	local li6 = mainPanelDevice:get_argument_value(228) or 0	-- Formation Light Knob
	helios.send(2556, string.format("%0.2f;%0.2f;%0.2f;%0.2f;%0.2f;%0.2f",li1,li2,li3,li4,li5,li6))

	-- GearLever Values
	helios.send(2557, string.format("%0.2f", mainPanelDevice:get_argument_value(83)))

	-- Throttle Values
	helios.send(2558, string.format("%0.2f", mainPanelDevice:get_argument_value(383)))

	-- Jettison Values
	local jt1 = mainPanelDevice:get_argument_value(364)
	local jt2 = mainPanelDevice:get_argument_value(367)
	helios.send(2559, string.format("%0.2f;%0.2f",jt1,jt2))

	-- Front Knobs
	local mvlm = mainPanelDevice:get_argument_value(345)
	local cbnTmp = mainPanelDevice:get_argument_value(373)
	local defogknob = mainPanelDevice:get_argument_value(374)
	helios.send(2560, string.format("%0.2f;%0.2f;%0.2f", mvlm,cbnTmp,defogknob))
	-- RaderKnobs
end