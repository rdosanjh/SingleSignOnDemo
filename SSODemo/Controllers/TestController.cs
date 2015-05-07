using System.Web.Http;

namespace SSODemo.Controllers
{
    public class TestController : ApiController
    {
        [Authorize]
        public string Get()
        {
            return "Hello";
        }
    }
}
