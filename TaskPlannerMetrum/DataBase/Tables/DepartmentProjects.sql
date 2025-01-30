USE [TaskPlanner]
GO

/****** Object:  Table [dbo].[DepartmentProjects]    Script Date: 16/01/2025 10:47:51 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[DepartmentProjects](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[DepartmentID] [int] NOT NULL,
	[contractID] [int] NULL,
	[TechLeaderID] [int] NULL,
	[ExpectedHour] [float] NULL,
	[FinancesID] [int] NOT NULL,
 CONSTRAINT [PK_DepartmentProjects] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[DepartmentProjects]  WITH CHECK ADD  CONSTRAINT [FK_Department_DepartmentProjects] FOREIGN KEY([DepartmentID])
REFERENCES [dbo].[Department] ([ID])
GO

ALTER TABLE [dbo].[DepartmentProjects] CHECK CONSTRAINT [FK_Department_DepartmentProjects]
GO

