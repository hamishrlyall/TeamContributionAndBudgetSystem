using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TeamContributionAndBudgetSystemWebApp.Models
{
    public class ProjectRoleLink
    {
        /// <summary>
        /// Identifier for this project role link.
        /// </summary>
        public int ProjectRoleLinkId { get; set; }

        /// <summary>
        /// Identifier for the ProjectRole which this link uses.
        /// </summary>
        [Display(Name = "Project Role")]
        public int ProjectRoleId { get; set; }

        /// <summary>
        /// Name of the ProjectRole which this link uses (optional).
        /// </summary>
        [Display(Name = "Project Role")]
        public string ProjectRoleName { get; set; }

        /// <summary>
        /// Identifier for the ProjectRoleGroup which this link uses.
        /// </summary>
        [Display(Name = "Project Role Group")]
        public int ProjectRoleGroupId { get; set; }

        /// <summary>
        /// Name of the ProjectRoleGroup which this link uses (optional).
        /// </summary>
        [Display(Name = "Project Role Group")]
        public string ProjectRoleGroupName { get; set; }

        /// <summary>
        /// Default constructor.
        /// </summary>
        public ProjectRoleLink() { }

        /// <summary>
        /// Constructor which copies the database model class.
        /// </summary>
        /// <param name="project">ProjectModel data which will be used to setup this class.</param>
        public ProjectRoleLink(TCABS_DataLibrary.Models.ProjectRoleLinkModel projectRoleLink)
        {
            ProjectRoleLinkId = projectRoleLink.ProjectRoleLinkId;
            ProjectRoleId = projectRoleLink.ProjectRoleId;
            ProjectRoleName = null;
            ProjectRoleGroupId = projectRoleLink.ProjectRoleGroupId;
            ProjectRoleGroupName = null;
        }

    }
}