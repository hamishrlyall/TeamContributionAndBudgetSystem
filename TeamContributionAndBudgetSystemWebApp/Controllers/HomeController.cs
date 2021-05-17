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
using System.Web.UI.WebControls;

namespace TeamContributionAndBudgetSystemWebApp.Controllers
{
   public class HomeController : Controller
   {
      private TCABS_Db_Context db = new TCABS_Db_Context( );

      public ActionResult Index( )
      { 
         string username = System.Web.HttpContext.Current.User.Identity.Name;
         //if( !string.IsNullOrEmpty( username ) )
         //{
         //   db = new TCABS_Db_Context( );
         //}

         return View( db );
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
         bool IsValidUser = db.Users
         .Any( u => u.Username.ToLower( ) == _User
         .Username.ToLower( ) && u
         .Password == _User.Password );

         if( IsValidUser )
         {
            FormsAuthentication.SetAuthCookie( _User.Username, false );
            
            if( FormsAuthentication.Authenticate( _User.Username, _User.Password ) )
            {
               Session[ "userID" ] = _User.Username;
            }

            return RedirectToAction( "Index", "Home" );
         }
         else
         {
            ModelState.AddModelError( "", "invalid Username or Password" );
         }

         return View( );
      }

      public ActionResult Logout( )
      {
         FormsAuthentication.SignOut( );
         return RedirectToAction( "Login" );
      }
   }
}