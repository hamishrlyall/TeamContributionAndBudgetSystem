using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.ModelBinding;
using TCABS_DataLibrary.BusinessLogic;

namespace TeamContributionAndBudgetSystemWebApp.Models
{
   public class UnitOffering
   {
      public UnitOffering( )
      {
         Enrollments = new HashSet<Enrollment>( );
         Teams = new HashSet<Team>( );
      }

      public UnitOffering( TCABS_DataLibrary.Models.UnitOfferingModel unitOfferingModel )
      {
         UnitOfferingId = unitOfferingModel.UnitOfferingId;
         ConvenorId = unitOfferingModel.ConvenorId;
         UnitId = unitOfferingModel.UnitId;
         TeachingPeriodId = unitOfferingModel.TeachingPeriodId;
         YearId = unitOfferingModel.YearId;

         Enrollments = new HashSet<Enrollment>( );
         Teams = new HashSet<Team>( );
      }

      public UnitOffering( TCABS_DataLibrary.Models.UnitOfferingModel unitOffering, 
                           TCABS_DataLibrary.Models.UnitModel unit,
                           TCABS_DataLibrary.Models.TeachingPeriodModel teachingperiod,
                           TCABS_DataLibrary.Models.YearModel year,
                           TCABS_DataLibrary.Models.UserModel convenor,
                           List<TCABS_DataLibrary.Models.TeamModel> teams,
                           List<TCABS_DataLibrary.Models.EnrollmentModel> enrollments )
      {
         UnitOfferingId = unitOffering.UnitOfferingId;
         UnitId = unitOffering.UnitId;
         TeachingPeriodId = unitOffering.TeachingPeriodId;
         YearId = unitOffering.YearId;
         ConvenorId = unitOffering.ConvenorId;

         Unit = new Unit( );
         if( unit?.UnitId == UnitId )
         {
            Unit.Name = unit.Name;
            Unit.UnitId = unit.UnitId;
         }

         TeachingPeriod = new TeachingPeriod( );
         if( teachingperiod?.TeachingPeriodId == TeachingPeriodId )
         {
            TeachingPeriod.Name = teachingperiod.Name;
            TeachingPeriod.Month = teachingperiod.Month;
            TeachingPeriod.Day = teachingperiod.Day;
            TeachingPeriod.TeachingPeriodId = teachingperiod.TeachingPeriodId;
         }

         Year = new Year( );
         if( year?.YearId == YearId )
         {
            Year.YearValue = year.Year;
            Year.YearId = year.YearId;
         }

         Convenor = new User( );
         if( convenor?.UserId == ConvenorId )
         {
            Convenor.Username = convenor.Username;
            Convenor.UserId = convenor.UserId;
         }

         Teams = new List<Team>( );

         foreach( var t in teams )
         {
            var team = new Team( )
            {
               TeamId = t.TeamId,
               Name = t.Name,
               UnitOfferingId = t.UnitOfferingId,
               SupervisorId = t.SupervisorId
            };
            team.UnitOffering = this;
            var supervisorModel = UserProcessor.SelectUserForUserId( team.SupervisorId );
            team.Supervisor = new User( )
            {
               UserId = supervisorModel.UserId,
               Username = supervisorModel.Username
            };

            Teams.Add( team );
         }

         Enrollments = new List<Enrollment>( );
         foreach( var e in enrollments )
         {
            var enrollment = new Enrollment( )
            {
               EnrollmentId = e.EnrollmentId,
               UnitOfferingId = e.UnitOfferingId,
               UserId = e.UserId,
               UnitOffering = this
            };

            var userData = UserProcessor.SelectUserForUserId( e.UserId );
            var user = new User( )
            {
               UserId = userData.UserId,
               Username = userData.Username,
               FirstName = userData.FirstName,
               LastName = userData.LastName,
            };
            enrollment.Student = user;

            Enrollments.Add( enrollment );
         }
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
      public virtual ICollection<Team> Teams { get; set; }

      public Team TeamHeadings = null;


      public List<User> GetStudents( )
      {
         var studentData = UserProcessor.SelectStudents( );
         var users = new List<User>( );

         foreach( var row in studentData )
         {
            var rowData = UserProcessor.SelectUserForUserId( row.UserId );
            var user = new User
            {
               UserId = rowData.UserId,
               Username = rowData.Username,
               //FirstName = rowData.FirstName,
               //LastName = rowData.LastName,
               //EmailAddress = rowData.Email,
               //PhoneNumber = rowData.PhoneNo,
               //Password = rowData.Password
            };

            users.Add( user );
         }

         return users;
      }
   }
}