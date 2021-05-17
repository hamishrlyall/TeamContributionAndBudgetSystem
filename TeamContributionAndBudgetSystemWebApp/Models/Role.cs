using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TeamContributionAndBudgetSystemWebApp.Models
{
   public class Role
   {
      public Role( )
      {
         this.UserRoles = new HashSet<UserRole>( );
         this.RolePermissions = new HashSet<RolePermission>( );
      }

      public int RoleId { get; set; }
      public string Name { get; set; }

      public virtual ICollection<UserRole> UserRoles { get; set; }
      public virtual ICollection<RolePermission> RolePermissions { get; set; }

   }
}