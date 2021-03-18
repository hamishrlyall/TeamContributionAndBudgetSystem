using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TCABS_DataLibrary.Models
{
   public class RoleModel
   {
      public int RoleId { get; set; }
      public string Name { get; set; }

      public virtual IQueryable<UserRoleModel> UserRoles { get; set; }
      public virtual IQueryable<RolePermissionModel> RolePermissions { get; set; }
   }
}
