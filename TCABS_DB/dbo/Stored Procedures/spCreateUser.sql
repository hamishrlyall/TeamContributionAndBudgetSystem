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

	--BEGIN TRY
		INSERT INTO [dbo].[User] (Username, FirstName, LastName, Email, PhoneNo, [Password], PasswordSalt )
		VALUES ( @Username, @FirstName, @LastName, @Email, @PhoneNo, @Password, @PasswordSalt )
	/*END TRY
	BEGIN CATCH
		DECLARE @errorNo INT;
		SELECT @errorNo = ERROR_NUMBER();
		IF @errorNo = 2627
		BEGIN
			RAISERROR('Username already exists', 10, 1);
		END
		ELSE
			THROW;
	END CATCH*/
END;