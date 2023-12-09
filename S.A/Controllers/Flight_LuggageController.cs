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
    public class Flight_LuggageController : Controller
    {
        private StarAllianceEntities1 db = new StarAllianceEntities1();

        // GET: Flight_Luggage
        public ActionResult Index()
        {
            var flight_Luggage = db.Flight_Luggage.Include(f => f.Passenger);
            return View(flight_Luggage.ToList());
        }

        // GET: Flight_Luggage/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Flight_Luggage flight_Luggage = db.Flight_Luggage.Find(id);
            if (flight_Luggage == null)
            {
                return HttpNotFound();
            }
            return View(flight_Luggage);
        }

        // GET: Flight_Luggage/Create
        public ActionResult InsertarFlightLuggage()
        {
            ViewBag.ID_Passenger = new SelectList(db.Passenger, "ID_Passenger", "Fst_Nombre");
            return View();
        }

        // POST: Flight_Luggage/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult InsertarFlightLuggage(int ID_Passenger, string Luggage_Type, decimal Luggage_Weight, decimal Extra_Luggage, decimal Total_Extra)
        {
            using (SqlConnection connection = new SqlConnection("Data Source=localhost;Initial Catalog=StarAlliance;Integrated Security=true"))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand("InsertFlightLuggage", connection))
                {
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@ID_Passenger", ID_Passenger);
                    command.Parameters.AddWithValue("@Luggage_Type", Luggage_Type);
                    command.Parameters.AddWithValue("@Luggage_Weight", Luggage_Weight);
                    command.Parameters.AddWithValue("@Extra_Luggage", Extra_Luggage);
                    command.Parameters.AddWithValue("@Total_Extra", Total_Extra);
                    command.ExecuteNonQuery();
                }
            }

            TempData["Mensaje"] = "Pasajero agregado exitosamente.";

            return RedirectToAction("Index");
        }


        // GET: Flight_Luggage/Edit/5
        public ActionResult ActualizarFlightLuggage(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Flight_Luggage flight_Luggage = db.Flight_Luggage.Find(id);
            if (flight_Luggage == null)
            {
                return HttpNotFound();
            }
            ViewBag.ID_Passenger = new SelectList(db.Passenger, "ID_Passenger", "Fst_Nombre", flight_Luggage.ID_Passenger);
            return View(flight_Luggage);
        }

        // POST: Flight_Luggage/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ActualizarFlightLuggage(int ID_Luggage, string Luggage_Type, decimal Luggage_Weight, decimal Extra_Luggage, decimal Total_Extra)
        {
            using (SqlConnection connection = new SqlConnection("Data Source=localhost;Initial Catalog=StarAlliance;Integrated Security=true"))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand("UpdateFlightLuggage", connection))
                {
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@ID_Luggage", ID_Luggage);
                    command.Parameters.AddWithValue("@Luggage_Type", Luggage_Type);
                    command.Parameters.AddWithValue("@Luggage_Weight", Luggage_Weight);
                    command.Parameters.AddWithValue("@Extra_Luggage", Extra_Luggage);
                    command.Parameters.AddWithValue("@Total_Extra", Total_Extra);
                    command.ExecuteNonQuery();
                }
            }

            TempData["Mensaje"] = "Información actualizada correctamente.";

            return RedirectToAction("Index");
        }


        // GET: Flight_Luggage/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Flight_Luggage flight_Luggage = db.Flight_Luggage.Find(id);
            if (flight_Luggage == null)
            {
                return HttpNotFound();
            }
            return View(flight_Luggage);
        }

        // POST: Flight_Luggage/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Flight_Luggage flight_Luggage = db.Flight_Luggage.Find(id);
            db.Flight_Luggage.Remove(flight_Luggage);
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
