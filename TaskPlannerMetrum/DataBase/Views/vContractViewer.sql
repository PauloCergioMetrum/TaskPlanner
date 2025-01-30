USE [TaskPlanner]
GO

/****** Object:  View [dbo].[vContractViewer]    Script Date: 16/01/2025 11:37:20 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



CREATE       VIEW [dbo].[vContractViewer]
AS
SELECT   f.id as 'financeid', c.id as 'cID'

,ui.full_name as 'Owner'
		,(Select name from workspace where id = c.TagID ) AS 'Company'
		,c.StartDate as 'StartDate'
		,c.InternalCode as 'SeniorCode'
		,cl.Name as 'ClientName'
		,f.Description as 'Description'
		,uv.full_name as 'Vendor'
		,f.Amount as 'Amount.'
		,f.Value as 'TotalValue'
		,f.InvoicedValue as 'InvoicedValue'
		,f.BaseDate as 'BaseDate'
		,f.EndDate as 'EndDate'
		,LEFT ((select * from convertDate(f.ExpectedInvoiceDate)),7) AS 'BillingMonth'
		,c.PaymentMethod as 'PaymentMethod'
		,f.paymentCondition as 'paymentCondition'
		,f.InvoicedDate as 'InvoicedDate'
		,f.invoice 'invoice'
		,d.Name as 'DepartmentName' 
		,f.FinanceType as 'FinanceType'
		,f.Status as 'PVStatus'
		,f.StatusDpv as 'PaymentStatus'
  FROM [Contracts] c
	inner join [vFinanceContract] f on c.id = f.ContractID
		inner join users ui on ui.id = c.inspectorID
			inner join Clients cl on cl.ID = c.ClientID
				inner join users uv on c.VendorID = uv.id
					inner join Department d on f.DepartmentID = d.ID
GO

