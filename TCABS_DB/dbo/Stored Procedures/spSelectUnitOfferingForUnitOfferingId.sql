CREATE PROCEDURE [dbo].[spSelectUnitOfferingForUnitOfferingId]
   @unitofferingid int
AS
BEGIN
   SELECT * FROM [dbo].[UnitOffering]
   WHERE UnitOfferingId = @unitofferingid
END;
