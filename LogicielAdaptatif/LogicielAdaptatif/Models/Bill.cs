//------------------------------------------------------------------------------
// <auto-generated>
//     Ce code a été généré à partir d'un modèle.
//
//     Des modifications manuelles apportées à ce fichier peuvent conduire à un comportement inattendu de votre application.
//     Les modifications manuelles apportées à ce fichier sont remplacées si le code est régénéré.
// </auto-generated>
//------------------------------------------------------------------------------

namespace LogicielAdaptatif.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Bill
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Bill()
        {
            this.Bill_Product = new HashSet<Bill_Product>();
        }
    
        public int id_bill { get; set; }
        public int id_user { get; set; }
        public System.DateTime bill_date { get; set; }
        public bool bill_paid { get; set; }
        public Nullable<System.DateTime> bill_date_paid { get; set; }
        public int bill_amount { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Bill_Product> Bill_Product { get; set; }
    }
}
