using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PizzaOrderWebApplication.Models;
using System.Data.SqlClient;
using PizzaOrderWebApplication.MyAPI;
using System.Configuration;
using System.Data;

namespace PizzaOrderWebApplication.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {

            return View();
        }

        public ActionResult Contact()
        {
            return View();
        }

        [HttpPost]
        public ActionResult GetContact(String name, String email, String phone, String message)
        {
            using (PizzaExpressModel context = new PizzaExpressModel())
            {
                object[] contactparams = new object[]
                {
                    new SqlParameter("@name", name),
                    new SqlParameter("@email", email),
                    new SqlParameter("@phone", phone),
                    new SqlParameter("@message", message)
                };
                context.Database.ExecuteSqlCommand("insert into Contact values(@name, @email, @phone, @message)", contactparams);
            }
            return Redirect("/Home/");
        }
        //Login
        public ActionResult Login(String thongbao)
        {
            ViewBag.thongbao2 = thongbao;
            return View();
        }
        //login nhan data
        [HttpPost]
        public ActionResult LoginAccess(String nname, String pass)
        {
            //tao ket noi
            // string strConnection = ConfigurationManager.ConnectionStrings["PizzaDB"].ToString();
            String strConnection = "Data Source=MANHDUNGIT\\SQLEXPRESS;Integrated Security= true;Initial Catalog=PizzaOrderWebApp";
            SqlConnection conn = new SqlConnection(strConnection);
            //thuc hien truy van
            DataTable dataTable = new DataTable();
            String querry = "select * from Accountuser where CustomerName ='" + nname + "' and Password ='" + pass + "'";
            try
            {
                conn.Open();
                SqlDataAdapter adapter = new SqlDataAdapter(querry, conn);
                adapter.Fill(dataTable);
            }
            catch (Exception ex) { }
            finally
            {
                conn.Close();
            }

            if (dataTable.Rows.Count > 0)
            {
                Session["AccountUser"] = "";
                //tao doi tuong luu vao session
                String name = dataTable.Rows[0][0].ToString();
                String mail = dataTable.Rows[0][1].ToString();
                String password = dataTable.Rows[0][2].ToString();
                String address = dataTable.Rows[0][3].ToString();
                String phone = dataTable.Rows[0][4].ToString();
                DateTime dateofbirth = DateTime.Parse(dataTable.Rows[0][5].ToString());
                User user = new User(name, mail, password, address, phone, dateofbirth);
                //luu session
                Session["AccountUser"] = user;
                Session.Timeout = 30;
                return Redirect("Index");
            }
            return RedirectToAction("Login", new { thongbao = "Đăng nhập thất bại" });


        }
        //Register
        public ActionResult Register(String thongbao)
        {
            ViewBag.thongbao2 = thongbao;
            return View();
        }
        //Register nhan data
        [HttpPost]
        public ActionResult RegisterAccess(String uname, String nemail, String pass, String address, String phone, DateTime birthday)
        {
            User user = new User(uname,nemail,pass,address,phone,birthday);
            //tao ket noi
            // string strConnection = ConfigurationManager.ConnectionStrings["PizzaDB"].ToString();
            String strConnection = "Data Source=MANHDUNGIT\\SQLEXPRESS;Integrated Security= true;Initial Catalog=PizzaOrderWebApp";
            SqlConnection conn = new SqlConnection(strConnection);
            //thuc hien truy van
            int k = 0;
            String querry = "insert into Accountuser(CustomerName,Email,Password,Address,Phone,Dateofbirth) values('" + uname + "','" + nemail + "','"+pass+"','"+address+"','"+phone+"','"+birthday+"')";
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand();
                SqlCommand command = new SqlCommand(querry, conn);
                k = (int)command.ExecuteNonQuery();
            }
            catch (Exception ex) { }
            finally
            {
                conn.Close();
            }

            if(k==0)
            {
                return RedirectToAction("Register", new { thongbao = "This Account has existed,please create another account" });
            }
            return Redirect("Login");


        }
    }
}