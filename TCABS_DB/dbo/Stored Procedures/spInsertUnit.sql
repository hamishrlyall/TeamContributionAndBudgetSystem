CREATE PROCEDURE [dbo].[spInsertUnit]
   @name NVARCHAR(50),
   @unitid int out
AS
BEGIN
   SET NOCOUNT ON;

   INSERT INTO [dbo].[Unit] ([Name])
   VALUES ( @name )

   SET @unitid = scope_identity( );
   SELECT * FROM [dbo].[Unit] WHERE [UnitId] = @unitid;

END;
