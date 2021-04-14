using Dapper;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using TCABS_DataLibrary.Models;

namespace TCABS_DataLibrary.DataAccess
{
   public static class SqlDataAccess
   {
      public static string GetConnectionString( string _ConnectionName = "TCABS_DB" )
      {
         return ConfigurationManager.ConnectionStrings[ _ConnectionName ].ConnectionString;
      }

      public static List<T> LoadData<T>( string _Sql )
      {
         using( IDbConnection _Cnn = new SqlConnection( GetConnectionString( ) ) )
         {
            return _Cnn.Query<T>( _Sql, commandType: CommandType.StoredProcedure ).ToList( );
         }
      }

      public static List<T> LoadData<T>( string _Sql, DynamicParameters _Data )
      {
         using( IDbConnection _Cnn = new SqlConnection( GetConnectionString( ) ) )
         {
            return _Cnn.Query<T>( _Sql, _Data, commandType: CommandType.StoredProcedure ).ToList( );
         }
      }

      public static int DeleteRecord<T>( string _Sql, T _Data )
      {
         using( IDbConnection _Cnn = new SqlConnection( GetConnectionString( ) ) )
         {
            return _Cnn.Execute( _Sql, _Data, commandType: CommandType.StoredProcedure );
         }
      }

      public static T ExecuteStoredProcedure<T>( string _Sql, DynamicParameters _Data )
      {
         using( IDbConnection _Cnn = new SqlConnection( GetConnectionString( ) ) )
         {
            return ( T ) _Cnn.QuerySingle<T>( _Sql, _Data, commandType: CommandType.StoredProcedure );
         }
      }
   }
}
