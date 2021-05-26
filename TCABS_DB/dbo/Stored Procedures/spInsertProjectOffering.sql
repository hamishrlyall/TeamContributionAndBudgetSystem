CREATE PROCEDURE [dbo].[spInsertProjectOffering]
   @projectid int,
   @unitofferingid int,
   @projectofferingid int out
AS
BEGIN
   SET NOCOUNT ON;

   INSERT INTO [dbo].[ProjectOffering] ([ProjectId], [UnitOfferingId])
   SELECT @projectid, @unitofferingid
   WHERE NOT EXISTS ( SELECT 1 FROM [dbo].[ProjectOffering] WHERE [ProjectId] = @projectid AND [UnitOfferingId] = @unitofferingid );

   SET @projectofferingid = scope_identity( );
   SELECT * FROM [dbo].[ProjectOffering] WHERE [ProjectOfferingId] = @projectofferingid;
END;