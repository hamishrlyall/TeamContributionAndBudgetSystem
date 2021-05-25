CREATE PROCEDURE [dbo].[spInsertTeam]
   @supervisorid int,
   @projectid int,
   @name NVARCHAR(50),
   @teamid int out
AS
BEGIN
   SET NOCOUNT ON;

   INSERT INTO [dbo].[Team] ([SupervisorId], [ProjectId], [Name])
   SELECT @supervisorid, @projectid, @name

   SET @teamid = scope_identity();
   SELECT * FROM [dbo].[Team] WHERE [TeamId] = @teamid;
END;