using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TeamContributionAndBudgetSystemWebApp.Models
{
    /// <summary>
    /// Contains information about a specific user, with respect to the web application.
    /// </summary>
    public class UserEdit
    {
        /// <summary>
        /// Default constructor for the User class.
        /// </summary>
        public UserEdit()
        {
        }

        /// <summary>
        /// A constructor which copies the information from a database user model.
        /// </summary>
        public UserEdit(TCABS_DataLibrary.Models.UserModel userModel)
        {
            UserId = userModel.UserId;
            Username = userModel.Username;
            FirstName = userModel.FirstName;
            LastName = userModel.LastName;
            EmailAddress = userModel.Email;
            PhoneNumber = userModel.PhoneNo;
        }

        public int UserId { get; set; }

        [Required(ErrorMessage = "You must enter a Username.")]
        public string Username { get; set; }

        [Display(Name = "First Name")]
        [Required(ErrorMessage = "You must enter your first name.")]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        [Required(ErrorMessage = "You must enter your last name.")]
        public string LastName { get; set; }

        [DataType(DataType.EmailAddress)]
        [Display(Name = "Email Address")]
        [Required(ErrorMessage = "You must enter your email address.")]
        public string EmailAddress { get; set; }

        [Display(Name = "Phone Number")]
        [DataType(DataType.PhoneNumber)]
        public int PhoneNumber { get; set; }

    }
}