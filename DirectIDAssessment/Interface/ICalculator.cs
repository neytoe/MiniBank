using DirectIDAssessment.Model;

namespace DirectIDAssessment.Interface
{
    public interface ICalculator
    {
        Customer GetCustomerAcoountInfo();
        EndOfDayBalance GetCustomerDailyBalance(Customer customerInfo, DateTime date);
    }  
}
