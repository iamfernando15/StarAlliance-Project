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
    public class CancellationsController : Controller
    {
        private StarAllianceEntities1 db = new StarAllianceEntities1();

        // GET: Cancellations
        public ActionResult Index()
        {
            var cancellation = db.Cancellation.Include(c => c.Passenger).Include(c => c.Det_Ticket);
            return View(cancellation.ToList());
        }

        // GET: Cancellations/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cancellation cancellation = db.Cancellation.Find(id);
            if (cancellation == null)
            {
                return HttpNotFound();
            }
            return View(cancellation);
        }

        // GET: Cancellations/Create
        public ActionResult RegistrarCancelacion()
        {
            ViewBag.ID_Passenger = new SelectList(db.Passenger, "ID_Passenger", "Fst_Nombre");
            ViewBag.ID_Ticket = new SelectList(db.Det_Ticket, "ID_Ticket", "ID_Ticket");
            return View();
        }

        // POST: Cancellations/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult RegistrarCancelacion(int ID_Ticket, int ID_Passenger, DateTime Cancellation_Date, string Reason, bool Refund)
        {
            using (SqlConnection connection = new SqlConnection("Data Source=localhost;Initial Catalog=StarAlliance;Integrated Security=true"))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand("RegistrarCancelacion", connection))
                {
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@ID_Ticket", ID_Ticket);
                    command.Parameters.AddWithValue("@ID_Passenger", ID_Passenger);
                    command.Parameters.AddWithValue("@Cancellation_Date", Cancellation_Date);
                    command.Parameters.AddWithValue("@Reason", Reason);
                    command.Parameters.AddWithValue("@Refund", Refund);
                    command.ExecuteNonQuery();
                }
            }

            TempData["Mensaje"] = "Cancelación registrada exitosamente.";

            return RedirectToAction("Index");
        }


        // GET: Cancellations/Edit/5
        public ActionResult UpdateCancellation(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cancellation cancellation = db.Cancellation.Find(id);
            if (cancellation == null)
            {
                return HttpNotFound();
            }
            ViewBag.ID_Passenger = new SelectList(db.Passenger, "ID_Passenger", "Fst_Nombre", cancellation.ID_Passenger);
            ViewBag.ID_Ticket = new SelectList(db.Det_Ticket, "ID_Ticket", "ID_Ticket", cancellation.ID_Ticket);
            return View(cancellation);
        }

        // POST: Cancellations/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UpdateCancellation(int ID_Cancellation, int ID_Ticket, int ID_Passenger, DateTime Cancellation_Date, string Reason, bool Refund)
        {
            using (SqlConnection connection = new SqlConnection("Data Source=localhost;Initial Catalog=StarAlliance;Integrated Security=true"))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand("sp_UpdateCancellation", connection))
                {
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@ID_Cancellation", ID_Cancellation);
                    command.Parameters.AddWithValue("@ID_Ticket", ID_Ticket);
                    command.Parameters.AddWithValue("@ID_Passenger", ID_Passenger);
                    command.Parameters.AddWithValue("@Cancellation_Date", Cancellation_Date);
                    command.Parameters.AddWithValue("@Reason", Reason);
                    command.Parameters.AddWithValue("@Refund", Refund);
                    command.ExecuteNonQuery();
                }
            }

            TempData["Mensaje"] = "Cancelación actualizada exitosamente.";

            return RedirectToAction("Index");
        }


        // GET: Cancellations/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cancellation cancellation = db.Cancellation.Find(id);
            if (cancellation == null)
            {
                return HttpNotFound();
            }
            return View(cancellation);
        }

        // POST: Cancellations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Cancellation cancellation = db.Cancellation.Find(id);
            db.Cancellation.Remove(cancellation);
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
