USE [TaskPlanner]
GO

/****** Object:  View [dbo].[vPM_OutsourcedServices_Combined]    Script Date: 16/01/2025 11:40:45 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE VIEW [dbo].[vPM_OutsourcedServices_Combined]
AS
SELECT 
    p.ID, 
    p.ValueUnit,
    p.Amount, 
    p.TypeID, 
    p.Description, 
    p.ContractID,
    p.Subcontracting,
    p.ValueUnit * p.Amount AS TotalOutsourcedServices_Planned,
    COALESCE(
        (SELECT SUM(TotalOutsourcedServices) 
         FROM dbo.vPM_OutsourcedServices_Made_Combined 
         WHERE ID_OutsourcedServices_Planned = p.ID), 
        0
    ) AS TotalOutsourcedServices_Made,
    CASE 
        WHEN COALESCE(
            (SELECT SUM(TotalOutsourcedServices) 
             FROM dbo.vPM_OutsourcedServices_Made_Combined 
             WHERE ID_OutsourcedServices_Planned = p.ID), 
            0
        ) = 0 THEN 0
        ELSE p.ValueUnit * p.Amount - COALESCE(
            (SELECT SUM(TotalOutsourcedServices) 
             FROM dbo.vPM_OutsourcedServices_Made_Combined 
             WHERE ID_OutsourcedServices_Planned = p.ID), 
            0
        )
    END AS Difference
FROM     
    dbo.PM_OutsourcedServices_Planned p
GO

