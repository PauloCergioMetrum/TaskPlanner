USE [TaskPlanner-Clone-PRD]
GO

/****** Object:  View [dbo].[vRightCardValue]    Script Date: 27/02/2025 09:18:29 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



CREATE VIEW [dbo].[vRightCardValue] AS
SELECT 
    f.[ContractID],
    CAST(SUM(ISNULL(f.[Value], 0)) AS DECIMAL(18, 2)) AS TotalValue,
    CAST(SUM(ISNULL(f.[InvoicedValue], 0)) AS DECIMAL(18, 2)) AS TotalInvoicedValue,
    CAST(ISNULL(omi.[TotalCostDifference], 0) AS DECIMAL(18, 2)) AS TotalCostDifference,
    CAST(ISNULL(omi.[TotalCostHoursExecuted], 0) AS DECIMAL(18, 2)) AS TotalCostHoursExecuted,



        (
           CAST(SUM(ISNULL(f.[InvoicedValue], 0)) AS DECIMAL(18, 2))
			 - CAST(ISNULL(omi.[TotalCostHoursExecuted], 0) AS DECIMAL(18, 2))
        ) AS TotalGrossResult, 


  (
        (
           CAST(SUM(ISNULL(f.[InvoicedValue], 0)) AS DECIMAL(18, 2))
			 - CAST(ISNULL(omi.[TotalCostHoursExecuted], 0) AS DECIMAL(18, 2))
        ) 
        / NULLIF(CAST(SUM(ISNULL(f.[InvoicedValue], 0)) AS DECIMAL(18, 2)), 0)
    ) AS TotalCostHoursExecutedPercentage,



  CAST(
        (

		CAST(SUM(omi.[TotalCostHoursExecuted]) AS DECIMAL(18,6))
           - CAST(SUM(f.[InvoicedValue]) AS DECIMAL(18,6)) 
            
        )
        AS VARCHAR(50)  
    ) AS PredictedMarkup


FROM [TaskPlanner-Clone-PRD].[dbo].[Finances] AS f
LEFT JOIN [TaskPlanner-Clone-PRD].[dbo].[OrderManagementInfo] AS omi
    ON f.[ContractID] = omi.[ContractID]
LEFT JOIN [TaskPlanner-Clone-PRD].[dbo].[Contracts] AS c
    ON f.[ContractID] = c.[id]
GROUP BY 
    f.[ContractID], 
    omi.[TotalCostDifference], 
    omi.[TotalCostHoursExecuted], 
    c.[PredictedMarkup];
GO

