using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TeamContributionAndBudgetSystemWebApp.Models
{
    /// <summary>
    /// A menu item is a single entry in the top-menu-bar on every page.
    /// Every one of these items is provides a link to a particular page.
    /// </summary>
    public class MenuItem
    {
        /// <summary>
        /// The name of the menu item.
        /// This label will be visable to the user.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// The .cshtml page which should be linked too.
        /// For example: "Index" for Index.cshtml
        /// </summary>
        public string Page { get; set; }

        /// <summary>
        /// The controller which manages the page to the loaded.
        /// For example: "Home" for the HomeController
        /// </summary>
        public string Controller { get; set; }

        /// <summary>
        /// Default constructor.
        /// </summary>
        public MenuItem() { }

        /// <summary>
        /// Use permissions to setup the menu item.
        /// </summary>
        public MenuItem(Permission p)
        {
            Title = p.LinkTitle;
            Page = p.LinkPage;
            Controller = p.LinkController;
        }
    }
}