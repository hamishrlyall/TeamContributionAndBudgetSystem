using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TCABS_DataLibrary.Models
{
    public class ProjectRoleLinkModel
    {
        /// <summary>
        /// Identifier for this project role link.
        /// </summary>
        public int ProjectRoleLinkId { get; set; }

        /// <summary>
        /// ProjectRoleGroup which this link uses.
        /// </summary>
        public int ProjectRoleGroupId { get; set; }

        /// <summary>
        /// ProjectRole which this link uses.
        /// </summary>
        public int ProjectRoleId { get; set; }
    }
}
