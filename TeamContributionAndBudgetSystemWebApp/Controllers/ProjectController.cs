using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TCABS_DataLibrary.BusinessLogic;
using TeamContributionAndBudgetSystemWebApp.Models;

namespace TeamContributionAndBudgetSystemWebApp.Controllers
{
    /// <summary>
    /// Used for project related pages.
    /// </summary>
    public class ProjectController : BaseController
    {
        /// <summary>
        /// A string to use with TempData[] for project error messages shown on the Index() page.
        /// </summary>
        public const string LabelError = "projectError";

        /// <summary>
        /// The main page of the project controller.
        /// Shows a list of all projects in the system.
        /// </summary>
        [HttpGet]
        public ActionResult Index()
        {
            // Make sure the user is logged in and that they have permission
            if (!IsUserLoggedIn) return RedirectToLogin();
            //if (!UserHasPermission(PermissionName.UserView)) return RedirectToPermissionDenied();

            // Set the page message
            ViewBag.Message = "Project List";

            // Get all projects from the database
            var projectModels = ProjectProcessor.GetAllProjects();

            // Get all project role groups from the database
            // Convert the list to a dictionary
            var projectRoleGroupModels = ProjectProcessor.GetAllProjectRoleGroups();
            Dictionary<int, string> projectRoleGroup = null;
            if (projectRoleGroupModels != null)
            {
                projectRoleGroup = new Dictionary<int, string>();
                foreach (var i in projectRoleGroupModels)
                    projectRoleGroup.Add(i.ProjectRoleGroupId, i.Name);
            }

            // Create the project list
            List<Project> projects = new List<Project>();
            if (projectRoleGroup != null)
                foreach (var p in projectModels) projects.Add(new Project(p, projectRoleGroup));
            else
                foreach (var p in projectModels) projects.Add(new Project(p));

            // Make sure the project descriptions are not too long
            const int maxDescriptionLength = 40;
            foreach (var p in projects)
            {
                if ((p.Description != null) && (p.Description.Length > maxDescriptionLength))
                    p.Description = p.Description.Substring(0, maxDescriptionLength - 3) + "...";
            }

            // Return the view, with the list of projects
            return View(projects);
        }

        /// <summary>
        /// The details page shows information about a specific project.
        /// </summary>
        /// <param name="id">The ID of the project to display.</param>
        [HttpGet]
        public ActionResult Details(int? id)
        {
            // Make sure the user is logged in
            if (!IsUserLoggedIn) return RedirectToLogin();

            // Check if a project ID was provided
            if (id == null) return RedirectToIndex();
            int projectId = (int)id;

            // Entry try-catch from here
            // Make sure any errors are displayed
            try
            {
                // Get the project data
                var projectModel = ProjectProcessor.GetProject(projectId);
                if (projectModel == null) return RedirectToIndexProjectNotFound(projectId);

                // Get name of assigned project role group
                var roleGroup = ProjectProcessor.GetProjectRoleGroup(projectModel.ProjectRoleGroupId);

                var teams = TeamProcessor.SelectTeamsForProjectId( projectId );

                // Convert the model data to non-model data
                // Pass the data to the view
                return View(new Project(projectModel, roleGroup, teams));
            }
            catch (Exception e)
            {
                return RedirectToIndex(e);
            }
        }

        /// <summary>
        /// The edit page shows information about a specific project.
        /// </summary>
        /// <param name="id">The ID of the project to display.</param>
        [HttpGet]
        public ActionResult Edit(int? id)
        {
            // Make sure the user is logged in
            if (!IsUserLoggedIn) return RedirectToLogin();

            // Check if a project ID was provided
            if (id == null) return RedirectToIndex();
            int projectId = (int)id;

            // Entry try-catch from here
            // Make sure any errors are displayed
            try
            {
                // Get the project data
                var projectModel = ProjectProcessor.GetProject(projectId);
                if (projectModel == null) return RedirectToIndexProjectNotFound(projectId);

                // Get all project role groups from the database
                var projectRoleGroupModels = ProjectProcessor.GetAllProjectRoleGroups();

                // Get list of available project role groups
                // Store it in a list for a drop-down-list
                ViewBag.RoleGroup = new SelectList(projectRoleGroupModels, "ProjectRoleGroupId", "Name", projectModel.ProjectRoleGroupId);

                // Convert the model data to non-model data
                // Pass the data to the view
                return View(new Project(projectModel, projectRoleGroupModels));
            }
            catch (Exception e)
            {
                return RedirectToIndex(e);
            }
        }

