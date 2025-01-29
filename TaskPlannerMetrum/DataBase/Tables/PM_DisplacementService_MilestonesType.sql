USE [TaskPlanner]
GO

/****** Object:  Table [dbo].[PM_DisplacementService_MilestonesType]    Script Date: 16/01/2025 10:57:48 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[PM_DisplacementService_MilestonesType](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[DisplacementServiceID] [int] NOT NULL,
	[MilestonesTypeID] [nchar](200) NOT NULL,
 CONSTRAINT [PK_PM_DisplacementService_MilestonesType] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

