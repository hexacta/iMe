using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Cors;

using iMe.Interfaces;
using iMe.Integration;
using iMe.Business;

namespace iMe.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class SocialNetworkController : ApiController
    {
        private ISocialService socialService;

        public SocialNetworkController(ISocialService service)
        {
            socialService = service;
        }
        

        [Route("getpersonalinfo/{clientType}/{userId}")]
        public async Task<IHttpActionResult> GetPersonalInfo(string clientType, string userId)
        {
            var response = await  socialService.GetPersonalInfo(clientType.ToLower(),userId);
            return Ok(response);
        }
    }
}