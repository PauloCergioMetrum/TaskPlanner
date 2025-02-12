USE [TaskPlanner]
GO

/****** Object:  View [dbo].[VgetMilestoneType_HH_Details]    Script Date: 11/02/2025 13:48:32 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [dbo].[VgetMilestoneType_HH_Details]
AS
WITH UniquePlanner AS (
    SELECT 
        ID, 
        PlannerTeamID,
        ROW_NUMBER() OVER (PARTITION BY PlannerTeamID ORDER BY ScheduledDate DESC) AS rn
    FROM dbo.ActivityPlan
)
SELECT 
    AP.[ID],
    AP.[ContractID],
    AP.[ActivitiesScopeListID],
    AP.[ScheduledDate],
    AP.[PlannedManHour],
    AP.[PlannerTeamID],
    AP.[NotesFromPlanner],
    AP.[ExecutorTeamID],
    AP.[ExecutedManHour],
    AP.[DepartamentID],
    D.[Name] AS DepartmentName,  -- Nome do departamento
    AP.[BusinessUnit],
    AP.[MilestonesID],
    T.[UserID] AS TeamUserID,  -- UserID da tabela Team
    U.[user_name] AS UserName,  -- Nome do usuário da tabela Users
    uhc.HourCost,
    uhc.UserID AS UHC_UserID, -- UserID retornada da tabela UserHourCosts (deve ser igual ao da tabela Team)
    uhc.FunctionName,
    uhc.CreationDate AS UHC_CreationDate
FROM dbo.ActivityPlan AP
LEFT JOIN UniquePlanner UP 
    ON AP.ID = UP.ID AND UP.rn = 1 -- Garante que apenas um PlannerTeamID aparece
LEFT JOIN dbo.Team T
    ON AP.[ExecutorTeamID] = T.[ID]
LEFT JOIN (
    SELECT 
        UserID, 
        HourCost, 
        FunctionName, 
        CreationDate
    FROM (
        SELECT 
            UserID, 
            HourCost, 
            FunctionName, 
            CreationDate,
            ROW_NUMBER() OVER (PARTITION BY UserID ORDER BY CreationDate DESC) AS rn
        FROM dbo.UserHourCosts
    ) AS sub
    WHERE rn = 1
) AS uhc
    ON T.[UserID] = uhc.[UserID]
LEFT JOIN dbo.Department D
    ON AP.[DepartamentID] = D.[ID]  -- Associa com a tabela Department para obter o nome
LEFT JOIN dbo.Users U
    ON T.[UserID] = U.[id];  -- Associa o UserID da tabela Team com o id da tabela Users
GO

