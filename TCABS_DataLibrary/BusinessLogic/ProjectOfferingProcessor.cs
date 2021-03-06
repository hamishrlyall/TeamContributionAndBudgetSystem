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
   public class ProjectOfferingProcessor
   {
      public static string GetConnectionString( string _ConnectionName = "TCABS_DB" )
      {
         return ConfigurationManager.ConnectionStrings[ _ConnectionName ].ConnectionString;
      }

      public static List<ProjectOfferingModel> SelectProjectOfferings( )
      {
         string sql = "spSelectProjectOfferings";
         return SqlDataAccess.LoadData<ProjectOfferingModel>( sql );
      }

      public static ProjectOfferingModel SelectProjectOfferingForProjectOfferingId( int ProjectOfferingId )
      {
         string sql = "spSelectProjectOfferingForProjectOfferingId";

         var data = new DynamicParameters( );
         data.Add( "ProjectOfferingId", ProjectOfferingId );

         return SqlDataAccess.ExecuteStoredProcedure<ProjectOfferingModel>( sql, data );
      }

      public static List<ProjectOfferingModel> SelectProjectOfferingsForUnitOfferingId( int _UnitOfferingId )
      {
         string sql = "spSelectProjectOfferingsForUnitOfferingId";
         var dynamicData = new DynamicParameters( );
         dynamicData.Add( "unitofferingId", _UnitOfferingId );

         return SqlDataAccess.LoadData<ProjectOfferingModel>( sql, dynamicData );
      }

      public static ProjectOfferingModel InsertProjectOffering( int _ProjectId, int _UnitOfferingId )
      {
         string sql = @"spInsertProjectOffering";
         var data = new DynamicParameters( );
         data.Add( "projectid", _ProjectId );
         data.Add( "unitofferingid", _UnitOfferingId );
         data.Add( "projectofferingid", null );

         return SqlDataAccess.ExecuteStoredProcedure<ProjectOfferingModel>( sql, data );
      }

      public static int DeleteProjectOffering( int projectOfferingId )
      {
         try
         {
            string sql = "spDeleteProjectOffering";
            return SqlDataAccess.DeleteRecord( sql, new { projectOfferingId = projectOfferingId } );
         }
         catch( Exception E )
         {
            SqlDataAccess.TryConvertExceptionMessage( E );
         }
         return 0;
      }

        /// <summary>
        /// Returns a list of projects which are assigned to a given user, either students through enrollments or conveners through unit offerings.
        /// </summary>
        public static List<ProjectModel> GetProjectOfferingsForUserId(int userId)
        {
            // Set query parameters
            var param = new DynamicParameters();
            param.Add("UserId", userId);

            // Execute stored procedure
            using (IDbConnection con = SqlDataAccess.OpenDatabaseConnection())
            {
                return con.Query<ProjectModel>("spGetProjectOfferingsForUserId", param, commandType: CommandType.StoredProcedure).AsList();
            }
        }

        /// <summary>
        /// Returns a list of projects which are assigned to a given student.
        /// </summary>
        public static List<ProjectModel> GetProjectOfferingsForUserIdOnlyStudents(int userId)
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

    }
}
