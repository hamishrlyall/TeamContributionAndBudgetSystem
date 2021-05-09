using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TCABS_DataLibrary.BusinessLogic;
using TCABS_DataLibrary.Models;
using TeamContributionAndBudgetSystemWebApp.Models;

namespace TeamContributionAndBudgetSystemWebApp.Controllers
{
    /// <summary>
    /// This base class contains functionality which is common to all controllers.
    /// Other controller classes should inherit this class to use the functionality.
    /// This includes information used by the _Layout.cshtml page.
    /// </summary>
    public abstract class BaseController : Controller
    {
        /// <summary>
        /// Check if a user (any user) is logged in.
        /// </summary>
        /// <returns>True if a user is already logged in, or false if not.</returns>
        public static bool IsUserLoggedIn()
        {
            return System.Web.HttpContext.Current.User.Identity.IsAuthenticated;
        }

        /// <summary>
        /// Get the username of the currently logged-in user, if any.
        /// </summary>
        /// <returns>The user's username, or null if no user is logged in.</returns>
        public static string GetLoggedInUsername()
        {
            return System.Web.HttpContext.Current.User.Identity.Name;
        }

        /// <summary>
        /// Check if the currently logged in user has permission to use a specific function.
        /// </summary>
        /// <param name="permissionName">The name used to identify the specific permission.</param>
        /// <returns>True if the user has permission, or false if they do not.</returns>
        public bool UserHasPermission(string permissionName)
        {
            return UserPermissions.Exists(x => x.PermissionName == permissionName);
        }

        /// <summary>
        /// Send a CSV file to the client browser as a download.
        /// </summary>
        /// <param name="fileContent">A string representing the content of the CSV file.</param>
        /// <param name="fileName">The name of the file.</param>
        public void SendFileCSV(string fileContent, string fileName)
        {
            Response.Clear(); // Clear everything and start from a clean slate
            Response.Buffer = true;
            Response.AppendHeader("content-disposition", "attachment; filename=" + fileName);
            Response.AppendHeader("content-length", fileContent.Length.ToString());
            Response.ContentType = "text/csv";
            Response.Output.Write(fileContent);
            Response.Flush();
            Response.End();
        }

        /// <summary>
        /// Generate a list of menu items suitable for the top menu bar.
        /// The method will check if any user is currently logged in and generate the menu bar accordingly.
        /// </summary>
        /// <returns>List of menu items.</returns>
        private List<MenuItem> GenerateMenuItems()
        {
            // Create list of menu items
            // Add a link to the home page
            List<MenuItem> menuItems = new List<MenuItem>() {
                new MenuItem()
                {
                    Title = "Home",
                    Page = "Index",
                    Controller = "Home"
                }
            };

            // Check if a user is logged in
            if (IsUserLoggedIn())
            {
                // Loop through the list of permissions and add the related menu items
                foreach (Permission p in UserPermissions)
                {
                    if (p.IsValidLink()) menuItems.Add(new MenuItem(p));
                }

                // Add a menu item for the logout page
                menuItems.Add(new MenuItem()
                {
                    Title = "Logout",
                    Page = "Logout",
                    Controller = "Home"
                });
            }
            else
            {
                // No permissions provided
                // Generate the "logged out" verions of the menu bar
                // Add a menu item for the login page
                menuItems.Add(new MenuItem()
                {
                    Title = "Login",
                    Page = "Login",
                    Controller = "Home"
                });
            }

            // Add links for the public pages
            menuItems.Add(new MenuItem()
            {
                Title = "Contact",
                Page = "Contact",
                Controller = "Home"
            });
            menuItems.Add(new MenuItem()
            {
                Title = "About",
                Page = "About",
                Controller = "Home"
            });
            return menuItems;
        }

        /// <summary>
        /// Returns the list of permissions for the currently logged in user.
        /// </summary>
        private List<Permission> GetLoggedInUserPermissions()
        {
            // Get the list of permissions for the currently logged-in user
            List<PermissionModel> permissionModels = PermissionProcessor.GetPermissionsFromUsername(GetLoggedInUsername());

            // Convert the permission list to the proper format
            List<Permission> result = new List<Permission>();
            foreach (PermissionModel p in permissionModels)
                result.Add(new Permission(p));
            return result;
        }

        /// <summary>
        /// Constructor for the base controller.
        /// This will get called one every time a page loads.
        /// </summary>
        public BaseController()
        {
            // Set the list of user permissions for the currently logged in user
            UserPermissions = IsUserLoggedIn() ? GetLoggedInUserPermissions() : new List<Permission>();

            // Add the menu items to the view bag
            ViewBag.MenuItems = GenerateMenuItems();
        }

        /// <summary>
        /// List of permissions for the currently logged-in user.
        /// If no user is logged-in then this list will be empty.
        /// </summary>
        public List<Permission> UserPermissions { get; }
    }
}