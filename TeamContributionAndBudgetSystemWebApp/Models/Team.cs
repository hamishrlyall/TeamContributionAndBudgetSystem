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

      public int ProjectId { get; set; }

      public string Name { get; set; }

      public virtual User Supervisor { get; set; }
      public virtual Project Project { get; set; }

      public virtual ICollection<Enrollment> Enrollments { get; set; }

      //public virtual List<User> AvailableEnrollments { get; set; }
      // Add list for Available Enrollments
      public List<User> GetAvailableEnrollments( int unitOfferingId )
      {
         var enrollmentData = EnrollmentProcessor.LoadEnrollmentsForUnitOffering( unitOfferingId );
         var users = new List<User>( );

         foreach( var row in enrollmentData )
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

      public Team( TCABS_DataLibrary.Models.ProjectModel project )
      {
         ProjectId = project.ProjectId;
         if( project?.ProjectId == project.ProjectId )
         {
            Project = new Project( )
            {
               ProjectId = project.ProjectId,
               Name = project.Name
            };
         }
      }

      public Team( TCABS_DataLibrary.Models.TeamModel team, TCABS_DataLibrary.Models.ProjectModel project )
      {
         TeamId = team.TeamId;
         Name = team.Name;
         ProjectId = team.ProjectId;
         if( project?.ProjectId == team.ProjectId )
         {
            Project = new Project( )
            {
               ProjectId = project.ProjectId,
               Name = project.Name
            };
         }
      }

      public Team( TCABS_DataLibrary.Models.TeamModel team, TCABS_DataLibrary.Models.ProjectModel project, List<TCABS_DataLibrary.Models.EnrollmentModel> enrollments )
      {
         TeamId = team.TeamId;
         Name = team.Name;
         ProjectId = team.ProjectId;
         if( project?.ProjectId == team.ProjectId )
         {
            Project = new Project( )
            {
               ProjectId = project.ProjectId,
               Name = project.Name
            };
            
            //GetAvailableEnrollments( team.ProjectId );
         }

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