using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Cors;

using iMe.Business;

namespace iMe.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class SocialNetworkController : ApiController
    {
        private readonly ISocialService socialService;

        public SocialNetworkController(ISocialService service)
        {
            this.socialService = service;
        }

        [Route("getpersonalinfo/{clientType}/{userId}")]
        public async Task<IHttpActionResult> GetPersonalInfo(string clientType, string userId)
        {
            var response = await this.socialService.GetPersonalInfo(clientType.ToLower(), userId);
            return this.Ok(response);
        }
    }
}