using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TeamContributionAndBudgetSystemWebApp.Controllers
{
   public class UnitOfferingController : BaseController
   {
      // GET: UnitOffering
      public ActionResult Index()
      {
         // Make sure the user is logged in and that they have permission
         if( !IsUserLoggedIn ) return RedirectToLogin( );

         // Set the page message
         ViewBag.Message = "UnitOffering List"
         return View();
      }
   }
}