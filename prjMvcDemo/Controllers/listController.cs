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
            string keyword = Request.Form["txtKeyword"];
            List<CCustomer> datas = null;
            if(string.IsNullOrEmpty(keyword))
                datas = (new CCustomerFactry()).queryALL();
            else
                datas = (new CCustomerFactry()).queryByKeyword(keyword);

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
        public ActionResult Edit(int? id)
        {
            if (id != null)
            {
                CCustomer X =  (new CCustomerFactry()).querybyId((int)id);
                if (X != null)
                    return View(X);
            }
            return RedirectToAction("List");
        }
        [HttpPost]
        public ActionResult Edit(CCustomer X)
        {
           (new CCustomerFactry()).update(X);

            return RedirectToAction("List");
        }
    }
}