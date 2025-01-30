USE [TaskPlanner]
GO

/****** Object:  StoredProcedure [dbo].[GetAllContractsGraphic]    Script Date: 16/01/2025 11:47:56 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[GetAllContractsGraphic]
    @ContractID INT = NULL,
    @InternalCode VARCHAR(100) = NULL
AS
BEGIN
    SELECT
        contract.id AS ContractID,
        contract.InternalCode,
        vContract.ClientName
    FROM Contracts AS contract
    INNER JOIN vContractList AS vContract ON contract.id = vContract.ContractID
    WHERE
        contract.InternalCode IS NOT NULL
        AND vContract.ClientName IS NOT NULL
        AND (@ContractID IS NULL OR contract.id = @ContractID)
        AND (@InternalCode IS NULL OR contract.InternalCode = @InternalCode);
END;
GO

