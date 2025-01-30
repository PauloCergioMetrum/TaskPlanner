USE [TaskPlanner]
GO

/****** Object:  StoredProcedure [dbo].[GetExecutivePVGraphic]    Script Date: 16/01/2025 12:48:11 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE  PROCEDURE [dbo].[GetExecutivePVGraphic]
AS
BEGIN
    SELECT 
        SUM(ValueTotal) AS TotalValue,
      
        COUNT(InternalCode) AS TotalInternalCode
    FROM 
        dbo.vContractList;
END
GO

