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

      public static List<RoleModel>SelectRoles( )
      {
         //string sql = @"select RoleId, Name from [dbo].[Role]";
         string sql = "spSelectRoles";
         return SqlDataAccess.LoadData<RoleModel>( sql );
      }

      public static RoleModel SelectRole( int _Id )
      {
         RoleModel data = new RoleModel { RoleId = _Id };
         //string sql = @"SELECT * FROM [dbo].[Role]
         //                WHERE RoleId = @RoleId";
         string sql = "spSelectRoleForRoleId";

         var dynamicData = new DynamicParameters( );
         dynamicData.Add( "roleid", _Id );

         return SqlDataAccess.ExecuteStoredProcedure<RoleModel>( sql, dynamicData );
      }

      public static RoleModel SelectRoleWithPermissions( int _RoleId )
      {
         //UserModel data = new UserModel { UserId = _Id };

         string sql = "spSelectRoleWithPermissions";

         using( IDbConnection _Cnn = new SqlConnection( GetConnectionString( ) ) )
         {
            //var results = _Cnn.QueryMultiple( @"
            //      SELECT * FROM [dbo].[Role] WHERE RoleId = @RoleId;
            //      SELECT * FROM [dbo].[RolePermission] WHERE RolePermissionId = @RolePermissionId;
            //      ", data );

            var dynamicData = new DynamicParameters( );
            dynamicData.Add( "roleid", _RoleId );

            var results = _Cnn.QueryMultiple( sql, dynamicData, commandType: CommandType.StoredProcedure );

            var role = results.ReadSingle<RoleModel>( );
            var rolePermissions = results.Read<RolePermissionModel>( );
            role.RolePermissions = new List<RolePermissionModel>( );
            role.RolePermissions.AddRange( rolePermissions );

            return role;
         }
      }

      public static List<RolePermissionModel> LoadRolePermissionsForRoleId( int _Id )
      {
         var rolePermissionModel = new RolePermissionModel( ) { RoleId = _Id };

         //string sql1 = @"SELECT r.[PermissionId],
         //               FROM[ Role ] r
         //               LEFT OUTER JOIN[ RolePermission ] rp ON rp.RoleId = r.RoleId
         //               LEFT OUTER JOIN[ Permission ] p ON p.PermissionId = rp.PermissionId
         //               WHERE r.RoleId = @roleid";

         string sql = "spSelectRolePermissionsForRoleId";

         var dynamicData = new DynamicParameters( );
         dynamicData.Add( "roleid", _Id );

         return SqlDataAccess.LoadData<RolePermissionModel>( sql, dynamicData );
      }
   }
}
