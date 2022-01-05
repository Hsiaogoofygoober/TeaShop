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
using Newtonsoft.Json.Linq;
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
        [HttpPost("{id:int}")]
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
                    PurchaseOrderId = id,
                    ProductId = item.ProductId,
                    PurchaseQuantity = item.PurchaseQuantity,
                    UnitPrice = item.UnitPrice
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
        [HttpPut("{id:int}")]
        public async Task<JsonResult> UpdatePurchaseDetail(int id, [Bind(include: "ProductId, PurchaseQuantity, ")] PurchaseOrderDetail _pod)
        {
            var configurationBuilder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json");

            IConfiguration config = configurationBuilder.Build();
            string connectinoString = config["ConnectionStrings:DBConnectionString"];

            var Conn = new SqlConnection(connectinoString);
            Conn.Open();

            string sqlstr = "UPDATE PurchaseOrderDetail SET ProductId = @ProductId, PurchaseQuantity = @PurchaseQuantity, UnitPrice = @UnitPrice";
            sqlstr += " WHERE PurchaseOrderID = @PurchaseOrderID";

            int affectRows = await Conn.ExecuteAsync(sqlstr, new
            {
                PurchaseOrderId = id,
                ProductId = _pod.ProductId,
                PurchaseTotal = _pod.PurchaseQuantity,
                UnitPrice = _pod.UnitPrice
            });

            return new JsonResult(1);
        }

        // DELETE api/<PurchaseDetailController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
