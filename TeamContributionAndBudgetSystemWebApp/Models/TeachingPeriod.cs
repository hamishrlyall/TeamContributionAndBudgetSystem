using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TeamContributionAndBudgetSystemWebApp.Models
{
   public class TeachingPeriod
   {
      /// <summary>
      /// Unique TeachingPeriod ID
      /// </summary>
      public int TeachingPeriodId { get; set; }

      /// <summary>
      /// A name used to identify this TeachingPeriod within the codebase. 
      /// </summary>
      public string Name { get; set; }
   }
}