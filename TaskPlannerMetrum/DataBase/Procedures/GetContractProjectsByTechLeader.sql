USE [TaskPlanner]
GO

/****** Object:  StoredProcedure [dbo].[GetContractProjectsByTechLeader]    Script Date: 16/01/2025 11:50:10 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[GetContractProjectsByTechLeader]
    @TechLeaderID INT,
    @InspectorName NVARCHAR(200)
AS
BEGIN
    IF @TechLeaderID IS NOT NULL
    BEGIN
        SELECT 
            [ClientName],
            [id],
            [InternalCode],
            [ClientID],
            [InspectorName],
            [EnableProject],
            [StartDate],
            COALESCE([DateRetroactive], '1901-01-01 00:00:00') AS DateRetroactive,
            [PlannedMenHour],
            [ExecutedMenHour],
            [Progress],
            [ExpectedHour],
            [Delayed],
            [Status]
        FROM vContractProject 
        WHERE id IN 
        (
            SELECT [contractID]
            FROM [TaskPlanner].[dbo].[DepartmentProjects]
            WHERE TechLeaderID = @TechLeaderID
        )
    END
    ELSE
    BEGIN
        SELECT 
            [ClientName],
            [id],
            [InternalCode],
            [ClientID],
            [InspectorName],
            [EnableProject],
            [StartDate],
            COALESCE([DateRetroactive], '1901-01-01 00:00:00') AS DateRetroactive,
            [PlannedMenHour],
            [ExecutedMenHour],
            [Progress],
            [ExpectedHour],
            [Delayed],
            [Status]
        FROM vContractProject 
        WHERE InspectorName = @InspectorName
    END
END
GO

