CREATE PROCEDURE [dbo].[spLoadUsers]
AS
BEGIN
   SET NOCOUNT ON;
   SELECT UserId, Username, FirstName, LastName, Email, PhoneNo, [Password] 
   FROM [dbo].[User]
END;
