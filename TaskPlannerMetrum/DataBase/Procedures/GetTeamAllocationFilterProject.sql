USE [TaskPlanner]
GO

/****** Object:  StoredProcedure [dbo].[GetTeamAllocationFilterProject]    Script Date: 16/01/2025 12:53:23 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[GetTeamAllocationFilterProject]
  
AS
BEGIN
 
    SELECT 
        SalesOrder
      
    FROM 
        [dbo].[GetTeamAllocationTable]
   
END
GO

