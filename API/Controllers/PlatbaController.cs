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
        private readonly IPaymentService _cardPaymentService;
        private readonly IPaymentService _cashPaymentService;
        /*
          {"castka":1,"mena":"EUR","datum":"0001-01-01T00:00:00","typ_platby":"CARD","seznam_polozek":null}
        */
        public PlatbaController(ILogger<PlatbaController> logger,CashPaymentService cashPaymentService,CardPaymentService cardPaymentService)
        {
            _logger = logger;
            _cardPaymentService = cardPaymentService;
            _cashPaymentService = cashPaymentService;
        }

        

        [HttpGet(Name = "GetPlatba")]
        public string Get(string json)
        {
            try
            {
                var platba = Platba.parseJson(json);
                var payment = new CashCardHandler(_cardPaymentService, _cashPaymentService);
                return payment.pay(platba);
            }
            catch(JsonException)
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
