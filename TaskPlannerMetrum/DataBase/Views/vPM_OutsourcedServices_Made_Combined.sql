USE [TaskPlanner]
GO

/****** Object:  View [dbo].[vPM_OutsourcedServices_Made_Combined]    Script Date: 16/01/2025 11:40:59 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



CREATE VIEW [dbo].[vPM_OutsourcedServices_Made_Combined]
AS
SELECT 
    [ID],
    [ID_OutsourcedServices_Planned],
    [Amount],
    [UnitaryValue],
    [NumberOOC],
    [NumberContract],
    [DateStart],
    [DateEnd],
	[Amount]* [UnitaryValue] AS [TotalOutsourcedServices]


FROM 
    [dbo].[PM_OutsourcedServices_Made];
GO

