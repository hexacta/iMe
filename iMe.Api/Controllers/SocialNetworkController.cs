using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Cors;

using iMe.Interfaces;
using iMe.Integration;

namespace iMe.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class SocialNetworkController : ApiController
    {
        private ISocialNetworkService _genericService;

        public SocialNetworkController(ISocialNetworkService service)
        {
            _genericService = service;
        }
        

        [Route("getpersonalinfo/{clientType}/{userId}")]
        public async Task<IHttpActionResult> GetPersonalInfo(string clientType, string userId)
        {
            var response = await _genericService.GetPersonalInfo(clientType.ToLower(),userId);
            return Ok(response);
        }
    }
}