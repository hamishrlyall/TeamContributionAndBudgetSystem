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
using System.Xml.Serialization;
using System.Data;
using System.Web.Security;

namespace TeamContributionAndBudgetSystemWebApp.Controllers
{
   // Comment
   public class UserController : BaseController
   {
      
      private TCABS_Db_Context db = new TCABS_Db_Context( );
      // GET: User
      public ActionResult Index( )
      {
         ViewBag.Message = "Users List";

         db.GetUsers( );

         return View( db );
      }
      public ActionResult Details(int? id )
      {
         if( id == null )
         {
            return new HttpStatusCodeResult( HttpStatusCode.BadRequest );
         }
         int userId = ( int ) id;

         db.GetUser( userId );
         PopulateRoleDropDownList( );

         return View( db );
      }

      /// <summary>
      /// This method is called when the user hits the submit button on the Details Page.
      /// It is used to add new UserRoles to the database.
      /// </summary>
      /// <param name="_UserRole"></param>
      /// The _UserRole parameter will contain the User data and the RoleId which will be sent to the database to insert a new UserRole row.
      /// <returns> This method will return the view with either the newly added UserRole or an error message.</returns>
      [HttpPost]
      [ValidateAntiForgeryToken]
      public ActionResult Details( UserRole _UserRole )
      {
         if( _UserRole.User == null )
         {
            return new HttpStatusCodeResult( HttpStatusCode.BadRequest );
         }
         try
         {
            // Send Create procedure to DataAccess
            var data = UserRoleProcessor.InsertUserRole( _UserRole.User.UserId, _UserRole.RoleId );
            if( data == null )
               throw new DataException("Role added was invalid.");
            var roleData = RoleProcessor.SelectRole( data.RoleId );
            var userRole = new UserRole( );
            userRole.UserRoleId = data.UserRoleId;
            userRole.UserId = data.UserId;
            userRole.RoleId = data.RoleId;
            userRole.Role = new Role { RoleId = roleData.RoleId, Name = roleData.Name };

            db.GetUser( _UserRole.User.UserId );
            var user = db.User;
            user.UserRoles.Add( userRole );
         }
         catch( DataException _Ex )
         {
            ModelState.AddModelError( "", $"Unable to save changes due to Error: { _Ex.Message }" );
         }

         //Reload User details here.
         db.GetUser( _UserRole.User.UserId );

         PopulateRoleDropDownList( );

         return View( db );
      }
      private void PopulateRoleDropDownList( )
      {
         ViewBag.RoleId = new SelectList( db.GetRoles( ), "RoleId", "Name", null );
      }

      public ActionResult DeleteUserRole( int id )
      {
         if( id < 1 )
         {
            return new HttpStatusCodeResult( HttpStatusCode.BadRequest );
         }
         var userRole = UserRoleProcessor.SelectUserRole( id );

         if( userRole == null )
         {
            return HttpNotFound( );
         }

         var user = UserProcessor.SelectUserWithRoles( userRole.UserId );
         return View( User );
      }

      [HttpDelete]
      [ValidateAntiForgeryToken]
      public ActionResult DeleteUserRole( UserRole _UserRole )
      {
         if( _UserRole == null )
         {
            return new HttpStatusCodeResult( HttpStatusCode.BadRequest );
         }
         var row = UserRoleProcessor.DeleteUserRole( _UserRole.UserId );

         return View( User );
      }

        /// <summary>
        /// Called when a GET request is made for the create user page.
        /// </summary>
        public ActionResult Create()
        {
            ViewBag.Message = "Create New User";

            return View();
        }

        /// <summary>
        /// Called when a POST request is made by the create user page.
        /// </summary>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(User model)
        {
            // Make sure the entered data is valid
            if (ModelState.IsValid)
            {
                // Generate a password salt
                // Hash the password
                string passwordSalt = UserLogin.CreatePasswordSalt();
                string password = UserLogin.HashPassword(model.Password, passwordSalt);

                // Clear password info from model, just in case for security
                model.Password = null;
                model.ConfirmPassword = null;

                // Create the user within the database
                int recordsCreated = CreateUser(
                    model.Username,
                    model.FirstName,
                    model.LastName,
                    model.EmailAddress,
                    model.PhoneNumber,
                    password,
                    passwordSalt);

                // Check for errors
                if (recordsCreated == 1)
                    return RedirectToAction("Index");
                else
                    ModelState.AddModelError("", "Failed to create user"); // TODO: Need to add proper error checking here, to provide useful/detailed responses
            }
            else
            {
                //show error
                var errors = ModelState.Values.SelectMany(v => v.Errors);
            }
            return View();
        }

        /// <summary>
        /// Called when a GET request is made for the create user in bulk page.
        /// </summary>
        [HttpGet]
        public ActionResult CreateBulk()
        {
            //if (!IsUserLoggedIn()) return RedirectToAction("Login", "Home");

            ViewBag.Message = "Create New Users using CSV File";

            return View();
        }

        /// <summary>
        /// Called when a POST request is made by the create user in bulk page.
        /// </summary>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateBulk(HttpPostedFileBase file)
        {
            //if (!IsUserLoggedIn()) return RedirectToAction("Login", "Home");
            try
            {
                // Decode the CSV file
                FileCSV data = new FileCSV(file);

                // Make sure the headers are correct
                // This will throw an exception if not
                data.ValidateHeaders(new string[] {
                    "Username",  // 0
                    "FirstName", // 1
                    "LastName",  // 2
                    "Email",     // 3
                    "PhoneNo",   // 4
                    "Password"   // 5
                });

                // Loop through each row of data
                // Generate the list of results
                int errorCount = 0;
                int successCount = 0;
                foreach (string[] row in data.Row)
                {
                    // Generate a password salt
                    // Hash the password
                    string passwordSalt = UserLogin.CreatePasswordSalt();
                    string password = UserLogin.HashPassword(row[5], passwordSalt);

                    // Create the user within the database
                    try
                    {
                        int recordsCreated = CreateUser(
                            row[0], // Username
                            row[1], // FirstName
                            row[2], // LastName
                            row[3], // Email
                            Convert.ToInt32(row[4]), // PhoneNo
                            password,
                            passwordSalt);
                        if (recordsCreated == 0) throw new Exception("Failed to create record");
                        data.SetComment(row, "");
                        successCount++;
                    }
                    catch(Exception e)
                    {
                        data.SetComment(row, e.Message);
                        errorCount++;
                    }
                }

                // Add success and error count to ViewBag, so that view can display it
                ViewBag.UploadSuccessCount = successCount;
                ViewBag.UploadFailureCount = errorCount;
                
                // Generate and record the error file, if required
                if (errorCount > 0)
                {
                    Session[FileCSV.SessionLabelUploadErrorLog] = Downloadable.CreateCSV(data.GenerateErrorFile(), "errors.csv");
                }
                else
                {
                    Session.Remove(FileCSV.SessionLabelUploadErrorLog);
                }
                // To get results use link as:
                // @Html.ActionLink( "Download Error Log", "Download", "Index", new { label = TeamContributionAndBudgetSystemWebApp.Models.FileCSV.LabelLastBulkUploadErrorLog } )

                return View();
            }
            catch
            {
                ViewBag.Message = "File upload failed.";
                return View();
            }
        }
    }
}