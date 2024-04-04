using API.Models;
using Microsoft.Extensions.Logging;
using System.Text.Json;
using System.Xml.Serialization;
using System.Xml;
using API.Services;
namespace TestPlatba
{
    [TestClass]
    public class UnitTest1
    {
        private string GetPlatba(string s)
        {
            var factory = new LoggerFactory();
            var logger = factory.CreateLogger<API.Controllers.PlatbaController>();
            CashPaymentService cash = new CashPaymentService();
            CardPaymentService card = new CardPaymentService();
            var controller = new API.Controllers.PlatbaController(logger,card,cash);
            return controller.Get(s);
        }

        private string toXML(Platba p)
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
            return s;
        }

        [TestMethod]
        public void CardTest()
        {
            Platba p=new Platba();
            p.typ_platby = "CARD";
            p.castka = 1;
            p.mena = "EUR";
            string json=JsonSerializer.Serialize(p);
            string expected = "1 EUR";

            var actual=GetPlatba(json);

            Assert.AreEqual(expected, actual);

        }

        [TestMethod]
        public void CashTest()
        {
            Platba p = new Platba();
            p.typ_platby = "CASH";
            p.castka = 1;
            p.mena = "EUR";
            string json = JsonSerializer.Serialize(p);
            string expected = toXML(p);

            var actual = GetPlatba(json);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void RejectedTest()
        {
            Platba p = new Platba();
            p.typ_platby = "NULL";
            p.castka = 1;
            p.mena = "EUR";
            string json = JsonSerializer.Serialize(p);
            string expected = "Payment rejected";

            var actual = GetPlatba(json);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void InvalidTest()
        {
            var x = new String('x',1);
            string json = JsonSerializer.Serialize(x);
            string expected = "Invalid Json";

            var actual = GetPlatba(json);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void CardPayment_ValidPlatba_ReturnsCorrectResponse()
        {
            var platba = new Platba
            {
                typ_platby = "CARD",
                castka = 100,
                mena = "USD"
            };
            string json = JsonSerializer.Serialize(platba);
            string expected = "100 USD";

            var actual = GetPlatba(json);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void CashPayment_ValidPlatba_ReturnsCorrectResponse()
        {
            var platba = new Platba
            {
                typ_platby = "CASH",
                castka = 50,
                mena = "EUR"
            };
            string json = JsonSerializer.Serialize(platba);
            string expected = toXML(platba);

            var actual = GetPlatba(json);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void UnknownPaymentType_ReturnsPaymentRejected()
        {
            var platba = new Platba
            {
                typ_platby = "UNKNOWN",
                castka = 75,
                mena = "GBP"
            };
            string json = JsonSerializer.Serialize(platba);
            string expected = "Payment rejected";

            var actual = GetPlatba(json);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void NullJson_ReturnsInvalidJson()
        {
            string json = null;
            string expected = "Invalid Json";

            var actual = GetPlatba(json);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void EmptyJson_ReturnsInvalidJson()
        {
            string json = "";
            string expected = "Invalid Json";

            var actual = GetPlatba(json);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void EmptyPlatba_ReturnsInvalidJson()
        {
            string json = "{}";
            string expected = "Invalid Json";

            var actual = GetPlatba(json);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void InvalidPaymentType_ReturnsPaymentRejected()
        {
            var platba = new Platba
            {
                typ_platby = "INVALID",
                castka = 100,
                mena = "USD"
            };
            string json = JsonSerializer.Serialize(platba);
            string expected = "Payment rejected";

            var actual = GetPlatba(json);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void ValidJsonButNoPaymentType_ReturnsPaymentRejected()
        {
            var json = "{\"castka\": 100, \"mena\": \"USD\", \"datum\": \"2024-03-21T12:00:00\"}";
            string expected = "Payment rejected";

            var actual = GetPlatba(json);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void randomText_returnSlop()
        {
            var json = "SLOP";
            string expected = "Invalid Json";

            var actual = GetPlatba(json);

            Assert.AreEqual(expected, actual);
        }
    }
}