USE [TaskPlanner]
GO

/****** Object:  StoredProcedure [dbo].[GetActivityPlanDetailsByExecutorID]    Script Date: 16/01/2025 11:47:42 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[GetActivityPlanDetailsByExecutorID]
    @ExecutorID INT


AS
BEGIN

  -- Variável precisa de declaração explícita de tipo de dado
    DECLARE @executorTeamID INT;
    
    -- Inicializando a variável com o ID da equipe do executor
    SET @executorTeamID = (SELECT ID FROM dbo.Team WHERE UserID = @ExecutorID);
  SELECT 
    AP.ActivitiesScopeListID AS activitiesScopeListID,
	AC.Description AS description,
	AP.ExecutedManHour AS executedManHour,
    CL.Name as ClientName,
	US.full_name AS executorName, 
    US.id AS userID,
	COALESCE (AP.NotesFromExecutor, '') AS notesFromExecutor, 
    AP.NotesFromPlanner as notesFromPlanner,
	AP.TaskDescription as taskDescription, 
    AP.PlannedManHour as plannedManHour, 
    AP.PlannerTeamID as plannerTeamID, 
    C.id AS contractID ,
	C.InternalCode AS projectName,
    CAST(AP.ScheduledDate AS datetime2) AS scheduledDate, 
    AP.Status as status, 
    AP.ID as id,
	CASE 
        WHEN AP.Status = '5' THEN 'Não Iniciada' 
        WHEN AP.Status = '1' THEN 'Concluída' 
        WHEN AP.Status = '2' THEN 'Em progresso' 
        WHEN AP.Status = '3' THEN 'Bloqueada' 
        WHEN AP.Status = '4' THEN 'Cancelada' 
        ELSE 'Não iniciada' 
    END AS statusName,
	AP.IsRework, 
    AP.BusinessUnit,
	AP.ExecutorTeamID as executorTeamID,
	vAP.EquipamentID as equipamentID,
	vAP.EquipamentName as equipamentName
FROM dbo.ActivityPlan AP      
	 INNER JOIN dbo.ActivitiesScopeList AS AC ON AC.ID = AP.ActivitiesScopeListID
	 INNER JOIN dbo.Contracts AS C ON C.id = AP.ContractID
	 INNER JOIN dbo.Clients as CL ON CL.id = C.ClientID
	 INNER JOIN dbo.Team as T ON T.ID = AP.ExecutorTeamID
	 INNER JOIN dbo.users AS U ON U.id = C.inspectorID
	 INNER JOIN dbo.users as US ON US.id = @ExecutorID
	 INNER JOIN dbo.vActivePlans As vAP on vAP.ID = AP.ID
    WHERE
        AP.ExecutorTeamID = @executorTeamID ;
END
GO

