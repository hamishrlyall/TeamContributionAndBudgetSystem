using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TeamContributionAndBudgetSystemWebApp.Models
{
    public class ProjectRoleGroup
    {
        /// <summary>
        /// Identifier for this project role group.
        /// </summary>
        public int ProjectRoleGroupId { get; set; }

        /// <summary>
        /// Name of this project role group.
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Default constructor.
        /// </summary>
        public ProjectRoleGroup() { }

        /// <summary>
        /// Constructor which copies the database model class.
        /// </summary>
        /// <param name="model"></param>
        public ProjectRoleGroup(TCABS_DataLibrary.Models.ProjectModel model)
        {
            ProjectRoleGroupId = model.ProjectRoleGroupId;
            Name = model.Name;
        }
    }
}