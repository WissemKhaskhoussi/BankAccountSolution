using BankAccountAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace BankAccountAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BankAccountController : ControllerBase
    {
        private readonly IBankAccountService _bankAccountService;

        public BankAccountController(IBankAccountService bankAccountService)
        {
            _bankAccountService = bankAccountService ?? throw new ArgumentNullException(nameof(bankAccountService));
        }

        [HttpPost("{accountId}/deposit")]
        public IActionResult Deposit(int accountId, [FromBody] decimal amount)
        {
            try
            {
                _bankAccountService.Deposit(accountId, amount);
                return Ok("Deposit successful");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost("{accountId}/withdraw")]
        public IActionResult Withdraw(int accountId, [FromBody] decimal amount)
        {
            try
            {
                _bankAccountService.Withdraw(accountId, amount);
                return Ok("Withdrawal successful");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("{accountId}/balance")]
        public IActionResult GetBalance(int accountId)
        {
            try
            {
                var balance = _bankAccountService.GetBalance(accountId);
                return Ok(balance);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("{accountId}/transactions")]
        public IActionResult GetTransactions(int accountId)
        {
            try
            {
                var transactions = _bankAccountService.GetTransactions(accountId);
                return Ok(transactions);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}