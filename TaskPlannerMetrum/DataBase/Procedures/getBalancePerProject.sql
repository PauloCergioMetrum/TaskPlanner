USE [TaskPlanner]
GO

/****** Object:  StoredProcedure [dbo].[getBalancePerProject]    Script Date: 16/01/2025 11:48:22 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[getBalancePerProject]
    @ContractIDs VARCHAR(MAX) = NULL,  -- String contendo IDs de contrato separados por vírgula
    @StartDate DATE = NULL,            -- Data de início do período
    @EndDate DATE = NULL               -- Data de término do período
AS
BEGIN
    SELECT 
        f.BusinessUnit,
        CONCAT(LEFT(DATENAME(month, c.StartDate), 3), '/', RIGHT(YEAR(c.StartDate), 2)) AS Period,  -- Formata para "jan/22" (exemplo: janeiro/22)
        COUNT(DISTINCT f.ContractID) AS 'AumontOpen',
        SUM(CASE WHEN c.[DateFineshed] IS NOT NULL THEN 1 ELSE 0 END) AS 'AumontClose'
    FROM 
        Finances f
    JOIN 
        Contracts c ON f.ContractID = c.ID
    WHERE 
        ((@ContractIDs IS NULL) OR (CHARINDEX(',' + CAST(f.ContractID AS VARCHAR) + ',', ',' + @ContractIDs + ',') > 0))
        AND ((@StartDate IS NULL AND @EndDate IS NULL) OR (c.StartDate BETWEEN @StartDate AND @EndDate))
    GROUP BY 
        f.BusinessUnit,
        DATENAME(month, c.StartDate),
        YEAR(c.StartDate);
END;
GO

