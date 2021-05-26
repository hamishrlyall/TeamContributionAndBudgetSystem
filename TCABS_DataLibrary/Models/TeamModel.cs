using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TCABS_DataLibrary.Models
{
   public class TeamModel
   {
      public int TeamId { get; set; }
      public int SupervisorId { get; set; }
      public int ProjectofferingId { get; set; }
      public string Name { get; set; }
   }
}
