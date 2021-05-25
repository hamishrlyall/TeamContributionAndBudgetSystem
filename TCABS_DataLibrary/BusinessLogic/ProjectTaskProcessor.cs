using Dapper;
using System.Collections.Generic;
using System.Data;
using TCABS_DataLibrary.DataAccess;
using TCABS_DataLibrary.Models;

namespace TCABS_DataLibrary.BusinessLogic
{
    public static class ProjectTaskProcessor
    {
        /// <summary>
        /// Returns a list of all tasks in the database.
        /// </summary>
        public static List<ProjectTaskModel> GetAllProjectTasks()
        {
            return SqlDataAccess.LoadData<ProjectTaskModel>("spGetAllProjectTasks");
        }

        /// <summary>
        /// Returns the details of a single task.
        /// </summary>
        public static ProjectTaskModel GetProjectTask(int projectTaskId)
        {
            // Set query parameters
            var param = new DynamicParameters();
            param.Add("ProjectTaskId", projectTaskId);

            // Execute stored procedure
            using (IDbConnection con = SqlDataAccess.OpenDatabaseConnection())
            {
                return con.QuerySingle<ProjectTaskModel>("spGetProjectTask", param, commandType: CommandType.StoredProcedure);
            }
        }

        /// <summary>
        /// Update the details of a task.
        /// </summary>
        public static void UpdateProjectTask(int projectTaskId, string description, int projectRoleId, int duration)
        {
            // Set query parameters
            var param = new DynamicParameters();
            param.Add("ProjectTaskId", projectTaskId);
            param.Add("Description", description);
            param.Add("ProjectRoleId", projectRoleId);
            param.Add("Duration", duration);

            // Execute stored procedure
            using (IDbConnection con = SqlDataAccess.OpenDatabaseConnection())
            {
                con.Execute("spUpdateProjectTask", param, commandType: CommandType.StoredProcedure);
            }
        }

        /// <summary>
        /// Create a new task.
        /// </summary>
        public static void CreateProjectTask(int enrollmentId, string description, int projectRoleId, int duration)
        {
            // Set query parameters
            var param = new DynamicParameters();
            param.Add("EnrollmentId", enrollmentId);
            param.Add("Description", description);
            param.Add("ProjectRoleId", projectRoleId);
            param.Add("Duration", duration);

            // Execute stored procedure
            using (IDbConnection con = SqlDataAccess.OpenDatabaseConnection())
            {
                con.Execute("spCreateProjectTask", param, commandType: CommandType.StoredProcedure);
            }
        }

        /// <summary>
        /// Create a new task.
        /// </summary>
        public static void ApproveProjectTask(int projectTaskId)
        {
            // Set query parameters
            var param = new DynamicParameters();
            param.Add("ProjectTaskId", projectTaskId);

            // Execute stored procedure
            using (IDbConnection con = SqlDataAccess.OpenDatabaseConnection())
            {
                con.Execute("spApproveProjectTask", param, commandType: CommandType.StoredProcedure);
            }
        }

    }
}
