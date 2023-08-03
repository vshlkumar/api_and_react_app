using EntityFrameworkUI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Security.Cryptography;
using System.Text;

namespace EntityFrameworkUI.Controllers
{
    [Route("/api/home")]
    public class HomeController : ControllerBase
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IToken _token;
        private readonly IConfiguration _configuration;

        public HomeController(ILogger<HomeController> logger, IToken token, IConfiguration configuration)
        {
            _logger = logger;
            _token = token;
            _configuration = configuration; 
        }

        [HttpGet("name")]
        public async Task<IActionResult> GetName()
        {
            using (SHA256 sha256Hash = SHA256.Create())
            {
                // ComputeHash - returns byte array
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes("Test@123"));
                // Convert byte array to a string
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }

            }

            var token = await _token.GetJwt("test@gmail.com", "test@123");
            var aa = _configuration["username"];
            Response.Cookies.Append("token", token);
            return Ok("token created and saved to cookie");
        }

        [HttpGet("token")]
        public async Task<IActionResult> GetToken()
        {
            var cookie = Request.Cookies.TryGetValue("token", out var token);

            if (cookie != null)
            {
                
            }

            return Ok(new List<string> { "A", "B" });
        }
    }
}