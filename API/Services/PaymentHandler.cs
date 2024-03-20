using API.Models;

namespace API.Services
{
    public class PaymentHandler
    {
        Dictionary<string, IPaymentService> payments = new Dictionary<string, IPaymentService>();
        public PaymentHandler(CardPaymentService card,CashPaymentService cash)
        {
            payments.Add("CARD", card);
            payments.Add("CASH", cash);
        }

        public string pay(Platba p)
        {
            IPaymentService payment = payments[p.typ_platby];
            return payment.Pay(p);
        }
    }
}
