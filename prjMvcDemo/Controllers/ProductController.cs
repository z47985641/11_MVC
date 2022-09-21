using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace prjMvcDemo.Controllers
{
    public class ProductController : Controller
    {
        // GET: Product
        public ActionResult List()
        {

            string keyword_string = Request.Form["txtKeyword"];
            int keyword_int = Convert.ToInt32(Request.Form["txtKeyword"]);
            //造成無法模糊搜尋
            dbdemoEntities db = new dbdemoEntities();
            var datas = from p in db.tProduct
                        select p;
            if (!string.IsNullOrEmpty(keyword_string))
            {
                datas = from p in db.tProduct
                        where p.fId == keyword_int
                        || p.fName == keyword_string
                        || p.fCost == keyword_int
                        || p.dQty == keyword_int
                        select p;
            }
            return View(datas);
        }
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(tProduct p)
        {
            dbdemoEntities db = new dbdemoEntities();
            db.tProduct.Add(p);
            db.SaveChanges();
            return RedirectToAction("List");
        }
        public ActionResult Delete(int? id)
        {

            if (id != null)
            {
                dbdemoEntities db = new dbdemoEntities();
                tProduct prod = db.tProduct.FirstOrDefault(p => p.fId == id);
                if (prod != null)
                {
                    db.tProduct.Remove(prod);
                    db.SaveChanges();
                }
            }
            return RedirectToAction("List");
        }
        public ActionResult Edit(int? id)
        {
            dbdemoEntities db = new dbdemoEntities();
            tProduct prod = db.tProduct.FirstOrDefault(p => p.fId == id);
            if (prod != null)
            {
               return View(prod);
            }
            return RedirectToAction("List");
        }
        [HttpPost]
        public ActionResult Edit(tProduct inProd)
        {
            dbdemoEntities db = new dbdemoEntities();
            tProduct prod = db.tProduct.FirstOrDefault(p => p.fId == inProd.fId);
            if (prod != null)
            {
                prod.fName = inProd.fName;
                prod.fCost = inProd.fCost;
                prod.dQty = inProd.dQty;
                prod.dPrice = inProd.dPrice;
                db.SaveChanges();
            }
            return RedirectToAction("List");
        }

    }
}