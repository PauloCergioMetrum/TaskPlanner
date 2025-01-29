USE [TaskPlanner]
GO

/****** Object:  Table [dbo].[PM_Scope_Traking]    Script Date: 16/01/2025 11:02:25 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[PM_Scope_Traking](
	[ID] [nvarchar](100) NOT NULL,
	[ContractID] [int] NOT NULL,
	[DateField] [datetime] NULL,
	[TextField] [nvarchar](max) NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

