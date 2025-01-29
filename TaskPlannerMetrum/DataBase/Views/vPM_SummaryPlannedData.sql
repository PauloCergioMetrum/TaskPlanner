USE [TaskPlanner]
GO

/****** Object:  View [dbo].[vPM_SummaryPlannedData]    Script Date: 16/01/2025 11:41:41 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [dbo].[vPM_SummaryPlannedData] AS
WITH CTE AS (
    SELECT 
        os.ContractID,
        ac.TypeAcquisitionID,
        ac.AmountPlanned,
        ac.Description AS AcquisitionDescription,
        ac.AmountValue,
        ac.TotalCost,
        m.CountMobilization,
        m.CountAccommodation,
        m.CountFood,
        m.CountAirTransport,
        m.CountGroundTransport,
        m.CountOthers,
        COALESCE(mob_sum.TotalMobilizationCount, 0) AS TotalMobilizationCount,
        COALESCE(acq_sum.AmountPlannedSum, 0) AS AmountPlannedSum,
        COALESCE(os_amount_sum.AmountSum, 0) AS AmountSum,
        CASE 
            WHEN ac.TypeAcquisitionID = 1 THEN 'FRETE'
            WHEN ac.TypeAcquisitionID = 2 THEN 'REVENDA'
            WHEN ac.TypeAcquisitionID = 3 THEN 'USO E CONSUMO'
            WHEN ac.TypeAcquisitionID = 4 THEN 'PRODUTO ACABADO'
            ELSE 'Tipo de Aquisição Desconhecido'
        END AS TypeAcquisitionName
    FROM TaskPlanner.dbo.PM_OutsourcedServices_Planned os
    LEFT JOIN TaskPlanner.dbo.PM_Acquisition_Planned ac ON os.ContractID = ac.ContractID
    LEFT JOIN TaskPlanner.dbo.PM_Mobilization_Planned m ON os.ContractID = m.ContractID
    LEFT JOIN (
        SELECT ContractID, SUM(CountMobilization) AS TotalMobilizationCount
        FROM TaskPlanner.dbo.PM_Mobilization_Planned
        GROUP BY ContractID
    ) mob_sum ON os.ContractID = mob_sum.ContractID
    LEFT JOIN (
        SELECT ContractID, SUM(AmountPlanned) AS AmountPlannedSum
        FROM TaskPlanner.dbo.PM_Acquisition_Planned
        GROUP BY ContractID
    ) acq_sum ON os.ContractID = acq_sum.ContractID
    LEFT JOIN (
        SELECT ContractID, SUM(Amount) AS AmountSum
        FROM TaskPlanner.dbo.PM_OutsourcedServices_Planned
        GROUP BY ContractID
    ) os_amount_sum ON os.ContractID = os_amount_sum.ContractID
)
SELECT 
    CAST(ROW_NUMBER() OVER (ORDER BY ContractID) AS INT) AS ID,
    ContractID, 
    TypeAcquisitionName,  
    MAX(AmountPlanned) AS AmountPlanned, 
    MAX(AcquisitionDescription) AS AcquisitionDescription,
    MAX(AmountValue) AS AmountValue,
    MAX(TotalCost) AS TotalCost,
    MAX(CountMobilization) AS CountMobilization,
    MAX(CountAccommodation) AS CountAccommodation,
    MAX(CountFood) AS CountFood,
    MAX(CountAirTransport) AS CountAirTransport,
    MAX(CountGroundTransport) AS CountGroundTransport,
    MAX(CountOthers) AS CountOthers,
    MAX(TotalMobilizationCount) AS TotalMobilizationCount,
    MAX(AmountPlannedSum) AS AmountPlannedSum,
    MAX(AmountSum) AS AmountSum
FROM CTE
GROUP BY ContractID, TypeAcquisitionName;
GO

