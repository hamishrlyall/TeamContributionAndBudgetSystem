CREATE PROCEDURE [dbo].[spSelectTeamsForUnitOfferingId]
   @unitofferingid int
AS
BEGIN
   SELECT * FROM [dbo].[Team]
   WHERE UnitOfferingId = @unitofferingid
END;
