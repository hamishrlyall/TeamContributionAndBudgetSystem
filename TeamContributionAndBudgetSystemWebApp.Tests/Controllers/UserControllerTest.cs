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
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using TCABS_DataLibrary.Models;
using TeamContributionAndBudgetSystemWebApp.Controllers;
using TeamContributionAndBudgetSystemWebApp.Models;

namespace TeamContributionAndBudgetSystemWebApp.Tests.Controllers
{
   [TestClass]
   public class UserControllerTest
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
             new GenericIdentity( "superadmin" ),
             new string[ 0 ]
             );
      }

      [TestMethod]
      public void Index_UserListPresent( )
      {
         InitialiseMockHttpContext( );

         //var connection = new MockDbConnection( );
         //var repository = new MyTestRepository( connection );


         //connection
         //   .Setup( c => c.Query<UserModel>(
         //      It.IsAny<string>( ), It.IsAny<object>( ),
         //      It.IsAny<IDbTransaction>( ), true, null, CommandType.StoredProcedure ) )
         //   .Returns( new List<UserModel>( )
         //      {  new UserModel( ) { UserId = 1 },
         //         new UserModel( ) { UserId = 2 },
         //         new UserModel( ) { UserId = 3 }
         //      } );

         UserController controller = new UserController( );

         ViewResult result = controller.Index( ) as ViewResult;

         //Verify the LoadDataQuery was called
         //connection.Verify( c => c.Query<UserModel>(
         //       It.IsAny<string>( ), It.IsAny<object>( ),
         //       It.IsAny<IDbTransaction>( ), true, null, CommandType.StoredProcedure ) );

         Assert.IsNotNull( result );
      }


      //private TCABS_Db_Context db = new TCABS_Db_Context( );
   }
}
