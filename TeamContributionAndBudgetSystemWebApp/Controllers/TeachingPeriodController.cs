using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using TCABS_DataLibrary.BusinessLogic;
using TCABS_DataLibrary.Models;
using TeamContributionAndBudgetSystemWebApp.Models;
using static TCABS_DataLibrary.BusinessLogic.TeachingPeriodProcessor;

namespace TeamContributionAndBudgetSystemWebApp.Controllers
{
   public class TeachingPeriodController : BaseController
   {
      private TCABS_Db_Context db = new TCABS_Db_Context( );

      /// <summary>
      /// The main page of the TeachingPeriod controller
      /// Shows a list of all TeachingPeriods in the system
      /// </summary>
      /// <returns></returns>
      public ActionResult Index( )
      {
            // Make sure the user is logged in and that they have permission
            if (!IsUserLoggedIn) return RedirectToLogin();
            if (!UserHasPermission(PermissionName.TeachingPeriod)) return RedirectToPermissionDenied();

         //Set the page message
         ViewBag.Message = "Teaching Period List";

         //db.GetTeachingPeriods( );

         // Get all teaching periods from the database
         var teachingPeriodModels = TeachingPeriodProcessor.SelectTeachingPeriods( );

         // Change the format of the year list
         List<TeachingPeriod> teachingPeriods = new List<TeachingPeriod>( );
         foreach( var t in teachingPeriodModels )
            teachingPeriods.Add( new TeachingPeriod( t ) );

         //Return the view, with the list of TeachingPeriods
         return View( teachingPeriods );
      }


      /// <summary>
      /// GET method for TeachingPeriod Edit page.
      /// </summary>
      /// <param name="id"></param>
      /// <returns></returns>
      public ActionResult Edit( int id )
      {
            // Make sure the user is logged in and that they have permission
            if (!IsUserLoggedIn) return RedirectToLogin();
            if (!UserHasPermission(PermissionName.TeachingPeriod)) return RedirectToPermissionDenied();

         if( id <= 0 )
         {
            //If no id supplied return error code
            return new HttpStatusCodeResult( HttpStatusCode.BadRequest );
         }

         // Attempt to GET TeachingPeriod for Id
         db.GetTeachingPeriod( ( int ) id );
         if( db.TeachingPeriod == null )
         {
            // If not Found return error code.
            return HttpNotFound( );
         }
         // Navigate to View
         return View( db );
      }

      /// <summary>
      /// POST method for TeachingPeriod Edit
      /// </summary>
      /// <param name="teachingPeriod"></param>
      /// <returns></returns>
      [HttpPost]
      [ValidateAntiForgeryToken]
      public ActionResult Edit( TeachingPeriod teachingPeriod )
      {
            // Make sure the user is logged in and that they have permission
            if (!IsUserLoggedIn) return RedirectToLogin();
            if (!UserHasPermission(PermissionName.TeachingPeriod)) return RedirectToPermissionDenied();

         if( ModelState.IsValid )
         {
            try
            {
               // Ensure Month and Day qualify as a valid date within a leap year
               if( !ValidDate( teachingPeriod.Month, teachingPeriod.Day ) )
                  throw new DataException( "Selected Date not valid." );

               // Construct DataLayer Model
               var teachingPeriodModel = new TeachingPeriodModel( );
               teachingPeriodModel.TeachingPeriodId = teachingPeriod.TeachingPeriodId;
               teachingPeriodModel.Name = teachingPeriod.Name;
               teachingPeriodModel.Month = teachingPeriod.Month;
               teachingPeriodModel.Day = teachingPeriod.Day;

               // Attempt to Edit TeachingPeriod
               TeachingPeriodProcessor.EditTeachingPeriod( teachingPeriodModel );

               // If successful return to Index
               return RedirectToAction( "Index" );
            }
            catch( Exception e )
            {
               // Show Valid Date Error or DataLayer Errors
               ModelState.AddModelError( "", e.Message );
            }
         }
         else
         {
            // Show ModelState Errors
            var errors = ModelState.Values.SelectMany( v => v.Errors );
         }

         // Redirects to page where data is reloaded
         db.GetTeachingPeriod( teachingPeriod.TeachingPeriodId );
         return View( db );
      }


