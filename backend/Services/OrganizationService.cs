using backend.DataBase;
using backend.Dto;
using backend.Interface;
using backend.Models;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace backend.Services
{
    public class OrganizationService:IOrganizationInterface
    {
        private readonly AppDbContext appDb;

        public OrganizationService(AppDbContext appDbContext)
        {
            this.appDb = appDbContext;
        }

        public async Task<BaseResponse<object>> getOrganizationAsync()
        {
            var organizationList = await appDb.organizations.ToListAsync();
            return new BaseResponse<object>("organizations list fetched successfully", (int)HttpStatusCode.Created, organizationList);
        }

        public async Task<BaseResponse<object>> createOrganizationAsync(createOrganization createOrganization)
        {
            var organizations = new OrganizationModule
            {
                Id = new Guid(),
                BankName = createOrganization.BankName,
                BranchName = createOrganization.BranchName,
                BranchCode = createOrganization.BranchCode,
                IFSCCode = createOrganization.IFSCCode,
                MICRCode = createOrganization.MICRCode
            };
            await appDb.organizations.AddAsync(organizations);
            await appDb.SaveChangesAsync();

            return new BaseResponse<object>("organizations created successfully", (int)HttpStatusCode.Created, organizations);
        }
    }
}
