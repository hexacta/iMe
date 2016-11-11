using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Cors;
using iMe.Factory;
using iMe.Interfaces;

namespace iMe.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class TwitterController : ApiController
    {
        private readonly ISocialNetworkClient client;

        public TwitterController()
        {
            
        }

        public TwitterController(ISocialNetworkClient client)
        {
            this.client = client;
        }

        [Route("twitter/getpersonalinfo/{userId}")]
        public async Task<IHttpActionResult> GetPersonalInfo(string userId)
        {
            var response = await client.GetPersonalInfo(userId);
            return Ok(response);
        }
    }
}
