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
    
    public partial class Flight_Seats
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Flight_Seats()
        {
            this.Det_Ticket = new HashSet<Det_Ticket>();
        }
    
        public int ID_Seat { get; set; }
        public int ID_Flight { get; set; }
        public string Seat_Location { get; set; }
        public string Seat_Number { get; set; }
        public Nullable<int> Assigned_Seats { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Det_Ticket> Det_Ticket { get; set; }
        public virtual Flight Flight { get; set; }
    }
}