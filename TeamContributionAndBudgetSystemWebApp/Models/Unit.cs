using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TeamContributionAndBudgetSystemWebApp.Models
{
   /// <summary>
   /// Contains information about a specific Unit, with respect to the web application.
   /// </summary>
   public class Unit
   {
      /// <summary>
      /// Default constructor for the Unit class.
      /// </summary>
      public Unit( )
      {
         //UnitOfferings = new HashSet<UnitOffering>( );
      }

      /// <summary>
      /// A constructor which copies the information from a database UnitModel.
      /// </summary>
      public Unit( TCABS_DataLibrary.Models.UnitModel unitModel )
      {
         UnitId = unitModel.UnitId;
         Name = unitModel.Name;

         //UnitOfferings = new HashSet<UnitOffering>( );
      }

      /// <summary>
      /// Unique Unit ID
      /// </summary>
      public int UnitId { get; set; }

      /// <summary>
      /// A name used to idenify this Unit within the Codebase
      /// </summary>
      [Required( ErrorMessage = "You must enter a Name" )]
      public string Name { get; set; }

      //public virtual ICollection<UnitOffering> UnitOfferings { get; set; }
   }
}