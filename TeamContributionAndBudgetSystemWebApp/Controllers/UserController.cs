using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TCABS_DataLibrary;
using static TCABS_DataLibrary.BusinessLogic.UserProcessor;
using TeamContributionAndBudgetSystemWebApp.Models;
using TCABS_DataLibrary.BusinessLogic;
using System.Net;
using System.Xml.Serialization;
using System.Data;
using System.Web.Security;

namespace TeamContributionAndBudgetSystemWebApp.Controllers
{
   // Comment
   public class UserController : Controller
   {
      private TCABS_Db_Context db = new TCABS_Db_Context( );

      private void PopulateRoleDropDownList( )
      {
         var data = RoleProcessor.LoadRoles( );
         List<Role> roles = new List<Role>( );

         foreach( var row in data )
         {
            roles.Add( new Role
            {
               RoleId = row.RoleId,
               Name = row.Name,
            } );
         }
         ViewBag.RoleId = new SelectList( roles, "RoleId", "Name", null );
      }
      // GET: User
      public ActionResult Index( )
      {
         ViewBag.Message = "Users List";
         db.GetUsers( );
         //var data = LoadUsers( );
         //List<User> users = new List<User>( );

         //foreach( var row in data )
         //{
         //   users.Add( new User
         //   {
         //      UserId = row.UserId,
         //      Username = row.Username,
         //      FirstName = row.FirstName,
         //      LastName = row.LastName,
         //      EmailAddress = row.Email,
         //      ConfirmEmailAddress = row.Email,
         //      PhoneNumber = row.PhoneNo,
         //   } );
         //}

         //PopulateRoleDropDownList( );

         return View( db.Users );
      }


      public ActionResult Details(int? id )
      {
         if( id == null )
         {
            return new HttpStatusCodeResult( HttpStatusCode.BadRequest );
         }
         int userId = ( int ) id;
         var data = UserProcessor.SelectUserWithRoles( userId );

         if( data == null )
         {
            return HttpNotFound( );
         }
         User User = new User( );
         User.UserId = data.UserId;
         User.Username = data.Username;
         User.FirstName = data.FirstName;
         User.LastName = data.LastName;
         User.EmailAddress = data.Email;
         User.PhoneNumber = data.PhoneNo;
         foreach( var ur in data.UserRoles )
         {
            var roleData = RoleProcessor.SelectRole( ur.RoleId );
            var userRole = new UserRole( );
            userRole.UserRoleId = ur.UserRoleId;
            userRole.UserId = ur.UserId;
            userRole.RoleId = ur.RoleId;
            userRole.Role = new Role { RoleId = roleData.RoleId, Name = roleData.Name };

            User.UserRoles.Add( userRole );
         }

         PopulateRoleDropDownList( );

         return View( User );
      }

      [HttpPost]
      [ValidateAntiForgeryToken]
      public ActionResult Details( [Bind( Include = "UserId,RoleId" )] User _User, Role _Role, string _Action )
      {
         if( _User == null )
         {
            return new HttpStatusCodeResult( HttpStatusCode.BadRequest );
         }
         try
         {
            // Send Create procedure to DataAccess
            var data = UserRoleProcessor.InsertUserRole( _User.UserId, _Role.RoleId );
            if( data == null )
               throw new DataException("Role added was invalid.");
            var roleData = RoleProcessor.SelectRole( data.RoleId );
            var userRole = new UserRole( );
            userRole.UserRoleId = data.UserRoleId;
            userRole.UserId = data.UserId;
            userRole.RoleId = data.RoleId;
            userRole.Role = new Role { RoleId = roleData.RoleId, Name = roleData.Name };

            _User.UserRoles.Add( userRole );
         }
         catch( DataException _Ex )
         {
            ModelState.AddModelError( "", $"Unable to save changes due to Error: { _Ex.Message }" );
         }

         //Reload User details here.
         var userData = UserProcessor.SelectUserWithRoles( _User.UserId );

         if( userData == null )
         {
            return HttpNotFound( );
         }
         User User = new User( );
         User.UserId = userData.UserId;
         User.Username = userData.Username;
         User.FirstName = userData.FirstName;
         User.LastName = userData.LastName;
         User.EmailAddress = userData.Email;
         User.PhoneNumber = userData.PhoneNo;
         foreach( var ur in userData.UserRoles )
         {
            var roleData = RoleProcessor.SelectRole( ur.RoleId );
            var userRole = new UserRole( );
            userRole.UserRoleId = ur.UserRoleId;
            userRole.UserId = ur.UserId;
            userRole.RoleId = ur.RoleId;
            userRole.Role = new Role { RoleId = roleData.RoleId, Name = roleData.Name };

            User.UserRoles.Add( userRole );
         }
         PopulateRoleDropDownList( );

         return View( User );
      }

      public ActionResult DeleteUserRole( int id )
      {
         if( id < 1 )
         {
            return new HttpStatusCodeResult( HttpStatusCode.BadRequest );
         }
         var userRole = UserRoleProcessor.SelectUserRole( id );

         if( userRole == null )
         {
            return HttpNotFound( );
         }

         var user = UserProcessor.SelectUserWithRoles( userRole.UserId );
         return View( User );
      }

      [HttpDelete]
      [ValidateAntiForgeryToken]
      public ActionResult DeleteUserRole( UserRole _UserRole )
      {
         if( _UserRole == null )
         {
            return new HttpStatusCodeResult( HttpStatusCode.BadRequest );
         }
         var row = UserRoleProcessor.DeleteUserRole( _UserRole.UserId );

         return View( User );
      }
   }
}