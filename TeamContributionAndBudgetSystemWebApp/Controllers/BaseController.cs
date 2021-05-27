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
        private const string sessionLabelCurrentUser = "currentUser";
        private const string sessionLabelCurrentPermissions = "currentPermission";
        private const string sessionLabelMenuItems = "menuItems";

        private User currentUser;
        private List<Permission> currentPermissions;
        private List<MenuItem> menuItems;

        /// <summary>
        /// Returns true if a user (any user) is logged in.
        /// </summary>
        public static bool IsUserLoggedIn
        {
            get
            {
                return System.Web.HttpContext.Current.User.Identity.IsAuthenticated;
            }
        }

        /// <summary>
        /// Get information about the user who is currently logged on to the system.
        /// </summary>
        public User CurrentUser
        {
            get
            {
                return currentUser;
            }
        }

        /// <summary>
        /// Returns an ActionResult which redirects to the login page.
        /// Use this when the user needs to login before they can access a page.
        /// </summary>
        protected ActionResult RedirectToLogin()
        {
            return RedirectToAction("Login", "Home");
        }

        /// <summary>
        /// Returns an ActionResult which redirects to a page used to indicat that the user does not have permission for this function.
        /// </summary>
        /// <returns></returns>
        protected ActionResult RedirectToPermissionDenied()
        {
            //return new HttpStatusCodeResult(System.Net.HttpStatusCode.Forbidden);
            return RedirectToAction("Forbidden", "Home");
        }

        /// <summary>
        /// Check if the currently logged in user has permission to use a specific function.
        /// </summary>
        /// <param name="permissionName">The name used to identify the specific permission.</param>
        /// <returns>True if the user has permission, or false if they do not.</returns>
        public bool UserHasPermission(string permissionName)
        {
            return
                IsUserLoggedIn &&
                (currentPermissions != null) &&
                currentPermissions.Exists(x => x.PermissionName == permissionName); ;
        }

        /// <summary>
        /// Refresh the permission data for the currently logged-in user.
        /// Call if the permissions for the current user are changed.
        /// This will also update the top menu bar to reflect the permission changes.
        /// </summary>
        public void RefreshPermissions()
        {
            // Update the list of current permissions
            currentPermissions = (currentUser != null) ? GetCurrentPermissions(currentUser) : null;

            // Update the list of menu items for the top menu bar
            menuItems = GenerateMenuItems(currentPermissions);

            // Update session variables
            System.Web.HttpContext.Current.Session[sessionLabelCurrentPermissions] = currentPermissions;
            System.Web.HttpContext.Current.Session[sessionLabelMenuItems] = menuItems;

            // Update viewbag object
            ViewBag.MenuItems = menuItems;
        }

        /// <summary>
        /// Constructor for the base controller.
        /// This will get called one every time a page loads.
        /// </summary>
        public BaseController()
        {
            // Setup the record of the currently logged-in user, if any
            // Check if any user is currently logged in
            currentUser = null;
            if (IsUserLoggedIn)
            {
                // Get name of the logged in user
                string username = System.Web.HttpContext.Current.User.Identity.Name;

                // Try to get the user data from the http session data
                currentUser = (User)System.Web.HttpContext.Current.Session[sessionLabelCurrentUser];
                if (currentUser != null)
                {
                    // Make sure the username is expected
                    // If it is not then this maybe a security issue
                    // In which case, Burn Everything!
                    if (currentUser.Username != username) SetUserLoggedOut();
                }
                else
                {
                    // Nothing found is session data
                    // Try to get the user data from the database, using the recorded name
                    var userModel = TCABS_DataLibrary.BusinessLogic.UserProcessor.SelectUserForUsername(username);
                    if (userModel != null)
                    {
                        // Found user data
                        currentUser = new User(userModel);

                        // Update session data
                        System.Web.HttpContext.Current.Session[sessionLabelCurrentUser] = currentUser;
                    }
                }
            }

            // Setup the list of permissions and menu items
            currentPermissions = null;
            /*
            if (currentUser != null)
            {
                // Try to load the permissions and menu items from the http session data
                currentPermissions = (List<Permission>)System.Web.HttpContext.Current.Session[sessionLabelCurrentPermissions];
                menuItems = (List<MenuItem>)System.Web.HttpContext.Current.Session[sessionLabelMenuItems];

                // Check if anything went wrong
                if ((currentPermissions == null) || (menuItems == null))
                {
                    // Just reload everything fresh
                    RefreshPermissions();
                }
            }
            else
            {
                // No user is currently logged in
                // Generate default menu bar
                menuItems = GenerateMenuItems(null);
            }
            //*/
            RefreshPermissions(); // Just refresh every load, to solve issues when changing permissions

            // Update the viewbag object
            ViewBag.MenuItems = menuItems;
        }

        /// <summary>
        /// Using during the login process to indicate that a user has successfully logged in.
        /// </summary>
        /// <param name="user">The user who has successfully logged in.</param>
        public void SetUserLoggedIn(User user)
        {
            // Set authentification cookie
            System.Web.Security.FormsAuthentication.SetAuthCookie(user.Username, false);

            // Record user data
            currentUser = user;
            System.Web.HttpContext.Current.Session[sessionLabelCurrentUser] = user;

            // Update permission data
            // This will also update the menu items
            RefreshPermissions();
        }

        /// <summary>
        /// Used during the logout process to clear related user data from memory.
        /// </summary>
        public void SetUserLoggedOut()
        {
            // Clear authentification cookie
            System.Web.Security.FormsAuthentication.SignOut();

            // Clear data
            currentUser = null;
            System.Web.HttpContext.Current.Session.RemoveAll();

            // Update permission data
            // This will also update the menu items
            RefreshPermissions();
        }

        /// <summary>
        /// Get the list of permissions for a given user.
        /// </summary>
        /// <param name="user">The user to which the permissions apply.</param>
        /// <returns>A list of permissions.</returns>
        private static List<Permission> GetCurrentPermissions(User user)
        {
            // Create an empty list of permissions
            List<Permission> permissions = new List<Permission>();

            // Get the list of permissions from the database, for the currently logged-in user
            List<TCABS_DataLibrary.Models.PermissionModel> permissionModels =
                TCABS_DataLibrary.BusinessLogic.PermissionProcessor.GetPermissionsFromUsername(user.Username);

            // Convert the permission list to the proper format
            foreach (var p in permissionModels)
                permissions.Add(new Permission(p));

            // Return the list of permissions
            return permissions;
        }

        /// <summary>
        /// Generate a list of menu items suitable for the top menu bar.
        /// The method will check if any user is currently logged in and generate the menu bar accordingly.
        /// </summary>
        /// <param name="permissions">The list of permissions used to build the menu, or null if logged out.</param>
        /// <returns>List of menu items.</returns>
        private static List<MenuItem> GenerateMenuItems(List<Permission> permissions)
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
            if (permissions != null)
            {
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
    }
}