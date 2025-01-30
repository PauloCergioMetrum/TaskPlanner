USE [TaskPlanner]
GO

/****** Object:  StoredProcedure [dbo].[CreateActivityPlan]    Script Date: 16/01/2025 11:46:44 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[CreateActivityPlan]
    @WorkspaceID INT,
    @ContractID INT,
    @ActivitiesScopeListID INT,
    @ScheduledDate DATETIME,
    @PlannedManHour DECIMAL(10, 2),
    @PlannerTeamID INT,
    @NotesFromPlanner NVARCHAR(MAX),
    @ExecutorTeamID INT,
    @Status NVARCHAR(50),
    @ExecutedManHour DECIMAL(10, 2),
    @NotesFromExecutor NVARCHAR(MAX),
    @ExecutedDate DATETIME,
    @IsRework BIT,
    @DepartamentID INT,
    @TaskDescription NVARCHAR(MAX),
    @BusinessUnit NVARCHAR(50),
    @EquipmentID INT,
    @MilestonesID INT
AS
BEGIN
    SET NOCOUNT ON;
	Declare @PlannerTeam_ID INT = (SELECT ID FROM dbo.Team WHERE UserID = @PlannerTeamID) 
	Declare @ExecutorTeam_ID INT = (SELECT ID FROM dbo.Team WHERE UserID = @ExecutorTeamID)
    INSERT INTO [TaskPlanner].[dbo].[ActivityPlan]
        ([WorkspaceID],
        [ContractID],
        [ActivitiesScopeListID],
        [ScheduledDate],
        [PlannedManHour],
        [PlannerTeamID],
        [NotesFromPlanner],
        [ExecutorTeamID],
        [Status],
        [ExecutedManHour],
        [NotesFromExecutor],
        [ExecutedDate],
        [IsRework],
        [DepartamentID],
        [TaskDescription],
        [BusinessUnit],
        [EquipmentID],
        [MilestonesID])
    VALUES
        (@WorkspaceID,
        @ContractID,
        @ActivitiesScopeListID,
        @ScheduledDate,
        @PlannedManHour,
        @PlannerTeam_ID,
        @NotesFromPlanner,
        @ExecutorTeamID,
        @Status,
        @ExecutedManHour,
        @NotesFromExecutor,
        @ExecutedDate,
        @IsRework,
        @DepartamentID,
        @TaskDescription,
        @BusinessUnit,
        @EquipmentID,
        @MilestonesID);
END;
GO

