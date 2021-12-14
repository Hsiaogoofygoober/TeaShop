using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Models;
using Dapper;
using static Dapper.SqlMapper;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PurchaseController : ControllerBase
    {
        [HttpGet]
        public JsonResult Get()
        {
            var configurationBuilder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json");

            IConfiguration config = configurationBuilder.Build();
            string connectinoString = config["ConnectionStrings:DBConnectionString"];

            var Conn = new SqlConnection(connectinoString);
            Conn.Open();

            string sqlstr = "Select poh.PurchaseOrderId, poh.SupplierId, poh.PurchaseTotal, poh.PurchaseDate, poh.Arrival, s.SupplierName";
            sqlstr += " From [PurchaseOrderHeader] AS poh INNER JOIN [Supplier] AS s";
            sqlstr += " ON poh.SupplierId = s.SupplierId";

            var resultDictionary = new Dictionary<int, PurchaseOrderHeader>();

            var endData = Conn.Query<PurchaseOrderHeader, Supplier, PurchaseOrderHeader>(
        //             *** 一對多的兩個關聯式資料表      *** 仍用第一個資料表 
                    sqlstr,
                    (poh, s) =>
                    {
                        poh.Supplier = s;
                        return poh;
                    },
                    splitOn: "SupplierId") // 重點!! "一對多"兩個關聯式資料表，表示"多"的那個(學生)資料表的ID(Key)
                    .Distinct();    // 加上這一段，可以把「重複的科系」資料取消，如果不加上這一句，「科系」會重複出現。

            if (Conn.State == ConnectionState.Open)
            {
                Conn.Close();
            }

            return new JsonResult(endData);
        }

        [HttpGet("{id}")]
        public JsonResult GetDetails(int id)
        {
            var configurationBuilder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json");

            IConfiguration config = configurationBuilder.Build();
            string connectinoString = config["ConnectionStrings:DBConnectionString"];

            var Conn = new SqlConnection(connectinoString);
            Conn.Open();

            string sqlstr = "Select * From [PurchaseOrderDetail] Where PurchaseOrderId = @PurchaseOrderId";
            var parameter = new { PurchaseOrderId = id };
            IEnumerable<PurchaseOrderDetail> result = Conn.Query<PurchaseOrderDetail>(sqlstr, parameter);

            if (Conn.State == ConnectionState.Open)
            {
                Conn.Close();
            }

            return new JsonResult(result);
        }
    }
}
