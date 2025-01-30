USE [TaskPlanner]
GO

/****** Object:  Table [dbo].[ClientFeedback]    Script Date: 16/01/2025 10:46:23 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[ClientFeedback](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[ContractID] [int] NOT NULL,
	[ContactStartDate] [date] NULL,
	[ClientResponse] [nvarchar](max) NULL,
	[ClientRating] [decimal](3, 1) NULL,
	[ReceivedComplaint] [nvarchar](max) NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

ALTER TABLE [dbo].[ClientFeedback]  WITH CHECK ADD FOREIGN KEY([ContractID])
REFERENCES [dbo].[Contracts] ([id])
GO

ALTER TABLE [dbo].[ClientFeedback]  WITH CHECK ADD CHECK  (([ClientRating]>=(1.0) AND [ClientRating]<=(5.0)))
GO

