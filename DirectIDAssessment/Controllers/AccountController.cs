using DirectIDAssessment.Interface;
using DirectIDAssessment.Model;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace DirectIDAssessment.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly ILogger<AccountController> _logger;
        private readonly ICalculator _calculator;

        public AccountController(ILogger<AccountController> logger, ICalculator calculator)
        {
            _logger = logger;
            _calculator = calculator;
        }

        [HttpGet("GetBalanceByDate/{date}")]
        public object GetBalanceByDate(DateTime date)
        {
            var customerAccountDetails = _calculator.GetCustomerAcoountInfo();
            var result = _calculator.GetCustomerDailyBalance(customerAccountDetails);
            //var currbalance = result.Where(x => x.Day < Convert.ToDateTime(date.ToShortDateString())).Sum(x => x.Balance) + root.accounts[0].balances.current.amount;
            var item = result.Where(x => x.Day == date.AddDays(-1)).Select(x => x).FirstOrDefault();

            
            
            return item;
        }
    }
}