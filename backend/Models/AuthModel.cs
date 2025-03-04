﻿using System.ComponentModel.DataAnnotations;

namespace backend.Models
{
    public class AuthModel
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public string UserName { get; set; }

        [Required]
        public string EmailId { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        public int Otp { get; set; }
        public bool isVerified { get; set; } = false;

        public ICollection<UserRole> UserRoles { get; set; }

        //refresh token
        public string? RefreshToken { get; set; }
        public DateTime? RefreshTokenExpiry { get; set; }
        public DateTime createdAt { get; set; } = DateTime.UtcNow;
        public DateTime updatedAt { get; set; } = DateTime.UtcNow;
    }
}
