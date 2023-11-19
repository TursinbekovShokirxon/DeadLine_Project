using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;

namespace DeadLineSevice.Controllers
{
    [ApiController, Route("[controller]")]
    [EnableRateLimiting("FixedWindowPolicy")]
    public class TaskController 
    {
    }
}
