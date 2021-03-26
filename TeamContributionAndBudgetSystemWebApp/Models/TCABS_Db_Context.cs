using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TCABS_DataLibrary;
using static TCABS_DataLibrary.BusinessLogic.UserProcessor;
using TeamContributionAndBudgetSystemWebApp.Models;
using TCABS_DataLibrary.BusinessLogic;

namespace TeamContributionAndBudgetSystemWebApp.Models
{
   public class TCABS_Db_Context
   {
      public virtual List<User> Users { get; set; }
      public virtual List<Role> Roles { get; set; }
      //public virtual List<UserRole> UserRoles { get; set; }

      public TCABS_Db_Context( )
      {
         // Only load this when viewing users.
         GetUsers( );
      }

      public void GetUsers( )
      {
         var userData = LoadUsers( );
         Users = new List<User>( );
         foreach( var row in userData )
         {
            var user = new User
            {
               UserId = row.UserId,
               Username = row.Username,
               FirstName = row.FirstName,
               LastName = row.LastName,
               EmailAddress = row.Email,
               ConfirmEmailAddress = row.Email,
               PhoneNumber = row.PhoneNo,
            };
            var userRoleModel = UserRoleProcessor.LoadRolesForUser( row.UserId );
            foreach( var ur in userRoleModel )
            {
               var roleModel = RoleProcessor.SelectRole( ur.RoleId );
               var role = new Role
               {
                  RoleId = roleModel.RoleId,
                  Name = roleModel.Name
               };
               var userRole = new UserRole
               {
                  UserId = ur.UserId,
                  RoleId = ur.RoleId,
                  User = user,
                  Role = role
               };
               user.UserRoles.Add( userRole );
            }
            Users.Add( user );
         }
      }
   }
}