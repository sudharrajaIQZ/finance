using System.ComponentModel.DataAnnotations;

namespace backend.Models
{
    public class RoleModule
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }
        public string? Description { get; set; }
        public ICollection<UserRole> userRoles { get; set; }
    }
}
