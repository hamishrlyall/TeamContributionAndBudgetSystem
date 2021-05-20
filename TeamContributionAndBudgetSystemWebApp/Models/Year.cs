using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Configuration;
using System.Linq;
using System.Web;

namespace TeamContributionAndBudgetSystemWebApp.Models
{
   public class Year
   {
      /// <summary>
      /// A constructor which copies the information from a database year model.
      /// </summary>
      public Year( TCABS_DataLibrary.Models.YearModel yearModel )
      {
         YearId = yearModel.YearId;
         YearValue = yearModel.Year;
      }

      /// <summary>
      /// Unique Year ID
      /// </summary>
      public int YearId { get; set; }

      /// <summary>
      /// A Unique specified Year
      /// </summary>
      [Required(ErrorMessage = "You must enter a valid Year")]
      [Range( 2021, 9999, ErrorMessage = "You must enter a valid Year." ) ]
      public int YearValue { get; set; }
   }
}