USE [TaskPlanner]
GO

/****** Object:  StoredProcedure [dbo].[GetEquipamentAvaibilaity]    Script Date: 16/01/2025 11:50:52 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[GetEquipamentAvaibilaity]
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
END;
GO

