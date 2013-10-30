using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ContosoSite.Models;

namespace ContosoSite.Controllers
{
    public class DTController : Controller
    {
        private ContosoUniversityEntities db = new ContosoUniversityEntities();

        // GET: /DT/
        public ActionResult Index()
        {
            var dts = db.DTs.Include(d => d.AREA).Include(d => d.TypeName);
            return View(dts.ToList());
        }

        // GET: /DT/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DT dt = db.DTs.Find(id);
            if (dt == null)
            {
                return HttpNotFound();
            }
            return View(dt);
        }

        // GET: /DT/Create
        public ActionResult Create()
        {
            ViewBag.areaid = new SelectList(db.AREAs, "AreaID", "areaname");
            ViewBag.industrytype = new SelectList(db.TypeNames, "TypeNameID", "typename1");
            return View();
        }

        // POST: /DT/Create
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="DTID,industrytype,tatol,years,areaid,bp")] DT dt)
        {
            if (ModelState.IsValid)
            {
                db.DTs.Add(dt);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.areaid = new SelectList(db.AREAs, "AreaID", "areaname", dt.areaid);
            ViewBag.industrytype = new SelectList(db.TypeNames, "TypeNameID", "typename1", dt.industrytype);
            return View(dt);
        }

        // GET: /DT/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DT dt = db.DTs.Find(id);
            if (dt == null)
            {
                return HttpNotFound();
            }
            ViewBag.areaid = new SelectList(db.AREAs, "AreaID", "areaname", dt.areaid);
            ViewBag.industrytype = new SelectList(db.TypeNames, "TypeNameID", "typename1", dt.industrytype);
            return View(dt);
        }

        // POST: /DT/Edit/5
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="DTID,industrytype,tatol,years,areaid,bp")] DT dt)
        {
            if (ModelState.IsValid)
            {
                db.Entry(dt).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.areaid = new SelectList(db.AREAs, "AreaID", "areaname", dt.areaid);
            ViewBag.industrytype = new SelectList(db.TypeNames, "TypeNameID", "typename1", dt.industrytype);
            return View(dt);
        }

        // GET: /DT/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DT dt = db.DTs.Find(id);
            if (dt == null)
            {
                return HttpNotFound();
            }
            return View(dt);
        }

        // POST: /DT/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            DT dt = db.DTs.Find(id);
            db.DTs.Remove(dt);
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
