using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("[controller]")]
public class LoginController : ControllerBase
{
    [HttpPost("{username}")]
    public IActionResult Login(string username)
    {
        if (string.IsNullOrEmpty(username))
        {
            return BadRequest("El nombre de usuario es obligatorio.");
        }

        return Ok(username);
    }
}
