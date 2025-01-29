USE [TaskPlanner]
GO

/****** Object:  Table [dbo].[Finances]    Script Date: 16/01/2025 10:48:39 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Finances](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[Description] [nvarchar](1000) NULL,
	[Amount] [nvarchar](50) NULL,
	[Value] [float] NOT NULL,
	[InvoicedValue] [float] NOT NULL,
	[BaseDate] [datetime2](7) NOT NULL,
	[EndDate] [datetime2](7) NOT NULL,
	[ExpectedInvoiceDate] [nvarchar](100) NULL,
	[Status] [nvarchar](50) NULL,
	[InvoicedDate] [datetime2](7) NOT NULL,
	[Billing] [nvarchar](50) NULL,
	[DepartmentID] [int] NOT NULL,
	[FinanceType] [nvarchar](50) NULL,
	[ContractID] [int] NOT NULL,
	[WorkSpaceID] [int] NOT NULL,
	[invoice] [varchar](50) NULL,
	[paymentCondition] [nvarchar](50) NULL,
	[BusinessUnit] [nvarchar](50) NULL,
	[StatusDpv] [nvarchar](200) NULL,
	[Guarantee] [bit] NULL,
	[GuaranteePeriod] [int] NULL,
 CONSTRAINT [PK_Finances] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[Finances]  WITH CHECK ADD  CONSTRAINT [FK_Finances_Contracts] FOREIGN KEY([ContractID])
REFERENCES [dbo].[Contracts] ([id])
GO

ALTER TABLE [dbo].[Finances] CHECK CONSTRAINT [FK_Finances_Contracts]
GO

ALTER TABLE [dbo].[Finances]  WITH CHECK ADD  CONSTRAINT [FK_Finances_Department] FOREIGN KEY([DepartmentID])
REFERENCES [dbo].[Department] ([ID])
GO

ALTER TABLE [dbo].[Finances] CHECK CONSTRAINT [FK_Finances_Department]
GO

