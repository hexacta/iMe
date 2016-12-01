using System.IO;
using System.Web;
using iMe.Integration;
using iMe.Integration.Helpers;
using iMe.Integration.Providers;
using iMe.Interfaces;
using iMe.Mapper;

using Microsoft.Practices.Unity;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using iMe.IServices;
using iMe.Services;

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
            IPersonalInfoService personalInfoNetworkService = this.container.Resolve<IPersonalInfoService>();

            Assert.IsNotNull(personalInfoNetworkService);
            Assert.IsInstanceOfType(personalInfoNetworkService, typeof(PersonalInfoService));
        }

        [TestMethod]
        public void WhenResolveTwitterSocialNetworkClientShouldNotBeNull()
        {
            ISocialNetworkProvider twitterProvider = this.container.Resolve<ISocialNetworkProvider>("twitter");

            Assert.IsNotNull(twitterProvider);
            Assert.IsInstanceOfType(twitterProvider, typeof(TwitterProvider));
        }

        [TestMethod]
        public void WhenResolveGitHubSocialNetworkClientShouldNotBeNull()
        {
            ISocialNetworkProvider gitHubProvider = this.container.Resolve<ISocialNetworkProvider>("github");

            Assert.IsNotNull(gitHubProvider);
            Assert.IsInstanceOfType(gitHubProvider, typeof(GitHubProvider));
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