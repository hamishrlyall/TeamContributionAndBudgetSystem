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
   public static class PermissionProcessor
   {
      public static PermissionModel SelectPermission( int _Id )
      {
         PermissionModel data = new PermissionModel { PermissionId = _Id };
         string sql = "spSelectPermission";

         var dynamicData = new DynamicParameters( );
         dynamicData.Add( "permissionid", _Id );

         return SqlDataAccess.ExecuteStoredProcedure<PermissionModel>( sql, dynamicData );
      }

        /// <summary>
        /// Retrieve a list of all permissions for particular user, using their username
        /// </summary>
        public static List<PermissionModel> GetPermissionsFromUsername(string username)
        {
            string procedureName = "spGetPermissionsFromUsername";
            var data = new DynamicParameters();
            data.Add("Username", username);
            return SqlDataAccess.LoadData<PermissionModel>(procedureName, data);
        }

        public static List<PermissionModel> SelectPermissions()
        {
            //string sql = @"select RoleId, Name from [dbo].[Role]";
            string sql = "spSelectPermissions";
            return SqlDataAccess.LoadData<PermissionModel>(sql);
        }

    }
}
