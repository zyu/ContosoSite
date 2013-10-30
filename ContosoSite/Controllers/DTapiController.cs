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
    public class DTapiController : ApiController
    {
        private ContosoUniversityEntities db = new ContosoUniversityEntities();

        // GET api/DTapi
        public IQueryable<DT> GetDTs()
        {
           // return db.DTs;
            var query = from a in db.DTs
                        where a.DTID < 6
                        select a;
            return query;
        }

        public IQueryable GetDTinfo()
        {
            var query = from a in db.DTs
                        join t in db.TypeNames on a.industrytype equals t.TypeNameID
                        select new { a, t.typename1 };
            return (IQueryable) query;
        }

        //public TouristAttraction GetTouristAttraction(double longitude, double latitude)
        //{
        //    var location = DbGeography.FromText(
        //        string.Format("POINT ({0} {1})", longitude, latitude));

        //    var query = from a in db.TouristAttractions
        //                orderby a.Location.Distance(location)
        //                select a;

        //    return query.FirstOrDefault();
        //}

        // GET api/DTapi/5
        [ResponseType(typeof(DT))]
        public IHttpActionResult GetDT(int id)
        {
            DT dt = db.DTs.Find(id);
            if (dt == null)
            {
                return NotFound();
            }

            return Ok(dt);
        }

        // PUT api/DTapi/5
        public IHttpActionResult PutDT(int id, DT dt)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != dt.DTID)
            {
                return BadRequest();
            }

            db.Entry(dt).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DTExists(id))
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

        // POST api/DTapi
        [ResponseType(typeof(DT))]
        public IHttpActionResult PostDT(DT dt)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.DTs.Add(dt);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = dt.DTID }, dt);
        }

        // DELETE api/DTapi/5
        [ResponseType(typeof(DT))]
        public IHttpActionResult DeleteDT(int id)
        {
            DT dt = db.DTs.Find(id);
            if (dt == null)
            {
                return NotFound();
            }

            db.DTs.Remove(dt);
            db.SaveChanges();

            return Ok(dt);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool DTExists(int id)
        {
            return db.DTs.Count(e => e.DTID == id) > 0;
        }
    }
}