CREATE PROCEDURE [dbo].[spGetProjectForProjectOfferingId]
   @unitofferingid int
AS
BEGIN
   SELECT * FROM [dbo].[ProjectOffering]
   WHERE ProjectOfferingId = @unitofferingid
END;