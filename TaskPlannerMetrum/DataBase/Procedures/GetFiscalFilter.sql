USE [TaskPlanner]
GO

/****** Object:  StoredProcedure [dbo].[GetFiscalFilter]    Script Date: 16/01/2025 12:49:27 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE   PROCEDURE [dbo].[GetFiscalFilter]
    @InspectorIDs NVARCHAR(MAX) = NULL, 
    @StartDate DATE = NULL,             
    @EndDate DATE = NULL,
    @BusinessUnits NVARCHAR(MAX) = NULL  -- Adicionando parâmetro para BusinessUnit
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @InspectorIDTable TABLE (InspectorID INT);
    DECLARE @BusinessUnitTable TABLE (BusinessUnit NVARCHAR(255));  -- Tabela temporária para BusinessUnit

    -- Garantindo que @InspectorIDs seja acessível
    IF @InspectorIDs IS NOT NULL
    BEGIN
        INSERT INTO @InspectorIDTable (InspectorID)
        SELECT value FROM STRING_SPLIT(@InspectorIDs, ',');
    END

    -- Garantindo que @BusinessUnits seja acessível
    IF @BusinessUnits IS NOT NULL
    BEGIN
        INSERT INTO @BusinessUnitTable (BusinessUnit)
        SELECT value FROM STRING_SPLIT(@BusinessUnits, ',');
    END

    SELECT
        ROW_NUMBER() OVER (ORDER BY C.inspectorID) AS [ID], 
        C.inspectorID AS [inspectorID],
        U.user_name AS [user_name],  
        F.WorkSpaceID AS [WorkSpaceID],
        W.Name AS [Name], 
        F.BusinessUnit AS [BusinessUnit],
        C.InternalCode AS [InternalCode],
        C.ClientID AS [ClientID],
        Cl.Name AS [ClientName], 
        C.Condition AS [Condition],
        F.Value AS [Value],
        F.InvoicedValue AS [InvoicedValue],
        F.ExpectedInvoiceDate AS [ExpectedInvoiceDate],  
        F.InvoicedDate AS [InvoicedDate],
        F.StatusDpv AS [StatusDpv],
        F.BaseDate AS [BaseDate]
    FROM
        [dbo].[Finances] F
    INNER JOIN
        [dbo].[Contracts] C ON F.ContractID = C.id
    INNER JOIN
        [dbo].[Team] T ON C.inspectorID = T.UserID
    INNER JOIN
        [dbo].[users] U ON T.UserID = U.id
    INNER JOIN
        [dbo].[Clients] Cl ON C.ClientID = Cl.ID
    INNER JOIN
        [dbo].[Workspace] W ON F.WorkSpaceID = W.ID
    
    WHERE
        (@InspectorIDs IS NULL OR @InspectorIDs = '' OR C.inspectorID IN (SELECT InspectorID FROM @InspectorIDTable)) 
        AND (@StartDate IS NULL OR F.BaseDate >= @StartDate)  
        AND (@EndDate IS NULL OR F.BaseDate <= @EndDate)
        AND (@BusinessUnits IS NULL OR @BusinessUnits = '' OR F.BusinessUnit IN (SELECT BusinessUnit FROM @BusinessUnitTable)); -- Novo filtro para BusinessUnit

END
GO

