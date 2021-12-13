//using Microsoft.Data.SqlClient;
//using Microsoft.Extensions.Configuration;
//using System;
//using System.Collections.Generic;
//using System.Data;
//using System.IO;
//using System.Linq;
//using System.Threading.Tasks;

//namespace WebApplication1.Repositories
//{
//    public class MyRepository : IRepository
//    {
//        public SqlConnection ConnOpen()
//        {
//            var configurationBuilder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json");

//            IConfiguration config = configurationBuilder.Build();
//            string connectinoString = config["ConnectionStrings:DBConnectionString"];

//            var Conn = new SqlConnection(connectinoString);
//            Conn.Open();

//            return Conn;
//        }

//        public void ConnClose(SqlConnection Conn)
//        {
//            if (Conn.State == ConnectionState.Open)
//            {
//                Conn.Close();
//            }
//        }
//    }
//}
