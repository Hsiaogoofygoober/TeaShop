using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.IO;
using Microsoft.Data.SqlClient;
using Dapper;
using static Dapper.SqlMapper;
using WebApplication1.Models;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StockController : ControllerBase
    {

        [HttpGet]
        public JsonResult Get()
        {
            var configurationBuilder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json");

            IConfiguration config = configurationBuilder.Build();
            string connectinoString = config["ConnectionStrings:DBConnectionString"];

            var Conn = new SqlConnection(connectinoString);
            Conn.Open();

            string sqlstr = "Select p.ProductId, p.Name, p.Type, p.ProductCategory, s.ProductId, s.StockId, s.StockAmount";
            sqlstr += " From [Stock] AS s INNER JOIN [Product] AS p ON p.ProductId = s.ProductId";

            var resultDictionary = new Dictionary<int, Stock>();
            
            var endData = Conn.Query<Stock, Product, Stock>(
                    //             *** 一對多的兩個關聯式資料表      *** 仍用第一個資料表 
                    sqlstr,
                    (s, p) =>
                    {
                        s.Product = p;
                        return s;
                    },
                    splitOn: "ProductId") // 重點!! "一對多"兩個關聯式資料表，表示"多"的那個(學生)資料表的ID(Key)
                    .Distinct();    // 加上這一段，可以把「重複的科系」資料取消，如果不加上這一句，「科系」會重複出現。

            if (Conn.State == ConnectionState.Open)
            {
                Conn.Close();
            }

            return new JsonResult(endData);
        }
    }
}
