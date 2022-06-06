using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MailletAssignment3.Models;

namespace MailletAssignment3.Controllers
{
    public class CustomersController : Controller
    {
        public ActionResult All(string id, int sortBy = 0, bool isDesc = true)
        {
            //open db connection
            BookEntities context = new BookEntities();
            List<Customer> cusList;
            //sort table col
            switch (sortBy)
            {
                case 1:
                    if (isDesc)
                    {
                        cusList = context.Customers.OrderBy(c => c.Name).ToList();
                        break;
                    }
                    else
                    {
                        cusList = context.Customers.OrderByDescending(c => c.Name).ToList();
                        break;
                    }
                case 2:
                    if (isDesc)
                    {
                        cusList = context.Customers.OrderBy(c => c.Address).ToList();
                        break;
                    }
                    else
                    {
                        cusList = context.Customers.OrderByDescending(c => c.Address).ToList();
                        break;
                    }
                case 3:
                    if (isDesc)
                    {
                        cusList = context.Customers.OrderBy(c => c.City).ToList();
                        break;
                    }
                    else
                    {
                        cusList = context.Customers.OrderByDescending(c => c.City).ToList();
                        break;
                    }
                case 4:
                    if (isDesc)
                    {
                        cusList = context.Customers.OrderBy(c => c.ZipCode).ToList();
                        break;
                    }
                    else
                    {
                        cusList = context.Customers.OrderByDescending(c => c.ZipCode).ToList();
                        break;
                    }
                case 5:
                    if (isDesc)
                    {
                        cusList = context.Customers.OrderBy(c => c.State).ToList();
                        break;
                    }
                    else
                    {
                        cusList = context.Customers.OrderByDescending(c => c.State).ToList();
                        break;
                    }
                case 0:
                default:
                    if (isDesc)
                    {
                        cusList = context.Customers.OrderBy(c => c.CustomerID).ToList();
                        break;
                    }
                    else
                    {
                        cusList = context.Customers.OrderByDescending(c => c.CustomerID).ToList();
                        break;
                    }
            }
            //trim and compare search
            if (!string.IsNullOrWhiteSpace(id))
            {
                id = id.Trim().ToLower();
                cusList = cusList.Where(c =>
                c.Name.ToLower().Contains(id) ||
                c.Address.ToLower().Contains(id) ||
                c.City.ToLower().Contains(id) ||
                c.State.ToLower().Contains(id) ||
                c.ZipCode.ToLower().Contains(id) ||
                c.CustomerID.ToString().Contains(id)
                ).ToList();
            }
            return View(cusList);
        }
    }
}