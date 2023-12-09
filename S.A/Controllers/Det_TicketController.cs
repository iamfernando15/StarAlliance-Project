using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CrystalDecisions.CrystalReports.Engine;
using S.A.Models;

namespace S.A.Controllers
{
    public class Det_TicketController : Controller
    {
        private StarAllianceEntities1 db = new StarAllianceEntities1();

        // GET: Det_Ticket



        public ActionResult BoletoList()
        {
            return View(db.Det_Ticket.ToList());

        }


        public ActionResult ExportReport()
        {
            ReportDocument rd = new ReportDocument();
            rd.Load(Path.Combine(Server.MapPath("~/Reportes"), "Voucher_Boleto.rpt"));

            // Obtener los datos de la base de datos
            var Det_Ticket = db.Det_Ticket.ToList();

            // Convertir tipos de datos nullable a tipos no nullable
            var Det_TicketCleaned = Det_Ticket.Select(item => new
            {
                ID_Ticket = item.ID_Ticket, // Usa el valor predeterminado si es nulo
                ID_Flight = item.ID_Flight,
                ID_Flight_Class = item.ID_Flight_Class,
                ID_Passenger = item.ID_Passenger,
                ID_Seat = item.ID_Seat,
                Bought_Ticket = item.Bought_Ticket,
                Ticket_Status = item.Ticket_Status.GetValueOrDefault(),
                Total_Price = item.Total_Price.GetValueOrDefault()
            }).ToList();

            rd.SetDataSource(Det_TicketCleaned);

            Response.Buffer = false;
            Response.ClearContent();
            Response.ClearHeaders();
            try
            {
                Stream stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
                stream.Seek(0, SeekOrigin.Begin);
                return File(stream, "application/pdf", "Voucher_Boleto.pdf");
            }
            catch
            {
                throw;
            }
        }













        public ActionResult VerificarEstadoBoleto()
        {
            return View();
        }

        [HttpPost]
        public ActionResult VerificarEstadoBoleto(int idPassenger)
        {
            string mensaje = string.Empty;

            using (SqlConnection connection = new SqlConnection("Data Source=localhost ;Initial Catalog=StarAlliance;Integrated Security=true"))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand("sp_Verify_Boleto", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    // Parámetros de entrada
                    command.Parameters.Add(new SqlParameter("@ID_Passenger", idPassenger));

                    // Parámetro de salida
                    SqlParameter mensajeParameter = new SqlParameter("@Mensaje", SqlDbType.VarChar, 100);
                    mensajeParameter.Direction = ParameterDirection.Output;
                    command.Parameters.Add(mensajeParameter);

                    command.ExecuteNonQuery();

                    // Obtener el valor del parámetro de salida
                    mensaje = mensajeParameter.Value.ToString();
                }
            }

            ViewBag.Mensaje = mensaje;
            return View();
        }



 




















        public ActionResult CalculateTicketPrice()
            {
                
                return View();
            }

            [HttpPost]
            public ActionResult CalculateTicketPrice(int ID_Ticket, int id_class, int Id_destination, int ID_STOP, int id_luggage)
            {
                
                string connectionString = "Data Source=localhost;Initial Catalog=StarAlliance;Integrated Security=true";

               
                decimal total_price = 0.00m;

               
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    
                    connection.Open();

                   
                    using (SqlCommand command = new SqlCommand("FACTURA_TICKET", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                      
                        command.Parameters.AddWithValue("@ID_Ticket", ID_Ticket);
                        command.Parameters.AddWithValue("@id_class", id_class);
                        command.Parameters.AddWithValue("@Id_destination", Id_destination);
                        command.Parameters.AddWithValue("@ID_STOP", ID_STOP);
                        command.Parameters.AddWithValue("@id_luggage", id_luggage);

                      
                        command.ExecuteNonQuery();


                    command.Parameters.Add("@total_price", SqlDbType.Decimal);
                    command.Parameters["@total_price"].Direction = ParameterDirection.Output;

                    // Obtiene el precio total calculado.
                    if (command.Parameters["@total_price"].Value != DBNull.Value)
                        {
                            total_price = Convert.ToDecimal(command.Parameters["@total_price"].Value);
                        }
                    }
                }

                // Redirige a una vista para mostrar el resultado.
                ViewBag.TotalPrice = total_price;
                return View("TicketCalculated");
            }

            public ActionResult TicketCalculated()
            {
               
                return View();
            }
        
    



















    public ActionResult Index()
        {
            var det_Ticket = db.Det_Ticket.Include(d => d.Flight).Include(d => d.Flight_Class).Include(d => d.Passenger).Include(d => d.Flight_Seats);
            return View(det_Ticket.ToList());
        }

