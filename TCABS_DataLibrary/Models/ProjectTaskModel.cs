using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TCABS_DataLibrary.Models
{
    public class ProjectTaskModel
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
        /// ProjectRole which this task is assigned to.
        /// </summary>
        public int ProjectRoleId { get; set; }

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
    }
}
