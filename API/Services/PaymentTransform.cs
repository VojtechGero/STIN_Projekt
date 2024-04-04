using API.Models;
using System.Xml;
using System.Xml.Serialization;
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
        public static string cash(string xml)
        {
            return xml;
        }
        public static string byCash(Platba p)
        {
            XmlSerializer x = new XmlSerializer(p.GetType());
            string s;
            using (var sww = new StringWriter())
            {
                using (XmlWriter writer = XmlWriter.Create(sww))
                {
                    x.Serialize(writer, p);
                    s = sww.ToString();
                }
            }
            return cash(s);
        }
    }
}
