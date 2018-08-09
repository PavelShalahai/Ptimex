using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PTimex;

namespace PTimex.Controllers
{
    public class TaskTimesController : Controller
    {
        private DbModel db = new DbModel();

        // GET: TaskTimes
        public ActionResult Index()
        {
            var taskTime = db.TaskTime.Include(t => t.ActivityType).Include(t => t.Member);
            return View(taskTime.ToList());
        }

        // GET: TaskTimes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TaskTime taskTime = db.TaskTime.Find(id);
            if (taskTime == null)
            {
                return HttpNotFound();
            }
            return View(taskTime);
        }

        // GET: TaskTimes/Create
        public ActionResult Create()
        {
            ViewBag.ActivityId = new SelectList(db.ActivityType, "Id", "Name");
            ViewBag.MemberId = new SelectList(db.Member, "Id", "Id");
            return View();
        }

        // POST: TaskTimes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,ActivityId,MemberId,IsDeleted,Description,TimeSpan")] TaskTime taskTime)
        {
            if (ModelState.IsValid)
            {
                db.TaskTime.Add(taskTime);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ActivityId = new SelectList(db.ActivityType, "Id", "Name", taskTime.ActivityId);
            ViewBag.MemberId = new SelectList(db.Member, "Id", "Id", taskTime.MemberId);
            return View(taskTime);
        }

        // GET: TaskTimes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TaskTime taskTime = db.TaskTime.Find(id);
            if (taskTime == null)
            {
                return HttpNotFound();
            }
            ViewBag.ActivityId = new SelectList(db.ActivityType, "Id", "Name", taskTime.ActivityId);
            ViewBag.MemberId = new SelectList(db.Member, "Id", "Id", taskTime.MemberId);
            return View(taskTime);
        }

        // POST: TaskTimes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,ActivityId,MemberId,IsDeleted,Description,TimeSpan")] TaskTime taskTime)
        {
            if (ModelState.IsValid)
            {
                db.Entry(taskTime).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ActivityId = new SelectList(db.ActivityType, "Id", "Name", taskTime.ActivityId);
            ViewBag.MemberId = new SelectList(db.Member, "Id", "Id", taskTime.MemberId);
            return View(taskTime);
        }

        // GET: TaskTimes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TaskTime taskTime = db.TaskTime.Find(id);
            if (taskTime == null)
            {
                return HttpNotFound();
            }
            return View(taskTime);
        }

        // POST: TaskTimes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TaskTime taskTime = db.TaskTime.Find(id);
            db.TaskTime.Remove(taskTime);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
