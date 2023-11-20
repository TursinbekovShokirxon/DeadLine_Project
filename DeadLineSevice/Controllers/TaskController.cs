using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;

namespace DeadLineSevice.Controllers
{
    [ApiController, Route("api/[controller]/[action]")]
    [EnableRateLimiting("FixedWindowPolicy")]
    public class TaskController : ControllerBase
    {

    }
}
