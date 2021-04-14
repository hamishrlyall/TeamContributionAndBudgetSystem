CREATE PROCEDURE [dbo].[spSelectUserForUsername]
   @Username NVARCHAR(50)
AS
BEGIN
   SET NOCOUNT ON;
   SELECT [UserId], [Username], [Firstname], [Lastname], [Email], [PhoneNo], [Password]
   FROM [User]
   WHERE [Username] = @Username
END;
