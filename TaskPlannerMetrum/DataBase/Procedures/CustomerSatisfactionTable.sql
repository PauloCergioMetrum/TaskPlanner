USE [TaskPlanner]
GO

/****** Object:  StoredProcedure [dbo].[CustomerSatisfactionTable]    Script Date: 16/01/2025 11:46:57 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[CustomerSatisfactionTable]
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        ROW_NUMBER() OVER (ORDER BY C.ContractID) AS ID,  
        C.ContractID,
        C.InternalCode AS ContractName,
        C.InspectorName,
        C.BusinessUnit,
        C.VendorName,
        C.ClientName AS ClientName,  
        C.StartDate,
        C.DateFineshed,
        (SELECT TOP 1 CONCAT(I.Name, ' - (', I.PhoneNumber, ')')  
         FROM dbo.PM_Information_General I  
         WHERE I.ContractID = C.ContractID AND I.Type = 1) AS Contact,  
        (SELECT MAX(ExecutedDate)  
         FROM dbo.ActivityPlan  
         WHERE ContractID = C.ContractID AND Status = 5) AS LastExecutedDate,
        CF.ContactStartDate AS LastContactDate,
        COALESCE(SC.ClientRating, 0) AS ClientRating,  
        COALESCE(SC.ReceivedComplaint, '') AS ReceivedComplaint,
        COALESCE(SC.FeedbackDate, NULL) AS FeedbackDate,
        C.StatusID,  
        SC.Status AS Status_satisfaction_Customer  
    FROM  
        dbo.vContractList C
        LEFT JOIN dbo.ClientFeedback CF ON CF.ContractID = C.ContractID
        LEFT JOIN dbo.Satisfaction_Customer SC ON SC.ContractID = C.ContractID  
        LEFT JOIN dbo.Clients CL ON C.ClientName = CL.ID  
    WHERE
        C.StatusID = 5  
    GROUP BY  
        C.ContractID,
        C.InternalCode,
        C.InspectorName,  
        C.ClientName,  
        CF.ContactStartDate,
        SC.ClientRating,
        SC.FeedbackDate,
        C.DateFineshed,  
        SC.ReceivedComplaint,
        C.BusinessUnit, 
        C.VendorName, 
        C.StatusID,  
        SC.Status,  
        C.StartDate 
    ORDER BY  
        C.ContractID;
END
GO

