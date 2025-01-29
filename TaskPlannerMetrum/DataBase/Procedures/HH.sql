USE [TaskPlanner]
GO

/****** Object:  StoredProcedure [dbo].[HH]    Script Date: 16/01/2025 12:54:54 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[HH]
    @ContractID int
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @MilestonesData TABLE (
        Name nvarchar(max),
        Description nvarchar(255),
        ID nvarchar(max),
        BusinessUnitID int,
        TypeID int,
        Value float,
        TechLeadID int,
        TechLeaderName nvarchar(255), 
        BusinessUnitName nvarchar(255)
    );

    INSERT INTO @MilestonesData (Name, Description, ID, BusinessUnitID, TypeID, Value, TechLeadID, TechLeaderName, BusinessUnitName)
    SELECT 
        MI.Name, 
        MV.Description, 
        MV.ID, 
        MV.BusinessUnitID, 
        MV.TypeID, 
        MV.Value, 
        MV.TechLeadID,
        u.full_name AS TechLeaderName,
        BU.Name AS BusinessUnitName 
    FROM TaskPlanner.dbo.MilestonesValue MV
    INNER JOIN TaskPlanner.dbo.MilestonesItem MI ON MV.MilestonesID = MI.ID
    LEFT JOIN TaskPlanner.dbo.users u ON MV.TechLeadID = u.id
    INNER JOIN TaskPlanner.dbo.BusinessUnit BU ON MV.BusinessUnitID = BU.ID 
    WHERE MV.ID = @ContractID;

    SELECT Name, Description, ID, BusinessUnitID, TypeID, Value, TechLeadID, TechLeaderName, BusinessUnitName
    FROM @MilestonesData;
END
GO

