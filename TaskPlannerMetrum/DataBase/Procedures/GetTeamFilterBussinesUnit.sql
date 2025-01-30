USE [TaskPlanner]
GO

/****** Object:  StoredProcedure [dbo].[GetTeamFilterBussinesUnit]    Script Date: 16/01/2025 12:54:24 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[GetTeamFilterBussinesUnit]
    @SelectedBusinessUnit NVARCHAR(100) = NULL
AS
BEGIN
    SET NOCOUNT ON;

    -- Selecionar as unidades de negócio, filtrando se o parâmetro for fornecido
    SELECT DISTINCT
        AP.[BusinessUnit]
    FROM 
        [dbo].[ActivityPlan] AP
    WHERE 
        @SelectedBusinessUnit IS NULL OR AP.[BusinessUnit] = @SelectedBusinessUnit;
END
GO

