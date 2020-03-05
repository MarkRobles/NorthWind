using NorthWind.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NorthWind.Controllers
{
    public class ProductController : Controller
    {

        public ActionResult HacerAlgoSinRespuesta()
        {
            return new EmptyResult();
        }
        public ActionResult NoAutorizado()
        {
            return new HttpUnauthorizedResult("AccesoDenegado");
        }
        public ActionResult Create()
        {
            Product NewProduct = new Product();

            var Context = new NORTHWNDEntities();
            var Categories = Context.Categories.Select(c => new { c.CategoryID, c.CategoryName});

            ViewBag.CategoryID = new SelectList(Categories,"CategoryID","CategoryName");

            return View(NewProduct);
        }

        [HttpPost]
        public ActionResult Create(Product newProduct)
        {
            ActionResult Result;
            if (ModelState.IsValid)
            {
                var Context = new NORTHWNDEntities();
                Context.Products.Add(newProduct);
                Context.SaveChanges();
                Result =
                    RedirectToAction("Details", new { id = newProduct.ProductID });
            }
            else
            {
                Result = View(newProduct);
            }
            return Result;
        }


        // GET: Product
        public ActionResult Index()
        {
            var Context = new NORTHWNDEntities();

            return View(Context.Products.ToList());
        }

        public ActionResult Details(int id)
        {
            ActionResult Result;
            var Context = new NORTHWNDEntities();
            var Product = Context.Products.FirstOrDefault(p => p.ProductID == id);

            if(Product!=null)
            {
                Result = View(Product);
            }
            else
            {
                Result = HttpNotFound("Producto no encontrado");

            }
            return Result;
        }


     
    }
}