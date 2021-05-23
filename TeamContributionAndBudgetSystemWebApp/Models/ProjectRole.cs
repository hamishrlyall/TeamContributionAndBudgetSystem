using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TeamContributionAndBudgetSystemWebApp.Models
{
    public class ProjectRole
    {
        /// <summary>
        /// Identifier for this project role.
        /// </summary>
        public int ProjectRoleId { get; set; }

        /// <summary>
        /// ProjectRoleGroup to which this role belongs.
        /// </summary>
        public int ProjectRoleGroupId { get; set; }

        /// <summary>
        /// Name of this project role.
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Default constructor.
        /// </summary>
        public ProjectRole() { }

        /// <summary>
        /// Constructor which copies the database model class.
        /// </summary>
        /// <param name="model"></param>
        public ProjectRole(TCABS_DataLibrary.Models.ProjectModel model)
        {
            ProjectRoleId = model.ProjectId;
            ProjectRoleGroupId = model.ProjectRoleGroupId;
            Name = model.Name;
        }
    }
}