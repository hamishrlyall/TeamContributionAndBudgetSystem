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
   public class RoleProcessor
   {
      public static string GetConnectionString( string _ConnectionName = "TCABS_DB" )
      {
         return ConfigurationManager.ConnectionStrings[ _ConnectionName ].ConnectionString;
      }

      public static List<RoleModel>LoadRoles( )
      {
         string sql = @"select RoleId, Name from [dbo].[Role]";

         return SqlDataAccess.LoadData<RoleModel>( sql );
      }

      public static RoleModel SelectRole( int _Id )
      {
         RoleModel data = new RoleModel { RoleId = _Id };
         string sql = @"SELECT * FROM [dbo].[Role]
                         WHERE RoleId = @RoleId";

         return SqlDataAccess.LoadSingleRecord( sql, data );
      }

      public static RoleModel SelectRoleWithPermissions( int _Id )
      {
         UserModel data = new UserModel { UserId = _Id };

         using( IDbConnection _Cnn = new SqlConnection( GetConnectionString( ) ) )
         {
            var results = _Cnn.QueryMultiple( @"
                  SELECT * FROM [dbo].[Role] WHERE RoleId = @RoleId;
                  SELECT * FROM [dbo].[RolePermission] WHERE RolePermissionId = @RolePermissionId;
                  ", data );

            var role = results.ReadSingle<RoleModel>( );
            var rolePermissions = results.Read<RolePermissionModel>( );
            role.RolePermissions = new List<RolePermissionModel>( );
            role.RolePermissions.AddRange( rolePermissions );

            return role;
         }
      }

      public static List<RolePermissionModel> LoadRolePermissionsForRoleId( int _Id )
      {
         string sql = @"SELECT r.[PermissionId],
                        FROM[ Role ] r
                        LEFT OUTER JOIN[ RolePermission ] rp ON rp.RoleId = r.RoleId
                        LEFT OUTER JOIN[ Permission ] p ON p.PermissionId = rp.PermissionId
                        WHERE r.RoleId = @roleid";

         return SqlDataAccess.LoadData<RolePermissionModel>( sql );
      }
   }
}
