CREATE PROCEDURE [dbo].[spUpdatePassword]
   @UserId int,
   @Password NVARCHAR(64),
   @PasswordSalt NVARCHAR(64)
AS
BEGIN
	IF @PasswordSalt IS NULL
		UPDATE [User]
		SET [Password] = @Password
		WHERE [UserId] = @UserId;
	ELSE
		UPDATE [User]
		SET [Password]     = @Password,
			[PasswordSalt] = @PasswordSalt
		WHERE [UserId] = @UserId;
END;
