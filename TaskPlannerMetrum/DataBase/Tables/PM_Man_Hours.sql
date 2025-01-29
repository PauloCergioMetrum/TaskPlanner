USE [TaskPlanner]
GO

/****** Object:  Table [dbo].[PM_Man_Hours]    Script Date: 16/01/2025 11:00:22 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[PM_Man_Hours](
	[ID] [nvarchar](255) NOT NULL,
	[Departmemt] [nvarchar](255) NOT NULL,
	[FunctionHH] [int] NOT NULL,
	[Type] [int] NOT NULL,
	[QuantityHH] [float] NOT NULL,
	[ContractID] [int] NOT NULL
) ON [PRIMARY]
GO

