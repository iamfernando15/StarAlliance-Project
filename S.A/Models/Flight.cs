//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace S.A.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Flight
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Flight()
        {
            this.Det_Ticket = new HashSet<Det_Ticket>();
            this.Flight_Class = new HashSet<Flight_Class>();
            this.Flight_Seats = new HashSet<Flight_Seats>();
            this.Itinerario = new HashSet<Itinerario>();
        }
    
        public int ID_Flight { get; set; }
        public int ID_Passenger { get; set; }
        public int ID_Origin { get; set; }
        public int ID_Destination { get; set; }
        public int ID_FlightStops { get; set; }
        public int ID_Luggage { get; set; }
        public Nullable<int> TotalSeats { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Det_Ticket> Det_Ticket { get; set; }
        public virtual Flight_Destination Flight_Destination { get; set; }
        public virtual Flight_Stops Flight_Stops { get; set; }
        public virtual Flight_Luggage Flight_Luggage { get; set; }
        public virtual Flight_Origin Flight_Origin { get; set; }
        public virtual Passenger Passenger { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Flight_Class> Flight_Class { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Flight_Seats> Flight_Seats { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Itinerario> Itinerario { get; set; }
    }
}