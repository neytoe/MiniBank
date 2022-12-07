using DirectIDAssessment.Interface;
using DirectIDAssessment.Model;
using Microsoft.AspNetCore.Mvc;

namespace DirectIDAssessment.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly ICalculator _calculator;
        private readonly ILogger<Account> _accountsLogger;

        public AccountController(ICalculator calculator, ILogger<Account> accountsLogger)
        {
            _calculator = calculator;
            _accountsLogger = accountsLogger;
        }

        [HttpGet("GetBalanceByDate/{date}")]
        public IActionResult GetBalanceByDate(DateTime date)
        {
            try
            {
                var customerAccountDetails = _calculator.GetCustomerAcoountInfo();
                var result = _calculator.GetCustomerDailyBalance(customerAccountDetails, date);

                return Ok(result);
            }
            catch (Exception  ex)
            {
                _accountsLogger.LogError(ex.Message, ex);
                return BadRequest("An error occured");
            }
        }
    }
}