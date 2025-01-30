USE [TaskPlanner]
GO

/****** Object:  StoredProcedure [dbo].[GetMilestoneType_HH_Details]    Script Date: 16/01/2025 12:50:56 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


 
 
 
CREATE PROCEDURE [dbo].[GetMilestoneType_HH_Details]
    @MilestonesValueID UNIQUEIDENTIFIER
AS
BEGIN
    -- Declare a variable to hold the ContractID
    DECLARE @ContractID INT;
 
    -- Retrieve the ContractID based on the provided MilestonesValueID
    SELECT @ContractID = ContractID
    FROM dbo.vMileStonesValue
    WHERE ID = @MilestonesValueID;
 
    -- First Query (Unchanged)
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
 
    -- Second Query (Modified to generate random MilestoneTypeID)
    SELECT
        CONVERT(VARCHAR(36), NEWID()) AS MilestoneTypeID,  -- Generate a random uniqueidentifier
        D.Name AS DepartmentName,
        vMT.DisplacementServiceName,
        D.ID AS DepartmentID,
        MV.ID AS MilestonesValueID,
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
        INNER JOIN dbo.MilestonesItem M ON M.ID = AP.MilestonesID
        INNER JOIN dbo.MilestonesValue MV ON MV.MilestonesID = M.ID
        INNER JOIN dbo.vPM_MilestoneType vMT ON vMT.MilestonesValueID = MV.ID
    WHERE
        AP.ContractID = @ContractID
    GROUP BY
        vMT.ID, D.Name, vMT.DisplacementServiceName, D.ID, MV.ID,
        vMT.DisplacementServicesID, F.ID, F.Name
END

GO

