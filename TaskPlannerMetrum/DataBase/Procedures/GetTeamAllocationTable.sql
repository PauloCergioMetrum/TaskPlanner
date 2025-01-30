USE [TaskPlanner]
GO

/****** Object:  StoredProcedure [dbo].[GetTeamAllocationTable]    Script Date: 16/01/2025 12:54:04 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[GetTeamAllocationTable]
    @BusinessUnit NVARCHAR(100) = NULL,
    @StartDate DATE = NULL,
    @EndDate DATE = NULL,
    @FunctionIDs NVARCHAR(MAX) = NULL,  
    @Project NVARCHAR(100) = NULL
AS
BEGIN
    SET NOCOUNT ON;

    -- Criar uma tabela temporária para armazenar os FunctionIDs
    DECLARE @FunctionIDTable TABLE (FunctionID INT);

    -- Inserir os FunctionIDs na tabela temporária
    IF (@FunctionIDs IS NOT NULL AND @FunctionIDs != '')
    BEGIN
        INSERT INTO @FunctionIDTable (FunctionID)
        SELECT DISTINCT value 
        FROM STRING_SPLIT(@FunctionIDs, ',');
    END

    -- Selecionar os dados com LEFT JOIN para garantir que todos os registros de Contracts sejam retornados
    SELECT 
        ROW_NUMBER() OVER (ORDER BY C.[InternalCode]) AS ID,  
        C.[InternalCode] AS SaleOrder, 
        CL.[Name] AS Client, 
        AP.[BusinessUnit] AS BusinessUnit, 
        AP.[PlannedManHour] AS PlannedHours, 
        AP.[ExecutedManHour] AS ExecutedHours,
        AP.[ScheduledDate] AS ScheduledDate,  
        U.full_name AS Executor,
        F.[Name] AS FunctionName  
    FROM 
        [dbo].[Contracts] C
    LEFT JOIN 
        [dbo].[Clients] CL ON CL.[ID] = C.[ClientID]
    LEFT JOIN 
        [dbo].[ActivityPlan] AP ON AP.[ContractID] = C.[ID]
    LEFT JOIN 
        [dbo].[Team] T ON T.[ID] = AP.[ExecutorTeamID]
    LEFT JOIN 
        [dbo].[Users] U ON U.[ID] = T.[UserID]
    LEFT JOIN 
        [dbo].[UserHourCosts] UHC ON UHC.[UserID] = U.[ID]
    LEFT JOIN 
        [dbo].[Functions] F ON F.[Name] = UHC.[FunctionName]  
    WHERE 
        (@BusinessUnit IS NULL OR AP.[BusinessUnit] = @BusinessUnit)
        AND (@Project IS NULL OR C.[InternalCode] = @Project)
        AND (
            NOT EXISTS (SELECT 1 FROM @FunctionIDTable) 
            OR F.[ID] IN (SELECT FunctionID FROM @FunctionIDTable)
        )
    GROUP BY 
        C.[InternalCode], 
        CL.[Name], 
        AP.[BusinessUnit],
        AP.[PlannedManHour],
        AP.[ExecutedManHour],
        AP.[ScheduledDate], 
        U.full_name,
        F.[Name];
END
GO

