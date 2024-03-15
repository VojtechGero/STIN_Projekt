using API;
using System.Text.Json;
namespace TestPlatba
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            Platba p=new Platba();
            p.typ_platby = "CARD";
            p.castka = 1;
            p.mena = "EUR";
            string json=JsonSerializer.Serialize(p);
            string test = "1 EUR";
            var controller = API.Controllers.PlatbaController();
            Assert.AreEqual(test, controller.Get(json));

        }
    }
}