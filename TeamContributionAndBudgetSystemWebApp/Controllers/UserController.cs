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

        /// <summary>
        /// The main page of the user controller.
        /// Shows a list of all users in the system.
        /// </summary>
        [HttpGet]
        public ActionResult Index()
        {
            // Make sure the user is logged in
            if (!IsUserLoggedIn) return RedirectToAction("Login", "Home");

            // Set the page message
            ViewBag.Message = "Users List";

            // Get all users from the database
            var userModels = UserProcessor.SelectUsers();

            // Change the format of the user list
            List<User> users = new List<User>();
            foreach (var u in userModels) users.Add(new User(u));

            // Return the view, with the list of users
            return View(users);
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

        [HttpGet]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            int userId = (int)id;

            TCABS_DataLibrary.Models.UserModel userModel = UserProcessor.SelectUserForUserId(userId);

            UserEdit user = new UserEdit(userModel);

            return View(user);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(UserEdit user)
        {
            // Make sure the entered data is valid
            if (ModelState.IsValid)
            {
                // Update the user within the database
                try
                {
                    UserProcessor.UpdateUser(
                        user.UserId,
                        user.Username,
                        user.FirstName,
                        user.LastName,
                        user.EmailAddress,
                        user.PhoneNumber);

                    return RedirectToAction("Index");
                }
                catch (Exception e)
                {
                    ModelState.AddModelError("", e.Message);
                }
            }
            else
            {
                //show error
                var errors = ModelState.Values.SelectMany(v => v.Errors);
            }
            return View(user);
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
        /// Called when a POST request is made by the create user page, with attached user data.
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
                try
                {
                    CreateUser(
                        model.Username,
                        model.FirstName,
                        model.LastName,
                        model.EmailAddress,
                        model.PhoneNumber,
                        password,
                        passwordSalt);
                    return RedirectToAction("Index");
                }
                catch(Exception e)
                {
                    ModelState.AddModelError("", e.Message);
                }
            }
            else
            {
                //show error
                var errors = ModelState.Values.SelectMany(v => v.Errors);
            }
            return View();
        }

        /// <summary>
        /// Called when a POST request is made to the create page, with an attached file.
        /// The attached file should contain new user information for bulk upload.
        /// </summary>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateBulk(HttpPostedFileBase file)
        {
            // Make sure the user is logged in
            if (!IsUserLoggedIn) return RedirectToAction("Login", "Home");

            // Create data which needs to be outside the try-ctach block
            FileCSV data = null;
            int uploadCount = 0;
            int failCount = 0;
            Downloadable errorFile = null;

            // Enter a try-catch block to make sure any exceptions are caught
            try
            {
                // Decode the CSV file
                data = new FileCSV(file);

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
                foreach (string[] row in data.Row)
                {
                    // Generate a password salt
                    // Hash the password
                    string passwordSalt = UserLogin.CreatePasswordSalt();
                    string password = UserLogin.HashPassword(row[5], passwordSalt);

                    // Create the user within the database
                    try
                    {
                        CreateUser(
                            row[0], // Username
                            row[1], // FirstName
                            row[2], // LastName
                            row[3], // Email
                            Convert.ToInt32(row[4]), // PhoneNo
                            password,
                            passwordSalt);
                        data.SetComment(row, "");
                        uploadCount++;
                    }
                    catch (Exception e)
                    {
                        data.SetComment(row, e.Message);
                        failCount++;
                    }
                }

                // Generate and record the error file, if required
                if (failCount > 0) errorFile = Downloadable.CreateCSV(data.GenerateErrorFile(), "errors.csv");
            }
            catch (Exception e)
            {
                // Record error message for View
                TempData["UploadError"] = e.Message;
            }

            // Record item counts for View
            if (uploadCount > 0) TempData["UploadCount"] = uploadCount;
            if (failCount > 0) TempData["FailCount"] = failCount;
            Session[FileCSV.SessionLabelUploadErrorLog] = (failCount > 0) ? errorFile : null;

            // All file processing has been completed
            // Go to the normal create page
            return RedirectToAction("Create", "User");
        }

        // GET: Employees/Delete/1
        [HttpGet]
        public ActionResult Delete(int? id)
        {
            if (id == null) return RedirectToAction("Index");
            int userId = (int)id;

            TCABS_DataLibrary.Models.UserModel userModel = UserProcessor.SelectUserForUserId(userId);

            UserEdit user = new UserEdit(userModel);

            return View(user);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(UserEdit user)
        {

            if (user.UserId == 1) // <-- TODO: Make this check better!!!
            {
                ModelState.AddModelError("", "Cannot delete super-user");
            }
            else
            {
                // Update the user within the database
                try
                {
                    UserProcessor.DeleteUser(user.UserId);
                    return RedirectToAction("Index");
                }
                catch (Exception e)
                {
                    //ModelState.AddModelError("", e.Message);
                    ModelState.AddModelError("", "Cannot delete this user");
                }
            }
            return View(user);
        }


    }
}

