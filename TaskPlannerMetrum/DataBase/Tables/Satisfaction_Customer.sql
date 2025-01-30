USE [TaskPlanner]
GO

/****** Object:  Table [dbo].[Satisfaction_Customer]    Script Date: 16/01/2025 11:29:59 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Satisfaction_Customer](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[ContractID] [int] NOT NULL,
	[ClientResponse] [nvarchar](max) NULL,
	[ClientRating] [int] NULL,
	[ReceivedComplaint] [nvarchar](255) NULL,
	[FeedbackDate] [datetime] NULL,
	[Status] [int] NULL,
 CONSTRAINT [PK_CustomerSatisfaction] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

