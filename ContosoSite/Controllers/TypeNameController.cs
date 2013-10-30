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
    public class TypeNameController : Controller
    {
        private ContosoUniversityEntities db = new ContosoUniversityEntities();

        // GET: /TypeName/
        public ActionResult Index()
        {
            return View(db.TypeNames.ToList());
        }

        // GET: /TypeName/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TypeName typename = db.TypeNames.Find(id);
            if (typename == null)
            {
                return HttpNotFound();
            }
            return View(typename);
        }


       
        // GET: /TypeName/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /TypeName/Create
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="TypeNameID,typename1,parentid")] TypeName typename)
        {
            if (ModelState.IsValid)
            {
                db.TypeNames.Add(typename);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(typename);
        }

        // GET: /TypeName/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TypeName typename = db.TypeNames.Find(id);
            if (typename == null)
            {
                return HttpNotFound();
            }
            return View(typename);
        }

        // POST: /TypeName/Edit/5
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="TypeNameID,typename1,parentid")] TypeName typename)
        {
            if (ModelState.IsValid)
            {
                db.Entry(typename).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(typename);
        }

        // GET: /TypeName/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TypeName typename = db.TypeNames.Find(id);
            if (typename == null)
            {
                return HttpNotFound();
            }
            return View(typename);
        }

        // POST: /TypeName/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TypeName typename = db.TypeNames.Find(id);
            db.TypeNames.Remove(typename);
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
