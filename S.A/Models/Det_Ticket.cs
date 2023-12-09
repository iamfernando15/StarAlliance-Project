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
    
    public partial class Det_Ticket
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Det_Ticket()
        {
            this.Cancellation = new HashSet<Cancellation>();
            this.Itinerario = new HashSet<Itinerario>();
        }
    
        public int ID_Ticket { get; set; }
        public int ID_Flight { get; set; }
        public int ID_Flight_Class { get; set; }
        public int ID_Passenger { get; set; }
        public int ID_Seat { get; set; }
        public System.DateTime Bought_Ticket { get; set; }
        public Nullable<bool> Ticket_Status { get; set; }
        public Nullable<decimal> Total_Price { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Cancellation> Cancellation { get; set; }
        public virtual Flight Flight { get; set; }
        public virtual Flight_Class Flight_Class { get; set; }
        public virtual Passenger Passenger { get; set; }
        public virtual Flight_Seats Flight_Seats { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Itinerario> Itinerario { get; set; }
    }
}