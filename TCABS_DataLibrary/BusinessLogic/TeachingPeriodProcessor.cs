using Dapper;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using TCABS_DataLibrary.DataAccess;
using TCABS_DataLibrary.Models;

namespace TCABS_DataLibrary.BusinessLogic
{
   public class TeachingPeriodProcessor
   {
      public static string GetConnectionString( string _ConnectionName = "TCABS_DB" )
      {
         return ConfigurationManager.ConnectionStrings[ _ConnectionName ].ConnectionString;
      }

      public static List<TeachingPeriodModel> SelectTeachingPeriods( )
      {
         string sql = "spSelectTeachingPeriods";

         return SqlDataAccess.LoadData<TeachingPeriodModel>( sql );
      }

      public static TeachingPeriodModel InsertTeachingPeriod( string name, int month, int day )
      {
         string sql = @"spInsertTeachingPeriod";
         var data = new DynamicParameters( );
         data.Add( "name", name );
         data.Add( "month", month );
         data.Add( "day", day );
         data.Add( "teachingperiodid", null );

         return SqlDataAccess.ExecuteStoredProcedure<TeachingPeriodModel>( sql, data );
      }

      public static TeachingPeriodModel SelectTeachingPeriodForTeachingPeriodId( int teachingPeriodId )
      {
         string sql = "spSelectTeachingPeriodForTeachingPeriodId";

         var dynamicData = new DynamicParameters( );
         dynamicData.Add( "TeachingPeriodId", teachingPeriodId );

         return SqlDataAccess.ExecuteStoredProcedure<TeachingPeriodModel>( sql, dynamicData );
      }

      public static TeachingPeriodModel SelectTeachingPeriodForName( string name )
      {
         string sql = "spSelectTeachingPeriodForName";
         var dynamicData = new DynamicParameters( );
         dynamicData.Add( "name", name );

         return SqlDataAccess.ExecuteStoredProcedure<TeachingPeriodModel>( sql, dynamicData );
      }

      public static int DeleteTeachingPeriod( int teachingPeriodId )
      {
         string sql = "spDeleteTeachingPeriod";

         return SqlDataAccess.DeleteRecord( sql, new { TeachingPeriodId = teachingPeriodId } );
      }

      public static void EditTeachingPeriod( TeachingPeriodModel teachingPeriod )
      {
         try
         {
            using( IDbConnection con = new SqlConnection( GetConnectionString( ) ) )
            {
               string sql = "spUpdateTeachingPeriod";
               var dynamicData = new DynamicParameters( );
               dynamicData.Add( "teachingperiodid", teachingPeriod.TeachingPeriodId );
               dynamicData.Add( "month", teachingPeriod.Month );
               dynamicData.Add( "day", teachingPeriod.Day );
               dynamicData.Add( "name", teachingPeriod.Name );

               con.Query<UnitModel>( sql, dynamicData, commandType: CommandType.StoredProcedure );
            }
         }
         catch( Exception e )
         {
            SqlDataAccess.TryConvertExceptionMessage( e );
         }
      }
   }
}
