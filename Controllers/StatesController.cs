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
        /// <summary>
        /// Queries states
        /// </summary>
        /// <param name="id">Return states that match id</param>
        /// <param name="sortBy">Sort states by field</param>
        /// <param name="isDesc">Swich between descending and ascending</param>
        /// <returns>View of states list</returns>
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
        /// <summary>
        /// get state to upsert by id
        /// </summary>
        /// <param name="id">id of state to upsert</param>
        /// <returns>state object to upsert</returns>
        [HttpGet]
        public ActionResult Upsert(string id)
        {
            BookEntities context = new BookEntities();
            State state = context.States.Where(c => c.StateCode == id).FirstOrDefault();
            return View(state);
        }
        /// <summary>
        /// upsert state
        /// </summary>
        /// <param name="state">state object to upsert</param>
        /// <returns>all state view after deleting</returns>
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
        /// <summary>
        /// get state to delete by id
        /// </summary>
        /// <param name="id">id of state to delete</param>
        /// <returns>state object to delete</returns>
        [HttpGet]
        public ActionResult Delete(string id)
        {
            BookEntities context = new BookEntities();
            State state = context.States.Where(c => c.StateCode == id).FirstOrDefault();
            return Delete(state);
        }
        /// <summary>
        /// delete state object
        /// </summary>
        /// <param name="state">state object to delete</param>
        /// <returns>all state view after deleting</returns>
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