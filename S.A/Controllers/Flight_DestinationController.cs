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
    public class Flight_DestinationController : Controller
    {
        private StarAllianceEntities1 db = new StarAllianceEntities1();

        // GET: Flight_Destination
        public ActionResult Index()
        {
            var flight_Destination = db.Flight_Destination.Include(f => f.Passenger);
            return View(flight_Destination.ToList());
        }

        // GET: Flight_Destination/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Flight_Destination flight_Destination = db.Flight_Destination.Find(id);
            if (flight_Destination == null)
            {
                return HttpNotFound();
            }
            return View(flight_Destination);
        }

        // GET: Flight_Destination/Create
        public ActionResult InsertarFlightDestination()
        {
            ViewBag.ID_Passenger = new SelectList(db.Passenger, "ID_Passenger", "Fst_Nombre");
            return View();
        }

        // POST: Flight_Destination/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult InsertarFlightDestination(int ID_Passenger, string Airport, string Country, string City, string ZipCode, decimal Destiny_Price, DateTime ArrivalDate, TimeSpan Arrival_Time)
        {
            using (SqlConnection connection = new SqlConnection("Data Source=localhost;Initial Catalog=StarAlliance;Integrated Security=true"))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand("InsertFlightDestination", connection))
                {
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@ID_Passenger", ID_Passenger);
                    command.Parameters.AddWithValue("@Airport", Airport);
                    command.Parameters.AddWithValue("@Country", Country);
                    command.Parameters.AddWithValue("@City", City);
                    command.Parameters.AddWithValue("@ZipCode", ZipCode);
                    command.Parameters.AddWithValue("@Destiny_Price", Destiny_Price);
                    command.Parameters.AddWithValue("@ArrivalDate", ArrivalDate);
                    command.Parameters.AddWithValue("@Arrival_Time", Arrival_Time);
                    command.ExecuteNonQuery();
                }
            }

            TempData["Mensaje"] = "Pasajero agregado exitosamente.";

            return RedirectToAction("Index");
        }

        // GET: Flight_Destination/Edit/5
        public ActionResult ActualizarFlightDestination(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Flight_Destination flight_Destination = db.Flight_Destination.Find(id);
            if (flight_Destination == null)
            {
                return HttpNotFound();
            }
            ViewBag.ID_Passenger = new SelectList(db.Passenger, "ID_Passenger", "Fst_Nombre", flight_Destination.ID_Passenger);
            return View(flight_Destination);
        }

        // POST: Flight_Destination/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ActualizarFlightDestination(int ID_Destination, string Airport, string Country, string City, string ZipCode, decimal Destiny_Price, DateTime ArrivalDate, TimeSpan Arrival_Time)
        {
            using (SqlConnection connection = new SqlConnection("Data Source=localhost;Initial Catalog=StarAlliance;Integrated Security=true"))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand("UpdateFlightDestination", connection))
                {
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@ID_Destination", ID_Destination);
                    command.Parameters.AddWithValue("@Airport", Airport);
                    command.Parameters.AddWithValue("@Country", Country);
                    command.Parameters.AddWithValue("@City", City);
                    command.Parameters.AddWithValue("@ZipCode", ZipCode);
                    command.Parameters.AddWithValue("@Destiny_Price", Destiny_Price);
                    command.Parameters.AddWithValue("@ArrivalDate", ArrivalDate);
                    command.Parameters.AddWithValue("@Arrival_Time", Arrival_Time);
                    command.ExecuteNonQuery();
                }
            }

            TempData["Mensaje"] = "Información actualizada correctamente.";

            return RedirectToAction("Index");
        }


        // GET: Flight_Destination/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Flight_Destination flight_Destination = db.Flight_Destination.Find(id);
            if (flight_Destination == null)
            {
                return HttpNotFound();
            }
            return View(flight_Destination);
        }

        // POST: Flight_Destination/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Flight_Destination flight_Destination = db.Flight_Destination.Find(id);
            db.Flight_Destination.Remove(flight_Destination);
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
