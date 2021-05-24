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
   public static class YearProcessor
   {
      public static string GetConnectionString( string _ConnectionName = "TCABS_DB" )
      {
         return ConfigurationManager.ConnectionStrings[ _ConnectionName ].ConnectionString;
      }

      public static List<YearModel> SelectYears( )
      {
         string sql = "spSelectYears";

         return SqlDataAccess.LoadData<YearModel>( sql );
      }

      public static YearModel InsertYear( int year )
      {
         string sql = "spInsertYear";
         var data = new DynamicParameters( );
         data.Add( "year", year );
         data.Add( "yearid", null );

         return SqlDataAccess.ExecuteStoredProcedure<YearModel>( sql, data );
      }

      public static YearModel SelectYearForYearId( int yearId )
      {
         string sql = "spSelectYearForYearId";

         var dynamicData = new DynamicParameters( );
         dynamicData.Add( "YearId", yearId );

         return SqlDataAccess.ExecuteStoredProcedure<YearModel>( sql, dynamicData );
      }

      public static YearModel SelectYearForYearValue( int year )
      {
         string sql = "spSelectYearForYearValue";

         var dynamicData = new DynamicParameters( );
         dynamicData.Add( "year", year );

         return SqlDataAccess.ExecuteStoredProcedure<YearModel>( sql, dynamicData );
      }

      public static int DeleteYear( int yearId )
      {
         string sql = "spDeleteYear";

         return SqlDataAccess.DeleteRecord( sql, new { YearId = yearId } );
      }
   }
}
