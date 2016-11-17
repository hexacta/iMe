using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Cors;

using iMe.Interfaces;

namespace iMe.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class SocialNetworkController : ApiController
    {
        private ISocialNetworkClient genericClient;

        public SocialNetworkController(ISocialNetworkClient client)
        {
            genericClient = client;
        }
        

        [Route("getpersonalinfo/{clientType}/{userId}")]
        public async Task<IHttpActionResult> GetPersonalInfo(string clientType, string userId)
        {
            var response = await genericClient.GetPersonalInfo(clientType.ToLower(),userId);
            return Ok(response);
        }
    }
}