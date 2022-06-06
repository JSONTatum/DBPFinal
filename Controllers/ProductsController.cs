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
    }
}