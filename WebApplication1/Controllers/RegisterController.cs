using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Models;
using Dapper;
using static Dapper.SqlMapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Data.SqlClient;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegisterController : ControllerBase
    {

        [HttpPost]
        public int Register([FromBody]User _user)
        {
            if (_user != null) 
            {
                var configurationBuilder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json");

                IConfiguration config = configurationBuilder.Build();
                string connectinoString = config["ConnectionStrings:DBConnectionString"];

                var Conn = new SqlConnection(connectinoString);
                Conn.Open();

                string sqlstr = "INSERT INTO [User] ([UserName], [Password])";
                sqlstr += " VALUES (@UserName, @Password)";

                int affectedRows = Conn.Execute(sqlstr, new
                {
                    UserName = _user.UserName,
                    Password = _user.Password
                });

                return affectedRows;
            }
            return 0;                
        }
    }
}
