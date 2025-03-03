using backend.DataBase;
using backend.Dto;
using backend.Interface;
using backend.Models;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace backend.Services
{
    public class TransactionService:ITransactionInterface
    {
        private readonly AppDbContext appdb;

        public TransactionService(AppDbContext appDbContext)
        {
            this.appdb = appDbContext;
        }

        //public async Task<IEnumerable<TransactionResponse>> getTransactionAsync()
       public async Task<IEnumerable<TransactionResponse>> getTransactionAsync()

        {
            var transactionList = await (from transaction in appdb.transaction
                                         join customer in appdb.customerDetails
                                         on transaction.CustomerId equals customer.Id
                                         select new TransactionResponse
                                         {
                                             Id = transaction.Id,
                                             CustomerId = transaction.CustomerId,
                                             CustomerName = customer.firstName,
                                             Type = transaction.Type.ToString(),
                                             Amount = transaction.Amount,
                                             TransactionDate = transaction.TransactionDate
                                         }).ToListAsync();

            return transactionList;
        }

        public async Task<BaseResponse<List<TransactionListById>>> getTransactionByIdAsync()
        {
            var customersWithTransactions = await appdb.customerDetails
                .Select(customer => new TransactionListById
                {
                    id = customer.Id.ToString(),
                    customerName = customer.firstName,
                    balance = customer.Balance.ToString(),
                    transactionResponses = appdb.transaction
                        .Where(t => t.CustomerId == customer.Id)
                        .Select(t => new TransactionResponse
                        {
                            Id = t.Id,
                            CustomerId = t.CustomerId,
                            CustomerName = customer.firstName,
                            Type = t.Type.ToString(),
                            Amount = t.Amount,
                            TransactionDate = t.TransactionDate
                        }).ToList()
                }).ToListAsync();

            if (customersWithTransactions == null || customersWithTransactions.Count == 0)
            {
                return new BaseResponse<List<TransactionListById>>("No customers found", (int)HttpStatusCode.NotFound, null);
            }

            return new BaseResponse<List<TransactionListById>>("Customer transactions retrieved successfully", (int)HttpStatusCode.OK, customersWithTransactions);
        }

        public async Task<BaseResponse<object>> createTransactionAsync(CreateTransaction createTransaction)
        {
            if (!Enum.TryParse(createTransaction.type, true, out TransactionType transactionType))
            {
                return new BaseResponse<object>("Invalid transaction type", (int)HttpStatusCode.BadRequest);
            }
 
            var findCustomer = await appdb.customerDetails.FirstOrDefaultAsync(e => e.Id == createTransaction.customerId);

            if(findCustomer == null)
            {
                return new BaseResponse<object>("Customer Not found", (int)HttpStatusCode.NotFound);
            }

            if(transactionType == TransactionType.Deposit)
            {
                findCustomer.Balance += createTransaction.amount;
            }else if(transactionType == TransactionType.Withdrawal)
            {
                if(findCustomer.Balance < createTransaction.amount)
                {
                    return new BaseResponse<object>("Insufficient balance", (int)HttpStatusCode.BadRequest);
                }
                findCustomer.Balance -= createTransaction.amount;
            }
            
            var addTransaction = new TransactionModule
                {
                    Id = new Guid(),
                    CustomerId = createTransaction.customerId,
                    Type = transactionType,
                    Amount = createTransaction.amount,
                    TransactionDate =DateTime.UtcNow,
                userId = createTransaction.userId
                };

            await appdb.transaction.AddAsync(addTransaction);
            await appdb.SaveChangesAsync();

            return new BaseResponse<object>("Transaction created", (int)HttpStatusCode.Created,addTransaction);
        }
    
    public async Task<BaseResponse<object>>getTransactionAllbyUser(getTransactionbyUser getTransactionbyUser)
        {
            var userWithTransactionCustomer = await appdb.userDetails.Select(user => new getTransactionbyUser
            {
                userId = user.Id.ToString(),
                userName = user.UserName,
                Withdrawal = appdb.transaction
            .Where(t => t.userId == user.Id.ToString() && t.Type == TransactionType.Withdrawal)
            .Sum(t => t.Amount),
                Deposit = appdb.transaction
            .Where(t => t.userId == user.Id.ToString() && t.Type == TransactionType.Deposit)
            .Sum(t => t.Amount)
            }).ToListAsync();
            foreach(var user in userWithTransactionCustomer)
            {
                user.Balance = user.Deposit - user.Withdrawal;
            }

            return new BaseResponse<object>("fetched suucessfully",(int) HttpStatusCode.Created,userWithTransactionCustomer);
        }
    }
}
