USE [TaskPlanner]
GO

/****** Object:  View [dbo].[ClientFeedbackDetailsView]    Script Date: 16/01/2025 11:33:19 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE VIEW [dbo].[ClientFeedbackDetailsView]
AS
SELECT 
    ROW_NUMBER() OVER (ORDER BY C.ID) AS ID,  
    C.ID AS ContractID,
    C.InternalCode AS ContractName,
    -- Utilizando TOP 1 para garantir que a subconsulta retorne apenas um valor
    (SELECT TOP 1 CONCAT(I.Name, ' - (', I.PhoneNumber, ')') 
     FROM dbo.PM_Information_General I 
     WHERE I.ContractID = C.ID AND I.Type = 1) AS Contact,
    COALESCE(S.Scope, 'Escopo não definido') AS Scope,
    -- Utilizando TOP 1 para garantir que a subconsulta retorne apenas um valor
    (SELECT TOP 1 MAX(ExecutedDate) 
     FROM dbo.ActivityPlan 
     WHERE ContractID = C.ID AND Status = 5) AS LastExecutedDate,
    -- Incluindo a maior ScheduledDate e Status da tabela ActivityPlan
    (SELECT TOP 1 ScheduledDate 
     FROM dbo.ActivityPlan 
     WHERE ContractID = C.ID 
     ORDER BY ScheduledDate DESC) AS LatestScheduledDate,
    (SELECT TOP 1 Status 
     FROM dbo.ActivityPlan 
     WHERE ContractID = C.ID 
     ORDER BY ScheduledDate DESC) AS LatestStatus,
    CF.ContactStartDate AS LastContactDate,
    COALESCE(SC.ClientResponse, '') AS ClientResponse,  
    COALESCE(SC.ClientRating, 0) AS ClientRating,     
    COALESCE(SC.ReceivedComplaint, '') AS ReceivedComplaint,
    COALESCE(SC.FeedbackDate, NULL) AS FeedbackDate,
    -- Renomeando DateFineshed para MaxScheduledDate
    C.DateFineshed AS MaxScheduledDate
FROM 
    dbo.Contracts C
    LEFT JOIN dbo.PM_TAP_Scope S ON S.ContractID = C.ID 
    LEFT JOIN dbo.ClientFeedback CF ON CF.ContractID = C.ID
    LEFT JOIN dbo.Satisfaction_Customer SC ON SC.ContractID = C.ID
GROUP BY 
    C.InternalCode,
    C.ID,
    COALESCE(S.Scope, 'Escopo não definido'),
    CF.ContactStartDate,
    SC.ClientResponse,
    SC.ClientRating,
    SC.ReceivedComplaint,
    SC.FeedbackDate,
    C.DateFineshed;
GO

