using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TCABS_DataLibrary.Models
{
   public class PermissionModel
   {
      public int PermissionId { get; set; }
      public string TableName { get; set; }
      public string Action { get; set; }
        public string DisplayValue { get; set; }

        public virtual IQueryable<RolePermissionModel> RolePermissions { get; set; }
   }
}
