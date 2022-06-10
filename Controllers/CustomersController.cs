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
        /// <summary>
        /// Queries customers
        /// </summary>
        /// <param name="id">Return customers that match id</param>
        /// <param name="sortBy">Sort customers by field</param>
        /// <param name="isDesc">Swich between descending and ascending</param>
        /// <returns>View of customer list</returns>
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
        /// <summary>
        /// Get individual customer matching id
        /// </summary>
        /// <param name="id">Customer ID</param>
        /// <returns>Customer object</returns>
        [HttpGet]
        public ActionResult Upsert(int id = 0)
        {
            BookEntities context = new BookEntities();
            Customer cus = context.Customers.Where(c => c.CustomerID == id).FirstOrDefault();
            return View(cus);
        }
        /// <summary>
        /// Update customer matching ID or adds if no ID exists
        /// </summary>
        /// <param name="cus">Customer object to upsert</param>
        /// <returns>Redirect to all</returns>
        [HttpPost]
        public ActionResult Upsert(Customer cus)
        {
            BookEntities context = new BookEntities();

            try
            {
                if (context.Customers.Where(c => c.CustomerID == cus.CustomerID).Count() > 0)
                {
                    Customer old = context.Customers.Where(c => c.CustomerID == cus.CustomerID).FirstOrDefault();
                    old.Name = cus.Name;
                    old.Address = cus.Address;
                    old.City = cus.City;
                    old.State = cus.State;
                    old.ZipCode = cus.ZipCode;
                }
                else
                {
                    context.Customers.Add(cus);
                }
                context.SaveChanges();
            }
            catch (Exception ex)
            {

            }
            return RedirectToAction("All");
        }
        /// <summary>
        /// get customer matching Id
        /// </summary>
        /// <param name="id">Customer ID to find</param>
        /// <returns>Returns customer object</returns>
        [HttpGet]
        public ActionResult Delete(int id)
        {
            BookEntities context = new BookEntities();
            Customer cus = context.Customers.Where(c => c.CustomerID == id).FirstOrDefault();
            return Delete(cus);
        }
        /// <summary>
        /// Delete customer matching object
        /// </summary>
        /// <param name="cus">Customer object to delete</param>
        /// <returns>Returns to All view after deleting</returns>
        [HttpDelete]
        public ActionResult Delete(Customer cus)
        {
            BookEntities context = new BookEntities();
            try
            {
                if (context.Customers.Where(c => c.CustomerID == cus.CustomerID).Count() > 0)
                {
                    Customer old = context.Customers.Where(c => c.CustomerID == cus.CustomerID).FirstOrDefault();
                    context.Customers.Remove(old);
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