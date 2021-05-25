CREATE PROCEDURE [dbo].[spDeleteTeam]
   @teamid int
AS
BEGIN
   DECLARE @teamMembersCount INT
   SELECT @teamMembersCount = COUNT(*) 
   FROM [dbo].[Enrollment]
   WHERE [TeamId] = @teamid

   DELETE [Team]
   WHERE TeamId = @teamid AND
   @teamMembersCount = 0
END;
