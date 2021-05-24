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
   public static class UnitOfferingProcessor
   {
      public static string GetConnectionString( string _ConnectionName = "TCABS_DB" )
      {
         return ConfigurationManager.ConnectionStrings[ _ConnectionName ].ConnectionString;
      }

      public static UnitOfferingModel InsertUnitOffering( int _UnitId, int _TeachingPeriodId, int _YearId, int _ConvenorId )
      {
         string sql = @"spInsertUnitOffering";

         var data = new DynamicParameters( );
         data.Add( "unitid", _UnitId );
         data.Add( "teachingperiodid", _TeachingPeriodId );
         data.Add( "yearid", _YearId );
         data.Add( "convenorid", _ConvenorId );
         data.Add( "unitofferingid", null );

         return SqlDataAccess.ExecuteStoredProcedure<UnitOfferingModel>( sql, data );
      }

      public static List<UnitOfferingModel> SelectUnitOfferings( )
      {
         string sql = "spSelectUnitOfferings";
         return SqlDataAccess.LoadData<UnitOfferingModel>( sql );
      }

      public static UnitOfferingModel SelectUnitOfferingForUnitOfferingId( int _UnitOfferingId )
      {
         string sql = "spSelectUnitOfferingForUnitOfferingId";

         var data = new DynamicParameters( );
         data.Add( "UnitOfferingId", _UnitOfferingId );

         return SqlDataAccess.ExecuteStoredProcedure<UnitOfferingModel>( sql, data );
      }

      public static UnitOfferingModel SelectUnitOfferingWithEnrollments( int _Id )
      {
         string sql = "spSelectUnitOfferingWithEnrollments";
         
         using( IDbConnection _Cnn = new SqlConnection( GetConnectionString( ) ) )
         {
            var results = _Cnn.QueryMultiple( sql, new { UnitOfferingId = _Id }, commandType: CommandType.StoredProcedure );
            var unitOffering = results.ReadSingle<UnitOfferingModel>( );
            var enrollments = results.Read<EnrollmentModel>( );
            unitOffering.Enrollments = new List<EnrollmentModel>( );
            unitOffering.Enrollments.AddRange( enrollments );

            return unitOffering;
         }
      }

      public static UnitOfferingModel SelectUnitOfferingForDetails( string unitName, string teachingPeriodName, int yearValue )
      {
         string sql = "spSelectUnitOfferingForDetails";

         var dynamicData = new DynamicParameters( );
         dynamicData.Add( "unitname", unitName );
         dynamicData.Add( "teachingperiodname", teachingPeriodName );
         dynamicData.Add( "year", yearValue );

         return SqlDataAccess.ExecuteStoredProcedure<UnitOfferingModel>( sql, dynamicData );
      }

      public static int DeleteUnitOffering( int unitOfferingId )
      {
         try
         {
            string sql = "spDeleteUnitOffering";
            return SqlDataAccess.DeleteRecord( sql, new { unitofferingid = unitOfferingId } );
         }
         catch(Exception E )
         {
            SqlDataAccess.TryConvertExceptionMessage( E );
         }
         return 0;
      }

      public static int SelectUnitOfferingCountForYear( int yearId )
      {
         try
         {
            string sql = "spSelectUnitOfferingCountForYear";
            var data = new DynamicParameters( );
            data.Add( "yearid", yearId );

            return SqlDataAccess.SelectCount( sql, data );
         }
         catch( Exception e )
         {
            SqlDataAccess.TryConvertExceptionMessage( e );
         }
         return 0;
      }
      public static int SelectUnitOfferingCountForUnit( int unitId )
      {
         try
         {
            string sql = "spSelectUnitOfferingCountForUnit";
            var data = new DynamicParameters( );
            data.Add( "unitid", unitId );

            return SqlDataAccess.SelectCount( sql, data );
         }
         catch( Exception e )
         {
            SqlDataAccess.TryConvertExceptionMessage( e );
         }
         return 0;
      }
      public static int SelectUnitOfferingCountForTeachingPeriod( int teachingPeriodId )
      {
         try
         {
            string sql = "spSelectUnitOfferingCountForTeachingPeriod";
            var data = new DynamicParameters( );
            data.Add( "teachingperiodid", teachingPeriodId );

            return SqlDataAccess.SelectCount( sql, data );
         }
         catch( Exception e )
         {
            SqlDataAccess.TryConvertExceptionMessage( e );
         }
         return 0;
      }
   }
}
