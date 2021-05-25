using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TeamContributionAndBudgetSystemWebApp.Models
{
    public class ProjectRole
    {
        /// <summary>
        /// Identifier for this project role.
        /// </summary>
        [Display(Name = "Project Role ID")]
        public int ProjectRoleId { get; set; }

        /// <summary>
        /// Name of this project role.
        /// </summary>
        [Display(Name = "Name")]
        [Required(ErrorMessage = "You must enter a name for the project role.")]
        [DataType(DataType.Text)]
        [StringLength(50)]
        public string Name { get; set; }
        /// <summary>
        /// Default constructor.
        /// </summary>
        public ProjectRole() { }

        /// <summary>
        /// Constructor which copies the database model class.
        /// </summary>
        /// <param name="model"></param>
        public ProjectRole(TCABS_DataLibrary.Models.ProjectRoleModel model)
        {
            ProjectRoleId = model.ProjectRoleId;
            Name = model.Name;
        }
    }
}