USE [TaskPlanner]
GO

/****** Object:  StoredProcedure [dbo].[getStatusPerPeriod]    Script Date: 16/01/2025 12:52:44 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



CREATE PROCEDURE [dbo].[getStatusPerPeriod]
    @StartDate DATE = NULL,
    @EndDate DATE = NULL
AS
BEGIN
    SELECT 
        FORMAT(StartDate, 'MMM/yy') AS Period,
        COUNT(*) AS AumountStart,
        SUM(CASE WHEN DateFineshed IS NOT NULL THEN 1 ELSE 0 END) AS AumountEnd
    FROM 
        [TaskPlanner].[dbo].[Contracts]
    WHERE 
        (@StartDate IS NULL OR StartDate >= @StartDate)
        AND (@EndDate IS NULL OR StartDate <= @EndDate)
    GROUP BY 
        FORMAT(StartDate, 'MMM/yy')
    ORDER BY 
        Period;
END
GO

