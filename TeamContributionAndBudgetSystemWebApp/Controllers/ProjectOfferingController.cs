using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TCABS_DataLibrary.BusinessLogic;
using TeamContributionAndBudgetSystemWebApp.Models;

namespace TeamContributionAndBudgetSystemWebApp.Controllers
{
   public class ProjectOfferingController : BaseController
   {
      /// <summary>
      /// A string to use with TempData[] for project error messages shown on the Index() page.
      /// </summary>
      public const string LabelError = "projectError";

      /// <summary>
      /// A string to use with TempData[] for project success messages shown on the Index() page.
      /// </summary>
      public const string LabelSuccess = "projectSuccess";

      private TCABS_Db_Context db = new TCABS_Db_Context( );

      // GET: ProjectOffering
      public ActionResult Index()
      {
            // Make sure the user is logged in and that they have permission
            if (!IsUserLoggedIn) return RedirectToLogin();
            if (!UserHasPermission(PermissionName.ProjectOffering)) return RedirectToPermissionDenied();

            ViewBag.Message = "ProjectOffering List";

         db.GetProjectOfferings( );
         return View( db.ProjectOfferings );
      }

      public ActionResult Details( int? id )
      {
            // Make sure the user is logged in and that they have permission
            if (!IsUserLoggedIn) return RedirectToLogin();
            if (!UserHasPermission(PermissionName.ProjectOffering)) return RedirectToPermissionDenied();

         if( id == null )
         {
            return new HttpStatusCodeResult( HttpStatusCode.BadRequest );
         }
         try
         {
            int projectOfferingId = ( int ) id;
            var projectOfferingModel = ProjectOfferingProcessor.SelectProjectOfferingForProjectOfferingId( projectOfferingId );
            if( projectOfferingModel == null )
               return RedirectToIndexIdNotFound( projectOfferingId );

            var project = ProjectProcessor.GetProject( projectOfferingModel.ProjectId );

            var unitOffering = UnitOfferingProcessor.SelectUnitOfferingForUnitOfferingId( projectOfferingModel.UnitOfferingId );

            var teams = TeamProcessor.SelectTeamsForProjectOfferingId( projectOfferingModel.ProjectOfferingId );

            var projectOffering = new ProjectOffering( projectOfferingModel, project, unitOffering, teams );

            return View( projectOffering );
         }
         catch( Exception e )
         {
            return RedirectToIndex( e );
         }
      }

      public ActionResult Create( )
      {
            // Make sure the user is logged in and that they have permission
            if (!IsUserLoggedIn) return RedirectToLogin();
            if (!UserHasPermission(PermissionName.ProjectOffering)) return RedirectToPermissionDenied();

         ViewBag.Message = "Create New Unit";

         var projectOffering = new ProjectOffering( );
         // Generate drop down lists for each field.
         ViewBag.UnitofferingId = new SelectList( projectOffering.GetUnitOfferings( ), "UnitOfferingId", "UnitName", null );
         ViewBag.ProjectId = new SelectList( projectOffering.GetProjects( ), "ProjectId", "Name", null );

         // Navigate to View
         return View( );
      }

      [HttpPost]
      [ValidateAntiForgeryToken]
      public ActionResult Create( ProjectOffering model )
      {
            // Make sure the user is logged in and that they have permission
            if (!IsUserLoggedIn) return RedirectToLogin();
            if (!UserHasPermission(PermissionName.ProjectOffering)) return RedirectToPermissionDenied();

         if( ModelState.IsValid )
         {
            try
            {
               //Validate values of Project and UnitOffering
               db.GetProject( model.ProjectId );
               db.GetUnitOffering( model.UnitOfferingId );

               ProjectOfferingProcessor.InsertProjectOffering(
                  model.ProjectId,
                  model.UnitOfferingId );

               // If Insert successful return to index
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
            // Show ModelState errors
            var errors = ModelState.Values.SelectMany( v => v.Errors );
         }
         ViewBag.UnitofferingId = new SelectList( db.GetUnitOfferings( ), "UnitOfferingId", "UnitName", null );
         ViewBag.ProjectId = new SelectList( db.GetProjects( ), "ProjectId", "Name", null );

         // Navigate to View
         return View( db );
      }

      [HttpGet]
      public ActionResult Delete( int? id )
      {
            // Make sure the user is logged in and that they have permission
            if (!IsUserLoggedIn) return RedirectToLogin();
            if (!UserHasPermission(PermissionName.ProjectOffering)) return RedirectToPermissionDenied();

         if( id == null )
         {
            // If no id provided show BadRequest Error
            return new HttpStatusCodeResult( HttpStatusCode.BadRequest );
         }
         int projectOfferingId = ( int ) id;

         var projectOffering = db.GetProjectOffering( projectOfferingId );

         return View( projectOffering );
      }

      [HttpPost]
      [ValidateAntiForgeryToken]
      public ActionResult Delete( ProjectOffering projectOffering )
      {
            // Make sure the user is logged in and that they have permission
            if (!IsUserLoggedIn) return RedirectToLogin();
            if (!UserHasPermission(PermissionName.ProjectOffering)) return RedirectToPermissionDenied();

         try
         {


            if( db.GetProjectOffering( projectOffering.ProjectOfferingId ) == null )
               throw new DataException( "This Project Offering does not exist." );

            var existingTeams = TeamProcessor.SelectTeamsForProjectOfferingId( projectOffering.ProjectOfferingId );
            foreach( var team in existingTeams )
            {
               TeamProcessor.DeleteTeam( team.TeamId );
            }

            int rowsDeleted = ProjectOfferingProcessor.DeleteProjectOffering( projectOffering.ProjectOfferingId );
            if( rowsDeleted <= 0 )
               throw new DataException( "Unable to Delete ProjectOffering." );

            // If delete successful return to Index
            return RedirectToAction( "Index" );
         }
         catch( Exception e )
         {
            // Show any errors
            ModelState.AddModelError( "", e.Message );
         }
         // If unsuccessful return to View
         return View( db );
      }

      /// <summary>
      /// A shorthand method for getting a RedirectToAction() leading to the index page.
      /// </summary>
      private ActionResult RedirectToIndex( )
      {
         return RedirectToAction( "Index" );
      }

      /// <summary>
      /// A shorthand method for getting a RedirectToAction() leading to the index page.
      /// Also displays an error message based on the provided exception.
      /// </summary>
      private ActionResult RedirectToIndex( Exception e )
      {
         TempData[ LabelError ] = e.Message;
         return RedirectToIndex( );
      }

      /// <summary>
      /// A shorthand method for getting a RedirectToAction() leading to the index page.
      /// Also displays an error message saying that the requested project does not exist.
      /// </summary>
      /// <param name="projectOfferingId">The project (ID) which could no be found.</param>
      private ActionResult RedirectToIndexIdNotFound( int projectOfferingId )
      {
         TempData[ LabelError ] = "A Project Offering with ID:" + projectOfferingId + " does not appear to exist";
         return RedirectToIndex( );
      }
   }
}