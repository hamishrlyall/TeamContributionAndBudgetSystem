CREATE PROCEDURE [dbo].[spSelectProjectOfferingForProjectOfferingId]
   @projectOfferingId int
AS
BEGIN
   SELECT * FROM [dbo].[ProjectOffering]
   WHERE ProjectOfferingId = @projectOfferingId
END;
