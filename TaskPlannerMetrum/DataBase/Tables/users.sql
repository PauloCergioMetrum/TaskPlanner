USE [TaskPlanner]
GO

/****** Object:  Table [dbo].[users]    Script Date: 16/01/2025 11:31:48 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[users](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[user_name] [nvarchar](100) NOT NULL,
	[full_name] [nvarchar](100) NOT NULL,
	[user_email] [nvarchar](100) NULL,
	[phone_number] [nvarchar](100) NULL,
	[password] [nvarchar](100) NULL,
	[department_id] [int] NULL,
	[WorkspaceID] [int] NULL,
	[permission_id] [int] NULL,
	[refresh_token] [nvarchar](500) NULL,
	[refresh_token_expiry_time] [datetime2](7) NULL,
	[CreationDate] [datetime2](7) NULL,
	[IsDarkMode] [bit] NOT NULL,
	[IsActive] [bit] NULL,
	[FunctionID] [int] NULL,
	[ManagementID] [int] NULL,
	[Registration] [nvarchar](150) NULL,
	[FunctionName] [varchar](255) NULL,
 CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[users]  WITH CHECK ADD  CONSTRAINT [FK_Users_Department1] FOREIGN KEY([department_id])
REFERENCES [dbo].[Department] ([ID])
GO

ALTER TABLE [dbo].[users] CHECK CONSTRAINT [FK_Users_Department1]
GO

ALTER TABLE [dbo].[users]  WITH CHECK ADD  CONSTRAINT [FK_users_Permissions] FOREIGN KEY([permission_id])
REFERENCES [dbo].[Permissions] ([id])
GO

ALTER TABLE [dbo].[users] CHECK CONSTRAINT [FK_users_Permissions]
GO

ALTER TABLE [dbo].[users]  WITH CHECK ADD  CONSTRAINT [FK_Users_Workspace] FOREIGN KEY([WorkspaceID])
REFERENCES [dbo].[Workspace] ([ID])
GO

ALTER TABLE [dbo].[users] CHECK CONSTRAINT [FK_Users_Workspace]
GO

