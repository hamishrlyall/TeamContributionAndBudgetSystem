using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TCABS_DataLibrary;
using TeamContributionAndBudgetSystemWebApp.Models;
using TCABS_DataLibrary.BusinessLogic;
using System.Net;
using System.Xml.Serialization;
using System.Data;
using System.Web.Security;
using System.Reflection;
using static TCABS_DataLibrary.BusinessLogic.UnitOfferingProcessor;

namespace TeamContributionAndBudgetSystemWebApp.Controllers
{
   public class UnitOfferingController : BaseController
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

      // GET: UnitOffering
      [HttpGet]
      public ActionResult Index()
      {
         // Make sure the user is logged in and that they have permission
         if( !IsUserLoggedIn ) return RedirectToLogin( );

         // Set the page message
         ViewBag.Message = "UnitOffering List";

         // Navigate to View with UnitOffering List
         db.GetUnitOfferings( );
         return View( db.UnitOfferings );
      }

      /// <summary>
      /// Navigate to Details page for a Unit Offering
      /// </summary>
      /// <param name="id"></param>
      /// <returns></returns>
      public ActionResult Details( int id )
      {
         // Make sure the user is logged in and that they have permission
         if( !IsUserLoggedIn )
            return RedirectToLogin( );

         if( id == 0 )
         {
            return new HttpStatusCodeResult( HttpStatusCode.BadRequest );
         }
         try
         {
            var unitOfferingModel = UnitOfferingProcessor.SelectUnitOfferingForUnitOfferingId( id );
            if( unitOfferingModel == null )
               return RedirectToIndexIdNotFound( id );

            var unit = UnitProcessor.SelectUnitForUnitId( unitOfferingModel.UnitId );
            var teachingPeriod = TeachingPeriodProcessor.SelectTeachingPeriodForTeachingPeriodId( unitOfferingModel.TeachingPeriodId );
            var year = YearProcessor.SelectYearForYearId( unitOfferingModel.YearId );
            var convenor = UserProcessor.SelectUserForUserId( unitOfferingModel.ConvenorId );

            var projectOfferings = ProjectOfferingProcessor.SelectProjectOfferingsForUnitOfferingId( id );
            var enrollments = EnrollmentProcessor.LoadEnrollmentsForUnitOffering( id );

            // Convert the model data to non-model data
            // Pass the data to the view
            var unitOffering = new UnitOffering( unitOfferingModel, unit, teachingPeriod, year, convenor, projectOfferings, enrollments );

            ViewBag.UserId = new SelectList( unitOffering.GetStudents( ), "UserId", "Username", null );
            return View( unitOffering );
         }
         catch( Exception e)
         {
            return RedirectToIndex( e );
         }

         // Find Unit Offering
         //db.GetUnitOffering( id );

         // Populate Student Drop Down List for to add new Enrollments
         //PopulateStudentDropDownList( );

         // Navigate to View
         //return View( db );
      }
      
      /// <summary>
      /// POST method to delete Enrolments for a Unit
      /// </summary>
      /// <param name="_EnrollmentId"></param>
      /// <returns></returns>
      [HttpPost]
      [MultiButton( MatchFormKey = "action", MatchFormValue = "Unenrol")]
      [ValidateAntiForgeryToken]
      public ActionResult Unenrol( int _EnrollmentId )
      {
         // Make sure the user is logged in and that they have permission
         if( !IsUserLoggedIn )
            return RedirectToLogin( );

         if( _EnrollmentId <= 0 )
         {
            return new HttpStatusCodeResult( HttpStatusCode.BadRequest );
         }
         try
         {
            //Attempt to delete Enrolment from database
            var rowsDeleted = EnrollmentProcessor.DeleteEnrollment( _EnrollmentId );
            if( rowsDeleted <= 0 )
               throw new DataException( "Unable to Delete Enrollment" );
         }
         catch( DataException _Ex )
         {
            //Error Handling
            ModelState.AddModelError( "", $"Unable to save changes due to Error: { _Ex.Message }" );
         }

         // Redirects to poage where data is reloaded.
         return Redirect( Request.UrlReferrer.ToString( ) );
      }

