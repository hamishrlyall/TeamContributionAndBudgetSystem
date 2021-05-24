using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TCABS_DataLibrary;
using static TCABS_DataLibrary.BusinessLogic.UserProcessor;
using TeamContributionAndBudgetSystemWebApp.Models;
using TCABS_DataLibrary.BusinessLogic;
using System.Web.Security;
using System.Web.WebPages;
using System.Configuration;
using System.Diagnostics.Contracts;

namespace TeamContributionAndBudgetSystemWebApp.Models
{
   /// <summary>
   /// Contains information relating to the currently logged in user and their session, if any.
   /// </summary>
   public class TCABS_Db_Context
   {
      public virtual User User { get; set; }
      public virtual UserRole UserRole { get; set; }
      public virtual Role Role { get; set; }
      public virtual RolePermission RolePermission { get; set; }
      public virtual Permission Permission { get; set; }
      public virtual Year Year { get; set; }
      public virtual Unit Unit { get; set; }
      public virtual TeachingPeriod TeachingPeriod { get; set; }
      public virtual UnitOffering UnitOffering { get; set; }
      public virtual Enrollment Enrollment { get; set; }

      public virtual List<User> Users { get; set; }
      public virtual List<Role> Roles { get; set; }
      public virtual List<UserRole> UserRoles { get; set; }
      public virtual List<MenuItem> MenuItems { get; set; }
      public virtual List<UnitOffering> UnitOfferings { get; set; }
      public virtual List<Enrollment> Enrollments { get; set; }
      public virtual List<Unit> Units { get; set; }
      public virtual List<TeachingPeriod> TeachingPeriods { get; set; }
      public virtual List<Year> Years { get; set; }
      //public virtual List<User> Students { get; set; }

      /// <summary>
      /// Default constructor for TCABS_Db_Context
      /// </summary>
      public TCABS_Db_Context( )
      {
         string username = System.Web.HttpContext.Current.User.Identity.Name;
         if( !string.IsNullOrEmpty( username ) )
         {
            GetMenu( username );
         }
      }

      /// <summary>
      /// Check if a user (any user) is logged in.
      /// </summary>
      /// <returns>True if a user is already logged in, or false if not.</returns>
      public bool IsUserLoggedIn()
      {
         return (User != null) && (User.Username != null);
      }

      private void GetMenu( string _Username )
      {
            /*
         MenuItems = new List<Models.MenuItem>( );
         var permissions = GetPermissions( _Username );

         MenuItems.Add( new Models.MenuItem( ) { LinkText = "Home", ActionName = "Index", ControllerName = "Home" } );

         foreach( var permission in permissions )
         {
            if( !MenuItems.Any( m => m.LinkText == permission.TableName ) )
               MenuItems.Add( new Models.MenuItem( ) { LinkText = permission.TableName, ActionName = "Index", ControllerName = permission.TableName + "/Index" } );
         }

         MenuItems.Add( new Models.MenuItem( ) { LinkText = "Logout", ActionName = "Logout", ControllerName = "Home" } );
            */
      }

      private List<Permission> GetPermissions( string _Username )
      {
         var user = GetUserForUsername( _Username );

         List<Permission> permissions = new List<Permission>( );

         foreach( var userRole in user.UserRoles )
         {
            foreach( var rolePermission in userRole.Role.RolePermissions )
            {
               if( !permissions.Any( p => p.PermissionId == rolePermission.Permission.PermissionId ) )
                  permissions.Add( rolePermission.Permission );
            }
         }

         return permissions;
      }

