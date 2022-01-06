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

            string sqlstr = "Select poh.PurchaseOrderId, poh.SupplierId, poh.PurchaseTotal, poh.PurchaseDate, poh.Arrival, s.SupplierId, s.SupplierName";
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

        [HttpGet("{id:int}")]
        public JsonResult GetDetails(int id)
        {
            var configurationBuilder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json");

            IConfiguration config = configurationBuilder.Build();
            string connectinoString = config["ConnectionStrings:DBConnectionString"];

            var Conn = new SqlConnection(connectinoString);
            Conn.Open();

            SqlDataReader dr = null;
            string sqlstr = "SELECT poh.PurchaseOrderID, poh.PurchaseDate, s.SupplierName, pod.ProductID, p.Type, p.ProductCategory, p.Name, pod.PurchaseQuantity, pod.UnitPrice FROM PurchaseOrderHeader AS poh";
            sqlstr += " INNER JOIN PurchaseOrderDetail AS pod ON pod.PurchaseOrderID = poh.PurchaseOrderID";
            sqlstr += " INNER JOIN Supplier AS s ON s.SupplierID = poh.SupplierID";
            sqlstr += " INNER JOIN Product AS p ON p.ProductID = pod.ProductID";
            sqlstr += " WHERE poh.PurchaseOrderID = @PurchaseOrderID";

            SqlCommand cmd = new SqlCommand(sqlstr, Conn);
            cmd.Parameters.AddWithValue("@PurchaseOrderID", id); // 防止Sql Injection
            dr = cmd.ExecuteReader();

            List<SPViewModel> resultViewModel = new List<SPViewModel>();

            while (dr.Read()) 
            {
                PurchaseOrderHeader poh = new PurchaseOrderHeader
                {
                    PurchaseOrderId = Convert.ToInt32(dr["PurchaseOrderId"]),
                    PurchaseDate = Convert.ToDateTime(dr["PurchaseDate"]),
                };

                Supplier s = new Supplier
                {
                    SupplierName = dr["SupplierName"].ToString(),
                };

                Product p = new Product
                {
                    ProductId = Convert.ToInt32(dr["ProductId"]),
                    Type = dr["Type"].ToString(),
                    ProductCategory = dr["ProductCategory"].ToString(),
                    Name = dr["Name"].ToString()
                };

                PurchaseOrderDetail pod = new PurchaseOrderDetail
                {
                    PurchaseQuantity = Convert.ToInt32(dr["PurchaseQuantity"]),
                    UnitPrice = Convert.ToDecimal(dr["UnitPrice"])
                };
                resultViewModel.Add(new SPViewModel { PodVM = pod, PohVM = poh, SVM = s, PVM = p });    
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

        [HttpPost]
        public async Task<JsonResult> CreatePurchaseHeader([FromBody]PurchaseOrderHeader _poh) 
        {
            var configurationBuilder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json");

            IConfiguration config = configurationBuilder.Build();
            string connectinoString = config["ConnectionStrings:DBConnectionString"];

            var Conn = new SqlConnection(connectinoString);
            Conn.Open();

            string sqlstr = "INSERT INTO [PurchaseOrderHeader] ([SupplierID], [PurchaseTotal], [PurchaseDate])";
            sqlstr += " VALUES (@SupplierID, @PurchaseTotal, @PurchaseDate)";

            int affectRows = await Conn.ExecuteAsync(sqlstr, new
            {
                SupplierID = _poh.SupplierId,
                PurchaseTotal = _poh.PurchaseTotal,
                PurchaseDate = _poh.PurchaseDate,
            });

            if (Conn.State == ConnectionState.Open) 
            {
                Conn.Close();
            }

            return new JsonResult(affectRows);
        }

        [HttpPut]
        public async Task<JsonResult> UpdatePurchaseHeader([Bind(include: "SupplierId, PurchaseTotal, PurchaseDate")]PurchaseOrderHeader _poh) 
        {
            var configurationBuilder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json");

            IConfiguration config = configurationBuilder.Build();
            string connectinoString = config["ConnectionStrings:DBConnectionString"];

            var Conn = new SqlConnection(connectinoString);
            Conn.Open();

            string sqlstr = "UPDATE PurchaseOrderHeader SET SupplierId = @SupplierId, PurchaseTotal = @PurchaseTotal";
            sqlstr += " WHERE PurchaseOrderID = @PurchaseOrderID";

            int affectRows = await Conn.ExecuteAsync(sqlstr, new
            {
                PurchaseOrderId = _poh.PurchaseOrderId,
                SupplierId = _poh.SupplierId,
                PurchaseTotal = _poh.PurchaseTotal,
                PurchaseDate = _poh.PurchaseDate
            });

            if (Conn.State == ConnectionState.Open)
            {
                Conn.Close();
            }

            return new JsonResult(affectRows);
        }

        [HttpDelete("{id:int}")]
        public async Task<JsonResult> DeletePurchaseHeader(int id)
        {
            var configurationBuilder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json");

            IConfiguration config = configurationBuilder.Build();
            string connectinoString = config["ConnectionStrings:DBConnectionString"];

            var Conn = new SqlConnection(connectinoString);
            Conn.Open();

            string sqlstr = "DELETE FROM PurchaseOrderHeader";
            sqlstr += " WHERE PurchaseOrderId = @PurchaseOrderId";

            int affectRows = await Conn.ExecuteAsync(sqlstr, new
            {
                PurchaseOrderId = id
            }); 

            if (Conn.State == ConnectionState.Open)
            {
                Conn.Close();
            }

            return new JsonResult(affectRows);
        }
    }
}
