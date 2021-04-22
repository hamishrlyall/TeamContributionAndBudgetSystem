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

        /// <summary>
        /// Update the password of a specific user
        /// </summary>
        public static void UpdatePassword(int userId, string passwordHash, string passwordSalt)
        {
            using (IDbConnection con = new SqlConnection(GetConnectionString()))
            {
                string sql = "spUpdatePassword";
                con.Query<UserModel>(
                    sql,
                    new
                    {
                        UserId = userId,
                        Password = passwordHash,
                        PasswordSalt = passwordSalt
                    },
                    commandType: CommandType.StoredProcedure);
            }
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

      public static List<UserModel> SelectUsers( )
      {
         //string sql = @"select UserId, Username, FirstName, LastName, Email, PhoneNo, Password from [dbo].[User]";

         string sql = "spSelectUsers";

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

      public static List<UserRoleModel> SelectUserRolesForUserId( int _Id )
      {
         string sql = "spSelectUserRolesForUserId";

         return SqlDataAccess.LoadData<UserRoleModel>( sql );
      }
   }
}
