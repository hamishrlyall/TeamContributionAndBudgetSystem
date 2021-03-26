﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TeamContributionAndBudgetSystemWebApp.Models
{
   public class Permission
   {
      public Permission( )
      {
         this.RolePermissions = new HashSet<RolePermission>( );
      }

      public int PermissionId { get; set; }
      public string Name { get; set; }

      public virtual ICollection<RolePermission> RolePermissions { get; set; }
   }
}