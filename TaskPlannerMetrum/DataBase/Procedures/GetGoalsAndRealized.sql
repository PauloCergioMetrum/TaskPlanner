USE [TaskPlanner]
GO

/****** Object:  StoredProcedure [dbo].[GetGoalsAndRealized]    Script Date: 16/01/2025 12:49:39 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[GetGoalsAndRealized]
    @year NVARCHAR(4),
    @startDate DATE = NULL,
    @endDate DATE = NULL
AS
BEGIN
    -- Meta para o ano especificado
    DECLARE @target DECIMAL(18, 2);
    SELECT @target = SUM(CAST(Value AS DECIMAL(18, 2)))
    FROM [dbo].[Goals]
    WHERE [Year] = @year;

    -- Valor realizado no período especificado
    DECLARE @realized DECIMAL(18, 2);
    SELECT @realized = SUM(CAST(InvoicedValue AS DECIMAL(18, 2)))
    FROM [dbo].[Finances]
    WHERE (InvoicedDate BETWEEN @startDate AND @endDate);

    -- Valor previsto com base no período passado
    DECLARE @predicted DECIMAL(18, 2);
    SELECT @predicted = SUM(CAST(Value AS DECIMAL(18, 2)))
    FROM [dbo].[Finances]
    WHERE (BaseDate BETWEEN @startDate AND @endDate);

    -- Retornar os resultados
    SELECT 
        ISNULL(@target, 0) AS [Target],       -- Meta
        ISNULL(@realized, 0) AS [Realized],   -- Realizado
        ISNULL(@predicted, 0) AS [Predicted]  -- Previsto
    ;
END;
GO

