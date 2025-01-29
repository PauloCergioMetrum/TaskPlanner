USE [TaskPlanner]
GO

/****** Object:  View [dbo].[vContractList]    Script Date: 16/01/2025 11:36:52 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [dbo].[vContractList]
AS
WITH businessUnit_cte AS (SELECT DISTINCT ContractID, BusinessUnit
                                                      FROM      dbo.Finances), finances_cte AS
    (SELECT ContractID, SUM(Value) AS ValueTotal, SUM(InvoicedValue) AS InvoicedValueTotal,
                           (SELECT STRING_AGG(BusinessUnit, ', ') AS Departments
                            FROM      businessUnit_cte AS v
                            WHERE   (ContractID = f.ContractID)) AS BusinessUnit
     FROM      dbo.Finances AS f
     GROUP BY ContractID), DistinctWorkspaces AS
    (SELECT DISTINCT ContractID, WorkSpaceID, WorkspaceName
     FROM      dbo.vFinanceContract)
    SELECT dbo.Clients.Name AS ClientName, dbo.Contracts.PaymentMethod, dbo.Contracts.Condition, dbo.Contracts.InternalCode, dbo.Contracts.StartDate, dbo.users.full_name AS InspectorName, vVendor.full_name AS VendorName, 
                      dbo.Contracts.id AS ContractID, COALESCE (dbo.Contracts.ClientOrder, 0) AS ClientOrder, dbo.Contracts.EnableProject, COALESCE
                          ((SELECT ValueTotal / 10 AS Expr1
                            FROM      finances_cte AS f
                            WHERE   (ContractID = dbo.Contracts.id)), 0) AS ValueTotal, COALESCE
                          ((SELECT InvoicedValueTotal / 10 AS Expr1
                            FROM      finances_cte AS f
                            WHERE   (ContractID = dbo.Contracts.id)), 0) AS InvoicedValueTotal, COALESCE
                          ((SELECT BusinessUnit
                            FROM      finances_cte AS f
                            WHERE   (ContractID = dbo.Contracts.id)), '-') AS BusinessUnit, dbo.Contracts.Observation, dbo.Contracts.DateRetroactive, dbo.Contracts.ValidityStartDate, dbo.Contracts.PredictedMarkup, dbo.Contracts.PredictedSavings, 
                      dbo.Contracts.ValidityEndDate,
                          (SELECT dbo.getStatus(dbo.Contracts.id, N'FINANCES') AS Expr1) AS StatusID, dbo.Contracts.PaymentCondition,
                          (SELECT CASE WHEN EXISTS
                                                 (SELECT 1
                                                  FROM      vFinanceContract
                                                  WHERE   ContractID = dbo.Contracts.id AND StatusDpv = 'FATURADO' AND Guarantee = 1 AND DateExpectedGarantee > GETDATE()) THEN 'Em Garantia' WHEN EXISTS
                                                 (SELECT 1
                                                  FROM      vFinanceContract
                                                  WHERE   ContractID = dbo.Contracts.id AND StatusDpv = 'FATURADO' AND Guarantee = 1 AND DateExpectedGarantee < GETDATE()) THEN 'Fora de Garantia' WHEN NOT EXISTS
                                                 (SELECT 1
                                                  FROM      vFinanceContract
                                                  WHERE   ContractID = dbo.Contracts.id AND StatusDpv != 'FATURADO') THEN 'Não Iniciado' ELSE 'Não Aplica' END AS statusGuarantee) AS statusGuarantee,
                          (SELECT dbo.getStatus(dbo.Contracts.id, N'FINANCES') AS Expr1) AS Status, ws.WorkSpaceID, ws.WorkspaceName, dbo.Contracts.DateFineshed
    FROM     dbo.Clients INNER JOIN
                      dbo.Contracts ON dbo.Clients.ID = dbo.Contracts.ClientID INNER JOIN
                      dbo.users ON dbo.Contracts.inspectorID = dbo.users.id INNER JOIN
                      dbo.users AS vVendor ON vVendor.id = dbo.Contracts.VendorID LEFT OUTER JOIN
                      dbo.StatusContract ON dbo.Contracts.StatusID = dbo.StatusContract.ID LEFT OUTER JOIN
                      DistinctWorkspaces AS ws ON dbo.Contracts.id = ws.ContractID
GO

EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[54] 4[39] 2[2] 3) )"
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
         Top = -120
         Left = 0
      End
      Begin Tables = 
         Begin Table = "Clients"
            Begin Extent = 
               Top = 7
               Left = 48
               Bottom = 170
               Right = 277
            End
            DisplayFlags = 280
            TopColumn = 6
         End
         Begin Table = "Contracts"
            Begin Extent = 
               Top = 175
               Left = 48
               Bottom = 338
               Right = 277
            End
            DisplayFlags = 280
            TopColumn = 17
         End
         Begin Table = "users"
            Begin Extent = 
               Top = 343
               Left = 48
               Bottom = 506
               Right = 313
            End
            DisplayFlags = 280
            TopColumn = 5
         End
         Begin Table = "vVendor"
            Begin Extent = 
               Top = 511
               Left = 48
               Bottom = 674
               Right = 313
            End
            DisplayFlags = 280
            TopColumn = 5
         End
         Begin Table = "StatusContract"
            Begin Extent = 
               Top = 679
               Left = 48
               Bottom = 842
               Right = 277
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "ws"
            Begin Extent = 
               Top = 847
               Left = 48
               Bottom = 988
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
         Al' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'vContractList'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane2', @value=N'ias = 900
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
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'vContractList'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=2 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'vContractList'
GO

