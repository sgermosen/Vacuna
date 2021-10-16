using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace VacunaAPI.Models
{
    public class RegisterViewModel
    {

        [Required]
        [StringLength(50)]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 5)]
        public string Password { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 5)]
        public string ConfirmPassword { get; set; }

        [Required]
        [StringLength(300)]
        public string FullName { get; set; }

        [StringLength(15)]
        public string Identification { get; set; }

        [StringLength(100)]
        public string Nationality { get; set; }

        public DateTime BornDate { get; set; }

        public IFormFile Dni { get; set; }


    }
}
