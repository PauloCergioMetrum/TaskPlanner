USE [TaskPlanner]
GO

/****** Object:  StoredProcedure [dbo].[GetEquipament]    Script Date: 16/01/2025 11:50:22 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[GetEquipament]
(
    @EquipamentID int  ,
    @StartDate DATE   ,
    @EndDate DATE  
 
) AS
BEGIN
Select
EquipamentName,
ScheduledDate,
ProjectName
from vActivePlans where ScheduledDate  BETWEEN @StartDate AND @EndDate and EquipamentID = @EquipamentID
END
GO

