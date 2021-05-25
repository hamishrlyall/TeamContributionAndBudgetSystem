using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TeamContributionAndBudgetSystemWebApp.Models
{
   public class Enrollment
   {
      /// <summary>
      /// Unique Enrollment ID relates to the Enrollment class
      /// </summary>
      public int EnrollmentId { get; set; }

      /// <summary>
      /// Unique User ID relates to the User class
      /// </summary>
      public int UserId { get; set; }

      /// <summary>
      /// Unique UnitOffering ID relates to the UnitOffering class
      /// </summary>
      public int UnitOfferingId { get; set; }

      /// <summary>
      /// Unique Team ID relates to the Team class
      /// </summary>
      public int TeamId { get; set; }

      public virtual User Student { get; set; }
      public virtual UnitOffering UnitOffering { get; set; }

      public string Username { get; set; }

      // TODO: Add Team model
      //public virtual Team Team { get; set; }
   }
}