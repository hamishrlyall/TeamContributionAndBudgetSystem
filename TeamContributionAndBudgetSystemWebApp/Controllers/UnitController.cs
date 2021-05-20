using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TCABS_DataLibrary.BusinessLogic;
using TeamContributionAndBudgetSystemWebApp.Models;
using static TCABS_DataLibrary.BusinessLogic.UnitProcessor;

namespace TeamContributionAndBudgetSystemWebApp.Controllers
{
   public class UnitController : BaseController
   {
      public TCABS_DB_Context db = new TCABS_DB_Context( );
      // GET: Unit
      public ActionResult Index()
      {
         return View();
      }
   }
}