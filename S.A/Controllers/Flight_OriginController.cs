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
    public class Flight_OriginController : Controller
    {
        private StarAllianceEntities1 db = new StarAllianceEntities1();

        // GET: Flight_Origin
        public ActionResult Index()
        {
            var flight_Origin = db.Flight_Origin.Include(f => f.Passenger);
            return View(flight_Origin.ToList());
        }

        // GET: Flight_Origin/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Flight_Origin flight_Origin = db.Flight_Origin.Find(id);
            if (flight_Origin == null)
            {
                return HttpNotFound();
            }
            return View(flight_Origin);
        }

        // GET: Flight_Origin/Create
        public ActionResult InsertFlightOrigin()
        {
            ViewBag.ID_Passenger = new SelectList(db.Passenger, "ID_Passenger", "Fst_Nombre");
            return View();
        }

        // POST: Flight_Origin/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult InsertFlightOrigin(int ID_Passenger, string Airport, string Country, string City, string ZipCode, string Airline, DateTime DepartureDate, DateTime  Departure_Time)
        {
            using (SqlConnection connection = new SqlConnection("Data Source=localhost;Initial Catalog=StarAlliance;Integrated Security=true"))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand("InsertFlightOrigin", connection))
                {
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@ID_Passenger", ID_Passenger);
                    command.Parameters.AddWithValue("@Airport", Airport);
                    command.Parameters.AddWithValue("@Country", Country);
                    command.Parameters.AddWithValue("@City", City);
                    command.Parameters.AddWithValue("@ZipCode", ZipCode);
                    command.Parameters.AddWithValue("@Airline", Airline);
                    command.Parameters.AddWithValue("@DepartureDate", DepartureDate);
                    command.Parameters.AddWithValue("@Departure_Time", Departure_Time);
                    command.ExecuteNonQuery();
                }

                TempData["Mensaje"] = "Pasajero agregado exitosamente.";

                return RedirectToAction("Index");
            }
        }


        // GET: Flight_Origin/Edit/5
        public ActionResult UpdateFlightOrigin(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Flight_Origin flight_Origin = db.Flight_Origin.Find(id);
            if (flight_Origin == null)
            {
                return HttpNotFound();
            }
            ViewBag.ID_Passenger = new SelectList(db.Passenger, "ID_Passenger", "Fst_Nombre", flight_Origin.ID_Passenger);
            return View(flight_Origin);
        }

        // POST: Flight_Origin/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UpdateFlightOrigin(int idOrigin, string airport, string airline)
        {
            using (SqlConnection connection = new SqlConnection("Data Source=localhost;Initial Catalog=StarAlliance;Integrated Security=true"))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand("UpdateFlightOrigin", connection))
                {
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@ID_Origin", idOrigin);
                    command.Parameters.AddWithValue("@Airport", airport);
                    command.Parameters.AddWithValue("@Airline", airline);
                    command.ExecuteNonQuery();
                }

                TempData["Mensaje"] = "Información actualizada correctamente.";

                return RedirectToAction("Index");

            }
        }

            
        

        // GET: Flight_Origin/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Flight_Origin flight_Origin = db.Flight_Origin.Find(id);
            if (flight_Origin == null)
            {
                return HttpNotFound();
            }
            return View(flight_Origin);
        }

        // POST: Flight_Origin/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Flight_Origin flight_Origin = db.Flight_Origin.Find(id);
            db.Flight_Origin.Remove(flight_Origin);
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
