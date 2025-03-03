using backend.Interface;
using backend.Services;

namespace backend.Services
{
    public static class ServiceRegisteration
    {
        public static void AddApplicationService(this IServiceCollection services)
        {
            services.AddScoped<IAuthInterface, AuthService>();
            services.AddScoped<ICustomerIntrerface, CustomerService>();
            services.AddScoped<IOrganizationInterface, OrganizationService>();
            services.AddScoped<ITransactionInterface, TransactionService>();
        }
    }
}
