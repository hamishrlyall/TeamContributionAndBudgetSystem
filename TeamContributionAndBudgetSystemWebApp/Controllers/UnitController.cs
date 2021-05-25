using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TCABS_DataLibrary.BusinessLogic;
using TCABS_DataLibrary.Models;
using TeamContributionAndBudgetSystemWebApp.Models;
using static TCABS_DataLibrary.BusinessLogic.UnitProcessor;

namespace TeamContributionAndBudgetSystemWebApp.Controllers
{
   public class UnitController : BaseController
   {
      private TCABS_Db_Context db = new TCABS_Db_Context( );
      
      /// <summary>
      /// The main page of the unit controller
      /// Shows a list of all units in the system
      /// </summary>
      /// <returns></returns>
      public ActionResult Index()
      {
         // Make sure the user is logged in and that they have permission
         if( !IsUserLoggedIn ) return RedirectToLogin( );

         // Set the page message
         ViewBag.Message = "Units List";

         // Get all units from the database
         var unitModels = UnitProcessor.SelectUnits( );

         // Change the format of the year list
         List<Unit> units = new List<Unit>( );
         foreach( var u in unitModels )
            units.Add( new Unit( u ) );

         // Return the view, with the list of years
         return View( units );
      }

      /// <summary>
      /// GET method for Unit Edit page.
      /// </summary>
      /// <param name="id"></param>
      /// <returns></returns>
      public ActionResult Edit( int id )
      {
         // Make sure the user is logged in and that they have permission
         if( !IsUserLoggedIn ) 
            return RedirectToLogin( );

         if( id <= 0 )
         {
            //If no id supplied return error code
            return new HttpStatusCodeResult( HttpStatusCode.BadRequest );
         }

         // Attempt to Get Unit for Id
         db.GetUnit( id );
         if( db.Unit == null)
         {
            // If not Found return error code.
            return HttpNotFound( );
         }
         // Navigate to View
         return View( db );
      }


      /// <summary>
      /// POST method for Unit Page.
      /// Uses ModelState Validation backed by server side validation.
      /// </summary>
      /// <param name="unit"></param>
      /// <returns></returns>
      [HttpPost]
      [ValidateAntiForgeryToken]
      public ActionResult Edit( Unit unit )
      {
         // Make sure the user is logged in and that they have permission
         if( !IsUserLoggedIn ) return RedirectToLogin( );

         if( ModelState.IsValid )
         {
            try
            {
               // Construct DataLayer Model
               var unitModel = new UnitModel( );
               unitModel.UnitId = unit.UnitId;
               unitModel.Name = unit.Name;

               // Attempt to Edit Unit
               UnitProcessor.EditUnit( unitModel );

               // If Edit successful return to Index
               return RedirectToAction( "Index" );
            }
            catch( Exception e )
            {
               // Show DataLayer errors
               ModelState.AddModelError( "", e.Message );
            }
         }
         else
         {
            // Show ModelState Errors
            var errors = ModelState.Values.SelectMany( v => v.Errors );
         }

         // Redirects to page where data is reloaded.
         db.GetUnit( unit.UnitId );
         return View( db );
      }

      /// <summary>
      /// Navigation method for Unit Create page
      /// </summary>
      /// <returns></returns>
      public ActionResult Create( )
      {
         // Make sure the user is logged in and that they have permission
         if( !IsUserLoggedIn ) return RedirectToLogin( );

         ViewBag.Message = "Create New Unit";

         // Navigate to View
         return View( );
      }

      /// <summary>
      /// POST method for Unit Create method.
      ///  Uses ModelState Validation backed by server side validation.
      /// </summary>
      /// <param name="model"></param>
      /// <returns></returns>
      [HttpPost]
      [ValidateAntiForgeryToken]
      public ActionResult Create( Unit model )
      {
         // Make sure the user is logged in and that they have permission
         if( !IsUserLoggedIn ) return RedirectToLogin( );

         if( ModelState.IsValid )
         {
            try
            {
               // Attempt to Insert new Unit
               UnitProcessor.InsertUnit( model.Name );

               // If Insert successful return to Index
               return RedirectToAction( "Index" );
            }
            catch( Exception e )
            {
               // Show DataLayer errors
               var errors = ModelState.Values.SelectMany( v => v.Errors );
            }
         }
         else
         {
            // Show ModelState Errors
            var errors = ModelState.Values.SelectMany( v => v.Errors );
         }

         // Return to View
         return View( );
      }

      /// <summary>
      /// GET method for Unit Delete Method
      /// </summary>
      /// <param name="id"></param>
      /// <returns></returns>
      [HttpGet]
      public ActionResult Delete( int id )
      {
         // Make sure the user is logged in and that they have permission
         if( !IsUserLoggedIn ) return RedirectToLogin( );

         if( id <= 0 )
         {
            return new HttpStatusCodeResult( HttpStatusCode.BadRequest );
         }

         // Navigate to View
         db.GetUnit( id );
         return View( db );
      }

      /// <summary>
      /// POST method for Unit Delete method
      /// </summary>
      /// <param name="unit"></param>
      /// <returns></returns>
      [HttpPost]
      [ValidateAntiForgeryToken]
      public ActionResult Delete( Unit unit )
      {
         // Make sure the user is logged in and that they have permission
         if( !IsUserLoggedIn ) return RedirectToLogin( );

         try
         {
            // Ensure no UnitOfferings are using this Unit before attempting delete.
            if( UnitOfferingProcessor.SelectUnitOfferingCountForUnit( unit.UnitId ) > 0 )
               throw new DataException( "Unable to delete Unit. One or more Unit Offerings require it." );

            // Attempt to Delete Unit
            UnitProcessor.DeleteUnit( unit.UnitId );

            // Return to Index if Delete successful.
            return RedirectToAction( "Index" );
         }
         catch( Exception e )
         {
            // Show Error
            ModelState.AddModelError( "", e.Message );
         }

         // If any error return to View
         db.GetUnit( unit.UnitId );
         return View( db );
      }
   }
}