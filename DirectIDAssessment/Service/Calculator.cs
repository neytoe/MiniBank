using DirectIDAssessment.Interface;
using DirectIDAssessment.Model;
using System.Text.Json;

namespace DirectIDAssessment.Service
{
    public class Calculator : ICalculator
    {

        public List<EndOfDayBalance> GetCustomerDailyBalance(Customer CustomerInfo){
            
            var dailyTransactions = CustomerInfo.accounts.SelectMany(x => x.transactions).GroupBy(x => x.bookingDate).OrderBy(x => x.Key).ToList();
            var currentbalance = CustomerInfo.accounts[0].balances.current.amount;
            
            var endofDayBalances = calculateCustomerDailyBalance(dailyTransactions, currentbalance);

            return endofDayBalances.OrderBy(x => x.Day).ToList();
        }

   

        public Customer GetCustomerAcoountInfo()
        {
            string customerProfile = System.IO.File.ReadAllText(@"./Apollo/apollo-carter.json");
            var customerAccount = JsonSerializer.Deserialize<Customer>(customerProfile, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            return customerAccount;
        }

        private List<EndOfDayBalance> calculateCustomerDailyBalance(List<IGrouping<DateTime,Transaction>> dailyTransactions, double currentBalance)
        {

            var endOfDayBalances = new List<EndOfDayBalance>();
            foreach (var transactions in dailyTransactions)
            {
                var credit = transactions.Where(x => x.creditDebitIndicator == "Credit").Sum(x => x.amount);
                var debit = transactions.Where(x => x.creditDebitIndicator == "Debit").Sum(x => x.amount);
                var closingbalance = currentBalance + credit - debit;
                var balance = new EndOfDayBalance()
                {
                    Balance = closingbalance,
                    Day = transactions.Key,
                    TotalCredits = credit,
                    TotalDebits = debit
                };
                endOfDayBalances.Add(balance);
                currentBalance = closingbalance;
            }
            return endOfDayBalances;
        }
    }
}
