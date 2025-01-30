USE [TaskPlanner]
GO

/****** Object:  Table [dbo].[Log]    Script Date: 29/01/2025 11:21:30 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Log](
	[LogId] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [int] NOT NULL,
	[UserName] [nvarchar](100) NOT NULL,
	[Action] [nvarchar](200) NOT NULL,
	[Description] [nvarchar](max) NULL,
	[CreatedAt] [datetime] NULL,
	[ModifiedAt] [datetime] NULL,
	[Status] [nvarchar](50) NULL,
	[SeverityLevel] [nvarchar](20) NULL,
	[ErrorDetails] [nvarchar](max) NULL,
PRIMARY KEY CLUSTERED 
(
	[LogId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

ALTER TABLE [dbo].[Log] ADD  DEFAULT (getdate()) FOR [CreatedAt]
GO

