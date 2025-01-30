USE [TaskPlanner]
GO

/****** Object:  View [dbo].[hh_graphic_detail]    Script Date: 16/01/2025 11:33:32 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

ALTER VIEW [dbo].[hh_graphic_detail] AS
SELECT  
    mi.ID AS MilestoneItemID,
    mi.ContractID,
    mv.ID AS MilestoneValueID,
    mv.MilestonesID,
    result.DisplacementServiceName,
    result.DisplacementServicesID,
    result.ValueHour,
    result.HoursExpected,
    result.HoursPlanned,
    result.HoursExecuted,
    result.CostHoursExpected,
    result.CostHoursPlanned,
    result.CostHoursExecuted
FROM 
    [TaskPlanner].[dbo].[MilestonesItem] mi
JOIN 
    [TaskPlanner].[dbo].[MilestonesValue] mv
ON 
    mi.ID = mv.MilestonesID
CROSS APPLY 
    (
        SELECT 
            vPMT.[ID] AS MilestoneTypeID,
            vPMT.[DisplacementServiceName],
            vPMT.[DisplacementServicesID],
            vPMT.[ValueHour],
            CAST(vPMT.[Hours] AS FLOAT) AS HoursExpected,
            CAST(ISNULL(SUM(AP.PlannedManHour), 0) AS FLOAT) AS HoursPlanned,
            CAST(ISNULL(SUM(AP.ExecutedManHour), 0) AS FLOAT) AS HoursExecuted,
            CAST(vPMT.[Hours] * vPMT.[ValueHour] AS FLOAT) AS CostHoursExpected,
            CAST(ISNULL(dbo.GetCostMilestoneByPlannedHours(vMV.ContractID, vMV.MilestonesID), 0) AS FLOAT) AS CostHoursPlanned,
            CAST(ISNULL(dbo.GetCostMilestoneByExecutedHours(vMV.ContractID, vMV.MilestonesID), 0) AS FLOAT) AS CostHoursExecuted
        FROM 
            [TaskPlanner].[dbo].[vPM_MilestoneType] vPMT
        INNER JOIN dbo.vMileStonesValue vMV ON vMV.ID = vPMT.MilestonesValueID
        INNER JOIN dbo.ActivityPlan AP ON AP.ContractID = vMV.ContractID
        WHERE 
            vPMT.MilestonesValueID = mv.ID
        GROUP BY 
            vPMT.ID, vPMT.DisplacementServiceName, vPMT.DisplacementServicesID,
            vPMT.ValueHour, vPMT.Hours, vMV.ContractID, vMV.MilestonesID
    ) result;
GO

