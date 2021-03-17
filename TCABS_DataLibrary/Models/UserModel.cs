using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TCABS_DataLibrary.DataAccess;

namespace TCABS_DataLibrary.Models
{
   public class UserModel
   {
      public int UserId { get; set; }
      public string Username { get; set; }
      public string FirstName { get; set; }
      public string LastName { get; set; }
      public string Email { get; set; }
      public int PhoneNo { get; set; }
      public string Password { get; set; }

      public virtual ICollection<UserRoleModel> UserRoles { get; set; }

      //public bool Add(UserModel _User )
      //{
      //}

      public UserModel SelectUser( int id )
      {
         using( IDbConnection _Cnn = new SqlConnection( SqlDataAccess.GetConnectionString( ) ) )
         {
            return _Cnn.Query<UserModel>( "SELECT * FROM [dbo].[User] WHERE UserId = @UserId", new UserModel { UserId = id } ).SingleOrDefault( );
         }
      }
   }

   public interface IUserRepository
   {
      List<UserModel> GetAll( );
      bool AddUser( UserModel _User );
      UserModel GetById( int _UserId );
   }
}
