USE [TaskPlanner]
GO

/****** Object:  Table [dbo].[PM_OutsourcedServices_Planned]    Script Date: 16/01/2025 11:01:55 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[PM_OutsourcedServices_Planned](
	[ID] [nvarchar](255) NOT NULL,
	[ValueUnit] [float] NOT NULL,
	[Amount] [int] NULL,
	[TypeID] [float] NOT NULL,
	[Description] [nvarchar](300) NULL,
	[ContractID] [int] NOT NULL,
	[Subcontracting] [bit] NULL,
 CONSTRAINT [PK_NewPM_OutsourcedServices_Planned] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

