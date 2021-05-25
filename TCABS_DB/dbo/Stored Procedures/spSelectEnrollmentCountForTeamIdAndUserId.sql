CREATE PROCEDURE [dbo].[spSelectEnrollmentCountForTeamIdAndUserId]
   @teamid int,
   @userid int
AS
BEGIN
   SELECT COUNT(*) FROM [Enrollment]
   WHERE [TeamId] = @teamid AND [UserId] = @userid 
END;