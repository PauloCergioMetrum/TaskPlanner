USE [TaskPlanner]
GO

/****** Object:  StoredProcedure [dbo].[HoursCost]    Script Date: 16/01/2025 12:55:07 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO




CREATE PROCEDURE [dbo].[HoursCost] (
    @startDate DATETIME2,
    @endDate DATETIME2,
    @contractID INT,
    @userID INT
)
as
BEGIN
    -- Corpo da stored procedure
    SELECT
        c.id as 'ContractID',
        ap.ID as 'ActivityID',
        u.id as 'UserID',
        ap.ScheduledDate,
        uhc.HourCost * ap.ExecutedManHour as 'DayCost'
    FROM
        [TaskPlanner].[dbo].[Contracts] c
        INNER JOIN ActivityPlan ap ON c.id = ap.ContractID
        INNER JOIN Team t ON t.ID = ap.ExecutorTeamID
        INNER JOIN users u ON u.id = t.UserID
        INNER JOIN UserHourCosts uhc ON u.id = uhc.UserID AND ap.ScheduledDate > uhc.StartDate AND ap.ScheduledDate < uhc.EndDate
		Where  ScheduledDate >= @startDate and  ScheduledDate <=@endDate
    
END 
GO

