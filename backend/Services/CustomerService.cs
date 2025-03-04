using backend.DataBase;
using backend.Dto;
using backend.Interface;
using backend.Models;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace backend.Services
{
    public class CustomerService : ICustomerIntrerface
    {
        private readonly AppDbContext appDb;
        public CustomerService(AppDbContext appDbContext)
        {
            this.appDb = appDbContext;
        }

        public async Task<BaseResponse<object>> getCustomerAsync()
        {
            var customerList = await appDb.customers.ToListAsync();

            return new BaseResponse<object>("Customer list fetched successfully", (int)HttpStatusCode.Created, customerList);
        }

        public async Task<BaseResponse<object>> createCustomerAsync(Customer customer)
        {
            var generatedAccountNumber = new GenerateAccountNumber().GenerateAccountNumberAll();
            var AddCustomer = new CustomerModule
            {
                Id = new Guid(),
                firstName = customer.firstName,
                lastName = customer.lastName,
                phoneNumber = customer.phoneNumber,
                Address = customer.Address,
                Gender = customer.Gender,
                DOB = customer.DOB,
                BankId = customer.BankId,
                AccountNumber = generatedAccountNumber,
                OpeningDate = DateTime.UtcNow,
                AccountType = customer.AccountType,
                Balance = 0,
                UserId = customer.UserId
            };

            await appDb.customers.AddAsync(AddCustomer);
            await appDb.SaveChangesAsync();

            return new BaseResponse<object>("Customer Created Successfully", (int)HttpStatusCode.Created);

        }

        public async Task<BaseResponse<object>> updateCustomerAsync(Guid id, UpdateCustomer updateCustomer)
        {
            var findCustomer = await appDb.customers.FirstOrDefaultAsync(c => c.Id == id);
            if (findCustomer != null)
            {
                findCustomer.firstName = updateCustomer.firstName;
                findCustomer.lastName = updateCustomer.lastName;
                findCustomer.phoneNumber = updateCustomer.phoneNumber;
                findCustomer.Address = updateCustomer.Address;
                findCustomer.DOB = updateCustomer.DOB;
                findCustomer.AccountType = updateCustomer.AccountType;

                return new BaseResponse<object>("updated Successfully", (int)HttpStatusCode.Created);
            }
            else
            {
                return new BaseResponse<object>("Customer not found", (int)HttpStatusCode.NotFound);
            }            
        }

        public async Task<BaseResponse<object>> findCustomerAsync(string id)
        {
            if (!Guid.TryParse(id, out Guid customerId))
            {
                return new BaseResponse<object>("Invalid customer ID format", (int)HttpStatusCode.BadRequest, null);
            }
            var findCustomer = await appDb.customers.FirstOrDefaultAsync(e => e.Id == customerId);         
            return new BaseResponse<object>("customer found successfully", (int)HttpStatusCode.Found, findCustomer);
        }

        public class GenerateAccountNumber
        {
            private const string StartNumber = "548602";
            private readonly string yearMonth;
            private static readonly Random random = new Random();

            public GenerateAccountNumber()
            {
                yearMonth = DateTime.Now.ToString("yyyyMM");
            }
            public string GenerateAccountNumberAll()
            {
                string lastDigits = random.Next(100, 999).ToString();
                return StartNumber + yearMonth + lastDigits;
            }
        }

    }
}
