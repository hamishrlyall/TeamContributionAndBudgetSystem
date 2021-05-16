CREATE PROCEDURE [dbo].[spUpdateUser]
   @UserId INT,
   @Username NVARCHAR(50),
   @FirstName NVARCHAR(50),
   @LastName NVARCHAR(100),
   @Email NVARCHAR(100),
   @PhoneNo INT,
   @Password NVARCHAR(64),
   @PasswordSalt NVARCHAR(64)
AS
BEGIN
   SET NOCOUNT ON;

	UPDATE [dbo].[User]
	SET 
		Username = @Username,
		FirstName = @FirstName,
		LastName = @LastName,
		Email = @Email,
		PhoneNo = @PhoneNo,
		[Password] = @Password,
		PasswordSalt = @PasswordSalt
	WHERE [UserId] = @UserId;
END;