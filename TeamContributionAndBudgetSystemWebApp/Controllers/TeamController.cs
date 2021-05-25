using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using TCABS_DataLibrary.BusinessLogic;
using TCABS_DataLibrary.Models;
using TeamContributionAndBudgetSystemWebApp.Models;

namespace TeamContributionAndBudgetSystemWebApp.Controllers
{
   public class TeamController : BaseController
   {

      private TCABS_Db_Context db = new TCABS_Db_Context( );
      /// <summary>
      /// A string to use with TempData[] for project error messages shown on the Index() page.
      /// </summary>
      public const string LabelError = "projectError";

      // Details
      public ActionResult Details( int? id )
      {
         // Make sure the user is logged in and that they have permission
         if( !IsUserLoggedIn )
            return RedirectToLogin( );

         if( id == null )
         {
            return new HttpStatusCodeResult( HttpStatusCode.BadRequest );
         }

         try
         {
            int teamId = ( int ) id;
            // Get the team data
            var teamModel = TeamProcessor.GetTeamForTeamId( teamId );
            if( teamModel == null )
               return RedirectToUnitOfferingIndex( );

            var unitOffering = UnitOfferingProcessor.SelectUnitOfferingForUnitOfferingId( teamModel.UnitOfferingId );

            var team = new Team( teamModel, unitOffering );
            ViewBag.EnrollmentId = new SelectList( team.GetAvailableEnrollments( team.UnitOfferingId ), "EnrollmentId", "Username", null );
            
            return View( team );
         }
         catch( Exception e )
         {
            return RedirectToIndex( e );
         }

      }

      //[HttpPost]
      //[MultiButton( MatchFormKey = "action", MatchFormValue = "Remove Team Member" )]
      //[ValidateAntiForgeryToken]
      //public ActionResult RemoveTeamMember( int _EnrollmentId )
      //{
      //   // Make sure the user is logged in and that they have permission
      //   if( !IsUserLoggedIn )
      //      return RedirectToLogin( );

      //   if( _EnrollmentId <= 0 )
      //   {
      //      return new HttpStatusCodeResult( HttpStatusCode.BadRequest );
      //   }
      //   try
      //   {
      //      //Attempt to delete Enrolment from database
      //      EnrollmentProcessor.UpdateEnrollmentWithTeamId( _EnrollmentId, 0 );
      //   }
      //   catch( DataException _Ex )
      //   {
      //      //Error Handling
      //      ModelState.AddModelError( "", $"Unable to save changes due to Error: { _Ex.Message }" );
      //   }

      //   return Redirect( Request.UrlReferrer.ToString( ) );
      //}

      [HttpPost]
      [MultiButton( MatchFormKey = "action", MatchFormValue = "Add Team Member")]
      [ValidateAntiForgeryToken]
      public ActionResult AddTeamMember( Enrollment _Enrollment )
      {
         // Make sure the user is logged in and that they have permission
         if( !IsUserLoggedIn )
            return RedirectToLogin( );

         if( _Enrollment.TeamId <= 0 )
         {
            return new HttpStatusCodeResult( HttpStatusCode.BadRequest );
         }
         try
         {
            // Ensure a User has been selected
            //if( _Enrollment.UserId <= 0 )
            //   throw new DataException( "No Enrolled User Selected." );
            var enrollmentData = EnrollmentProcessor.SelectEnrollmentForEnrollmentId( _Enrollment.EnrollmentId );

            //Check if Student is already a member of this team
            var rowsFound = EnrollmentProcessor.SelectEnrollmentCountForTeamIdAndUserId( _Enrollment.TeamId, enrollmentData.UserId );
            if( rowsFound > 0 )
               throw new DataException( $"User is already a member of this Team." );

            // Attempt to update enrollment with TeamId
            EnrollmentProcessor.UpdateEnrollmentWithTeamId( enrollmentData.EnrollmentId, _Enrollment.TeamId );

            return Redirect( Request.UrlReferrer.ToString( ) );
         }
         catch( Exception _Ex)
         {
            // Show Model Errors reload data and return to view.
            ModelState.AddModelError( "", $"Unable to save changes due to Error: {_Ex.Message}" );
         }

         // Get the team data
         var teamModel = TeamProcessor.GetTeamForTeamId( _Enrollment.TeamId );

         if( teamModel == null )
            return RedirectToUnitOfferingIndex( );

         var unitOffering = UnitOfferingProcessor.SelectUnitOfferingForUnitOfferingId( teamModel.UnitOfferingId );

         var team = new Team( teamModel, unitOffering );
         ViewBag.EnrollmentId = new SelectList( team.GetAvailableEnrollments( team.UnitOfferingId ), "EnrollmentId", "Username", null );

         return View( team );
      }

      // Create
      public ActionResult Create( int? id )
      {
         // Make sure the user is logged in and that they have permission
         if( !IsUserLoggedIn )
            return RedirectToLogin( );

         if( id == null )
         {
            return new HttpStatusCodeResult( HttpStatusCode.BadRequest );
         }

         ViewBag.Message = "Create New UnitOffering";

         int unitOfferingId = ( int ) id;

         var unitOfferingModel = UnitOfferingProcessor.SelectUnitOfferingForUnitOfferingId( unitOfferingId );
         if( unitOfferingModel == null )
            return RedirectToIndexUnitOfferingNotFound( unitOfferingId );

         //var projectModel = ProjectProcessor.GetProject( id );
         //if( projectModel == null ) return RedirectToIndexProjectNotFound( id );

         // Generate Supervisor Drop down List for each field.
         // Supervisor

         var team = new Team( unitOfferingModel );

         ViewBag.SupervisorId = new SelectList( team.GetAvailableSupervisors( ), "UserId", "Username", null );

         return View( team );
      }

