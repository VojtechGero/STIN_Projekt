using API.Models;
using API.Services;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
namespace API.Controllers
{
    [ApiController]
    [Route("platba")]
    public class PlatbaController : ControllerBase
    {
        private readonly ILogger<PlatbaController> _logger;

        public PlatbaController(ILogger<PlatbaController> logger)
        {
            _logger = logger;
        }

        

        [HttpGet(Name = "GetPlatba")]
        public string Get(string json)
        {
            try
            {
                Platba p = JsonSerializer.Deserialize<Platba>(json);
                PaymentHandler payment = new PaymentHandler(new CardPaymentService(),new CashPaymentService());
                return payment.pay(p);
            }
            catch(JsonException e)
            {
                return "Invalid Json";
            }
            catch (Exception)
            {
                return "Payment rejected";
            }
            
        }
    }
}
