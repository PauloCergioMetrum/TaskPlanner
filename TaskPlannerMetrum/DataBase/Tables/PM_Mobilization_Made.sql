USE [TaskPlanner]
GO

/****** Object:  Table [dbo].[PM_Mobilization_Made]    Script Date: 16/01/2025 11:01:07 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[PM_Mobilization_Made](
	[ID] [nvarchar](255) NOT NULL,
	[CountMobilization] [float] NOT NULL,
	[CountAccommodation] [float] NULL,
	[CountFood] [float] NOT NULL,
	[CountAirTransport] [float] NOT NULL,
	[CountGroundTransport] [float] NULL,
	[CountOthers] [float] NOT NULL,
	[MobilizationPlannedID] [nvarchar](255) NOT NULL,
	[DateStart] [datetime] NOT NULL,
	[DateEnd] [datetime] NOT NULL,
	[Description] [nvarchar](500) NULL,
	[TotalValue]  AS (((((isnull([CountMobilization],(0))+isnull([CountAccommodation],(0)))+isnull([CountFood],(0)))+isnull([CountAirTransport],(0)))+isnull([CountGroundTransport],(0)))+isnull([CountOthers],(0))) PERSISTED NOT NULL,
 CONSTRAINT [PK_PM_Mobilization_Made] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[PM_Mobilization_Made]  WITH NOCHECK ADD  CONSTRAINT [FK_PM_Mobilization_Made_PM_Mobilization_Planned] FOREIGN KEY([MobilizationPlannedID])
REFERENCES [dbo].[PM_Mobilization_Planned] ([ID])
GO

ALTER TABLE [dbo].[PM_Mobilization_Made] CHECK CONSTRAINT [FK_PM_Mobilization_Made_PM_Mobilization_Planned]
GO

