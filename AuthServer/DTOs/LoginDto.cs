﻿using System.ComponentModel.DataAnnotations;

namespace AuthServer.Controllers
{
    public class LoginDto
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}