using API.Models;
using Microsoft.Extensions.Logging;
using System.Text;
using System.Text.Json;
namespace TestPlatba
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void CardTest()
        {
            //Arrange
            Platba p=new Platba();
            p.typ_platby = "CARD";
            p.castka = 1;
            p.mena = "EUR";
            string json=JsonSerializer.Serialize(p);
            string expected = "1 EUR";

            var factory=new LoggerFactory();
            var logger = factory.CreateLogger<API.Controllers.PlatbaController>();
            var controller = new API.Controllers.PlatbaController(logger);

            //Act
            string actual= controller.Get(json);

            //Assert
            Assert.AreEqual(expected, actual);

        }

        [TestMethod]
        public void CashTest()
        {
            //Arrange
            Platba p = new Platba();
            p.typ_platby = "CASH";
            p.castka = 1;
            p.mena = "EUR";
            string json = JsonSerializer.Serialize(p);
            string expected = "1 EUR by cash";

            var factory = new LoggerFactory();
            var logger = factory.CreateLogger<API.Controllers.PlatbaController>();
            var controller = new API.Controllers.PlatbaController(logger);

            //Act
            string actual = controller.Get(json);

            //Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void RejectedTest()
        {
            Platba p = new Platba();
            p.typ_platby = "SLOP";
            p.castka = 1;
            p.mena = "EUR";
            string json = JsonSerializer.Serialize(p);
            string expected = "Payment rejected";

            var factory = new LoggerFactory();
            var logger = factory.CreateLogger<API.Controllers.PlatbaController>();
            var controller = new API.Controllers.PlatbaController(logger);

            //Act
            string actual = controller.Get(json);

            //Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void InvalidTest()
        {
            var x = new String('x',1);
            string json = JsonSerializer.Serialize(x);
            string expected = "Invalid Json";

            var factory = new LoggerFactory();
            var logger = factory.CreateLogger<API.Controllers.PlatbaController>();
            var controller = new API.Controllers.PlatbaController(logger);

            //Act
            string actual = controller.Get(json);

            //Assert
            Assert.AreEqual(expected, actual);
        }
    }
}