using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TeamContributionAndBudgetSystemWebApp.Models
{
   public class TeachingPeriod
   {
      /// <summary>
      /// Default constructor for the TeachingPeriod class
      /// </summary>
      public TeachingPeriod( )
      {
         
      }

      /// <summary>
      /// A constructor which copies the information from a database TeachingPeriodModel
      /// </summary>
      public TeachingPeriod( TCABS_DataLibrary.Models.TeachingPeriodModel teachingPeriodModel)
      {
         TeachingPeriodId = teachingPeriodModel.TeachingPeriodId;
         Name = teachingPeriodModel.Name;
      }

      /// <summary>
      /// Unique TeachingPeriod ID
      /// </summary>
      public int TeachingPeriodId { get; set; }

      /// <summary>
      /// A name used to identify this TeachingPeriod within the codebase. 
      /// </summary>
      [Required( ErrorMessage = "You must enter a Name" )]
      public string Name { get; set; }
      [Range( 1, 12, ErrorMessage = "You must enter a valid Month." )]
      public int Month { get; set; }
      [Range( 1, 31, ErrorMessage = "You must enter a valid Day." )]
      public int Day { get; set; }
   }
}