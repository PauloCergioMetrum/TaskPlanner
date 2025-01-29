USE [TaskPlanner]
GO

/****** Object:  StoredProcedure [dbo].[GetReportDetailsTable]    Script Date: 16/01/2025 12:52:26 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[GetReportDetailsTable]
    @startDate DATE = NULL,
    @endDate DATE = NULL,
    @BusinessUnits NVARCHAR(MAX) = NULL,
    @InspectorIDs NVARCHAR(MAX) = NULL
AS
BEGIN
    -- Seleciona as informações necessárias combinando as tabelas Finances, Users, Contracts, Workspace e Clients
    SELECT 
        f.ID AS [FinanceID],                      -- ID da tabela Finances
        u.full_name AS [Inspector],               -- FISCAL
        w.Name AS [Company],                      -- EMPRESA
        f.BusinessUnit AS [BusinessUnit],         -- UN (Unidade de Negócio)
        c.InternalCode AS [InternalCode],         -- PV (Pedido de Venda / Nome do Projeto)
        cl.Name AS [Client],                      -- CLIENTE
        f.Description AS [SaleDescription],       -- DESCRIÇÃO DA VENDA
        f.Value AS [ItemValue],                   -- VALOR DO ITEM
        f.InvoicedValue AS [InvoicedValue],       -- VALOR FATURADO
        FORMAT(TRY_CAST(SUBSTRING(f.ExpectedInvoiceDate, 6, 20) AS DATETIME), 'MMM/yy') AS [PredictedInvoicingMonth], -- MÊS PREVISTO FATURAMENTO
        f.InvoicedDate AS [InvoicingDate],        -- DATA DE FATURAMENTO
        f.StatusDpv AS [SalesOrderStatus]         -- STATUS DO PV
    FROM 
        [dbo].[Finances] f
    LEFT JOIN 
        [dbo].[Contracts] c ON f.ContractID = c.id
    LEFT JOIN 
        [dbo].[Users] u ON c.inspectorID = u.id
    LEFT JOIN 
        [dbo].[Workspace] w ON c.WorkSpaceID = w.ID
    LEFT JOIN 
        [dbo].[Clients] cl ON c.ClientID = cl.ID
    WHERE 
        (@startDate IS NULL OR f.InvoicedDate >= @startDate)
        AND (@endDate IS NULL OR f.InvoicedDate <= @endDate)
        AND (@BusinessUnits IS NULL OR f.BusinessUnit IN (SELECT value FROM STRING_SPLIT(@BusinessUnits, ',')))
        AND (@InspectorIDs IS NULL OR c.inspectorID IN (SELECT value FROM STRING_SPLIT(@InspectorIDs, ',')))
    ORDER BY 
        [InvoicingDate], [BusinessUnit];
END;
GO

