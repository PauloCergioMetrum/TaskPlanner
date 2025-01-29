USE [TaskPlanner]
GO

/****** Object:  StoredProcedure [dbo].[GetMaterialAndServiceCount]    Script Date: 16/01/2025 12:50:06 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[GetMaterialAndServiceCount]
    @startDate DATE,
    @endDate DATE,
    @BusinessUnits NVARCHAR(MAX) = NULL,
    @InspectorIDs NVARCHAR(MAX) = NULL
AS
BEGIN
    -- Cria uma CTE para somar os valores baseados na InvoicedDate
    ;WITH InvoicedDateCTE AS (
        SELECT
            FORMAT(InvoicedDate, 'MMM/yy') AS [MonthYear],
            SUM(CASE WHEN FinanceType = 'M' THEN Value ELSE 0 END) AS [ExpectedMaterialValue], -- Ajustado
            SUM(CASE WHEN FinanceType = 'S' THEN Value ELSE 0 END) AS [ExpectedServiceValue], -- Ajustado
            SUM(CASE WHEN FinanceType = 'M' THEN InvoicedValue ELSE 0 END) AS [InvoicedMaterialValue], -- Ajustado
            SUM(CASE WHEN FinanceType = 'S' THEN InvoicedValue ELSE 0 END) AS [InvoicedServiceValue] -- Ajustado
        FROM [dbo].[Finances]
        WHERE
            (InvoicedDate BETWEEN @startDate AND @endDate)
            AND (@BusinessUnits IS NULL OR BusinessUnit IN (SELECT value FROM STRING_SPLIT(@BusinessUnits, ',')))
            AND (@InspectorIDs IS NULL OR ContractID IN (SELECT value FROM STRING_SPLIT(@InspectorIDs, ',')))
        GROUP BY
            FORMAT(InvoicedDate, 'MMM/yy')
    )
    -- Seleciona os resultados da CTE
    SELECT
        [MonthYear],
        ISNULL([InvoicedMaterialValue], 0) AS [InvoicedMaterial],
        ISNULL([InvoicedServiceValue], 0) AS [InvoicedService],
        ISNULL([ExpectedMaterialValue], 0) AS [ExpectedMaterial],
        ISNULL([ExpectedServiceValue], 0) AS [ExpectedService]
    FROM
        InvoicedDateCTE
    ORDER BY
        [MonthYear];
END;
GO

