USE [TaskPlanner]
GO

/****** Object:  View [dbo].[vActivePlansByExecutor]    Script Date: 16/01/2025 11:35:58 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO




CREATE VIEW [dbo].[vActivePlansByExecutor]
AS
SELECT 
    dbo.ActivityPlan.ActivitiesScopeListID as activitiesScopeListID, 
    dbo.ActivitiesScopeList.Description as description, 
    dbo.ActivityPlan.ExecutedManHour as executedManHour,
	(select Name from Clients where ID = dbo.Contracts.id ) as ClientName,
    users1.full_name AS executorName, 
    users1.id AS userID, 
    COALESCE (dbo.ActivityPlan.NotesFromExecutor, '') AS notesFromExecutor, 
    dbo.ActivityPlan.NotesFromPlanner as notesFromPlanner, 
    dbo.ActivityPlan.TaskDescription as taskDescription, 
    dbo.ActivityPlan.PlannedManHour as plannedManHour, 
    dbo.ActivityPlan.PlannerTeamID as plannerTeamID, 
    dbo.Contracts.id AS contractID ,
	dbo.Contracts.InternalCode AS projectName,
    CAST(dbo.ActivityPlan.ScheduledDate AS datetime2) AS scheduledDate, 
    dbo.ActivityPlan.Status as status, 
    dbo.ActivityPlan.ID as id,	
    CASE 
        WHEN dbo.ActivityPlan.Status = '5' THEN 'Não Iniciada' 
        WHEN dbo.ActivityPlan.Status = '1' THEN 'Concluída' 
        WHEN dbo.ActivityPlan.Status = '2' THEN 'Em progresso' 
        WHEN dbo.ActivityPlan.Status = '3' THEN 'Bloqueada' 
        WHEN dbo.ActivityPlan.Status = '4' THEN 'Cancelada' 
        ELSE 'Não iniciada' 
    END AS statusName,     
    dbo.ActivityPlan.IsRework, 
    dbo.ActivityPlan.BusinessUnit
FROM     
    dbo.ActivitiesScopeList 
    INNER JOIN dbo.ActivityPlan ON dbo.ActivitiesScopeList.ID = dbo.ActivityPlan.ActivitiesScopeListID 
    INNER JOIN dbo.Contracts ON dbo.ActivityPlan.ContractID = dbo.Contracts.id 
    INNER JOIN dbo.Team ON dbo.ActivityPlan.ExecutorTeamID = dbo.Team.ID 
    INNER JOIN dbo.users ON dbo.users.id = dbo.Contracts.inspectorID 
    INNER JOIN dbo.users AS users1 ON dbo.Team.UserID = users1.id
GO

