using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.IO;
using Microsoft.Data.SqlClient;
using Dapper;
using static Dapper.SqlMapper;
using WebApplication1.Models;
using System.Collections.Generic;
using System.Linq;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StockController : ControllerBase
    {

        //[HttpGet]
        //public JsonResult Get()
        //{
        //    var configurationBuilder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json");

        //    IConfiguration config = configurationBuilder.Build();
        //    string connectinoString = config["ConnectionStrings:DBConnectionString"];

        //    var Conn = new SqlConnection(connectinoString);
        //    Conn.Open();

        //    string sqlstr = "Select s.StockId, s.StockAmount, s.ProductId, p.Name, p.Type, p.ProductCategory, p.ProductPicture, p.ProductDescription, pod.PurchaseQuantity, sod.SqlesQuantity";
        //    sqlstr += " From [Stock] AS s INNER JOIN [Product] AS p ON p.ProductId = s.ProductId";
        //    sqlstr += " INNER JOIN [PurchaseOrderDetail] AS pod ON pod.ProductId = s.ProductId";
        //    sqlstr += " INNER JOIN [SalesOrderDetail] AS sod ON sod.ProductId = s.ProductId";

        //    SqlDataReader dr = null;
        //    SqlCommand cmd = new SqlCommand(sqlstr, Conn);
        //    dr = cmd.ExecuteReader();

        //    return new JsonResult(multi);
        //}
    }
}
