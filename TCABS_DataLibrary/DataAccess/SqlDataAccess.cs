using Dapper;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TCABS_DataLibrary.DataAccess
{
   // DO NOT TOUCH
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
            return _Cnn.Query<T>( _Sql ).ToList( );
         }
      }

      public static T LoadSingleRecord<T>( string _Sql, T _Data )
      {
         using( IDbConnection _Cnn = new SqlConnection( GetConnectionString( ) ) )
         {
            return ( T )_Cnn.Query( _Sql, _Data );
         }
      }

      public static int SaveData<T>( string _Sql, T _Data )
      {
         using( IDbConnection _Cnn = new SqlConnection( GetConnectionString( ) ) )
         {
            return _Cnn.Execute( _Sql, _Data );
         }
      }
   }
}
