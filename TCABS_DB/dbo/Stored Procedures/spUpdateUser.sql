CREATE PROCEDURE [dbo].[spUpdateUser]
   @UserId INT,
   @Username NVARCHAR(50),
   @FirstName NVARCHAR(50),
   @LastName NVARCHAR(100),
   @Email NVARCHAR(100),
   @PhoneNo INT
AS
BEGIN
   SET NOCOUNT ON;

	UPDATE [dbo].[User]
	SET 
		Username = @Username,
		FirstName = @FirstName,
		LastName = @LastName,
		Email = @Email,
		PhoneNo = @PhoneNo
	WHERE [UserId] = @UserId;
END;
