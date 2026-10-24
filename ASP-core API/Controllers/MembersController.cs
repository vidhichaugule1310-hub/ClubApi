using ClubApi.Models;
using ClubApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace ClubApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MembersController : ControllerBase
    {
        
        private readonly IMemberService _service;
        public MembersController(IMemberService service) => _service = service;

        [HttpGet] public IActionResult Get() => Ok(_service.GetAll());
        [HttpPost] public IActionResult Post(Member m) => Ok(_service.Add(m));

        [HttpGet("instance")]
        public IActionResult Instance([FromServices] IMemberService other)
            => Ok(new { controller = _service.InstanceId, other = other.InstanceId });
        [HttpGet("boom")]
        public IActionResult Boom() => throw new Exception("test error");
    }
}
