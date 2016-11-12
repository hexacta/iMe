using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Cors;
using iMe.Factory;
using iMe.Interfaces;

namespace iMe.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class SocialNetworkController : ApiController
    {
        private ISocialNetworkClient genericClient;

        public SocialNetworkController()
        {
            
        }

        public SocialNetworkController(ISocialNetworkClient client)
        {
            genericClient = client;
        }
        

        [Route("getpersonalinfo/{userId}")]
        public async Task<IHttpActionResult> GetPersonalInfo(string userId)
        {
            var response = await genericClient.GetPersonalInfo(userId);
            return Ok(response);
        }
    }
}