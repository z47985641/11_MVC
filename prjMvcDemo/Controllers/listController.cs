using prjMvcDemo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace prjMvcDemo.Controllers
{
    public class listController : Controller
    {
        // GET: list
        public ActionResult Index()
        {
            List<CCustomer> datas = (new CCustomerFactry()).queryALL();
            return View(datas);
        }
    }
}