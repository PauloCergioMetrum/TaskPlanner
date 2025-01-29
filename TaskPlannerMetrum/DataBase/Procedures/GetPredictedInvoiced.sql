USE [TaskPlanner]
GO

/****** Object:  StoredProcedure [dbo].[GetPredictedInvoiced]    Script Date: 16/01/2025 12:51:35 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[GetPredictedInvoiced]
    @startDate DATE = NULL,
    @endDate DATE = NULL,
    @BusinessUnits NVARCHAR(MAX) = NULL, -- Lista de BusinessUnits separadas por vírgula
    @InspectorIDs NVARCHAR(MAX) = NULL  -- Lista de InspectorIDs separadas por vírgula
AS
BEGIN
    -- CTE para somar os valores baseados em InvoicedDate
    ;WITH InvoicedDateCTE AS (
        SELECT 
            FORMAT(InvoicedDate, 'MMM/yy') AS [MonthYear],
            SUM(ISNULL(Value, 0)) AS [ExpectedInvoiceCount],
            SUM(ISNULL(InvoicedValue, 0)) AS [InvoicedCount]
        FROM [dbo].[Finances]
        WHERE 
            (@startDate IS NULL OR InvoicedDate >= @startDate)
            AND (@endDate IS NULL OR InvoicedDate <= @endDate)
            AND (@BusinessUnits IS NULL OR BusinessUnit IN (SELECT value FROM STRING_SPLIT(@BusinessUnits, ',')))
            AND (@InspectorIDs IS NULL OR ContractID IN (
                SELECT c.id
                FROM [dbo].[Contracts] c
                WHERE c.inspectorID IN (SELECT value FROM STRING_SPLIT(@InspectorIDs, ','))
            ))
        GROUP BY 
            FORMAT(InvoicedDate, 'MMM/yy')
    )
    -- Seleciona os resultados da CTE
    SELECT
        i.[MonthYear],
        ISNULL(i.[InvoicedCount], 0) AS [InvoicedCount],
        ISNULL(i.[ExpectedInvoiceCount], 0) AS [ExpectedInvoiceCount]
    FROM 
        InvoicedDateCTE i
    ORDER BY 
        [MonthYear];
END;
GO

