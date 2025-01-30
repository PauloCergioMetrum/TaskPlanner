USE [TaskPlanner]
GO

/****** Object:  Table [dbo].[PmCostMade]    Script Date: 16/01/2025 11:16:39 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[PmCostMade](
	[Id] [nvarchar](255) NOT NULL,
	[Amount] [int] NOT NULL,
	[ValueUnit] [float] NOT NULL,
	[Description] [nvarchar](255) NOT NULL,
	[Pm_Cost_Planned_Id] [nvarchar](100) NULL
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[PmCostMade]  WITH NOCHECK ADD  CONSTRAINT [FK_PmCostMade_Pm_Cost_Planned] FOREIGN KEY([Pm_Cost_Planned_Id])
REFERENCES [dbo].[Pm_Cost_Planned] ([Id])
GO

ALTER TABLE [dbo].[PmCostMade] CHECK CONSTRAINT [FK_PmCostMade_Pm_Cost_Planned]
GO

