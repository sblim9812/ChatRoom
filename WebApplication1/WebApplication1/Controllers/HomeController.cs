using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using WebApplication1.Models;
using WebApplication1.ViewModels;

namespace WebApplication1.Controllers
{
    public class HomeController : Controller
    {
        ApplicationDbContext dbContext = new ApplicationDbContext();

        public ActionResult Index(string currentSortOption, string searchString, string currentFilter, int? page)
        {
            ViewBag.CurrentSort = currentSortOption;
            ViewBag.NameSortParm = String.IsNullOrEmpty(currentSortOption) ? "name_desc" : "";
            ViewBag.DateSortparm = currentSortOption == "Price" ? "price_acs" : "name_acs";

            if (searchString != null)
            { page = 1; }
            else
            { searchString = currentFilter; }

            ViewBag.CurrentFilter = searchString;
            var products = from w in dbContext.Products
                        select w;

            if (!String.IsNullOrEmpty(searchString))
            {
                products = products.Where(s => s.Name.Contains(searchString));
            }

            switch (currentSortOption)
            {
                case "name_acs":
                    products = products.OrderBy(w => w.Name);
                    break;
                case "name_desc":
                    products = products.OrderByDescending(w => w.Name);
                    break;
                case "price_acs":
                    products = products.OrderBy(w => w.Price);
                    break;
                case "price_desc":
                    products = products.OrderByDescending(w => w.Price);
                    break;
                case "qty_acs":
                    products = products.OrderBy(w => w.Qty);
                    break;
                case "qty_desc":
                    products = products.OrderByDescending(w => w.Qty);
                    break;
                default:
                    products = products.OrderBy(w => w.Name);
                    break;
            }

            int pageSize = 10;
            int pageNumber = (page ?? 1);

            return Request.IsAjaxRequest()
                ? (ActionResult)PartialView("ProductList", products.ToPagedList(pageNumber, pageSize))
                : View(products.ToPagedList(pageNumber, pageSize));
        }

        [HttpPost]
        public JsonResult Search(string searchName)
        {   
             var products = from w in dbContext.Products
                        where w.Name.Contains(searchName)
                        select w;
            return Json(products.ToList().Take(10));
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        //public ActionResult Up(int? id)
        //{
        //    Product product = dbContext.Products.Find(id);
        //    var target = from r in dbContext.Products
        //                 where r.Id == product.Id
        //                 select r;
        //    return RedirectToAction("Index");
        //}

        [HttpPost]
        public ActionResult Up(int? id, VotingVM obj)
        {
            Product product = dbContext.Products.Find(id);
            var target = from r in dbContext.Products
                         where r.Id == product.Id
                         select r;
            product.Up += 1;
            obj.Up = 1;
            obj.Down = 0;
            dbContext.Entry(product).State = EntityState.Modified;
            dbContext.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Down(int? id, VotingVM obj)
        {
            Product product = dbContext.Products.Find(id);
            var target = from r in dbContext.Products
                         where r.Id == product.Id
                         select r;
            product.Down += 1;
            obj.Down = 1;
            obj.Up = 0;
            dbContext.Entry(target).State = EntityState.Modified;
            dbContext.SaveChanges();
            return RedirectToAction("Index");
        }

    }
}