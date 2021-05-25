using Dapper;
using System.Collections.Generic;
using System.Data;
using TCABS_DataLibrary.DataAccess;
using TCABS_DataLibrary.Models;

namespace TCABS_DataLibrary.BusinessLogic
{
    public static class ProjectProcessor
    {
        /// <summary>
        /// Returns a list of all projects in the database.
        /// </summary>
        public static List<ProjectModel> GetAllProjects()
        {
            return SqlDataAccess.LoadData<ProjectModel>("spGetAllProjects");
        }

        /// <summary>
        /// Returns the details of a single project.
        /// </summary>
        public static ProjectModel GetProject(int projectId)
        {
            // Set query parameters
            var param = new DynamicParameters();
            param.Add("ProjectId", projectId);

            // Execute stored procedure
            using (IDbConnection con = SqlDataAccess.OpenDatabaseConnection())
            {
                return con.QuerySingle<ProjectModel>("spGetProject", param, commandType: CommandType.StoredProcedure);
            }
        }

        /// <summary>
        /// Returns a list of projects which are assigned to a given user, either students through enrollments or conveners through unit offerings.
        /// </summary>
        public static List<ProjectModel> GetProjectsForUserId(int userId)
        {
            // Set query parameters
            var param = new DynamicParameters();
            param.Add("UserId", userId);

            // Execute stored procedure
            using (IDbConnection con = SqlDataAccess.OpenDatabaseConnection())
            {
                return con.Query<ProjectModel>("spGetProjectsForUserId", param, commandType: CommandType.StoredProcedure).AsList();
            }
        }

        /// <summary>
        /// Returns a list of projects which are assigned to a given student.
        /// </summary>
        public static List<ProjectModel> GetProjectsForUserIdOnlyStudents(int userId)
        {
            // Set query parameters
            var param = new DynamicParameters();
            param.Add("UserId", userId);

            // Execute stored procedure
            using (IDbConnection con = SqlDataAccess.OpenDatabaseConnection())
            {
                return con.Query<ProjectModel>("spGetProjectsForUserIdOnlyStudents", param, commandType: CommandType.StoredProcedure).AsList();
            }
        }

        /// <summary>
        /// Update the details of a project.
        /// </summary>
        public static void UpdateProject(int projectId, string name, string description, int projectRoleGroupId)
        {
            // Set query parameters
            var param = new DynamicParameters();
            param.Add("ProjectId", projectId);
            param.Add("Name", name);
            param.Add("Description", description);
            param.Add("ProjectRoleGroupId", projectRoleGroupId);

            // Execute stored procedure
            using (IDbConnection con = SqlDataAccess.OpenDatabaseConnection())
            {
                con.Execute("spUpdateProject", param, commandType: CommandType.StoredProcedure);
            }
        }

        /// <summary>
        /// Delete a project from the database.
        /// </summary>
        public static void DeleteProject(int projectId)
        {
            // Set query parameters
            var param = new DynamicParameters();
            param.Add("ProjectId", projectId);

            // Execute stored procedure
            using (IDbConnection con = SqlDataAccess.OpenDatabaseConnection())
            {
                con.Execute("spDeleteProject", param, commandType: CommandType.StoredProcedure);
            }
        }

        /// <summary>
        /// Create a new project.
        /// </summary>
        public static void CreateProject(string name, string description, int projectRoleGroupId)
        {
            // Set query parameters
            var param = new DynamicParameters();
            param.Add("Name", name);
            param.Add("Description", description);
            param.Add("ProjectRoleGroupId", projectRoleGroupId);

            // Execute stored procedure
            using (IDbConnection con = SqlDataAccess.OpenDatabaseConnection())
            {
                con.Execute("spCreateProject", param, commandType: CommandType.StoredProcedure);
            }
        }

    }
}
