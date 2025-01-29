USE [TaskPlanner]
GO

/****** Object:  Table [dbo].[PM_Mobilization_Planned]    Script Date: 16/01/2025 11:01:28 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[PM_Mobilization_Planned](
	[ID] [nvarchar](255) NOT NULL,
	[CountMobilization] [float] NOT NULL,
	[CountAccommodation] [float] NULL,
	[CountFood] [float] NOT NULL,
	[CountAirTransport] [float] NOT NULL,
	[CountGroundTransport] [float] NULL,
	[CountOthers] [float] NOT NULL,
	[ContractID] [int] NOT NULL,
 CONSTRAINT [PK_NewPM_Mobilization_Planned] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