        /// <summary>
        /// The POST edit page is called when a submit button is pressed on the project edit View.
        /// </summary>
        /// <param name="id">The project information as returned by the View.</param>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Project project)
        {
            // Make sure the entered data is valid
            if (ModelState.IsValid)
            {
                // Update the project within the database
                try
                {
                    ProjectProcessor.UpdateProject(
                        project.ProjectId,
                        project.Name,
                        project.Description,
                        project.ProjectRoleGroupId);

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

            // Get list of available project role groups
            // Store it in a list for a drop-down-list
            ViewBag.RoleGroup = new SelectList(ProjectProcessor.GetAllProjectRoleGroups(), "ProjectRoleGroupId", "Name", project.ProjectRoleGroupId);

            // Return to same page with same data
            return View(project);
        }

        /// <summary>
        /// The delete page shows a confirmation message about deleting the project.
        /// </summary>
        /// <param name="id">The ID of the project to display.</param>
        [HttpGet]
        public ActionResult Delete(int? id)
        {
            // Make sure the user is logged in
            if (!IsUserLoggedIn) return RedirectToLogin();

            // Check if a project ID was provided
            if (id == null) return RedirectToIndex();
            int projectId = (int)id;

            // Entry try-catch from here
            // Make sure any errors are displayed
            try
            {
                // Get the project data
                var projectModel = ProjectProcessor.GetProject(projectId);
                if (projectModel == null) return RedirectToIndexProjectNotFound(projectId);

                // Convert the model data to non-model data
                // Pass the data to the view
                return View(new Project(projectModel));
            }
            catch (Exception e)
            {
                return RedirectToIndex(e);
            }
        }

        /// <summary>
        /// The POST edit page is called when a submit button is pressed on the project edit View.
        /// </summary>
        /// <param name="id">The project information as returned by the View.</param>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(Project project)
        {
            // Make sure the user is logged in
            if (!IsUserLoggedIn) return RedirectToLogin();

            // TODO: Need to add some checking before Delete occurs
            // Cannot delete a project if there is a team assigned.

            // Delete the project from the database
            try
            {
                ProjectProcessor.DeleteProject(project.ProjectId);
                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                return RedirectToIndex(e);
            }

        }

        /// <summary>
        /// The create page is used to create a new project.
        /// </summary>
        /// <param name="id">The ID of the project to display.</param>
        [HttpGet]
        public ActionResult Create()
        {
            // Make sure the user is logged in
            if (!IsUserLoggedIn) return RedirectToLogin();

            // Get list of available project role groups
            // Store it in a list for a drop-down-list
            ViewBag.RoleGroup = new SelectList(ProjectProcessor.GetAllProjectRoleGroups(), "ProjectRoleGroupId", "Name", null);

            // Go to view
            return View();
        }

        /// <summary>
        /// The POST create page is called when a submit button is pressed on the project create View.
        /// </summary>
        /// <param name="id">The project information as returned by the View.</param>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Project project)
        {
            // Make sure the entered data is valid
            if (ModelState.IsValid)
            {
                // Update the project within the database
                try
                {
                    ProjectProcessor.CreateProject(
                        project.Name,
                        project.Description,
                        project.ProjectRoleGroupId);

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

            // Get list of available project role groups
            // Store it in a list for a drop-down-list
            ViewBag.RoleGroup = new SelectList(ProjectProcessor.GetAllProjectRoleGroups(), "ProjectRoleGroupId", "Name", project.ProjectRoleGroupId);

            // Return to same page with same data
            return View(project);
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
        /// Also displays an error message saying that the requested project does not exist.
        /// </summary>
        /// <param name="projectId">The project (ID) which could no be found.</param>
        private ActionResult RedirectToIndexProjectNotFound(int projectId)
        {
            TempData[LabelError] = "A project with ID:" + projectId + " does not appear to exist";
            return RedirectToIndex();
        }

    }
}