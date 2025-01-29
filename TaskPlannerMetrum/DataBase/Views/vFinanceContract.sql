USE [TaskPlanner]
GO

/****** Object:  View [dbo].[vFinanceContract]    Script Date: 16/01/2025 11:37:41 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO





CREATE VIEW [dbo].[vFinanceContract]
AS
    SELECT        c.InternalCode AS ContractName, 
              f.paymentCondition,
              d.Name AS DepartmentName, 
              f.invoice, f.id, f.Description,
              f.Amount, f.Value, 
              f.InvoicedValue,
              f.BaseDate, f.EndDate,
              f.ExpectedInvoiceDate,
              f.Status,
              f.InvoicedDate,
              f.Billing,    
              f.DepartmentID,
              f.FinanceType, 
              f.ContractID, 
              f.BusinessUnit,
			  f.WorkSpaceID,
			    w.Name AS WorkspaceName,
              IIF(
                 f.InvoicedValue > 0 AND f.InvoicedDate > '2020-01-01 00:00:00' AND invoice <> '',
                 'FATURADO',
                 IIF(
                      f.StatusDpv = 'CANCELADO' OR f.StatusDpv = 'PARALISADO', 
                      f.StatusDpv, 
                      'EM ANDAMENTO'
                     )
              ) AS 'StatusDpv',
              f.Guarantee , 
              f.GuaranteePeriod , 
              ISNULL(CASE WHEN f.StatusDpv = 'FATURADO' THEN DATEADD(MONTH, f.GuaranteePeriod,f.InvoicedDate) ELSE f.InvoicedDate END, '2023-01-04 00:00:00.0000000') AS DateExpectedGarantee , 
              CASE WHEN f.Guarantee = 1 THEN 'GARANTIA' ELSE 'NÃO SE APLICA'END AS StatusGuarantee 
FROM            dbo.Contracts AS c INNER JOIN
                         dbo.Finances AS f ON c.id = f.ContractID INNER JOIN
                             dbo.Department AS d ON d.ID = f.DepartmentID
							 INNER JOIN dbo.Workspace AS w ON w.ID= f.WorkSpaceID
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
         Begin Table = "c"
            Begin Extent = 
               Top = 6
               Left = 38
               Bottom = 136
               Right = 216
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "f"
            Begin Extent = 
               Top = 6
               Left = 254
               Bottom = 136
               Right = 453
            End
            DisplayFlags = 280
            TopColumn = 15
         End
         Begin Table = "d"
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
         Column = 2085
         Alias = 1725
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
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'vFinanceContract'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=1 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'vFinanceContract'
GO

