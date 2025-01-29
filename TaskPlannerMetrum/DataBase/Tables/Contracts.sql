USE [TaskPlanner]
GO

/****** Object:  Table [dbo].[Contracts]    Script Date: 16/01/2025 10:47:02 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Contracts](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[inspectorID] [int] NOT NULL,
	[TagID] [int] NOT NULL,
	[StartDate] [datetime2](7) NOT NULL,
	[InternalCode] [nvarchar](50) NULL,
	[ClientID] [int] NOT NULL,
	[Condition] [nvarchar](100) NULL,
	[PaymentMethod] [nvarchar](50) NULL,
	[VendorID] [int] NOT NULL,
	[EnableProject] [bit] NULL,
	[ClientOrder] [bigint] NOT NULL,
	[Observation] [nvarchar](max) NULL,
	[DateRetroactive] [datetime2](7) NULL,
	[PredictedSavings] [nvarchar](50) NULL,
	[PredictedMarkup] [nvarchar](50) NULL,
	[ValidityStartDate] [datetime2](7) NULL,
	[ValidityEndDate] [datetime2](7) NULL,
	[StatusID] [int] NULL,
	[PaymentCondition] [nvarchar](50) NULL,
	[WorkSpaceID] [int] NOT NULL,
	[DateFineshed] [datetime2](7) NULL,
 CONSTRAINT [PK_Contracts] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

ALTER TABLE [dbo].[Contracts] ADD  DEFAULT ((1)) FOR [WorkSpaceID]
GO

ALTER TABLE [dbo].[Contracts]  WITH CHECK ADD  CONSTRAINT [FK_Contracts_Clients] FOREIGN KEY([ClientID])
REFERENCES [dbo].[Clients] ([ID])
GO

ALTER TABLE [dbo].[Contracts] CHECK CONSTRAINT [FK_Contracts_Clients]
GO

ALTER TABLE [dbo].[Contracts]  WITH CHECK ADD  CONSTRAINT [FK_Contracts_users] FOREIGN KEY([inspectorID])
REFERENCES [dbo].[users] ([id])
GO

ALTER TABLE [dbo].[Contracts] CHECK CONSTRAINT [FK_Contracts_users]
GO

ALTER TABLE [dbo].[Contracts]  WITH CHECK ADD  CONSTRAINT [FK_Contracts_users2] FOREIGN KEY([VendorID])
REFERENCES [dbo].[users] ([id])
GO

ALTER TABLE [dbo].[Contracts] CHECK CONSTRAINT [FK_Contracts_users2]
GO

