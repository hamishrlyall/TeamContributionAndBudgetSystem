using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TCABS_DataLibrary.Models
{
    public class ProjectModel
    {
        /// <summary>
        /// Identifier for this project.
        /// </summary>
        public int ProjectId { get; set; }

        /// <summary>
        /// Name of this project.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Description of this project.
        /// </summary>
        public string Description { get; set; }
        
        /// <summary>
        /// ProjectRoleGroup which this project uses.
        /// </summary>
        public int ProjectRoleGroupId { get; set; }
    }
}
