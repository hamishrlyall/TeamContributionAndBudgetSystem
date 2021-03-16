using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TCABS_DataLibrary.Models
{
   public class UserModel
   {
      public int UserId { get; set; }
      public string Username { get; set; }
      public string FirstName { get; set; }
      public string LastName { get; set; }
      public string Email { get; set; }
      public int PhoneNo { get; set; }
      public string Password { get; set; }

      public virtual ICollection<UserRoleModel> UserRoles { get; set; }
   }
}
