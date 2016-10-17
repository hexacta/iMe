using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Cors;
using NetworkAccess;
using iMe.Factory;

namespace iMe.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class SocialNetworkController : ApiController
    {
        private ISocialNetworkClient client;

        [Route("socialnetwork/getpersonalinfo/{clientType}/{userId}")]
        public async Task<IHttpActionResult> GetPersonalInfo(string clientType, string userId)
        {
            this.client = ClientFactory.GetClient(clientType);
            var response = await client.GetPersonalInfo(userId);
            return Ok(response);
        }
    }
}
