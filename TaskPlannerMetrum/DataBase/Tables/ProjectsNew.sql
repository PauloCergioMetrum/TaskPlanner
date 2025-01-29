USE [TaskPlanner]
GO

/****** Object:  Table [dbo].[ProjectsNew]    Script Date: 16/01/2025 11:21:09 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[ProjectsNew](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[PlannedManHour] [float] NULL,
	[ExecutedManHour] [float] NULL,
	[ExpectedManHor] [float] NULL,
	[DepartamentID] [int] NULL,
	[ContractID] [int] NOT NULL,
	[Status] [char](10) NULL,
	[PaymentCondition] [nvarchar](50) NULL,
 CONSTRAINT [PK_ProjectsNew] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[ProjectsNew]  WITH CHECK ADD  CONSTRAINT [FK_ProjectsNew_Contracts] FOREIGN KEY([ContractID])
REFERENCES [dbo].[Contracts] ([id])
GO

ALTER TABLE [dbo].[ProjectsNew] CHECK CONSTRAINT [FK_ProjectsNew_Contracts]
GO

