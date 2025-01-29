USE [TaskPlanner]
GO

/****** Object:  Table [dbo].[StatusContract]    Script Date: 16/01/2025 11:30:34 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[StatusContract](
	[ID] [int] NOT NULL,
	[StatusDescription] [nvarchar](50) NOT NULL,
	[Priority] [int] NULL,
	[Category] [nvarchar](20) NULL,
	[Color] [nvarchar](7) NULL,
 CONSTRAINT [PK_[StatusContract] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

