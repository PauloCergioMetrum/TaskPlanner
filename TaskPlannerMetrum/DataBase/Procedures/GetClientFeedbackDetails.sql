USE [TaskPlanner]
GO

/****** Object:  StoredProcedure [dbo].[GetClientFeedbackDetails]    Script Date: 16/01/2025 11:49:05 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



CREATE     PROCEDURE [dbo].[GetClientFeedbackDetails]
    @ContractID INT
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        ROW_NUMBER() OVER (ORDER BY C.ID) AS ID,  
        C.InternalCode AS ContractName,
        (SELECT CONCAT(I.Name, ' - (', I.PhoneNumber, ')') 
         FROM dbo.PM_Information_General I 
         WHERE I.ContractID = C.ID AND I.Type = 1) AS Contact,
        S.Scope AS Scope,
        (SELECT MAX(ExecutedDate) FROM dbo.ActivityPlan WHERE ContractID = C.ID AND Status = 5) AS LastExecutedDate,
        SC.FeedbackDate AS LastContactDate,
        COALESCE(SC.ClientResponse, '') AS ClientResponse,  
        COALESCE(SC.ClientRating, 0) AS ClientRating,     
        COALESCE(SC.ReceivedComplaint, '') AS ReceivedComplaint,
        COALESCE(SC.FeedbackDate, NULL) AS FeedbackDate 
    FROM 
        dbo.Contracts C
		LEFT JOIN dbo.Satisfaction_Customer SC ON SC.ContractID = C.ID
        LEFT JOIN dbo.PM_TAP_Scope S ON S.ContractID = SC.ID        
        
    WHERE 
        C.ID = @ContractID
    GROUP BY 
        C.InternalCode,
        C.ID,
        S.Scope,        
        SC.ClientResponse,
        SC.ClientRating,
        SC.ReceivedComplaint,
        SC.FeedbackDate; 
END
GO

