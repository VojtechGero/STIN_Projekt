using API.Models;

namespace API.Services
{
    public interface IPaymentService
    {
        string Pay(Platba p);
    }
}
