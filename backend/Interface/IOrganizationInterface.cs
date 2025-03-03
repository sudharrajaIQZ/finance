using backend.Dto;

namespace backend.Interface
{
    public interface IOrganizationInterface
    {
        Task<BaseResponse<object>> getOrganizationAsync();
        Task<BaseResponse<object>> createOrganizationAsync(createOrganization createOrganization);
    }
}
