using API;
using Microsoft.Extensions.Logging;
using System.Text.Json;
namespace TestPlatba
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
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
    }
}