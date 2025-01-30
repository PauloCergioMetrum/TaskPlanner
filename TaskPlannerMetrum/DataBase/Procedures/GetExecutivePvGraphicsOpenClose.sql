USE [TaskPlanner]
GO

/****** Object:  StoredProcedure [dbo].[GetExecutivePvGraphicsOpenClose]    Script Date: 16/01/2025 12:48:57 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[GetExecutivePvGraphicsOpenClose]
    @Closed BIT = NULL,
    @Open BIT = NULL
AS
BEGIN
    IF @Closed = 1
    BEGIN
        -- Retornar registros com StatusID = 5 (fechados)
        SELECT 
            BusinessUnit,
            FORMAT(StartDate, 'MM-yyyy') AS StartDate,
            SUM(ValueTotal) AS ValueTotal
        FROM 
            [TaskPlanner].[dbo].[vContractList]
        WHERE 
            StatusID = 5
        GROUP BY 
            BusinessUnit, FORMAT(StartDate, 'MM-yyyy')
    END
    ELSE IF @Open = 1
    BEGIN
        -- Retornar registros com StatusID diferente de 5 (abertos)
        SELECT 
            BusinessUnit,
            FORMAT(StartDate, 'MM-yyyy') AS StartDate,
            SUM(ValueTotal) AS ValueTotal
        FROM 
            [TaskPlanner].[dbo].[vContractList]
        WHERE 
            StatusID <> 5
        GROUP BY 
            BusinessUnit, FORMAT(StartDate, 'MM-yyyy')
    END
    ELSE
    BEGIN
        -- Retornar todos os registros sem filtrar por StatusID
        SELECT 
            BusinessUnit,
            FORMAT(StartDate, 'MM-yyyy') AS StartDate,
            SUM(ValueTotal) AS ValueTotal
        FROM 
            [TaskPlanner].[dbo].[vContractList]
        GROUP BY 
            BusinessUnit, FORMAT(StartDate, 'MM-yyyy')
    END
END
GO

