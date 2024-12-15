using Application.UserFeatures;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("user")]
    public class UserController : ControllerBase
    {
        private readonly IUserApplication _userApplication;

        public UserController(IUserApplication userApplication)
        {
            _userApplication = userApplication;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUser([FromRoute] Guid id)
        {
            var result = await _userApplication.GetByIdAsync(id);

            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        // NOTE: this endpoint has been created only for convenience
        [HttpGet]
        public async Task<IActionResult> GetAllUsers()
        {
            var result = await _userApplication.GetAllAsync();

            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }


        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] CreateUserDto createUserDto)
        {
            var id = await _userApplication.CreateAsync(createUserDto);

            return this.Created($"{this.HttpContext.Request.Path}/{id}", null);
        }

        [HttpPatch("{id}/deactivate")]
        public async Task<IActionResult> DeactivateUser([FromRoute] Guid id)
        {
            var result = await _userApplication.DeactivateAsync(id);

            if (result == null)
            {
                return NotFound();
            }

            return Ok();
        }
    }
}
