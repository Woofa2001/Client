//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WPFProject.Data
{
    using System;
    using System.Collections.Generic;
    
    public partial class PEOPLE
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public PEOPLE()
        {
            this.DEALS = new HashSet<DEALS>();
            this.DEALS1 = new HashSet<DEALS>();
            this.PROPOSALS = new HashSet<PROPOSALS>();
        }
    
        public int ID_PEOPLE { get; set; }
        public string SURNAME { get; set; }
        public string NAME { get; set; }
        public string MIDNAME { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DEALS> DEALS { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DEALS> DEALS1 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PROPOSALS> PROPOSALS { get; set; }
    }
}
