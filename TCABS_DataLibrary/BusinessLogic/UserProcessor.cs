using Dapper;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.InteropServices;
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
      public static int CreateUser( string _Username, string _FirstName, string _LastName, string _Email, int _PhoneNo, string _Password )
      {
         try
         {
            string sql = "spCreateUser";

            using( IDbConnection _Cnn = new SqlConnection( GetConnectionString( ) ) )
            {
               var user = _Cnn.Query<UserModel>
                           (
                              sql,
                              new {
                                 Username = _Username,
                                 FirstName = _FirstName,
                                 LastName = _LastName,
                                 Email = _Email,
                                 PhoneNo = _PhoneNo,
                                 Password = _Password },
                              commandType: CommandType.StoredProcedure
                           );

               return user.Count( );
            }
         }
         catch( Exception _Ex )
         {
            return 0;
         }
      }

      public static List<UserModel> LoadUsers( )
      {
         //string sql = @"select UserId, Username, FirstName, LastName, Email, PhoneNo, Password from [dbo].[User]";

         string sql = "spLoadUsers";

         return SqlDataAccess.LoadData<UserModel>( sql );
      }

      public static UserModel SelectUserWithRoles( int _Id )
      {
         string sql = "spSelectUserWithRoles";

         using( IDbConnection _Cnn = new SqlConnection( GetConnectionString( ) ) )
         {
            var results = _Cnn.QueryMultiple( sql, new { UserId = _Id }, commandType: CommandType.StoredProcedure );
            var user = results.ReadSingle<UserModel>( );
            var userRoles = results.Read<UserRoleModel>( );
            user.UserRoles = new List<UserRoleModel>( );
            user.UserRoles.AddRange( userRoles );
            
            return user;
         }
      }

      public static UserModel SelectUserForUsername( string _Username )
      {
         string sql = "spSelectUserForUsername";

         var dynamicData = new DynamicParameters( );
         dynamicData.Add( "Username", _Username );

         return SqlDataAccess.ExecuteStoredProcedure<UserModel>( sql, dynamicData );

         //using( IDbConnection _Cnn = new SqlConnection( GetConnectionString( ) ) )
         //{
         //   var results = _Cnn.QueryMultiple( sql, new { Username = _Username }, commandType: CommandType.StoredProcedure );
         //   var user = results.ReadSingle<UserModel>( );
         //   var userRoles = results.Read<UserRoleModel>( );
         //   user.UserRoles = new List<UserRoleModel>( );
         //   user.UserRoles.AddRange( userRoles );

         //   return user;
         //}
      }

      public static List<UserRoleModel> LoadUserRolesForUserId( int _Id )
      {
         string sql = "spGetUserRolesByUserId";

         return SqlDataAccess.LoadData<UserRoleModel>( sql );
      }
   }
}
