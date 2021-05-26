using Dapper;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using TCABS_DataLibrary.DataAccess;
using TCABS_DataLibrary.Models;

namespace TCABS_DataLibrary.BusinessLogic
{
   public class TeamProcessor
   {
      public static string GetConnectionString( string _ConnectionName = "TCABS_DB" )
      {
         return ConfigurationManager.ConnectionStrings[ _ConnectionName ].ConnectionString;
      }

      public static List<TeamModel> SelectTeamsForProjectOfferingId( int _ProjectOfferingId )
      {
         string sql = "spSelectTeamsForProjectOfferingId";
         var dynamicData = new DynamicParameters( );
         dynamicData.Add( "projectofferingid", _ProjectOfferingId );

         return SqlDataAccess.LoadData<TeamModel>( sql, dynamicData );
      }

      public static int DeleteTeam( int _TeamId )
      {
         string sql = "spDeleteTeam";

         return SqlDataAccess.DeleteRecord( sql, new { TeamId = _TeamId } );
      }

      public static TeamModel InsertTeam( int _SupervisorId, int _ProjectOfferingId, string _Name )
      {
         try
         {
            string sql = @"spInsertTeam";
            var data = new DynamicParameters( );
            data.Add( "supervisorid", _SupervisorId );
            data.Add( "projectofferingid", _ProjectOfferingId );
            data.Add( "name", _Name );
            data.Add( "teamid", null );

            return SqlDataAccess.ExecuteStoredProcedure<TeamModel>( sql, data );
         }
         catch( Exception e )
         {
            SqlDataAccess.TryConvertExceptionMessage( e );
         }

         return null;
      }

      public static TeamModel GetTeamForTeamId( int _TeamId )
      {
         string sql = "spSelectTeam";
         var dynamicData = new DynamicParameters( );
         dynamicData.Add( "TeamId", _TeamId );

         return SqlDataAccess.ExecuteStoredProcedure<TeamModel>( sql, dynamicData );
      }

      public static void EditTeam( TeamModel teamModel )
      {
         try
         {
            using( IDbConnection con = new SqlConnection( GetConnectionString( ) ) )
            {
               string sql = "spUpdateTeam";
               var dynamicData = new DynamicParameters( );
               dynamicData.Add( "teamid", teamModel.TeamId );
               dynamicData.Add( "supervisorid", teamModel.SupervisorId );
               dynamicData.Add( "name", teamModel.Name );

               con.Query<TeamModel>( sql, dynamicData, commandType: CommandType.StoredProcedure );
            }
         }
         catch( Exception e )
         {
            SqlDataAccess.TryConvertExceptionMessage( e );
         }
      }

   }
}
