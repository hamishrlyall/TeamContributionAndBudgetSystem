using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TCABS_DataLibrary;
using static TCABS_DataLibrary.BusinessLogic.UserProcessor;
using TeamContributionAndBudgetSystemWebApp.Models;
using TCABS_DataLibrary.BusinessLogic;
using System.Net;

namespace TeamContributionAndBudgetSystemWebApp.Controllers
{
   public class UserController : Controller
   {
      // GET: User
      public ActionResult Index( )
      {
         ViewBag.Message = "Users List";

         var data = LoadUsers( );
         List<UserModel> users = new List<UserModel>( );

         foreach( var row in data )
         {
            users.Add( new UserModel
            {
               UserId = row.UserId,
               Username = row.Username,
               FirstName = row.FirstName,
               LastName = row.LastName,
               EmailAddress = row.Email,
               ConfirmEmailAddress = row.Email,
               PhoneNumber = row.PhoneNo,
            } );
         }

         return View( users );
      }


      public ActionResult Details(int? id )
      {
         if( id == null )
         {
            return new HttpStatusCodeResult( HttpStatusCode.BadRequest );
         }
         int userId = ( int ) id;
         var data = UserProcessor.SelectUser( userId );

         if( data == null )
         {
            return HttpNotFound( );
         }
         UserModel User = new UserModel( );
         User.UserId = data.UserId;
         User.Username = data.Username;
         User.FirstName = data.FirstName;
         User.LastName = data.LastName;
         User.EmailAddress = data.Email;
         User.PhoneNumber = data.PhoneNo;

         return View( User );
      }

   }
}