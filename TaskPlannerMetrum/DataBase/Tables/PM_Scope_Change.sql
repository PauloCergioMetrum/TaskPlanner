USE [TaskPlanner]
GO

/****** Object:  Table [dbo].[PM_Scope_Change]    Script Date: 16/01/2025 11:02:11 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[PM_Scope_Change](
	[ID] [nvarchar](255) NULL,
	[ContractID] [int] NULL,
	[Description] [nvarchar](255) NULL,
	[Reason] [nvarchar](255) NULL,
	[Type] [int] NULL,
	[Value] [float] NULL,
	[Date] [datetime] NULL
) ON [PRIMARY]
GO

