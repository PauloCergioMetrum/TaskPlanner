USE [TaskPlanner]
GO

/****** Object:  StoredProcedure [dbo].[InsertActivityPlan]    Script Date: 16/01/2025 12:55:25 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[InsertActivityPlan]
    @ActivitiesScopeListID INT,
    @ExecutedManHour DECIMAL,
    @ExecutorTeamID INT,
    @NotesFromExecutor NVARCHAR(MAX),
    @NotesFromPlanner NVARCHAR(MAX),
    @PlannedManHour DECIMAL,
    @PlannerTeamID INT,
    @TaskDescription NVARCHAR(MAX),
    @ContractID INT,
    @ScheduledDate DATE,
    @Status NVARCHAR(50),
    @WorkspaceID INT,
    @IsRework BIT,
    @DepartamentID INT,
    @BusinessUnit NVARCHAR(50)
AS
BEGIN
    -- Calcula a soma total das horas planejadas para o executor na data especificada
    DECLARE @TotalPlannedManHourForExecutor DECIMAL;

    SELECT @TotalPlannedManHourForExecutor = ISNULL(SUM(PlannedManHour), 0)
    FROM [dbo].[ActivityPlan]
    WHERE ExecutorTeamID = @ExecutorTeamID AND ScheduledDate = @ScheduledDate;

    -- Insere a atividade planejada
    INSERT INTO [dbo].[ActivityPlan] (
        [ActivitiesScopeListID],
        [ExecutedManHour],
        [ExecutorTeamID],
        [NotesFromExecutor],
        [NotesFromPlanner],
        [PlannedManHour],
        [PlannerTeamID],
        [TaskDescription],
        [ContractID],
        [ScheduledDate],
        [Status],
        [WorkspaceID],
        [IsRework],
        [DepartamentID],
        [BusinessUnit]
    ) VALUES (
        @ActivitiesScopeListID,
        @ExecutedManHour,
        @ExecutorTeamID,
        @NotesFromExecutor,
        @NotesFromPlanner,
        @PlannedManHour,
        @PlannerTeamID,
        @TaskDescription,
        @ContractID,
        @ScheduledDate,
        @Status,
        @WorkspaceID,
        @IsRework,
        @DepartamentID,
        @BusinessUnit
    );

    -- Retorna a soma total das horas planejadas para o executor na data especificada
    SELECT @TotalPlannedManHourForExecutor AS TotalPlannedManHour;
END;
GO

