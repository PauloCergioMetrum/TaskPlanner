USE [TaskPlanner]
GO

/****** Object:  View [dbo].[SalesOrderManagementCardValues]    Script Date: 16/01/2025 11:35:00 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE VIEW [dbo].[SalesOrderManagementCardValues] AS
SELECT 
    [ContractID],
    MAX([ContractName]) AS ContractName,  -- Assume que ContractName é único por ContractID
    MAX([Value]) AS Value,                -- Assume que Value é único por ContractID
    SUM([InvoicedValue]) AS InvoicedValue, -- Soma os valores de InvoicedValue
    MAX([BusinessUnit]) AS BusinessUnit   -- Assume que BusinessUnit é único por ContractID
FROM 
    [TaskPlanner].[dbo].[vFinanceContract]
WHERE 
    ContractID = '100'
GROUP BY 
    [ContractID]
GO

