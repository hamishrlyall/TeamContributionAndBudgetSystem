using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TCABS_DataLibrary.BusinessLogic;
using TeamContributionAndBudgetSystemWebApp.Models;

namespace TeamContributionAndBudgetSystemWebApp.Controllers
{
    public class ProjectRoleGroupController : BaseController
    {
        /// <summary>
        /// A string to use with TempData[] for project role group error messages shown on the Index() page.
        /// </summary>
        public const string LabelError = "projectRoleGroupError";

        /// <summary>
        /// A string to use with TempData[] for project role group success messages shown on the Index() page.
        /// </summary>
        public const string LabelSuccess = "projectRoleGroupSuccess";

        /// <summary>
        /// The main page of the project role group controller.
        /// Shows a list of all project role groups in the system.
        /// </summary>
        [HttpGet]
        public ActionResult Index()
        {
            // Make sure the user is logged in and that they have permission
            if (!IsUserLoggedIn) return RedirectToLogin();
            //if (!UserHasPermission(PermissionName.UserView)) return RedirectToPermissionDenied();

            // Set the page message
            ViewBag.Message = "Project List";

            // Get all project role groups from the database
            var projectRoleGroupModels = ProjectProcessor.GetAllProjectRoleGroups();

            // Convert the list to the correct type
            List<ProjectRoleGroup> projectRoleGroups = new List<ProjectRoleGroup>();
            foreach (var p in projectRoleGroupModels) projectRoleGroups.Add(new ProjectRoleGroup(p));

            // Return the view, with the list of project role groups
            return View(projectRoleGroups);
        }

        /// <summary>
        /// The details page shows information about a specific project role group.
        /// </summary>
        /// <param name="id">The ID of the project role group to display.</param>
        [HttpGet]
        public ActionResult Details(int? id)
        {
            // Make sure the user is logged in
            if (!IsUserLoggedIn) return RedirectToLogin();

            // Check if a project role group ID was provided
            if (id == null) return RedirectToIndex();
            int projectRoleGroupId = (int)id;

            // Entry try-catch from here
            // Make sure any errors are displayed
            try
            {
                // Get the project role group data
                var projectRoleGroupModel = ProjectProcessor.GetProjectRoleGroup(projectRoleGroupId);
                if (projectRoleGroupModel == null) return RedirectToIndexIdNotFound(projectRoleGroupId);

                // Convert the model data to non-model data
                ProjectRoleGroup projectRoleGroup = new ProjectRoleGroup(projectRoleGroupModel);

                // Get list of all project roles
                var projectRoleModels = ProjectProcessor.GetAllProjectRoles();

                // Get the links which are assigned to this group
                projectRoleGroup.Links = GetLinksForGroup(projectRoleGroup.ProjectRoleGroupId, projectRoleModels);

                // Pass the data to the view
                return View(projectRoleGroup);
            }
            catch (Exception e)
            {
                return RedirectToIndex(e);
            }
        }

        /// <summary>
        /// The edit page shows information about a specific project role group.
        /// </summary>
        /// <param name="id">The ID of the project role group to display.</param>
        [HttpGet]
        public ActionResult Edit(int? id)
        {
            // Make sure the user is logged in
            if (!IsUserLoggedIn) return RedirectToLogin();

            // Check if a project role group ID was provided
            if (id == null) return RedirectToIndex();
            int projectRoleGroupId = (int)id;

            // Entry try-catch from here
            // Make sure any errors are displayed
            try
            {
                // Get the project role group data
                var projectRoleGroupModel = ProjectProcessor.GetProjectRoleGroup(projectRoleGroupId);
                if (projectRoleGroupModel == null) return RedirectToIndexIdNotFound(projectRoleGroupId);

                // Convert the model data to non-model data
                ProjectRoleGroup projectRoleGroup = new ProjectRoleGroup(projectRoleGroupModel);

                // Get list of all project roles
                var projectRoleModels = ProjectProcessor.GetAllProjectRoles();

                // Get the links which are assigned to this group
                projectRoleGroup.Links = GetLinksForGroup(projectRoleGroup.ProjectRoleGroupId, projectRoleModels);

                // Setup the dropdown list with all project roles
                SetupRoleDropDownList(projectRoleModels);

                // Pass the data to the view
                return View(projectRoleGroup);
            }
            catch (Exception e)
            {
                return RedirectToIndex(e);
            }
        }

