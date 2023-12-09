using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using S.A.Models;

namespace S.A.Controllers
{
    public class Flight_ClassController : Controller
    {
        private StarAllianceEntities1 db = new StarAllianceEntities1();

        // GET: Flight_Class
        public ActionResult Index()
        {
            var flight_Class = db.Flight_Class.Include(f => f.Flight);
            return View(flight_Class.ToList());
        }

        // GET: Flight_Class/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Flight_Class flight_Class = db.Flight_Class.Find(id);
            if (flight_Class == null)
            {
                return HttpNotFound();
            }
            return View(flight_Class);
        }

        // GET: Flight_Class/Create
        public ActionResult InsertarFlightClass()
        {
            ViewBag.ID_Flight = new SelectList(db.Flight, "ID_Flight", "ID_Flight");
            return View();
        }

        // POST: Flight_Class/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult InsertarFlightClass(int ID_Flight, bool Bussines_Class, bool Premium_Class, bool Tourist_Class)
        {
            using (SqlConnection connection = new SqlConnection("Data Source=localhost;Initial Catalog=StarAlliance;Integrated Security=true"))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand("sp_InsertFlightClass", connection))
                {
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@ID_Flight", ID_Flight);
                    command.Parameters.AddWithValue("@Bussines_Class", Bussines_Class);
                    command.Parameters.AddWithValue("@Premium_Class", Premium_Class);
                    command.Parameters.AddWithValue("@Tourist_Class", Tourist_Class);
                    command.ExecuteNonQuery();
                }
            }

            TempData["Mensaje"] = "Clase de vuelo agregada exitosamente.";

            return RedirectToAction("Index");
        }

        // GET: Flight_Class/Edit/5
        public ActionResult ActualizarFlightClass(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Flight_Class flight_Class = db.Flight_Class.Find(id);
            if (flight_Class == null)
            {
                return HttpNotFound();
            }
            ViewBag.ID_Flight = new SelectList(db.Flight, "ID_Flight", "ID_Flight", flight_Class.ID_Flight);
            return View(flight_Class);
        }

        // POST: Flight_Class/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ActualizarFlightClass(int ID_Flight_Class, int ID_Flight, bool Bussines_Class, bool Premium_Class, bool Tourist_Class)
        {
            using (SqlConnection connection = new SqlConnection("Data Source=localhost;Initial Catalog=StarAlliance;Integrated Security=true"))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand("sp_UpdateFlightClass", connection))
                {
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@ID_Flight_Class", ID_Flight_Class);
                    command.Parameters.AddWithValue("@ID_Flight", ID_Flight);
                    command.Parameters.AddWithValue("@Bussines_Class", Bussines_Class);
                    command.Parameters.AddWithValue("@Premium_Class", Premium_Class);
                    command.Parameters.AddWithValue("@Tourist_Class", Tourist_Class);
                    command.ExecuteNonQuery();
                }
            }

            TempData["Mensaje"] = "Información de clase de vuelo actualizada correctamente.";

            return RedirectToAction("Index");
        }


        // GET: Flight_Class/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Flight_Class flight_Class = db.Flight_Class.Find(id);
            if (flight_Class == null)
            {
                return HttpNotFound();
            }
            return View(flight_Class);
        }

        // POST: Flight_Class/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Flight_Class flight_Class = db.Flight_Class.Find(id);
            db.Flight_Class.Remove(flight_Class);
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
