using prjMvcDemo.Models;
using prjXamlDemo.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace prjMvcDemo.Controllers
{
    public class AController : Controller
    {

        public string sayHello()
        {
            return "hello asp.net";
        }
        public string lotto()
        {
            return (new CLottoGen()).getNumber();
        }

        public string demoResponse()
        {
            Response.Clear();
            Response.ContentType = "application/octet-stream";
            Response.Filter.Close();
            Response.WriteFile(@"C:\ASP.NET\上課筆記\螢幕擷取畫面 2022-09-16 120238.png");
            Response.End();
            return "";
        }


        public string demoRequest()   //?pid=2 問號右邊傳入至左側網址
        {
            string id = Request.QueryString["pid"];
            if (id == "1")
                return "XBox 加入購物車成功";
            else if (id == "2")
                return "PS5 加入購物車成功";
            return "找不到該產品資料";
        }
        public string demoParameter(int? id)   
        {
            if (id == null)
                return "未設定ID";
            if (id == 1)
                return "XBox 加入購物車成功";
            else if (id == 2)
                return "PS5 加入購物車成功";
            return "找不到該產品資料";
        }
        public string queryById(int? id)
        {
            string s = "未設定查詢條件";
            if (id == null)
                return s;
            SqlConnection con = new SqlConnection();
            con.ConnectionString = @"Data Source=.;Initial Catalog=Northwind;Integrated Security=True";
            con.Open();

            SqlCommand cmd = new SqlCommand("SELECT * FROM tCustomer WHERE fId=" + id.ToString(), con);
            SqlDataReader reader = cmd.ExecuteReader();
            s = "查詢不到任何資料";
            if (reader.Read())
            {
                s = reader["fName"].ToString() + " / " + reader["fPhone"].ToString();
            }
            con.Close();
                return s;



        }

        public string demoServer()
        {
            return "目前伺服器上的實體位置：" + Server.MapPath("..");
        }

        // GET: A
        public ActionResult Index(int? id)
        {
            CCustomer X = null;
            if (id != null)
            { 
            SqlConnection con = new SqlConnection();
            con.ConnectionString = @"Data Source=.;Initial Catalog=dbdemo;Integrated Security=True";
            con.Open();

            SqlCommand cmd = new SqlCommand("SELECT * FROM tCustomer WHERE fId=" + id.ToString(), con);
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                    X = new CCustomer();
                    X.fid = (int)reader["fid"];
                    X.fname = reader["fname"].ToString();
                    X.fphone = reader["fphone"].ToString();
                    X.fEmail = reader["fEmail"].ToString();
            }
            con.Close();
            }
            return View(X);
        }
        public string testingdelete(int? id)
        {
            if (id == null)
                return "是不是在鬧";
              
            (new CCustomerFactry()).delete((int)id);
            return "刪除資料成功";
        }
        public string testinginsert()
        {
            CCustomer X = new CCustomer()
            {
                fname = "林軒",
                fphone = "0985623147",
                fEmail = "zxc321@gmail.com",
                fAdress = "新北市",
                fPassword = "assd123456"
            
            };
            (new CCustomerFactry()).create(X);
            return "新增成功";
        }

    }
}