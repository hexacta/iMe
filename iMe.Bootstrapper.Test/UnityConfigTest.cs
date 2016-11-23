using System.IO;
using System.Web;

using iMe.Business;
using iMe.Integration;
using iMe.Integration.Helpers;
using iMe.Integration.Services;
using iMe.Interfaces;
using iMe.Mapper;

using Microsoft.Practices.Unity;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace iMe.Bootstrapper.Test
{
    [TestClass]
    public class UnityConfigTest
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
        public void WhenResolveSocialServiceShouldNotBeNull()
        {
            ISocialService socialNetworkService = this.container.Resolve<ISocialService>();

            Assert.IsNotNull(socialNetworkService);
            Assert.IsInstanceOfType(socialNetworkService, typeof(PersonalInfoService));
        }

        [TestMethod]
        public void WhenResolveTwitterSocialNetworkClientShouldNotBeNull()
        {
            ISocialNetworkService twitterService = this.container.Resolve<ISocialNetworkService>("twitter");

            Assert.IsNotNull(twitterService);
            Assert.IsInstanceOfType(twitterService, typeof(TwitterService));
        }

        [TestMethod]
        public void WhenResolveGitHubSocialNetworkClientShouldNotBeNull()
        {
            ISocialNetworkService gitHubService = this.container.Resolve<ISocialNetworkService>("github");

            Assert.IsNotNull(gitHubService);
            Assert.IsInstanceOfType(gitHubService, typeof(GitHubService));
        }

        [TestMethod]
        public void WhenResolveIEntityMapperShouldNotBeNull()
        {
            IEntityMapper mapper = this.container.Resolve<IEntityMapper>();

            Assert.IsNotNull(mapper);
            Assert.IsInstanceOfType(mapper, typeof(EntityMapper));
        }

        [TestMethod]
        public void WhenResolveIHttpHelperShouldNotBeNull()
        {
            IHttpHelper httpHelper = this.container.Resolve<IHttpHelper>();

            Assert.IsNotNull(httpHelper);
            Assert.IsInstanceOfType(httpHelper, typeof(HttpClientHelper));
        }
    }
}