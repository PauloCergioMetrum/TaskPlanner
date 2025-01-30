USE [TaskPlanner]
GO

/****** Object:  View [dbo].[vTabelaoExport]    Script Date: 16/01/2025 11:43:14 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO







CREATE VIEW [dbo].[vTabelaoExport]
AS
SELECT   f.id as 'financeid', c.id as 'cID'

,ui.full_name as 'RESPONSÁVEL'
		,(Select name from workspace where id = c.TagID ) AS 'EMPRESA'
		,c.StartDate as 'DATA'
		,c.InternalCode as 'SENIOR'
		,cl.Name as 'CLIENTE'
		,f.Description as 'DESCRICAO DA VENDA'
		,uv.full_name as 'VENDEDOR'
		,f.Amount as 'QNT.'
		,f.Value as 'VALOR PV'
		,f.InvoicedValue as 'VALOR FATURADO'
		,f.BaseDate as 'DATA BASE'
		,f.EndDate as 'DATA REPROGRAMADA'
		,f.ExpectedInvoiceDate as  'MES PREVISTO FATURAMENTO'
		,c.PaymentMethod as 'FORMA DE PAGAMENTO'
		,f.paymentCondition as 'COND. PG'
		,f.InvoicedDate as 'FATURADO DIA'
		,f.invoice 'NOTA FISCAL'
		,d.Name as 'SETOR RESPONSAVEL' 
		,f.FinanceType as 'TIPO'
		,f.Status as 'STATUS PV'
		,f.StatusDpv as 'Status Faturamento'
  FROM [Contracts] c
	inner join [Finances] f on c.id = f.ContractID
		inner join users ui on ui.id = c.inspectorID
			inner join Clients cl on cl.ID = c.ClientID
				inner join users uv on c.VendorID = uv.id
					inner join Department d on f.DepartmentID = d.ID
GO

