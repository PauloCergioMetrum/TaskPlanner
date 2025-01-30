USE [TaskPlanner]
GO

/****** Object:  View [dbo].[vPm_Cost_Planned]    Script Date: 16/01/2025 11:39:22 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE VIEW [dbo].[vPm_Cost_Planned]
AS
SELECT 
    TypeID, 
    Id, 
    Amount, 
    ValueUnit, 
    Description, 
    ContractID,
    -- Multiplica os valores de Total ao invés de somá-los
    (SELECT COALESCE(PRODUTO.Total, 0)
     FROM (
        SELECT 
            EXP(SUM(LOG(NULLIF(cm.Total, 0)))) AS Total
        FROM dbo.vPmCostMade AS cm
        WHERE cm.Pm_Cost_Planned_Id = cp.Id
     ) PRODUTO) AS Total, 
    (Amount * ValueUnit) AS TotalPlanned,  -- Calcula TotalPlanned como Amount * ValueUnit
    ((Amount * ValueUnit) - 
     (SELECT COALESCE(PRODUTO.Total, 0)
      FROM (
        SELECT 
            EXP(SUM(LOG(NULLIF(cm.Total, 0)))) AS Total
        FROM dbo.vPmCostMade AS cm
        WHERE cm.Pm_Cost_Planned_Id = cp.Id
      ) PRODUTO)) AS Difference  -- Calcula Difference como TotalPlanned - Total
FROM 
    dbo.Pm_Cost_Planned AS cp
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
         Begin Table = "cp"
            Begin Extent = 
               Top = 7
               Left = 48
               Bottom = 339
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
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'vPm_Cost_Planned'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=1 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'vPm_Cost_Planned'
GO

