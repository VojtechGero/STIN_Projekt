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

        private string pay(float castka,string mena)
        {
            return castka.ToString() + " "+mena;
        }

        [HttpGet(Name = "GetPlatba")]
        public string Get(string json)
        {
            try
            {
                Platba p = JsonSerializer.Deserialize<Platba>(json);
                if (p.typ_platby == "CARD")
                {
                    return pay(p.castka, p.mena);
                }
                if(p.typ_platby == "CASH")
                {
                    return p.castka.ToString() + p.mena + " by cash";
                }
                return "NOT CARD";
            }catch (Exception ex)
            {
                return ex.Message;
            }
            
        }
    }
}
