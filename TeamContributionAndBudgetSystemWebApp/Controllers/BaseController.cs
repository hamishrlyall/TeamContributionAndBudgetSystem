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
        public List<Permission> UserPermission { get; set; }

        /// <summary>
        /// Constructor for the base controller.
        /// </summary>
        public BaseController()
        {
            //System.Diagnostics.Debug.WriteLine("BaseController()");
            // Check if user is currently logged in
            if (IsUserLoggedIn())
            {
                System.Diagnostics.Debug.WriteLine("BaseController(), logged in");
                // Get permissions for the currently logged in user
                UserPermission = GetLoggedInUserPermissions();

                // Add the menu items to the view bag
                ViewBag.MenuItems = GenerateMenuItems(UserPermission);
            }
            else
            {
                System.Diagnostics.Debug.WriteLine("BaseController(), logged out");
                // Set blank permissions, to prevent null access errors
                UserPermission = new List<Permission>();

                // Add the menu items to the view bag
                ViewBag.MenuItems = GenerateMenuItems(null);
            }
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
        /// Check if a user (any user) is logged in.
        /// </summary>
        /// <returns>True if a user is already logged in, or false if not.</returns>
        public static bool IsUserLoggedIn()
        {
            return System.Web.HttpContext.Current.User.Identity.IsAuthenticated;
        }

        /// <summary>
        /// Generate a list of menu items suitable for the top menu bar.
        /// </summary>
        /// <param name="permissions">The permission list which specifies which menu items should be available, or null if there is no user logged in.</param>
        /// <returns>List of menu items.</returns>
        public static List<MenuItem> GenerateMenuItems(List<Permission> permissions = null)
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

            // Check if permissions were provided
            if (permissions == null)
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
            else
            {
                // Permissions were provided
                // Loop through the list of permissions and add the related menu items
                foreach (Permission p in permissions)
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

        public List<Permission> GetLoggedInUserPermissions()
        {
            // Get the list of permissions for the currently logged-in user
            List<PermissionModel> permissionModels = PermissionProcessor.GetPermissionsFromUsername(GetLoggedInUsername());

            // Convert the permission list to the proper format
            List<Permission> result = new List<Permission>();
            foreach (PermissionModel p in permissionModels)
                result.Add(new Permission(p));
            return result;
        }
    }
}