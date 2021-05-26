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

      public int ProjectOfferingId { get; set; }

      public string Name { get; set; }

      public virtual User Supervisor { get; set; }
      public virtual ProjectOffering ProjectOffering { get; set; }
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

      public Team( TCABS_DataLibrary.Models.ProjectOfferingModel projectOffering )
      {
         ProjectOfferingId = projectOffering.ProjectOfferingId;
         if( projectOffering?.ProjectOfferingId == ProjectOfferingId )
         {
            GetProjectOffering( projectOffering.ProjectOfferingId );
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



      public Team( TCABS_DataLibrary.Models.TeamModel team, TCABS_DataLibrary.Models.ProjectOfferingModel projectOffering )
      {
         TeamId = team.TeamId;
         Name = team.Name;
         ProjectOfferingId = team.ProjectofferingId;
         SupervisorId = team.SupervisorId;
         if( projectOffering?.ProjectOfferingId == team.ProjectofferingId )
         {
            GetProjectOffering( team.ProjectofferingId );
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

      private void GetProjectOffering( int id )
      {
         var data = ProjectOfferingProcessor.SelectProjectOfferingForProjectOfferingId( id );
         ProjectOffering = new ProjectOffering( )
         {
            ProjectOfferingId = data.ProjectOfferingId,
            ProjectId = data.ProjectId,
            UnitOfferingId = data.UnitOfferingId
         };

         var projectData = ProjectProcessor.GetProject( data.ProjectId );
         ProjectOffering.Project = new Project( )
         {
            ProjectId = projectData.ProjectId,
            ProjectRoleGroupId = projectData.ProjectRoleGroupId,
            Name = projectData.Name
         };

         var unitOfferingData = UnitOfferingProcessor.SelectUnitOfferingForUnitOfferingId( data.UnitOfferingId );
         ProjectOffering.UnitOffering = new UnitOffering( )
         {
            UnitOfferingId = unitOfferingData.UnitOfferingId,
            TeachingPeriodId = unitOfferingData.TeachingPeriodId,
            YearId = unitOfferingData.YearId,
            ConvenorId = unitOfferingData.ConvenorId,
            UnitId = unitOfferingData.UnitId,
         };

         var teachingperiodData = TeachingPeriodProcessor.SelectTeachingPeriodForTeachingPeriodId( ProjectOffering.UnitOffering.TeachingPeriodId );
         ProjectOffering.UnitOffering.TeachingPeriod = new TeachingPeriod( )
         {
            TeachingPeriodId = teachingperiodData.TeachingPeriodId,
            Name = teachingperiodData.Name,
            Day = teachingperiodData.Day,
            Month = teachingperiodData.Month
         };
         var yearData = YearProcessor.SelectYearForYearId( ProjectOffering.UnitOffering.YearId );
         ProjectOffering.UnitOffering.Year = new Year( )
         {
            YearId = yearData.YearId,
            YearValue = yearData.Year
         };

         var convenorData = UserProcessor.SelectUserForUserId( ProjectOffering.UnitOffering.ConvenorId );
         ProjectOffering.UnitOffering.Convenor = new User( )
         {
            UserId = convenorData.UserId,
            Username = convenorData.Username
         };

         var unitData = UnitProcessor.SelectUnitForUnitId( ProjectOffering.UnitOffering.UnitId );
         ProjectOffering.UnitOffering.Unit = new Unit( )
         {
            UnitId = unitData.UnitId,
            Name = unitData.Name
         };
      }
   }
}