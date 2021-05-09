CREATE PROCEDURE [dbo].[spCreateUser]
   @Username NVARCHAR(50),
   @FirstName NVARCHAR(50),
   @LastName NVARCHAR(100),
   @Email NVARCHAR(100),
   @PhoneNo INT,
   @Password NVARCHAR(100),
   @PasswordSalt NVARCHAR(100) = NULL
AS
BEGIN
   SET NOCOUNT ON;

   INSERT INTO [dbo].[User] (Username, FirstName, LastName, Email, PhoneNo, [Password], PasswordSalt )
   VALUES ( @Username, @FirstName, @LastName, @Email, @PhoneNo, @Password, @PasswordSalt )
END;