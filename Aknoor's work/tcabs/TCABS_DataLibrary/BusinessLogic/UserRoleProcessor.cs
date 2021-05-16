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
         string sql = "spSelectUserRole";
         var data = new DynamicParameters( );
         data.Add( "userroleid", _Id );

         return SqlDataAccess.ExecuteStoredProcedure<UserRoleModel>( sql, data );
      }

      public static int DeleteUserRole( int _Id )
      {
         UserRoleModel data = new UserRoleModel { UserRoleId = _Id };
         string sql = "spDeleteUserRole";

         return SqlDataAccess.DeleteRecord( sql, data );
      }

      public static List<UserRoleModel> SelectUserRoles( )
      {
         string sql = "spSelectUserRoles";
         return SqlDataAccess.LoadData<UserRoleModel>( sql );
      }

      public static List<UserRoleModel> LoadRolesForUser( int _Id )
      {
         string sql = "spSelectUserRolesForUserId";
         var data = new DynamicParameters( );
         data.Add( "userid", _Id );

         return SqlDataAccess.LoadData<UserRoleModel>( sql, data );
      }

      public static UserRoleModel InsertUserRole( int _UserId, int _RoleId )
      {
         string sql = @"spInsertUserRole";
         var data = new DynamicParameters( );
         data.Add( "userid", _UserId );
         data.Add( "roleid", _RoleId );
         data.Add( "userroleid", null );

         return SqlDataAccess.ExecuteStoredProcedure<UserRoleModel>( sql, data );

      }
   }
}
