USE [TaskPlanner]
GO

/****** Object:  View [dbo].[vProjectList]    Script Date: 16/01/2025 11:42:06 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO





CREATE VIEW [dbo].[vProjectList]
AS
SELECT   dbo.Projects.ID AS ProjectID, dbo.Projects.Name AS ProjectName, dbo.Projects.ClientID, dbo.Clients.Name AS ClientName, dbo.Projects.PlannedManHour, dbo.Projects.StartDate, dbo.Projects.PlannedEndDate, dbo.Projects.EndDate, dbo.Projects.PMTeamID, vusers.userName AS full_name, dbo.Projects.ExecutedManHour, 
             dbo.Projects.ContractEndDate, dbo.Projects.ExpectedManHor, dbo.Projects.Status, dbo.Projects.TechLeaderID, vuser1.userName as TechLeaderName
FROM     dbo.Projects INNER JOIN
             dbo.Clients ON dbo.Clients.ID = dbo.Projects.ClientID INNER JOIN
             dbo.vUserList AS vusers ON vusers.PMTeamID = dbo.Projects.PMTeamID INNER JOIN 
			 dbo.vUserList AS vuser1 ON vuser1.UserID = dbo.Projects.TechLeaderID
			 
			
GO

EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[40] 4[20] 2[20] 3) )"
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
         Top = -1360
         Left = 0
      End
      Begin Tables = 
         Begin Table = "Projects"
            Begin Extent = 
               Top = 6
               Left = 261
               Bottom = 136
               Right = 448
            End
            DisplayFlags = 280
            TopColumn = 10
         End
         Begin Table = "Clients"
            Begin Extent = 
               Top = 6
               Left = 38
               Bottom = 136
               Right = 223
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "vusers"
            Begin Extent = 
               Top = 138
               Left = 38
               Bottom = 268
               Right = 208
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
         Alias = 1680
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'vProjectList'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=1 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'vProjectList'
GO

