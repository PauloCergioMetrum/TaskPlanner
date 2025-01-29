USE [TaskPlanner]
GO

/****** Object:  Table [dbo].[PM_MilestonesType]    Script Date: 16/01/2025 11:00:55 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[PM_MilestonesType](
	[ID] [nvarchar](200) NULL,
	[DepartmentID] [int] NOT NULL,
	[Hours] [float] NOT NULL,
	[MilestonesValueID] [nvarchar](200) NOT NULL,
	[DisplacementServicesID] [int] NOT NULL,
	[FunctionID] [int] NOT NULL,
	[ValueHour] [float] NULL,
	[TotalMilesStone]  AS ([Hours]*[ValueHour])
) ON [PRIMARY]
GO

