using DirectIDAssessment.Interface;
using DirectIDAssessment.Model;
using System.Text.Json;

namespace DirectIDAssessment.Service
{
    public class Calculator : ICalculator
    {

        public EndOfDayBalance GetCustomerDailyBalance(Customer customerInfo, DateTime date){
            
            var dailyTransactions = customerInfo.Accounts[0]?.Transactions.GroupBy(x => Convert.ToDateTime(x.BookingDate.ToShortDateString())).OrderBy(x => x.Key).ToList();
            var currentbalance = customerInfo.Accounts[0].Balances.Current.Amount;
            
            var endofDayBalances = calculateCustomerDailyBalance(dailyTransactions, currentbalance);

            var item = endofDayBalances.Where(x => x.Day.ToShortDateString() == date.ToShortDateString()).Select(x => x).FirstOrDefault();

            return item ;
        }

   

        public Customer GetCustomerAcoountInfo()
        {
            string customerProfile = File.ReadAllText(@"./MockDb/apollo-carter.json");
            var customerAccount = JsonSerializer.Deserialize<Customer>(customerProfile, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            return customerAccount;
        }

        private List<EndOfDayBalance> calculateCustomerDailyBalance(List<IGrouping<DateTime,Transaction>> dailyTransactions, double currentBalance)
        {

            var endOfDayBalances = new List<EndOfDayBalance>();
            foreach (var transactions in dailyTransactions)
            {
                var credit = transactions.Where(x => x.CreditDebitIndicator == "Credit").Sum(x => x.Amount);
                var debit = transactions.Where(x => x.CreditDebitIndicator == "Debit").Sum(x => x.Amount);
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
