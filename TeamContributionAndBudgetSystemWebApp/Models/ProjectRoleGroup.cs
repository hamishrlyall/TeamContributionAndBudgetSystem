using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TeamContributionAndBudgetSystemWebApp.Models
{
    public class ProjectRoleGroup
    {
        /// <summary>
        /// Identifier for this project role group.
        /// </summary>
        [Display(Name = "Project Role Group ID")]
        public int ProjectRoleGroupId { get; set; }

        /// <summary>
        /// Name of this project role group.
        /// </summary>
        [Display(Name = "Name")]
        [Required(ErrorMessage = "You must enter a name for the project role group.")]
        [DataType(DataType.Text)]
        [StringLength(50)]
        public string Name { get; set; }
        /// <summary>
        /// Default constructor.
        /// </summary>
        public ProjectRoleGroup() { }

        /// <summary>
        /// Constructor which copies the database model class.
        /// </summary>
        /// <param name="model"></param>
        public ProjectRoleGroup(TCABS_DataLibrary.Models.ProjectRoleGroupModel model)
        {
            ProjectRoleGroupId = model.ProjectRoleGroupId;
            Name = model.Name;
        }
    }
}