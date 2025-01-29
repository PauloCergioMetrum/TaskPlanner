USE [TaskPlanner]
GO

/****** Object:  View [dbo].[vMileStonesValue]    Script Date: 16/01/2025 11:38:34 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



CREATE VIEW [dbo].[vMileStonesValue]
AS
SELECT
    mi.ContractID,
    mi.Name AS MilestonesName,
    mv.MilestonesID,
    mv.ScheduledDate,
    mv.RescheduledDate,
    CASE
        WHEN mv.TypeID = 2 THEN mv.ExecutedDate
        ELSE (
            SELECT TOP 1 ap.ScheduledDate
            FROM ActivityPlan ap
            WHERE ap.ContractID = mi.ContractID
              AND ap.Status = 1
              AND ap.MilestonesID = mv.MilestonesID
            ORDER BY ap.ScheduledDate DESC
        )
    END AS ExecutedDate,
    mv.Description,
    mv.TypeID AS MilestonesTypeID,
    mv.Baseline,
    mv.ID,
    mv.Value,
    mv.TechLeadID,
    CASE
        WHEN mv.TechLeadID = 0 THEN NULL
        ELSE u.full_name
    END AS TechLeaderName,
    b.Name AS BusinessName,
    mv.TypeMilestonesID,
    mv.BusinessUnitID,
    f.BusinessUnit AS FinanceBusinessUnit -- Retorna o BusinessUnit relacionado
FROM
    dbo.MilestonesValue mv
INNER JOIN dbo.MilestonesItem mi ON mv.MilestonesID = mi.ID
LEFT JOIN dbo.Team t ON mv.TechLeadID = t.ID
LEFT JOIN dbo.users u ON t.UserID = u.ID
LEFT JOIN dbo.BusinessUnit b ON b.ID = mv.BusinessUnitID
LEFT JOIN dbo.Finances f ON mv.BusinessUnitID = f.ID -- Relaciona pelo ID de Finances com o BusinessUnitID
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
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "MilestonesValue"
            Begin Extent = 
               Top = 7
               Left = 48
               Bottom = 392
               Right = 277
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "MilestonesItem"
            Begin Extent = 
               Top = 7
               Left = 325
               Bottom = 368
               Right = 554
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
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'vMileStonesValue'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=1 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'vMileStonesValue'
GO

