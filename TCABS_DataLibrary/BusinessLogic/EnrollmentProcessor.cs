using Dapper;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using TCABS_DataLibrary.DataAccess;
using TCABS_DataLibrary.Models;

namespace TCABS_DataLibrary.BusinessLogic
{
   public class EnrollmentProcessor
   {
      public static string GetConnectionString( string _ConnectionName = "TCABS_DB" )
      {
         return ConfigurationManager.ConnectionStrings[ _ConnectionName ].ConnectionString;
      }

      public static int SelectEnrollmentCountForUnitOfferingIdAndUserId( int _UnitOfferingId, int _UserId )
      {
         try
         {
            string sql = "spSelectEnrollmentCountForUnitOfferingIdAndUserId";
            var data = new DynamicParameters( );
            data.Add( "unitofferingid", _UnitOfferingId );
            data.Add( "userid", _UserId );

            return SqlDataAccess.SelectCount( sql, data );
         }
         catch( Exception e )
         {
            SqlDataAccess.TryConvertExceptionMessage( e );
         }
         return 0;
      }
      public static int SelectEnrollmentCountForTeamIdAndUserId( int _TeamId, int _UserId )
      {
         try
         {
            string sql = "spSelectEnrollmentCountForTeamIdAndUserId";
            var data = new DynamicParameters( );
            data.Add( "teamid", _TeamId );
            data.Add( "userid", _UserId );

            return SqlDataAccess.SelectCount( sql, data );
         }
         catch( Exception e )
         {
            SqlDataAccess.TryConvertExceptionMessage( e );
         }
         return 0;
      }

      public static int DeleteEnrollment( int _Id )
      {
         string sql = "spDeleteEnrollment";

         return SqlDataAccess.DeleteRecord( sql, new { EnrollmentId = _Id } );
      }

      public static List<EnrollmentModel> LoadEnrollmentsForUnitOffering( int _Id )
      {
         string sql = "spSelectEnrollmentsForUnitOfferingId";
         var data = new DynamicParameters( );
         data.Add( "unitofferingid", _Id );

         return SqlDataAccess.LoadData<EnrollmentModel>( sql, data );
      }

      public static List<EnrollmentModel> LoadEnrollmentsForTeam( int _Id )
      {
         string sql = "spSelectEnrollmentsForTeamId";
         var data = new DynamicParameters( );
         data.Add( "teamid", _Id );

         return SqlDataAccess.LoadData<EnrollmentModel>( sql, data );
      }

      public static EnrollmentModel InsertEnrollmentModel( int _UnitOfferingId, int _UserId )
      {
         try
         {
            string sql = @"spInsertEnrollment";
            var data = new DynamicParameters( );
            data.Add( "unitofferingid", _UnitOfferingId );
            data.Add( "userid", _UserId );
            data.Add( "enrollmentid", null );

            return SqlDataAccess.ExecuteStoredProcedure<EnrollmentModel>( sql, data );
         }
         catch( Exception e )
         {
            SqlDataAccess.TryConvertExceptionMessage( e );
         }
         
         return null;
      }

      public static EnrollmentModel SelectEnrollmentForEnrollmentId( int _Id )
      {
         string sql = "spSelectEnrollmentForEnrollmentId";

         var dynamicData = new DynamicParameters( );
         dynamicData.Add( "enrollmentid", _Id );

         return SqlDataAccess.ExecuteStoredProcedure<EnrollmentModel>( sql, dynamicData );
      }

      public static void UpdateEnrollmentWithTeamId( int _EnrollmentId, int _TeamId )
      {
         try
         {
            using( IDbConnection con = new SqlConnection( GetConnectionString( ) ) )
            {
               string sql = "spUpdateEnrollmentWithTeamId";
               var dynamicData = new DynamicParameters( );
               dynamicData.Add( "enrollmentid", _EnrollmentId );
               dynamicData.Add( "teamid", _TeamId );

               con.Query<UnitModel>( sql, dynamicData, commandType: CommandType.StoredProcedure );
            }

         }
         catch( Exception e)
         {
            SqlDataAccess.TryConvertExceptionMessage( e );
         }
      }
   }
}