        // GET: Det_Ticket/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Det_Ticket det_Ticket = db.Det_Ticket.Find(id);
            if (det_Ticket == null)
            {
                return HttpNotFound();
            }
            return View(det_Ticket);
        }

        // GET: Det_Ticket/Create
        public ActionResult RegistrarBoleto()
        {
            ViewBag.ID_Flight = new SelectList(db.Flight, "ID_Flight", "ID_Flight");
            ViewBag.ID_Flight_Class = new SelectList(db.Flight_Class, "ID_Flight_Class", "ID_Flight_Class");
            ViewBag.ID_Passenger = new SelectList(db.Passenger, "ID_Passenger", "Fst_Nombre");
            ViewBag.ID_Seat = new SelectList(db.Flight_Seats, "ID_Seat", "Seat_Location");
            return View();
        }

        // POST: Det_Ticket/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult RegistrarBoleto(int ID_Flight, int ID_Flight_Class, int ID_Passenger, int ID_Seat, DateTime Bought_Ticket, bool Ticket_Status, decimal Total_Price)
        {
            using (SqlConnection connection = new SqlConnection("Data Source=localhost;Initial Catalog=StarAlliance;Integrated Security=true"))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand("RegistrarBoleto", connection))
                {
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@ID_Flight", ID_Flight);
                    command.Parameters.AddWithValue("@ID_Flight_Class", ID_Flight_Class);
                    command.Parameters.AddWithValue("@ID_Passenger", ID_Passenger);
                    command.Parameters.AddWithValue("@ID_Seat", ID_Seat);
                    command.Parameters.AddWithValue("@Bought_Ticket", Bought_Ticket);
                    command.Parameters.AddWithValue("@Ticket_Status", Ticket_Status);
                    command.Parameters.AddWithValue("@Total_Price", Total_Price);
                    command.ExecuteNonQuery();
                }
            }

            TempData["Mensaje"] = "Boleto registrado exitosamente.";

            return RedirectToAction("Index");
        }


        // GET: Det_Ticket/Edit/5
        public ActionResult ActualizarDetTicket(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Det_Ticket det_Ticket = db.Det_Ticket.Find(id);
            if (det_Ticket == null)
            {
                return HttpNotFound();
            }
            ViewBag.ID_Flight = new SelectList(db.Flight, "ID_Flight", "ID_Flight", det_Ticket.ID_Flight);
            ViewBag.ID_Flight_Class = new SelectList(db.Flight_Class, "ID_Flight_Class", "ID_Flight_Class", det_Ticket.ID_Flight_Class);
            ViewBag.ID_Passenger = new SelectList(db.Passenger, "ID_Passenger", "Fst_Nombre", det_Ticket.ID_Passenger);
            ViewBag.ID_Seat = new SelectList(db.Flight_Seats, "ID_Seat", "Seat_Location", det_Ticket.ID_Seat);
            return View(det_Ticket);
        }

        // POST: Det_Ticket/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ActualizarDetTicket(int ID_Ticket, int ID_Flight, int ID_Flight_Class, int ID_Passenger, int ID_Seat, DateTime Bought_Ticket, bool Ticket_Status, decimal Total_Price)
        {
            using (SqlConnection connection = new SqlConnection("Data Source=localhost;Initial Catalog=StarAlliance;Integrated Security=true"))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand("sp_UpdateDetTicket", connection))
                {
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@ID_Ticket", ID_Ticket);
                    command.Parameters.AddWithValue("@ID_Flight", ID_Flight);
                    command.Parameters.AddWithValue("@ID_Flight_Class", ID_Flight_Class);
                    command.Parameters.AddWithValue("@ID_Passenger", ID_Passenger);
                    command.Parameters.AddWithValue("@ID_Seat", ID_Seat);
                    command.Parameters.AddWithValue("@Bought_Ticket", Bought_Ticket);
                    command.Parameters.AddWithValue("@Ticket_Status", Ticket_Status);
                    command.Parameters.AddWithValue("@Total_Price", Total_Price);
                    command.ExecuteNonQuery();
                }
            }

            TempData["Mensaje"] = "Información de boleto actualizada correctamente.";

            return RedirectToAction("Mensaje");
        }


        // GET: Det_Ticket/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Det_Ticket det_Ticket = db.Det_Ticket.Find(id);
            if (det_Ticket == null)
            {
                return HttpNotFound();
            }
            return View(det_Ticket);
        }

        // POST: Det_Ticket/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Det_Ticket det_Ticket = db.Det_Ticket.Find(id);
            db.Det_Ticket.Remove(det_Ticket);
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
