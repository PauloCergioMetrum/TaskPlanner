USE [TaskPlanner]
GO

/****** Object:  StoredProcedure [dbo].[GetTechLeadersByContract]    Script Date: 16/01/2025 12:54:37 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[GetTechLeadersByContract]
    @ContractID INT
AS
BEGIN
    SELECT 
           T.UserID AS UserID,          
           U.full_name AS Name,         
           D.Name AS DepartmentName      
    FROM [TaskPlanner].[dbo].[MileStonesItem] MI
    INNER JOIN MilestonesValue AS MV ON MV.MilestonesID = MI.ID
    INNER JOIN Team AS T ON T.UserID = MV.TechLeadID 
    INNER JOIN users AS U ON U.id = T.UserID     
    INNER JOIN Department AS D ON D.ID = U.department_id
    WHERE MI.ContractID = @ContractID AND MV.BusinessUnitID <> 0;
END;
GO

