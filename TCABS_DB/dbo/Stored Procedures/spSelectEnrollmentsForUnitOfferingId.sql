CREATE PROCEDURE [dbo].[spSelectEnrollmentsForUnitOfferingId]
   @unitofferingid int
AS
BEGIN
   SELECT * FROM [dbo].[Enrollments]
   WHERE UnitOfferingId = @unitofferingid
END;
