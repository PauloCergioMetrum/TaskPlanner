USE [TaskPlanner]
GO

/****** Object:  View [dbo].[vContractProject]    Script Date: 16/01/2025 11:37:07 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



CREATE VIEW [dbo].[vContractProject]
AS

--select * from ActivityPlan

with activities_cte
AS(
SELECT   ContractID
		,SUM(PlannedManHour) as 'PlannedManHour' 
		,SUM(ExecutedManHour) as 'ExecutedManHour'
		,cast(Count(status) as float) as 'TotalActivities'
		,cast(COUNT(CASE WHEN status IN (1,6) THEN 1 END) as float)*100 'FinishedActivities'
FROM ActivityPlan
group by ContractID
),
delayedTasks_cte
AS(
SELECT [ContractID]
      ,COUNT(ID) as 'delayed'
  FROM [TaskPlanner].[dbo].[ActivityPlan]
  where [ScheduledDate]<cast(GETDATE() as date) and [Status] in(2,5)
  group by ContractID
),
expected_cte 
AS(
SELECT   contractID
		,SUM(ExpectedHour) as 'ExpectedHour'
FROM DepartmentProjects
GROUP BY contractID
)
--select * from expected_cte
--select *
--	,ROUND((FinishedActivities/TotalActivities),2) as 'Progress'
--	from activities
SELECT   
    cl.Name AS ClientName,
    c.id, 
    c.InternalCode,
    c.ClientID,
    u.full_name AS InspectorName,
    c.EnableProject,
    c.StartDate,
    DateRetroactive,
    CAST(COALESCE(a.PlannedManHour,0) AS NVARCHAR)  AS 'PlannedMenHour',
    CAST(COALESCE(a.ExecutedManHour,0) AS NVARCHAR)  AS 'ExecutedMenHour',
    CAST(ROUND(COALESCE(a.FinishedActivities,0) / COALESCE(a.TotalActivities,1),2) AS NVARCHAR) AS 'Progress',
    CAST(COALESCE(e.ExpectedHour,0) AS NVARCHAR)  AS 'ExpectedHour',
    COALESCE(delayed,0) AS 'Delayed',
    --'Em Andamento' AS Status
	iif((SELECT dbo.getStatus(c.id, 'Finances'))in(11,4),1,(SELECT dbo.getStatus(c.id, 'Finances'))) AS Status
FROM  
    Clients cl
    INNER JOIN Contracts c ON cl.ID = c.ClientID 
    INNER JOIN users u ON c.inspectorID = u.id 
    INNER JOIN dbo.StatusContract ON C.StatusID = dbo.StatusContract.ID
    LEFT JOIN activities_cte a ON a.ContractID = c.id 
    LEFT JOIN expected_cte e ON e.contractID = c.id 
    LEFT JOIN delayedTasks_cte d ON d.ContractID = c.id

GO

EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[54] 4[7] 2[20] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "Clients"
            Begin Extent = 
               Top = 7
               Left = 329
               Bottom = 170
               Right = 558
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "Contracts"
            Begin Extent = 
               Top = 7
               Left = 606
               Bottom = 170
               Right = 835
            End
            DisplayFlags = 280
            TopColumn = 8
         End
         Begin Table = "users"
            Begin Extent = 
               Top = 175
               Left = 608
               Bottom = 338
               Right = 873
            End
            DisplayFlags = 280
            TopColumn = 7
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 900
         Table = 1176
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1356
         SortOrder = 1416
         GroupBy = 1350
         Filter = 1356
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'vContractProject'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=1 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'vContractProject'
GO

