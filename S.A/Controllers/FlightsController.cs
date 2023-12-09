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
    public class FlightsController : Controller
    {
        private StarAllianceEntities1 db = new StarAllianceEntities1();

        // GET: Flights


        
            public ActionResult RegistrarNumeroAsientos()
            {
                return View();
            }

            [HttpPost]
        public ActionResult RegistrarNumeroAsientos(int ID_Flight, int TotalSeats)
        {
            using (SqlConnection connection = new SqlConnection("Data Source=localhost ;Initial Catalog=StarAlliance;Integrated Security=true"))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand("RegistrarNumeroAsientos", connection))
                {
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@ID_Flight", ID_Flight);
                    command.Parameters.AddWithValue("@TotalSeats", TotalSeats);
                    command.ExecuteNonQuery();
                }

                // Actualiza el TotalSeats en la base de datos
               
                    var flight = db.Flight.FirstOrDefault(f => f.ID_Flight == ID_Flight);
                    if (flight != null)
                    {
                        flight.TotalSeats = TotalSeats;
                        db.SaveChanges();
                    }
                

                // Coloca el mensaje en ViewBag para mostrarlo en la vista
                ViewBag.Mensaje = "Número de asientos registrado exitosamente.";

                return View();
            }
        }



        public ActionResult ActualizarAsientosReservados()
            {
                return View();
            }


   
            [HttpPost]
        public ActionResult ActualizarAsientosReservados(int ID_Flight)
            {
                // Conexión a la base de datos
                string connectionString = "Data Source=localhost;Initial Catalog=StarAlliance;Integrated Security=true";

                try
                {
                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        connection.Open();

                        // Crear el comando para ejecutar el procedimiento almacenado
                        using (SqlCommand command = new SqlCommand("ActualizarAsientosReservados", connection))
                        {
                            command.CommandType = System.Data.CommandType.StoredProcedure;
                            command.Parameters.AddWithValue("@ID_Flight", ID_Flight);

                            // Ejecutar el procedimiento almacenado
                            command.ExecuteNonQuery();
                        }
                    }

                    // Redirigir a la vista que deseas después de ejecutar el procedimiento almacenado
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    // Manejar cualquier excepción que pueda ocurrir aquí
                    ViewBag.Error = "Ocurrió un error al actualizar los asientos: " + ex.Message;
                    return View();
                }
            }



















    public ActionResult Index()
        {
            var flight = db.Flight.Include(f => f.Flight_Destination).Include(f => f.Flight_Stops).Include(f => f.Flight_Luggage).Include(f => f.Flight_Origin).Include(f => f.Passenger);
            return View(flight.ToList());
        }

        // GET: Flights/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Flight flight = db.Flight.Find(id);
            if (flight == null)
            {
                return HttpNotFound();
            }
            return View(flight);
        }


        // POST: Flights/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
   

        public ActionResult InsertarFlight()
        {
            ViewBag.ID_Passenger = new SelectList(db.Passenger, "ID_Passenger", "ID_Passenger");
            ViewBag.ID_Origin = new SelectList(db.Flight_Origin, "ID_Origin", "ID_Origin");
            ViewBag.ID_Destination = new SelectList(db.Flight_Destination, "ID_Destination", "ID_Destination");
            ViewBag.ID_FlightStops = new SelectList(db.Flight_Stops, "ID_FlightStops", "ID_FlightStops");
            ViewBag.ID_Luggage = new SelectList(db.Flight_Luggage, "ID_Luggage", "ID_Luggage");

            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult InsertarFlight(int ID_Passenger, int ID_Origin, int ID_Destination, int ID_FlightStops, int ID_Luggage, int TotalSeats)
        {
            using (SqlConnection connection = new SqlConnection("Data Source=localhost;Initial Catalog=StarAlliance;Integrated Security=true"))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand("sp_InsertFlight", connection))
                {
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@ID_Passenger", ID_Passenger);
                    command.Parameters.AddWithValue("@ID_Origin", ID_Origin);
                    command.Parameters.AddWithValue("@ID_Destination", ID_Destination);
                    command.Parameters.AddWithValue("@ID_FlightStops", ID_FlightStops);
                    command.Parameters.AddWithValue("@ID_Luggage", ID_Luggage);
                    command.Parameters.AddWithValue("@TotalSeats", TotalSeats);
                    command.ExecuteNonQuery();
                }
            }

            TempData["Mensaje"] = "Pasajero agregado exitosamente.";

            return RedirectToAction("Index");
        }



        // GET: Flights/Edit/5
        public ActionResult ActualizarFlight(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Flight flight = db.Flight.Find(id);
            if (flight == null)
            {
                return HttpNotFound();
            }
            ViewBag.ID_Destination = new SelectList(db.Flight_Destination, "ID_Destination", "Airport", flight.ID_Destination);
            ViewBag.ID_FlightStops = new SelectList(db.Flight_Stops, "ID_FlightStops", "Airport", flight.ID_FlightStops);
            ViewBag.ID_Luggage = new SelectList(db.Flight_Luggage, "ID_Luggage", "Luggage_Type", flight.ID_Luggage);
            ViewBag.ID_Origin = new SelectList(db.Flight_Origin, "ID_Origin", "Airport", flight.ID_Origin);
            ViewBag.ID_Passenger = new SelectList(db.Passenger, "ID_Passenger", "Fst_Nombre", flight.ID_Passenger);
            return View(flight);
        }

        // POST: Flights/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
      
        public ActionResult ActualizarFlight(int ID_Flight, int ID_Passenger, int ID_Origin, int ID_Destination, int ID_FlightStops, int ID_Luggage, int TotalSeats)
        {
            using (SqlConnection connection = new SqlConnection("Data Source=localhost;Initial Catalog=StarAlliance;Integrated Security=true"))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand("sp_UpdateFlight", connection))
                {
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@ID_Flight", ID_Flight);
                    command.Parameters.AddWithValue("@ID_Passenger", ID_Passenger);
                    command.Parameters.AddWithValue("@ID_Origin", ID_Origin);
                    command.Parameters.AddWithValue("@ID_Destination", ID_Destination);
                    command.Parameters.AddWithValue("@ID_FlightStops", ID_FlightStops);
                    command.Parameters.AddWithValue("@ID_Luggage", ID_Luggage);
                    command.Parameters.AddWithValue("@TotalSeats", TotalSeats);
                    command.ExecuteNonQuery();
                }
            }

            TempData["Mensaje"] = "Información actualizada correctamente.";

            return RedirectToAction("Mensaje");
        }


        // GET: Flights/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Flight flight = db.Flight.Find(id);
            if (flight == null)
            {
                return HttpNotFound();
            }
            return View(flight);
        }

        // POST: Flights/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Flight flight = db.Flight.Find(id);
            db.Flight.Remove(flight);
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
