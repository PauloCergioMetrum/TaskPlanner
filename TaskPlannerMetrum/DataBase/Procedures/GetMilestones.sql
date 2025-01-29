USE [TaskPlanner]
GO

/****** Object:  StoredProcedure [dbo].[GetMilestones]    Script Date: 16/01/2025 12:50:37 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[GetMilestones]
    @ContractID INT
AS
BEGIN
    -- Seleciona os registros da tabela MilestonesItemDefault sem filtro de ContractID
    SELECT 
        MID.[ID],
        NULL AS [ContractID],
        MID.[Name],
        NULL AS [IsDefault]
    FROM [TaskPlanner].[dbo].[MilestonesItemDefault] MID

    UNION

    -- Seleciona os registros da tabela MilestonesItem com filtro de ContractID
    SELECT 
        MI.[ID],
        MI.[ContractID],
        MI.[Name],
        MI.[IsDefault]
    FROM [TaskPlanner].[dbo].[MilestonesItem] MI
    WHERE MI.[ContractID] = @ContractID;
END
GO

