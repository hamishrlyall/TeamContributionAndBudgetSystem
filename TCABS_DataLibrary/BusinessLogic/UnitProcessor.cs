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

         return SqlDataAccess.LoadData<UnitModel>( sql );
      }

      public static UnitModel InsertUnit( string name )
      {
         string sql = @"spInsertUnit";
         var data = new DynamicParameters( );
         data.Add( "name", name );
         data.Add( "unitid", null );

         return SqlDataAccess.ExecuteStoredProcedure<UnitModel>( sql, data );
      }

      public static UnitModel SelectUnitForUnitId( int unitId )
      {
         string sql = "spSelectUnitForUnitId";

         var dynamicData = new DynamicParameters( );
         dynamicData.Add( "UnitId", unitId );

         return SqlDataAccess.ExecuteStoredProcedure<UnitModel>( sql, dynamicData );
      }

      public static UnitModel SelectUnitForName( string name )
      {
         string sql = "spSelectUnitForName";
         var dynamicData = new DynamicParameters( );
         dynamicData.Add( "name", name );

         return SqlDataAccess.ExecuteStoredProcedure<UnitModel>( sql, dynamicData );
      }

      public static int DeleteUnit( int unitId )
      {
         string sql = "spDeleteUnit";

         return SqlDataAccess.DeleteRecord( sql, new { UnitId = unitId } );
      }

      public static void EditUnit( UnitModel unit )
      {
         try
         {
            using( IDbConnection con = new SqlConnection( GetConnectionString( ) ) )
            {
               string sql = "spUpdateUnit";
               var dynamicData = new DynamicParameters( );
               dynamicData.Add( "unitid", unit.UnitId );
               dynamicData.Add( "name", unit.Name );

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
