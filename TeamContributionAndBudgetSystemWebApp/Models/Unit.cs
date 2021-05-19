using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TeamContributionAndBudgetSystemWebApp.Models
{
   public class Unit
   {
      /// <summary>
      /// Unique Unit ID
      /// </summary>
      public int UnitId { get; set; }
      
      /// <summary>
      /// A name used to idenify this Unit within the Codebase
      /// </summary>
      public string Name { get; set; }
   }
}