USE [TaskPlanner]
GO

/****** Object:  StoredProcedure [dbo].[GetCombinedMilestoneDetails]    Script Date: 16/01/2025 11:49:19 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[GetCombinedMilestoneDetails]
    @MilestonesValueID UNIQUEIDENTIFIER
AS
BEGIN
    -- Declare a variable to hold the ContractID
    DECLARE @ContractID INT;

    -- Retrieve the ContractID based on the provided MilestonesValueID
    SELECT @ContractID = ContractID
    FROM dbo.vMileStonesValue
    WHERE ID = @MilestonesValueID;

    -- First Query (Modified to match columns)
    SELECT
    
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
        CAST(ISNULL(dbo.GetCostMilestoneByExecutedHours(vMV.ContractID, vMV.MilestonesID), 0) AS FLOAT) AS CostHoursExecuted,
        NULL AS ScheduledDate
    FROM
        [TaskPlanner].[dbo].[vPM_MilestoneType] vPMT
        INNER JOIN dbo.vMileStonesValue vMV ON vMV.ID = vPMT.MilestonesValueID
        INNER JOIN dbo.ActivityPlan AP ON AP.ContractID = vMV.ContractID
    WHERE
        vPMT.MilestonesValueID = @MilestonesValueID
    GROUP BY
        vPMT.ID, vPMT.DepartmentName, vPMT.DisplacementServiceName, vPMT.DepartmentID,
        vMV.ID, vPMT.DisplacementServicesID, vPMT.FunctionID, vPMT.FunctionName,
        vPMT.ValueHour, vPMT.Hours, vMV.ContractID, vMV.MilestonesID

    UNION ALL

    -- Second Query (Modified to use @ContractID)
    SELECT
       
        vMT.ID AS MilestoneTypeID,
        D.Name AS DepartmentName,
        vMT.DisplacementServiceName,
        D.ID AS DepartmentID,
        MV.ID AS MilestonesValueID,
        vMT.DisplacementServicesID,
        F.ID AS FunctionID,
        F.Name AS FunctionName,
        CAST(0 AS FLOAT) AS ValueHour,
        CAST(0 AS FLOAT) AS HoursExpected,
        CAST(ISNULL(SUM(AP.PlannedManHour), 0) AS FLOAT) AS HoursPlanned,
        CAST(ISNULL(SUM(AP.ExecutedManHour), 0) AS FLOAT) AS HoursExecuted,
        CAST(0 AS FLOAT) AS CostHoursExpected,
        CAST(ISNULL(dbo.GetCostByExecutedHours(U.ID, AP.ScheduledDate, AP.PlannedManHour), 0) AS FLOAT) AS CostHoursPlanned,
        CAST(ISNULL(dbo.GetCostByExecutedHours(U.ID, AP.ScheduledDate, AP.ExecutedManHour), 0) AS FLOAT) AS CostHoursExecuted,
        AP.ScheduledDate
    FROM
        dbo.ActivityPlan AP
		left JOIN dbo.Team T on T.ID = AP.ExecutorTeamID
        left JOIN dbo.Users U ON U.ID = T.ID
        inner JOIN dbo.Functions F ON F.ID = U.FunctionID
        inner JOIN dbo.Department D ON D.ID = U.department_id
        inner JOIN dbo.MilestonesItem M ON M.ID = AP.MilestonesID
        inner JOIN dbo.MilestonesValue MV ON MV.MilestonesID = M.ID
        inner JOIN dbo.vPM_MilestoneType vMT ON vMT.MilestonesValueID = MV.ID
    WHERE
        AP.ContractID = @ContractID
    GROUP BY
        vMT.ID, D.Name, vMT.DisplacementServiceName, D.ID, MV.ID,
        vMT.DisplacementServicesID, F.ID, F.Name, vMT.ValueHour,
        AP.ScheduledDate, AP.PlannedManHour, AP.ExecutedManHour, U.ID
END
GO

