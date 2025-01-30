USE [TaskPlanner]
GO

/****** Object:  StoredProcedure [dbo].[GetActivityPlanByExecutorTeamIDAndPeriod]    Script Date: 16/01/2025 11:47:30 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[GetActivityPlanByExecutorTeamIDAndPeriod]
(
@ExecutorTeamIDs VARCHAR(MAX)  ,
    @StartDate DATE   ,
    @EndDate DATE  ,
    @HoraSchedule FLOAT = null
)
AS
BEGIN
    -- Converte a string de IDs em uma tabela temporária
    DECLARE @ExecutorTeamIDList TABLE (ID INT)
    
    SET @ExecutorTeamIDs = REPLACE(@ExecutorTeamIDs, ' ', '')
    SET @ExecutorTeamIDs = REPLACE(@ExecutorTeamIDs, ',', ';')
    WHILE LEN(@ExecutorTeamIDs) > 0
    BEGIN
        IF CHARINDEX(';', @ExecutorTeamIDs) > 0
        BEGIN
            INSERT INTO @ExecutorTeamIDList (ID) VALUES (CAST(LEFT(@ExecutorTeamIDs, CHARINDEX(';', @ExecutorTeamIDs) - 1) AS INT))
            SET @ExecutorTeamIDs = RIGHT(@ExecutorTeamIDs, LEN(@ExecutorTeamIDs) - CHARINDEX(';', @ExecutorTeamIDs))
        END
        ELSE
        BEGIN
            INSERT INTO @ExecutorTeamIDList (ID) VALUES (CAST(@ExecutorTeamIDs AS INT))
            SET @ExecutorTeamIDs = ''
        END
    END

    -- Seleciona os dados com base na lista de IDs de equipe de executores e período, excluindo fins de semana
     SELECT 
        (SELECT InternalCode FROM Contracts WHERE id = [ContractID]) AS Project,
        [ScheduledDate],
        CONVERT(VARCHAR(5), FLOOR([PlannedManHour])) + ':' +
        RIGHT('0' + CONVERT(VARCHAR(2), ROUND(( [PlannedManHour] - FLOOR([PlannedManHour])) * 60, 0)), 2) AS PlannedManHours,
		(SELECT full_name FROM users WHERE id =(select UserID FROM Team where ID = [ExecutorTeamID])) AS Executor,
        CONVERT(VARCHAR(5), FLOOR(@HoraSchedule + [PlannedManHour])) + ':' +
        RIGHT('0' + CONVERT(VARCHAR(2), ROUND((@HoraSchedule + [PlannedManHour] - FLOOR(@HoraSchedule + [PlannedManHour])) * 60, 0)), 2) AS TotalManHour,
        CASE WHEN @HoraSchedule + [PlannedManHour] > 8 THEN 'true' ELSE 'false' END AS isOverAllocated,
		(Select Name from Clients where ID = (Select ClientID from dbo.Contracts where id = ContractID)) as ClientName 

	FROM [TaskPlanner].[dbo].[ActivityPlan]
	inner join Team on ExecutorTeamID = Team.ID
    WHERE UserID IN (SELECT ID FROM @ExecutorTeamIDList)
    AND ScheduledDate BETWEEN @StartDate AND @EndDate
    AND DATENAME(WEEKDAY, ScheduledDate) NOT IN ('Saturday', 'Sunday')
	
END
GO

