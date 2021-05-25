CREATE PROCEDURE [dbo].[spUpdateEnrollmentWithTeamId]
   @enrollmentid int,
   @teamid int
AS
BEGIN
   UPDATE [dbo].[Enrollment]
   SET [TeamId] = @teamid
   WHERE [EnrollmentId] = @enrollmentid;
END;