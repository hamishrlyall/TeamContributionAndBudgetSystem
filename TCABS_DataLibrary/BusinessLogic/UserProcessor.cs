using Dapper;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
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
      public static string GetConnectionString( string _ConnectionName = "TCABS_DB" )
      {
         return ConfigurationManager.ConnectionStrings[ _ConnectionName ].ConnectionString;
      }
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
         try
         {
            string sql = @"insert into [dbo].[User] (Username, FirstName, LastName, Email, PhoneNo, Password )
                        values ( @Username, @FirstName, @LastName, @Email, @PhoneNo, @Password );";

            return SqlDataAccess.SaveData( sql, data );
         }
         catch( Exception _Ex )
         {
            return 0;
         }
      }

      public static List<UserModel> LoadUsers( )
      {
         string sql = @"select UserId, Username, FirstName, LastName, Email, PhoneNo, Password from [dbo].[User]";

         return SqlDataAccess.LoadData<UserModel>( sql );
      }

      public static UserModel SelectUserWithRoles( int _Id )
      {
         UserModel data = new UserModel { UserId = _Id };

         using( IDbConnection _Cnn = new SqlConnection( GetConnectionString( ) ) )
         {
            var results = _Cnn.QueryMultiple( @"
                  SELECT * FROM [dbo].[User] WHERE UserId = @UserId;
                  SELECT * FROM [dbo].[UserRole] WHERE UserId = @UserId;
                  ", data );
            var user = results.ReadSingle<UserModel>( );
            var userRoles = results.Read<UserRoleModel>( );
            user.UserRoles = new List<UserRoleModel>( );
            user.UserRoles.AddRange( userRoles );
            
            return user;
         }
      }

      public static List<UserRoleModel> LoadUserRolesForUserId( int _Id )
      {

         string sql = @"SELECT r.[RoleId], r.[Name]
                        FROM[ User ] u
                        LEFT OUTER JOIN[ UserRole ] ur ON ur.UserId = u.UserId
                        LEFT OUTER JOIN[ Role ] r ON r.RoleId = ur.RoleId
                        WHERE u.UserId = @userid";

         return SqlDataAccess.LoadData<UserRoleModel>( sql );
      }
   }
}
