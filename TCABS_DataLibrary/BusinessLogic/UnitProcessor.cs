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
   public static class UnitProcessor
   {
      public static string GetConnectionString( string _ConnectionName = "TCABS_DB" )
      {
         return ConfigurationManager.ConnectionStrings[ _ConnectionName ].ConnectionString;
      }

      public static List<UnitModel> SelectUnits( )
      {
         string sql = "spSelectUnits";

         return SqlDataAccess.LoadData( sql );
      }

      public static void CreateUnit( string unitName )
      {
         try
         {
            using( IDbConnection con = new SqlConnection( GetConnectionString( ) ) )
            {
               string sql = "spInsertUnit";
               con.Query<UnitModel>( sql = new { UnitName = unitName }, commandType.StoredProcedure );
            }
         }
         catch( Exception e )
         {
            SqlDataAccess.TryConvertExceptionMessage( e );
         }
      }
   }
}
