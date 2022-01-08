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
using System;

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

        [HttpGet("{id:int}")]
        public JsonResult GetDetails(int id) 
        {
            var configurationBuilder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json");

            IConfiguration config = configurationBuilder.Build();
            string connectinoString = config["ConnectionStrings:DBConnectionString"];

            var Conn = new SqlConnection(connectinoString);
            Conn.Open();

            SqlDataReader dr = null;
            string sqlstr = "SELECT soh.SalesOrderID, soh.SalesDate, soh.Customer, sod.ProductID, p.Type, p.ProductCategory, p.Name, sod.SalesQuantity, sod.UnitPrice FROM SalesOrderHeader AS soh";
            sqlstr += " INNER JOIN SalesOrderDetail AS sod ON sod.SalesOrderID = soh.SalesOrderID";
            sqlstr += " INNER JOIN Product AS p ON p.ProductID = sod.ProductID";
            sqlstr += " WHERE soh.SalesOrderID = @SalesOrderID";

            SqlCommand cmd = new SqlCommand(sqlstr, Conn);
            cmd.Parameters.AddWithValue("@SalesOrderID", id); // 防止Sql Injection
            dr = cmd.ExecuteReader();

            List<SSViewModel> resultViewModel = new List<SSViewModel>();

            while (dr.Read())
            {
                SalesOrderHeader soh = new SalesOrderHeader
                {
                    SalesOrderId = Convert.ToInt32(dr["SalesOrderId"]),
                    SalesDate = Convert.ToDateTime(dr["SalesDate"]),
                    Customer = (dr["Customer"].ToString())
                };

                Product p = new Product
                {
                    ProductId = Convert.ToInt32(dr["ProductId"]),
                    Type = dr["Type"].ToString(),
                    ProductCategory = dr["ProductCategory"].ToString(),
                    Name = dr["Name"].ToString()
                };

                SalesOrderDetail sod = new SalesOrderDetail
                {
                    SalesQuantity = Convert.ToInt32(dr["SalesQuantity"]),
                    UnitPrice = Convert.ToDecimal(dr["UnitPrice"])
                };
                resultViewModel.Add(new SSViewModel { SODVM = sod, SOHVM = soh, PVM = p });
            }

            if (dr != null)
            {
                cmd.Cancel();
                dr.Close();
            }

            if (Conn.State == ConnectionState.Open)
            {
                Conn.Close();
            }

            return new JsonResult(resultViewModel);
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
