using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MailletAssignment3.Models;

namespace MailletAssignment3.Controllers
{
    public class OrderOptionsController : Controller
    {
        /// <summary>
        /// Queries order options
        /// </summary>
        /// <param name="id">Return order options that match search id</param>
        /// <param name="sortBy">Sort order options by field</param>
        /// <param name="isDesc">Swich between descending and ascending</param>
        /// <returns>View of order options list</returns>
        public ActionResult All(string id, int sortBy = 0, bool isDesc = true)
        {
            //open db connection
            BookEntities context = new BookEntities();
            List<OrderOption> ooList;
            //sort table col
            switch (sortBy)
            {
                case 1:
                    if (isDesc)
                    {
                        ooList = context.OrderOptions.OrderBy(c => c.FirstBookShipCharge).ToList();
                        break;
                    }
                    else
                    {
                        ooList = context.OrderOptions.OrderByDescending(c => c.FirstBookShipCharge).ToList();
                        break;
                    }
                case 2:
                    if (isDesc)
                    {
                        ooList = context.OrderOptions.OrderBy(c => c.AdditionalBookShipCharge).ToList();
                        break;
                    }
                    else
                    {
                        ooList = context.OrderOptions.OrderByDescending(c => c.AdditionalBookShipCharge).ToList();
                        break;
                    }
                case 0:
                default:
                    if (isDesc)
                    {
                        ooList = context.OrderOptions.OrderBy(c => c.SalesTaxRate).ToList();
                        break;
                    }
                    else
                    {
                        ooList = context.OrderOptions.OrderByDescending(c => c.SalesTaxRate).ToList();
                        break;
                    }
            }
            //trim and compare search
            if (!string.IsNullOrWhiteSpace(id))
            {
                id = id.Trim().ToLower();
                ooList = ooList.Where(c =>
                c.FirstBookShipCharge.ToString().Contains(id) ||
                c.AdditionalBookShipCharge.ToString().Contains(id) ||
                c.SalesTaxRate.ToString().Contains(id)
                ).ToList();
            }
            return View(ooList);
        }
        /// <summary>
        /// get orderoption to upsert
        /// </summary>
        /// <param name="id">orderoption to get</param>
        /// <returns>order option object</returns>
        [HttpGet]
        public ActionResult Upsert(int id = 0)
        {
            BookEntities context = new BookEntities();
            OrderOption oo = context.OrderOptions.First();
            return View(oo);
        }
        /// <summary>
        /// upsert orderoption
        /// </summary>
        /// <param name="oo">orderoption object</param>
        /// <returns>all orderoption view after upsert</returns>
        [HttpPost]
        public ActionResult Upsert(OrderOption oo)
        {
            BookEntities context = new BookEntities();

            try
            {
                if (context.OrderOptions.Count() > 0)
                {
                    OrderOption old = context.OrderOptions.First();
                    old.SalesTaxRate = oo.SalesTaxRate;
                    old.SalesTaxRate = oo.FirstBookShipCharge;
                    old.AdditionalBookShipCharge = oo.AdditionalBookShipCharge;
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