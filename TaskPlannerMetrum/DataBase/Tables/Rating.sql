USE [TaskPlanner]
GO

/****** Object:  Table [dbo].[Rating]    Script Date: 16/01/2025 11:21:20 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Rating](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[RatingProjectID] [int] NOT NULL,
	[RatingDescriptionID] [int] NOT NULL,
	[Value] [int] NOT NULL,
 CONSTRAINT [PK_Rating] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[Rating]  WITH CHECK ADD  CONSTRAINT [FK_Rating_RatingDescription] FOREIGN KEY([RatingDescriptionID])
REFERENCES [dbo].[RatingDescription] ([ID])
GO

ALTER TABLE [dbo].[Rating] CHECK CONSTRAINT [FK_Rating_RatingDescription]
GO

ALTER TABLE [dbo].[Rating]  WITH CHECK ADD  CONSTRAINT [FK_Rating_RatingProject] FOREIGN KEY([RatingProjectID])
REFERENCES [dbo].[RatingProject] ([ID])
GO

ALTER TABLE [dbo].[Rating] CHECK CONSTRAINT [FK_Rating_RatingProject]
GO

