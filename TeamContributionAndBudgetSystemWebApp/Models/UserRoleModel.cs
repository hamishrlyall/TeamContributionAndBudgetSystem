using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TCABS_DataLibrary;
using static TCABS_DataLibrary.BusinessLogic.UserRoleProcessor;
using TeamContributionAndBudgetSystemWebApp.Models;

namespace TeamContributionAndBudgetSystemWebApp.Models
{
   public class UserRoleModel
   {

      //public override string[ ] GetRolesForUser( string username )
      //{
      //   using( TCABS_DataLibrary _Context = new TCABS_DataLibrary( ) )
      //   {
      //      var userRoles = ( from user in _Context.Users
      //                        join userRole in _Context.UserRoles
      //                        on user.UserId equals userRole.UserId
      //                        join role in _Context.Roles
      //                        on userRole.RoleId equals role.RoleId
      //                        where user.Username == username
      //                        select role.Name ).ToArray( );
      //      return userRoles;
      //   }
      //}
   }
}