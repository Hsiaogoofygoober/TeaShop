using Microsoft.AspNetCore.Mvc;
//using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using System;
using System.IO;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Data.SqlClient;
using WebApplication1.Models;
using Dapper;
using static Dapper.SqlMapper;
using System.Collections.Generic;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {

        [HttpPost]
        public async Task<JsonResult> Login([FromBody]User _user) 
        {
            var configurationBuilder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json");

            IConfiguration config = configurationBuilder.Build();
            string connectinoString = config["ConnectionStrings:DBConnectionString"];

            var Conn = new SqlConnection(connectinoString);
            Conn.Open();

            string sqlstr = "Select * From [User] Where UserName = @UserName and Password = @Password";
            var parameter = new { UserName = _user.UserName, Password = _user.Password };

            User result = Conn.QuerySingleOrDefault<User>(sqlstr, parameter);

            if (result == null)
            {
                return new JsonResult(false);
            }
            else 
            {
                var claims = new List<Claim>
                    {
                        new Claim("UserID", result.UserId.ToString()),
                        new Claim(ClaimTypes.Name, result.UserName),
                        new Claim(ClaimTypes.Role, "Admininstrator")
                    };

                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                var authProperties = new AuthenticationProperties
                {
                    AllowRefresh = true,
                    ExpiresUtc = DateTimeOffset.UtcNow.AddHours(3),
                    IsPersistent = true
                };

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                                                                    new ClaimsPrincipal(claimsIdentity),
                                                                    authProperties);

                return new JsonResult(true);
            }
        }

        public async Task<bool> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            return true;
        }
    }
}
