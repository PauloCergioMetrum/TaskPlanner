USE [TaskPlanner]
GO

/****** Object:  Table [dbo].[Projects]    Script Date: 16/01/2025 11:16:50 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Projects](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](100) NOT NULL,
	[InternalCode] [nvarchar](100) NOT NULL,
	[ClientID] [int] NOT NULL,
	[WorkspaceID] [int] NOT NULL,
	[PlannedManHour] [float] NULL,
	[ExecutedManHour] [float] NULL,
	[StartDate] [datetime2](7) NULL,
	[ContractEndDate] [datetime2](7) NULL,
	[PlannedEndDate] [datetime2](7) NULL,
	[EndDate] [datetime2](7) NULL,
	[PMTeamID] [int] NOT NULL,
	[ExpectedManHor] [float] NULL,
	[Status] [nvarchar](50) NULL,
	[TechLeaderID] [int] NULL,
	[DepartamentID] [int] NULL,
 CONSTRAINT [PK_Projects] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[Projects]  WITH CHECK ADD  CONSTRAINT [FK_Projects_Clients] FOREIGN KEY([ClientID])
REFERENCES [dbo].[Clients] ([ID])
GO

ALTER TABLE [dbo].[Projects] CHECK CONSTRAINT [FK_Projects_Clients]
GO

ALTER TABLE [dbo].[Projects]  WITH CHECK ADD  CONSTRAINT [FK_Projects_Team] FOREIGN KEY([PMTeamID])
REFERENCES [dbo].[Team] ([ID])
GO

ALTER TABLE [dbo].[Projects] CHECK CONSTRAINT [FK_Projects_Team]
GO

ALTER TABLE [dbo].[Projects]  WITH CHECK ADD  CONSTRAINT [FK_Projects_users] FOREIGN KEY([TechLeaderID])
REFERENCES [dbo].[users] ([id])
GO

ALTER TABLE [dbo].[Projects] CHECK CONSTRAINT [FK_Projects_users]
GO

ALTER TABLE [dbo].[Projects]  WITH CHECK ADD  CONSTRAINT [FK_Projects_Workspace] FOREIGN KEY([WorkspaceID])
REFERENCES [dbo].[Workspace] ([ID])
GO

ALTER TABLE [dbo].[Projects] CHECK CONSTRAINT [FK_Projects_Workspace]
GO

