CREATE PROCEDURE [dbo].[spUpdateUser]
   @Username NVARCHAR(50),
   @FirstName NVARCHAR(50),
   @LastName NVARCHAR(100),
   @Email NVARCHAR(100),
   @PhoneNo INT,
   @Password NVARCHAR(50)
AS
BEGIN
   SET NOCOUNT ON;

   UPDATE [dbo].[User] (Username, FirstName, LastName, Email, PhoneNo, [Password] )
   VALUES ( @Username, @FirstName, @LastName, @Email, @PhoneNo, @Password )
END;