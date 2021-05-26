CREATE PROCEDURE [dbo].[spDeleteProjectOffering]
   @projectofferingid int
AS
BEGIN
   DECLARE @teamCount INT
   SELECT @teamCount = COUNT(*) 
   FROM [dbo].[Team]
   WHERE [ProjectOfferingId] = @projectOfferingId

   DELETE FROM [dbo].[ProjectOffering]
   WHERE ProjectId = @projectofferingid 
   AND @teamCount = 0
END;
