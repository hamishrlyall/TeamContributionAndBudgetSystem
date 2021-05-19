using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TCABS_DataLibrary.Models
{
   public class EnrollmentModel
   {
      public int EnrollmentId { get; set; }
      public int UserId { get; set; }
      public int UnitOfferingId { get; set; }
      public int TeamId { get; set; }
   }
}
