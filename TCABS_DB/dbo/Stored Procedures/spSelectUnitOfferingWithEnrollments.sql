CREATE PROCEDURE [dbo].[spSelectUnitOfferingWithEnrollments]
   @unitofferingid int
AS
BEGIN
   SET NOCOUNT ON;
   SELECT * FROM [UnitOffering]
   WHERE [UnitOfferingId]= @unitofferingid;
   SELECT * FROM [Enrollment]
   WHERE [UnitOfferingId]= @unitofferingid;
END;
