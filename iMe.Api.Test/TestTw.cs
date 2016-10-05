using Microsoft.VisualStudio.TestTools.UnitTesting;
using iMe.Controllers;
using System.Threading.Tasks;

namespace iMe.Tests
{
    [TestClass]
    public class TestTw
    {
        [TestMethod]
        public async Task TwitterControler_GetPersonalInfoTest()
        {
            var controller = new TwitterController();
            var result = await controller.GetPersonalInfo("Hexacta");
            Assert.IsNotNull(result);
        }
    }
}
