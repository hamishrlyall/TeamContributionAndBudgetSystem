
namespace TCABS_DataLibrary.Models
{
    public class PermissionModel
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
        /// A name used to group several items together.
        /// This label will be visable to the user.
        /// This field is optional.
        /// </summary>
        public string LinkGroup { get; set; }

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
    }
}
