USE [TaskPlanner]
GO

/****** Object:  StoredProcedure [dbo].[GetNumberOfContractsForBusinessUnit]    Script Date: 16/01/2025 12:51:18 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE  PROCEDURE [dbo].[GetNumberOfContractsForBusinessUnit]
    @ContractIDs VARCHAR(MAX) = NULL,
    @BusinessUnitNames VARCHAR(MAX) = NULL,
    @StartPeriod VARCHAR(7) = NULL,  -- Formato 'YYYY/MM'
    @EndPeriod VARCHAR(7) = NULL     -- Formato 'YYYY/MM'
AS
BEGIN
    -- Validação dos parâmetros de período
    IF (@StartPeriod IS NOT NULL AND TRY_CONVERT(DATE, @StartPeriod + '/01') IS NULL)
    BEGIN
        RAISERROR('O valor de @StartPeriod não está em um formato válido YYYY/MM.', 16, 1);
        RETURN;
    END

    IF (@EndPeriod IS NOT NULL AND TRY_CONVERT(DATE, @EndPeriod + '/01') IS NULL)
    BEGIN
        RAISERROR('O valor de @EndPeriod não está em um formato válido YYYY/MM.', 16, 1);
        RETURN;
    END

    -- Construção das datas de início e fim
    DECLARE @StartDate DATE = NULL;
    DECLARE @EndDate DATE = NULL;

    IF @StartPeriod IS NOT NULL
        SET @StartDate = DATEFROMPARTS(LEFT(@StartPeriod, 4), RIGHT(@StartPeriod, 2), 1);

    IF @EndPeriod IS NOT NULL
        SET @EndDate = EOMONTH(DATEFROMPARTS(LEFT(@EndPeriod, 4), RIGHT(@EndPeriod, 2), 1));
    IF @StartDate IS NOT NULL AND @EndDate IS NOT NULL AND @StartDate > @EndDate
    BEGIN
        RAISERROR('O período inicial não pode ser maior que o período final.', 16, 1);
        RETURN;
    END

    SELECT 
        f.BusinessUnit AS label,
        COUNT(DISTINCT f.ContractID) AS value
    FROM
        [TaskPlanner].[dbo].[Finances] f
    JOIN 
        [TaskPlanner].[dbo].[Contracts] c ON f.ContractID = c.ID
    WHERE 
        (@ContractIDs IS NULL OR CHARINDEX(',' + CAST(f.ContractID AS VARCHAR) + ',', ',' + @ContractIDs + ',') > 0)
        AND (@BusinessUnitNames IS NULL OR CHARINDEX(',' + f.BusinessUnit + ',', ',' + @BusinessUnitNames + ',') > 0)
        AND (
            (@StartDate IS NULL AND @EndDate IS NULL)
            OR
            (
                (@StartDate IS NULL OR f.[BaseDate] <= @EndDate)  -- Alteração aqui
                AND (@EndDate IS NULL OR f.[EndDate] >= @StartDate)  -- E aqui
            )
        )
    GROUP BY 
        f.BusinessUnit;
END
GO

