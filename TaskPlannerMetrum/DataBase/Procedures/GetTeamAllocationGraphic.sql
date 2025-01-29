USE [TaskPlanner]
GO

/****** Object:  StoredProcedure [dbo].[GetTeamAllocationGraphic]    Script Date: 16/01/2025 12:53:39 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[GetTeamAllocationGraphic]
    @StartDate DATE = NULL, 
    @EndDate DATE = NULL,
    @FunctionIDs NVARCHAR(MAX) = NULL -- Parâmetro para a lista de FunctionID
AS
BEGIN
    -- Separar a lista de IDs em uma tabela temporária
    DECLARE @FunctionIDTable TABLE (ID INT);
    IF @FunctionIDs IS NOT NULL
    BEGIN
        INSERT INTO @FunctionIDTable (ID)
        SELECT value FROM STRING_SPLIT(@FunctionIDs, ',');
    END

    SELECT
        PC.Name AS FunctionName,
        CASE 
            WHEN YEAR(AP.ScheduledDate) BETWEEN 1900 AND 2100 
            THEN FORMAT(AP.ScheduledDate, 'MMMM/yyyy') 
            ELSE 'Invalid Date'
        END AS Month,
        ISNULL(SUM(AP.PlannedManHour), 0) AS TotalPlannedHours, 
        ISNULL(SUM(AP.ExecutedManHour), 0) AS TotalExecutedHours,
        CASE 
            WHEN ISNULL(SUM(AP.ExecutedManHour), 0) - ISNULL(SUM(AP.PlannedManHour), 0) > 0 
            THEN (ISNULL(SUM(AP.ExecutedManHour), 0) - ISNULL(SUM(AP.PlannedManHour), 0))
                 * ISNULL(SUM(AP.PlannedManHour), 0)
                 * ISNULL(SUM(AP.ExecutedManHour), 0)
            ELSE 0
        END AS AvailableTime
    FROM
        [ActivityPlan] AP
    INNER JOIN
        [Functions] PC ON AP.ExecutorTeamID = PC.ID 
    WHERE
        (@StartDate IS NULL OR AP.ScheduledDate >= @StartDate) 
        AND (@EndDate IS NULL OR AP.ScheduledDate <= @EndDate)
        AND (NOT EXISTS(SELECT 1 FROM @FunctionIDTable) OR PC.ID IN (SELECT ID FROM @FunctionIDTable))
    GROUP BY
        PC.Name, 
        CASE 
            WHEN YEAR(AP.ScheduledDate) BETWEEN 1900 AND 2100 
            THEN FORMAT(AP.ScheduledDate, 'MMMM/yyyy') 
            ELSE 'Invalid Date'
        END
    ORDER BY
        PC.Name,
        Month;

END;
GO

