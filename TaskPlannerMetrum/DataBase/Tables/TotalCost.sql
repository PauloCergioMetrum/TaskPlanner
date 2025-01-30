USE [TaskPlanner]
GO

/****** Object:  Table [dbo].[TotalCost]    Script Date: 16/01/2025 11:31:11 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[TotalCost](
	[ID] [nvarchar](50) NOT NULL,
	[TypeAcquisitionID] [int] NOT NULL,
	[AmountPlanned] [int] NULL,
	[Description] [nvarchar](max) NULL,
	[ContractID] [int] NOT NULL,
	[AmountValue] [float] NULL,
	[Value] [float] NOT NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

