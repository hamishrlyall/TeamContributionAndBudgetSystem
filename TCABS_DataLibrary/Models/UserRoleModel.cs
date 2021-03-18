using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TCABS_DataLibrary.Models
{
   public class UserRoleModel
   {
      public int UserRoleId { get; set; }
      public int UserId { get; set; }
      public int RoleId { get; set; }

      public virtual UserRoleModel User { get; set; }
      public virtual RoleModel Role { get; set; }
   }
}
