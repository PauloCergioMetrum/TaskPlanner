USE [TaskPlanner]
GO

/****** Object:  StoredProcedure [dbo].[GetTeamAllocationGraphicFunctions]    Script Date: 16/01/2025 12:53:52 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[GetTeamAllocationGraphicFunctions]
    @StartDate DATE = NULL,
    @EndDate DATE = NULL,
    @FunctionIDs NVARCHAR(MAX) = NULL
AS
BEGIN 
    SET NOCOUNT ON;

    -- Criar uma tabela temporária para armazenar os FunctionIDs
    DECLARE @FunctionIDTable TABLE (FunctionID INT);

    -- Inserir os FunctionIDs na tabela temporária
    INSERT INTO @FunctionIDTable (FunctionID)
    SELECT value 
    FROM STRING_SPLIT(@FunctionIDs, ',');

    SELECT  
        f.Name AS FunctionName,  
        COUNT(DISTINCT uhc.UserID) AS Quantity
    FROM
        UserHourCosts uhc
    JOIN
        Functions f ON uhc.FunctionName = f.Name  -- Corrigido para comparar nomes textuais
    WHERE
        (@StartDate IS NULL OR uhc.StartDate >= @StartDate)
        AND (@EndDate IS NULL OR uhc.EndDate <= @EndDate)
        AND (NOT EXISTS (SELECT 1 FROM @FunctionIDTable) OR f.ID IN (SELECT FunctionID FROM @FunctionIDTable))
    GROUP BY
        f.Name 
    ORDER BY
        f.Name;
END;
GO

