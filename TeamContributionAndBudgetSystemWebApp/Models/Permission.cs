using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TeamContributionAndBudgetSystemWebApp.Models
{
    public class Permission
    {
        /// <summary>
        /// Unique permission ID
        /// </summary>
        public int PermissionId { get; set; }

        /// <summary>
        /// A name used to identifier this permissions within the codebase.
        /// </summary>
        public string PermissionName { get; set; }

        /// <summary>
        /// The name of the menu item to be created for this permission.
        /// This label will be visable to the user.
        /// This field is optional.
        /// </summary>
        public string LinkTitle { get; set; }

        /// <summary>
        /// The .cshtml page which is used to run this procedure.
        /// For example: "Index" for Index.cshtml
        /// </summary>
        public string LinkPage { get; set; }

        /// <summary>
        /// The controller which manages the page to the loaded.
        /// For example: "Home" for the HomeController
        /// </summary>
        public string LinkController { get; set; }

        /// <summary>
        /// Default constructor.
        /// </summary>
        public Permission() { }

        /// <summary>
        /// Setup this Permission using the data library PermissionModel
        /// </summary>
        public Permission(TCABS_DataLibrary.Models.PermissionModel p)
        {
            PermissionId = p.PermissionId;
            PermissionName = p.PermissionName;
            LinkTitle = p.LinkTitle;
            LinkPage = p.LinkPage;
            LinkController = p.LinkController;
        }

        /// <summary>
        /// Check if this permission has all the required link values.
        /// </summary>
        /// <returns>True if all Link* values are non-null, or false if any are null.</returns>
        public bool IsValidLink()
        {
            return
                (LinkTitle != null) &&
                (LinkPage != null) &&
                (LinkController != null);
        }
    }
}