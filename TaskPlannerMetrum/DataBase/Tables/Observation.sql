USE [TaskPlanner]
GO

/****** Object:  Table [dbo].[Observation]    Script Date: 16/01/2025 10:55:10 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Observation](
	[ID] [nvarchar](max) NOT NULL,
	[ContractID] [int] NOT NULL,
	[Datails] [nvarchar](max) NOT NULL,
	[Date] [datetime2](7) NOT NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

ALTER TABLE [dbo].[Observation]  WITH CHECK ADD  CONSTRAINT [FK_Observation_Contracts] FOREIGN KEY([ContractID])
REFERENCES [dbo].[Contracts] ([id])
GO

ALTER TABLE [dbo].[Observation] CHECK CONSTRAINT [FK_Observation_Contracts]
GO

