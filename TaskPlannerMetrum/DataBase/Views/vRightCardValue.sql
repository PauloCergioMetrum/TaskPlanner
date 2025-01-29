USE [TaskPlanner]
GO

/****** Object:  View [dbo].[vRightCardValue]    Script Date: 16/01/2025 11:42:52 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



CREATE VIEW [dbo].[vRightCardValue] AS
SELECT 
      f.[ContractID],
      CAST(SUM(ISNULL(f.[Value], 0)) AS FLOAT) AS TotalValue,
      CAST(SUM(ISNULL(f.[InvoicedValue], 0)) AS FLOAT) AS TotalInvoicedValue,
      CAST(ISNULL(omi.[TotalCostDifference], 0) AS FLOAT) AS TotalCostDifference,
      CAST(ISNULL(omi.[TotalCostHoursExecuted], 0) AS FLOAT) AS TotalCostHoursExecuted,
      ISNULL(c.[PredictedMarkup], 0) AS PredictedMarkup,
      CASE 
          WHEN SUM(ISNULL(f.[InvoicedValue], 0)) <> 0 
          THEN (ISNULL(omi.[TotalCostHoursExecuted], 0) / SUM(ISNULL(f.[InvoicedValue], 0)) * 100) 
          ELSE 0 
      END AS TotalCostHoursExecutedPercentage
FROM [TaskPlanner].[dbo].[Finances] AS f
JOIN [TaskPlanner].[dbo].[OrderManagementInfo] AS omi
      ON f.[ContractID] = omi.[ContractID]
JOIN [TaskPlanner].[dbo].[Contracts] AS c
      ON f.[ContractID] = c.[id]
GROUP BY f.[ContractID], omi.[TotalCostDifference], omi.[TotalCostHoursExecuted], c.[PredictedMarkup];
GO

