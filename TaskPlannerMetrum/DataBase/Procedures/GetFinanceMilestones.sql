USE [TaskPlanner]
GO

/****** Object:  StoredProcedure [dbo].[GetFinanceMilestones]    Script Date: 16/01/2025 12:49:11 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[GetFinanceMilestones]
    @ContractID INT
AS
BEGIN
    SELECT 
        [StatusDpv] AS MilestonesName,
        10030 AS MilestonesID,
        BaseDate AS ScheduledDate, 
        EndDate AS RescheduledDate,
        [InvoicedDate] AS ExecutedDate,
        [Description],
        2 AS MilestonesTypeID,
        NULL AS Baseline,
        NEWID() AS  ID,
        [InvoicedValue] AS Value,
        [ContractID],
        NULL AS TypeMilestonesID, 
        NULL AS TechLeadID, 
        NULL AS BusinessUnitID 
    FROM 
        [TaskPlanner].[dbo].[vFinanceContract]
    WHERE 
        [StatusDpv] IN ('FATURADO', 'EM ANDAMENTO', 'CANCELADO','PARALIZADO') 
        AND [ContractID] = @ContractID

    UNION ALL

    SELECT 
        [MilestonesName],
        [MilestonesID],
        [ScheduledDate],
        [RescheduledDate],
        [ExecutedDate],
        [Description],
        [MilestonesTypeID],
        [Baseline],
        [ID],
        [Value],
        [ContractID],
        [TypeMilestonesID], 
        [TechLeadID],       
        [BusinessUnitID]    
    FROM 
        [TaskPlanner].[dbo].[vMileStonesValue]
    WHERE 
        [ContractID] = @ContractID
END;
GO

