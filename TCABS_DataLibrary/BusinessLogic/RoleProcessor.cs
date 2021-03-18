using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TCABS_DataLibrary.DataAccess;
using TCABS_DataLibrary.Models;

namespace TCABS_DataLibrary.BusinessLogic
{
   public class RoleProcessor
   {
      public static List<RoleModel>LoadRoles( )
      {
         string sql = @"select RoleId, Name from [dbo].[Role]";

         return SqlDataAccess.LoadData<RoleModel>( sql );
      }

      public static RoleModel SelectRole( int _Id )
      {
         RoleModel data = new RoleModel { RoleId = _Id };
         string sql = @"SELECT * FROM [dbo].[Role]
                         WHERE RoleId = @RoleId";

         return SqlDataAccess.LoadSingleRecord( sql, data );
      }
   }
}
