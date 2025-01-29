USE [TaskPlanner]
GO

/****** Object:  StoredProcedure [dbo].[GetPvSummary]    Script Date: 16/01/2025 12:52:07 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[GetPvSummary]
    @StartYearMonth DATETIME = NULL, 
    @EndYearMonth DATETIME = NULL     
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @StartDate DATETIME;
    DECLARE @EndDate DATETIME;
    
    IF @StartYearMonth IS NOT NULL
        SET @StartDate = DATEADD(MONTH, DATEDIFF(MONTH, 0, @StartYearMonth), 0); 
    
    IF @EndYearMonth IS NOT NULL
        SET @EndDate = EOMONTH(@EndYearMonth);

    PRINT 'StartDate: ' + CONVERT(VARCHAR, @StartDate, 121);
    PRINT 'EndDate: ' + CONVERT(VARCHAR, @EndDate, 121);

    SELECT 
        YEAR(f.BaseDate) AS Ano,
        DATENAME(MONTH, f.BaseDate) AS Mes,
        SUM(CASE WHEN c.StatusID = 11 THEN 1 ELSE 0 END) AS Abertos,
        SUM(CASE WHEN c.StatusID = 5 THEN 1 ELSE 0 END) AS Fechados
    FROM 
        [dbo].[Contracts] c
    INNER JOIN 
        [dbo].[Finances] f ON c.id = f.ContractID
    WHERE 
        (@StartDate IS NULL OR f.BaseDate >= @StartDate)
        AND (@EndDate IS NULL OR f.BaseDate <= @EndDate) 
    GROUP BY 
        YEAR(f.BaseDate), 
        DATENAME(MONTH, f.BaseDate)
    ORDER BY 
        Ano, 
        MIN(f.BaseDate);
END
GO

