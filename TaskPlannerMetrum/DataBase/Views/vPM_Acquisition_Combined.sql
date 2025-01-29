USE [TaskPlanner]
GO

/****** Object:  View [dbo].[vPM_Acquisition_Combined]    Script Date: 16/01/2025 11:38:59 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE   VIEW [dbo].[vPM_Acquisition_Combined] AS
SELECT DISTINCT
    p.ID,
    p.TypeAcquisitionID,
    p.AmountPlanned,
    p.Description,
    p.ContractID,
    p.AmountValue,
    (p.AmountValue * p.AmountPlanned) - COALESCE(m.TotalMade, 0) AS Difference,
    p.AmountValue * p.AmountPlanned AS PredictedTotal,
    COALESCE(m.TotalMade, 0) AS TotalCost,
    (p.AmountValue * p.AmountPlanned) + COALESCE(m.TotalMade, 0) AS CombinedTotal
FROM dbo.PM_Acquisition_Planned p
LEFT JOIN (
    SELECT AquisitionPlannedID, SUM(Amount * [Value]) AS TotalMade
    FROM dbo.PM_Acquisition_Made
    GROUP BY AquisitionPlannedID
) m ON p.ID = m.AquisitionPlannedID;
GO