      [HttpPost]
      [ValidateAntiForgeryToken]
      public ActionResult Create( Team model )
      {
         // Make sure the user is logged in and that they have permission
         if( !IsUserLoggedIn )
            return RedirectToLogin( );

         if( ModelState.IsValid )
         {
            try
            {
               // Attempt to Insert new Team
               TeamProcessor.InsertTeam( model.SupervisorId, model.UnitOfferingId, model.Name );

               // If Insert succesful return to Project
               //return RedirectToAction( "Details", "UnitOffering", model.UnitOfferingId );
               return RedirectToAction( "Details", new RouteValueDictionary(
                        new { controller = "UnitOffering", action = "Details", Id = model.UnitOfferingId } ) );
            }
            catch(Exception e)
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

         return View( model );
      }


      // Edit
      public ActionResult Edit(int? id)
      {
         // Make sure the user is logged in and that they have permission
         if( !IsUserLoggedIn )
            return RedirectToLogin( );

         if( id == null )
         {
            //If no id supplied return error code
            return new HttpStatusCodeResult( HttpStatusCode.BadRequest );
         }

         int teamId = ( int ) id;

         //Attempt to Get team for Id
         var teamModel = TeamProcessor.GetTeamForTeamId( teamId );
         if( teamModel == null )
            return RedirectToUnitOfferingIndex( );


         var unitOffering = UnitOfferingProcessor.SelectUnitOfferingForUnitOfferingId( teamModel.UnitOfferingId );
         //var project = ProjectProcessor.GetProject( teamModel.UnitOfferingId );


         var team = new Team( teamModel, unitOffering );
         ViewBag.SupervisorId = new SelectList( team.GetAvailableSupervisors( ), "UserId", "Username", null );

         return View( team );
      }

      [HttpPost]
      [ValidateAntiForgeryToken]
      public ActionResult Edit( Team team )
      {
         // Make sure the user is logged in and that they have permission
         if( !IsUserLoggedIn ) 
            return RedirectToLogin( );

         if( ModelState.IsValid )
         {
            try
            {
               var teamModel = new TeamModel( );
               teamModel.TeamId = team.TeamId;
               teamModel.SupervisorId = team.SupervisorId;
               teamModel.Name = team.Name;

               TeamProcessor.EditTeam( teamModel );

               return RedirectToAction( "Details", new RouteValueDictionary(
                           new { controller = "UnitOffering", action = "Details", Id = team.UnitOfferingId } ) );
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
         return View( team );
      }

      // Delete
      [HttpGet]
      public ActionResult Delete( int? id )
      {
         // Make sure the user is logged in and that they have permission
         if( !IsUserLoggedIn )
            return RedirectToLogin( );

         if( id == null )
         {
            // If no id provided show BadRequest Error
            return new HttpStatusCodeResult( HttpStatusCode.BadRequest );
         }

         int teamId = ( int ) id;

         // Get the team data
         var teamModel = TeamProcessor.GetTeamForTeamId( teamId );
         if( teamModel == null )
            return RedirectToUnitOfferingIndex( );

         var unitOffering = UnitOfferingProcessor.SelectUnitOfferingForUnitOfferingId( teamModel.UnitOfferingId );
         //var project = ProjectProcessor.GetProject( teamModel.UnitOfferingId );

         //var enrollments = EnrollmentProcessor.LoadEnrollmentsForTeam( id );

         return View( new Team( teamModel, unitOffering ) );
      }

      [HttpPost]
      [ValidateAntiForgeryToken]
      public ActionResult Delete( Team team )
      {
         // Make sure the user is logged in and that they have permission
         if( !IsUserLoggedIn )
            return RedirectToLogin( );

         try
         {


            int rowsDeleted = TeamProcessor.DeleteTeam( team.TeamId );
            if( rowsDeleted <= 0 )
               throw new DataException( "Unable to Delete Team." );

            return RedirectToAction( "Details", new RouteValueDictionary(
                        new { controller = "UnitOffering", action = "Details", Id = team.UnitOfferingId } ) );
         }
         catch( Exception e )
         {
            // Show any errors
            ModelState.AddModelError( "", e.Message );
         }

         //Find Team
         var teamModel = TeamProcessor.GetTeamForTeamId( team.TeamId );
         var unitOffering = UnitOfferingProcessor.SelectUnitOfferingForUnitOfferingId( teamModel.UnitOfferingId );
         return View( new Team( teamModel, unitOffering ) );
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
         return RedirectToUnitOfferingIndex( );
      }

      /// <summary>
      /// A shorthand method for getting a RedirectToAction() leading to the index page.
      /// </summary>
      private ActionResult RedirectToUnitOfferingIndex( )
      {
         return RedirectToAction("Index", "UnitOffering" );
      }

      /// <summary>
      /// A shorthand method for getting a RedirectToAction() leading to the index page.
      /// Also displays an error message saying that the requested project does not exist.
      /// </summary>
      /// <param name="unitOfferingId">The project (ID) which could no be found.</param>
      private ActionResult RedirectToIndexUnitOfferingNotFound( int unitOfferingId )
      {
         TempData[ LabelError ] = "A Unit Offering with ID:" + unitOfferingId + " does not appear to exist";
         return RedirectToIndex( );
      }
   }
}