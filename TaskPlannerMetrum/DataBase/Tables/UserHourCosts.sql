USE [TaskPlanner]
GO

/****** Object:  Table [dbo].[UserHourCosts]    Script Date: 16/01/2025 11:31:34 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[UserHourCosts](
	[ID] [nvarchar](max) NOT NULL,
	[StartDate] [datetime2](7) NOT NULL,
	[EndDate] [datetime2](7) NOT NULL,
	[HourCost] [float] NOT NULL,
	[UserID] [int] NOT NULL,
	[FunctionName] [varchar](255) NULL,
	[CreationDate] [datetime2](7) NULL,
	[FunctionID] [int] NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

