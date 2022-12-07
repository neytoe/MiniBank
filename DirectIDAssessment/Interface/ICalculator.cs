using DirectIDAssessment.Model;

namespace DirectIDAssessment.Interface
{
    public interface ICalculator
    {
        Customer GetCustomerAcoountInfo();
        List<EndOfDayBalance> calculateCustomerDailyBalance();
        List<EndOfDayBalance> GetCustomerDailyBalance(Customer CustomerInfo);
    }  
}
