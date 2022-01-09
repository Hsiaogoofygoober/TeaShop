using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Models;
using Dapper;
using static Dapper.SqlMapper;
using Microsoft.AspNetCore.Authorization;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SalesDetailController : ControllerBase
    {

        [HttpPost("{id:int}")]
        public JsonResult Create(int id,[FromBody]JObject data)
        {
            var configurationBuilder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json");

            IConfiguration config = configurationBuilder.Build();
            string connectinoString = config["ConnectionStrings:DBConnectionString"];

            var Conn = new SqlConnection(connectinoString);
            Conn.Open();

            List<SalesOrderDetail> s1 = new List<SalesOrderDetail>();
            for (int i = 0; i < data.Count / 3; i++)
            {
                string productId = "ProductId" + i;
                string productQuantity = "SalesQuantity" + i;
                string productPrice = "UnitPrice" + i;

                if (data.GetValue(productId) == null || data.GetValue(productQuantity) == null)
                {
                    return new JsonResult(0);
                }

                SalesOrderDetail s = new SalesOrderDetail
                {
                    SalesOrderId = id,
                    ProductId = Convert.ToInt32(data.GetValue(productId)),
                    SalesQuantity = Convert.ToInt32(data.GetValue(productQuantity)),
                    UnitPrice = Convert.ToDecimal(data.GetValue(productPrice))
                };

                s1.Add(s);
            }

            string sqlstr = "INSERT INTO [SalesOrderDetail] ([SalesOrderId], [ProductId], [SalesQuantity], [UnitPrice])";
            sqlstr += " VALUES (@SalesOrderId, @ProductId, @SalesQuantity, @UnitPrice)";

            int affectRows = Conn.Execute(sqlstr, s1);

            if (Conn.State == ConnectionState.Open)
            {
                Conn.Close();
            }

            return new JsonResult(affectRows);
        }

        // PUT api/PurchaseDetail/5
        [HttpPut("{id:int}")]
        public async Task<JsonResult> UpdateSalesDetail(int id, [Bind(include: "ProductId, SalesQuantity, UnitPrice")] SalesOrderDetail _sod)
        {
            var configurationBuilder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json");

            IConfiguration config = configurationBuilder.Build();
            string connectinoString = config["ConnectionStrings:DBConnectionString"];

            var Conn = new SqlConnection(connectinoString);
            Conn.Open();

            string sqlstr = "UPDATE SalesOrderDetail SET ProductId = @ProductId, SalesQuantity = @SalesQuantity, UnitPrice = @UnitPrice";
            sqlstr += " WHERE SalesOrderID = @SalesOrderID";

            int affectRows = await Conn.ExecuteAsync(sqlstr, new
            {
                SalesOrderId = id,
                ProductId = _sod.ProductId,
                SalesTotal = _sod.SalesQuantity,
                UnitPrice = _sod.UnitPrice
            });

            return new JsonResult(affectRows);
        }
    }
}
