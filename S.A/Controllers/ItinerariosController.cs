 using System;

using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;

using System.Web.Mvc;
using CrystalDecisions.CrystalReports.Engine;
using S.A.Models;

namespace S.A.Controllers      
{
    public class ItinerariosController : Controller
    {
        private StarAllianceEntities1 db = new StarAllianceEntities1();

        // GET: Itinerarios



        public ActionResult ItineraryList()
        {
            return View(db.Itinerario.ToList());

        }

        /*
        public ActionResult exportReport()
        {
            ReportDocument rd = new ReportDocument();
            rd.Load(Path.Combine(Server.MapPath("~/Reportes"), "Reporte_Itinerario.rpt"));
            rd.SetDataSource(db.Itinerario.ToList());
            Response.Buffer = false;
            Response.ClearContent();
            Response.ClearHeaders();
            try
            {
                Stream stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
                stream.Seek(0, SeekOrigin.Begin);
                return File(stream, "application/pdf", "Itinerario_Viaje.pdf");
            }
            catch
            {
                throw;
            }
        } */

        
                public ActionResult exportReport()
                {
                    ReportDocument rd = new ReportDocument();
                    rd.Load(Path.Combine(Server.MapPath("~/Reportes"), "Reporte_Itinerario.rpt"));

                    // Obtener los datos de la base de datos
                    var itinerarios = db.Itinerario.ToList();

                    // Convertir tipos de datos nullable a tipos no nullable
                    var itinerariosCleaned = itinerarios.Select(item => new
                    {
                        ID_Itinerary = item.ID_Itinerary,
                        // ... otras propiedades
                        ID_Flight = item.ID_Flight  , // Convertir nullable a no nullable
                        ID_Ticket = item.ID_Ticket  ,
                        ID_Flight_Class = item.ID_Flight_Class,
                        Airline = item.Airline,
                        Country_Origin = item.Country_Origin, // Obtener el país de origen desde Flight_Origin
                        Country_Destination = item.Country_Destination


                        // Convertir nullable a no nullable
                    }).ToList();

                    rd.SetDataSource(itinerariosCleaned);

                    Response.Buffer = false;
                    Response.ClearContent();
                    Response.ClearHeaders();
                    try
                    {
                        Stream stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
                        stream.Seek(0, SeekOrigin.Begin);
                        return File(stream, "application/pdf", "Itinerario_Viaje.pdf");
                    }
                    catch
                    {
                        throw;
                    }
                }
                









        public ActionResult Index()
        {
            var itinerario = db.Itinerario.Include(i => i.Det_Ticket).Include(i => i.Flight).Include(i => i.Flight_Class);
            return View(itinerario.ToList());
        }

        // GET: Itinerarios/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Itinerario itinerario = db.Itinerario.Find(id);
            if (itinerario == null)
            {
                return HttpNotFound();
            }
            return View(itinerario);
        }

        // GET: Itinerarios/Create
        public ActionResult LlenarItinerario()
        {
            ViewBag.ID_Ticket = new SelectList(db.Det_Ticket, "ID_Ticket", "ID_Ticket");
            ViewBag.ID_Flight = new SelectList(db.Flight, "ID_Flight", "ID_Flight");
            ViewBag.ID_Flight_Class = new SelectList(db.Flight_Class, "ID_Flight_Class", "ID_Flight_Class");

            return View();


        }
        // POST: Itinerarios/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LlenarItinerario(int ID_Flight, int ID_Ticket, string Airline, string Country_Origin, DateTime Departure_DateTime, string Country_Destination, DateTime Arrival_DateTime, int ID_Flight_Class)
        {
            using (SqlConnection connection = new SqlConnection("Data Source=localhost;Initial Catalog=StarAlliance;Integrated Security=true"))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand("LlenarItinerario", connection))
                {
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@ID_Flight", ID_Flight);
                    command.Parameters.AddWithValue("@ID_Ticket", ID_Ticket);
                    command.Parameters.AddWithValue("@Airline", Airline);
                    command.Parameters.AddWithValue("@Country_Origin", Country_Origin);
                    command.Parameters.AddWithValue("@Departure_DateTime", Departure_DateTime);
                    command.Parameters.AddWithValue("@Country_Destination", Country_Destination);
                    command.Parameters.AddWithValue("@Arrival_DateTime", Arrival_DateTime);
                    command.Parameters.AddWithValue("@ID_Flight_Class", ID_Flight_Class);
                    command.ExecuteNonQuery();
                }
            }

            TempData["Mensaje"] = "Itinerario agregado exitosamente.";

            return RedirectToAction("Index");
        }


        // GET: Itinerarios/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Itinerario itinerario = db.Itinerario.Find(id);
            if (itinerario == null)
            {
                return HttpNotFound();
            }
            ViewBag.ID_Ticket = new SelectList(db.Det_Ticket, "ID_Ticket", "ID_Ticket", itinerario.ID_Ticket);
            ViewBag.ID_Flight = new SelectList(db.Flight, "ID_Flight", "ID_Flight", itinerario.ID_Flight);
            ViewBag.ID_Flight_Class = new SelectList(db.Flight_Class, "ID_Flight_Class", "ID_Flight_Class", itinerario.ID_Flight_Class);
            return View(itinerario);
        }

        // POST: Itinerarios/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID_Itinerary,ID_Flight,ID_Ticket,Airline,Country_Origin,Departure_DateTime,Country_Destination,Arrival_DateTime,ID_Flight_Class")] Itinerario itinerario)
        {
            if (ModelState.IsValid)
            {
                db.Entry(itinerario).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ID_Ticket = new SelectList(db.Det_Ticket, "ID_Ticket", "ID_Ticket", itinerario.ID_Ticket);
            ViewBag.ID_Flight = new SelectList(db.Flight, "ID_Flight", "ID_Flight", itinerario.ID_Flight);
            ViewBag.ID_Flight_Class = new SelectList(db.Flight_Class, "ID_Flight_Class", "ID_Flight_Class", itinerario.ID_Flight_Class);
            return View(itinerario);
        }

        // GET: Itinerarios/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Itinerario itinerario = db.Itinerario.Find(id);
            if (itinerario == null)
            {
                return HttpNotFound();
            }
            return View(itinerario);
        }

        // POST: Itinerarios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Itinerario itinerario = db.Itinerario.Find(id);
            db.Itinerario.Remove(itinerario);
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
