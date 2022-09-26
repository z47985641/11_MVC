using prjMvcDemo.Models;
using prjMvcDemo.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace prjMvcDemo.Controllers
{
    public class ShoppingCartController : Controller
    {
        // GET: ShoppingCart
        public ActionResult List()
        {
            dbdemoEntities db = new dbdemoEntities();

            var datas = from p in db.tProduct
                        select p;
            return View(datas);
        }
        public ActionResult AddToCart(int? Id)
        {
            dbdemoEntities db = new dbdemoEntities();
            tProduct prod = db.tProduct.FirstOrDefault(p => p.fId == Id);
            if (prod != null)
            {
                return View(prod);
            }
            return RedirectToAction("List");
        }
        [HttpPost]
        public ActionResult AddToCart(CAddToCartModel p)
        {
            dbdemoEntities db = new dbdemoEntities();
            tProduct prod = db.tProduct.FirstOrDefault(t => t.fId ==p.txtId);
            if (prod != null)
            {
                tShoppingCart item = new tShoppingCart();
                item.fCount = p.txtcount;
                item.fCustomer = 1;
                item.fDate = DateTime.Now.ToString("yyyyy/mm/dd");
                item.fPrice = prod.dPrice;
                item.fProduct = prod.fId;
                db.tShoppingCart.Add(item);
                db.SaveChanges();
            }
            return RedirectToAction("List");
        }
        //__________________________________
        public ActionResult AddToSeestion(int? Id)
        {
            dbdemoEntities db = new dbdemoEntities();
            tProduct prod = db.tProduct.FirstOrDefault(p => p.fId == Id);
            if (prod != null)
            {
                return View(prod);
            }
            return RedirectToAction("List");
        }
        [HttpPost]
        public ActionResult AddToSeestion(CAddToCartModel p)
        {
            dbdemoEntities db = new dbdemoEntities();
            tProduct prod = db.tProduct.FirstOrDefault(t => t.fId == p.txtId);
            if (prod != null)
            {
                List<CshoppingCart> cart = Session["KK"] as List<CshoppingCart>;
                if (cart == null)
                {
                    cart = new List<CshoppingCart>();
                    Session["KK"] = cart;
                }
                CshoppingCart item = new CshoppingCart()
                {
                    count = p.txtcount,
                    price = (decimal)prod.dPrice,
                    productId = prod.fId
                };
                cart.Add(item);
                

            }
            return RedirectToAction("List");
        }
        public ActionResult CartView()
        { 
           List<CshoppingCart> cart = Session["KK"] as List<CshoppingCart>;
            if (cart == null)
                return RedirectToAction("List");
            return View(cart);
        }

        }
}