USE [TaskPlanner]
GO

/****** Object:  Table [dbo].[UserTask]    Script Date: 16/01/2025 11:31:59 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[UserTask](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[ActivitiesScopeListID] [int] NOT NULL,
	[UserID] [int] NOT NULL,
	[ContractID] [int] NOT NULL,
	[ActivityPlanID] [int] NOT NULL,
	[Rating] [float] NOT NULL,
	[Support] [bit] NULL,
 CONSTRAINT [PK_UserTask] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[UserTask]  WITH CHECK ADD  CONSTRAINT [FK_UserTask_ActivitiesScopeList] FOREIGN KEY([ActivitiesScopeListID])
REFERENCES [dbo].[ActivitiesScopeList] ([ID])
GO

ALTER TABLE [dbo].[UserTask] CHECK CONSTRAINT [FK_UserTask_ActivitiesScopeList]
GO

ALTER TABLE [dbo].[UserTask]  WITH CHECK ADD  CONSTRAINT [FK_UserTask_Contracts] FOREIGN KEY([ContractID])
REFERENCES [dbo].[Contracts] ([id])
GO

ALTER TABLE [dbo].[UserTask] CHECK CONSTRAINT [FK_UserTask_Contracts]
GO

ALTER TABLE [dbo].[UserTask]  WITH CHECK ADD  CONSTRAINT [FK_UserTask_users] FOREIGN KEY([UserID])
REFERENCES [dbo].[users] ([id])
GO

ALTER TABLE [dbo].[UserTask] CHECK CONSTRAINT [FK_UserTask_users]
GO

