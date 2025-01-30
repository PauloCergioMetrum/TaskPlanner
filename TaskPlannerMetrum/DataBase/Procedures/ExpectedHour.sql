USE [TaskPlanner]
GO

/****** Object:  StoredProcedure [dbo].[ExpectedHour]    Script Date: 16/01/2025 11:47:17 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[ExpectedHour]
    @Start datetime2,
	@End datetime2,
	@ContractID int
AS
BEGIN

   IF @ContractID <> 0
   BEGIN
    SELECT SUM(ExpectedHour)FROM 
	[TaskPlanner].[dbo].[DepartmentProjects] iNNER JOIN   
	(SELECT DISTINCT([ContractID])
	FROM [TaskPlanner].[dbo].[vReports_PlannedExecuted] WHERE ScheduledDate >= @Start AND ScheduledDate <= @End  and ContractID = @ContractID) AS vPlanned ON [DepartmentProjects].contractID = vPlanned.ContractID
   END
   ELSE
    BEGIN
    SELECT SUM(ExpectedHour)FROM 
	[TaskPlanner].[dbo].[DepartmentProjects] iNNER JOIN   
	(SELECT DISTINCT([ContractID])
	FROM [TaskPlanner].[dbo].[vReports_PlannedExecuted] WHERE ScheduledDate >= @Start AND ScheduledDate <= @End ) AS vPlanned ON [DepartmentProjects].contractID = vPlanned.ContractID
   END
    
END
GO

