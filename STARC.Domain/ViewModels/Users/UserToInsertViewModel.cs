﻿using System.ComponentModel.DataAnnotations;

namespace STARC.Domain.ViewModels.Users
{
    public class UserToInsertViewModel
    {
        [Required(ErrorMessage = "First Name required")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last Name required")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "UserName required")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Password required")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Email required")]
        public string Email { get; set; }

        [Required(ErrorMessage = "User Profile required")]
        public int? UserProfileId { get; set; }

        [Required(ErrorMessage = "Customer required")]
        public long? CustomerId { get; set; }
    }
}