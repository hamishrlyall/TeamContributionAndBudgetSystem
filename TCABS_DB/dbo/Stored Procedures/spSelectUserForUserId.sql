CREATE PROCEDURE [dbo].[spSelectUserForUserId]
   @userid int
AS
BEGIN
   SET NOCOUNT ON;
   SELECT [UserId], [Username], [FirstName], [LastName], [Email], [PhoneNo], [Password]
   FROM [User]
   WHERE [UserId] = @userid
END;
