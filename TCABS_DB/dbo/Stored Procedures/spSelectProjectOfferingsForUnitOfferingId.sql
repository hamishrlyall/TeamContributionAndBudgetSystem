CREATE PROCEDURE [dbo].[spSelectProjectOfferingsForUnitOfferingId]
   @unitofferingid int
AS
BEGIN
   SELECT * FROM [dbo].[ProjectOffering]
   WHERE UnitOfferingId = @unitofferingid
END;
