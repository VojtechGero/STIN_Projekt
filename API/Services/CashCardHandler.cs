using API.Models;

namespace API.Services
{
    public class CashCardHandler : IPaymentHandler
    {
        public Dictionary<string, IPaymentService> payments { get; set; }
        public CashCardHandler(IPaymentService card, IPaymentService cash)
        {
            payments=new Dictionary<string, IPaymentService>();
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
