using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TCABS_DataLibrary.BusinessLogic;
using TCABS_DataLibrary.Models;

namespace TeamContributionAndBudgetSystemWebApp.Models
{
   public class ProjectOffering
   {
      public ProjectOffering( )
      {
         Teams = new HashSet<Team>( );
      }

      public ProjectOffering( TCABS_DataLibrary.Models.ProjectOfferingModel projectOffering,
                              TCABS_DataLibrary.Models.ProjectModel project,
                              TCABS_DataLibrary.Models.UnitOfferingModel unitOffering,
                              List<TeamModel> teams )
      {
         ProjectOfferingId = projectOffering.ProjectOfferingId;
         UnitOfferingId = projectOffering.UnitOfferingId;
         ProjectId = projectOffering.ProjectId;

         UnitOffering = new UnitOffering( );
         if( unitOffering?.UnitOfferingId == UnitOfferingId )
         {
            UnitOffering.UnitOfferingId = unitOffering.UnitOfferingId;
            UnitOffering.TeachingPeriodId = unitOffering.TeachingPeriodId;
            UnitOffering.YearId = unitOffering.YearId;
            UnitOffering.ConvenorId = unitOffering.ConvenorId;
         }
         UnitOffering.Unit = GetUnit( unitOffering.UnitId );

         Project = new Project( );
         if( project?.ProjectId == ProjectId )
         {
            Project.ProjectId = project.ProjectId;
            Project.ProjectRoleGroupId = project.ProjectRoleGroupId;
            Project.Name = project.Name;
         }

         Teams = new List<Team>( );
         foreach( var t in teams )
         {
            var team = new Team( )
            {
               TeamId = t.TeamId,
               ProjectOfferingId = t.ProjectofferingId,
               SupervisorId = t.SupervisorId,
               Name = t.Name
            };
            team.ProjectOffering = this;
            team.Supervisor = GetUser( t.SupervisorId );

            Teams.Add( team );
         }
      }

      public List<Project> GetProjects( )
      {
         var data = ProjectProcessor.GetAllProjects( );
         Projects = new List<Project>( );
         foreach( var row in data )
         {
            var project = new Project( )
            {
               ProjectId = row.ProjectId,
               Name = row.Name,
               ProjectRoleGroupId = row.ProjectRoleGroupId
            };
            Projects.Add( project );
         }

         return Projects;
      }

      public User GetUser( int id )
      {
         var data = UserProcessor.SelectUserWithRoles( id );
         var user = new User
         {
            UserId = data.UserId,
            Username = data.Username,

         };
         User = user;

         return User;
      }

      public Unit GetUnit( int id )
      {
         var data = UnitProcessor.SelectUnitForUnitId( id );
         var unit = new Unit
         {
            UnitId = data.UnitId,
            Name = data.Name
         };

         Unit = unit;

         return Unit;
      }


      public List<UnitOffering> GetUnitOfferings( )
      {
         var data = UnitOfferingProcessor.SelectUnitOfferings( );
         UnitOfferings = new List<UnitOffering>( );

         foreach( var row in data )
         {
            var unitOffering = new UnitOffering( )
            {
               UnitOfferingId = row.UnitOfferingId,
               ConvenorId = row.ConvenorId,
               UnitId = row.UnitId,
               TeachingPeriodId = row.TeachingPeriodId,
               YearId = row.YearId
            };

            //unitOffering.Convenor = GetUser( row.ConvenorId );
            unitOffering.Unit = GetUnit( row.UnitId );
            //unitOffering.Year = GetYear( row.YearId );
            //unitOffering.TeachingPeriod = GetTeachingPeriod( row.TeachingPeriodId );
            unitOffering.UnitName = unitOffering.Unit.Name;

            UnitOfferings.Add( unitOffering );
         }
         return UnitOfferings;
      }

      private void GetUnitOffering( int id )
      {
         var data = UnitOfferingProcessor.SelectUnitOfferingForUnitOfferingId( id );
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

      public int ProjectOfferingId { get; set; }
      public int UnitOfferingId { get; set; }
      public int ProjectId { get; set; }

      public virtual UnitOffering UnitOffering { get; set; }
      public virtual Project Project { get; set; }

      public virtual ICollection<Team> Teams { get; set; }
      public Team TeamHeadings = null;

      public virtual Unit Unit { get; set; }
      public virtual User User { get; set; }
      public virtual List<UnitOffering> UnitOfferings { get; set; }
      public virtual List<Project> Projects { get; set; }

   }
}