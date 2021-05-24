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
      public static int SelectCount( string _Sql, DynamicParameters _Data )
      {
         using( IDbConnection con = new SqlConnection( GetConnectionString( ) ) )
         {
            return con.ExecuteScalar<int>( _Sql, _Data, commandType: CommandType.StoredProcedure );
         }
      }

      /// <summary>
      /// A shorthand method to open a new database connection.
      /// </summary>
      public static IDbConnection OpenDatabaseConnection( )
      {
         return new System.Data.SqlClient.SqlConnection( SqlDataAccess.GetConnectionString( ) );
      }


      /// <summary>
      /// Try to convert an SQL exception to a more user readable message.
      /// This method will always throw an exception.
      /// </summary>
      public static void TryConvertExceptionMessage(Exception exception)
        {
            // Get the exception message
            string message = exception.Message;

         // Check for violation of unique key constraint
         if (message.StartsWith("Violation of UNIQUE KEY constraint"))
         {
               // Example:
               // Violation of UNIQUE KEY constraint 'UQ_User_Username'. Cannot insert duplicate key in object 'dbo.User'. The duplicate key value is (TestUser1).

               // Locate the unique constraint name
               int indexBegin = message.IndexOf('\'');
               if (indexBegin == -1) throw exception;
               int indexEnd = message.IndexOf('\'', indexBegin + 1);
               if (indexEnd == -1) throw exception;

               // Locate column name within constraint name
               indexBegin = message.LastIndexOf('_', indexEnd);
               if (indexBegin == -1) throw exception;
               string columnName = message.Substring(indexBegin + 1, indexEnd - indexBegin - 1);

               // Locate duplicate value
               indexBegin = message.IndexOf('(');
               if (indexBegin == -1) throw exception;
               indexEnd = message.LastIndexOf(')');
               if (indexEnd == -1) throw exception;
               string value = message.Substring(indexBegin + 1, indexEnd - indexBegin - 1);

               // Throw new exception
               throw new Exception("The value (" + value + ") already exists within column " + columnName + ". Values in this column must be unique.");
         }

         // If here then failed to identify exception type
         // Rethrow the same exception
         throw exception;
      }
   }
}
