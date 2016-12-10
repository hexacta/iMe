using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Cors;
using iMe.IServices;

namespace iMe.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class SocialNetworkController : ApiController
    {
        private readonly IPersonalInfoService _personalInfoService;

        public SocialNetworkController(IPersonalInfoService service)
        {
            this._personalInfoService = service;
        }

        [Route("getpersonalinfo/{clientType}/{userId}")]
        public async Task<IHttpActionResult> GetPersonalInfo(string clientType, string userId)
        {
            var response = await this._personalInfoService.GetPersonalInfo(clientType.ToLower(), userId);
            return this.Ok(response);
        }
    }
}