using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using TCABS_DataLibrary.BusinessLogic;
using TeamContributionAndBudgetSystemWebApp.Models;

namespace TeamContributionAndBudgetSystemWebApp.Controllers
{
    /// <summary>
     /// Used for task task related pages.
     /// </summary>
    public class ProjectTaskController : BaseController
    {
        /// <summary>
        /// A string to use with TempData[] for project task error messages shown on the Index() page.
        /// </summary>
        public const string LabelError = "projectTaskError";

        /// <summary>
        /// A string to use with TempData[] for project task success messages shown on the Index() page.
        /// </summary>
        public const string LabelSuccess = "projectTaskSuccess";

        /// <summary>
        /// The main page of the project task controller.
        /// Shows a list of all project tasks in the system.
        /// </summary>
        [HttpGet]
        public ActionResult Index()
        {
            // Make sure the user is logged in and that they have permission
            if (!IsUserLoggedIn) return RedirectToLogin();
            //if (!UserHasPermission(PermissionName.UserView)) return RedirectToPermissionDenied();

            // Set the page message
            ViewBag.Message = "Project Task List";

            // Get all project tasks from the database
            var projectTaskModels = ProjectTaskProcessor.GetAllProjectTasks();

            // Convert the list to the correct type
            List<ProjectTask> projectTasks = new List<ProjectTask>();
            foreach (var p in projectTaskModels) projectTasks.Add(new ProjectTask(p));

            // Return the view, with the list of project tasks
            return View(projectTasks);
        }

        /// <summary>
        /// The details page shows information about a specific project task.
        /// </summary>
        /// <param name="id">The ID of the project task to display.</param>
        [HttpGet]
        public ActionResult Details(int? id)
        {
            // Make sure the user is logged in
            if (!IsUserLoggedIn) return RedirectToLogin();

            // Check if a project task ID was provided
            if (id == null) return RedirectToIndex();
            int projectTaskId = (int)id;

            // Entry try-catch from here
            // Make sure any errors are displayed
            try
            {
                // Get the project task data
                var projectTaskModel = ProjectTaskProcessor.GetProjectTask(projectTaskId);
                if (projectTaskModel == null) return RedirectToIndexIdNotFound(projectTaskId);

                // Convert the model data to non-model data
                var projectTask = new ProjectTask(projectTaskModel)
                {
                    // Fill in missing text data
                    ProjectRoleName = ProjectRoleProcessor.GetProjectRole(projectTaskModel.ProjectRoleId)?.Name,
                    StudentUsername = UserProcessor.SelectUserForEnrollment(projectTaskModel.EnrollmentId)?.Username
                };

                // Pass the data to the view
                return View(projectTask);
            }
            catch (Exception e)
            {
                return RedirectToIndex(e);
            }
        }

        /// <summary>
        /// The edit page shows information about a specific project task.
        /// </summary>
        /// <param name="id">The ID of the project task to display.</param>
        [HttpGet]
        public ActionResult Edit(int? id)
        {
            // Make sure the user is logged in
            if (!IsUserLoggedIn) return RedirectToLogin();

            // Check if a project task ID was provided
            if (id == null) return RedirectToIndex();
            int projectTaskId = (int)id;

            // Entry try-catch from here
            // Make sure any errors are displayed
            try
            {
                // Get the project task data
                var projectTaskModel = ProjectTaskProcessor.GetProjectTask(projectTaskId);
                if (projectTaskModel == null) return RedirectToIndexIdNotFound(projectTaskId);
                
                // Convert the model data to non-model data
                var projectTask = new ProjectTask(projectTaskModel)
                {
                    // Fill in missing text data
                    StudentUsername = UserProcessor.SelectUserForEnrollment(projectTaskModel.EnrollmentId)?.Username
                };

                // Get all possible roles which could be assigned to this task
                var projectRoles = ProjectRoleProcessor.GetProjectRolesForEnrollment(projectTaskModel.EnrollmentId);

                // Setup the project role drop-down list
                SetupRoleDropDownList(projectRoles, projectTask.ProjectRoleId);

                // Pass the data to the view
                return View(projectTask);
            }
            catch (Exception e)
            {
                return RedirectToIndex(e);
            }
        }

        /// <summary>
        /// The POST edit page is called when a submit button is pressed on the project task edit View.
        /// </summary>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ProjectTask projectTask)
        {
            // Make sure the entered data is valid
            if (ModelState.IsValid)
            {
                // Update the project task within the database
                try
                {
                    ProjectTaskProcessor.UpdateProjectTask(
                        projectTask.ProjectTaskId,
                        projectTask.Description,
                        projectTask.ProjectRoleId,
                        projectTask.Duration);
                    TempData[LabelSuccess] = "Updated task.";
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

            // Get all possible roles which could be assigned to this task
            var projectRoles = ProjectRoleProcessor.GetProjectRolesForEnrollment(projectTask.EnrollmentId);

            // Setup the project role drop-down list
            SetupRoleDropDownList(projectRoles, projectTask.ProjectRoleId);

            // Return to same page with same data
            return View(projectTask);
        }

        /// <summary>
        /// The create page is used to create a new project task.
        /// </summary>
        [HttpGet]
        public ActionResult Create()
        {
            // Make sure the user is logged in
            if (!IsUserLoggedIn) return RedirectToLogin();

            // Get the list of projects which can be accessed by the current user
            var projectModels = ProjectOfferingProcessor.GetProjectOfferingsForUserId(CurrentUser.UserId);

            // Setup the project drop-down list
            ViewBag.Projects = new SelectList(projectModels, "ProjectId", "Name");

            // Create a project task
            ProjectTask projectTask = new ProjectTask();

            // Go to view
            return View(projectTask);
        }

        /// <summary>
        /// The POST create page is called when a submit button is pressed on the project task create View.
        /// </summary>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ProjectTask projectTask)
        {
            // Make sure the entered data is valid
            if (ModelState.IsValid)
            {
                // Update the project within the database
                try
                {
                    ProjectTaskProcessor.CreateProjectTask(
                        projectTask.EnrollmentId,
                        projectTask.Description,
                        projectTask.ProjectRoleId,
                        projectTask.Duration);
                    TempData[LabelSuccess] = "Created new task: " + projectTask.Description;
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
            return View(projectTask);
        }

        /// <summary>
        /// Store a list of project roles within the dropdown list.
        /// </summary>
        private void SetupRoleDropDownList(List<TCABS_DataLibrary.Models.ProjectRoleModel> projectRoleModels, int selectedProjectRoleId)
        {
            // Store the data for the dropdown list
            ViewBag.Roles = new SelectList(projectRoleModels, "ProjectRoleId", "Name", selectedProjectRoleId);
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
        /// Also displays an error message saying that the requested project task does not exist.
        /// </summary>
        /// <param name="projectTaskId">The project task (ID) which could no be found.</param>
        private ActionResult RedirectToIndexIdNotFound(int projectTaskId)
        {
            TempData[LabelError] = "A project-task with ID:" + projectTaskId + " does not appear to exist";
            return RedirectToIndex();
        }


    }
}