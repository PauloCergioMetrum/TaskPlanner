USE [TaskPlanner]
GO

/****** Object:  Table [dbo].[PM_Acquisition_Made]    Script Date: 16/01/2025 10:55:37 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[PM_Acquisition_Made](
	[ID] [nvarchar](max) NOT NULL,
	[Amount] [float] NULL,
	[AquisitionPlannedID] [nvarchar](max) NOT NULL,
	[Value] [float] NULL,
	[DateAcquisition] [datetime] NOT NULL,
	[DateAcquisitionDelivery] [datetime] NOT NULL,
	[TotalCost] [float] NULL,
	[Description] [nvarchar](max) NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

ALTER TABLE [dbo].[PM_Acquisition_Made] ADD  DEFAULT ((0)) FOR [TotalCost]
GO

