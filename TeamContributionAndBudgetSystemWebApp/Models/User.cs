using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TeamContributionAndBudgetSystemWebApp.Models
{
   public class User
   {
      //[Display( Name = "User ID" )]
      //[Range( 100000, 999999, ErrorMessage = "You need to enter a valid UserId" )]
      //public int UserId { get; set; }

      public User( )
      {
         this.UserRoles = new HashSet<UserRole>( );
      }

      public int UserId { get; set; }

      [Required( ErrorMessage = "You must enter a Username.")]
      public string Username { get; set; }

      [Display( Name = "First Name" )]
      [Required( ErrorMessage = "You must enter your first name." )]
      public string FirstName { get; set; }

      [Display( Name = "Last Name" )]
      [Required( ErrorMessage = "You must enter your last name." )]
      public string LastName { get; set; }

      [DataType( DataType.EmailAddress )]
      [Display( Name = "Email Address" )]
      [Required( ErrorMessage = "You must enter your email address." )]
      public string EmailAddress { get; set; }

      [Display(Name = "Confirm Email")]
      [Compare( "EmailAddress", ErrorMessage = "The email and confirm email do not match." )]
      public string ConfirmEmailAddress { get; set; }
      
      [Display( Name = "Phone Number")]
      [DataType(DataType.PhoneNumber)]
      public int PhoneNumber { get; set; }
      
      [Display(Name = "Password")]
      [Required(ErrorMessage = "You must enter a password.")]
      [DataType(DataType.Password)]
      [StringLength(100, MinimumLength = 8, ErrorMessage = "Your password must have between 8-100 characters.")]
      public string Password { get; set; }

      [Display(Name = "Confirm Password")]
      [DataType(DataType.Password)]
      [Compare("Password", ErrorMessage =  "Your password and confirm password do not match.")]
      public string ConfirmPassword { get; set; }

      public virtual ICollection<UserRole> UserRoles { get; set; }
   }
}