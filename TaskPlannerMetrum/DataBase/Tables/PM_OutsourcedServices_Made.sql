USE [TaskPlanner]
GO

/****** Object:  Table [dbo].[PM_OutsourcedServices_Made]    Script Date: 16/01/2025 11:01:41 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[PM_OutsourcedServices_Made](
	[ID] [nvarchar](255) NOT NULL,
	[ID_OutsourcedServices_Planned] [nvarchar](255) NOT NULL,
	[Amount] [int] NOT NULL,
	[UnitaryValue] [float] NOT NULL,
	[NumberOOC] [float] NULL,
	[NumberContract] [int] NOT NULL,
	[DateStart] [date] NULL,
	[DateEnd] [date] NOT NULL
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[PM_OutsourcedServices_Made]  WITH NOCHECK ADD  CONSTRAINT [FK_PM_OutsourcedServices_Made_PM_OutsourcedServices_Planned] FOREIGN KEY([ID_OutsourcedServices_Planned])
REFERENCES [dbo].[PM_OutsourcedServices_Planned] ([ID])
GO

ALTER TABLE [dbo].[PM_OutsourcedServices_Made] CHECK CONSTRAINT [FK_PM_OutsourcedServices_Made_PM_OutsourcedServices_Planned]
GO

