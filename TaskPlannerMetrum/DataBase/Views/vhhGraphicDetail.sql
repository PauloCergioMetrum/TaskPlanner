USE [TaskPlanner]
GO

/****** Object:  View [dbo].[vhhGraphicDetail]    Script Date: 16/01/2025 11:37:56 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE VIEW [dbo].[vhhGraphicDetail] AS
SELECT 
    vPMT.[ID] AS MilestoneTypeID,
    vPMT.[DepartmentName],
    vPMT.[DisplacementServiceName],
    vPMT.[DepartmentID],
    vMV.[ID] AS MilestonesValueID,
    vMV.MilestonesID, 
    MI.ContractID AS ContractID, -- Alterado o alias para ContractID
    vPMT.[DisplacementServicesID],
    vPMT.[FunctionID],
    vPMT.[FunctionName],
    vPMT.[ValueHour],
    CAST(vPMT.[Hours] AS FLOAT) AS HoursExpected,
    CAST(ISNULL(SUM(AP.PlannedManHour), 0) AS FLOAT) AS HoursPlanned,
    CAST(ISNULL(SUM(AP.ExecutedManHour), 0) AS FLOAT) AS HoursExecuted,
    CAST(vPMT.[Hours] * vPMT.[ValueHour] AS FLOAT) AS CostHoursExpected,
    CAST(ISNULL(dbo.GetCostMilestoneByPlannedHours(vMV.ContractID, vMV.MilestonesID), 0) AS FLOAT) AS CostHoursPlanned,
    CAST(ISNULL(dbo.GetCostMilestoneByExecutedHours(vMV.ContractID, vMV.MilestonesID), 0) AS FLOAT) AS CostHoursExecuted,
    NULL AS ScheduledDate
FROM
    [TaskPlanner].[dbo].[vPM_MilestoneType] vPMT
    INNER JOIN dbo.vMileStonesValue vMV ON vMV.ID = vPMT.MilestonesValueID
    INNER JOIN dbo.MilestonesItem MI ON MI.ID = vMV.MilestonesID
    INNER JOIN dbo.ActivityPlan AP ON AP.ContractID = vMV.ContractID
GROUP BY
    vPMT.ID, vPMT.DepartmentName, vPMT.DisplacementServiceName, vPMT.DepartmentID,
    vMV.ID, vMV.MilestonesID, MI.ContractID, 
    vPMT.DisplacementServicesID, vPMT.FunctionID, vPMT.FunctionName,
    vPMT.ValueHour, vPMT.Hours, vMV.ContractID

UNION ALL

SELECT
    CONVERT(VARCHAR(36), NEWID()) AS MilestoneTypeID, 
    D.Name AS DepartmentName,
    vMT.DisplacementServiceName,
    D.ID AS DepartmentID,
    MV.ID AS MilestonesValueID,
    MV.MilestonesID, 
    MI.ContractID AS ContractID, -- Alterado o alias para ContractID
    vMT.DisplacementServicesID,
    F.ID AS FunctionID,
    F.Name AS FunctionName,
    CAST(0 AS FLOAT) AS ValueHour,
    CAST(0 AS FLOAT) AS HoursExpected,
    SUM(CAST(ISNULL(AP.PlannedManHour, 0) AS FLOAT)) AS HoursPlanned,
    SUM(CAST(ISNULL(AP.ExecutedManHour, 0) AS FLOAT)) AS HoursExecuted,
    CAST(0 AS FLOAT) AS CostHoursExpected,
    SUM(CAST(ISNULL(dbo.GetCostByExecutedHours(U.ID, AP.ScheduledDate, AP.PlannedManHour), 0) AS FLOAT)) AS CostHoursPlanned,
    SUM(CAST(ISNULL(dbo.GetCostByExecutedHours(U.ID, AP.ScheduledDate, AP.ExecutedManHour), 0) AS FLOAT)) AS CostHoursExecuted,
    NULL AS ScheduledDate
FROM
    dbo.ActivityPlan AP
    LEFT JOIN dbo.Users U ON U.ID = AP.ExecutorTeamID
    LEFT JOIN dbo.Functions F ON F.ID = U.FunctionID
    INNER JOIN dbo.Department D ON D.ID = U.department_id
    INNER JOIN dbo.MilestonesItem MI ON MI.ID = AP.MilestonesID 
    INNER JOIN dbo.MilestonesValue MV ON MV.MilestonesID = MI.ID
    INNER JOIN dbo.vPM_MilestoneType vMT ON vMT.MilestonesValueID = MV.ID
GROUP BY
    vMT.ID, D.Name, vMT.DisplacementServiceName, D.ID, MV.ID, MV.MilestonesID, MI.ContractID, 
    vMT.DisplacementServicesID, F.ID, F.Name;
GO

