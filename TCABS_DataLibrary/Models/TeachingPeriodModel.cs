using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TCABS_DataLibrary.Models
{
   public class TeachingPeriodModel
   {
      public int TeachingPeriodId { get; set; }
      public string Name { get; set; }
      public int Month { get; set; }
      public int Day { get; set; }
   }
}
