namespace backend.Dto
{
    public class CreateTransaction
    {
        public Guid Id { get; set; }
        public Guid customerId { get; set; }
        public string type { get; set; }
        public double amount { get; set; }
        public string userId { get; set; }
    }

    public class TransactionResponse
    {
        public Guid Id { get; set; }
        public Guid CustomerId { get; set; }
        public string CustomerName { get; set; }
        public string Type { get; set; }
        public double Amount { get; set; }
        public DateTime TransactionDate { get; set; }
    }

    public class TransactionListById
    {
        public string id { get; set; }
        public string customerName { get; set; }
        public List<TransactionResponse> transactionResponses { get; set; } = new();
        public string balance { get; set; }
    }
    public class getTransactionbyUser {
        public string userId { get; set; }
        public string userName { get; set; }
        public double Deposit { get; set; }
        public double Withdrawal { get; set; }
        public double Balance { get; set; }
    }
}
