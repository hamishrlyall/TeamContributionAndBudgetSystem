CREATE PROCEDURE [dbo].[spSelectTeamsForProjectOfferingId]
   @projectofferingid int
AS
BEGIN
   SELECT * FROM [dbo].[Team]
   WHERE ProjectOfferingId = @projectofferingid
END;
