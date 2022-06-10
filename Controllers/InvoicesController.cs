using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MailletAssignment3.Models;

namespace MailletAssignment3.Controllers
{
    public class InvoicesController : Controller
    {
        /// <summary>
        /// Queries invoices
        /// </summary>
        /// <param name="id">Return invoices that match id</param>
        /// <param name="sortBy">Sort invoices by field</param>
        /// <param name="isDesc">Swich between descending and ascending</param>
        /// <returns>View of invoice list</returns>
        public ActionResult All(string id, int sortBy = 0, bool isDesc = true)
        {
            //open db connection
            BookEntities context = new BookEntities();
            List<Invoice> invList;
            //sort table col
            switch (sortBy)
            {
                case 1:
                    if (isDesc)
                    {
                        invList = context.Invoices.OrderBy(c => c.CustomerID).ToList();
                        break;
                    }
                    else
                    {
                        invList = context.Invoices.OrderByDescending(c => c.CustomerID).ToList();
                        break;
                    }
                case 2:
                    if (isDesc)
                    {
                        invList = context.Invoices.OrderBy(c => c.InvoiceDate).ToList();
                        break;
                    }
                    else
                    {
                        invList = context.Invoices.OrderByDescending(c => c.InvoiceDate).ToList();
                        break;
                    }
                case 3:
                    if (isDesc)
                    {
                        invList = context.Invoices.OrderBy(c => c.ProductTotal).ToList();
                        break;
                    }
                    else
                    {
                        invList = context.Invoices.OrderByDescending(c => c.ProductTotal).ToList();
                        break;
                    }
                case 4:
                    if (isDesc)
                    {
                        invList = context.Invoices.OrderBy(c => c.SalesTax).ToList();
                        break;
                    }
                    else
                    {
                        invList = context.Invoices.OrderByDescending(c => c.SalesTax).ToList();
                        break;
                    }
                case 5:
                    if (isDesc)
                    {
                        invList = context.Invoices.OrderBy(c => c.InvoiceTotal).ToList();
                        break;
                    }
                    else
                    {
                        invList = context.Invoices.OrderByDescending(c => c.InvoiceTotal).ToList();
                        break;
                    }
                case 0:
                default:
                    if (isDesc)
                    {
                        invList = context.Invoices.OrderBy(c => c.InvoiceID).ToList();
                        break;
                    }
                    else
                    {
                        invList = context.Invoices.OrderByDescending(c => c.InvoiceID).ToList();
                        break;
                    }
            }
            //trim and compare search
            if (!string.IsNullOrWhiteSpace(id))
            {
                id = id.Trim().ToLower();
                invList = invList.Where(c =>
                c.CustomerID.ToString().Contains(id) ||
                c.InvoiceDate.ToString().Contains(id) ||
                c.ProductTotal.ToString().Contains(id) ||
                c.SalesTax.ToString().Contains(id) ||
                c.Shipping.ToString().Contains(id) ||
                c.InvoiceTotal.ToString().Contains(id) ||
                c.InvoiceID.ToString().Contains(id)
                ).ToList();
            }
            return View(invList);
        }
        /// <summary>
        /// get invoice to upsert
        /// </summary>
        /// <param name="id">id of invoice to get</param>
        /// <returns>invoice object to upsert</returns>
        [HttpGet]
        public ActionResult Upsert(int id = 0)
        {
            BookEntities context = new BookEntities();
            Invoice invoice = context.Invoices.Where(c => c.InvoiceID == id).FirstOrDefault();
            return View(invoice);
        }
        /// <summary>
        /// upsert an invoice
        /// </summary>
        /// <param name="invoice">invoice to upsert</param>
        /// <returns>view of all invoices after upserting</returns>
        [HttpPost]
        public ActionResult Upsert(Invoice invoice)
        {
            BookEntities context = new BookEntities();

            try
            {
                if (context.Invoices.Where(c => c.InvoiceID == invoice.InvoiceID).Count() > 0)
                {
                    Invoice old = context.Invoices.Where(c => c.InvoiceID == invoice.InvoiceID).FirstOrDefault();
                    old.CustomerID = invoice.CustomerID;
                    old.InvoiceDate = invoice.InvoiceDate;
                    old.ProductTotal = invoice.ProductTotal;
                    old.SalesTax = invoice.SalesTax;
                    old.Shipping = invoice.Shipping;
                    old.InvoiceTotal = invoice.InvoiceTotal;
                }
                else
                {
                    context.Invoices.Add(invoice);
                }
                context.SaveChanges();
            }
            catch (Exception ex)
            {

            }
            return RedirectToAction("All");
        }
        /// <summary>
        /// get invoice to delete
        /// </summary>
        /// <param name="id">id of invoice to delete</param>
        /// <returns>invoice object to delete</returns>
        [HttpGet]
        public ActionResult Delete(int id)
        {
            BookEntities context = new BookEntities();
            Invoice invoice = context.Invoices.Where(c => c.InvoiceID == id).First();
            return Delete(invoice);
        }
        /// <summary>
        /// delete invoice
        /// </summary>
        /// <param name="invoice">invoice object to delete</param>
        /// <returns>all invoice view after deleting</returns>
        [HttpDelete]
        public ActionResult Delete(Invoice invoice)
        {
            BookEntities context = new BookEntities();
            try
            {
                if (context.Invoices.Where(c => c.InvoiceID == invoice.InvoiceID).Count() > 0)
                {
                    Invoice old = context.Invoices.Where(c => c.InvoiceID == invoice.InvoiceID).FirstOrDefault();
                    context.Invoices.Remove(old);
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