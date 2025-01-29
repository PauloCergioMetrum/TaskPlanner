USE [TaskPlanner]
GO

/****** Object:  UserDefinedFunction [dbo].[convertDate]    Script Date: 16/01/2025 12:56:52 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE FUNCTION [dbo].[convertDate](
	-- Add the parameters for the function here
	  @Date2Convert nvarchar(100)
)
RETURNS TABLE 
AS
RETURN 
(
SELECT 

    CASE 
        WHEN ISDATE(
            CASE 
                WHEN CHARINDEX(',', @Date2Convert) > 0 THEN SUBSTRING(@Date2Convert, CHARINDEX(',', @Date2Convert) + 2, LEN(@Date2Convert) - CHARINDEX(',', @Date2Convert) - 9)
                WHEN CHARINDEX('T', @Date2Convert) > 0 THEN SUBSTRING(@Date2Convert, 1, 23) -- Considerando o formato "2024-03-29T03:00:00.000Z"
                ELSE NULL
            END
        ) = 1
        THEN CONVERT(DATETIME2, 
            CASE 
                WHEN CHARINDEX(',', @Date2Convert) > 0 THEN SUBSTRING(@Date2Convert, CHARINDEX(',', @Date2Convert) + 2, LEN(@Date2Convert) - CHARINDEX(',', @Date2Convert) - 9)
                WHEN CHARINDEX('T', @Date2Convert) > 0 THEN SUBSTRING(@Date2Convert, 1, 23) -- Considerando o formato "2024-03-29T03:00:00.000Z"
                ELSE NULL
            END, 
            109)
        ELSE NULL
    END AS ConvertedDate
)
GO

