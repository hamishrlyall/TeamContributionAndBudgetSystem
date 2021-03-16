using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using TCABS_DataLibrary.DataAccess;
using TCABS_DataLibrary.Models;

namespace TCABS_DataLibrary.BusinessLogic
{
   public static class UserProcessor
   {
      public static int CreateUser( string _Username, string _FirstName, string _LastName, string _Email, int _PhoneNo )
      {
         UserModel data = new UserModel
         {
            Username = _Username,
            FirstName = _FirstName,
            LastName = _LastName,
            Email = _Email,
            PhoneNo = _PhoneNo
         };

         string sql = @"insert into [dbo].[User] (Username, FirstName, LastName, Email, PhoneNo, Password )
                        values ( @Username, @FirstName, @LastName, @Email, @PhoneNo, @Password );";

         return SqlDataAccess.SaveData( sql, data );
      }

      public static List<UserModel> LoadUsers( )
      {
         string sql = @"select UserId, Username, FirstName, LastName, Email, PhoneNo from [dbo].[User]";

         return SqlDataAccess.LoadData<UserModel>( sql );
      }

      public static UserModel SelectUser( int _Id )
      {
         UserModel data = new UserModel { UserId = _Id };
         string sql = @"@UserId
                        BEGIN
                           SET NOCOUNT ON;
                           SELECT [UserId], [Username], [Firstname], [Lastname], [Email], [PhoneNo]
                           FROM [dbo].[User]
                           WHERE [UserId] = @UserId
                        END;";

         return SqlDataAccess.LoadSingleRecord<UserModel>( sql, data );
      }
   }
}
