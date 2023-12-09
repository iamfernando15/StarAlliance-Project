using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;
using S.A.Models;

namespace S.A.Controllers
{
    public class PassengersController : Controller
    {
        private StarAllianceEntities1 db = new StarAllianceEntities1();

        // GET: Passengers
        public ActionResult Index()
        {
            return View(db.Passenger.ToList());
        }

        // GET: Passengers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Passenger passenger = db.Passenger.Find(id);
            if (passenger == null)
            {
                return HttpNotFound();
            }
            return View(passenger);
        }


        public ActionResult CalculateRefund()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CalculateRefund(int idPassenger, string cancellationReason)
        {
            string refundMessage = "";
            decimal refundAmount = 0.00m;

            if (cancellationReason == "Situación de la aerolínea")
            {
                refundMessage = "Tu reembolso ha sido calculado exitosamente. Monto del reembolso: $" + refundAmount;
           
                
            }
            else
            {
                using (SqlConnection connection = new SqlConnection("Data Source=localhost;Initial Catalog=StarAlliance;Integrated Security=true"))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand("CalculatePassengerRefund", connection))
                {
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@ID_Passenger", idPassenger);
                    command.Parameters.AddWithValue("@Cancellation_Reason", cancellationReason);
                    command.Parameters.Add("@RefundAmount", SqlDbType.Decimal);
                    command.Parameters["@RefundAmount"].Direction = ParameterDirection.Output;

                    command.ExecuteNonQuery();

                    refundAmount = Convert.ToDecimal(command.Parameters["@RefundAmount"].Value);
                }
            }



                refundMessage = "No calificas para un reembolso en esta situación.";


            }

            ViewBag.RefundMessage = refundMessage;

            return View();
        }












        // GET: Passengers/Create
        public ActionResult RegistrarPasajero()
        {
            return View();
        }

        // POST: Passengers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]


        public ActionResult RegistrarPasajero(string Fst_Nombre, string Snd_Nombre, string Fst_Apellido, string Snd_Apellido, DateTime Birthdate, string Identification, string Phone_Number, string Adress, string Email)
        {
            using (SqlConnection connection = new SqlConnection("Data Source=localhost;Initial Catalog=StarAlliance;Integrated Security=true"))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand("RegistrarPasajero", connection))
                {
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@Fst_Nombre", Fst_Nombre);
                    command.Parameters.AddWithValue("@Snd_Nombre", Snd_Nombre);
                    command.Parameters.AddWithValue("@Fst_Apellido", Fst_Apellido);
                    command.Parameters.AddWithValue("@Snd_Apellido", Snd_Apellido);
                    command.Parameters.AddWithValue("@Birthdate", Birthdate);
                    command.Parameters.AddWithValue("@Identification", Identification);
                    command.Parameters.AddWithValue("@Phone_Number", Phone_Number);
                    command.Parameters.AddWithValue("@Adress", Adress);
                    command.Parameters.AddWithValue("@Email", Email);
                    command.ExecuteNonQuery();
                }
            }

            ViewBag.Mensaje = "Pasajero agregado exitosamente.";

            return RedirectToAction("Index");

          
        }




        // GET: Passengers/Edit/5
        public ActionResult UpdatePassenger(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Passenger passenger = db.Passenger.Find(id);
            if (passenger == null)
            {
                return HttpNotFound();
            }
            return View(passenger);
        }

        // POST: Passengers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]



        public ActionResult UpdatePassenger(int ID_Passenger, string Phone_Number, string Adress, string Email)
        {
            using (SqlConnection connection = new SqlConnection("Data Source=localhost;Initial Catalog=StarAlliance;Integrated Security=true"))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand("UpdatePassenger", connection))
                {
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@ID_Passenger", ID_Passenger);
                    command.Parameters.AddWithValue("@Phone_Number", Phone_Number);
                    command.Parameters.AddWithValue("@Adress", Adress);
                    command.Parameters.AddWithValue("@Email", Email);
                    command.ExecuteNonQuery();
                }
            }

            ViewBag.Mensaje = "Información actualizada correctamente.";

            return RedirectToAction("Index");
        }



        // GET: Passengers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Passenger passenger = db.Passenger.Find(id);
            if (passenger == null)
            {
                return HttpNotFound();
            }
            return View(passenger);
        }

        

        // POST: Passengers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Passenger passenger = db.Passenger.Find(id);
            db.Passenger.Remove(passenger);
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
