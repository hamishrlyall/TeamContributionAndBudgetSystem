CREATE PROCEDURE [dbo].[spSelectEnrollmentsForUnitOfferingId]
   @unitofferingid int
AS
BEGIN
   SELECT * FROM [dbo].[Enrollment]
   WHERE UnitOfferingId = @unitofferingid
END;
