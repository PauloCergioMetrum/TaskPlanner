USE [TaskPlanner]
GO

/****** Object:  View [dbo].[vActivePlans]    Script Date: 29/01/2025 11:17:45 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [dbo].[vActivePlans]
AS
SELECT dbo.ActivityPlan.ActivitiesScopeListID, dbo.ActivitiesScopeList.Description, dbo.ActivityPlan.ExecutedManHour, users1.full_name AS ExecutorName, users1.id AS ExecutorTeamID, COALESCE (dbo.ActivityPlan.NotesFromExecutor, '') 
                  AS NotesFromExecutor, dbo.ActivityPlan.NotesFromPlanner, dbo.ActivityPlan.TaskDescription, dbo.ActivityPlan.PlannedManHour, dbo.ActivityPlan.PlannerTeamID, dbo.Contracts.id AS ContractID, 
                  CAST(dbo.ActivityPlan.ScheduledDate AS datetime2) AS ScheduledDate, dbo.ActivityPlan.Status, dbo.ActivityPlan.ID,
                      (SELECT COUNT(*) AS Expr1
                       FROM      dbo.ActivityPlan AS ap
                       WHERE   (Status = 5) AND (ScheduledDate < CAST(GETDATE() AS DATE)) AND (ContractID = dbo.Contracts.id) AND (dbo.ActivityPlan.MilestonesID = 0) AND (MilestonesID = 0) OR
                                         (Status = 5) AND (ScheduledDate < CAST(GETDATE() AS DATE)) AND (ContractID = dbo.Contracts.id) AND (MilestonesID IS NULL) AND (dbo.ActivityPlan.MilestonesID IS NULL) OR
                                         (Status = 5) AND (ScheduledDate < CAST(GETDATE() AS DATE)) AND (ContractID = dbo.Contracts.id) AND (dbo.ActivityPlan.MilestonesID = MilestonesID) AND (dbo.ActivityPlan.MilestonesID <> 0)) AS LateTaskCount, 
                  CASE WHEN dbo.ActivityPlan.Status = '5' THEN 'Não Iniciada' WHEN dbo.ActivityPlan.Status = '1' THEN 'Concluída' WHEN dbo.ActivityPlan.Status = '2' THEN 'Em progresso' WHEN dbo.ActivityPlan.Status = '3' THEN 'Bloqueada' WHEN dbo.ActivityPlan.Status
                   = '4' THEN 'Cancelada' ELSE 'Não iniciada' END AS statusName, dbo.Contracts.InternalCode AS ProjectName, dbo.ActivityPlan.IsRework, dbo.ActivityPlan.MilestonesID, COALESCE
                      ((SELECT Name
                        FROM      dbo.Equipment
                        WHERE   (ID = dbo.ActivityPlan.EquipmentID)), ' ') AS EquipamentName, dbo.ActivityPlan.EquipmentID AS EquipamentID, MD.Name AS MilestoneName, dbo.ActivityPlan.BusinessUnit, dbo.ActivityPlan.EndofActivities
FROM     dbo.ActivitiesScopeList INNER JOIN
                  dbo.ActivityPlan ON dbo.ActivitiesScopeList.ID = dbo.ActivityPlan.ActivitiesScopeListID INNER JOIN
                  dbo.Contracts ON dbo.ActivityPlan.ContractID = dbo.Contracts.id INNER JOIN
                  dbo.Team ON dbo.ActivityPlan.ExecutorTeamID = dbo.Team.ID INNER JOIN
                  dbo.users ON dbo.users.id = dbo.Contracts.inspectorID INNER JOIN
                  dbo.users AS users1 ON dbo.Team.UserID = users1.id LEFT OUTER JOIN
                  dbo.MilestonesItem AS MD ON MD.ID = dbo.ActivityPlan.MilestonesID
GO

EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[65] 4[2] 2[13] 3) )"
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
               Top = 175
               Left = 48
               Bottom = 511
               Right = 281
            End
            DisplayFlags = 280
            TopColumn = 9
         End
         Begin Table = "Contracts"
            Begin Extent = 
               Top = 343
               Left = 48
               Bottom = 506
               Right = 277
            End
            DisplayFlags = 280
            TopColumn = 17
         End
         Begin Table = "Team"
            Begin Extent = 
               Top = 511
               Left = 48
               Bottom = 674
               Right = 277
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "users"
            Begin Extent = 
               Top = 679
               Left = 48
               Bottom = 842
               Right = 313
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "users1"
            Begin Extent = 
               Top = 847
               Left = 48
               Bottom = 1010
               Right = 313
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "MD"
            Begin Extent = 
               Top = 1015
               Left = 48
               Bottom = 1134
               Right = 277
            End
            DisplayFla' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'vActivePlans'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane2', @value=N'gs = 280
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
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'vActivePlans'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=2 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'vActivePlans'
GO

