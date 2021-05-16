CREATE PROCEDURE [dbo].[spSelectUserForUserId]
   @userid int
AS
BEGIN
   SET NOCOUNT ON;
   SELECT [UserId], [Username], [Firstname], [Lastname], [Email], [PhoneNo], [Password]
   FROM [User]
   WHERE [UserId] = @userid
END;
