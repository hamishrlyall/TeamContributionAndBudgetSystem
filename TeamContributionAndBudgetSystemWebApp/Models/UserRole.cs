using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TCABS_DataLibrary;
using static TCABS_DataLibrary.BusinessLogic.UserRoleProcessor;
using TeamContributionAndBudgetSystemWebApp.Models;

namespace TeamContributionAndBudgetSystemWebApp.Models
{
   public class UserRole
   {
      public int UserRoleId { get; set; }
      public int UserId { get; set; }
      public int RoleId { get; set; }

      public virtual Role Role { get; set; }
      public virtual User User { get; set; }
   }
}