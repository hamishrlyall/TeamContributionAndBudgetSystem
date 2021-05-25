CREATE PROCEDURE [dbo].[spSelectTeam]
   @teamid int
AS
BEGIN
   SELECT * FROM [dbo].[Team]
   WHERE TeamId = @teamid
END;

