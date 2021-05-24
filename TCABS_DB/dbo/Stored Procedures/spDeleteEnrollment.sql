CREATE PROCEDURE [dbo].[spDeleteEnrollment]
   @enrollmentid int
AS
BEGIN
   DELETE [Enrollment] 
   WHERE EnrollmentId = @enrollmentid
END;