      [HttpPost]
      [MultiButton( MatchFormKey = "action", MatchFormValue = "Enrol")]
      [ValidateAntiForgeryToken]
      public ActionResult Enrol( Enrollment _Enrollment )
      {
         // Make sure the user is logged in and that they have permission
         if( !IsUserLoggedIn )
            return RedirectToLogin( );

         // Ensure Enrollment Model is associated with UnitOffering
         if( _Enrollment.UnitOfferingId <= null )
         {
            return new HttpStatusCodeResult( HttpStatusCode.BadRequest );
         }
         try
         {
            // Ensure a User has beeen selected.
            if( _Enrollment.UserId <= 0 )
               throw new DataException( "No User Selected." );

            // Check if Student is already enrolled in this unit
            var rowsFound = EnrollmentProcessor.SelectEnrollmentCountForUnitOfferingIdAndUserId( _Enrollment.UnitOfferingId, _Enrollment.UserId );
            if( rowsFound > 0 )
               throw new DataException( $"User is already enrolled in this Unit Offering." );

            //Attempt to insert new Enrollment to database using data from parameter
            var data = EnrollmentProcessor.InsertEnrollmentModel( _Enrollment.UnitOfferingId, _Enrollment.UserId );
            // Checks if Insert operation was successful if not throws an error.
            if( data == null )
               throw new DataException( "Enrollmment added was invalid." );
         }
         catch( Exception _Ex )
         {
            // Show Model Errors reload data and return to view.
            ModelState.AddModelError( "", $"Unable to save changes due to Error: { _Ex.Message}" );
            db.GetUnitOffering( _Enrollment.UnitOfferingId );
            PopulateStudentDropDownList( );
            return View( db );
         }

         // Redirects to page where data is reloaded.
         return Redirect( Request.UrlReferrer.ToString( ) );
      }

      // Helper method to provide Student Drop Down List for enrollments
      private void PopulateStudentDropDownList( )
      {
         ViewBag.UserId = new SelectList( db.GetStudents( ), "UserId", "Username", null );
      }

      /// <summary>
      /// Navigation method for UnitOffering Create Page
      /// </summary>
      /// <returns></returns>
      public ActionResult Create( )
      {
         // Make sure the user is logged in and that they have permission
         if( !IsUserLoggedIn )
            return RedirectToLogin( );

         ViewBag.Message = "Create New UnitOffering";
         // Generate drop down lists for each field.
         PopulateConvenorDropDownList( );
         PopulateTeachingPeriodDropDownList( );
         PopulateUnitDropDownList( );
         PopulateYearDropDownList( );

         // Navigate to View
         return View( );
      }


      // Helper method to provide Convenor Drop Down List for new UnitOfferings
      private void PopulateConvenorDropDownList( )
      {
         ViewBag.ConvenorId = new SelectList( db.GetConvenors( ), "UserId", "Username", null );
      }

      // Helper method to provide Unit Drop Down List for new UnitOfferings
      private void PopulateUnitDropDownList( )
      {
         ViewBag.UnitId = new SelectList( db.GetUnits( ), "UnitId", "Name", null );
      }

      // Helper method to provide TeachingPeriod Drop Down List for new UnitOfferings
      private void PopulateTeachingPeriodDropDownList( )
      {
         ViewBag.TeachingPeriodId = new SelectList( db.GetTeachingPeriods( ), "TeachingPeriodId", "Name", null );
      }

      // Helper method to provide Year Drop Down List for new UnitOfferings
      private void PopulateYearDropDownList( )
      {
         ViewBag.YearId = new SelectList( db.GetYears( ), "YearId", "YearValue", null );
      }

