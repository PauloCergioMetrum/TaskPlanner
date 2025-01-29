USE [TaskPlanner]
GO

/****** Object:  View [dbo].[vAcquisitionChart]    Script Date: 16/01/2025 11:35:13 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [dbo].[vAcquisitionChart] AS
SELECT TOP (1000) 
    [ID],
    [TypeAcquisitionID],
    CASE 
        WHEN [TypeAcquisitionID] = 1 THEN 'FRETE'
        WHEN [TypeAcquisitionID] = 2 THEN 'REVENDA'
        WHEN [TypeAcquisitionID] = 3 THEN 'USO E CONSUMO'
        WHEN [TypeAcquisitionID] = 4 THEN 'PRODUTO ACABADO'
        ELSE 'OUTRO'
    END AS TypeAcquisitionDescription,
    [PredictedTotal],
    [TotalCost],
    [CombinedTotal],
    [ContractID]
FROM 
    [dbo].[vPM_Acquisition_Combined];
GO

