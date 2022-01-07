using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Data.SqlClient;
using System.Collections.Generic;
using System.IO;
using System.Data;
using WebApplication1.Models;
using Dapper;
using static Dapper.SqlMapper;
using System.Threading.Tasks;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        [HttpGet]
        public async Task<JsonResult> Get() 
        {
            var configurationBuilder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json");

            IConfiguration config = configurationBuilder.Build();
            string connectinoString = config["ConnectionStrings:DBConnectionString"];

            var Conn = new SqlConnection(connectinoString);
            Conn.Open();

            string sqlstr = "Select * From [Product]";
            IEnumerable<Product> result = await Conn.QueryAsync<Product>(sqlstr, Conn);

            if (Conn.State == ConnectionState.Open)
            {
                Conn.Close();
            }

            return new JsonResult(result);
        }

        [HttpPost]
        public async Task<JsonResult> Post([FromBody]Product _product) 
        {
            if (_product.Name != "" && _product.Type != "" && _product.ProductCategory != "" && _product.ProductDescription != "")
            {
                var configurationBuilder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json");

                IConfiguration config = configurationBuilder.Build();
                string connectinoString = config["ConnectionStrings:DBConnectionString"];

                var Conn = new SqlConnection(connectinoString);
                Conn.Open();

                string sqlstr = "INSERT INTO [Product] ([Name], [Type], [ProductCategory], [ProductDescription])";
                sqlstr += " VALUES (@Name, @Type, @ProductCategory, @ProductDescription)";
                
                int affectedRows = await Conn.ExecuteAsync(sqlstr, new
                {
                    Name = _product.Name,
                    Type = _product.Type,
                    ProductCategory = _product.ProductCategory,
                    ProductDescription = _product.ProductDescription,
                });

                return new JsonResult(affectedRows);
            }

            return new JsonResult(0);
        }

        [HttpPut]
        public async Task<JsonResult> UpdateProduct([Bind(include: "Name, Type, ProductCategory, ProductDescription")]Product _product) 
        {
            var configurationBuilder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json");

            IConfiguration config = configurationBuilder.Build();
            string connectinoString = config["ConnectionStrings:DBConnectionString"];

            var Conn = new SqlConnection(connectinoString);
            Conn.Open();

            string sqlstr = "UPDATE Product SET Name = @Name, Type = @Type, ProductCategory = @ProductCategory, ProductDescription = @ProductDescription";
            sqlstr += " WHERE ProductId = @ProductId";

            int affectRows = await Conn.ExecuteAsync(sqlstr, new
            {
               ProductId = _product.ProductId,
               Name = _product.Name,
               Type = _product.Type,
               ProductCategory = _product.ProductCategory,
               ProductDescription = _product.ProductDescription
            });

            if (Conn.State == ConnectionState.Open)
            {
                Conn.Close();
            }

            return new JsonResult(affectRows);
        }
    }
}
