USE [TaskPlanner]
GO

/****** Object:  UserDefinedFunction [dbo].[GetCostByExecutedHours]    Script Date: 16/01/2025 12:57:36 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE FUNCTION [dbo].[GetCostByExecutedHours]
(
    @UserID INT,
	@DateRef DATETIME2,
	@Hours float
)
RETURNS DECIMAL(10, 2)
AS
BEGIN
    DECLARE @TotalCost DECIMAL(10, 2);
    -- Calcular o custo total baseado nos dados das tabelas ActivityPlan e UserHourCosts
    SELECT @TotalCost = SUM(@Hours * HourCost)
    FROM         
        [TaskPlanner].[dbo].[UserHourCosts] WHERE UserID = @UserID AND  @DateRef BETWEEN StartDate AND EndDate;                                              

    RETURN @TotalCost;
END
GO

