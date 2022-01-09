using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using Microsoft.Data.SqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using WebApplication1.Models;
using Newtonsoft.Json.Linq;
using Dapper;
using static Dapper.SqlMapper;
using Microsoft.AspNetCore.Authorization;

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

            SqlDataReader dr = null;
            string sqlstr = "Select p.ProductId, p.Name, p.Type, p.ProductCategory, s.StockId, s.StockAmount";
            sqlstr += " From [Product] AS p INNER JOIN [Stock] AS s ON p.ProductId = s.ProductId";

            //var endData = Conn.Query<Product, Stock, Product>(
            //        //             *** 一對多的兩個關聯式資料表      *** 仍用第一個資料表 
            //        sqlstr,
            //        (p, s) =>
            //        {
            //            p.Stocks = s;
            //            return p;
            //        },
            //        splitOn: "ProductId") // 重點!! "一對多"兩個關聯式資料表，表示"多"的那個(學生)資料表的ID(Key)
            //        .Distinct();    // 加上這一段，可以把「重複的科系」資料取消，如果不加上這一句，「科系」會重複出現。

            SqlCommand cmd = new SqlCommand(sqlstr, Conn);
            dr = cmd.ExecuteReader();

            List<PSViewModel> resultViewModel = new List<PSViewModel>();

            while (dr.Read())
            {
                Product p = new Product
                {
                    ProductId = Convert.ToInt32(dr["ProductId"]),
                    Name = dr["Name"].ToString(),
                    Type = dr["Type"].ToString(),
                    ProductCategory = dr["ProductCategory"].ToString(),

                };

                Stock s = new Stock
                {
                    StockId = Convert.ToInt32(dr["StockId"]),
                    StockAmount = Convert.ToInt32(dr["StockAmount"]),
                };

                resultViewModel.Add(new PSViewModel { PVM = p, SVM = s});
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


        [Authorize]
        [HttpPost]
        public JsonResult CreateProduct([FromBody]JObject data)
        {
            var configurationBuilder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json");

            IConfiguration config = configurationBuilder.Build();
            string connectinoString = config["ConnectionStrings:DBConnectionString"];

            var Conn = new SqlConnection(connectinoString);
            Conn.Open();

            Product _p = data["Product"].ToObject<Product>();

            string sqlstr1 = "INSERT INTO Product ([Name], [Type], [ProductCategory], [ProductDescription])";
            sqlstr1 += " VALUES (@Name, @Type, @ProductCategory, @ProductDescription)";

            int affectRows1 = Conn.Execute(sqlstr1, new
            {
                Name = _p.Name,
                Type = _p.Type,
                ProductCategory = _p.ProductCategory,
                ProductDescription = _p.ProductDescription
            });

            string sql = "SELECT ProductId FROM [Product]";
            sql += " WHERE Type = @Type AND Name = @Name";

            SqlDataReader dr = null;
            SqlCommand cmd = new SqlCommand(sql, Conn);
            cmd.Parameters.AddWithValue("@Type", _p.Type); // 防止Sql Injection\
            cmd.Parameters.AddWithValue("@Name", _p.Name);
            dr = cmd.ExecuteReader();

            Product _product = null ;
            while (dr.Read()) 
            {
                _product = new Product
                {
                    ProductId = Convert.ToInt32(dr["ProductId"]),
                };     
            }

            string sqlstr2 = "INSERT INTO Stock ([ProductID], [StockAmount])";
            sqlstr2 += " VALUES (@ProductID, @StockAmount)";

            int affectRows2 = Conn.Execute(sqlstr2, new
            {
                ProductId = _product.ProductId,
                StockAmount = 0
            });

            string sqlstr3 = "UPDATE Product SET ProductPicture = @ProductPicture";
            sqlstr2 += " WHERE ProductId = @ProductId";

            int affectRows3 = Conn.Execute(sqlstr3, new
            {
                ProductId = _product.ProductId,
                ProductPicture = "../productImg/"+ _product.ProductId +".jpg"
            });

            if (dr != null)
            {
                cmd.Cancel();
                dr.Close();
            }

            if (Conn.State == ConnectionState.Open)
            {
                Conn.Close();
            }

            return new JsonResult(affectRows1 + affectRows2);
        }
    }
}
