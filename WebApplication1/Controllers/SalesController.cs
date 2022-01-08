using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.IO;
using WebApplication1.Models;
using Dapper;
using static Dapper.SqlMapper;
using System.Data;
using System.Threading.Tasks;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SalesController : ControllerBase
    {

        [HttpGet]
        public async Task<JsonResult> Get()
        {
            var configurationBuilder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json");

            IConfiguration config = configurationBuilder.Build();
            string connectinoString = config["ConnectionStrings:DBConnectionString"];

            var Conn = new SqlConnection(connectinoString);
            Conn.Open();

            string sqlstr = "Select * From [SalesOrderHeader]";
            IEnumerable<SalesOrderHeader> result = await Conn.QueryAsync<SalesOrderHeader>(sqlstr, Conn);

            if (Conn.State == ConnectionState.Open)
            {
                Conn.Close();
            }

            return new JsonResult(result);
        }

        [HttpGet("{id}")]
        public async Task<JsonResult> GetDetails(int id) 
        {
            var configurationBuilder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json");

            IConfiguration config = configurationBuilder.Build();
            string connectinoString = config["ConnectionStrings:DBConnectionString"];

            var Conn = new SqlConnection(connectinoString);
            Conn.Open();

            string sqlstr = "Select * From [SalesOrderDetail] Where SalesOrderId = @SalesOrderId";
            var parameter = new { SalesOrderId = id };
            IEnumerable<SalesOrderDetail> result = await Conn.QueryAsync<SalesOrderDetail>(sqlstr, parameter);

            if (Conn.State == ConnectionState.Open)
            {
                Conn.Close();
            }

            return new JsonResult(result);
        }

        [HttpDelete("{id:int}")]
        public async Task<JsonResult> DeleteSalesHeader(int id)
        {
            var configurationBuilder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json");

            IConfiguration config = configurationBuilder.Build();
            string connectinoString = config["ConnectionStrings:DBConnectionString"];

            var Conn = new SqlConnection(connectinoString);
            Conn.Open();

            string sqlstr = "DELETE FROM SalesOrderHeader";
            sqlstr += " WHERE SalesOrderId = @SalesOrderId";

            int affectRows = await Conn.ExecuteAsync(sqlstr, new
            {
                SalesOrderId = id
            });

            if (Conn.State == ConnectionState.Open)
            {
                Conn.Close();
            }

            return new JsonResult(affectRows);
        }
    }
}
