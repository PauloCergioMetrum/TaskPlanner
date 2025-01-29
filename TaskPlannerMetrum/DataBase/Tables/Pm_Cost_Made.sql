USE [TaskPlanner]
GO

/****** Object:  Table [dbo].[Pm_Cost_Made]    Script Date: 16/01/2025 10:57:19 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Pm_Cost_Made](
	[ID] [nvarchar](100) NOT NULL,
	[Amount] [int] NOT NULL,
	[ValueUnit] [float] NOT NULL,
	[Description] [nvarchar](100) NOT NULL,
	[Pm_Cost_PlannedID] [nvarchar](100) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[Pm_Cost_Made]  WITH NOCHECK ADD FOREIGN KEY([Pm_Cost_PlannedID])
REFERENCES [dbo].[Pm_Cost_Planned1] ([ID])
GO

