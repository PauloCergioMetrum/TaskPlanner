USE [TaskPlanner]
GO

/****** Object:  Table [dbo].[ActivityPlan]    Script Date: 11/02/2025 13:53:27 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[ActivityPlan](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[WorkspaceID] [int] NOT NULL,
	[ContractID] [int] NOT NULL,
	[ActivitiesScopeListID] [int] NOT NULL,
	[ScheduledDate] [date] NOT NULL,
	[PlannedManHour] [float] NOT NULL,
	[PlannerTeamID] [int] NOT NULL,
	[NotesFromPlanner] [nvarchar](500) NOT NULL,
	[ExecutorTeamID] [int] NOT NULL,
	[Status] [nvarchar](50) NOT NULL,
	[ExecutedManHour] [float] NOT NULL,
	[NotesFromExecutor] [nvarchar](500) NULL,
	[ExecutedDate] [datetime2](7) NULL,
	[IsRework] [bit] NULL,
	[DepartamentID] [int] NOT NULL,
	[TaskDescription] [nvarchar](max) NULL,
	[BusinessUnit] [nvarchar](500) NULL,
	[EquipmentID] [int] NULL,
	[MilestonesID] [int] NULL,
	[EndofActivities] [datetime] NULL,
 CONSTRAINT [PK_ActivityPlan] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

ALTER TABLE [dbo].[ActivityPlan] ADD  DEFAULT ((1)) FOR [DepartamentID]
GO

ALTER TABLE [dbo].[ActivityPlan]  WITH NOCHECK ADD  CONSTRAINT [FK_ActivityPlan_ActivitiesScopeList] FOREIGN KEY([ActivitiesScopeListID])
REFERENCES [dbo].[ActivitiesScopeList] ([ID])
GO

ALTER TABLE [dbo].[ActivityPlan] CHECK CONSTRAINT [FK_ActivityPlan_ActivitiesScopeList]
GO

ALTER TABLE [dbo].[ActivityPlan]  WITH NOCHECK ADD  CONSTRAINT [FK_ActivityPlan_Contracts] FOREIGN KEY([ContractID])
REFERENCES [dbo].[Contracts] ([id])
GO

ALTER TABLE [dbo].[ActivityPlan] CHECK CONSTRAINT [FK_ActivityPlan_Contracts]
GO

ALTER TABLE [dbo].[ActivityPlan]  WITH NOCHECK ADD  CONSTRAINT [FK_ActivityPlan_Equipment_ActivityPlan] FOREIGN KEY([EquipmentID])
REFERENCES [dbo].[Equipment] ([ID])
GO

ALTER TABLE [dbo].[ActivityPlan] CHECK CONSTRAINT [FK_ActivityPlan_Equipment_ActivityPlan]
GO

ALTER TABLE [dbo].[ActivityPlan]  WITH NOCHECK ADD  CONSTRAINT [FK_ActivityPlan_Team] FOREIGN KEY([PlannerTeamID])
REFERENCES [dbo].[Team] ([ID])
GO

ALTER TABLE [dbo].[ActivityPlan] CHECK CONSTRAINT [FK_ActivityPlan_Team]
GO

ALTER TABLE [dbo].[ActivityPlan]  WITH NOCHECK ADD  CONSTRAINT [FK_ActivityPlan_Team1] FOREIGN KEY([ExecutorTeamID])
REFERENCES [dbo].[Team] ([ID])
GO

ALTER TABLE [dbo].[ActivityPlan] CHECK CONSTRAINT [FK_ActivityPlan_Team1]
GO

ALTER TABLE [dbo].[ActivityPlan]  WITH NOCHECK ADD  CONSTRAINT [FK_ActivityPlan_Workspace] FOREIGN KEY([WorkspaceID])
REFERENCES [dbo].[Workspace] ([ID])
GO

ALTER TABLE [dbo].[ActivityPlan] CHECK CONSTRAINT [FK_ActivityPlan_Workspace]
GO

