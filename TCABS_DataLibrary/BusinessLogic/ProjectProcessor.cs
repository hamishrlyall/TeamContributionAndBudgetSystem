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
        /// Returns a list of all project role groups in the database.
        /// </summary>
        public static List<ProjectRoleGroupModel> GetAllProjectRoleGroups()
        {
            return SqlDataAccess.LoadData<ProjectRoleGroupModel>("spGetAllProjectRoleGroups");
        }

        /// <summary>
        /// Returns a list of all project roles in the database.
        /// </summary>
        public static List<ProjectRoleModel> GetAllProjectRoles()
        {
            return SqlDataAccess.LoadData<ProjectRoleModel>("spGetAllProjectRoles");
        }

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

      //public static ProjectModel GetProjectForProjectOfferingId( int projectOfferingId )
      //{
      //   string sql = "spGetProjectForProjectOfferingId";
      //   var data = new DynamicParameters( );
      //   data.Add( "projectofferingid", projectOfferingId );

      //   //Execute stored procedure
      //   using( IDbConnection con = SqlDataAccess.OpenDatabaseConnection( ) )
      //   {
      //      return con.QuerySingle<ProjectModel>( sql, data, commandType: CommandType.StoredProcedure );
      //   }
      //}

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
