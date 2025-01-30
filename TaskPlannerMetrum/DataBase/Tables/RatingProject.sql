USE [TaskPlanner]
GO

/****** Object:  Table [dbo].[RatingProject]    Script Date: 16/01/2025 11:21:57 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[RatingProject](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[ProjectID] [int] NOT NULL,
	[UserID] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[RatingProject]  WITH CHECK ADD  CONSTRAINT [FK_RatingProject_Contracts] FOREIGN KEY([ProjectID])
REFERENCES [dbo].[Contracts] ([id])
GO

ALTER TABLE [dbo].[RatingProject] CHECK CONSTRAINT [FK_RatingProject_Contracts]
GO

