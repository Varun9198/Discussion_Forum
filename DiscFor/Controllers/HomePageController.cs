using DiscFor.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DiscFor.Controllers
{
    public class HomePageController : Controller
    {
        Model1 db;
        // GET: HomePage
        public ActionResult Index()
        {
            db = new Model1();
                ViewBag.Questions = db.Questions.ToList();
                ViewBag.Answers = db.Answers.ToList();
            return View();
        }

        // GET: HomePage/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: HomePage/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: HomePage/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: HomePage/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: HomePage/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: HomePage/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: HomePage/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
