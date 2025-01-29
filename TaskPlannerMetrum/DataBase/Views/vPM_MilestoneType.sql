USE [TaskPlanner]
GO

/****** Object:  View [dbo].[vPM_MilestoneType]    Script Date: 16/01/2025 11:40:05 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE VIEW [dbo].[vPM_MilestoneType]
AS
SELECT 
    DEP.Name AS DepartmentName,
    DisService.Name AS DisplacementServiceName,
    dbo.PM_MilestonesType.DepartmentID,
    dbo.PM_MilestonesType.MilestonesValueID,
    dbo.PM_MilestonesType.DisplacementServicesID,
    dbo.PM_MilestonesType.Hours,
    dbo.PM_MilestonesType.ID,
    Functions.ID AS FunctionID,
    Functions.Name AS FunctionName,
    dbo.PM_MilestonesType.ValueHour,
    dbo.PM_MilestonesType.TotalMilesStone,
    MilestonesValue.MilestonesID, -- Adicionando o campo MilestonesID

    -- Subconsulta para retornar a coluna TotalRecordsByContract da view vActivePlans
    (SELECT COUNT(*)
     FROM dbo.vActivePlans AP
     WHERE AP.MilestonesID = MilestonesValue.MilestonesID) AS TotalRecordsByContract

FROM 
    dbo.PM_MilestonesType
INNER JOIN 
    dbo.Department AS DEP ON DEP.ID = dbo.PM_MilestonesType.DepartmentID
INNER JOIN 
    dbo.DisplacementServices AS DisService ON DisService.ID = dbo.PM_MilestonesType.DisplacementServicesID
INNER JOIN 
    dbo.Functions AS Functions ON Functions.ID = dbo.PM_MilestonesType.FunctionID
LEFT JOIN 
    dbo.MilestonesValue ON dbo.PM_MilestonesType.MilestonesValueID = MilestonesValue.ID; -- Fazendo o relacionamento com a tabela MilestonesValue
GO

EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[67] 4[3] 2[11] 3) )"
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
         Begin Table = "PM_MilestonesType"
            Begin Extent = 
               Top = 7
               Left = 879
               Bottom = 338
               Right = 1133
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "DEP"
            Begin Extent = 
               Top = 7
               Left = 48
               Bottom = 170
               Right = 277
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "DisService"
            Begin Extent = 
               Top = 175
               Left = 48
               Bottom = 294
               Right = 277
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "Functions"
            Begin Extent = 
               Top = 294
               Left = 48
               Bottom = 413
               Right = 277
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
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'vPM_MilestoneType'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=1 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'vPM_MilestoneType'
GO

