CREATE TABLE [dbo].[User]
(
   [UserId] INT IDENTITY(1,1) NOT NULL,
   [Username] NVARCHAR(50) NOT NULL,
   [FirstName] NVARCHAR(100) NOT NULL,
   [LastName] NVARCHAR(100) NOT NULL,
   [Email] NVARCHAR(100) NOT NULL,
   [PhoneNo] INT NOT NULL,
   [Password] NVARCHAR(100) NOT NULL,
   [PasswordSalt] NVARCHAR(100)
   CONSTRAINT PK_UserId PRIMARY KEY (UserId),
   CONSTRAINT UQ_User_Username UNIQUE (Username)
)