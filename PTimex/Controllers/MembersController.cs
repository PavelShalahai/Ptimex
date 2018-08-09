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
    public class MembersController : Controller
    {
        private DbModel db = new DbModel();

        // GET: Members
        public ActionResult Index()
        {
            var member = db.Member.Include(m => m.Position).Include(m => m.Project).Include(m => m.User);
            return View(member.ToList());
        }

        // GET: Members/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Member member = db.Member.Find(id);
            if (member == null)
            {
                return HttpNotFound();
            }
            return View(member);
        }

        // GET: Members/Create
        public ActionResult Create()
        {
            ViewBag.PositionId = new SelectList(db.Position, "Id", "Name");
            ViewBag.ProjectId = new SelectList(db.Project, "Id", "Title");
            ViewBag.UserId = new SelectList(db.User, "Id", "FirstName");
            return View();
        }

        // POST: Members/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,ProjectId,UserId,PositionId,IsDeleted")] Member member)
        {
            if (ModelState.IsValid)
            {
                db.Member.Add(member);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.PositionId = new SelectList(db.Position, "Id", "Name", member.PositionId);
            ViewBag.ProjectId = new SelectList(db.Project, "Id", "Title", member.ProjectId);
            ViewBag.UserId = new SelectList(db.User, "Id", "FirstName", member.UserId);
            return View(member);
        }

        // GET: Members/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Member member = db.Member.Find(id);
            if (member == null)
            {
                return HttpNotFound();
            }
            ViewBag.PositionId = new SelectList(db.Position, "Id", "Name", member.PositionId);
            ViewBag.ProjectId = new SelectList(db.Project, "Id", "Title", member.ProjectId);
            ViewBag.UserId = new SelectList(db.User, "Id", "FirstName", member.UserId);
            return View(member);
        }

        // POST: Members/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,ProjectId,UserId,PositionId,IsDeleted")] Member member)
        {
            if (ModelState.IsValid)
            {
                db.Entry(member).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.PositionId = new SelectList(db.Position, "Id", "Name", member.PositionId);
            ViewBag.ProjectId = new SelectList(db.Project, "Id", "Title", member.ProjectId);
            ViewBag.UserId = new SelectList(db.User, "Id", "FirstName", member.UserId);
            return View(member);
        }

        // GET: Members/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Member member = db.Member.Find(id);
            if (member == null)
            {
                return HttpNotFound();
            }
            return View(member);
        }

        // POST: Members/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Member member = db.Member.Find(id);
            db.Member.Remove(member);
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
