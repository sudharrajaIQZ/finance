using backend.Dto;
using backend.Models;

namespace backend.Interface
{
    public interface ITransactionInterface
    {
        Task<IEnumerable<TransactionResponse>>getTransactionAsync();
        Task<BaseResponse<object>>createTransactionAsync(CreateTransaction createTransaction);
        Task<BaseResponse<List<TransactionListById>>> getTransactionByIdAsync();
        // Task<BaseResponse<object>> getTransactionAllbyUser(getTransactionbyUser getTransactionbyUser);

    }
}
