USE [TaskPlanner]
GO

/****** Object:  Table [dbo].[Pm_Type_OutsourcedServices]    Script Date: 16/01/2025 11:16:27 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Pm_Type_OutsourcedServices](
	[ID] [int] NOT NULL,
	[Name] [nvarchar](255) NOT NULL,
	[Type] [int] NULL,
 CONSTRAINT [PK_Pm_Type_OutsourcedServices] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

