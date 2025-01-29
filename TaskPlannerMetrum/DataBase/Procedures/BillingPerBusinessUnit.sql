USE [TaskPlanner]
GO

/****** Object:  StoredProcedure [dbo].[BillingPerBusinessUnit]    Script Date: 16/01/2025 11:46:33 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



CREATE PROCEDURE [dbo].[BillingPerBusinessUnit]
    @startDate DATE = NULL,
    @endDate DATE = NULL,
    @BusinessUnits NVARCHAR(MAX) = NULL,
    @InspectorIDs NVARCHAR(MAX) = NULL
AS
BEGIN
    -- CTE para calcular os valores faturados baseado em InvoicedDate
    ;WITH InvoicedDateCTE AS (
        SELECT
            FORMAT(InvoicedDate, 'MMM/yy') AS [MonthYear],
            BusinessUnit,
            SUM(InvoicedValue) AS [ValueInvoice] -- Valor de fato recebido
        FROM [dbo].[Finances]
        LEFT JOIN [dbo].[Contracts] ON [dbo].[Finances].ContractID = [dbo].[Contracts].ID
        WHERE
            (InvoicedDate BETWEEN ISNULL(@startDate, '1900-01-01') AND ISNULL(@endDate, '2100-12-31'))
            AND (@BusinessUnits IS NULL OR BusinessUnit IN (SELECT value FROM STRING_SPLIT(@BusinessUnits, ',')))
            AND (@InspectorIDs IS NULL OR [dbo].[Contracts].inspectorID IN (SELECT value FROM STRING_SPLIT(@InspectorIDs, ',')))
        GROUP BY
            FORMAT(InvoicedDate, 'MMM/yy'),
            BusinessUnit
    ),
    -- CTE para calcular os valores previstos baseado em InvoicedDate
    ExpectedInvoiceCTE AS (
        SELECT
            FORMAT(InvoicedDate, 'MMM/yy') AS [MonthYear],
            BusinessUnit,
            SUM(Value) AS [ValueExpectedInvoice] -- Valor previsto (futuro) baseado na data de faturamento
        FROM [dbo].[Finances]
        LEFT JOIN [dbo].[Contracts] ON [dbo].[Finances].ContractID = [dbo].[Contracts].ID
        WHERE
            (InvoicedDate BETWEEN ISNULL(@startDate, '1900-01-01') AND ISNULL(@endDate, '2100-12-31'))
            AND (@BusinessUnits IS NULL OR BusinessUnit IN (SELECT value FROM STRING_SPLIT(@BusinessUnits, ',')))
            AND (@InspectorIDs IS NULL OR [dbo].[Contracts].inspectorID IN (SELECT value FROM STRING_SPLIT(@InspectorIDs, ',')))
        GROUP BY
            FORMAT(InvoicedDate, 'MMM/yy'),
            BusinessUnit
    )
    -- Junta os resultados das CTEs
    SELECT
        COALESCE(i.[MonthYear], e.[MonthYear]) AS [MonthYear],
        COALESCE(i.[BusinessUnit], e.[BusinessUnit]) AS [BusinessUnit],
        ISNULL(e.[ValueExpectedInvoice], 0) AS [ValueExpectedInvoice],
        ISNULL(i.[ValueInvoice], 0) AS [ValueInvoice]
    FROM
        InvoicedDateCTE i
    FULL OUTER JOIN
        ExpectedInvoiceCTE e
    ON i.[MonthYear] = e.[MonthYear] AND i.[BusinessUnit] = e.[BusinessUnit]
    ORDER BY
        [MonthYear], [BusinessUnit];
END;
GO

