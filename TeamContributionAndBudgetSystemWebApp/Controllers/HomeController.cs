using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TCABS_DataLibrary;
using static TCABS_DataLibrary.BusinessLogic.UserProcessor;
using TeamContributionAndBudgetSystemWebApp.Models;
using System.Security;
using System.Web.Security;

namespace TeamContributionAndBudgetSystemWebApp.Controllers
{
   public class HomeController : Controller
   {
      private TCABS_Db_Context db = new TCABS_Db_Context( );

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
      [HttpGet]
      public ActionResult Login( )
      {
         ViewBag.Message = "User Sign Up";

         return View( );
      }

      [HttpPost]
      [ValidateAntiForgeryToken]
      public ActionResult Login( User _User )
      {
         //var errors = ModelState.Values.SelectMany( v => v.Errors );
         //if( ModelState.IsValid )
         //{
         bool IsValidUser = db.Users
         .Any( u => u.Username.ToLower( ) == _User
         .Username.ToLower( ) && u
         .Password == _User.Password );

         if( IsValidUser )
         {
            FormsAuthentication.SetAuthCookie( _User.Username, false );
            return RedirectToAction( "Index", "Home" );
         }
         else
         {
            ModelState.AddModelError( "", "invalid Username or Password" );
         }
         //}
         //
         return View( );
      }

      public ActionResult Logout( )
      {
         FormsAuthentication.SignOut( );
         return RedirectToAction( "Login" );
      }
      //// GET
      //public ActionResult SignUp( )
      //{
      //   ViewBag.Message = "User Sign Up";

      //   return View( );
      //}

      //[HttpPost]
      //[ValidateAntiForgeryToken]
      //public ActionResult SignUp( User _Model )
      //{
      //   if( ModelState.IsValid )
      //   {
      //      int recordsCreate = CreateUser( _Model.Username, _Model.FirstName, _Model.LastName, _Model.EmailAddress, _Model.PhoneNumber );

      //      return RedirectToAction( "Index" );
      //   }

      //   return View( );
      //}
   }
}