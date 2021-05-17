using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TCABS_DataLibrary.Models
{
   public class RolePermissionModel
   {
      public int RolePermissionId { get; set; }
      public int RoleId { get; set; }
      public int PermissionId { get; set; }

      public virtual RoleModel Role { get; set; }
      public virtual PermissionModel Permission { get; set; }
   }
}
