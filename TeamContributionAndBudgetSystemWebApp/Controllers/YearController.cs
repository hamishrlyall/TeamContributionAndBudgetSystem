using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TCABS_DataLibrary.BusinessLogic;
using TeamContributionAndBudgetSystemWebApp.Models;
using static TCABS_DataLibrary.BusinessLogic.YearProcessor;

namespace TeamContributionAndBudgetSystemWebApp.Controllers
{
   public class YearController : BaseController
   {
      private TCABS_Db_Context db = new TCABS_Db_Context( );

      /// <summary>
      /// The main page of the year controller
      /// Shows a list of all units in the system
      /// </summary>
      /// <returns></returns>
      public ActionResult Index()
      {
            // Make sure the user is logged in and that they have permission
            if (!IsUserLoggedIn) return RedirectToLogin();
            if (!UserHasPermission(PermissionName.Year)) return RedirectToPermissionDenied();

         // Set the page message
         ViewBag.Message = "Years List";

         // Get all years from the database
         var yearModels = YearProcessor.SelectYears( );

         // Change the format of the year list
         List<Year> years = new List<Year>( );
         foreach( var y in yearModels )
            years.Add( new Year( y ) );

         // Return the view, with the list of years
         return View( years);
      }

      /// <summary>
      /// Navigation method for Year Create page
      /// </summary>
      /// <returns></returns>
      public ActionResult Create( )
      {
            // Make sure the user is logged in and that they have permission
            if (!IsUserLoggedIn) return RedirectToLogin();
            if (!UserHasPermission(PermissionName.Year)) return RedirectToPermissionDenied();

         ViewBag.Message = "Create New Year";

         return View( );
      }

      /// <summary>
      /// POST method for Year Create page
      /// </summary>
      /// <param name="model"></param>
      /// <returns></returns>
      [HttpPost]
      [ValidateAntiForgeryToken]
      public ActionResult Create( Year model )
      {
            // Make sure the user is logged in and that they have permission
            if (!IsUserLoggedIn) return RedirectToLogin();
            if (!UserHasPermission(PermissionName.Year)) return RedirectToPermissionDenied();

         if( ModelState.IsValid )
         {
            try
            {
               // Attempt to Insert Year
               YearProcessor.InsertYear( model.YearValue );

               // If Insert successful return to Index
               return RedirectToAction( "Index" );
            }
            catch( Exception e )
            {
               // Show any datalayer errors
               var errors = ModelState.Values.SelectMany( v => v.Errors );
            }
         }
         else
         {
            // show any modelstate errors
            var errors = ModelState.Values.SelectMany( v => v.Errors );
         }

         // If unsuccessful return to View
         return View( );
      }

      /// <summary>
      /// GET method for Year Delete
      /// </summary>
      /// <param name="id"></param>
      /// <returns></returns>
      [HttpGet]
      public ActionResult Delete( int id )
      {
            // Make sure the user is logged in and that they have permission
            if (!IsUserLoggedIn) return RedirectToLogin();
            if (!UserHasPermission(PermissionName.Year)) return RedirectToPermissionDenied();

         if( id <= 0 )
         {
            // if no id provided return BadRequest error
            return new HttpStatusCodeResult( HttpStatusCode.BadRequest );
         }

         // Attempt to Get Year
         db.GetYear( id );

         // Navigate to View
         return View( db );
      }

      /// <summary>
      /// POST method for Year Delete
      /// </summary>
      /// <param name="year"></param>
      /// <returns></returns>
      [HttpPost]
      [ValidateAntiForgeryToken]
      public ActionResult Delete( Year year )
      {
            // Make sure the user is logged in and that they have permission
            if (!IsUserLoggedIn) return RedirectToLogin();
            if (!UserHasPermission(PermissionName.Year)) return RedirectToPermissionDenied();

         try
         {
            // Ensure no UnitOffering is using given Year
            if( UnitOfferingProcessor.SelectUnitOfferingCountForYear( year.YearId ) > 0 )
               throw new DataException( "Unable to delete Year. One or more Unit Offerings require it." );

            // Attempt to Delete Year
            YearProcessor.DeleteYear( year.YearId );

            // If delete successful return to Index
            return RedirectToAction( "Index" );
         }
         catch( Exception e )
         {
            // Show any errors
            ModelState.AddModelError( "", e.Message );
         }
         // If unsuccessful reload data and return to View.
         db.GetYear( year.YearId );
         return View( db );
      }
   }
}