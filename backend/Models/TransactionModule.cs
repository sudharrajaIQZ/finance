using System.ComponentModel.DataAnnotations;

namespace backend.Models
{
    public class TransactionModule
    {
        internal string userId;

        [Key]
        public Guid Id { get; set; }

        [Required]
        public Guid CustomerId { get; set; }

        [Required]
        public TransactionType Type { get; set; }

        [Required]
        public double Amount { get; set; }

        [Required]
        public DateTime TransactionDate { get; set; } = DateTime.UtcNow;

    }
    public enum TransactionType
    {
        Deposit,
        Withdrawal
    }

}
