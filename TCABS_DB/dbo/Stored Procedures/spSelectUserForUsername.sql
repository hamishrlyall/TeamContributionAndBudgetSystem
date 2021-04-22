CREATE PROCEDURE [dbo].[spSelectUserForUsername]
   @Username NVARCHAR(50)
AS
BEGIN
   SET NOCOUNT ON;
   SELECT [UserId], [Username], [FirstName], [LastName], [Email], [PhoneNo], [Password], [PasswordSalt]
   FROM [User]
   WHERE [Username] = @Username
END;