      public User GetUser( int id )
      {
         var data = UserProcessor.SelectUserWithRoles( id );
         var user = new User
         {
            UserId = data.UserId,
            Username = data.Username,
            FirstName = data.FirstName,
            LastName = data.LastName,
            EmailAddress = data.Email,
            PhoneNumber = data.PhoneNo,
            Password = data.Password,
         };

         foreach( var userRoleData in data.UserRoles )
         {
            var userRole = new UserRole( );
            userRole.UserRoleId = userRoleData.UserRoleId;
            userRole.RoleId = userRoleData.RoleId;
            userRole.UserId = userRoleData.UserId;
            userRole.User = user;

            var roleData = RoleProcessor.SelectRoleWithPermissions( userRoleData.RoleId );
            var role = new Role( );
            role.RoleId = roleData.RoleId;
            role.Name = roleData.Name;
            foreach( var rolePermissionData in roleData.RolePermissions )
            {
               var rolePermission = new RolePermission( );
               rolePermission.RoleId = rolePermissionData.RoleId;
               rolePermission.PermissionId = rolePermissionData.RoleId;
               rolePermission.Role = role;

               var permissionData = PermissionProcessor.SelectPermission( rolePermissionData.PermissionId );
               var permission = new Permission(permissionData);

               rolePermission.Permission = permission;

               role.RolePermissions.Add( rolePermission );
            }
            userRole.Role = role;
            user.UserRoles.Add( userRole );
         }
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

      public Year GetYear( int id )
      {
         var data = YearProcessor.SelectYearForYearId( id );
         var year = new Year( ) {
            YearId = data.YearId,
            YearValue = data.Year
         };

         Year = year;

         return Year;
      }

      public TeachingPeriod GetTeachingPeriod( int id )
      {
         var data = TeachingPeriodProcessor.SelectTeachingPeriodForTeachingPeriodId( id );
         var teachingPeriod = new TeachingPeriod
         {
            TeachingPeriodId = data.TeachingPeriodId,
            Name = data.Name,
            Month = data.Month,
            Day = data.Day
         };
         TeachingPeriod = teachingPeriod;

         return TeachingPeriod;
      }

      public UnitOffering GetUnitOffering( int id )
      {
         var data = UnitOfferingProcessor.SelectUnitOfferingWithEnrollments( id );

         var unitOffering = new UnitOffering( )
         {
            UnitOfferingId = data.UnitOfferingId,
            ConvenorId = data.ConvenorId,
            UnitId = data.UnitId,
            TeachingPeriodId = data.TeachingPeriodId,
            YearId = data.YearId
         };

         unitOffering.Convenor = GetUser( data.ConvenorId );
         unitOffering.Unit = GetUnit( data.UnitId );
         unitOffering.Year = GetYear( data.YearId );
         unitOffering.TeachingPeriod = GetTeachingPeriod( data.TeachingPeriodId );

         if( data.Enrollments != null )
         {
            foreach( var enrollmentData in data.Enrollments )
            {
               var enrollment = new Enrollment( );
               enrollment.EnrollmentId = enrollmentData.EnrollmentId;
               enrollment.UnitOfferingId = enrollmentData.UnitOfferingId;
               enrollment.UserId = enrollmentData.UserId;
               enrollment.UnitOffering = unitOffering;

               var userData = UserProcessor.SelectUserForUserId( enrollmentData.UserId );
               var user = new User( );
               user.UserId = userData.UserId;
               user.Username = userData.Username;
               user.FirstName = userData.FirstName;
               user.LastName = userData.LastName;
               enrollment.Student = user;

               unitOffering.Enrollments.Add( enrollment );
            }
         }

         UnitOffering = unitOffering;
         return UnitOffering;
      }

      public UnitOffering GetUnitOfferingForDetails( string unitName, string teachingPeriodName, int yearValue )
      {
         var data = UnitOfferingProcessor.SelectUnitOfferingForDetails( unitName, teachingPeriodName, yearValue );

         var unitOffering = new UnitOffering( )
         {
            UnitOfferingId = data.UnitOfferingId,
            ConvenorId = data.ConvenorId,
            UnitId = data.UnitId,
            TeachingPeriodId = data.TeachingPeriodId,
            YearId = data.YearId
         };

         unitOffering.Convenor = GetUser( data.ConvenorId );
         unitOffering.Unit = GetUnit( data.UnitId );
         unitOffering.Year = GetYear( data.YearId );
         unitOffering.TeachingPeriod = GetTeachingPeriod( data.TeachingPeriodId );

         UnitOffering = unitOffering;
         return UnitOffering;
      }

      public User GetUserForUsername( string _Username )
      {
         var data = UserProcessor.SelectUserForUsername( _Username );
         data = UserProcessor.SelectUserWithRoles( data.UserId );

         var user = new User
         {
            UserId = data.UserId,
            Username = data.Username,
            FirstName = data.FirstName,
            LastName = data.LastName,
            EmailAddress = data.Email,
            PhoneNumber = data.PhoneNo,
            Password = data.Password,
         };

         foreach( var userRoleData in data.UserRoles )
         {
            var userRole = new UserRole( );
            userRole.RoleId = userRoleData.RoleId;
            userRole.UserId = userRoleData.UserId;
            userRole.User = user;

            var roleData = RoleProcessor.SelectRoleWithPermissions( userRoleData.RoleId );
            var role = new Role( );
            role.RoleId = roleData.RoleId;
            role.Name = roleData.Name;
            foreach( var rolePermissionData in roleData.RolePermissions )
            {
               var rolePermission = new RolePermission( );
               rolePermission.RoleId = rolePermissionData.RoleId;
               rolePermission.PermissionId = rolePermissionData.RoleId;
               rolePermission.Role = role;

               var permissionData = PermissionProcessor.SelectPermission( rolePermissionData.PermissionId );
               var permission = new Permission(permissionData);

               rolePermission.Permission = permission;

               role.RolePermissions.Add( rolePermission );
            }
            userRole.Role = role;
            user.UserRoles.Add( userRole );
         }

         return user;
      }

      public Unit GetUnitForName( string name )
      {
         var data = UnitProcessor.SelectUnitForName( name );
         var unit = new Unit
         {
            UnitId = data.UnitId,
            Name = data.Name
         };

         Unit = unit;
         return Unit;

      }

      public TeachingPeriod GetTeachingPeriodForName( string name )
      {
         var data = TeachingPeriodProcessor.SelectTeachingPeriodForName( name );
         var teachingPeriod = new TeachingPeriod
         {
            TeachingPeriodId = data.TeachingPeriodId,
            Name = data.Name,
            Month = data.Month,
            Day = data.Day
         };

         TeachingPeriod = teachingPeriod;
         return TeachingPeriod;
      }

      public Year GetYearForYearValue( int yearValue )
      {
         var data = YearProcessor.SelectYearForYearValue( yearValue );
         var year = new Year( )
         {
            YearId = data.YearId,
            YearValue = data.Year
         };

         Year = year;
         return Year;
      }

      public void GetUsers( )
      {
         var userData = UserProcessor.SelectUsers( );
         Users = new List<User>( );
         foreach( var row in userData )
         {
            var user = new User
            {
               UserId = row.UserId,
               Username = row.Username,
               FirstName = row.FirstName,
               LastName = row.LastName,
               EmailAddress = row.Email,
               PhoneNumber = row.PhoneNo,
               Password = row.Password
            };
            var userRoleModel = UserRoleProcessor.LoadRolesForUser( row.UserId );
            foreach( var ur in userRoleModel )
            {
               var roleModel = RoleProcessor.SelectRole( ur.RoleId );
               var role = new Role
               {
                  RoleId = roleModel.RoleId,
                  Name = roleModel.Name
               };
               var userRole = new UserRole
               {
                  UserRoleId = ur.UserRoleId,
                  UserId = ur.UserId,
                  RoleId = ur.RoleId,
                  User = user,
                  Role = role
               };
               user.UserRoles.Add( userRole );
            }
            Users.Add( user );
         }
      }

      public List<Role> GetRoles( )
      {
         var data = RoleProcessor.SelectRoles( );
         Roles = new List<Role>( );

         foreach( var row in data )
         {
            Roles.Add( new Role
            {
               RoleId = row.RoleId,
               Name = row.Name,
            } );
         }

         return Roles;
      }

      public List<User> GetStudents( )
      {
         var studentData = UserProcessor.SelectStudents( );
         Users = new List<User>( );

         foreach( var row in studentData )
         {
            var rowData = UserProcessor.SelectUserForUserId( row.UserId );
            var user = new User
            {
               UserId = rowData.UserId,
               Username = rowData.Username,
               FirstName = rowData.FirstName,
               LastName = rowData.LastName,
               EmailAddress = rowData.Email,
               PhoneNumber = rowData.PhoneNo,
               Password = rowData.Password
            };

            Users.Add( user );
         }

         return Users;
      }

      public List<User> GetConvenors( )
      {
         var convenorData = UserProcessor.SelectConvenors( );
         Users = new List<User>( );
         foreach( var row in convenorData )
         {
            var rowData = UserProcessor.SelectUserForUserId( row.UserId );
            var user = new User
            {
               UserId = rowData.UserId,
               Username = rowData.Username,
               FirstName = rowData.FirstName,
               LastName = rowData.LastName,
               EmailAddress = rowData.Email,
               PhoneNumber = rowData.PhoneNo,
               Password = rowData.Password
            };

            Users.Add( user );
         }

         return Users;
      }

      public List<Unit> GetUnits( )
      {
         var unitData = UnitProcessor.SelectUnits( );
         Units = new List<Unit>( );
         foreach( var row in unitData )
         {
            var unit = new Unit( )
            {
               UnitId = row.UnitId,
               Name = row.Name
            };

            Units.Add( unit );
         }
         return Units;
      }

      public List<TeachingPeriod> GetTeachingPeriods( )
      {
         var teachingPeriodData = TeachingPeriodProcessor.SelectTeachingPeriods( );
         TeachingPeriods = new List<TeachingPeriod>( );
         foreach( var row in teachingPeriodData )
         {
            var teachingPeriod = new TeachingPeriod( )
            {
               TeachingPeriodId = row.TeachingPeriodId,
               Name = row.Name
            };

            TeachingPeriods.Add( teachingPeriod );
         }
         return TeachingPeriods;
      }

      public List<Year> GetYears( )
      {
         var yearData = YearProcessor.SelectYears( );
         Years = new List<Year>( );
         foreach( var row in yearData )
         {
            var year = new Year( )
            {
               YearId = row.YearId,
               YearValue = row.Year
            };

            Years.Add( year );
         }
         return Years;
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
            
            unitOffering.Convenor = GetUser( row.ConvenorId );
            unitOffering.Unit = GetUnit( row.UnitId );
            unitOffering.Year = GetYear( row.YearId );
            unitOffering.TeachingPeriod = GetTeachingPeriod( row.TeachingPeriodId );

            UnitOfferings.Add( unitOffering );
         }
         return UnitOfferings;
      }

   }
}