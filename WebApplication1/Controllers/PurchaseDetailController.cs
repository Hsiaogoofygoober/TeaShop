using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Models;
using Dapper;
using static Dapper.SqlMapper;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PurchaseDetailController : ControllerBase
    {
        // GET: api/<PurchaseDetailController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<PurchaseDetailController>/5
        //[HttpGet("{id}")]
        //public string Get(int id)
        //{
        //    return "value";
        //}
        // POST api/<PurchaseDetailController>
        [HttpPost("{id}")]
        public async Task<JsonResult> CreateDetails(int id, [FromBody] List<PurchaseOrderDetail> _pod)
        {
            var configurationBuilder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json");

            IConfiguration config = configurationBuilder.Build();
            string connectinoString = config["ConnectionStrings:DBConnectionString"];

            var Conn = new SqlConnection(connectinoString);
            Conn.Open();

            string sqlstr = "INSERT INTO [PurchaseOrderDetail] ([PurchaseOrderId], [ProductId], [PurchaseQuantity], [UnitPrice])";
            sqlstr += " VALUES (@PurchaseOrderId, @ProductId, @PurchaseQuantity, @UnitPrice)";

            var ListProduct = new List<PurchaseOrderDetail>();
            foreach (var item in _pod)
            {
                ListProduct.Add(new PurchaseOrderDetail()
                {
                    PurchaseOrderId = id, ProductId = item.ProductId, PurchaseQuantity = item.PurchaseQuantity, UnitPrice = item.UnitPrice
                });
            }

            int affectRows = await Conn.ExecuteAsync(sqlstr, ListProduct);

            if (Conn.State == ConnectionState.Open)
            {
                Conn.Close();
            }

            return new JsonResult(affectRows);
}

        // PUT api/PurchaseDetail/5
        [HttpPut("{id}")]
        public async Task<JsonResult> UpdatePurchaseDetail([Bind(include: "ProductId, PurchaseQuantity, UnitPrice")] PurchaseOrderDetail _pod)
        {
            var configurationBuilder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json");

            IConfiguration config = configurationBuilder.Build();
            string connectinoString = config["ConnectionStrings:DBConnectionString"];

            var Conn = new SqlConnection(connectinoString);
            Conn.Open();

            string sqlstr = "UPDATE PurchaseOrderDetail SET SupplierId = @SupplierId, PurchaseTotal = @PurchaseTotal";
            sqlstr += " WHERE PurchaseOrderID = @PurchaseOrderID";

            return new JsonResult(1);
        }

        // DELETE api/<PurchaseDetailController>/5
        [HttpDelete("{id}")]
public void Delete(int id)
{
}
    }
}
