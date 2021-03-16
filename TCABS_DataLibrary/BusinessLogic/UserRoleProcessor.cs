using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TCABS_DataLibrary.DataAccess;
using TCABS_DataLibrary.Models;

namespace TCABS_DataLibrary.BusinessLogic
{
   public class UserRoleProcessor
   {
      public static int CreateUserRole( int _UserId, int _RoleId )
      {
         UserRoleModel data = new UserRoleModel
         {
            UserId = _UserId,
            RoleId = _RoleId,
         };

         string sql = @"insert into [dbo].[UserRole]( UserId, RoleId )
                     values ( @UserId, @RoleId );";

         return SqlDataAccess.SaveData( sql, data );
      }

      public static List<UserRoleModel> LoadRoles( )
      {
         string sql = @"select UserRoleId, UserId, RoleId [dbo].[Role]";

         return SqlDataAccess.LoadData<UserRoleModel>( sql );
      }
   }
}
