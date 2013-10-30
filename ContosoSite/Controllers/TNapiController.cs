using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using ContosoSite.Models;

namespace ContosoSite.Controllers
{
    public class TNapiController : ApiController
    {
        private ContosoUniversityEntities db = new ContosoUniversityEntities() ;

        // GET api/TNapi
        public IQueryable<TypeName> GetTypeNames()
        {
            return db.TypeNames;
        }


        //selfp selfp/getnamebyparent/parentid

        public IQueryable GetNameByParent(int id)
        { 
            var query = from s in db.TypeNames
                        join d in db.DTs on s.TypeNameID equals d.industrytype
                        join a in db.AREAs on d.areaid  equals a.AreaID
                        where s.parentid == id && a.areaname != "总 计"
                        select new { s.typename1, d.tatol,d.years,a.areaname ,a.x_coord,a.y_coord};
            return (IQueryable) query;
           // return db.TypeNames.Include(d => d.DTs).Where(s => s.parentid == id);
        }

        // GET api/TNapi/5
        [ResponseType(typeof(TypeName))]
        public IHttpActionResult GetTypeName(int id)
        {
            TypeName typename = db.TypeNames.Find(id);
            if (typename == null)
            {
                return NotFound();
            }

            return Ok(typename);
        }

        // PUT api/TNapi/5
        public IHttpActionResult PutTypeName(int id, TypeName typename)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != typename.TypeNameID)
            {
                return BadRequest();
            }

            db.Entry(typename).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TypeNameExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST api/TNapi
        [ResponseType(typeof(TypeName))]
        public IHttpActionResult PostTypeName(TypeName typename)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.TypeNames.Add(typename);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = typename.TypeNameID }, typename);
        }

        // DELETE api/TNapi/5
        [ResponseType(typeof(TypeName))]
        public IHttpActionResult DeleteTypeName(int id)
        {
            TypeName typename = db.TypeNames.Find(id);
            if (typename == null)
            {
                return NotFound();
            }

            db.TypeNames.Remove(typename);
            db.SaveChanges();

            return Ok(typename);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool TypeNameExists(int id)
        {
            return db.TypeNames.Count(e => e.TypeNameID == id) > 0;
        }
    }
}