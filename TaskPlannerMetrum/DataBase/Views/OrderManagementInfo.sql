USE [TaskPlanner]
GO

/****** Object:  View [dbo].[OrderManagementInfo]    Script Date: 16/01/2025 11:34:43 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO




CREATE VIEW [dbo].[OrderManagementInfo] AS
WITH CostData AS (
    SELECT 
        mi.ContractID,
        CAST(ISNULL(dbo.GetCostMilestoneByPlannedHours(mi.ContractID, mv.MilestonesID), 0) AS FLOAT) AS CostHoursPlanned,
        CAST(ISNULL(dbo.GetCostMilestoneByExecutedHours(mi.ContractID, mv.MilestonesID), 0) AS FLOAT) AS CostHoursExecuted,
        CAST(ISNULL(pmt.Hours * pmt.ValueHour, 0) AS FLOAT) AS DifferenceExpectedExecuted
    FROM 
        [dbo].[MilestonesItem] mi
    INNER JOIN 
        [dbo].[MilestonesValue] mv ON mi.ID = mv.MilestonesID
    INNER JOIN 
        [dbo].[vPM_MilestoneType] pmt ON pmt.MilestonesValueID = mv.ID
),
TotalDifferenceData AS (
    SELECT 
        p.ContractID,
        CAST(COALESCE(
            (SELECT SUM(vMTD.CostHoursExpected)
             FROM dbo.MilestonesItem mi
             INNER JOIN dbo.MilestonesValue mv ON mv.MilestonesID = mi.ID
             INNER JOIN dbo.vw_MilestoneType_HH_Details vMTD ON vMTD.MilestonesValueID = mv.ID
             WHERE mi.ContractID = p.ContractID), 0) AS FLOAT) +
        CAST(COALESCE(SUM(p.TotalPlanned), 0) AS FLOAT) +
        CAST(COALESCE(
            (SELECT SUM(TotalPlanned)
             FROM [dbo].[vPM_Mobilization_Combined] m
             WHERE m.ContractID = p.ContractID), 0) AS FLOAT) +
        CAST(COALESCE(
            (SELECT SUM(PredictedTotal)
             FROM [dbo].[vPM_Acquisition_Combined] a
             WHERE a.ContractID = p.ContractID), 0) AS FLOAT) +
        CAST(COALESCE(
            (SELECT SUM(TotalOutsourcedServices_Planned)
             FROM [dbo].[vPM_OutsourcedServices_Combined] o
             WHERE o.ContractID = p.ContractID), 0) AS FLOAT) AS TotalDifferenceExpectedExecuted
    FROM 
        [dbo].[vPm_Cost_Planned] p
    GROUP BY 
        p.ContractID
),
GrandTotalData AS (
    SELECT 
        mi.ContractID,
        CAST(ISNULL((SELECT SUM(Total)
                     FROM [dbo].[vPm_Cost_Planned]
                     WHERE ContractID = mi.ContractID), 0) AS FLOAT) +
        CAST(ISNULL((SELECT SUM(TotalMade)
                     FROM [dbo].[vPM_Mobilization_Combined]
                     WHERE ContractID = mi.ContractID), 0) AS FLOAT) +
        CAST(ISNULL((SELECT SUM(TotalCost)
                     FROM [dbo].[vPM_Acquisition_Combined]
                     WHERE ContractID = mi.ContractID), 0) AS FLOAT) +
        CAST(ISNULL((SELECT SUM(TotalOutsourcedServices_Made)
                     FROM [dbo].[vPM_OutsourcedServices_Combined]
                     WHERE ContractID = mi.ContractID), 0) AS FLOAT) AS GrandTotalSum
    FROM 
        [dbo].[MilestonesItem] mi
    GROUP BY 
        mi.ContractID
)



SELECT 
    cd.ContractID,
    c.ValidityStartDate,
    c.ValidityEndDate,
    CAST(SUM(cd.CostHoursPlanned) AS FLOAT) AS TotalCostHoursPlanned,
    CAST(gtd.GrandTotalSum AS FLOAT) AS TotalCostHoursExecuted, -- GrandTotalSum como TotalCostHoursExecuted
    CAST(tdd.TotalDifferenceExpectedExecuted AS FLOAT) AS TotalDifferenceExpectedExecuted,
    CAST(SUM(cd.DifferenceExpectedExecuted) - gtd.GrandTotalSum AS FLOAT) AS TotalCostDifference -- Ajuste na soma para evitar duplicidade
FROM 
    CostData cd
INNER JOIN 
    [dbo].[Contracts] c ON cd.ContractID = c.ID
INNER JOIN 
    TotalDifferenceData tdd ON cd.ContractID = tdd.ContractID
LEFT JOIN 
    GrandTotalData gtd ON cd.ContractID = gtd.ContractID
GROUP BY 
    cd.ContractID,
    c.ValidityStartDate,
    c.ValidityEndDate,
    gtd.GrandTotalSum,
    tdd.TotalDifferenceExpectedExecuted;
GO

