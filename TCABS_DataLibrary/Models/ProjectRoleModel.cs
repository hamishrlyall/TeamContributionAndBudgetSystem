using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TCABS_DataLibrary.Models
{
    public class ProjectRoleModel
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
    }
}
