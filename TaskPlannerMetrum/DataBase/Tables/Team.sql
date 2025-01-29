USE [TaskPlanner]
GO

/****** Object:  Table [dbo].[Team]    Script Date: 16/01/2025 11:30:45 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Team](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[DepartmentID] [int] NOT NULL,
	[SeniorityLevel] [nvarchar](20) NOT NULL,
	[WorkForceClass] [nvarchar](20) NOT NULL,
	[WorkForceType] [nvarchar](20) NOT NULL,
	[HoursAvailability] [decimal](18, 2) NOT NULL,
	[ManHourCost] [decimal](18, 2) NOT NULL,
	[UserID] [int] NOT NULL,
	[isLeader] [bit] NULL,
 CONSTRAINT [PK_Team] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[Team]  WITH CHECK ADD  CONSTRAINT [FK_Team_Department] FOREIGN KEY([DepartmentID])
REFERENCES [dbo].[Department] ([ID])
GO

ALTER TABLE [dbo].[Team] CHECK CONSTRAINT [FK_Team_Department]
GO

