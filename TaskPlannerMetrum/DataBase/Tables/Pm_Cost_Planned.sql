USE [TaskPlanner]
GO

/****** Object:  Table [dbo].[Pm_Cost_Planned]    Script Date: 16/01/2025 10:57:31 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Pm_Cost_Planned](
	[Id] [nvarchar](100) NOT NULL,
	[TypeID] [int] NOT NULL,
	[Amount] [int] NOT NULL,
	[ValueUnit] [float] NOT NULL,
	[Description] [nvarchar](255) NOT NULL,
	[ContractID] [int] NOT NULL,
	[TotalPlanned]  AS ([Amount]+[ValueUnit]),
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

