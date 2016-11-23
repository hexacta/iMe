using System.IO;
using System.Threading.Tasks;
using System.Web;

using iMe.Bootstrapper;

using Microsoft.Practices.Unity;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace iMe.Integration.Test
{
    [TestClass]
    public class GitHubServiceTest
    {
        private IUnityContainer container;

        [TestInitialize]
        public void Setup()
        {
            if (this.container == null)
            {
                var request = new HttpRequest("fake", "https://127.0.0.1", null);
                var respons = new HttpResponse(new StringWriter());
                var context = new HttpContext(request, respons);
                HttpContext.Current = context;

                this.container = UnityConfig.GetConfiguredContainer();
            }
        }

        [TestMethod]
        public void ShouldReturnSocialNetworkType()
        {
            ISocialNetworkService service = this.container.Resolve<ISocialNetworkService>("github");
            var name = service.SocialNetworkName;
            Assert.IsNotNull(name);
        }

        [TestMethod]
        public async Task ShouldReturnSocialUserData()
        {
            ISocialNetworkService service = this.container.Resolve<ISocialNetworkService>("github");
            var userData = await service.GetPersonalInfo("hexacta");
            Assert.IsNotNull(userData);
        }
    }
}