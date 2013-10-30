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
    public class DataTableController : Controller
    {
        private ContosoUniversityEntities db = new ContosoUniversityEntities();

        // GET: /DataTable/
        public ActionResult Index()
        {
            var datatables = db.DataTables.Include(d => d.AREA).Include(d => d.TypeName);
            return View(datatables.ToList());
        }

        // GET: /DataTable/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DataTable datatable = db.DataTables.Find(id);
            if (datatable == null)
            {
                return HttpNotFound();
            }
            return View(datatable);
        }

        // GET: /DataTable/Create
        public ActionResult Create()
        {
            ViewBag.areaid = new SelectList(db.AREAs, "AreaID", "areaname");
            ViewBag.industrytype = new SelectList(db.TypeNames, "TypeNameID", "typename1");
            return View();
        }

        // POST: /DataTable/Create
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="DataTableID,industrytype,tatol,years,areaid,bp")] DataTable datatable)
        {
            if (ModelState.IsValid)
            {
                db.DataTables.Add(datatable);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.areaid = new SelectList(db.AREAs, "AreaID", "areaname", datatable.areaid);
            ViewBag.industrytype = new SelectList(db.TypeNames, "TypeNameID", "typename1", datatable.industrytype);
            return View(datatable);
        }

        // GET: /DataTable/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DataTable datatable = db.DataTables.Find(id);
            if (datatable == null)
            {
                return HttpNotFound();
            }
            ViewBag.areaid = new SelectList(db.AREAs, "AreaID", "areaname", datatable.areaid);
            ViewBag.industrytype = new SelectList(db.TypeNames, "TypeNameID", "typename1", datatable.industrytype);
            return View(datatable);
        }

        // POST: /DataTable/Edit/5
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="DataTableID,industrytype,tatol,years,areaid,bp")] DataTable datatable)
        {
            if (ModelState.IsValid)
            {
                db.Entry(datatable).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.areaid = new SelectList(db.AREAs, "AreaID", "areaname", datatable.areaid);
            ViewBag.industrytype = new SelectList(db.TypeNames, "TypeNameID", "typename1", datatable.industrytype);
            return View(datatable);
        }

        // GET: /DataTable/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DataTable datatable = db.DataTables.Find(id);
            if (datatable == null)
            {
                return HttpNotFound();
            }
            return View(datatable);
        }

        // POST: /DataTable/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            DataTable datatable = db.DataTables.Find(id);
            db.DataTables.Remove(datatable);
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
