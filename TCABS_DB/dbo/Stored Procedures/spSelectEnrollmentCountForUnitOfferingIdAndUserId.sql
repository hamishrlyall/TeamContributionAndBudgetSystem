CREATE PROCEDURE [dbo].[spSelectEnrollmentCountForUnitOfferingIdAndUserId]
   @unitofferingid int,
   @userid int
AS
BEGIN
   SELECT COUNT(*) FROM [Enrollment]
   WHERE [UnitOfferingId] = @unitofferingid AND [UserId] = @userid 
END;
