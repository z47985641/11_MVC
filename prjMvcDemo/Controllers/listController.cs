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
        public ActionResult List()
        {
            List<CCustomer> datas = (new CCustomerFactry()).queryALL();
            return View(datas);
        }
        public ActionResult New()
        {
            return View();
        }
        public ActionResult Delete(int? id)
        {
            if (id != null)
                (new CCustomerFactry()).delete((int)id);
            return RedirectToAction("List");
        }
        public ActionResult Save()
        {
            CCustomer X = new CCustomer();
            X.fname = Request.Form["txtName"];
            X.fphone = Request.Form["txtPhone"];
            X.fEmail = Request.Form["txtEmail"];
            X.fAdress = Request.Form["txtAddress"];
            X.fPassword = Request.Form["txtPassword"];
            (new CCustomerFactry()).create(X);


            return RedirectToAction("List");
        }
    }
}