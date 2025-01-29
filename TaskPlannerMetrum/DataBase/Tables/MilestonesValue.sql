USE [TaskPlanner]
GO

/****** Object:  Table [dbo].[MilestonesValue]    Script Date: 16/01/2025 10:54:55 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[MilestonesValue](
	[ID] [nvarchar](max) NOT NULL,
	[Baseline] [int] NULL,
	[MilestonesID] [int] NOT NULL,
	[ScheduledDate] [datetime] NULL,
	[RescheduledDate] [datetime] NULL,
	[ExecutedDate] [datetime] NULL,
	[Description] [nvarchar](max) NULL,
	[TypeID] [int] NULL,
	[Value] [float] NULL,
	[TypeMilestonesID] [nvarchar](200) NULL,
	[TechLeadID] [int] NULL,
	[BusinessUnitID] [int] NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

ALTER TABLE [dbo].[MilestonesValue]  WITH NOCHECK ADD  CONSTRAINT [FK_MilestonesValue_MilestonesItem] FOREIGN KEY([MilestonesID])
REFERENCES [dbo].[MilestonesItem] ([ID])
GO

ALTER TABLE [dbo].[MilestonesValue] CHECK CONSTRAINT [FK_MilestonesValue_MilestonesItem]
GO

