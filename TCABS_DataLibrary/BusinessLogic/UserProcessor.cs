using Dapper;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using TCABS_DataLibrary.DataAccess;
using TCABS_DataLibrary.Models;

namespace TCABS_DataLibrary.BusinessLogic
{
   public static class UserProcessor
   {
      public static string GetConnectionString( string _ConnectionName = "TCABS_DB" )
      {
         return ConfigurationManager.ConnectionStrings[ _ConnectionName ].ConnectionString;
      }

        /// <summary>
        /// Update the password of a specific user
        /// </summary>
        public static void UpdatePassword(int userId, string passwordHash, string passwordSalt)
        {
            // Open a connection to the database
            using (IDbConnection con = new SqlConnection(GetConnectionString()))
            {
                // Run the database command
                string sql = "spUpdatePassword";
                con.Query<UserModel>(
                    sql,
                    new
                    {
                        UserId = userId,
                        Password = passwordHash,
                        PasswordSalt = passwordSalt
                    },
                    commandType: CommandType.StoredProcedure);
            }
        }

        /// <summary>
        /// Create a new user record within the database.
        /// </summary>
        public static void CreateUser(string username, string firstName, string lastName, string email, int phoneNo, string password, string passwordSalt)
        {
            try
            {
                // Open a connection to the database
                using (IDbConnection con = new SqlConnection(GetConnectionString()))
                {
                    // Run the database command
                    string sql = "spCreateUser";
                    con.Query<UserModel>(
                        sql,
                        new
                        {
                            Username = username,
                            FirstName = firstName,
                            LastName = lastName,
                            Email = email,
                            PhoneNo = phoneNo,
                            Password = password,
                            PasswordSalt = passwordSalt
                        },
                        commandType: CommandType.StoredProcedure
                    );
                }
            }
            catch(Exception e)
            {
                SqlDataAccess.TryConvertExceptionMessage(e);
            }
        }

        /// <summary>
        /// Create a new user record within the database.
        /// </summary>
        public static void UpdateUser(int userId, string username, string firstName, string lastName, string email, int phoneNo)
        {
            try
            {
                // Open a connection to the database
                using (IDbConnection con = new SqlConnection(GetConnectionString()))
                {
                    // Run the database command
                    string sql = "spUpdateUser";
                    con.Query<UserModel>(
                        sql,
                        new
                        {
                            UserId = userId,
                            Username = username,
                            FirstName = firstName,
                            LastName = lastName,
                            Email = email,
                            PhoneNo = phoneNo
                        },
                        commandType: CommandType.StoredProcedure
                    );
                }
            }
            catch (Exception e)
            {
                SqlDataAccess.TryConvertExceptionMessage(e);
            }
        }

      public static List<UserModel> SelectUsers( )
      {
         string sql = "spSelectUsers";

         return SqlDataAccess.LoadData<UserModel>( sql );
      }

      public static List<UserModel> SelectStudents( )
      {
         string sql = "spSelectStudents";
         return SqlDataAccess.LoadData<UserModel>( sql );
      }

      public static List<UserModel> SelectSupervisors( )
      {
         string sql = "spSelectSupervisors";
         return SqlDataAccess.LoadData<UserModel>( sql );
      }

      public static List<UserModel> SelectConvenors( )
      {
         string sql = "spSelectConvenors";
         return SqlDataAccess.LoadData<UserModel>( sql);
      }

      public static UserModel SelectUserWithRoles( int _Id )
      {
         string sql = "spSelectUserWithRoles";

         using( IDbConnection _Cnn = new SqlConnection( GetConnectionString( ) ) )
         {
            var results = _Cnn.QueryMultiple( sql, new { UserId = _Id }, commandType: CommandType.StoredProcedure );
            var user = results.ReadSingle<UserModel>( );
            var userRoles = results.Read<UserRoleModel>( );
            user.UserRoles = new List<UserRoleModel>( );
            user.UserRoles.AddRange( userRoles );
            
            return user;
         }
      }

      public static UserModel SelectUserForUsername( string _Username )
      {
         string sql = "spSelectUserForUsername";

         var dynamicData = new DynamicParameters( );
         dynamicData.Add( "Username", _Username );

         return SqlDataAccess.ExecuteStoredProcedure<UserModel>( sql, dynamicData );
      }

      public static UserModel SelectUserForUserId(int _UserId)
      {
         string sql = "spSelectUserForUserId";

         var dynamicData = new DynamicParameters();
         dynamicData.Add("UserId", _UserId);

         return SqlDataAccess.ExecuteStoredProcedure<UserModel>(sql, dynamicData);

      }

        /// <summary>
        /// Use an enrollment ID to get the related user data.
        /// </summary>
        public static UserModel SelectUserForEnrollment(int enrollmentId)
        {
            {
                string sql = "spSelectUserFromEnrollment";

                var dynamicData = new DynamicParameters();
                dynamicData.Add("EnrollmentId", enrollmentId);

                return SqlDataAccess.ExecuteStoredProcedure<UserModel>(sql, dynamicData);
            }
        }

        public static List<UserRoleModel> SelectUserRolesForUserId( int _Id )
      {
         string sql = "spSelectUserRolesForUserId";

         return SqlDataAccess.LoadData<UserRoleModel>( sql );
      }

        public static void DeleteUser(int userId)
        {
            try
            {
                // Open a connection to the database
                using (IDbConnection con = new SqlConnection(GetConnectionString()))
                {
                    // Run the database command
                    string sql = "spDeleteUser";
                    con.Query<UserModel>(
                        sql,
                        new
                        {
                            UserId = userId
                        },
                        commandType: CommandType.StoredProcedure
                    );
                }
            }
            catch (Exception e)
            {
                SqlDataAccess.TryConvertExceptionMessage(e);
            }
        }
    }
}
