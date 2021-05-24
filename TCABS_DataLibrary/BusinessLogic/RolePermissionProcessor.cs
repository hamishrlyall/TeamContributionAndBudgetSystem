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
    public static class RolePermissionProcessor
    {
        public static string GetConnectionString(string _ConnectionName = "TCABS_DB")
        {
            return ConfigurationManager.ConnectionStrings[_ConnectionName].ConnectionString;
        }
        public static int DeleteUserRole(int _PermissionId, int _RoleId)
        {
            string sql = @"spDeleteUserRoles";
            var data = new DynamicParameters();
            data.Add("@param2", _PermissionId);
            data.Add("@param1", _RoleId);
            
            return SqlDataAccess.DeleteRecord(sql, data);

        }
        public static int CreateUserRole(int _PermissionId, int _RoleId)
        {
            string sql = @"sp_createRolePermission";
            var data = new DynamicParameters();
            data.Add("@permissionid", _PermissionId);
            data.Add("@roleid", _RoleId);

            return SqlDataAccess.DeleteRecord(sql, data);

        }
        //public static RolePermissionModel SelectPermission(int _Id)
        //{
        //    PermissionModel data = new PermissionModel { PermissionId = _Id };
        //    string sql = "spSelectPermission";

        //    var dynamicData = new DynamicParameters();
        //    dynamicData.Add("permissionid", _Id);

        //    return SqlDataAccess.ExecuteStoredProcedure<PermissionModel>(sql, dynamicData);
        //}


        //public static List<PermissionModel> SelectPermissions()
        //{
        //    //string sql = @"select RoleId, Name from [dbo].[Role]";
        //    string sql = "spSelectPermissions";
        //    return SqlDataAccess.LoadData<PermissionModel>(sql);
        //}


    }
}
