USE [TaskPlanner]
GO

/****** Object:  StoredProcedure [dbo].[GetBalanceOfProjects]    Script Date: 16/01/2025 11:48:10 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[GetBalanceOfProjects]
AS
BEGIN
    SELECT 
    FORMAT(StartDate, 'MMM/yy') AS StartDate ,
    COUNT(*) AS AmountStart,
    10 AS AmountEnd


FROM 
    [TaskPlanner].[dbo].[Contracts]
GROUP BY 
    FORMAT(StartDate, 'MMM/yy')
ORDER BY 
    StartDate
END
GO

