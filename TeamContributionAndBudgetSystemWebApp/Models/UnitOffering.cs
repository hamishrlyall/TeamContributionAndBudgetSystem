using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TeamContributionAndBudgetSystemWebApp.Models
{
   public class UnitOffering
   {
      /// <summary>
      /// Unique UnitOffering ID
      /// </summary>
      public int UnitOfferingId { get; set; }

      /// <summary>
      /// Unique Convenor ID relates to the User class
      /// </summary>
      public int ConvenorId { get; set; }

      /// <summary>
      /// Unique Unit ID relates to the Unit class
      /// </summary>
      public int UnitId { get; set; }

      /// <summary>
      /// Unique TeachingPeriod ID relates to the TeachingPeriod class
      /// </summary>
      public int TeachingPeriodId { get; set; }

      /// <summary>
      /// Unique Year ID Relates to the Year class
      /// </summary>
      public int YearId { get; set; }

      public virtual User Convenor { get; set; }
      public virtual Unit Unit { get; set; }
      public virtual TeachingPeriod TeachingPeriod { get; set; }
      public virtual Year Year { get; set; }
   }
}