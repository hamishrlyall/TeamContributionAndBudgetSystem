using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TeamContributionAndBudgetSystemWebApp.Models
{
    public class ProjectTask
    {
        /// <summary>
        /// Identifier for this task.
        /// </summary>
        public int ProjectTaskId { get; set; }

        /// <summary>
        /// Description of this task.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Enrollment of the student who created this task.
        /// </summary>
        public int EnrollmentId { get; set; }

        /// <summary>
        /// (optional) Username of the student who created this task.
        /// </summary>
        public string StudentUsername { get; set; }

        /// <summary>
        /// ProjectRole which this task is assigned to.
        /// </summary>
        public int ProjectRoleId { get; set; }

        /// <summary>
        /// (optional) Name of the ProjectRole which this task is assigned to.
        /// </summary>
        public string ProjectRoleName { get; set; }

        /// <summary>
        /// Duration of this task in minutes.
        /// </summary>
        public int Duration { get; set; }

        /// <summary>
        /// True if this task has been approved.
        /// </summary>
        public bool Approved { get; set; }

        /// <summary>
        /// True if this task has been modified after having been approved.
        /// </summary>
        public bool Modified { get; set; }

        /// <summary>
        /// Default constructor.
        /// </summary>
        public ProjectTask() {
            Approved = false;
            Modified = false;
            Duration = 0;
        }

        /// <summary>
        /// Constructor which copies the database model class.
        /// </summary>
        /// <param name="model"></param>
        public ProjectTask(TCABS_DataLibrary.Models.ProjectTaskModel model)
        {
            ProjectTaskId = model.ProjectTaskId;
            Description = model.Description;
            EnrollmentId = model.EnrollmentId;
            ProjectRoleId = model.ProjectRoleId;
            Duration = model.Duration;
            Approved = model.Approved;
            Modified = model.Modified;
        }
    }
}