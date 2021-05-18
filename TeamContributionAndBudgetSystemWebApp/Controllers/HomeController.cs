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
   public class HomeController : BaseController
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

        /// <summary>
        /// Called when a GET request is made for the Login page.
        /// </summary>
        [HttpGet]
        public ActionResult Login()
        {
            // Check if a user is already logged in
            // If so then redirect to the index page instead of showing the login page
            if (db.IsUserLoggedIn())
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ViewBag.Message = "User Sign Up";

                return View();
            }
        }

        /// <summary>
        /// Called when a POST request is made by the Login page.
        /// </summary>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login( UserLogin login )
        {

            // Check if a user is already logged in
            if (db.IsUserLoggedIn())
                return RedirectToAction("Index", "Home");

            // Try to login the user
            User user = login.ValidateUser();
            if (user != null)
            {
                // Set authentification cookie
                FormsAuthentication.SetAuthCookie(user.Username, false);

                // Update session info
                Session["userID"] = user.Username;
                db.User = user;

                // Go to home page
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ModelState.AddModelError("", "invalid Username or Password");
            }

            /*
            bool IsValidUser = db.Users.Any(
               u => u.Username.ToLower() == _User.Username.ToLower() &&
               u.Password == _User.Password
            );

            if (IsValidUser)
            {
                FormsAuthentication.SetAuthCookie(_User.Username, false);

                if (FormsAuthentication.Authenticate(_User.Username, _User.Password))
                {
                    Session["userID"] = _User.Username;
                }

                return RedirectToAction("Index", "Home");
            }
            else
            {
                ModelState.AddModelError("", "invalid Username or Password");
            }
            */

            return View();
        }

        /// <summary>
        /// Called when a GET request is made for the Login page.
        /// </summary>
        public ActionResult Logout()
        {
            // Clear authentification cookie
            FormsAuthentication.SignOut();

            // Update session info
            Session["userID"] = null;

            // Reset the TCABS context
            db = new TCABS_Db_Context();

            // Go to home page
            return RedirectToAction("Index");
        }
    }
}