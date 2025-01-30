USE [TaskPlanner]
GO

/****** Object:  View [dbo].[vIndirectcostChart]    Script Date: 16/01/2025 11:38:12 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [dbo].[vIndirectcostChart] AS
SELECT 
    planned.Id AS PlannedId,
    made.Id AS MadeId,
    planned.TypeID,
    CASE 
        WHEN planned.TypeID = 1 THEN 'CUSTO ADMINISTRATIVO'
        WHEN planned.TypeID = 2 THEN 'CREA'
        WHEN planned.TypeID = 3 THEN 'SESMT'
        ELSE 'Outro'
    END AS TypeDescription,
    planned.ContractID,
    planned.Total AS PlannedTotal,
    planned.TotalPlanned, 
    made.Total AS MadeTotal
FROM 
    dbo.vPm_Cost_Planned AS planned
LEFT JOIN 
    dbo.vPmCostMade AS made ON planned.Id = made.Pm_Cost_Planned_Id;
GO

