USE [TaskPlanner]
GO

/****** Object:  Table [dbo].[PM_TAP_Resources]    Script Date: 16/01/2025 11:02:50 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[PM_TAP_Resources](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[ContractID] [int] NULL,
	[Mobilizations] [nvarchar](max) NULL,
	[MobilizationsValue] [nvarchar](max) NULL,
	[ExpectedEquipment] [nvarchar](max) NULL,
	[ExpectedEquipmentValue] [nvarchar](max) NULL,
	[Acquitions] [nvarchar](max) NULL,
	[AcquitionsValue] [nvarchar](max) NULL,
	[ThirdPartyServices] [nvarchar](max) NULL,
	[ThirdPartyServicesValue] [nvarchar](max) NULL,
 CONSTRAINT [PK_PM_TAP_Resources] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

