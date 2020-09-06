using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LikeButton.Controllers
{
    [Authorize]
    [Route("[controller]")]
    [ApiController]
    public class BaseController : ControllerBase
    {
    }
}