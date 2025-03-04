﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend.Models
{
    public class UserRole
    {
        [Key]
        [Column(Order =1)]
        public Guid UserId { get; set; }
        public AuthModel User { get; set; }

        [Key]
        [Column(Order = 2)]
        public Guid RoleId { get; set; }
        public RoleModule Role { get; set; }
    }
}
