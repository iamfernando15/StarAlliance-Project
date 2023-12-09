using System;
using System.Collections.Generic;
using System.Configuration;
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
    public class Flight_SeatsController : Controller
    {
        private StarAllianceEntities1 db = new StarAllianceEntities1();

        // GET: Flight_Seats

 

    public ActionResult Index()
        {
            var flight_Seats = db.Flight_Seats.Include(f => f.Flight);
            return View(flight_Seats.ToList());
        }

        // GET: Flight_Seats/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Flight_Seats flight_Seats = db.Flight_Seats.Find(id);
            if (flight_Seats == null)
            {
                return HttpNotFound();
            }
            return View(flight_Seats);
        }

        // GET: Flight_Seats/Create
        public ActionResult InsertarFlightSeat()
        {
            ViewBag.ID_Flight = new SelectList(db.Flight, "ID_Flight", "ID_Flight");
            return View();
        }

        // POST: Flight_Seats/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult InsertarFlightSeat(int ID_Flight, string Seat_Location, string Seat_Number, int Assigned_Seats)
        {
            using (SqlConnection connection = new SqlConnection("Data Source=localhost;Initial Catalog=StarAlliance;Integrated Security=true"))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand("sp_InsertFlightSeat", connection))
                {
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@ID_Flight", ID_Flight);
                    command.Parameters.AddWithValue("@Seat_Location", Seat_Location);
                    command.Parameters.AddWithValue("@Seat_Number", Seat_Number);
                    command.Parameters.AddWithValue("@Assigned_Seats", Assigned_Seats);
                    command.ExecuteNonQuery();
                }
            }

            TempData["Mensaje"] = "Asiento de vuelo agregado exitosamente.";

            return RedirectToAction("Index");
        }

        // GET: Flight_Seats/Edit/5
        public ActionResult ActualizarFlightSeat(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Flight_Seats flight_Seats = db.Flight_Seats.Find(id);
            if (flight_Seats == null)
            {
                return HttpNotFound();
            }
            ViewBag.ID_Flight = new SelectList(db.Flight, "ID_Flight", "ID_Flight", flight_Seats.ID_Flight);
            return View(flight_Seats);
        }

        // POST: Flight_Seats/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ActualizarFlightSeat(int ID_Seat, int ID_Flight, string Seat_Location, string Seat_Number, int Assigned_Seats)
        {
            using (SqlConnection connection = new SqlConnection("Data Source=localhost;Initial Catalog=StarAlliance;Integrated Security=true"))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand("sp_UpdateFlightSeat", connection))
                {
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@ID_Seat", ID_Seat);
                    command.Parameters.AddWithValue("@ID_Flight", ID_Flight);
                    command.Parameters.AddWithValue("@Seat_Location", Seat_Location);
                    command.Parameters.AddWithValue("@Seat_Number", Seat_Number);
                    command.Parameters.AddWithValue("@Assigned_Seats", Assigned_Seats);
                    command.ExecuteNonQuery();
                }
            }

            TempData["Mensaje"] = "Información de asiento de vuelo actualizada correctamente.";

            return RedirectToAction("Index");
        }

        // GET: Flight_Seats/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Flight_Seats flight_Seats = db.Flight_Seats.Find(id);
            if (flight_Seats == null)
            {
                return HttpNotFound();
            }
            return View(flight_Seats);
        }

        // POST: Flight_Seats/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Flight_Seats flight_Seats = db.Flight_Seats.Find(id);
            db.Flight_Seats.Remove(flight_Seats);
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
