using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GeexBlog.Controllers
{
    public class AdminController : Controller
    {
        // GET: Index
        public ActionResult Index()
        {
            DataTable dt = new DataTable();

            using (SQLiteConnector connector = new SQLiteConnector())
            {
                dt = connector.LoadData("Select * from posts");
            }


            using (SQLiteConnector connector = new SQLiteConnector())
            {
                connector.ExecuteQuery("insert into posts(title,content,created) values('başlık','content','tarih')");
            }


            return View();
        }
    }
}