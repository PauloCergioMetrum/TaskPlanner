USE [TaskPlanner]
GO

/****** Object:  View [dbo].[vChartStatusreportTotalcost]    Script Date: 16/01/2025 11:36:41 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [dbo].[vChartStatusreportTotalcost] AS
WITH TotalCalculations AS (
    SELECT
        SUM(ISNULL(MT.TotalMilesStone, 0)) + SUM(ISNULL(CP.TotalPlanned, 0)) AS TotalExpected,
        SUM(ISNULL(MT.ValueHour, 0) * ISNULL(AP.ExecutedManHour, 0)) + SUM(ISNULL(CM.Total, 0)) AS TotalExecutedManHours
    FROM [TaskPlanner].[dbo].[MilestonesItem] MI
    LEFT JOIN dbo.MilestonesValue MV ON MV.MilestonesID = MI.ID
    LEFT JOIN dbo.PM_MilestonesType MT ON MT.MilestonesValueID = MV.ID
    LEFT JOIN dbo.ActivityPlan AP ON AP.ContractID = MI.ContractID
    LEFT JOIN dbo.Pm_Cost_Planned CP ON CP.ContractID = MI.ContractID
    LEFT JOIN dbo.vPmCostMade CM ON CM.Pm_Cost_Planned_Id = CP.Id
),

TotalPlannedAndExecuted AS (
    SELECT 
        SUM([TotalPlanned]) AS TotalPlannedSum,
        SUM([TotalMade]) AS TotalPlannedSumExecuted
    FROM [TaskPlanner].[dbo].[vPM_Mobilization_Combined]
),

AcquisitionTotals AS (
    SELECT 
        SUM([TotalCost]) AS TotalCostECT
    FROM [TaskPlanner].[dbo].[vPM_Acquisition_Combined]
),

ValueTotals AS (
    SELECT 
        SUM([Value]) AS TotalValue
    FROM [TaskPlanner].[dbo].[vPM_Acquisition_Cost]
)

SELECT 
    (TC.TotalExpected + AT.TotalCostECT) AS TotalExpected,
    (TC.TotalExecutedManHours + VT.TotalValue) AS TotalExecutedManHours
FROM 
    TotalCalculations TC, 
    TotalPlannedAndExecuted TPE,
    AcquisitionTotals AT,
    ValueTotals VT;
GO