        /// <summary>
        /// The POST edit page is called when a submit button is pressed on the project role group edit View.
        /// </summary>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ProjectRoleGroup projectRoleGroup)
        {
            try
            {
                // Check which submit button was pressed
                if (Request.Form["Save"] != null)
                {

                    // Make sure the user is logged in
                    if (!IsUserLoggedIn) return RedirectToLogin();

                    // Make sure the entered data is valid
                    if (ModelState.IsValid)
                    {
                        // Update the project role group within the database
                        try
                        {
                            ProjectProcessor.UpdateProjectRoleGroup(
                                projectRoleGroup.ProjectRoleGroupId,
                                projectRoleGroup.Name);
                            TempData[LabelSuccess] = "Updated group: " + projectRoleGroup.Name;
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
                }
                else if (Request.Form["Add"] != null)
                {
                    // Add a role link the project role group
                    try
                    {
                        ProjectProcessor.CreateProjectRoleLink(
                            projectRoleGroup.AddLink,
                            projectRoleGroup.ProjectRoleGroupId);
                        ViewBag.Added = "Added project role to group";
                    }
                    catch (Exception e)
                    {
                        ViewBag.AddFail = e.Message;
                    }
                }
                else
                {
                    // Remove a role link the project role group
                    try
                    {
                        const string marker = "Delete.";
                        foreach (string key in Request.Form.AllKeys)
                        {
                            if (key.StartsWith(marker))
                            {
                                ProjectProcessor.DeleteProjectRoleLink(Convert.ToInt32(key.Substring(marker.Length)));
                                ViewBag.Removed = "Removed project role from group";
                                break;
                            }
                        }
                    }
                    catch (Exception e)
                    {
                        ViewBag.RemoveFail = e.Message;
                    }
                }

                // Get the project role group data
                var projectRoleGroupModel = ProjectProcessor.GetProjectRoleGroup(projectRoleGroup.ProjectRoleGroupId);
                if (projectRoleGroupModel == null) return RedirectToIndexIdNotFound(projectRoleGroup.ProjectRoleGroupId);

                // Convert the model data to non-model data
                projectRoleGroup = new ProjectRoleGroup(projectRoleGroupModel);

                // Get list of all project roles
                var projectRoleModels = ProjectProcessor.GetAllProjectRoles();

                // Get the links which are assigned to this group
                projectRoleGroup.Links = GetLinksForGroup(projectRoleGroup.ProjectRoleGroupId, projectRoleModels);

                // Setup the dropdown list with all project roles
                SetupRoleDropDownList(projectRoleModels);

                // Return to same page with same data
                return View(projectRoleGroup);
            }
            catch (Exception e)
            {
                return RedirectToIndex(e);
            }
        }

        /// <summary>
        /// The delete page shows a confirmation message about deleting the project role group.
        /// </summary>
        /// <param name="id">The ID of the project role group to display.</param>
        [HttpGet]
        public ActionResult Delete(int? id)
        {
            // Make sure the user is logged in
            if (!IsUserLoggedIn) return RedirectToLogin();

            // Check if a project role group ID was provided
            if (id == null) return RedirectToIndex();
            int projectRoleGroupId = (int)id;

            // Entry try-catch from here
            // Make sure any errors are displayed
            try
            {
                // Get the project role group data
                var projectRoleGroupModel = ProjectProcessor.GetProjectRoleGroup(projectRoleGroupId);
                if (projectRoleGroupModel == null) return RedirectToIndexIdNotFound(projectRoleGroupId);

                // Convert the model data to non-model data
                // Pass the data to the view
                return View(new ProjectRoleGroup(projectRoleGroupModel));
            }
            catch (Exception e)
            {
                return RedirectToIndex(e);
            }
        }

        /// <summary>
        /// The POST edit page is called when a submit button is pressed on the project role group edit View.
        /// </summary>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(ProjectRoleGroup projectRoleGroup)
        {
            // Make sure the user is logged in
            if (!IsUserLoggedIn) return RedirectToLogin();

            // Delete the project from the database
            try
            {
                ProjectProcessor.DeleteProjectRoleGroup(projectRoleGroup.ProjectRoleGroupId);
                TempData[LabelSuccess] = "Deleted group: " + projectRoleGroup.Name;
                return RedirectToIndex();
            }
            catch (Exception e)
            {
                return RedirectToIndex(e);
            }

        }

        /// <summary>
        /// The create page is used to create a new project role group.
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
        /// The POST create page is called when a submit button is pressed on the project role group create View.
        /// </summary>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ProjectRoleGroup projectRoleGroup)
        {
            // Make sure the entered data is valid
            if (ModelState.IsValid)
            {
                // Update the project within the database
                try
                {
                    ProjectProcessor.CreateProjectRoleGroup(projectRoleGroup.Name);
                    TempData[LabelSuccess] = "Created new group: " + projectRoleGroup.Name;
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
            return View(projectRoleGroup);
        }

        /// <summary>
        /// Get a list of all project role links which use the given project role group.
        /// </summary>
        private List<ProjectRoleLink> GetLinksForGroup(int projectRoleGroupId, List<TCABS_DataLibrary.Models.ProjectRoleModel> projectRoleModels)
        {
            // Get all project role links which use this project role group
            var linkModels = ProjectProcessor.GetProjectRoleLinksForGroup(projectRoleGroupId);

            // Create a new list of projet role links (non-model)
            List<ProjectRoleLink> links = new List<ProjectRoleLink>();
            foreach (var linkModel in linkModels)
            {
                // Create new link data
                ProjectRoleLink link = new ProjectRoleLink(linkModel);

                // Fill in the role name field within the link data
                foreach (var roleModel in projectRoleModels)
                {
                    if (link.ProjectRoleId == roleModel.ProjectRoleId)
                    {
                        link.ProjectRoleName = roleModel.Name;
                        break;
                    }
                }

                // Add link to list
                links.Add(link);
            }
            return links;
        }

        /// <summary>
        /// Store all project roles within a dropdown list.
        /// </summary>
        private void SetupRoleDropDownList(List<TCABS_DataLibrary.Models.ProjectRoleModel> projectRoleModels)
        {
            // Store the data for the dropdown list
            ViewBag.Roles = new SelectList(projectRoleModels, "ProjectRoleId", "Name");
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
        /// <param name="projectRoleGroupId">The project role (ID) which could no be found.</param>
        private ActionResult RedirectToIndexIdNotFound(int projectRoleGroupId)
        {
            TempData[LabelError] = "A project-role-group with ID:" + projectRoleGroupId + " does not appear to exist";
            return RedirectToIndex();
        }

    }
}