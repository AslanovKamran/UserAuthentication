using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UserAuth.Repository.Interfaces;

namespace UserAuth.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	[Produces("application/json")]
	public class UsersController : ControllerBase
	{
		private readonly IUserRepository _repos;
		public UsersController(IUserRepository repos) => _repos = repos;

		[HttpGet]
		[ProducesResponseType(200)]
		[ProducesResponseType(401)]
		[ProducesResponseType(403)]
		//[Authorize(Roles = "Admin")]
		public async Task<IActionResult> GetUsers()
		{
			var users = await _repos.GetUsersAsync();
			return Ok(users);
		}

		[HttpGet]
		[Route("{id}")]
		[ProducesResponseType(200)]
		[ProducesResponseType(401)]
		[ProducesResponseType(403)]
		[ProducesResponseType(404)]
		//[Authorize(Roles = "Admin")]
		public async Task<IActionResult> GetUser(int id)
		{
			var user = await _repos.GetUserAsync(id);
			return user == null ? NotFound("No Such A User") : Ok(user);
		}
	}
}
