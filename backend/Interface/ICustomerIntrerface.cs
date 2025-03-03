using backend.Dto;

namespace backend.Interface
{
    public interface ICustomerIntrerface
    {
        Task<BaseResponse<object>> getCustomerAsync();
        Task<BaseResponse<object>> createCustomerAsync(Customer customer);
        Task<BaseResponse<object>> updateCustomerAsync(Guid id, UpdateCustomer updateCustomer);
        Task<BaseResponse<object>> findCustomerAsync(string id);
    }
}
