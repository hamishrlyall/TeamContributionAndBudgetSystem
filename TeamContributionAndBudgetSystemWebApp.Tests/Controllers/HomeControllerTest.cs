using Dapper.MoqTests;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using TCABS_DataLibrary.Models;
using TeamContributionAndBudgetSystemWebApp;
using TeamContributionAndBudgetSystemWebApp.Controllers;

namespace TeamContributionAndBudgetSystemWebApp.Tests.Controllers
{
   [TestClass]
   public class HomeControllerTest
   {
      /// <summary>
      /// Initialises a Mock Http Context which can be used to simulate Authorization
      /// </summary>
      private void InitialiseMockHttpContext( )
      {
         HttpContext.Current = new HttpContext(
             new HttpRequest( "", "http://tempuri.org", "" ),
             new HttpResponse( new StringWriter( ) )
             );

         // User is logged in
         HttpContext.Current.User = new GenericPrincipal(
             new GenericIdentity( "mockUser" ),
             new string[ 0 ]
             );
      }

      /// <summary>
      /// Test the result of navigating to the Home page where the user is logged in.
      /// This test should pass the mock validation previously set up.
      /// The app should then attempt to retrieve the logged in users permissions.
      /// </summary>
      [TestMethod]
      public void Index_UserIsLoggedIn( )
      {
         InitialiseMockHttpContext( );

         var connection = new MockDbConnection( );

         connection
            .Setup( c => c.Query<PermissionModel>(
               It.IsAny<string>( ), It.IsAny<object>( ),
               It.IsAny<IDbTransaction>( ), true, null, CommandType.StoredProcedure ) )
            .Returns( new List<PermissionModel>( ) { new PermissionModel( ) { PermissionId = 1 } } );

         HomeController controller = new HomeController( );

         ViewResult result = controller.Index( ) as ViewResult;

         //Verify the LoadDataQuery was called
         connection.Verify( c => c.Query<PermissionModel>(
                It.IsAny<string>( ), It.IsAny<object>( ),
                It.IsAny<IDbTransaction>( ), true, null, CommandType.StoredProcedure ) );

         Assert.IsNotNull( result );
      }

      [TestMethod]
      public void Index_UserIsLoggedOut( )
      {
         InitialiseMockHttpContext( );

         // User is logged out
         HttpContext.Current.User = new GenericPrincipal(
             new GenericIdentity( String.Empty ),
             new string[ 0 ]
             );

         // Arrange
         HomeController controller = new HomeController( );

         // Act
         ViewResult result = controller.Index( ) as ViewResult;

         // Assert
         Assert.IsNotNull( result );
      }
   }
}
