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
    public class Flight_StopsController : Controller
    {
        private StarAllianceEntities1 db = new StarAllianceEntities1();

        // GET: Flight_Stops
        public ActionResult Index()
        {
            var flight_Stops = db.Flight_Stops.Include(f => f.Passenger);
            return View(flight_Stops.ToList());
        }

        // GET: Flight_Stops/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Flight_Stops flight_Stops = db.Flight_Stops.Find(id);
            if (flight_Stops == null)
            {
                return HttpNotFound();
            }
            return View(flight_Stops);
        }

        // GET: Flight_Stops/Create
        public ActionResult InsertarFlightStop()
        {
            ViewBag.ID_Passenger = new SelectList(db.Passenger, "ID_Passenger", "Fst_Nombre");
            return View();
        }

        // POST: Flight_Stops/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult InsertarFlightStop(int ID_Passenger, bool Transhipment, bool Partner_Airline, string Airport, DateTime StopTime, decimal Transhipment_Price, decimal PA_Price)
        {
            using (SqlConnection connection = new SqlConnection("Data Source=localhost;Initial Catalog=StarAlliance;Integrated Security=true"))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand("InsertFlightStop", connection))
                {
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@ID_Passenger", ID_Passenger);
                    command.Parameters.AddWithValue("@Transhipment", Transhipment);
                    command.Parameters.AddWithValue("@Partner_Airline", Partner_Airline);
                    command.Parameters.AddWithValue("@Airport", Airport);
                    command.Parameters.AddWithValue("@StopTime", StopTime);
                    command.Parameters.AddWithValue("@Transhipment_Price", Transhipment_Price);
                    command.Parameters.AddWithValue("@PA_Price", PA_Price);
                    command.ExecuteNonQuery();
                }
            }

            TempData["Mensaje"] = "Pasajero agregado exitosamente.";

            return RedirectToAction("Index");
        }



        // GET: Flight_Stops/Edit/5
        public ActionResult ActualizarFlightStop(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Flight_Stops flight_Stops = db.Flight_Stops.Find(id);
            if (flight_Stops == null)
            {
                return HttpNotFound();
            }
            ViewBag.ID_Passenger = new SelectList(db.Passenger, "ID_Passenger", "Fst_Nombre", flight_Stops.ID_Passenger);
            return View(flight_Stops);
        }

        // POST: Flight_Stops/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ActualizarFlightStop(int ID_FlightStops, bool Transhipment, bool Partner_Airline, string Airport, DateTime StopTime, decimal Transhipment_Price, decimal PA_Price)
        {
            using (SqlConnection connection = new SqlConnection("Data Source=localhost;Initial Catalog=StarAlliance;Integrated Security=true"))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand("UpdateFlightStop", connection))
                {
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@ID_FlightStops", ID_FlightStops);
                    command.Parameters.AddWithValue("@Transhipment", Transhipment);
                    command.Parameters.AddWithValue("@Partner_Airline", Partner_Airline);
                    command.Parameters.AddWithValue("@Airport", Airport);
                    command.Parameters.AddWithValue("@StopTime", StopTime);
                    command.Parameters.AddWithValue("@Transhipment_Price", Transhipment_Price);
                    command.Parameters.AddWithValue("@PA_Price", PA_Price);
                    command.ExecuteNonQuery();
                }
            }

            TempData["Mensaje"] = "Información actualizada correctamente.";

            return RedirectToAction("Index");
        }

                
       

        // GET: Flight_Stops/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Flight_Stops flight_Stops = db.Flight_Stops.Find(id);
            if (flight_Stops == null)
            {
                return HttpNotFound();
            }
            return View(flight_Stops);
        }

        // POST: Flight_Stops/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Flight_Stops flight_Stops = db.Flight_Stops.Find(id);
            db.Flight_Stops.Remove(flight_Stops);
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
