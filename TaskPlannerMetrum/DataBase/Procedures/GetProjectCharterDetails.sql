USE [TaskPlanner]
GO

/****** Object:  StoredProcedure [dbo].[GetProjectCharterDetails]    Script Date: 16/01/2025 12:51:48 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[GetProjectCharterDetails]
    @ContractID INT
AS
BEGIN
    SET NOCOUNT ON;

    WITH DistinctDepartments AS (
    SELECT DISTINCT 
        DP.Name,
        MI.ContractID
    FROM MilestonesItem AS MI
    LEFT JOIN MilestonesValue AS MV ON MV.MilestonesID = MI.ID
    LEFT JOIN PM_MilestonesType AS MT ON MT.MilestonesValueID = MV.ID
    LEFT JOIN Department AS DP ON DP.ID = MT.DepartmentID
    WHERE MI.ContractID = @ContractID AND MV.BusinessUnitID <> 0
)
SELECT
    ISNULL(C.InternalCode, '') AS ProjectName,
    ISNULL(CL.Name, '') AS ClientName,
    ISNULL(W.Name, '') AS WorkspaceName,
    ISNULL(
        (SELECT STRING_AGG(Name, ', ') FROM DistinctDepartments WHERE ContractID = C.ID),
        ''
    ) AS Departments,
    --ISNULL((
    --    SELECT STRING_AGG(BU.Name, ', ')
    --    FROM [TaskPlanner].[dbo].[vMileStonesValue]
    --    INNER JOIN BusinessUnit AS BU ON BU.ID = BusinessUnitID 
    --    WHERE ContractID = C.ID AND BusinessUnitID <> 0
    --), '') AS BusinessUnit,
	CLS.BusinessUnit AS BusinessUnit,
    ISNULL(U.full_name, '') AS InspectorName,
    ISNULL(FORMAT(C.ValidityStartDate, 'dd/MM/yyyy'), '') AS ValidityStartDate,
    ISNULL(FORMAT(C.ValidityEndDate, 'dd/MM/yyyy'), '') AS ValidityEndDate,
    ISNULL(TAP_INFO.Local, '') AS Local,
    ISNULL(CAST(TAP_INFO.ConsultantID AS VARCHAR), '') AS ConsultantID,
    ISNULL(RL.Name, '') AS RiskLevel,
    ISNULL(TAP_SCOPE.Scope, '') AS Scope,
    ISNULL(TAP_SCOPE.Goal, '') AS Goal,
    ISNULL(TAP_SCOPE.GeneralRisks, '') AS GeneralRisks,
    ISNULL(TAP_SCOPE.Premises, '') AS Premises,
    ISNULL(TAP_SCOPE.OutScope, '') AS OutScope,
    ISNULL(TAP_SCOPE.Deliveries, '') AS Deliveries
FROM dbo.Contracts C 
LEFT JOIN PM_TAP_General_Info AS TAP_INFO ON TAP_INFO.ContractID = C.id
LEFT JOIN PM_TAP_RiskLevel AS RL ON RL.ID = TAP_INFO.RiskLevelID
LEFT JOIN PM_TAP_Scope AS TAP_SCOPE ON TAP_SCOPE.ContractID = C.id
INNER JOIN Users AS U ON U.ID = C.InspectorID 
INNER JOIN Clients AS CL ON CL.ID = C.ClientID
INNER JOIN Workspace AS W ON W.ID = C.WorkSpaceID
INNER JOIN vContractList AS CLS  ON CLS.ContractID = C.id
WHERE C.id = @ContractID
GROUP BY 
     C.id,
	 CLS.BusinessUnit,
     ISNULL(C.InternalCode, ''), 
     ISNULL(CL.Name, ''), 
     ISNULL(W.Name, ''), 
     ISNULL(U.full_name, ''), 
     ISNULL(FORMAT(C.ValidityStartDate, 'dd/MM/yyyy'), ''), 
     ISNULL(FORMAT(C.ValidityEndDate, 'dd/MM/yyyy'), ''), 
     ISNULL(TAP_INFO.Local, ''), 
     ISNULL(CAST(TAP_INFO.ConsultantID AS VARCHAR), ''), 
     ISNULL(RL.Name, ''), 
     ISNULL(TAP_SCOPE.Scope, ''), 
     ISNULL(TAP_SCOPE.Goal, ''), 
     ISNULL(TAP_SCOPE.GeneralRisks, ''), 
     ISNULL(TAP_SCOPE.Premises, ''), 
     ISNULL(TAP_SCOPE.OutScope, ''), 
     ISNULL(TAP_SCOPE.Deliveries, '');
END;
GO

