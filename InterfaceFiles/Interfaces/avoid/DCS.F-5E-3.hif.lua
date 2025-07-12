--[[

function driver.processHighImportance(mainPanelDevice)
    -- called at configured update rate

    -- example for combining/processing arguments:
    helios.send(2001, string.format(
            "%0.4f;%0.4f;%0.4f",
            mainPanelDevice:get_argument_value(220),
            mainPanelDevice:get_argument_value(219),
            mainPanelDevice:get_argument_value(218)
        )
    )

    -- example for structured indications data:
    local li = helios.parseIndication(1)
    if li then
        helios.send(2002, string.format("%s", helios.ensureString(li.someNamedField1)))
        helios.send(2003, string.format("%s", helios.ensureString(li.someNamedField2)))
    end
    
    -- Calcuate HSI Value HSI_Range_100 HSI_Range_10 HSI_Range_1
	helios.send(2029, string.format("%0.2f;%0.2f;%0.4f", mainPanelDevice:get_argument_value(268), mainPanelDevice:get_argument_value(269), mainPanelDevice:get_argument_value(270)))
end

]]

--[[

function driver.processLowImportance(mainPanelDevice) --luacheck: no unused args
    -- same things as processHighImportance can be done here, called a few times per second at most
	-- TACAN Channel
	helios.send(2263, string.format("%0.2f;%0.2f;%0.2f", mainPanelDevice:get_argument_value(263), mainPanelDevice:get_argument_value(264), mainPanelDevice:get_argument_value(265)))
end

]]