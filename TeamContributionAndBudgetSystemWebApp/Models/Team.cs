using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Web;
using TCABS_DataLibrary.BusinessLogic;

namespace TeamContributionAndBudgetSystemWebApp.Models
{
   public class Team
   {
      public int TeamId { get; set; }

      public int SupervisorId { get; set; }

      public int UnitOfferingId { get; set; }

      public string Name { get; set; }

      public virtual User Supervisor { get; set; }
      public virtual UnitOffering UnitOffering { get; set; }
      public string UnitName { get; set; }

      public virtual ICollection<Enrollment> Enrollments { get; set; }

      //public virtual List<User> AvailableEnrollments { get; set; }
      // Add list for Available Enrollments
      public List<Enrollment> GetAvailableEnrollments( int unitOfferingId )
      {
         var enrollmentData = EnrollmentProcessor.LoadEnrollmentsForUnitOffering( unitOfferingId );
         var enrollments = new List<Enrollment>( );

         foreach( var row in enrollmentData )
         {
            var rowData = UserProcessor.SelectUserForUserId( row.UserId );
            var enrollment = new Enrollment
            {
               EnrollmentId = row.EnrollmentId,
               UserId = row.UserId,
               Username = rowData.Username,
               //FirstName = rowData.FirstName,
               //LastName = rowData.LastName,
               //EmailAddress = rowData.Email,
               //PhoneNumber = rowData.PhoneNo,
               //Password = rowData.Password
            };

            enrollments.Add( enrollment );
         }

         return enrollments;
      }

      public List<User> GetAvailableSupervisors( )
      {
         var users = new List<User>( );

         var data = UserProcessor.SelectSupervisors( );
         foreach( var u in data )
         {
            var rowData = UserProcessor.SelectUserForUserId( u.UserId );
            var user = new User
            {
               UserId = rowData.UserId,
               Username = rowData.Username
            };
            users.Add( user );
         }

         return users;
      }

      public Team( )
      {
         Enrollments = new HashSet<Enrollment>( );
      }

      public Team( TCABS_DataLibrary.Models.UnitOfferingModel unitOffering )
      {
         UnitOfferingId = unitOffering.UnitOfferingId;
         if( unitOffering?.UnitOfferingId == unitOffering.UnitOfferingId )
         {
            GetUnitOffering( unitOffering.UnitOfferingId );
            //Project = new Project( )
            //{
            //   ProjectId = unitOffering.ProjectId,
            //   Name = unitOffering.Name
            //};
         }
      }

      //public Team( TCABS_DataLibrary.Models.TeamModel team, TCABS_DataLibrary.Models.UnitOfferingModel unitOffering )
      //{
      //   TeamId = team.TeamId;
      //   Name = team.Name;
      //   UnitOfferingId = team.UnitOfferingId;
      //   if( unitOffering?.UnitOfferingId == team.UnitOfferingId )
      //   {
      //      UnitOffering = new UnitOffering( )
      //      {
      //         UnitOfferingId = unitOffering.UnitOfferingId
      //      };
      //   }
      //}

      private void GetUnitOffering( int id )
      {
         var data = UnitOfferingProcessor.SelectUnitOfferingForUnitOfferingId( UnitOfferingId );
         UnitOffering = new UnitOffering( )
         {
            UnitOfferingId = data.UnitOfferingId,
            TeachingPeriodId = data.TeachingPeriodId,
            YearId = data.YearId,
            ConvenorId = data.ConvenorId,
            UnitId = data.UnitId,
         };

         var teachingperiodData = TeachingPeriodProcessor.SelectTeachingPeriodForTeachingPeriodId( UnitOffering.TeachingPeriodId );
         UnitOffering.TeachingPeriod = new TeachingPeriod( )
         {
            TeachingPeriodId = teachingperiodData.TeachingPeriodId,
            Name = teachingperiodData.Name,
            Day = teachingperiodData.Day,
            Month = teachingperiodData.Month
         };
         var yearData = YearProcessor.SelectYearForYearId( UnitOffering.YearId );
         UnitOffering.Year = new Year( )
         {
            YearId = yearData.YearId,
            YearValue = yearData.Year
         };

         var convenorData = UserProcessor.SelectUserForUserId( UnitOffering.ConvenorId );
         UnitOffering.Convenor = new User( )
         {
            UserId = convenorData.UserId,
            Username = convenorData.Username
         };

         var unitData = UnitProcessor.SelectUnitForUnitId( UnitOffering.UnitId );
         UnitOffering.Unit = new Unit( )
         {
            UnitId = unitData.UnitId,
            Name = unitData.Name
         };
      }

      public Team( TCABS_DataLibrary.Models.TeamModel team, TCABS_DataLibrary.Models.UnitOfferingModel unitOffering )
      {
         TeamId = team.TeamId;
         Name = team.Name;
         UnitOfferingId = team.UnitOfferingId;
         SupervisorId = team.SupervisorId;
         if( unitOffering?.UnitOfferingId == team.UnitOfferingId )
         {
            GetUnitOffering( team.UnitOfferingId );
         }

         var supervisor = UserProcessor.SelectUserForUserId( team.SupervisorId );
         Supervisor = new User( )
         {
            UserId = supervisor.UserId,
            Username = supervisor.Username
         };

         var enrollments = EnrollmentProcessor.LoadEnrollmentsForTeam( team.TeamId );

         Enrollments = new List<Enrollment>( );

         foreach( var e in enrollments )
         {
            var enrollment = new Enrollment( )
            {
               EnrollmentId = e.EnrollmentId,
               UserId = e.UserId,
               UnitOfferingId = e.UnitOfferingId,
               TeamId = e.TeamId
            };
            var student = UserProcessor.SelectUserForUserId( enrollment.UserId );
            enrollment.Student = new User( )
            {
               UserId = student.UserId,
               Username = student.Username
            };

            Enrollments.Add( enrollment );
         }
      }
   }
}