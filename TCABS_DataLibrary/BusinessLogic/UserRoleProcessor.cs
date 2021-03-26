using Dapper;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TCABS_DataLibrary.DataAccess;
using TCABS_DataLibrary.Models;

namespace TCABS_DataLibrary.BusinessLogic
{
   public class UserRoleProcessor
   {
      public static string GetConnectionString( string _ConnectionName = "TCABS_DB" )
      {
         return ConfigurationManager.ConnectionStrings[ _ConnectionName ].ConnectionString;
      }
      public static UserRoleModel SelectUserRole( int _Id )
      {
         UserRoleModel data = new UserRoleModel { UserRoleId = _Id };
         string sql = @"SELECT * FROM [dbo].[UserRole]
                         WHERE UserRoleId = @UserRoleId";

         return SqlDataAccess.LoadSingleRecord( sql, data );
      }

      public static int DeleteUserRole( int _Id )
      {
         UserRoleModel data = new UserRoleModel { UserRoleId = _Id };
         string sql = @"DELETE [UserRole] WHERE UserRoleId = @UserRoleId";

         return SqlDataAccess.DeleteRecord( sql, data );
      }

      //public static int CreateUserRole( int _UserId, int _RoleId )
      //{
      //   UserRoleModel data = new UserRoleModel
      //   {
      //      UserId = _UserId,
      //      RoleId = _RoleId,
      //   };

      //   string sql = @"insert into [dbo].[UserRole]( UserId, RoleId )
      //               values ( @UserId, @RoleId );";

      //   return SqlDataAccess.SaveData( sql, data );
      //}

      public static List<UserRoleModel> LoadRoles( )
      {
         string sql = @"select UserRoleId, UserId, RoleId [dbo].[Role]";

         return SqlDataAccess.LoadData<UserRoleModel>( sql );
      }

      public static List<UserRoleModel> LoadRolesForUser( int _Id )
      {
         UserRoleModel data = new UserRoleModel { UserId = _Id };
         string sql = @"select * FROM [dbo].[UserRole] WHERE UserId = @UserId";

         return SqlDataAccess.LoadData<UserRoleModel>( sql, data );
      }

      public static UserRoleModel InsertUserRole( int _UserId, int _RoleId )
      {
         string sql = @"spInsertUserRole";
         try
         {
            using( IDbConnection _Cnn = new SqlConnection( GetConnectionString( ) ) )
            {
               var userRole = _Cnn.Query<UserRoleModel>( sql, new { UserId = _UserId, RoleId = _RoleId, UserRoleId = 0 }, commandType: CommandType.StoredProcedure ).FirstOrDefault( );

               //var userRole = new UserRoleModel
               //{
               //   UserId = _UserId,
               //   RoleId = _RoleId
               //};
               return userRole;
               //return SqlDataAccess.LoadSingleRecord<UserRoleModel>( "SELECT * FROM [dbo].[UserRole] WHERE UserId = @UserId AND RoleId = @RoleId;", userRole );
            }
         }
         catch( Exception _Ex )
         {
            throw _Ex;
         }

      }
   }
}
