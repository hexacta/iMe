using Microsoft.VisualStudio.TestTools.UnitTesting;
using iMe.Controllers;
using System.Threading.Tasks;
using iMe.Dto;
using Microsoft.Practices.Unity;
using TwitterAccess;

namespace iMe.Tests
{
    [TestClass]
    public class SocialNetworkControllerTest
    {
        private IUnityContainer container;

        [TestInitialize]
        public void Setup()
        {
            container = UnityConfig.GetConfiguredContainer();
            
        }

        [TestMethod]
        public async Task SocialNetworkController_GetPersonalInfoTestWithTw()
        {
            var controller = new SocialNetworkController();
            var result = await controller.GetPersonalInfo("twitter","Hexacta");
            Assert.IsNotNull(result);
            
        }
    }
}
