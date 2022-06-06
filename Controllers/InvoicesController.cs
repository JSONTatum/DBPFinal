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
        // GET: Invoices
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
    }
}