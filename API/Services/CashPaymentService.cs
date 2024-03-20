using API.Models;

namespace API.Services
{
    public class CashPaymentService : IPaymentService
    {
        public static string Pay(Platba p)
        {
            return PaymentTransform.byCash(p);
        } 
        string IPaymentService.Pay(Platba p)
        {
            return Pay(p);
        }
    }
}
