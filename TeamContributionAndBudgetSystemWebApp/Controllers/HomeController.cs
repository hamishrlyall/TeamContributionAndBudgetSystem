using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TCABS_DataLibrary;
using static TCABS_DataLibrary.BusinessLogic.UserProcessor;
using TeamContributionAndBudgetSystemWebApp.Models;

namespace TeamContributionAndBudgetSystemWebApp.Controllers
{
   public class HomeController : Controller
   {
      public ActionResult Index( )
      {
         return View( );
      }

      public ActionResult About( )
      {
         ViewBag.Message = "Your application description page.";

         return View( );
      }

      public ActionResult Contact( )
      {
         ViewBag.Message = "Your contact page.";

         return View( );
      }

      // GET
      public ActionResult SignUp( )
      {
         ViewBag.Message = "User Sign Up";

         return View( );
      }

      [HttpPost]
      [ValidateAntiForgeryToken]
      public ActionResult SignUp( UserModel _Model )
      {
         if( ModelState.IsValid )
         {
            int recordsCreate = CreateUser( _Model.Username, _Model.FirstName, _Model.LastName, _Model.EmailAddress, _Model.PhoneNumber );

            return RedirectToAction( "Index" );
         }

         return View( );
      }
   }
}