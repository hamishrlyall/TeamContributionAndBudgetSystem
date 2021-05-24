CREATE PROCEDURE [dbo].[spInsertEnrollment]
   @unitofferingid int,
   @userid int,
   @enrollmentid int out
AS
BEGIN
   SET NOCOUNT ON;

   INSERT INTO [dbo].[Enrollment]( [UnitOfferingId], [UserId] )
   SELECT @unitofferingid, @userid
   WHERE NOT EXISTS ( SELECT 1 FROM [dbo].[Enrollment] 
   WHERE [UnitOfferingId] = @unitofferingid AND [UserId] = @userid );

   SET @enrollmentid = scope_identity( );
   SELECT * FROM [dbo].[Enrollment] WHERE [EnrollmentId] = @enrollmentid;

END;