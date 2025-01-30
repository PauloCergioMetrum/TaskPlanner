USE [TaskPlanner]
GO

/****** Object:  Table [dbo].[ContractTechLeaders]    Script Date: 16/01/2025 10:47:21 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[ContractTechLeaders](
	[ContractID] [int] NOT NULL,
	[TechLeaderID] [int] NOT NULL,
	[ID] [int] IDENTITY(1,1) NOT NULL
) ON [PRIMARY]
GO

