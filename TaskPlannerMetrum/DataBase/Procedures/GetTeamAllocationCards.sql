USE [TaskPlanner]
GO

/****** Object:  StoredProcedure [dbo].[GetTeamAllocationCards]    Script Date: 16/01/2025 12:52:58 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[GetTeamAllocationCards]
    @DateStart DATE = NULL,
    @DateEnd DATE = NULL
AS
BEGIN
    SELECT
        SUM(PlannedManHour) AS TotalPlannedHours,
        SUM(ExecutedManHour) AS TotalExecutedHours
    FROM
        ActivityPlan
    WHERE
        (@DateStart IS NULL OR ScheduledDate >= @DateStart) AND
        (@DateEnd IS NULL OR ScheduledDate <= @DateEnd);
END
GO

