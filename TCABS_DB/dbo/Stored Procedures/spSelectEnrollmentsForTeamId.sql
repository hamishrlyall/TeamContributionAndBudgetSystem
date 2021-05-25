CREATE PROCEDURE [dbo].[spSelectEnrollmentsForTeamId]
   @teamid int
AS
BEGIN
   SELECT * FROM [dbo].[Enrollment]
   WHERE [TeamId] = @teamid
END;
