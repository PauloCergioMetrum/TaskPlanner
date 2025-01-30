USE [TaskPlanner]
GO

/****** Object:  Table [dbo].[PM_Functions_MilestoneType]    Script Date: 16/01/2025 10:59:42 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[PM_Functions_MilestoneType](
	[ID] [nvarchar](255) NOT NULL,
	[FunctionID] [int] NOT NULL,
	[MilesstoneTypeID] [nvarchar](100) NOT NULL,
 CONSTRAINT [PK_PM_Functions_MilestoneType] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[PM_Functions_MilestoneType]  WITH NOCHECK ADD FOREIGN KEY([FunctionID])
REFERENCES [dbo].[Functions] ([ID])
GO

ALTER TABLE [dbo].[PM_Functions_MilestoneType]  WITH NOCHECK ADD FOREIGN KEY([FunctionID])
REFERENCES [dbo].[Functions] ([ID])
GO

