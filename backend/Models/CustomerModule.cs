using System.ComponentModel.DataAnnotations;

namespace backend.Models
{
    public class CustomerModule
    {
        [Key]
        public Guid Id { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }

        [Required]
        public int phoneNumber { get; set; }
        public string Address { get; set; }
        public string Gender { get; set; }

        [Required]
        public DateOnly DOB { get; set; }
        public string BankId { get; set; }

        [Required]
        public string AccountNumber { get; set; }
        public DateTime OpeningDate { get; set; } = DateTime.UtcNow;

        [Required]
        public string AccountType { get; set; }
        public double Balance { get; set; } = 0;
        public bool isActive { get; set; } = true;
        public string UserId { get; set; }
        public DateTime createdAt { get; set; } = DateTime.UtcNow;
        public DateTime updatedAt { get; set; } = DateTime.UtcNow;

    }

}
