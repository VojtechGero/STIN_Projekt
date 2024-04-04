using API.Models;

namespace API.Services
{
    public interface IPaymentHandler
    {
        Dictionary<string, IPaymentService> payments { get; set; }

        string pay(Platba p);
    }
}
