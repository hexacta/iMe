using System.Threading.Tasks;
using System.Web.Http;
using TwitterAccess;
using System.Web.Http;

namespace iMe.Controllers
{
    public class TwitterController : ApiController
    {
        public async Task<IHttpActionResult> GetPersonalInfo(string userId)
        {
            var response = await (new TwitterClient()).GetPersonalInfo(userId);
            return Ok(response);
        }
    }
}
