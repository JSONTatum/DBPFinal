using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MailletAssignment3.Models;

namespace MailletAssignment3.Controllers
{
    public class StatesController : Controller
    {
        // GET: States
        public ActionResult All(string id, int sortBy = 0, bool isDesc = true)
        {
            //open db connection
            BookEntities context = new BookEntities();
            List<State> stateList;
            //sort table col
            switch (sortBy)
            {
                case 1:
                    if (isDesc)
                    {
                        stateList = context.States.OrderBy(c => c.StateName).ToList();
                        break;
                    }
                    else
                    {
                        stateList = context.States.OrderByDescending(c => c.StateName).ToList();
                        break;
                    }
                case 0:
                default:
                    if (isDesc)
                    {
                        stateList = context.States.OrderBy(c => c.StateCode).ToList();
                        break;
                    }
                    else
                    {
                        stateList = context.States.OrderByDescending(c => c.StateCode).ToList();
                        break;
                    }
            }
            //trim and compare search
            if (!string.IsNullOrWhiteSpace(id))
            {
                id = id.Trim().ToLower();
                stateList = stateList.Where(c =>
                c.StateName.ToLower().Contains(id) ||
                c.StateCode.ToString().ToLower().Contains(id)
                ).ToList();
            }
            return View(stateList);
        }
    }
}