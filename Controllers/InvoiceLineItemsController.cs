using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MailletAssignment3.Models;

namespace MailletAssignment3.Controllers
{
    public class InvoiceLineItemsController : Controller
    {
        // GET: InvoiceLineItems
        public ActionResult All(string id, int sortBy = 0, bool isDesc = true)
        {
            //open db connection
            BookEntities context = new BookEntities();
            List<InvoiceLineItem> iltList;
            //sort table col
            switch (sortBy)
            {
                case 1:
                    if (isDesc)
                    {
                        iltList = context.InvoiceLineItems.OrderBy(c => c.ProductCode).ToList();
                        break;
                    }
                    else
                    {
                        iltList = context.InvoiceLineItems.OrderByDescending(c => c.ProductCode).ToList();
                        break;
                    }
                case 2:
                    if (isDesc)
                    {
                        iltList = context.InvoiceLineItems.OrderBy(c => c.UnitPrice).ToList();
                        break;
                    }
                    else
                    {
                        iltList = context.InvoiceLineItems.OrderByDescending(c => c.UnitPrice).ToList();
                        break;
                    }
                case 3:
                    if (isDesc)
                    {
                        iltList = context.InvoiceLineItems.OrderBy(c => c.Quantity).ToList();
                        break;
                    }
                    else
                    {
                        iltList = context.InvoiceLineItems.OrderByDescending(c => c.Quantity).ToList();
                        break;
                    }
                case 4:
                    if (isDesc)
                    {
                        iltList = context.InvoiceLineItems.OrderBy(c => c.ItemTotal).ToList();
                        break;
                    }
                    else
                    {
                        iltList = context.InvoiceLineItems.OrderByDescending(c => c.ItemTotal).ToList();
                        break;
                    }
                case 0:
                default:
                    if (isDesc)
                    {
                        iltList = context.InvoiceLineItems.OrderBy(c => c.InvoiceID).ToList();
                        break;
                    }
                    else
                    {
                        iltList = context.InvoiceLineItems.OrderByDescending(c => c.InvoiceID).ToList();
                        break;
                    }
            }
            //trim and compare search
            if (!string.IsNullOrWhiteSpace(id))
            {
                id = id.Trim().ToLower();
                iltList = iltList.Where(c =>
                c.ProductCode.ToLower().Contains(id) ||
                c.UnitPrice.ToString().Contains(id) ||
                c.Quantity.ToString() == id ||
                c.ItemTotal.ToString().Contains(id) ||
                c.InvoiceID.ToString().Contains(id)
                ).ToList();
            }
            return View(iltList);
        }
        // UPSERT: InvoiceLineItems
        [HttpGet]
        public ActionResult Upsert(string id)
        {
            BookEntities context = new BookEntities();
            InvoiceLineItem ilt;
            string productCode = null;
            int invoiceID = 0;
            if (id != null)
            {
                productCode = id.Substring(0, 4);
                invoiceID = int.Parse(id.Substring(4));
                ilt = context.InvoiceLineItems.Where(c => c.InvoiceID == invoiceID && c.ProductCode == productCode).FirstOrDefault();
            }
            else
            {
                ilt = context.InvoiceLineItems.Create();
            }
            return View(ilt);
        }
        [HttpPost]
        public ActionResult Upsert(InvoiceLineItem ilt)
        {
            BookEntities context = new BookEntities();

            try
            {
                if (context.InvoiceLineItems.Where(c => c.InvoiceID == ilt.InvoiceID && c.ProductCode == ilt.ProductCode).Count() > 0)
                {
                    InvoiceLineItem old = context.InvoiceLineItems.Where(c => c.InvoiceID == ilt.InvoiceID && c.ProductCode == ilt.ProductCode).FirstOrDefault();
                    old.UnitPrice = ilt.UnitPrice;
                    old.Quantity = ilt.Quantity;
                    old.ItemTotal = ilt.ItemTotal;
                }
                else
                {
                    context.InvoiceLineItems.Add(ilt);
                }
                context.SaveChanges();
            }
            catch (Exception ex)
            {

            }
            return RedirectToAction("All");
        }
        // DELETE: InvoiceLineItems
        [HttpGet]
        public ActionResult Delete(string id)
        {
            string productCode = id.Substring(0,4);
            int invoiceID = int.Parse(id.Substring(4));
            BookEntities context = new BookEntities();
            InvoiceLineItem ilt = context.InvoiceLineItems.Where(c => c.ProductCode == productCode && c.InvoiceID == invoiceID).FirstOrDefault();
            return View(ilt);
        }
        [HttpDelete]
        public ActionResult Delete(InvoiceLineItem ilt)
        {
            BookEntities context = new BookEntities();
            try
            {
                if (context.InvoiceLineItems.Where(c => c.ProductCode == ilt.ProductCode && c.InvoiceID == ilt.InvoiceID).Count() > 0)
                {
                    InvoiceLineItem old = context.InvoiceLineItems.Where(c => c.ProductCode == ilt.ProductCode && c.InvoiceID == ilt.InvoiceID).FirstOrDefault();
                    context.InvoiceLineItems.Remove(old);
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