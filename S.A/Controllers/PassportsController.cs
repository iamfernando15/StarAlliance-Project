using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Services.Description;
using S.A.Models;

namespace S.A.Controllers
{
    public class PassportsController : Controller
    {
        private StarAllianceEntities1 db = new StarAllianceEntities1();

        // GET: Passports
        public ActionResult Index()
        {
            var passport = db.Passport.Include(p => p.Passenger);
            return View(passport.ToList());
        }

        // GET: Passports/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Passport passport = db.Passport.Find(id);
            if (passport == null)
            {
                return HttpNotFound();
            }
            return View(passport);
        }

        // GET: Passports/Create
        public ActionResult InsertarPasaporte()
        {
            ViewBag.ID_Passenger = new SelectList(db.Passenger, "ID_Passenger", "Fst_Nombre");
            return View();
        }

        // POST: Passports/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult InsertarPasaporte(int ID_Passenger, string Passport_Type, string Country_Code, string Passport_Num, string Nationality, string Gender, DateTime IssueDate, DateTime ExpiryDate)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection("Data Source=localhost;Initial Catalog=StarAlliance;Integrated Security=true"))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand("RegistrarPasaporte", connection))
                    {
                        command.CommandType = System.Data.CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@ID_Passenger", ID_Passenger);
                        command.Parameters.AddWithValue("@Passport_Type", Passport_Type);
                        command.Parameters.AddWithValue("@Country_Code", Country_Code);
                        command.Parameters.AddWithValue("@Passport_Num", Passport_Num);
                        command.Parameters.AddWithValue("@Nationality", Nationality);
                        command.Parameters.AddWithValue("@Gender", Gender);
                        command.Parameters.AddWithValue("@IssueDate", IssueDate);
                        command.Parameters.AddWithValue("@ExpiryDate", ExpiryDate);
                        command.ExecuteNonQuery();
                    }

                    TempData["Mensaje"] = "Pasajero agregado exitosamente.";
                }
            } catch (SqlException ex)
            {
                // Si ocurre una excepción SQL, verifica si es debido al trigger de fecha y muestra el mensaje adecuado
                if (ex.Number == 50000 && ex.Message.Contains("La fecha de emisión debe ser anterior a la fecha de vencimiento."))
                {
                    TempData["Mensaje"] = "Error: La fecha de emisión debe ser anterior a la fecha de vencimiento.";
                }
                else
                {
                    TempData["Mensaje"] = "Error al agregar el pasaporte. Por favor, verifica los datos.";
                }
            

            return RedirectToAction("Mensaje");

        }
            return RedirectToAction("Index");
        }

            
        



        // GET: Passports/Edit/5
        public ActionResult ActualizarPasaporte(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Passport passport = db.Passport.Find(id);
            if (passport == null)
            {
                return HttpNotFound();
            }
            ViewBag.ID_Passenger = new SelectList(db.Passenger, "ID_Passenger", "Fst_Nombre", passport.ID_Passenger);
            return View(passport);
        }

        // POST: Passports/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ActualizarPasaporte(int ID_Passport, int ID_Passenger, string Passport_Type, string Country_Code, string Passport_Num, string Nationality, string Gender, DateTime IssueDate, DateTime ExpiryDate)
        {
            using (SqlConnection connection = new SqlConnection("Data Source=localhost;Initial Catalog=StarAlliance;Integrated Security=true"))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand("sp_UpdatePassport", connection))
                {
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@ID_Passport", ID_Passport);
                    command.Parameters.AddWithValue("@ID_Passenger", ID_Passenger);
                    command.Parameters.AddWithValue("@Passport_Type", Passport_Type);
                    command.Parameters.AddWithValue("@Country_Code", Country_Code);
                    command.Parameters.AddWithValue("@Passport_Num", Passport_Num);
                    command.Parameters.AddWithValue("@Nationality", Nationality);
                    command.Parameters.AddWithValue("@Gender", Gender);
                    command.Parameters.AddWithValue("@IssueDate", IssueDate);
                    command.Parameters.AddWithValue("@ExpiryDate", ExpiryDate);
                    command.ExecuteNonQuery();
                }

                TempData["Mensaje"] = "Información actualizada correctamente.";
            }

            return RedirectToAction("Index");
        }


        // GET: Passports/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Passport passport = db.Passport.Find(id);
            if (passport == null)
            {
                return HttpNotFound();
            }
            return View(passport);
        }

        // POST: Passports/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Passport passport = db.Passport.Find(id);
            db.Passport.Remove(passport);
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
