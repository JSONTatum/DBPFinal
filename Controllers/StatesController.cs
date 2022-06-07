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
        // UPSERT: States
        [HttpGet]
        public ActionResult Upsert(string id)
        {
            BookEntities context = new BookEntities();
            State state = context.States.Where(c => c.StateCode == id).FirstOrDefault();
            return View(state);
        }
        [HttpPost]
        public ActionResult Upsert(State state)
        {
            BookEntities context = new BookEntities();

            try
            {
                if (context.States.Where(c => c.StateCode == state.StateCode).Count() > 0)
                {
                    State old = context.States.Where(c => c.StateCode == state.StateCode).FirstOrDefault();
                    old.StateCode = state.StateCode;
                    old.StateName = state.StateName;
                }
                else
                {
                    context.States.Add(state);
                }
                context.SaveChanges();
            }
            catch (Exception ex)
            {

            }
            return RedirectToAction("All");
        }
        // DELETE: States
        [HttpGet]
        public ActionResult Delete(string id)
        {
            BookEntities context = new BookEntities();
            State state = context.States.Where(c => c.StateCode == id).FirstOrDefault();
            return View(state);
        }
        [HttpDelete]
        public ActionResult Delete(State state)
        {
            BookEntities context = new BookEntities();
            try
            {
                if (context.States.Where(c => c.StateCode== state.StateCode).Count() > 0)
                {
                    State old = context.States.Where(c => c.StateCode== state.StateCode).FirstOrDefault();
                    context.States.Remove(old);
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