      /// <summary>
      /// POST method for UnitOffering Create
      /// </summary>
      /// <param name="model"></param>
      /// <returns></returns>
      [HttpPost]
      [ValidateAntiForgeryToken]
      public ActionResult Create( UnitOffering model )
      {
         // Make sure the user is logged in and that they have permission
         if( !IsUserLoggedIn )
            return RedirectToLogin( );

         if( ModelState.IsValid )
         {
            try
            {
               // Validate values of Year and TeachingPeriod
               db.GetYear( model.YearId );
               db.GetTeachingPeriod( model.TeachingPeriodId );
               ValidDate( db.Year.YearValue, db.TeachingPeriod.Month, db.TeachingPeriod.Day );

               // Attempt to Insert new UnitOffering
               UnitOfferingProcessor.InsertUnitOffering(
                  model.UnitId,
                  model.TeachingPeriodId,
                  model.YearId,
                  model.ConvenorId );

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

         // Reload drop down lists for create
         PopulateConvenorDropDownList( );
         PopulateTeachingPeriodDropDownList( );
         PopulateUnitDropDownList( );
         PopulateYearDropDownList( );

         // return to create view
         return View( db );
      }

      // Ensure Year value, Month value, and Day value are a valid date.
      private void ValidDate( int year, int month, int day )
      {
         try
         {
            var date = new DateTime( year, month, day );
         }
         catch(Exception e)
         {
            ModelState.AddModelError( "", "The selected Year is not compatible with the starting date of the Teaching Period" );
         }
      }

      /// <summary>
      /// GET method for UnitOfferingDelete page
      /// </summary>
      /// <param name="id"></param>
      /// <returns></returns>
      [HttpGet]
      public ActionResult Delete( int id )
      {
         // Make sure the user is logged in and that they have permission
         if( !IsUserLoggedIn )
            return RedirectToLogin( );

         if( id == null )
         {
            // If no id provided show BadRequest Error
            return new HttpStatusCodeResult( HttpStatusCode.BadRequest );
         }

         // Attempt to Get UnitOffering
         db.GetUnitOffering( id );

         // Navigate to View
         return View( db );
      }

      /// <summary>
      /// POST method for UnitOffering Delete method
      /// </summary>
      /// <param name="unitoffering"></param>
      /// <returns></returns>
      [HttpPost]
      [ValidateAntiForgeryToken]
      public ActionResult Delete( UnitOffering unitoffering )
      {
         // Make sure the user is logged in and that they have permission
         if( !IsUserLoggedIn )
            return RedirectToLogin( );

         try
         {
            // Find UnitOffering
            db.GetUnitOffering( unitoffering.UnitOfferingId );
            var date = new DateTime( db.UnitOffering.Year.YearValue, db.UnitOffering.TeachingPeriod.Month, db.UnitOffering.TeachingPeriod.Day );

            // Ensure commencement date is not in past.
            if( date < DateTime.Now )
               throw new DataException( "Cannot remove Unit Offering which has already commenced." );

            // Ensure UnitOffering has no enrollments
            if( db.UnitOffering.Enrollments.Any( ) )
               throw new DataException( "Cannot Delete Unit Offering containing Enrollments" );

            // Attempt to delete UnitOffering
            int rowsDeleted = UnitOfferingProcessor.DeleteUnitOffering( unitoffering.UnitOfferingId );
            if( rowsDeleted <= 0 )
               throw new DataException( "Unable to Delete UnitOffering." );

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

      [HttpPost]
      [ValidateAntiForgeryToken]
      public ActionResult CreateBulkUnitOffering( HttpPostedFileBase file )
      {
         // Make sure the user is logged in and that they have permission
         if( !IsUserLoggedIn ) return RedirectToLogin( );

         //Create data which needs to be outside the try-catch block
         FileCSV data = null;
         int uploadCount = 0;
         int failCount = 0;
         Downloadable errorFile = null;

         // Enter a try-catch block to make sure any exceptions are caught
         try
         {
            // Decode the CSV file
            data = new FileCSV( file );

            // Make sure the headers are correct
            // This will throw an exception if not
            data.ValidateHeaders( new string[ ]
            {
               "Unit",           // 0
               "TeachingPeriod", // 1
               "Year",           // 2
               "Convenor"        // 3
            } );

            // Loop through each row of data
            // Generate the list of results
            foreach( string[ ] row in data.Row )
            {
               try
               {
                  var unit = db.GetUnitForName( row[ 0 ] );
                  if( unit == null )
                     throw new DataException( "Unit doesn't exist. " );

                  var teachingPeriod = db.GetTeachingPeriodForName( row[ 1 ] );
                  if( teachingPeriod == null )
                     throw new DataException( "TeachingPeriod doesn't exist. " );

                  var year = db.GetYearForYearValue( Int32.Parse( row[ 2 ] ) );
                  if( year == null )
                     throw new DataException( "Year doesn't exist. " );

                  var convenor = db.GetUserForUsername( row[ 3 ] );
                  if( convenor == null )
                     throw new DataException( "Convenor doesn't exist. " );


                  UnitOfferingProcessor.InsertUnitOffering( unit.UnitId, teachingPeriod.TeachingPeriodId, year.YearId, convenor.UserId );
                  data.SetComment( row, "" );
                  uploadCount++;
               }
               catch( Exception e )
               {
                  data.SetComment( row, e.Message );
                  failCount++;
               }
            }

            // Generate and record the error file, if required
            if( failCount > 0 ) errorFile = Downloadable.CreateCSV( data.GenerateErrorFile( ), "errors.csv" );
         }
         catch( Exception e )
         {
            // Record error message for View
            TempData[ "UploadError" ] = e.Message;
         }

         // Record item counts for View
         if( uploadCount > 0 ) TempData[ "UploadCount" ] = uploadCount;
         if( failCount > 0 ) TempData[ "FailCount" ] = failCount;
         Session[ FileCSV.SessionLabelUploadErrorLog ] = ( failCount > 0 ) ? errorFile : null;

         // All file processing has been completed
         // Go to normal create page

         return RedirectToAction( "Create", "UnitOffering" );
      }

      [HttpPost]
      [ValidateAntiForgeryToken]
      public ActionResult CreateBulkEnrollment( HttpPostedFileBase file )
      {
         // Make sure the user is logged in and that they have permission
         if( !IsUserLoggedIn ) return RedirectToLogin( );

         //Create data which needs to be outside the try-catch block
         FileCSV data = null;
         int uploadCount = 0;
         int failCount = 0;
         Downloadable errorFile = null;

         // Enter a try-catch block to make sure any exceptions are caught
         try
         {
            // Decode the CSV file
            data = new FileCSV( file );

            // Make sure the headers are correct
            // This will throw an exception if not
            data.ValidateHeaders( new string[ ]
            {
               "Unit",           // 0
               "TeachingPeriod", // 1
               "Year",           // 2
               "Student"        // 3
            } );

            // Loop through each row of data
            // Generate the list of results
            foreach( string[ ] row in data.Row )
            {
               try
               {
                  //var unit = db.GetUnitForName( row[ 0 ] );
                  //if( unit == null )
                  //   throw new DataException( "Unit doesn't exist. " );

                  //var teachingPeriod = db.GetTeachingPeriodForName( row[ 1 ] );
                  //if( teachingPeriod == null )
                  //   throw new DataException( "TeachingPeriod doesn't exist. " );

                  //var year = db.GetYearForYearValue( Int32.Parse( row[ 2 ] ) );
                  //if( year == null )
                  //   throw new DataException( "Year doesn't exist. " );

                  var unitOffering = db.GetUnitOfferingForDetails( row[ 0 ], row[ 1 ], Int32.Parse( row[ 2 ] ) );
                  if( unitOffering == null )
                     throw new DataException( "UnitOffering doesn't exist. " );


                  var student = db.GetUserForUsername( row[ 3 ] );
                  if( student == null )
                     throw new DataException( "Convenor doesn't exist. " );


                  EnrollmentProcessor.InsertEnrollmentModel( unitOffering.UnitOfferingId, student.UserId );
                  data.SetComment( row, "" );
                  uploadCount++;
               }
               catch( Exception e )
               {
                  data.SetComment( row, e.Message );
                  failCount++;
               }
            }

            // Generate and record the error file, if required
            if( failCount > 0 ) errorFile = Downloadable.CreateCSV( data.GenerateErrorFile( ), "errors.csv" );
         }
         catch( Exception e )
         {
            // Record error message for View
            TempData[ "UploadError" ] = e.Message;
         }

         // Record item counts for View
         if( uploadCount > 0 ) TempData[ "UploadCount" ] = uploadCount;
         if( failCount > 0 ) TempData[ "FailCount" ] = failCount;
         Session[ FileCSV.SessionLabelUploadErrorLog ] = ( failCount > 0 ) ? errorFile : null;

         // All file processing has been completed
         // Go to normal create page

         return RedirectToAction( "Index", "UnitOffering" );
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
      /// <param name="unitOfferingId">The project (ID) which could no be found.</param>
      private ActionResult RedirectToIndexIdNotFound( int unitOfferingId )
      {
         TempData[ LabelError ] = "A Unit Offering with ID:" + unitOfferingId + " does not appear to exist";
         return RedirectToIndex( );
      }
   }
}