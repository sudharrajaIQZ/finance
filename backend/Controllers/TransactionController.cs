using backend.Dto;
using backend.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace backend.Controllers
{
    [Route("api/transaction")]
    [ApiController]
    public class TransactionController : ControllerBase
    {
        private readonly ITransactionInterface transaction;

        public TransactionController(ITransactionInterface transactionInterface)
        {
            this.transaction = transactionInterface;
        }

        [HttpGet]
        public async Task<IActionResult> getTransaction()
        {
            var getTransactionList = await transaction.getTransactionAsync();
            return Ok(getTransactionList);
        }

        [HttpGet("transaction-by-id")]
        public async Task<IActionResult> getTransactionbyId()
        {
            var getTransactionList = await transaction.getTransactionByIdAsync();
            return Ok(getTransactionList);
        }

        [HttpPost]        
        public async Task<IActionResult> createTransaction([FromBody] CreateTransaction createTransaction)
        {
            var addTransaction = await transaction.createTransactionAsync(createTransaction);
            return StatusCode(addTransaction.Code,addTransaction);
        }

        [HttpGet("transaction-all-user")]
        public async Task<IActionResult> getAllTransaction()
        {
            var getAllTransaction = await transaction.getTransactionAsync();
            return StatusCode((int) HttpStatusCode.Created, getAllTransaction);
        }

    }
}
