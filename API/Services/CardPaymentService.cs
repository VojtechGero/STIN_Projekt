using API.Models;

namespace API.Services
{
    public class CardPaymentService : IPaymentService
    {
        public static string Pay(Platba p)
        {
            return PaymentTransform.byCard(p);
        }
        string IPaymentService.Pay(Platba p)
        {
            return Pay(p);
        }
    }
}
