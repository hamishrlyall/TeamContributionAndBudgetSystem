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
      public virtual List<UserRole> UserRoles { get; set; }
      //public virtual List<UserRole> UserRoles { get; set; }

      public TCABS_Db_Context( )
      {
         // Only load this when viewing users.
         GetUsers( );
      }

      public void GetUser( int id )
      {
         var data = UserProcessor.SelectUserWithRoles( id );
         var user = new User
         {
            UserId = data.UserId,
            Username = data.Username,
            FirstName = data.FirstName,
            LastName = data.LastName,
            EmailAddress = data.Email,
            PhoneNumber = data.PhoneNo,
            Password = data.Password,
         };

         foreach( var userRoleData in data.UserRoles )
         {
            var userRole = new UserRole( );
            userRole.RoleId = userRoleData.RoleId;
            userRole.UserId = userRoleData.UserId;
            userRole.User = user;

            var roleData = RoleProcessor.SelectRoleWithPermissions( userRoleData.RoleId );
            var role = new Role( );
            role.RoleId = roleData.RoleId;
            role.Name = roleData.Name;
            foreach( var rolePermissionData in roleData.RolePermissions )
            {
               var rolePermission = new RolePermission( );
               rolePermission.RoleId = rolePermissionData.RoleId;
               rolePermission.PermissionId = rolePermissionData.RoleId;
               rolePermission.Role = role;

               var permissionData = PermissionProcessor.SelectPermission( rolePermissionData.PermissionId );
               var permission = new Permission( );
               permission.PermissionId = permissionData.PermissionId;
               permission.Name = permissionData.Name;

               rolePermission.Permission = permission;

               role.RolePermissions.Add( rolePermission );
            }
            userRole.Role = role;
            user.UserRoles.Add( userRole );
         }
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
               PhoneNumber = row.PhoneNo,
               Password = row.Password
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
            }
            Users.Add( user );
         }
      }
   }
}