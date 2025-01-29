USE [TaskPlanner]
GO

/****** Object:  Table [dbo].[WorkDays]    Script Date: 16/01/2025 11:32:19 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[WorkDays](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Day] [date] NOT NULL,
	[IsHolidayOrWeekend] [binary](1) NOT NULL,
	[SpecialPermissionNeed] [binary](1) NOT NULL,
	[WorkspaceID] [int] NOT NULL,
 CONSTRAINT [PK_WorkDays] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[WorkDays]  WITH CHECK ADD  CONSTRAINT [FK_WorkDays_Workspace] FOREIGN KEY([WorkspaceID])
REFERENCES [dbo].[Workspace] ([ID])
GO

ALTER TABLE [dbo].[WorkDays] CHECK CONSTRAINT [FK_WorkDays_Workspace]
GO

