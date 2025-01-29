USE [TaskPlanner]
GO

/****** Object:  StoredProcedure [dbo].[GetContractDetails]    Script Date: 16/01/2025 11:49:36 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



CREATE  PROCEDURE [dbo].[GetContractDetails]
    @ContractIDs VARCHAR(MAX) = NULL,      -- String contendo IDs de contrato separados por vírgula
    @TechLeadIDs VARCHAR(MAX) = NULL,      -- String contendo IDs de líder técnico separados por vírgula
    @InspectorIDs VARCHAR(MAX) = NULL,     -- String contendo IDs de fiscais separados por vírgula
    @BusinessUnitIDs NVARCHAR(MAX) = NULL, -- String contendo nomes de unidades de negócios separados por vírgula
    @StartDate DATETIME = NULL,            -- Data e hora de início do período
    @EndDate DATETIME = NULL               -- Data e hora de término do período
AS
BEGIN
    SELECT 
        c.id AS ContractID,
        c.InternalCode AS InternalCode,
        c.StartDate AS StartDate,
        c.[DateFineshed] AS EndDate,  -- Ajustado para retornar DATETIME formatado como string
		
        STUFF((
            SELECT DISTINCT ', ' + bu.Name
            FROM dbo.MilestonesItem mi
            JOIN dbo.MilestonesValue mv ON mi.ID = mv.MilestonesID
            JOIN dbo.BusinessUnit bu ON mv.BusinessUnitID = bu.ID
            WHERE mi.ContractID = c.id
              AND (@BusinessUnitIDs IS NULL OR bu.Name IN (SELECT value FROM STRING_SPLIT(@BusinessUnitIDs, ',')))
            FOR XML PATH('')), 1, 2, '') AS BusinessUnits,
        ISNULL(u.full_name, '') AS ProjectInspector,
        ISNULL(cl.Name, '') AS ClientName,
        ISNULL(tl.full_name, '') AS TechLead
    FROM 
        dbo.Contracts c
    LEFT JOIN dbo.Users u ON c.InspectorID = u.ID
    LEFT JOIN dbo.Clients cl ON c.ClientID = cl.ID
    LEFT JOIN dbo.MilestonesItem mi ON c.id = mi.ContractID
    LEFT JOIN dbo.MilestonesValue mv ON mi.ID = mv.MilestonesID
    LEFT JOIN dbo.Users tl ON mv.TechLeadID = tl.ID
    WHERE 
        (@ContractIDs IS NULL OR CHARINDEX(',' + CAST(c.id AS VARCHAR) + ',', ',' + @ContractIDs + ',') > 0)  -- Verifica se o ID do contrato está na lista, se @ContractIDs não for nulo
        AND (@TechLeadIDs IS NULL OR CHARINDEX(',' + CAST(mv.TechLeadID AS VARCHAR) + ',', ',' + @TechLeadIDs + ',') > 0)  -- Verifica se o ID do líder técnico está na lista, se @TechLeadIDs não for nulo
        AND (@InspectorIDs IS NULL OR CHARINDEX(',' + CAST(c.InspectorID AS VARCHAR) + ',', ',' + @InspectorIDs + ',') > 0)  -- Verifica se o ID do fiscal está na lista, se @InspectorIDs não for nulo
        AND (@StartDate IS NULL OR c.StartDate >= @StartDate)
        AND (@EndDate IS NULL OR c.StartDate <= @EndDate)
        AND (@BusinessUnitIDs IS NULL OR EXISTS (
            SELECT 1
            FROM dbo.MilestonesItem mi
            JOIN dbo.MilestonesValue mv ON mi.ID = mv.MilestonesID
            JOIN dbo.BusinessUnit bu ON mv.BusinessUnitID = bu.ID
            WHERE mi.ContractID = c.id
              AND bu.Name IN (SELECT value FROM STRING_SPLIT(@BusinessUnitIDs, ','))
        ));

END;
GO

