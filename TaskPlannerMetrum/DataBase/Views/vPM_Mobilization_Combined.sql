USE [TaskPlanner]
GO

/****** Object:  View [dbo].[vPM_Mobilization_Combined]    Script Date: 16/01/2025 11:40:16 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


-- Cria a view
CREATE VIEW [dbo].[vPM_Mobilization_Combined] AS
SELECT 
    P.ID,
    P.CountMobilization,
    P.CountAccommodation,
    P.CountFood,
    P.CountAirTransport,
    P.CountGroundTransport,
    P.CountOthers,
    P.ContractID,
    ISNULL((SELECT SUM(M.CountAccommodation + 
                        M.CountFood + 
                        M.CountAirTransport + 
                        M.CountGroundTransport + 
                        M.CountOthers) 
            FROM [TaskPlanner].[dbo].[PM_Mobilization_Made] M 
            WHERE M.MobilizationPlannedID = P.ID), 0) AS TotalMade,
    ISNULL(SUM(
        P.CountAccommodation + 
        P.CountFood + 
        P.CountAirTransport + 
        P.CountGroundTransport + 
        P.CountOthers), 0) AS TotalPlanned,
    ISNULL(SUM(
        P.CountAccommodation + 
        P.CountFood + 
        P.CountAirTransport + 
        P.CountGroundTransport + 
        P.CountOthers), 0) -
    ISNULL((SELECT SUM(M.CountAccommodation + 
                        M.CountFood + 
                        M.CountAirTransport + 
                        M.CountGroundTransport + 
                        M.CountOthers) 
            FROM [TaskPlanner].[dbo].[PM_Mobilization_Made] M 
            WHERE M.MobilizationPlannedID = P.ID), 0) AS Difference,
    ISNULL(SUM(
        P.CountAccommodation + 
        P.CountFood + 
        P.CountAirTransport + 
        P.CountGroundTransport + 
        P.CountOthers), 0) +
    ISNULL((SELECT SUM(M.CountAccommodation + 
                        M.CountFood + 
                        M.CountAirTransport + 
                        M.CountGroundTransport + 
                        M.CountOthers) 
            FROM [TaskPlanner].[dbo].[PM_Mobilization_Made] M 
            WHERE M.MobilizationPlannedID = P.ID), 0) AS TotalOfEverything
FROM 
    dbo.PM_Mobilization_Planned P
GROUP BY
    P.ID,
    P.CountMobilization,
    P.CountAccommodation,
    P.CountFood,
    P.CountAirTransport,
    P.CountGroundTransport,
    P.CountOthers,
    P.ContractID;
GO

