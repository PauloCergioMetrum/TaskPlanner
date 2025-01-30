USE [TaskPlanner]
GO

/****** Object:  View [dbo].[vPlannedHours]    Script Date: 16/01/2025 11:38:47 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO





CREATE VIEW [dbo].[vPlannedHours]
AS
WITH hoursPlanned_cte
AS(
  SELECT  
		  c.InternalCode 
		 ,c.clientid
		 ,c.StatusID
		 ,d.contractID
		 ,DepartmentID
		 ,d.TechLeaderID
		 ,SUM(expectedhour) AS 'ExpectedHour'
		 ,COALESCE(SUM(executedmanhour),0) AS 'ExecutedHour'
		 ,COALESCE(SUM(Plannedmanhour),0) AS 'PlannedHour'
  FROM DepartmentProjects d
  INNER JOIN Contracts c ON c.id = d.contractID
  left JOIN [ActivityPlan] a ON a.ContractID = d.contractID and a.DepartamentID = d.DepartmentID
  GROUP BY c.InternalCode,c.clientid,d.contractID,DepartmentID,d.TechLeaderID,C.StatusID
  )


  SELECT ROW_NUMBER() over (order by h.contractid) as 'ID'
		 ,h.contractID as 'ContractID'
		 ,h.InternalCode as 'ProjectName'
		 ,h.StatusID
		 ,c.Name AS 'ClientName'
		 ,d.Name AS 'DepartmentName' 
		 ,h.TechLeaderID
		 ,COALESCE((select u.user_name from users u where u.id = h.TechLeaderID ),'Não cadastrado')  as 'TechLeader'
		 ,ExpectedHour
		 ,PlannedHour
		 ,ExecutedHour 
  FROM hoursPlanned_cte h
   INNER JOIN Clients c ON c.ID = h.ClientID
   INNER JOIN Department d ON h.DepartmentID = d.ID


GO

