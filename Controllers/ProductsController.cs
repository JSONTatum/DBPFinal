using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MailletAssignment3.Models;

namespace MailletAssignment3.Controllers
{
    public class ProductsController : Controller
    {
        // GET: Products
        public ActionResult All(string id, int sortBy = 0, bool isDesc = true)
        {
            //open db connection
            BookEntities context = new BookEntities();
            List<Product> prodList;
            //sort table col
            switch (sortBy)
            {
                case 1:
                    if (isDesc)
                    {
                        prodList = context.Products.OrderBy(c => c.Description).ToList();
                        break;
                    }
                    else
                    {
                        prodList = context.Products.OrderByDescending(c => c.Description).ToList();
                        break;
                    }
                case 2:
                    if (isDesc)
                    {
                        prodList = context.Products.OrderBy(c => c.UnitPrice).ToList();
                        break;
                    }
                    else
                    {
                        prodList = context.Products.OrderByDescending(c => c.UnitPrice).ToList();
                        break;
                    }
                case 3:
                    if (isDesc)
                    {
                        prodList = context.Products.OrderBy(c => c.OnHandQuantity).ToList();
                        break;
                    }
                    else
                    {
                        prodList = context.Products.OrderByDescending(c => c.OnHandQuantity).ToList();
                        break;
                    }
                case 0:
                default:
                    if (isDesc)
                    {
                        prodList = context.Products.OrderBy(c => c.ProductCode).ToList();
                        break;
                    }
                    else
                    {
                        prodList = context.Products.OrderByDescending(c => c.ProductCode).ToList();
                        break;
                    }
            }
            //trim and compare search
            if (!string.IsNullOrWhiteSpace(id))
            {
                id = id.Trim().ToLower();
                prodList = prodList.Where(c =>
                c.Description.ToLower().Contains(id) ||
                c.UnitPrice.ToString().Contains(id) ||
                c.OnHandQuantity.ToString().Contains(id) ||
                c.ProductCode.ToString().ToLower().Contains(id)
                ).ToList();
            }
            return View(prodList);
        }
        // UPSERT: Products
        [HttpGet]
        public ActionResult Upsert(string id)
        {
            BookEntities context = new BookEntities();
            Product prod = context.Products.Where(c => c.ProductCode == id).FirstOrDefault();
            return View(prod);
        }
        [HttpPost]
        public ActionResult Upsert(Product prod)
        {
            BookEntities context = new BookEntities();

            try
            {
                if (context.Products.Where(c => c.ProductCode == prod.ProductCode).Count() > 0)
                {
                    Product old = context.Products.Where(c => c.ProductCode == prod.ProductCode).FirstOrDefault();
                    old.Description = prod.Description;
                    old.UnitPrice = prod.UnitPrice;
                    old.OnHandQuantity = prod.OnHandQuantity;
                }
                else
                {
                    context.Products.Add(prod);
                }
                context.SaveChanges();
            }
            catch (Exception ex)
            {

            }
            return RedirectToAction("All");
        }
        // DELETE: Products
        [HttpGet]
        public ActionResult Delete(string id)
        {
            BookEntities context = new BookEntities();
            Product product = context.Products.Where(c => c.ProductCode== id).FirstOrDefault();
            return View(product);
        }
        [HttpDelete]
        public ActionResult Delete(Product product)
        {
            BookEntities context = new BookEntities();
            try
            {
                if (context.Products.Where(c => c.ProductCode == product.ProductCode).Count() > 0)
                {
                    Product old = context.Products.Where(c => c.ProductCode == product.ProductCode).FirstOrDefault();
                    context.Products.Remove(old);
                }
                context.SaveChanges();
            }
            catch (Exception ex)
            {

            }
            return RedirectToAction("All");
        }
    }
}