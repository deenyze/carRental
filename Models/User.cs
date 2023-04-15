﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarRentalAPI.Models
{
    public class User
    {
        public int UserId { get; set; }
        [Required, MinLength(2), MaxLength(15)]
        [Display(Name = "Username")]
        public string UserName { get; set; } = "";
        [Required, MinLength(8), MaxLength(20)]
        public string Password { get; set; } = "";
        [Display(Name = "Administrator")]
        public bool IsAdmin { get; set; } = false;
        [Required]
        public string Email { get; set; } = "";
        [Required]
        [Display(Name = "Phone")]
        public int PhoneNr { get; set; }
        [NotMapped]
        public virtual List<Booking> Bookings { get; set; } = new List<Booking>();
    }
}
