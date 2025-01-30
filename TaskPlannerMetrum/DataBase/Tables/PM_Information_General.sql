USE [TaskPlanner]
GO

/****** Object:  Table [dbo].[PM_Information_General]    Script Date: 16/01/2025 11:00:06 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[PM_Information_General](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[ContractID] [int] NOT NULL,
	[Type] [int] NOT NULL,
	[Name] [nvarchar](100) NOT NULL,
	[Role] [nvarchar](100) NOT NULL,
	[Email] [nvarchar](100) NOT NULL,
	[PhoneNumber] [nvarchar](100) NOT NULL
) ON [PRIMARY]
GO

