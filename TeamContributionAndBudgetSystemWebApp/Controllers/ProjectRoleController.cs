using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TCABS_DataLibrary.BusinessLogic;
using TeamContributionAndBudgetSystemWebApp.Models;

namespace TeamContributionAndBudgetSystemWebApp.Controllers
{
    public class ProjectRoleController : BaseController
    {
        /// <summary>
        /// A string to use with TempData[] for project role error messages shown on the Index() page.
        /// </summary>
        public const string LabelError = "projectRoleError";

        /// <summary>
        /// A string to use with TempData[] for project role success messages shown on the Index() page.
        /// </summary>
        public const string LabelSuccess = "projectRoleSuccess";

        /// <summary>
        /// The main page of the project role controller.
        /// Shows a list of all project roles in the system.
        /// </summary>
        [HttpGet]
        public ActionResult Index()
        {
            // Make sure the user is logged in and that they have permission
            if (!IsUserLoggedIn) return RedirectToLogin();
            //if (!UserHasPermission(PermissionName.UserView)) return RedirectToPermissionDenied();

            // Set the page message
            ViewBag.Message = "Project Role List";

            // Get all project roles from the database
            var projectRoleModels = ProjectRoleProcessor.GetAllProjectRoles();

            // Convert the list to the correct type
            List<ProjectRole> projectRoles = new List<ProjectRole>();
            foreach (var p in projectRoleModels) projectRoles.Add(new ProjectRole(p));

            // Return the view, with the list of project roles
            return View(projectRoles);
        }

        /// <summary>
        /// The edit page shows information about a specific project role.
        /// </summary>
        /// <param name="id">The ID of the project role to display.</param>
        [HttpGet]
        public ActionResult Edit(int? id)
        {
            // Make sure the user is logged in
            if (!IsUserLoggedIn) return RedirectToLogin();

            // Check if a project role ID was provided
            if (id == null) return RedirectToIndex();
            int projectRoleId = (int)id;

            // Entry try-catch from here
            // Make sure any errors are displayed
            try
            {
                // Get the project role data
                var projectRoleModel = ProjectRoleProcessor.GetProjectRole(projectRoleId);
                if (projectRoleModel == null) return RedirectToIndexIdNotFound(projectRoleId);

                // Convert the model data to non-model data
                // Pass the data to the view
                return View(new ProjectRole(projectRoleModel));
            }
            catch (Exception e)
            {
                return RedirectToIndex(e);
            }
        }

        /// <summary>
        /// The POST edit page is called when a submit button is pressed on the project role edit View.
        /// </summary>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ProjectRole projectRole)
        {
            // Make sure the entered data is valid
            if (ModelState.IsValid)
            {
                // Update the project role within the database
                try
                {
                    ProjectRoleProcessor.UpdateProjectRole(
                        projectRole.ProjectRoleId,
                        projectRole.Name);
                    TempData[LabelSuccess] = "Updated role: " + projectRole.Name;
                    return RedirectToIndex();
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

            // Return to same page with same data
            return View(projectRole);
        }

        /// <summary>
        /// The delete page shows a confirmation message about deleting the project role.
        /// </summary>
        /// <param name="id">The ID of the project role to display.</param>
        [HttpGet]
        public ActionResult Delete(int? id)
        {
            // Make sure the user is logged in
            if (!IsUserLoggedIn) return RedirectToLogin();

            // Check if a project role ID was provided
            if (id == null) return RedirectToIndex();
            int projectRoleId = (int)id;

            // Entry try-catch from here
            // Make sure any errors are displayed
            try
            {
                // Get the project role data
                var projectRoleModel = ProjectRoleProcessor.GetProjectRole(projectRoleId);
                if (projectRoleModel == null) return RedirectToIndexIdNotFound(projectRoleId);

                // Convert the model data to non-model data
                // Pass the data to the view
                return View(new ProjectRole(projectRoleModel));
            }
            catch (Exception e)
            {
                return RedirectToIndex(e);
            }
        }

        /// <summary>
        /// The POST edit page is called when a submit button is pressed on the project role edit View.
        /// </summary>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(ProjectRole projectRole)
        {
            // Make sure the user is logged in
            if (!IsUserLoggedIn) return RedirectToLogin();

            // Delete the project from the database
            try
            {
                ProjectRoleProcessor.DeleteProjectRole(projectRole.ProjectRoleId);
                TempData[LabelSuccess] = "Deleted role: " + projectRole.Name;
                return RedirectToIndex();
            }
            catch (Exception e)
            {
                return RedirectToIndex(e);
            }

        }

        /// <summary>
        /// The create page is used to create a new project role.
        /// </summary>
        [HttpGet]
        public ActionResult Create()
        {
            // Make sure the user is logged in
            if (!IsUserLoggedIn) return RedirectToLogin();

            // Go to view
            return View();
        }

        /// <summary>
        /// The POST create page is called when a submit button is pressed on the project role create View.
        /// </summary>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ProjectRole projectRole)
        {
            // Make sure the entered data is valid
            if (ModelState.IsValid)
            {
                // Update the project within the database
                try
                {
                    ProjectRoleProcessor.CreateProjectRole(projectRole.Name);
                    TempData[LabelSuccess] = "Created new role: " + projectRole.Name;
                    return RedirectToIndex();
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

            // Return to same page with same data
            return View(projectRole);
        }

        /// <summary>
        /// A shorthand method for getting a RedirectToAction() leading to the index page.
        /// </summary>
        private ActionResult RedirectToIndex()
        {
            return RedirectToAction("Index");
        }

        /// <summary>
        /// A shorthand method for getting a RedirectToAction() leading to the index page.
        /// Also displays an error message based on the provided exception.
        /// </summary>
        private ActionResult RedirectToIndex(Exception e)
        {
            TempData[LabelError] = e.Message;
            return RedirectToIndex();
        }

        /// <summary>
        /// A shorthand method for getting a RedirectToAction() leading to the index page.
        /// Also displays an error message saying that the requested project role does not exist.
        /// </summary>
        /// <param name="projectRoleId">The project role (ID) which could no be found.</param>
        private ActionResult RedirectToIndexIdNotFound(int projectRoleId)
        {
            TempData[LabelError] = "A project-role with ID:" + projectRoleId + " does not appear to exist";
            return RedirectToIndex();
        }

    }
}