USE [TaskPlanner]
GO

/****** Object:  View [dbo].[vReports_PlannedExecuted]    Script Date: 16/01/2025 11:42:34 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO









CREATE VIEW [dbo].[vReports_PlannedExecuted]
AS
SELECT
    dbo.ActivityPlan.ActivitiesScopeListID,
    dbo.ActivitiesScopeList.Description,
    dbo.Contracts.inspectorID,
    dbo.Contracts.InternalCode,
    CAST(dbo.ActivityPlan.ScheduledDate AS datetime2) AS ScheduledDate,
    dbo.ActivityPlan.PlannedManHour,
    dbo.ActivityPlan.ExecutedManHour,
    dbo.users.full_name AS InspectorName,
    users1.full_name AS ExecutorName,
    users1.id AS ExecutorID,
    dbo.Contracts.id AS ContractID,
    Teams.ID AS TechLeaderID,
    userTeams.full_name AS TechLeaderName,
    dbo.ActivityPlan.ID AS ActivityPlanID,
    users1.IsActive AS IsActive_UserName,
    userTeams.IsActive AS IsActive_TechLeader,
    usersInspectors.IsActive AS IsActive_Inspector,
    ISNULL(dbo.ActivityPlan.TaskDescription, ' ') AS TaskDescription,
    UserHourCosts.StartDate,
    UserHourCosts.EndDate,  
    UserHourCosts.HourCost  
FROM
    dbo.ActivitiesScopeList
INNER JOIN
    dbo.ActivityPlan ON dbo.ActivitiesScopeList.ID = dbo.ActivityPlan.ActivitiesScopeListID
INNER JOIN
    dbo.Contracts ON dbo.ActivityPlan.ContractID = dbo.Contracts.id
INNER JOIN
    dbo.Team ON dbo.ActivityPlan.ExecutorTeamID = dbo.Team.ID
INNER JOIN
    dbo.users ON dbo.users.id = dbo.Contracts.inspectorID
INNER JOIN
    dbo.Team AS Teams ON Teams.ID = dbo.ActivityPlan.PlannerTeamID
INNER JOIN
    dbo.users AS userTeams ON Teams.UserID = userTeams.id
INNER JOIN
    dbo.users AS users1 ON dbo.Team.UserID = users1.id
INNER JOIN
    dbo.users AS usersInspectors ON dbo.Contracts.inspectorID = usersInspectors.id
LEFT JOIN
    dbo.UserHourCosts ON dbo.Team.UserID = dbo.UserHourCosts.UserID 
GO

EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[42] 4[3] 2[37] 3) )"
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
         Begin Table = "ActivitiesScopeList"
            Begin Extent = 
               Top = 7
               Left = 48
               Bottom = 170
               Right = 277
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "ActivityPlan"
            Begin Extent = 
               Top = 7
               Left = 325
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
            TopColumn = 0
         End
         Begin Table = "Team"
            Begin Extent = 
               Top = 175
               Left = 48
               Bottom = 338
               Right = 277
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "users"
            Begin Extent = 
               Top = 175
               Left = 325
               Bottom = 338
               Right = 590
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "DEP"
            Begin Extent = 
               Top = 175
               Left = 638
               Bottom = 338
               Right = 867
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "Teams"
            Begin Extent = 
               Top = 175
               Left = 915
               Bottom = 338
               Right = 1144
            End
            DisplayFlags' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'vReports_PlannedExecuted'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane2', @value=N' = 280
            TopColumn = 0
         End
         Begin Table = "userTeams"
            Begin Extent = 
               Top = 343
               Left = 48
               Bottom = 506
               Right = 313
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "users1"
            Begin Extent = 
               Top = 7
               Left = 883
               Bottom = 170
               Right = 1148
            End
            DisplayFlags = 280
            TopColumn = 0
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
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'vReports_PlannedExecuted'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=2 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'vReports_PlannedExecuted'
GO

