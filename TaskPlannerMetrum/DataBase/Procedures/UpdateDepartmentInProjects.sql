USE [TaskPlanner]
GO

/****** Object:  StoredProcedure [dbo].[UpdateDepartmentInProjects]    Script Date: 16/01/2025 12:55:45 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[UpdateDepartmentInProjects] 

AS
BEGIN
 update DepartmentProjects
  set departmentid = (select DepartmentID from Finances f where f.id = FinancesID)
  where DepartmentID <> (select DepartmentID from Finances f where f.id = FinancesID)
END

GO

