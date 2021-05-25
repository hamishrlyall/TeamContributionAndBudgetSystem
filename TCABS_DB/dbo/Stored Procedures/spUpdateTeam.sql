CREATE PROCEDURE [dbo].[spUpdateTeam]
   @teamid INT,
   @supervisorid INT,
   @name NVARCHAR(50)
AS
BEGIN
   UPDATE [dbo].[Team]
   SET [Name] = @name, [SupervisorId] = @supervisorid
   WHERE [TeamId] = @teamid;
END;
