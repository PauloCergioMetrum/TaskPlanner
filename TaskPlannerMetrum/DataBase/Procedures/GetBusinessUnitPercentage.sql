USE [TaskPlanner]
GO

/****** Object:  StoredProcedure [dbo].[GetBusinessUnitPercentage]    Script Date: 16/01/2025 11:48:46 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE   PROCEDURE [dbo].[GetBusinessUnitPercentage]
    @BusinessUnits NVARCHAR(MAX) = NULL -- Add parameter to accept multiple BusinessUnit values
AS
BEGIN
    DECLARE @TotalCount INT;

    -- Calculate the total count considering the filter if provided
    SELECT @TotalCount = COUNT(*) 
    FROM [dbo].[Finances]
    WHERE (@BusinessUnits IS NULL OR [BusinessUnit] IN (SELECT value FROM STRING_SPLIT(@BusinessUnits, ',')));

    -- Select the BusinessUnits, count, and percentage with filtering
    SELECT 
        [BusinessUnit],
        COUNT(*) AS [UnitCount],
        CAST(ROUND((COUNT(*) * 100.0 / @TotalCount), 2) AS DECIMAL(5, 2)) AS [Percentage]
    FROM 
        [dbo].[Finances]
    WHERE (@BusinessUnits IS NULL OR [BusinessUnit] IN (SELECT value FROM STRING_SPLIT(@BusinessUnits, ',')))
    GROUP BY 
        [BusinessUnit]
    ORDER BY 
        [Percentage] DESC;
END
GO

