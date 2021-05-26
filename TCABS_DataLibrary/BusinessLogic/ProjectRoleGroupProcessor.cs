using Dapper;
using System.Collections.Generic;
using System.Data;
using TCABS_DataLibrary.DataAccess;
using TCABS_DataLibrary.Models;

namespace TCABS_DataLibrary.BusinessLogic
{
    public static class ProjectRoleGroupProcessor
    {
        /// <summary>
        /// Returns a list of all project role groups in the database.
        /// </summary>
        public static List<ProjectRoleGroupModel> GetAllProjectRoleGroups()
        {
            return SqlDataAccess.LoadData<ProjectRoleGroupModel>("spGetAllProjectRoleGroups");
        }

        /// <summary>
        /// Returns the details of a single project role group.
        /// </summary>
        public static ProjectRoleGroupModel GetProjectRoleGroup(int projectRoleGroupId)
        {
            // Set query parameters
            var param = new DynamicParameters();
            param.Add("ProjectRoleGroupId", projectRoleGroupId);

            // Execute stored procedure
            using (IDbConnection con = SqlDataAccess.OpenDatabaseConnection())
            {
                return con.QuerySingle<ProjectRoleGroupModel>("spGetProjectRoleGroup", param, commandType: CommandType.StoredProcedure);
            }
        }

        /// <summary>
        /// Update the details of a project role group.
        /// </summary>
        public static void UpdateProjectRoleGroup(int projectRoleGroupId, string name)
        {
            // Set query parameters
            var param = new DynamicParameters();
            param.Add("ProjectRoleGroupId", projectRoleGroupId);
            param.Add("Name", name);

            // Execute stored procedure
            using (IDbConnection con = SqlDataAccess.OpenDatabaseConnection())
            {
                con.Execute("spUpdateProjectRoleGroup", param, commandType: CommandType.StoredProcedure);
            }
        }

        /// <summary>
        /// Delete a project role group from the database.
        /// </summary>
        public static void DeleteProjectRoleGroup(int projectRoleGroupId)
        {
            // Set query parameters
            var param = new DynamicParameters();
            param.Add("ProjectRoleGroupId", projectRoleGroupId);

            // Execute stored procedure
            using (IDbConnection con = SqlDataAccess.OpenDatabaseConnection())
            {
                con.Execute("spDeleteProjectRoleGroup", param, commandType: CommandType.StoredProcedure);
            }
        }

        /// <summary>
        /// Create a new project role group.
        /// </summary>
        public static void CreateProjectRoleGroup(string name)
        {
            // Set query parameters
            var param = new DynamicParameters();
            param.Add("Name", name);

            // Execute stored procedure
            using (IDbConnection con = SqlDataAccess.OpenDatabaseConnection())
            {
                con.Execute("spCreateProjectRoleGroup", param, commandType: CommandType.StoredProcedure);
            }
        }

    }
}
