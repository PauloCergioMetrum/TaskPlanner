USE [TaskPlanner]
GO

/****** Object:  Table [dbo].[Clients]    Script Date: 16/01/2025 10:46:46 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Clients](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](100) NOT NULL,
	[WorkspaceID] [int] NOT NULL,
	[Country] [nvarchar](100) NULL,
	[State] [nvarchar](100) NULL,
	[City] [nvarchar](100) NULL,
	[Address] [nvarchar](100) NULL,
	[PMContactName] [nvarchar](100) NULL,
	[PMPhoneNumber] [nvarchar](20) NULL,
	[Cnpj] [nvarchar](100) NULL,
 CONSTRAINT [PK_Clients] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[Clients]  WITH CHECK ADD  CONSTRAINT [FK_Clients_Workspace] FOREIGN KEY([WorkspaceID])
REFERENCES [dbo].[Workspace] ([ID])
GO

ALTER TABLE [dbo].[Clients] CHECK CONSTRAINT [FK_Clients_Workspace]
GO

