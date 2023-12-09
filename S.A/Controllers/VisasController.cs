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
    public class VisasController : Controller
    {
        private StarAllianceEntities1 db = new StarAllianceEntities1();

        // GET: Visas
        public ActionResult Index()
        {
            var visa = db.Visa.Include(v => v.Passenger).Include(v => v.Passport);
            return View(visa.ToList());
        }

        // GET: Visas/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Visa visa = db.Visa.Find(id);
            if (visa == null)
            {
                return HttpNotFound();
            }
            return View(visa);
        }

        // GET: Visas/Create
        public ActionResult RegistrarVisa()
        {
            ViewBag.ID_Passenger = new SelectList(db.Passenger, "ID_Passenger", "Fst_Nombre");
            ViewBag.ID_Passport = new SelectList(db.Passport, "ID_Passport", "Passport_Type");
            return View();
        }

        // POST: Visas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult RegistrarVisa(int ID_Passport, int ID_Passenger, string Issuing_Postname, string Control_Number, string Visa_Num, string Visa_Type, string Visa_Class, string Entries, string Annotation, DateTime IssueDate, DateTime ExpiryDate)
        {
            using (SqlConnection connection = new SqlConnection("Data Source=localhost;Initial Catalog=StarAlliance;Integrated Security=true"))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand("RegistrarVisa", connection))
                {
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@ID_Passport", ID_Passport);
                    command.Parameters.AddWithValue("@ID_Passenger", ID_Passenger);
                    command.Parameters.AddWithValue("@Issuing_Postname", Issuing_Postname);
                    command.Parameters.AddWithValue("@Control_Number", Control_Number);
                    command.Parameters.AddWithValue("@Visa_Num", Visa_Num);
                    command.Parameters.AddWithValue("@Visa_Type", Visa_Type);
                    command.Parameters.AddWithValue("@Visa_Class", Visa_Class);
                    command.Parameters.AddWithValue("@Entries", Entries);
                    command.Parameters.AddWithValue("@Annotation", Annotation);
                    command.Parameters.AddWithValue("@IssueDate", IssueDate);
                    command.Parameters.AddWithValue("@ExpiryDate", ExpiryDate);
                    command.ExecuteNonQuery();
                }
            }

            ViewBag.Mensaje = "Pasajero agregado exitosamente.";

            return RedirectToAction("Index");
        }


        // GET: Visas/Edit/5
        public ActionResult ActualizarVisa(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Visa visa = db.Visa.Find(id);
            if (visa == null)
            {
                return HttpNotFound();
            }
            ViewBag.ID_Passenger = new SelectList(db.Passenger, "ID_Passenger", "Fst_Nombre", visa.ID_Passenger);
            ViewBag.ID_Passport = new SelectList(db.Passport, "ID_Passport", "Passport_Type", visa.ID_Passport);
            return View(visa);
        }

        // POST: Visas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]

        public ActionResult ActualizarVisa(int ID_Visa, int ID_Passport, int ID_Passenger, string Issuing_Postname, string Control_Number, string Visa_Num, string Visa_Type, string Visa_Class, string Entries, string Annotation, DateTime IssueDate, DateTime ExpiryDate)
        {
            using (SqlConnection connection = new SqlConnection("Data Source=localhost;Initial Catalog=StarAlliance;Integrated Security=true"))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand("sp_UpdateVisa", connection))
                {
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@ID_Visa", ID_Visa);
                    command.Parameters.AddWithValue("@ID_Passport", ID_Passport);
                    command.Parameters.AddWithValue("@ID_Passenger", ID_Passenger);
                    command.Parameters.AddWithValue("@Issuing_Postname", Issuing_Postname);
                    command.Parameters.AddWithValue("@Control_Number", Control_Number);
                    command.Parameters.AddWithValue("@Visa_Num", Visa_Num);
                    command.Parameters.AddWithValue("@Visa_Type", Visa_Type);
                    command.Parameters.AddWithValue("@Visa_Class", Visa_Class);
                    command.Parameters.AddWithValue("@Entries", Entries);
                    command.Parameters.AddWithValue("@Annotation", Annotation);
                    command.Parameters.AddWithValue("@IssueDate", IssueDate);
                    command.Parameters.AddWithValue("@ExpiryDate", ExpiryDate);
                    command.ExecuteNonQuery();
                }
            }

            ViewBag.Mensaje = "Información actualizada correctamente.";

            return RedirectToAction("Index");
        }

               
      

        // GET: Visas/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Visa visa = db.Visa.Find(id);
            if (visa == null)
            {
                return HttpNotFound();
            }
            return View(visa);
        }

        // POST: Visas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Visa visa = db.Visa.Find(id);
            db.Visa.Remove(visa);
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
