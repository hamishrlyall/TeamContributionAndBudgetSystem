CREATE PROCEDURE [dbo].[spSelectEnrollmentForEnrollmentId]
   @enrollmentid int
AS
BEGIN
   SET NOCOUNT ON;
   SELECT * FROM [Enrollment]
   WHERE [EnrollmentId] = @enrollmentid
END;
