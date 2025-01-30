USE [TaskPlanner]
GO

/****** Object:  UserDefinedFunction [dbo].[GetCostMilestoneByPlannedHours]    Script Date: 16/01/2025 12:58:40 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE FUNCTION [dbo].[GetCostMilestoneByPlannedHours]
(
    @ContractID INT,
    @MilestoneID INT
)
RETURNS DECIMAL(10, 2)
AS
BEGIN
    DECLARE @TotalCost DECIMAL(10, 2);

    -- Calcular o custo total baseado nos dados das tabelas ActivityPlan e UserHourCosts
    SELECT @TotalCost = SUM(temp.PlannedManHour * uhc.HourCost)
    FROM 
        (SELECT
            ExecutorTeamID,
            PlannedManHour,
            ScheduledDate    
        FROM [TaskPlanner].[dbo].[ActivityPlan]
        WHERE ContractID = @ContractID
          AND MilestonesID = @MilestoneID) AS temp
    INNER JOIN 
        [TaskPlanner].[dbo].[UserHourCosts] uhc ON uhc.UserID = temp.ExecutorTeamID
                                              AND temp.ScheduledDate BETWEEN uhc.StartDate AND uhc.EndDate;

    RETURN @TotalCost;
END
GO

