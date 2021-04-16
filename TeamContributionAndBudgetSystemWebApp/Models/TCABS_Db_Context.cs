using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TCABS_DataLibrary;
using static TCABS_DataLibrary.BusinessLogic.UserProcessor;
using TeamContributionAndBudgetSystemWebApp.Models;
using TCABS_DataLibrary.BusinessLogic;
using System.Web.Security;
using System.Web.WebPages;

namespace TeamContributionAndBudgetSystemWebApp.Models
{
   public class TCABS_Db_Context
   {
      public virtual User User { get; set; }
      public virtual UserRole UserRole { get; set; }
      public virtual Role Role { get; set; }
      public virtual RolePermission RolePermission { get; set; }
      public virtual Permission Permission { get; set; }
      public virtual List<User> Users { get; set; }
      public virtual List<Role> Roles { get; set; }
      public virtual List<UserRole> UserRoles { get; set; }
      public virtual List<MenuItem> MenuItems { get; set; }

      public TCABS_Db_Context( )
      {
         string username = System.Web.HttpContext.Current.User.Identity.Name;

         GetUsers( );
         if( !string.IsNullOrEmpty( username ) )
         {
            GetMenu( username );
         }
      }

      private void GetMenu( string _Username )
      {
         MenuItems = new List<Models.MenuItem>( );
         var permissions = GetPermissions( _Username );

         MenuItems.Add( new Models.MenuItem( ) { LinkText = "Home", ActionName = "Index", ControllerName = "Home" } );

         foreach( var permission in permissions )
         {
            if( !MenuItems.Any( m => m.LinkText == permission.TableName ) )
               MenuItems.Add( new Models.MenuItem( ) { LinkText = permission.TableName, ActionName = "Index", ControllerName = permission.TableName + "/Index" } );
         }

         MenuItems.Add( new Models.MenuItem( ) { LinkText = "Logout", ActionName = "Logout", ControllerName = "Account" } );
      }

      [Authorize]
      private List<Permission> GetPermissions( string _Username )
      {
         var user = GetUserForUsername( _Username );

         List<Permission> permissions = new List<Permission>( );

         foreach( var userRole in user.UserRoles )
         {
            foreach( var rolePermission in userRole.Role.RolePermissions )
            {
               if( !permissions.Any( p => p.PermissionId == rolePermission.Permission.PermissionId ) )
                  permissions.Add( rolePermission.Permission );
            }
         }

         return permissions;
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
               permission.TableName = permissionData.TableName;
               permission.Action = permissionData.Action;

               rolePermission.Permission = permission;

               role.RolePermissions.Add( rolePermission );
            }
            userRole.Role = role;
            user.UserRoles.Add( userRole );
         }
         User = user;
      }

      public List<Role> GetRoles( )
      {
         var data = RoleProcessor.SelectRoles( );
         Roles = new List<Role>( );

         foreach( var row in data )
         {
            Roles.Add( new Role
            {
               RoleId = row.RoleId,
               Name = row.Name,
            } );
         }

         return Roles;
      }

      public User GetUserForUsername( string _Username )
      {
         var data = UserProcessor.SelectUserForUsername( _Username );
         data = UserProcessor.SelectUserWithRoles( data.UserId );

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
               permission.TableName = permissionData.TableName;
               permission.Action = permissionData.Action;

               rolePermission.Permission = permission;

               role.RolePermissions.Add( rolePermission );
            }
            userRole.Role = role;
            user.UserRoles.Add( userRole );
         }

         return user;
      }

      public void GetUsers( )
      {
         var userData = SelectUsers( );
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