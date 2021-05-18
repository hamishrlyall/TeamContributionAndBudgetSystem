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
        /// A page used to display a permission-denied message.
        /// </summary>
        public ActionResult Forbidden()
        {
            return View();
        }

        /// <summary>
        /// Called when a GET request is made for the Login page.
        /// </summary>
        [HttpGet]
        public ActionResult Login()
        {
            // Check if a user is already logged in
            // If so then redirect to the index page instead of showing the login page
            if (IsUserLoggedIn)
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
            if (IsUserLoggedIn)
                return RedirectToAction("Index", "Home");

            // Try to login the user
            User user = login.ValidateUser();
            if (user != null)
            {
                // Record that user logged in successfully
                SetUserLoggedIn(user);

                // Go to home page
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ModelState.AddModelError("", "invalid Username or Password");
            }
            return View();
        }

        /// <summary>
        /// Called when a GET request is made for the Login page.
        /// </summary>
        public ActionResult Logout()
        {
            // Record that user logged out
            SetUserLoggedOut();

            // Go to home page
            return RedirectToAction("Index");
        }

        /// <summary>
        /// Called when a requested is made to download a file.
        /// </summary>
        /// <param name="label">The label used for the specific file to download.</param>
        public ActionResult Download(string label)
        {
            // Make sure label pointers to a valid item.
            if ((label != null) && (Session[label] != null))
            {
                // Make sure the object at label is a downloadable file
                if (Session[label] is Downloadable)
                {
                    return (Downloadable)Session[label];
                }
            }

            // If here then redirect to default page
            return RedirectToAction("Index");
        }
    }
}