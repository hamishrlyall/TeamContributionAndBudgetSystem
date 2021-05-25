using Dapper;
using System.Collections.Generic;
using System.Data;
using TCABS_DataLibrary.DataAccess;
using TCABS_DataLibrary.Models;

namespace TCABS_DataLibrary.BusinessLogic
{
    public static class ProjectRoleLinkProcessor
    {
        /// <summary>
        /// Returns a list of project role links for a given project role.
        /// </summary>
        public static List<ProjectRoleLinkModel> GetProjectRoleLinksForRole(int projectRoleId)
        {
            // Set query parameters
            var param = new DynamicParameters();
            param.Add("ProjectRoleId", projectRoleId);

            // Execute stored procedure
            using (IDbConnection con = SqlDataAccess.OpenDatabaseConnection())
            {
                return con.Query<ProjectRoleLinkModel>("spGetProjectRoleLinksForRole", param, commandType: CommandType.StoredProcedure).AsList();
            }
        }

        /// <summary>
        /// Returns a list of project role links for a given project role group.
        /// </summary>
        public static List<ProjectRoleLinkModel> GetProjectRoleLinksForGroup(int projectRoleGroupId)
        {
            // Set query parameters
            var param = new DynamicParameters();
            param.Add("ProjectRoleGroupId", projectRoleGroupId);

            // Execute stored procedure
            using (IDbConnection con = SqlDataAccess.OpenDatabaseConnection())
            {
                return con.Query<ProjectRoleLinkModel>("spGetProjectRoleLinksForGroup", param, commandType: CommandType.StoredProcedure).AsList();
            }
        }

        /// <summary>
        /// Returns the details of a single project role link.
        /// </summary>
        public static ProjectRoleLinkModel GetProjectRoleLink(int projectRoleLinkId)
        {
            // Set query parameters
            var param = new DynamicParameters();
            param.Add("ProjectRoleLinkId", projectRoleLinkId);

            // Execute stored procedure
            using (IDbConnection con = SqlDataAccess.OpenDatabaseConnection())
            {
                return con.QuerySingle<ProjectRoleLinkModel>("spGetProjectRoleLink", param, commandType: CommandType.StoredProcedure);
            }
        }

        /// <summary>
        /// Delete a project role link from the database.
        /// </summary>
        public static void DeleteProjectRoleLink(int projectRoleLinkId)
        {
            // Set query parameters
            var param = new DynamicParameters();
            param.Add("ProjectRoleLinkId", projectRoleLinkId);

            // Execute stored procedure
            using (IDbConnection con = SqlDataAccess.OpenDatabaseConnection())
            {
                con.Execute("spDeleteProjectRoleLink", param, commandType: CommandType.StoredProcedure);
            }
        }

        /// <summary>
        /// Create a new project role link.
        /// </summary>
        public static void CreateProjectRoleLink(int projectRoleId, int projectRoleGroupId)
        {
            // Set query parameters
            var param = new DynamicParameters();
            param.Add("ProjectRoleId", projectRoleId);
            param.Add("ProjectRoleGroupId", projectRoleGroupId);

            // Execute stored procedure
            using (IDbConnection con = SqlDataAccess.OpenDatabaseConnection())
            {
                con.Execute("spCreateProjectRoleLink", param, commandType: CommandType.StoredProcedure);
            }
        }

    }
}
