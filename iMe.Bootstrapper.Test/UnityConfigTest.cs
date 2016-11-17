using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Microsoft.Practices.Unity;
using iMe.Interfaces;
using iMe.Bootstrapper;
using iMe.Business;
using iMe.Integration;
using iMe.Mapper;
using iMe.Integration.Clients;
using iMe.Integration.Helpers;

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
        public void WhenResolveGenericSocialNetworkClientShouldNotBeNull()
        {
            ISocialNetworkClient socialNetworkClient = this.container.Resolve<ISocialNetworkClient>();

            Assert.IsNotNull(socialNetworkClient);
            Assert.IsInstanceOfType(socialNetworkClient,typeof(GenericClient));
        }

        [TestMethod]
        public void WhenResolveTwitterSocialNetworkClientShouldNotBeNull()
        {
            ISocialNetworkClient twitterClient = this.container.Resolve<ISocialNetworkClient>("twitter");

            Assert.IsNotNull(twitterClient);
            Assert.IsInstanceOfType(twitterClient, typeof(TwitterClient));
        }

        [TestMethod]
        public void WhenResolveGitHubSocialNetworkClientShouldNotBeNull()
        {
            ISocialNetworkClient gitHubClient = this.container.Resolve<ISocialNetworkClient>("github");

            Assert.IsNotNull(gitHubClient);
            Assert.IsInstanceOfType(gitHubClient, typeof(GitHubClient));
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

