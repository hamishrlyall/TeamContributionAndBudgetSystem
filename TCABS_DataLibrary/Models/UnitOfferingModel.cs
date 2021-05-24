using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TCABS_DataLibrary.Models
{
   public class UnitOfferingModel
   {
      public int UnitOfferingId { get; set; }
      public int ConvenorId { get; set; }
      public int UnitId { get; set; }
      public int TeachingPeriodId { get; set; }
      public int YearId { get; set; }

      public virtual List<EnrollmentModel> Enrollments { get; set; }
   }
}
