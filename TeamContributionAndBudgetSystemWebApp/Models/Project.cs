using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using TCABS_DataLibrary.BusinessLogic;

namespace TeamContributionAndBudgetSystemWebApp.Models
{
   public class Project
   {
      /// <summary>
      /// Identifier for this project.
      /// </summary>
      [Display(Name = "Project ID")]
      public int ProjectId { get; set; }

      /// <summary>
      /// Name of this project.
      /// </summary>
      [Display(Name = "Name")]
      [Required(ErrorMessage = "You must enter a name for the project.")]
      [DataType(DataType.Text)]
      [StringLength(50)]
      public string Name { get; set; }

      /// <summary>
      /// Description of this project.
      /// </summary>
      [Display(Name = "Description")]
      [DataType(DataType.MultilineText)]
      public string Description { get; set; }

      /// <summary>
      /// Identifier for the ProjectRoleGroup which this project uses.
      /// </summary>
      [Display(Name = "Project Role Group")]
      public int ProjectRoleGroupId { get; set; }

      /// <summary>
      /// Name of the ProjectRoleGroup which this project uses (optional).
      /// </summary>
      [Display(Name = "Project Role Group")]
      public string ProjectRoleGroupName { get; set; }



      /// <summary>
      /// Default constructor.
      /// </summary>
      public Project() 
      {
         
      }

      /// <summary>
      /// Constructor which copies the database model class.
      /// </summary>
      /// <param name="project">ProjectModel data which will be used to setup this class.</param>
      public Project(TCABS_DataLibrary.Models.ProjectModel project)
      {
         ProjectId = project.ProjectId;
         Name = project.Name;
         Description = project.Description;
         ProjectRoleGroupId = project.ProjectRoleGroupId;
         ProjectRoleGroupName = null;
      }

      /// <summary>
      /// Constructor which copies the database model class.
      /// </summary>
      /// <param name="project">ProjectModel data which will be used to setup this class.</param>
      /// <param name="roleGroups">A list of ProjectRoleGroup which should contain the role group for this project.</param>
      public Project(TCABS_DataLibrary.Models.ProjectModel project, Dictionary<int, string> roleGroups)
      {
         ProjectId = project.ProjectId;
         Name = project.Name;
         Description = project.Description;
         ProjectRoleGroupId = project.ProjectRoleGroupId;
         if (roleGroups.TryGetValue(ProjectRoleGroupId, out string temp)) ProjectRoleGroupName = temp;
      }

      /// <summary>
      /// Constructor which copies the database model class.
      /// </summary>
      /// <param name="project">ProjectModel data which will be used to setup this class.</param>
      /// <param name="roleGroups">A list of ProjectRoleGroup which should contain the role group for this project.</param>
      public Project(TCABS_DataLibrary.Models.ProjectModel project, List<TCABS_DataLibrary.Models.ProjectRoleGroupModel> roleGroups)
      {
         ProjectId = project.ProjectId;
         Name = project.Name;
         Description = project.Description;
         ProjectRoleGroupId = project.ProjectRoleGroupId;
         if (roleGroups != null)
         {
               foreach (var r in roleGroups)
               {
                  if (r.ProjectRoleGroupId == ProjectRoleGroupId)
                  {
                     ProjectRoleGroupName = r.Name;
                     break;
                  }
               }
         }
      }

      /// <summary>
      /// Constructor which copies the database model class.
      /// </summary>
      /// <param name="project">ProjectModel data which will be used to setup this class.</param>
      /// <param name="roleGroup">A ProjectRoleGroup which should be the same as used by this project (safe if passing null).</param>
      public Project(TCABS_DataLibrary.Models.ProjectModel project, TCABS_DataLibrary.Models.ProjectRoleGroupModel roleGroup )
      {
         ProjectId = project.ProjectId;
         Name = project.Name;
         Description = project.Description;
         ProjectRoleGroupId = project.ProjectRoleGroupId;
         if (roleGroup?.ProjectRoleGroupId == ProjectRoleGroupId) ProjectRoleGroupName = roleGroup.Name;
      }
   }
}