using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TCABS_DataLibrary;
using static TCABS_DataLibrary.BusinessLogic.UserProcessor;
using TeamContributionAndBudgetSystemWebApp.Models;
using TCABS_DataLibrary.BusinessLogic;
using System.Net;
using System.Xml.Serialization;
using System.Data;
using System.Web.Security;
using TeamContributionAndBudgetSystemWebApp.ViewModel;
namespace TeamContributionAndBudgetSystemWebApp.Controllers
{
    public class RolePermissionController : Controller
    {
        private TCABS_Db_Context db = new TCABS_Db_Context();
        // GET: RolePermission
        public ActionResult Index()
        {
            
            return View(db);
        }

        public ActionResult Create()
        {
            return View();
        }
       [HttpGet]
        public ActionResult Details(int id)
        {
            db.curRoleId = id;
            //List<RolePermission> lstRolePermission =db.getRolePermissionByRoleId(id);
            // base on role id , need to get list of rolePermissions

            //return View(lstRolePermission);

            PopulateRoleDropDownList();

            return View(db);
        }

        public ActionResult Save(int id)
        {
            db.curRoleId = id;
           

            return View(db);
        }


        private void PopulateRoleDropDownList()
        {
            List<Permission> permissions = db.GetPermissions();
            
            ViewBag.PermissionId = new SelectList(db.GetPermissions(), "PermissionId", "TableName", null);
            ViewBag.RoleId = db.curRoleId;

        }



        // User _User, Role _Role, RolePermission p, string _Action

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Details(string id, string PermissionID) 

        {

            //if (p == null)
            //{
            //    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            //}
            /*   try
               {
                   // Send Create procedure to DataAccess
                   var data = UserRoleProcessor.InsertUserRole(_User.UserId, _Role.RoleId);
                   if (data == null)
                       throw new DataException("Role added was invalid.");
                   var roleData = RoleProcessor.SelectRole(data.RoleId);
                   var userRole = new UserRole();
                   userRole.UserRoleId = data.UserRoleId;
                   userRole.UserId = data.UserId;
                   userRole.RoleId = data.RoleId;
                   userRole.Role = new Role { RoleId = roleData.RoleId, Name = roleData.Name };

                   _User.UserRoles.Add(userRole);
               }
               catch (DataException _Ex)
               {
                   ModelState.AddModelError("", $"Unable to save changes due to Error: { _Ex.Message }");
               }

               //Reload User details here.
               db.GetUser(_User.UserId);

               PopulateRoleDropDownList();
               */
            int d = RolePermissionProcessor.CreateUserRole(int.Parse(PermissionID),int.Parse(id));
            if (d > 0)
                return View("Index", db);

            return View(db);
            
        }

        public ActionResult Delete(string id)
        {
            string[] c = id.Split(',') ;
            db.curRoleId = int.Parse(c[1]);
            var permission = db.getRolePermissionByRoleId(db.curRoleId);
            var p = permission.Find(a => a.Permission.PermissionId == int.Parse(c[0]));
            ViewBag.PermID=int.Parse(c[0]);
            //ViewBag.RoleName=db.Roles.Find(a => a.RoleId == db.curRoleId).Name;
            //ViewBag.TableName = param[1];
            //ViewBag.Action = param[2];
            //List<RolePermission> lstRolePermission =db.getRolePermissionByRoleId(id);
            // base on role id , need to get list of rolePermissions

            //return View(lstRolePermission);

            PopulateRoleDropDownList();

            return View(db);
        }
      
        public ActionResult DeleteP(string id)
        {
            string[] val = id.Split(',');
            int d=RolePermissionProcessor.DeleteUserRole(int.Parse(val[0]), int.Parse(val[1]));
            if(d>0)
            return View("Index",db);

            return View(db);
        }


    }
}