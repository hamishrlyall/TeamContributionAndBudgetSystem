using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.ModelBinding;

namespace TeamContributionAndBudgetSystemWebApp.Models
{
   public class UnitOffering
   {
      public UnitOffering( )
      {
         Enrollments = new HashSet<Enrollment>( );
      }

      public UnitOffering( TCABS_DataLibrary.Models.UnitOfferingModel unitOfferingModel )
      {
         UnitOfferingId = unitOfferingModel.UnitOfferingId;
         ConvenorId = unitOfferingModel.ConvenorId;
         UnitId = unitOfferingModel.UnitId;
         TeachingPeriodId = unitOfferingModel.TeachingPeriodId;
         YearId = unitOfferingModel.YearId;

         Enrollments = new HashSet<Enrollment>( );
      }

      /// <summary>
      /// Unique UnitOffering ID
      /// </summary>
      public int UnitOfferingId { get; set; }

      /// <summary>
      /// Unique Convenor ID relates to the User class
      /// </summary>
      [Required( ErrorMessage = "You must select a Convenor.")]
      public int ConvenorId { get; set; }

      /// <summary>
      /// Unique Unit ID relates to the Unit class
      /// </summary>
      [Required( ErrorMessage = "You must select a Unit.")]
      public int UnitId { get; set; }

      /// <summary>
      /// Unique TeachingPeriod ID relates to the TeachingPeriod class
      /// </summary>
      [Required( ErrorMessage = "You must select a Teaching Period." )]
      public int TeachingPeriodId { get; set; }

      /// <summary>
      /// Unique Year ID Relates to the Year class
      /// </summary>
      [Required( ErrorMessage = "You must select a Year." )]
      public int YearId { get; set; }

      public virtual User Convenor { get; set; }
      public virtual Unit Unit { get; set; }
      public virtual TeachingPeriod TeachingPeriod { get; set; }
      public virtual Year Year { get; set; }

      public virtual ICollection<Enrollment> Enrollments { get; set; }
   }
}