using API.Models;

namespace API.Services
{
    public class PaymentTransform
    {
        private static string pay(float castka, string mena)
        {
            return $"{castka} {mena}";
        }
        public static string byCard(Platba p)
        {
            return pay(p.castka, p.mena);
        }
        public static string byCash(Platba p)
        {
            return $"{p.castka} {p.mena} by cash";
        }
    }
}
