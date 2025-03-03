using System.ComponentModel.DataAnnotations;

namespace backend.Dto
{
    public class Customer
    {
        public string firstName { get; set; }
        public string lastName { get; set; }
        public int phoneNumber { get; set; }
        public string Address { get; set; }
        public string Gender { get; set; }
        public DateOnly DOB { get; set; }
        public string BankId { get; set; }
        public DateTime OpeningDate { get; set; }
        public double Balance { get; set; }
        public string AccountType { get; set; }
        public string UserId { get; set; }
    }

    public class UpdateCustomer
    {
        public string firstName { get; set; }
        public string lastName { get; set; }
        public int phoneNumber { get; set; }
        public string Address { get; set; }
        public string Gender { get; set; }
        public DateOnly DOB { get; set; }
        public string BankId { get; set; }
        public string AccountType { get; set; }
    }
}
