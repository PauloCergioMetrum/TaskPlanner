USE [TaskPlanner]
GO

/****** Object:  StoredProcedure [dbo].[ActivityPlanHHTable]    Script Date: 16/01/2025 11:46:21 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO




CREATE PROCEDURE [dbo].[ActivityPlanHHTable]
    @ContractID INT
AS
BEGIN
    SELECT 
         ap.[ID], -- Adiciona a coluna IDUSE
         ap.[BusinessUnit], 
         mi.[Name] AS MilestoneName,
         u.[user_name] AS ExecutorUserName,  
         t.[SeniorityLevel] AS ExecutorSeniorityLevel,
         ap.[PlannedManHour],
         ap.[ExecutedManHour],
         (SELECT SUM(ap2.[ExecutedManHour]) 
          FROM [TaskPlanner].[dbo].[ActivityPlan] ap2 
          WHERE ap2.[ExecutorTeamID] = ap.[ExecutorTeamID]
          AND ap2.[ContractID] = @ContractID) AS TotalHours
    FROM [TaskPlanner].[dbo].[ActivityPlan] ap
    INNER JOIN [TaskPlanner].[dbo].[Team] t
        ON ap.[ExecutorTeamID] = t.[ID]  
    INNER JOIN [TaskPlanner].[dbo].[Users] u
        ON t.[UserID] = u.[ID] 
    INNER JOIN [TaskPlanner].[dbo].[MilestonesItem] mi
        ON ap.[MilestonesID] = mi.[ID]      
    WHERE ap.[ContractID] = @ContractID;
END;
GO

