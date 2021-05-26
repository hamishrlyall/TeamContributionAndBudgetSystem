CREATE PROCEDURE [dbo].[spInsertTeam]
   @supervisorid int,
   @projectofferingid int,
   @name NVARCHAR(50),
   @teamid int out
AS
BEGIN
   SET NOCOUNT ON;

   INSERT INTO [dbo].[Team] ([SupervisorId], [ProjectOfferingId], [Name])
   SELECT @supervisorid, @projectofferingid, @name

   SET @teamid = scope_identity();
   SELECT * FROM [dbo].[Team] WHERE [TeamId] = @teamid;
END;