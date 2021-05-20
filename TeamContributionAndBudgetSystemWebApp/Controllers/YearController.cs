using System;
using System.Collections.Generic;
using System.Linq;
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
         // GET: Year
         public ActionResult Index()
         {
            // Make sure the user is logged in and that they have permission
            if( !IsUserLoggedIn ) return RedirectToLogin( );

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

      [HttpPost]
      [MultiButton( MatchFormKey = "action", MatchFormValue = "Add Year")]
      [ValidateAntiForgeryToken]
      public ActionResult AddYear( Year model )
      {
         // Make sure the user is logged in and that they have permission
         if( !IsUserLoggedIn ) return RedirectToLogin( );

         if( ModelState.IsValid )
         {
            try
            {
               YearProcessor.CreateYear( model.YearValue );

               //return Redirect( Request.UrlReferrer.ToString( ) );
            }
            catch( Exception e )
            {
               ModelState.AddModelError( "", e.Message );
            }
         }
         else
         {
            var errors = ModelState.Values.SelectMany( v => v.Errors );
         }
         // Redirects to page where data is reloaded.
         return Redirect( Request.UrlReferrer.ToString( ) );
      }
    }
}