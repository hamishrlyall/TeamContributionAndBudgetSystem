
namespace TeamContributionAndBudgetSystemWebApp.Models
{
    /// <summary>
    /// This class contains a list of names used for specific permissions.
    /// The purpose of this class is for convenience and to try and prevent typos.
    /// </summary>
    public static class PermissionName
    {
        /// <summary>
        /// Allowed to modify user data.
        /// </summary>
        public const string UserModify = "UserModify";

        /// <summary>
        /// Allowed to view user data (readonly).
        /// </summary>
        public const string UserView = "UserView";
        
        /// <summary>
        /// Allowed to modify user-role data.
        /// </summary>
        public const string UserRolesModify = "UserRolesModify";

    }
}