using Dapper;
using System.Collections.Generic;
using System.Data;
using TCABS_DataLibrary.DataAccess;
using TCABS_DataLibrary.Models;

namespace TCABS_DataLibrary.BusinessLogic
{
    public static class ProjectRoleProcessor
    {
        /// <summary>
        /// Returns a list of all project roles in the database.
        /// </summary>
        public static List<ProjectRoleModel> GetAllProjectRoles()
        {
            return SqlDataAccess.LoadData<ProjectRoleModel>("spGetAllProjectRoles");
        }

        /// <summary>
        /// Returns the details of a single project role.
        /// </summary>
        public static ProjectRoleModel GetProjectRole(int projectRoleId)
        {
            // Set query parameters
            var param = new DynamicParameters();
            param.Add("ProjectRoleId", projectRoleId);

            // Execute stored procedure
            using (IDbConnection con = SqlDataAccess.OpenDatabaseConnection())
            {
                return con.QuerySingle<ProjectRoleModel>("spGetProjectRole", param, commandType: CommandType.StoredProcedure);
            }
        }

        /// <summary>
        /// Returns a list of all project roles which are usable by the project, assigned to a given unit offering, assigned to the given enrollment.
        /// </summary>
        public static List<ProjectRoleModel> GetProjectRolesForEnrollment(int enrollmentId)
        {
            // Set query parameters
            var param = new DynamicParameters();
            param.Add("EnrollmentId", enrollmentId);

            // Execute stored procedure
            using (IDbConnection con = SqlDataAccess.OpenDatabaseConnection())
            {
                return con.Query<ProjectRoleModel>("spGetProjectRolesForEnrollment", param, commandType: CommandType.StoredProcedure).AsList();
            }
        }

        /// <summary>
        /// Update the details of a project role.
        /// </summary>
        public static void UpdateProjectRole(int projectRoleId, string name)
        {
            // Set query parameters
            var param = new DynamicParameters();
            param.Add("ProjectRoleId", projectRoleId);
            param.Add("Name", name);

            // Execute stored procedure
            using (IDbConnection con = SqlDataAccess.OpenDatabaseConnection())
            {
                con.Execute("spUpdateProjectRole", param, commandType: CommandType.StoredProcedure);
            }
        }

        /// <summary>
        /// Delete a project role from the database.
        /// </summary>
        public static void DeleteProjectRole(int projectRoleId)
        {
            // Set query parameters
            var param = new DynamicParameters();
            param.Add("ProjectRoleId", projectRoleId);

            // Execute stored procedure
            using (IDbConnection con = SqlDataAccess.OpenDatabaseConnection())
            {
                con.Execute("spDeleteProjectRole", param, commandType: CommandType.StoredProcedure);
            }
        }

        /// <summary>
        /// Create a new project role.
        /// </summary>
        public static void CreateProjectRole(string name)
        {
            // Set query parameters
            var param = new DynamicParameters();
            param.Add("Name", name);

            // Execute stored procedure
            using (IDbConnection con = SqlDataAccess.OpenDatabaseConnection())
            {
                con.Execute("spCreateProjectRole", param, commandType: CommandType.StoredProcedure);
            }
        }

    }
}
