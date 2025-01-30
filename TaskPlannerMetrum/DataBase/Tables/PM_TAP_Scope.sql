USE [TaskPlanner]
GO

/****** Object:  Table [dbo].[PM_TAP_Scope]    Script Date: 16/01/2025 11:15:32 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[PM_TAP_Scope](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[ContractID] [int] NOT NULL,
	[Goal] [nvarchar](max) NULL,
	[Scope] [nvarchar](max) NULL,
	[Deliveries] [nvarchar](max) NULL,
	[Premises] [nvarchar](max) NULL,
	[GeneralRisks] [nvarchar](max) NULL,
	[OutScope] [nvarchar](max) NULL,
 CONSTRAINT [PK_PM_TAP_Scope] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

