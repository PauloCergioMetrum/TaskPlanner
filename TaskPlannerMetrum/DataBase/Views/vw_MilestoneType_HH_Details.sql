USE [TaskPlanner]
GO

/****** Object:  View [dbo].[vw_MilestoneType_HH_Details]    Script Date: 16/01/2025 11:43:57 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE VIEW [dbo].[vw_MilestoneType_HH_Details] AS
    SELECT TOP (1000) 
           vPMT.[ID] AS MilestoneTypeID,
           vPMT.[DepartmentName],
           vPMT.[DisplacementServiceName],
           vPMT.[DepartmentID],
           vMV.[ID] AS MilestonesValueID,
           vPMT.[DisplacementServicesID],
           vPMT.[FunctionID],
           vPMT.[FunctionName],
           vPMT.[ValueHour],
           CAST(vPMT.[Hours] AS FLOAT) AS HoursExpected,           
           CAST(ISNULL(SUM(AP.PlannedManHour), 0) AS FLOAT) AS HoursPlanned,
           CAST(ISNULL(SUM(AP.ExecutedManHour), 0) AS FLOAT) AS HoursExecuted,
           CAST(vPMT.[Hours] * vPMT.[ValueHour] AS FLOAT) AS CostHoursExpected,
           CAST(ISNULL(dbo.GetCostMilestoneByPlannedHours(vMV.ContractID, vMV.MilestonesID), 0) AS FLOAT) AS CostHoursPlanned,
           CAST(ISNULL(dbo.GetCostMilestoneByExecutedHours(vMV.ContractID, vMV.MilestonesID), 0) AS FLOAT) AS CostHoursExecuted
    FROM [TaskPlanner].[dbo].[vPM_MilestoneType] vPMT
    INNER JOIN dbo.vMileStonesValue vMV ON vMV.ID = vPMT.MilestonesValueID
    INNER JOIN dbo.ActivityPlan AP ON AP.ContractID = vMV.ContractID
    -- LEFT JOIN dbo.vContractProject vCP ON vCP.id = vMV.ContractID
    GROUP BY 
           vPMT.ID, vPMT.DepartmentName, vPMT.DisplacementServiceName, vPMT.DepartmentID, 
           vMV.ID, vPMT.DisplacementServicesID, vPMT.FunctionID, vPMT.FunctionName, 
           vPMT.ValueHour, vPMT.Hours, vMV.ContractID, vMV.MilestonesID
GO

