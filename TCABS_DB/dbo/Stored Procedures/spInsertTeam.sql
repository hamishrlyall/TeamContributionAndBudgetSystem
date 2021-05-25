CREATE PROCEDURE [dbo].[spInsertTeam]
   @supervisorid int,
   @unitofferingid int,
   @name NVARCHAR(50),
   @teamid int out
AS
BEGIN
   SET NOCOUNT ON;

   INSERT INTO [dbo].[Team] ([SupervisorId], [UnitOfferingId], [Name])
   SELECT @supervisorid, @unitofferingid, @name

   SET @teamid = scope_identity();
   SELECT * FROM [dbo].[Team] WHERE [TeamId] = @teamid;
END;