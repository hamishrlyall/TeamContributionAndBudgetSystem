using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using TCABS_DataLibrary.BusinessLogic;

namespace TeamContributionAndBudgetSystemWebApp.Models
{
    /// <summary>
    /// This class is used to manage user login functionality.
    /// </summary>
    public class UserLogin
    {
        [Required(ErrorMessage = "You must enter a Username.")]
        public string Username { get; set; }

        [Display(Name = "Password")]
        [Required(ErrorMessage = "You must enter a password.")]
        [DataType(DataType.Password)]
        [StringLength(100, MinimumLength = 8, ErrorMessage = "Your password must have between 8-100 characters.")]
        public string Password { get; set; }

        /// <summary>
        /// Check if the contained username and password match a user.
        /// The password property will be cleared on exit.
        /// </summary>
        /// <returns>The user data which matches the valid username and password, or null if username and/or password was incorrect.</returns>
        public User ValidateUser()
        {
            User result = null;
            bool isUsernameAndPasswordValid = false;
            TCABS_DataLibrary.Models.UserModel user = null;

            // Try to get the user data from the database
            try
            {
                user = UserProcessor.SelectUserForUsername(Username);
            }
            catch { }

            // Check if any user data was found
            if (user != null)
            {

                // Check if password salt exists
                if (user.PasswordSalt != null)
                {
                    // Check if the login password matches the stored password
                    // Exit here if incorrect
                    if (user.Password == HashPassword(Password, user.PasswordSalt))
                    {
                        isUsernameAndPasswordValid = true;
                    }
                }
                else
                {
                    // No password exists
                    // This should only be the case for debugging, where data might have been directly added to the database
                    // If user data is entered via the normal means then password salt should always exist
                    // Create a new password salt
                    user.PasswordSalt = CreatePasswordSalt();

                    // Check if the login password matches the stored password
                    // Assume password is plain-text because no salt exists yet
                    if (user.Password == Password)
                    {
                        isUsernameAndPasswordValid = true;

                        // If here then password is correct
                        // Make a hashed version of the password
                        user.Password = HashPassword(Password, user.PasswordSalt);

                        // Save the new hashed password and new password salt in the database
                        UserProcessor.UpdatePassword(user.UserId, user.Password, user.PasswordSalt);
                    }
                }

                // If the username and password is value then record the user information
                if (isUsernameAndPasswordValid)
                {
                    result = new User
                    {
                        UserId = user.UserId,
                        Username = user.Username,
                        FirstName = user.FirstName,
                        LastName = user.LastName,
                        EmailAddress = user.Email,
                        PhoneNumber = user.PhoneNo,
                    };
                }
            }

            // Clear the password data and then return the result
            Password = null;
            return result;
        }

        /// <summary>
        /// Create a string value suitable for use as password salt.
        /// </summary>
        /// <returns>A base64 string used for password salt.</returns>
        public static string CreatePasswordSalt()
        {
            // Create a buffer used to contain a 256-bit salt value
            byte[] salt = new byte[256 / 8];
            using (var rng = System.Security.Cryptography.RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
            }

            // Convert the buffer to a base64 string and return it
            return Convert.ToBase64String(salt);
        }

        /// <summary>
        /// Return a hashed version of the supplied password, using the password salt as well.
        /// </summary>
        public static string HashPassword(string password, string salt)
        {
            // Source: https://docs.microsoft.com/en-us/aspnet/core/security/data-protection/consumer-apis/password-hashing?view=aspnetcore-5.0
            return Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: password,
                salt: Convert.FromBase64String(salt),
                prf: KeyDerivationPrf.HMACSHA512,
                iterationCount: 10000,
                numBytesRequested: 256 / 8));
        }
    }
}