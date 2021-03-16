CREATE PROCEDURE [dbo].[sp_SelectUserForUserId]
   @userid int
AS
BEGIN
   SET NOCOUNT ON;
   SELECT [UserId], [Username], [Firstname], [Lastname], [Email], [PhoneNo]
   FROM [User]
   WHERE [UserId] = @userid
END;
