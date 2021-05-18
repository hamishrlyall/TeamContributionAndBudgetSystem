﻿using System;
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
using System.Reflection;

namespace TeamContributionAndBudgetSystemWebApp.Controllers
{
   [AttributeUsage( AttributeTargets.Method, AllowMultiple = false, Inherited = true )]
   public class MultiButtonAttribute : ActionNameSelectorAttribute
   {
      public string MatchFormKey { get; set; }
      public string MatchFormValue { get; set; }

      public override bool IsValidName( ControllerContext controllerContext, string actionName, MethodInfo methodInfo )
      {
         return controllerContext.HttpContext.Request[ MatchFormKey ] != null &&
            controllerContext.HttpContext.Request[ MatchFormKey ] == MatchFormValue;
      }
   }

   // Comment
   public class UserController : BaseController
   {
      
      private TCABS_Db_Context db = new TCABS_Db_Context( );
      // GET: User
      public ActionResult Index( )
      {
         ViewBag.Message = "Users List";

         db.GetUsers( );

         return View( db );
      }
      /// <summary>
      /// Get method for Details page.
      /// </summary>
      /// <param name="id"> UserId </param>
      /// <returns></returns>
      public ActionResult Details(int? id )
      {
         // null safe check
         if( id == null )
         {
            return new HttpStatusCodeResult( HttpStatusCode.BadRequest );
         }
         try
         {
            int userId = ( int ) id;

            // Retrieves User for id
            db.GetUser( userId );

            if( db.User == null )
               throw new DataException( "Unable to find User" );
         }
         catch( DataException _Ex )
         {
            // Error handling
            ModelState.AddModelError( "", $"Unable navigate to page due to error: { _Ex.Message }" );
            return Redirect( Request.UrlReferrer.ToString( ) );
         }

         PopulateRoleDropDownList( );
         return View( db );
      }

      /// <summary>
      /// This method is called when the user hits the delete button on a UserRole Row on the Details Page.
      /// It is used to remove UserRoles from the database.
      /// </summary>
      /// <param name="_UserRoleId"></param>
      /// The _UserRoleId which will be sent to the database to remove the referenced UserRole row.
      /// <returns>This method will Redirect to the Details view with the UserRole removed.</returns>
      [HttpPost]
      [MultiButton(MatchFormKey="action", MatchFormValue="Delete")]
      [ValidateAntiForgeryToken]
      public ActionResult Delete( int _UserRoleId )
      {
         // Null safe check to prevent crashes.
         if( _UserRoleId <= 0 )
         {
            return new HttpStatusCodeResult( HttpStatusCode.BadRequest );
         }
         try
         {
            // Attempt to delete UserRole from database
            var rowsDeleted = UserRoleProcessor.DeleteUserRole( _UserRoleId );
            // If Delete operation was unsuccessful throw an error.
            if( rowsDeleted <= 0 )
               throw new DataException( "Unable to delete UserRole" );
         }
         catch( DataException _Ex)
         {
            // Error handling
            ModelState.AddModelError( "", $"Unable to save changes due to Error: { _Ex.Message }" );
         }
         // Redirects to page where data is reloaded.
         return Redirect( Request.UrlReferrer.ToString( ) );
      }

      /// <summary>
      /// This method is called when the user hits the Save button on the Details Page.
      /// It is used to add new UserRoles to the database.
      /// </summary>
      /// <param name="_UserRole"></param>
      /// The _UserRole parameter will contain the User data and the RoleId which will be sent to the database to insert a new UserRole row.
      /// <returns> This method will return the view with either the newly added UserRole or an error message.</returns>
      [HttpPost]
      [MultiButton( MatchFormKey = "action", MatchFormValue = "Save")]
      [ValidateAntiForgeryToken]
      public ActionResult Save( UserRole _UserRole )
      {
         // Null safe check to prevent crashes.
         if( _UserRole.User == null )
         {
            return new HttpStatusCodeResult( HttpStatusCode.BadRequest );
         }
         try
         {
            // Attempt to insert new UserRole to database using data from parameter
            var data = UserRoleProcessor.InsertUserRole( _UserRole.User.UserId, _UserRole.RoleId );
            // Checks if Insert operation was successful. If not throws an error.
            if( data == null )
               throw new DataException( "Role added was invalid." );
         }
         catch( DataException _Ex )
         {
            // Error handling
            ModelState.AddModelError( "", $"Unable to save changes due to Error: { _Ex.Message }" );
         }

         // Redirects to page where data is reloaded.
         return Redirect( Request.UrlReferrer.ToString( ) );
      }

      /// <summary>
      /// Used to add the List of available Roles to the ViewBag.
      /// </summary>
      private void PopulateRoleDropDownList( )
      {
         ViewBag.RoleId = new SelectList( db.GetRoles( ), "RoleId", "Name", null );
      }

      /// <summary>
      /// Called when a GET request is made for the create user page.
      /// </summary>
      public ActionResult Create()
      {
         ViewBag.Message = "Create New User";

         return View();
      }

      /// <summary>
      /// Called when a POST request is made by the create user page.
      /// </summary>
      [HttpPost]
      [ValidateAntiForgeryToken]
      public ActionResult Create(User model)
      {
         // Make sure the entered data is valid
         if (ModelState.IsValid)
         {
               // Generate a password salt
               // Hash the password
               string passwordSalt = UserLogin.CreatePasswordSalt();
               string password = UserLogin.HashPassword(model.Password, passwordSalt);

               // Clear password info from model, just in case for security
               model.Password = null;
               model.ConfirmPassword = null;

               // Create the user within the database
               int recordsCreated = CreateUser(
                  model.Username,
                  model.FirstName,
                  model.LastName,
                  model.EmailAddress,
                  model.PhoneNumber,
                  password,
                  passwordSalt);

               // Check for errors
               if (recordsCreated == 1)
                  return RedirectToAction("Index");
               else
                  ModelState.AddModelError("", "Failed to create user"); // TODO: Need to add proper error checking here, to provide useful/detailed responses
         }
         else
         {
               //show error
               var errors = ModelState.Values.SelectMany(v => v.Errors);
         }
         return View();
      }
   }
}