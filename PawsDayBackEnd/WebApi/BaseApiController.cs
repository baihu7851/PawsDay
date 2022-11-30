using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PawsDayBackEnd.Filters;

namespace PawsDayBackEnd.WebApi
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [ServiceFilter(typeof(AdminAuthorize))]
    public abstract class BaseApiController : ControllerBase
    {
    }
}
