USE [TaskPlanner]
GO

/****** Object:  StoredProcedure [dbo].[GetMilestoneDetails]    Script Date: 16/01/2025 12:50:21 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



CREATE PROCEDURE [dbo].[GetMilestoneDetails]
    @ContractID INT
AS
BEGIN
    SET NOCOUNT ON;

    WITH UniqueMilestones AS (
        SELECT DISTINCT
            ap.ContractID,
            ISNULL(ap.MilestonesID, 0) AS MilestonesID,
            ap.MilestoneName,
            mv.TechLeadID,
            mv.BusinessUnitID,
            mv.ScheduledDate,  
            mv.RescheduledDate,
            ap.LateTaskCount AS Delayed,

           (SELECT SUM(PT.Hours)  
             FROM [TaskPlanner].[dbo].[MilestonesValue] MV
             INNER JOIN dbo.MilestonesItem AS MI ON MI.ID = MV.MilestonesID  
             INNER JOIN dbo.PM_MilestonesType AS PT ON PT.MilestonesValueID = MV.ID
             WHERE MI.ContractID = @ContractID 
               AND MI.ID = ap.MilestonesID) AS ExpectedHours,
            SUM(ap.PlannedManHour) AS TotalPlannedManHour,
            SUM(ap.ExecutedManHour) AS TotalExecutedManHour
        FROM
            vActivePlans ap        
        LEFT JOIN
            MilestonesValue mv ON mv.MilestonesID = ap.MilestonesID		
        WHERE
            ap.ContractID = @ContractID
        GROUP BY
            ap.ContractID,
            ap.MilestonesID,
            ap.MilestoneName,
            mv.TechLeadID,
            mv.BusinessUnitID,
            mv.ScheduledDate,  
            mv.RescheduledDate,
            ap.LateTaskCount
    )

    SELECT DISTINCT
        um.ContractID,
        um.MilestonesID,
        um.MilestoneName,
        um.TechLeadID,
        u.user_name AS TechLeadName,
        um.BusinessUnitID,
        CASE 
            WHEN um.MilestonesID <> 0 THEN vc.BusinessUnit
            ELSE NULL
        END AS BusinessUnit,
        um.ScheduledDate, 
        um.RescheduledDate,
        um.Delayed,
        um.ExpectedHours,
		um.TotalPlannedManHour,
		um.TotalExecutedManHour
    FROM
        UniqueMilestones um
    LEFT JOIN
        users u ON um.TechLeadID = u.id
    LEFT JOIN
        vFinanceContract vc ON vc.ContractID = um.ContractID;
END;
GO

