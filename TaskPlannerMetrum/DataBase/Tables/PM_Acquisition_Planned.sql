USE [TaskPlanner]
GO

/****** Object:  Table [dbo].[PM_Acquisition_Planned]    Script Date: 16/01/2025 10:56:10 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[PM_Acquisition_Planned](
	[ID] [nvarchar](50) NOT NULL,
	[TypeAcquisitionID] [int] NOT NULL,
	[AmountPlanned] [int] NULL,
	[Description] [nvarchar](max) NULL,
	[ContractID] [int] NOT NULL,
	[AmountValue] [float] NULL,
	[TotalCost] [float] NOT NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

ALTER TABLE [dbo].[PM_Acquisition_Planned] ADD  CONSTRAINT [DF_TotalCost]  DEFAULT ((0)) FOR [TotalCost]
GO

