USE [TaskPlanner]
GO

/****** Object:  View [dbo].[vBusinessUnit]    Script Date: 16/01/2025 11:36:10 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE VIEW [dbo].[vBusinessUnit] AS
SELECT DISTINCT ISNULL([BusinessUnit], '-') AS [BusinessUnit]
FROM [TaskPlanner].[dbo].[Finances]
WHERE [BusinessUnit] IS NOT NULL;
GO