      /// <summary>
      /// Navigation method for TeachingPeriod Create page
      /// </summary>
      /// <returns></returns>
      public ActionResult Create( )
      {
            // Make sure the user is logged in and that they have permission
            if (!IsUserLoggedIn) return RedirectToLogin();
            if (!UserHasPermission(PermissionName.TeachingPeriod)) return RedirectToPermissionDenied();

         ViewBag.Message = "Create New Teaching Period";
         
         // Navigate to View
         return View( );
      }

      /// <summary>
      /// POST method for TeachingPeriod Create method.
      ///  Uses ModelState Validation backed by server side validation.
      /// </summary>
      /// <param name="model"></param>
      /// <returns></returns>
      [HttpPost]
      [ValidateAntiForgeryToken]
      public ActionResult Create( TeachingPeriod model )
      {
            // Make sure the user is logged in and that they have permission
            if (!IsUserLoggedIn) return RedirectToLogin();
            if (!UserHasPermission(PermissionName.TeachingPeriod)) return RedirectToPermissionDenied();

         if( ModelState.IsValid )
         {
            try
            {
               // Ensure Month and Day qualify as a valid date within a leap year
               if( !ValidDate( model.Month, model.Day ) )
                  throw new DataException( "Selected Date not valid." );

               // Attempt to Insert TeachingPeriod
               TeachingPeriodProcessor.InsertTeachingPeriod( model.Name, model.Month, model.Day );

               // If Insert successful return to Index
               return RedirectToAction( "Index" );
            }
            catch( Exception e )
            {
               // Show Valid Date Error or DataLayer Errors
               var errors = ModelState.Values.SelectMany( v => v.Errors );
            }
         }
         else
         {
            // Show modelState errors
            var errors = ModelState.Values.SelectMany( v => v.Errors );
         }
         
         // Return to View
         return View( );
      }

      /// <summary>
      /// Helper method to validate any date within a leap year.
      /// </summary>
      /// <param name="month"></param>
      /// <param name="day"></param>
      /// <returns></returns>
      private bool ValidDate( int month, int day )
      {
         if( ( month == 2 && day > 29 ) ||
             ( month == 4 ||
               month == 6 ||
               month == 9 ||
               month == 11 )
               && day > 30 )
            return false;
         else 
            return true;
      }

      /// <summary>
      /// GET method for TeachingPeriod Delete Method
      /// </summary>
      /// <param name="id"></param>
      /// <returns></returns>
      [HttpGet]
      public ActionResult Delete( int id )
      {
            // Make sure the user is logged in and that they have permission
            if (!IsUserLoggedIn) return RedirectToLogin();
            if (!UserHasPermission(PermissionName.TeachingPeriod)) return RedirectToPermissionDenied();

         if( id <= 0 )
         {
            return new HttpStatusCodeResult( HttpStatusCode.BadRequest );
         }

         // Get TeachingPeriod and Navigate to View
         db.GetTeachingPeriod( id );
         return View( db );
      }

      /// <summary>
      /// POST method for TeachingPeriod Delete method
      /// </summary>
      /// <param name="teachingPeriod"></param>
      /// <returns></returns>
      [HttpPost]
      [ValidateAntiForgeryToken]
      public ActionResult Delete( TeachingPeriod teachingPeriod )
      {
            // Make sure the user is logged in and that they have permission
            if (!IsUserLoggedIn) return RedirectToLogin();
            if (!UserHasPermission(PermissionName.TeachingPeriod)) return RedirectToPermissionDenied();

         try
         {
            // Ensure no UnitOfferings are using this TeachingPeriod before attempting delete.
            if( UnitOfferingProcessor.SelectUnitOfferingCountForTeachingPeriod( teachingPeriod.TeachingPeriodId ) > 0 )
               throw new DataException( "Unable to delete Teaching Period. One or more Unit Offerings require it." );

            // Attempt to Delete TeachingPeriod
            TeachingPeriodProcessor.DeleteTeachingPeriod( teachingPeriod.TeachingPeriodId );

            // Return to Index if Delete Successful
            return RedirectToAction( "Index" );
         }
         catch( Exception e )
         {
            //Show error
            ModelState.AddModelError( "", e.Message );
         }
         // If any error return to View
         db.GetTeachingPeriod( teachingPeriod.TeachingPeriodId );
         return View( db );
      }
   }
}