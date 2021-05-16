using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TeamContributionAndBudgetSystemWebApp.Models
{
   public class RolePermission
   {
      public int RolePermissionId { get; set; }
      public int RoleId { get; set; }
      public int PermissionId { get; set; }
      public virtual Role Role { get; set; }
      public virtual Permission Permission { get; set; }
   }
}